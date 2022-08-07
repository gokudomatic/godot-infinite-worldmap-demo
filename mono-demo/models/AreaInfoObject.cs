using Godot;

namespace godot_infinite_worldmap_demo.mono_demo.models
{
    public class AreaInfoObject
    {
     	public float biome;
      public float heat;
      public float moisture;
      public float altitude;
      public Color color;

      public AreaInfoObject(float biome,float heat,float moisture,float altitude,float[] color)
      {
        this.biome=biome;
        this.heat=heat;
        this.moisture=moisture;
        this.altitude=altitude;
        this.color=new Color(color[0],color[1],color[2]);
      }
    }
}