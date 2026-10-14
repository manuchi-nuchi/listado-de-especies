using Godot;
using Godot.Collections;
using System;

public partial class ListSelection : Node
{
	[Export] Array<ListButton> buttons;
	[Export(PropertyHint.File, "*.tscn")] string mainSceneName;
	Vector2I current = Globals.Current;
	int columns = 4, rows = 3;


	public override void _Ready()
	{
		for (int i = 0; i < buttons.Count; i++)
			if (i == Current)
				buttons[i].Select();
			else
				buttons[i].Deselect(true);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey && eventKey.Pressed && !eventKey.Echo)
		{
			switch (eventKey.Keycode)
			{
                case Key.Left:
					CurrentButton.Deselect();
                    current.X = (--current.X + columns) % columns;
					CurrentButton.Select();
                    break;
                case Key.Right:
					CurrentButton.Deselect();
                    current.X = ++current.X % columns;
					CurrentButton.Select();
                    break;
                case Key.Up:
					CurrentButton.Deselect();
                    current.Y = (--current.Y + rows) % rows;
					CurrentButton.Select();
                    break;
                case Key.Down:
					CurrentButton.Deselect();
                    current.Y = ++current.Y % rows;
					CurrentButton.Select();
                    break;
				case Key.Enter:
					if (CurrentButton.list != null)
					{
						Globals.List = CurrentButton.list;
						Globals.Current = current;
						GetTree().ChangeSceneToFile(mainSceneName);
					}
					break;
            }
		}
    }

	int VtoI()
	{
		return current.X + current.Y * columns;
	}

	int Current
	{
		get => VtoI();
	}

	ListButton CurrentButton
	{
		get => buttons[Current];
	}
}
