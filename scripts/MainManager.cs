using Godot;
using Godot.Collections;
using System;

public partial class MainManager : Node
{
	[Export] SpeciesList list;
	[Export] Array<SpeciesLabel> labels;
	[Export] RichTextLabel scientificNameLabel;
	[Export] TextureRect image;
	int current = 0;
	int offset = -2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetNames();
	}

	void SetNames()
	{
        for (int i = 0; i < labels.Count; i++)
        {
            labels[i].NewText(list[BoundedIndex(i)].commonName);
        }

		scientificNameLabel.Text = list[current].scientificName;
		image.Texture = list[current].image;
    }

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
            if (keyEvent.Keycode == Key.Down)
            {
                current = ++current % list.Count;
				SetNames();
            }
            else if (keyEvent.Keycode == Key.Up)
            {
                current = (--current + list.Count) % list.Count;
				SetNames();
            }
        }
	}


	int BoundedIndex(int i)
	{
		return (current + offset + i + list.Count) % list.Count;
	}
}
