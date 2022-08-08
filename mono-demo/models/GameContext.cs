using Godot;
using System.Collections.Generic;

namespace godot_infinite_worldmap_demo.mono_demo.models
{
    public class GameContext
    {
        public bool cancelThread=false;
        public int[] cameraSize=new int[]{64,64};
        public int resolutionIdx=0;
        public int[] resolutions={16,12,8,6,4,2,1};
        public float zoom=1/8;
        public Vector2 worldOffset;

        public NoiseWorld noises=new NoiseWorld();

        public List<float>[] currentMapArray;


        public int[] getNoiseOffset(){
          int x=(int)((worldOffset.x*zoom-cameraSize[0]/2)*resolutions[resolutionIdx]);
          int y=(int)((worldOffset.y*zoom-cameraSize[1]/2)*resolutions[resolutionIdx]);

          return new int[]{x,y};
        }
    }
}