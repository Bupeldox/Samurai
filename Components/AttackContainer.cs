using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IAttackable{

	void ATTACK();
}

public partial class AttackContainer : Node2D
{

	List<IAttackable> attackables;	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		attackables = GetChildren().Where(i=>i is IAttackable).Select(i=>(IAttackable)i).ToList(); 
	}

	public void AW(int index){
		attackables[index].ATTACK();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
