using Godot;
using System;
using System.Threading.Tasks;

public partial class SceneTransitionAnimation : Node
{
	[Export] float fadeDuration = 1;
	[Export] float pauseDuration = .5f;
	[Export] Curve ease;
	[Export] CanvasItem target;
	ShaderMaterial material;


    public override void _Ready()
    {
		material = target.Material as ShaderMaterial;

		Actions.ChangeScene += PlaySceneTransition;
    }


	async Task FadeIn()
	{
        Place();
        await Animation(1, 0);
	}

	async Task FadeOut()
	{
        Place();
        await Animation(0, 1);
	}

	async Task Pause()
	{
		await ToSignal(GetTree().CreateTimer(pauseDuration), SceneTreeTimer.SignalName.Timeout);
	}


	async void PlaySceneTransition()
	{
		Globals.ChangingScene = true;

		await FadeIn();

		ChangeScene();

		await Pause();

        await FadeOut();

		Globals.ChangingScene = false;
	}

	void Place()
	{
        target.GetParent<Control>().Position = Globals.TransitionPosition;
    }

	void ChangeScene()
	{
        GetTree().ChangeSceneToFile(Globals.NewScenePath);
    }



    float t, v, starTime;
	async Task Animation (float from, float to)
	{
		t = 0;
		starTime = Time.GetTicksMsec();

		while (t < fadeDuration)
		{
			t = (Time.GetTicksMsec() - starTime) / 1000f;
			v = Mathf.Lerp(from, to, ease.Sample(t / fadeDuration));
			material.SetShaderParameter("openness", v);

			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
		}

        material.SetShaderParameter("openness", to);
    }
}
