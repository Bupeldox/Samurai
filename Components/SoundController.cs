using Godot;
using System;
using System.Collections.Generic;

public partial class SoundController : Node2D
{

	public enum Audios{
		Heavier,
		Lighter,
		Ambient,
		Bong,

	}
	List<AudioStreamPlayer2D> audios;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// List<Audios> queue = new List<
	public void playAudio(bool queue,Audios audio){
		if(queue){
				//laterrr.
				//AnimationPlayer t;
				//t.Play();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
