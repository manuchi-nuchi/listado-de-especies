using Godot;
using System;

[GlobalClass]
public partial class Species : Resource
{
    [Export] public string commonName;
    [Export] public string scientificName;
    [Export] public Texture2D image;
    [Export] public Color color1 = Color.FromHsv(.2f, .3f, .8f);
    [Export] public Color color2 = Color.FromHsv(.1f, .55f, .65f);
    [Export] public Color color3 = Color.FromHsv(.0f, .8f, .4f);
}
