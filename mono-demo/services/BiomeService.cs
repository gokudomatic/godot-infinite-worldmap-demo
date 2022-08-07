namespace godot_infinite_worldmap_demo.mono_demo.services
{

  public class BiomeService
  {

    private static float COLDEST = 0.05F;
    private static float COLDER = 0.18F;
    private static float COLD = 0.4F;
    private static float WARM = 0.5F;
    private static float WARMER = 0.7F;
 
    private static float DRYEST = 0.27F;
    private static float DRYER = 0.4F;
    private static float DRY = 0.6F;
    private static float WET = 0.8F;
    private static float WETTER = 0.9F;
    private static float WETTEST = 1.0F;
 
    private static float cDeepWater=0.0F;
    private static float cShallowWater=0.1F;
    private static float cSand=0.2F;
    private static float cDesert=0.25F;
    private static float cGrass=0.3F;
    private static float cSavanna=0.35F;
    private static float cForest=0.4F;
    private static float cSeasonalForest=0.45F;
    private static float cBorealForest=0.5F;
    private static float cRainForest=0.55F;
    private static float cRock=0.6F;
    private static float cTundra=0.65F;
    private static float cSnow=0.7F;
 
    private static float altDeepWater=0.45F;
    private static float altShallowWater=0.48F;
    private static float altSand=0.5F;
    private static float altGrass=0.63F;
    private static float altForest=0.75F;
    private static float altRock=0.88F;
    private static float altSnow=1F;


          
      


    public static float getBiome(float height,float heat,float moisture){
      float result=height;
      if(height<altSand)
        {return getWaterBiotop(height,moisture,heat);}
      else if(height<altForest)
        {return getMainlandBiotop(moisture,heat);}
      else if(height<altRock)
        {return cRock;}
      else
        {return cSnow;}
    }

    private static float getColdBiotop(float moisture){
      if(moisture<DRYER)
        {return cGrass;}
      else if (moisture<DRY)
        {return cForest;}
      else
        {return cBorealForest;}
    }

    private static float getWarmBiotop(float moisture){
      if(moisture<DRYER)
        {return cGrass;}
      else if(moisture<WET)
        {return cForest;}
      else if(moisture<WETTER)
        {return cSeasonalForest;}
      else
        {return cRainForest;}
    }

    private static float getHotBiotop(float moisture){
      if(moisture<DRYER)
        {return cDesert;}
      else if(moisture<WET)
        {return cSavanna;}
      else
        {return cRainForest;}
    }

    private static float getMainlandBiotop(float moisture,float heat) {
      if (heat<COLDEST)
        {return cSnow;}
      else if(heat<COLDER)
        {return cTundra;}
      else if(heat<COLD)
        {return getColdBiotop(moisture);}
      else if(heat<WARMER)
        {return getWarmBiotop(moisture);}
      else
        {return getHotBiotop(moisture);}
    }

    private static float getBeachBiotop(float moisture,float heat) {
      if (heat<COLDER)
        {return cTundra;}
      else if (heat<WARMER)
        {return cGrass;
      } else {
        if(moisture<DRYER)
          {return cDesert;}
        else if(moisture<WET)
          {return cSavanna;}
        else
          {return cGrass;}
      }
    }

    private static float getWaterBiotop(float height, float moisture, float heat) {
      if(height<altDeepWater)
        {return cDeepWater;}
      else if(height<altShallowWater)
        {return cShallowWater;}
      else
        {return getBeachBiotop(moisture,heat);}
    }


  }
}