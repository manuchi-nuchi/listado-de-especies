using Godot;
using System;


public partial class ListButton : Node
{
	[Export] public SpeciesList list = null;
	[Export] RichTextLabel label;
	[Export] Control underline;
	[Export] float targetScaleValue = 0.005f;
	Vector2 targetScale;
	float minWidth = 512;
	[Export] float maxWidth = 62000;
	float startWidth, targetWidth;
	[Export] float animationDuration = 1;
	[Export] Curve ease;
	float t = Mathf.Inf, v;
	Vector2 size;



	public override void _Ready()
	{
		targetScale = new Vector2(targetScaleValue, targetScaleValue);

		size = underline.Size;
	}

    public override void _Process(double delta)
    {
		if (t > animationDuration)
			return;

		t += (float)delta;
		v = ease.Sample(t / animationDuration);
        size.X = Mathf.Lerp(startWidth, targetWidth, v);
		underline.Size = size;
		underline.Position = -size / 2f;

        underline.Scale = underline.Size.X <= 513 ? Vector2.Zero : targetScale;
    }

	public void Select()
	{
		t = t < animationDuration ? animationDuration - t : 0;
		startWidth = minWidth;
		targetWidth = maxWidth;
    }

	public void Deselect(bool insta = false)
	{
		if (insta)
		{
			size.X = minWidth;
			underline.Size = size;
			underline.Position = -size / 2f;
			underline.Scale = Vector2.Zero;
		}
		else
		{
			t = t < animationDuration ? animationDuration - t : 0;
			startWidth = maxWidth;
			targetWidth = minWidth;
		}
	}
}
