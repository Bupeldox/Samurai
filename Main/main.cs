using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class main : Node2D
{
	[Export]
	PackedScene[] Levels;
	
	[Export]
	Label titleLabel;

	Node loadedScene;
	int currentSceneIndex = 0;
	List<Node> loadedLevels;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadScene(0);
	}

	private void LoadScene(int index){

		index = index%(Levels.Count()-1);//loop


		if(loadedScene!=null){
			loadedScene.QueueFree();
		}

		var ins = Levels[index].Instantiate();
		AddChild(ins);

		titleLabel.Text = "Title: "+Levels[index].ResourcePath;

		loadedScene = ins;
		currentSceneIndex = index;

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
