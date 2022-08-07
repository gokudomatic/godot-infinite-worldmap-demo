using Godot;
using System;
using godot_infinite_worldmap_demo.mono_demo.models;
using godot_infinite_worldmap_demo.mono_demo.services;

public class MapComponent : ColorRect
{
  private int mode=0;
  private float activeTimeout=0;
  private ImageTexture currentMapTexture;

  private OpenSimplexNoise colormap;

  private MapService mapService;
  public GameContext context;

  private bool paused=false;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    mapService=new MapService();
  }

  public void updatePosition() {
    mode=1;
    initialRun();
  }

  private void initialRun(){
    cancelThread();
    context.resolutionIdx=context.resolutions.Length-1;
    // worker_cancel_task=false
    updateMap();
  }

  private void updateMap(){
    this.currentMapTexture=mapService.generateImage(context,false);
    (this.Material as ShaderMaterial).SetShaderParam("BIOME_MAP",this.currentMapTexture);
  }

  private void cancelThread() {}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
