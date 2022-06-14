shader_type canvas_item;
uniform sampler2D MAIN_ELEVATION_MAP;
uniform sampler2D ELEVATION_MAP;
uniform sampler2D MOISTURE_MAP;
uniform sampler2D HEAT_MAP;
uniform sampler2D COLOR_MAP;
uniform sampler2D WATER_MAP;


uniform int color_map_offsets_n = 0;

const float PI = 3.14159265358979323846;
const float UNCERTAINTY = 1e-2;

const float COLDEST = 0.05;
const float COLDER = 0.18;
const float COLD = 0.4;
const float WARM = 0.6;
const float WARMER = 0.8;

const float DRYEST = 0.27;
const float DRYER = 0.4;
const float DRY = 0.6;
const float WET = 0.8;
const float WETTER = 0.9;
const float WETTEST = 1.0;

const float cDeepWater=0f;
const float cShallowWater=0.1f;
const float cSand=0.2f;
const float cDesert=0.25f;
const float cGrass=0.3f;
const float cSavanna=0.35f;
const float cForest=0.4f;
const float cSeasonalForest=0.45f;
const float cBorealForest=0.5f;
const float cRainForest=0.55f;
const float cRock=0.6f;
const float cTundra=0.65f;
const float cSnow=0.7f;


const float altDeepWater=0.4f;
const float altShallowWater=0.48f;
const float altSand=0.5f;
const float altGrass=0.63f;
const float altForest=0.75f;
const float altRock=0.8f;
const float altSnow=1f;

uniform vec2 heat_map_minmax = vec2(0.0, 1.0);
uniform vec2 moisture_map_minmax = vec2(0.0, 1.0);

float normalized(float x, vec2 minmax) {
	return (x - minmax.x) / (minmax.y - minmax.x);
}

float getColdBiotop(float moisture) {
	if(moisture<DRYER) {
		return cGrass;
	} else if(moisture<DRY) {
		return cForest;
	} else {
		return cBorealForest;
	}
}

float getWarmBiotop(float moisture) {
	if(moisture<DRYER) {
		return cGrass;
	} else if(moisture<WET) {
		return cForest;
	} else if(moisture<WETTER) {
		return cSeasonalForest;
	} else {
		return cRainForest;
	}
}

float getHotBiotop(float moisture) {
	if(moisture<DRYER) {
		return cDesert;
	} else if(moisture<WET) {
		return cSavanna;
	} else {
		return cRainForest;
	}
}

float getMainlandBiotop(float moisture, float heat) {
	if (heat<COLDEST) {
		return cSnow;
	} else if(heat<COLDER) {
		return cTundra;
	} else if(heat<COLD) {
		return getColdBiotop(moisture);
	} else if(heat<WARM) {
		return getWarmBiotop(moisture);
	} else {
		return getHotBiotop(moisture);
	}
}

float getWaterBiotop(float height, float moisture, float heat) {
	if(height<altDeepWater){
		return cDeepWater;
	} else if(height<altShallowWater){
		return cShallowWater;
	} else {
		return cShallowWater;
//		if(moisture<DRYER){
//			return cDesert;
//		} else {
//			return getMainlandBiotop(moisture, heat);
//		}
	}
}

void fragment(){
	float main_height=texture(MAIN_ELEVATION_MAP, UV).x;
	//main_height=1.0-0.2/main_height;
    float height = texture(ELEVATION_MAP, UV).x;
//	float finalHeight=( pow(height,2) + 2f * main_height*main_height ) / 3f;
	float finalHeight=( min(pow(height,2)+height,1) + 2f * main_height*main_height ) / 3f;
	float heat = texture(HEAT_MAP, UV).x;//( texture(HEAT_MAP, UV).x + (1.0 - texture(MAIN_ELEVATION_MAP, UV).x )) / 2f ;
	float moisture = texture(MOISTURE_MAP, UV).x;
	
	if(finalHeight<altSand) {
		finalHeight=getWaterBiotop(finalHeight, moisture, heat);
	} else if(finalHeight<altForest) {
		finalHeight=getMainlandBiotop(moisture,heat);
	} else if(finalHeight<altRock) {
		finalHeight=cRock;
	} else {
		finalHeight=cSnow;
	}
	
	
	// centered cursor
	if(abs(0.5-UV.x)<0.001&&abs(0.5-UV.y)<0.001){
		COLOR=vec4(1f,1f,1f,1f);
	} else {
		COLOR = texture(COLOR_MAP, vec2(finalHeight, 0));
	}
	
	if(abs(0.25-finalHeight)<0.05){
		COLOR=vec4(0f,0f,0f,1f);
	} 
	
	//COLOR = vec4(finalHeight,0.0f,0.0f,1f);
}