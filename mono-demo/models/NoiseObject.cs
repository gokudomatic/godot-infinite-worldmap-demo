namespace godot_infinite_worldmap_demo.mono_demo.models
{
    public class NoiseObject
    {
      public int seed_nr;
      public int octaves;
      public float period;
      public float initial_period;
      public float persistence;
      public float lacunarity;

      public NoiseObject(int seed_nr,int octaves,float period,float persistence,float lacunarity)
      {
     		this.seed_nr=seed_nr;
        this.octaves=octaves;
        this.period=period;
        this.initial_period=period;
        this.persistence=persistence;
        this.lacunarity=lacunarity;
      }
    }
}