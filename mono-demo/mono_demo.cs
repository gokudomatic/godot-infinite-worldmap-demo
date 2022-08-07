using Godot;
using System;
using godot_infinite_worldmap_demo.mono_demo.services;
using godot_infinite_worldmap_demo.mono_demo.models;
using godot_infinite_worldmap_demo.mono_demo.services.generators;

public class mono_demo : Control
{

  private GameContext context=new GameContext();
  private int seed=0;
  private MapComponent map;

    // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    map=(MapComponent)GetNode("MapComponent");
    initNoises();
    map.context=context;
    map.updatePosition();
  }

  private void initNoises() {
    context.noises.setNoise(NoiseWorld.ELEVATION_NOISE,createOpenNoise(seed,6,60,0.5F,3.1F));
    context.noises.setNoise(NoiseWorld.MAIN_ELEVATION_NOISE,createOpenNoise(seed,9,360,1,1));
    context.noises.setNoise(NoiseWorld.MOISTURE_NOISE,createOpenNoise(seed+1,4,8,3,0.4F));
    context.noises.setNoise(NoiseWorld.HEAT_NOISE,createOpenNoise(seed+2,4,8,3,0.4F));
  }

  private INoiseGenerator createOpenNoise(int seed, int octave, float period, float persistence, float lacunarity){
    OpenSimplexNoise osn=new OpenSimplexNoise();
    osn.Seed=seed;
    osn.Octaves=octave;
    osn.Period=period;
    osn.Persistence=persistence;
    osn.Lacunarity=lacunarity;
    return new GodotNoiseGenerator(osn);
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

}
