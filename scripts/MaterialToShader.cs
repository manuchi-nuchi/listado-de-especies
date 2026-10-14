using Godot;
using System;

public partial class MaterialToShader : TextureRect
{
	[Export] Texture2D gradient;


	public override void _Ready()
	{
		ShaderMaterial material = (ShaderMaterial)Material.Duplicate();
		material.SetShaderParameter("gradient", gradient);
		Material = material;
	}
}
