using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

public partial class Cutscene : Node2D
{

	[Export]
	float imageTime=4,transitionTime=1;
 	[Signal]
    public delegate void CutsceneCompleteEventHandler();
	
	List<Sprite2D> images;
	Sprite2D background ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		images = GetChildren().Where(i=>i.Name!="Background").Select(i=>(Sprite2D)i).ToList();
		background = GetNode<Sprite2D>("Background");

		images.ForEach(i=>i.Visible = false);
		background.Visible = false;
		background.ZIndex = 1;
		//STARTCUTSCENE();
	}
	bool going = false;
	public void STARTCUTSCENE(){
		Visible=true;
		going = true;
		timer = 0;
		
		background.Visible = true;
		var m = background.Modulate;
		m.A=0;
		background.Modulate = m;
		isTransitioningOut = true;
	}
	int imageIndex = -1;
	bool isTransitioningIn = false;//going into the new image
	bool isTransitioningOut = false;//going out of the last scene/image
	float timer = 0;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double triangle)
	{
		float delta = (float)triangle;
		if(!going){return;}

		timer+=delta;

		if(isTransitioningIn){
			if(timer>transitionTime){
				isTransitioningIn = false;
				timer=0;
				return;
			}

			var mod = background.Modulate;
			mod.A = 1-(timer/transitionTime);
			background.Modulate = mod;
			return;
		}

		if(isTransitioningOut){
			if(timer>transitionTime){
				isTransitioningOut = false;
				timer=0;
				if(imageIndex>=0){
					images[imageIndex].Visible = false;
				}
				imageIndex++;
				if(imageIndex>=images.Count()){
					//end
					going = false;
					EmitSignal(SignalName.CutsceneComplete);
					return;
				}
				isTransitioningIn =true;
				images[imageIndex].Visible = true;
				return;
			}

			var mod = background.Modulate;
			mod.A = (timer/transitionTime);
			background.Modulate = mod;
			return;
		}

		//not transitioning

		if(timer>imageTime){
			isTransitioningOut = true;
			timer = 0;
			return;
		}
	}
	
}
