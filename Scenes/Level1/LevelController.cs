using Godot;
using System;

public partial class LevelController : Node
{
	[Signal]
	public delegate void LevelEndEventHandler();

	AnimationPlayer animationPlayer;
	Cutscene cutscene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cutscene = GetNode<Cutscene>("Cutscene");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

	}

	public void _handle_cutscene_end(){
		EmitSignal(SignalName.LevelEnd,null);
	}
	public void _on_animation_player_animation_finished(string animName){
		GD.Print("animationEnd");
		cutscene.STARTCUTSCENE();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
