using Godot;
using Godot.Collections;
using System;

public partial class MainManager : Node
{
	[Export] SpeciesList list;
	[Export] Array<SpeciesLabel> labels;
	[Export] RichTextLabel scientificNameLabel;
	[Export] TextureRect image;
	
	[Export] CanvasItem bg;
	[Export] CanvasItem watercolor;
	[Export] CanvasItem frame;
	[Export] float animationDuration = 0.5f;
	[Export] Curve ease;

	int current = 0;
	int offset = -2;

	[Export(PropertyHint.File, "*.tscn")] string listSelectionSceneName;


	public override void _Ready()
	{
		if (Globals.List != null)
			list = Globals.List;

		SetSpecies();
	}

	float t = Mathf.Inf, v;
	Color previous1, previous2, previous3;
    public override void _Process(double delta)
    {
		if (t > animationDuration)
			return;

		t += (float)delta;
		v = ease.Sample(t / animationDuration);
        bg.Modulate = previous1.Lerp(CurrentPlant.color1, v);
        watercolor.Modulate = previous2.Lerp(CurrentPlant.color2, v);
        frame.SelfModulate = previous3.Lerp(CurrentPlant.color3, v);
    }

	void SetSpecies()
	{
        for (int i = 0; i < labels.Count; i++)
        {
            labels[i].NewText(list[BoundedIndex(i)].commonName);
        }

		scientificNameLabel.Text = CurrentPlant.scientificName;
		image.Texture = CurrentPlant.Image;

		SetColors();
    }

	void SetColors()
	{
		t = 0;
		previous1 = bg.Modulate;
		previous2 = watercolor.Modulate;
		previous3 = frame.SelfModulate;
	}

    void NextImage()
    {
        image.Texture = CurrentPlant.NextImage;
    }

    void PreviousImage()
    {
        image.Texture = CurrentPlant.PreviousImage;
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

	Species CurrentPlant
	{
		get => list[current];
	}
}
