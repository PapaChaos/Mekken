using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_Explosion : MonoBehaviour
{
	public ParticleSystem ps;
	public ParticleSystemRenderer psr;
	public float blackbodyValue = 5000;
	public Gradient grad = new Gradient();

	public float EmissiveStrength = 5;

	public float TimePassed;
	public float TimeLeft;
	public float TimeMax;

	void Start()
	{
		var main = ps.main;
		var colorLifeTime = ps.colorOverLifetime;

		main.startColor = Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue);
		colorLifeTime.enabled = true;

		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(5000), 0.0f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(2500), 0.5f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(500), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
		colorLifeTime.color = grad;
	}
	private void Update()
	{
		TimePassed += Time.deltaTime;
		if (TimePassed == 0)
			TimePassed = 0.01f;
		TimeLeft = 1-(TimePassed/TimeMax);

		blackbodyValue = (4500 * TimeLeft) + 500f;

		psr.material.SetColor("_EmissionColor", Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue) * EmissiveStrength); //"_EmisColor"

	}
}
