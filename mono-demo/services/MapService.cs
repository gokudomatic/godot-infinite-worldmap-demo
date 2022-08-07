using System;
using Godot;
using godot_infinite_worldmap_demo.mono_demo.models;
using System.Collections.Generic;

namespace godot_infinite_worldmap_demo.mono_demo.services
{
    public class MapService
    {   
        
        public ImageTexture generateImage(GameContext context, bool isCancellable) {
          int zoomFactor=context.resolutions[context.resolutionIdx];
            int[] camera_zoomed_size=new int[]{context.cameraSize[0]*zoomFactor,context.cameraSize[1]*zoomFactor};

            context.currentMapArray=new List<List<float>>(camera_zoomed_size[1]);
            for (int i=0;i<camera_zoomed_size[1];i++) { context.currentMapArray.Add(null); }

            int max_thread_count=6;
            
            List<System.Threading.Thread> workerThreads = new List<System.Threading.Thread>();

            for (int i = 0; i < camera_zoomed_size[1]; i++){
              if (isCancellable && context.cancelThread) {
                return null;
              }
              
              int noise_y=i+context.getNoiseOffset()[1];
              
              get_elevation_image_line(context, isCancellable,i,noise_y,camera_zoomed_size[0],camera_zoomed_size);

              // System.Threading.Thread t = new System.Threading.Thread(() => get_elevation_image_line(context, isCancellable,i,noise_y,camera_zoomed_size[0],camera_zoomed_size));

              // workerThreads.Add(t);
              // t.Start();
              
              // if (workerThreads.Count>=max_thread_count){
              //   foreach (System.Threading.Thread d in workerThreads) d.Join();
              //   workerThreads = new List<System.Threading.Thread>();
              // }
            }
            
            // if (workerThreads.Count>0){
            //   foreach (System.Threading.Thread d in workerThreads) d.Join();
            // }
            
            StreamPeerBuffer bytes = new StreamPeerBuffer();
            
            foreach (List<float> line in context.currentMapArray){
              foreach(float d in line){
                bytes.PutFloat(d);
              }
            }
            
            Image image = new Image();
            image.CreateFromData(camera_zoomed_size[0], camera_zoomed_size[1], false, Image.Format.Rgbf, bytes.DataArray);

            ImageTexture out1 = new ImageTexture();
            out1.CreateFromImage(image, 5);
            return out1;

        }

        private void get_elevation_image_line(GameContext context,bool is_cancellable,int y,int noise_y,int line_size,int[] camera_zoomed_size){
          
          List<float> lineData=new List<float>();
          
          for (int x=0;x<line_size;x++){
            if (context.cancelThread && is_cancellable){
              return;
            }
            
            int noise_x=x+context.getNoiseOffset()[0];
            
            float main_height= getNoiseValue(context.noises.getNoise(NoiseWorld.MAIN_ELEVATION_NOISE),noise_x,noise_y);
            float height= getNoiseValue(context.noises.getNoise(NoiseWorld.ELEVATION_NOISE),noise_x,noise_y);
            float elevation=(float) (( Math.Min(Math.Pow(height,2)+height,1) + 2.0 * main_height*main_height ) / 3.0);
            
            float heat=getNoiseValue(context.noises.getNoise(NoiseWorld.HEAT_NOISE),noise_x,noise_y);
            float moisture=getNoiseValue(context.noises.getNoise(NoiseWorld.MOISTURE_NOISE),noise_x,noise_y);
            
            float biome_idx=BiomeService.getBiome(elevation,heat,moisture);
            
            float[] biome_color=getBiomeColor(biome_idx);
            
            lineData.Add(biome_color[0]);
            lineData.Add(biome_color[1]);
            lineData.Add(biome_color[2]);
            
            if(x==camera_zoomed_size[0]/2 && y==camera_zoomed_size[1]/2){
              // TODO current_area_info=new AreaInfoObject(biome_idx,heat,moisture,elevation,biome_color);
            }
          }

          context.currentMapArray[y]=lineData;
        }

        private float getNoiseValue(INoiseGenerator n,int x,int y){
          return (1.0F+n.getNoiseAt(x, y))/2.0F;
        }


        private float[] getBiomeColor(float idx){
          switch(idx){
            case 0: 
              return new float[]{0,0,0.5F};
            case 0.1F: 
              return new float[]{25/255.0F,25/255.0F,150/255.0F};
            case 0.2F:
              return new float[]{240/255.0F,240/255.0F,64/255.0F};
            case 0.25F:
              return new float[]{238/255.0F,218/255.0F,130/255.0F};
            case 0.3F:
              return new float[]{0/255.0F,220/255.0F,20/255.0F};
            case 0.35F:
              return new float[]{177/255.0F,209/255.0F,110/255.0F};
            case 0.4F:
              return new float[]{16/255.0F,160/255.0F, 0};
            case 0.45F:
              return new float[]{73/255.0F,100/255.0F, 35/255.0F};
            case 0.5F:
              return new float[]{95/255.0F,115/255.0F, 62/255.0F};
            case 0.55F:
              return new float[]{29/255.0F,73/255.0F, 40/255.0F};
            case 0.6F:
              return new float[]{0.5F,0.5F,0.5F};
            case 0.65F:
              return new float[]{96/255.0F,131/255.0F,112/255.0F};
            default:
              return new float[]{1.0F,1.0F,1.0F};
          }
        }

    }
}