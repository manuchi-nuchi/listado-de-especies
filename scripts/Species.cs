using Godot;
using System;

[GlobalClass]
public partial class Species : Resource
{
    [Export] public string commonName;
    [Export] public string scientificName;
    [Export] public Texture2D image;
}
