using Godot;
using System;

public partial class Fade : Sprite2D
{
	private float currentDarkness = 1;
	private bool targetBlack = true;

	public main mainNode {get;set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ZIndex = 1;
	}

	public void FadeToBlack(){
		targetBlack = true;
	}
	public void FadeToNotBlack(){
		targetBlack = false;
	}
	float fadeTime = 2f;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(targetBlack){
			currentDarkness=1;
			// if(currentDarkness<1){
			// 	currentDarkness += (float)delta;
			// }
		}
		else{
			if(currentDarkness>0){
				currentDarkness-=(float)delta/fadeTime;
			}else{
				currentDarkness = 0;
			}
		}
		
		var col = Modulate;
		col.A = currentDarkness;
		Modulate = col;
	}
}
