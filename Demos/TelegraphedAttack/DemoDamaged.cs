using Godot;
using System;

public partial class DemoDamaged : Label
{
	// Called when the node enters the scene tree for the first time.

	int damageCount  =0;
	public override void _Ready()
	{
	}

	public void _attack_do_damage(){

		damageCount++;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = "Damage Count:"+damageCount;
	}
}
