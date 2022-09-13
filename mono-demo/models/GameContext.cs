using Godot;
using System.Collections.Generic;

namespace godot_infinite_worldmap_demo.mono_demo.models
{
    public class GameContext
    {
        public bool cancelThread=false;
        public int[] cameraSize=new int[]{64,64};
        public int _resolutionIdx=6;
        public int[] resolutions={16,12,8,6,4,2,1};
        public Vector2 worldOffset;
        public float _zoom=1F/8F;

        public int resolutionIdx {
          get {
            return _resolutionIdx;
          }
          set {
            _resolutionIdx=value;
            updateNoises();
          }
        }

        public float zoom { 
          get { 
            return _zoom; 
          }
          set {
            this._zoom=value;
            updateNoises();
          }
        }

        public void updateNoises() {
          noises.zoom=currentResZoom;
        }

        public float currentResZoom {
          get {
            return _zoom*resolutions[_resolutionIdx];
          }
        }

        public NoiseWorld noises=new NoiseWorld();

        public List<float>[] currentMapArray;


        public int[] getNoiseOffset(){
          int x=(int)((worldOffset.x*_zoom-cameraSize[0]/2)*resolutions[resolutionIdx]);
          int y=(int)((worldOffset.y*_zoom-cameraSize[1]/2)*resolutions[resolutionIdx]);

          return new int[]{x,y};
        }
    }
}