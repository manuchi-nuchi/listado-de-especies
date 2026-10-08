using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;


[GlobalClass]
public partial class SpeciesList : Resource
{
    [Export] public Array<Species> species;

    public Species this[int x]
    {
        get => species[x];
    }

    public int Count
    {
        get => species.Count;
    }
}
