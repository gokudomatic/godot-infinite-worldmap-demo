using Godot;

namespace godot_infinite_worldmap_demo.mono_demo.services.generators
{
    public class GodotNoiseGenerator:INoiseGenerator
    {
        private OpenSimplexNoise noise;

        private float initialPeriod;

        public int seed { get { return this.noise.Seed; }set {this.noise.Seed=value; } }
        int octave { get { return this.noise.Octaves; }set {this.noise.Octaves=value; } }
        public float period { get { return this.noise.Period; }set {this.noise.Period=value; } }
        float persistence { get { return this.noise.Persistence; }set {this.noise.Persistence=value; } }
        float lacunarity { get { return this.noise.Lacunarity; }set {this.noise.Lacunarity=value; } }

        public GodotNoiseGenerator(OpenSimplexNoise noise)
        {
          this.noise=noise;
          initialPeriod=noise.Period;
        }

        public float getNoiseAt(int x,int y) {
          return this.noise.GetNoise2d(x,y);
        }

        public float zoom { get{
          return this.noise.Period;
        } set {
          this.noise.Period=initialPeriod*value;
          System.Console.WriteLine(value);
        }}
    }
}