using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class main : Node2D
{
	[Export]
	PackedScene[] Levels;
	

	Node loadedScene;
	int currentSceneIndex = 0;
	List<Node> loadedLevels;
	
	[Export]
 	Fade fade;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		LoadScene(0);
	}

	private void LoadScene(int index){
		
		//positive mod pls
		index = (index+(Levels.Count()-1))%(Levels.Count()-1);//loop
		fade.FadeToBlack();
		fade.FadeToNotBlack();

		if(loadedScene!=null){
			loadedScene.QueueFree();
		}

		var ins = Levels[index].Instantiate();
		AddChild(ins);
		if(ins is LevelController){
			((LevelController)ins).mainNode = this;
		}

		loadedScene = ins;
		currentSceneIndex = index;
	}
	float fadeAmount = 1;//1 = black
	bool targetBlack = false;
	public void OnLevelEnd(){
		LoadScene(currentSceneIndex+1);
	}
    public override void _Input(InputEvent @event)
    {
		if(Input.IsActionJustPressed("DevNextLevel")){
			LoadScene(currentSceneIndex+1);
		}
		if(Input.IsActionJustPressed("DevPrevLevel")){
			LoadScene(currentSceneIndex-1);
		}

        base._Input(@event);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
