using Godot;
using System;

public partial class SpeciesLabel : Control
{
	[Export] RichTextLabel label;

	public void NewText(string text)
	{
		label.Text = text;
	}
}
