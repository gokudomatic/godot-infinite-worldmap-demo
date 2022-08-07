using godot_infinite_worldmap_demo.mono_demo.services;
using System.Collections.Generic;

namespace godot_infinite_worldmap_demo.mono_demo.models
{
    public class NoiseWorld
    {
      public const string ELEVATION_NOISE="elevation";
      public const string MAIN_ELEVATION_NOISE="mainElevation";
      public const string MOISTURE_NOISE="moisture";
      public const string HEAT_NOISE="heat";

      private Dictionary<string, INoiseGenerator> noise = new Dictionary<string,INoiseGenerator>();

      public INoiseGenerator getNoise(string name){return this.noise[name]; }

      public void setNoise(string name, INoiseGenerator value){ 
        if(!this.noise.ContainsKey(name)){
          this.noise.Add(name,value);
        } else {
          this.noise[name]=value;
        }
      }
    }
}