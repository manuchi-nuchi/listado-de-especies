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

	[Export(PropertyHint.File, "*.tscn")] string listSelectionSceneName;


	public override void _Ready()
	{
		if (Globals.List != null)
			list = Globals.List;

		SetSpecies();
	}

	void SetSpecies()
	{
        for (int i = 0; i < labels.Count; i++)
        {
            labels[i].NewText(list[BoundedIndex(i)].commonName);
        }

		scientificNameLabel.Text = list[current].scientificName;
		image.Texture = list[current].Image;
    }

    void NextImage()
    {
        image.Texture = list[current].NextImage;
    }

    void PreviousImage()
    {
        image.Texture = list[current].PreviousImage;
    }

    public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			switch (keyEvent.Keycode)
			{
				case Key.Down:
					current = ++current % list.Count;
					SetSpecies();
					break;
				case Key.Up:
					current = (--current + list.Count) % list.Count;
					SetSpecies();
					break;
				case Key.Right:
					NextImage();
					break;
				case Key.Left:
					PreviousImage();
					break;

				case Key.Escape:
					GetTree().ChangeSceneToFile(listSelectionSceneName);
					break;
			}
        }
	}


	int BoundedIndex(int i)
	{
		return (current + offset + i + list.Count) % list.Count;
	}
}
