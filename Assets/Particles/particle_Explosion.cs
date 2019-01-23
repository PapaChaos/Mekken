//Created by Dan Wad. 22. Jan.
//Updated by Dan Wad. 23. Jan. Added comments and made variable names more readable.

//todo: add burst shockwave and embers?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_Explosion : MonoBehaviour
{
	public ParticleSystem ps_Explosion;				//Reference the Explosion Particle System
	public ParticleSystemRenderer psr_Explosion;	//Reference the Explosion Particle System Renderer
	public float blackbodyValue = 5000;             //Blackbody color temperature. More info: http://www.giangrandi.ch/optics/blackbody/blackbody.shtml
	public Gradient grad = new Gradient();			//Albedo Color over time Gradient.

	public float EmissiveStrength = 5;				//The strength of the Emissive Texture.

	private float TimePassed;						//Shouldn't be editable.
	public float TimeLeftRatio;						//this goes from 1 to 0 and should help					
	public float TimeMax;							//Couldn't find an variable in the Particle System to get the duration of the particle emitter. Only duration I found was the one spawning the particles not their life time.

	void Start()
	{
		//Albedo color might not really be important as the emissive might 

		var main = ps_Explosion.main;
		var colorLifeTime = ps_Explosion.colorOverLifetime;

		main.startColor = Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue);
		colorLifeTime.enabled = true;

		//This is to set the albedo color over lifetime. Should start at a bright white then turn quickly yellow and end with a dark red. Todo?? make the different temperature values variables?
		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(5000), 0.0f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(2500), 0.5f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(500), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
		colorLifeTime.color = grad;
	}
	private void Update()
	{
		if (TimeMax != 0f)	//just checks if the TimeMax is set.
		{
			TimePassed += Time.deltaTime;

			if ((1 - (TimePassed / TimeMax)) > 0f)              //make sure that the TimeLeftRatio doesn't drop under 0.
				TimeLeftRatio = 1 - (TimePassed / TimeMax);
			else
				TimeLeftRatio = 0f;                             //make sure that the TimeLeftRatio ends at 0.

			blackbodyValue = (4500 * TimeLeftRatio) + 500f;     //this shouldn't go under 500.

			psr_Explosion.material.SetColor("_EmissionColor", Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue) * EmissiveStrength); //Just sets the emissive color based on the temperature of the explosion.
		}
		else
			print("You need to set the Time Max variable in "+ gameObject.transform.name); //just easy debug if someone messes up.

	}
}