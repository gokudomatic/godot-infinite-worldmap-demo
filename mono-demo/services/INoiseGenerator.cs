namespace godot_infinite_worldmap_demo.mono_demo.services
{
    public interface INoiseGenerator
    {
      float getNoiseAt(int x,int y);

      int seed { get;set; }
    }
}