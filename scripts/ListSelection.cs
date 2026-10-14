using Godot;
using Godot.Collections;
using System;

public partial class ListSelection : Node
{
	[Export] Array<ListButton> buttons;
	[Export] Array<CanvasItem> bg;
	Array<Tween> tweens;
	[Export] float bgFadeDuration = .2f;

	[Export(PropertyHint.File, "*.tscn")] string mainSceneName;
	Vector2I current = Globals.Current;
	int columns = 4, rows = 3;
	Color shown = new Color(1, 1, 1, 1), hidden = new Color(1, 1, 1, 0);


	public override void _Ready()
	{
		for (int i = 0; i < buttons.Count; i++)
			if (i == Current)
				buttons[i].Select();
			else
				buttons[i].Deselect(true);

		tweens = new Array<Tween>();
		for (int i = 0; i < bg.Count; i++)
			tweens.Add(null);

		for (int i = 0; i < bg.Count; i++)
		{
			HideBg(i, true);
		
			if (i == current.X)
				ShowBg(i);
		}
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
					HideBg(current.X);
                    current.X = (--current.X + columns) % columns;
					CurrentButton.Select();
					ShowBg(current.X);
                    break;
                case Key.Right:
					CurrentButton.Deselect();
					HideBg(current.X);
                    current.X = ++current.X % columns;
					CurrentButton.Select();
					ShowBg(current.X);
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


    void ShowBg(int index)
    {
		tweens[index]?.Kill();
		tweens[index] = CreateTween();
		tweens[index].TweenInterval(bgFadeDuration * .5);
		tweens[index].TweenProperty(bg[index], "modulate", shown, bgFadeDuration)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.InOut);
    }

    void HideBg(int index, bool insta = false)
    {
		if (insta)
		{
			bg[index].Modulate = hidden;
		}
		else
		{
			tweens[index]?.Kill();
			tweens[index] = CreateTween();
			tweens[index].TweenProperty(bg[index], "modulate", hidden, bgFadeDuration)
				.SetTrans(Tween.TransitionType.Sine)
				.SetEase(Tween.EaseType.InOut);
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
