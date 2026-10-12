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


	public override void _Ready()
	{
		targetScale = new Vector2(targetScaleValue, targetScaleValue);
	}

	public void Select()
	{
		underline.Scale = targetScale;
	}

	public void Deselect()
	{
		underline.Scale = Vector2.Zero;
	}
}
