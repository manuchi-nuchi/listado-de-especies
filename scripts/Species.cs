using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class Species : Resource
{
    [Export] public string commonName;
    [Export] public string scientificName;
    [Export] public Array<Texture2D> images;
    int currentImage = 0;

    [Export] public Color color1 = Color.FromHsv(.2f, .3f, .8f);
    [Export] public Color color2 = Color.FromHsv(.1f, .55f, .65f);
    [Export] public Color color3 = Color.FromHsv(.0f, .8f, .4f);

    public Texture2D NextImage
    {
        get
        {
            currentImage = ++currentImage % images.Count;
            return images[currentImage];
        }
    }

    public Texture2D PreviousImage
    {
        get
        {
            currentImage = (--currentImage + images.Count) % images.Count;
            return images[currentImage];
        }
    }

    public Texture2D Image
    {
        get
        {
            return images[currentImage];
        }
    }
}
