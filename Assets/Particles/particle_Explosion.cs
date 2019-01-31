//Created by Dan Wad. 22. Jan.
//Updated by Dan Wad. 23. Jan. Added comments and made variable names more readable.

//todo: add burst shockwave and embers?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_Explosion : MonoBehaviour
{
	public ParticleSystem ps_Explosion;				//Reference the Explosion Particle System
	public ParticleSystem psr_Explosion;           //Reference the Explosion Particle System Renderer

    public ParticleSystem psr_Sparks;
    public ParticleSystem psr_SmokePillar;

    public float blackbodyMaxValue = 5000;			//the starting value.
	public float blackbodyMinValue = 500;			//the end value.
	public float blackbodyValue = 5000;             //Blackbody color temperature. More info: http://www.giangrandi.ch/optics/blackbody/blackbody.shtml

	public Gradient grad = new Gradient();			//Albedo Color over time Gradient.

	public float EmissiveStrength = 5;				//The strength of the Emissive Texture.

	private float TimePassed;						//Shouldn't be editable.
	public float TimeLeftRatio;						//this goes from 1 to 0 and should help					
	public float TimeMax;							//Couldn't find an variable in the Particle System to get the duration of the particle emitter. Only duration I found was the one spawning the particles not their life time.

    private float runTimeMax = 0;

    public void PlaySystem()
    {
        ps_Explosion.Play();
        psr_Explosion.Play();
        psr_Sparks.Play();
        psr_SmokePillar.Play();
        runTimeMax = TimeMax;

    }

    private void Awake()
    {
        ps_Explosion.Stop();
        psr_Explosion.Stop();
        psr_Sparks.Stop();
        psr_SmokePillar.Stop();
    }


    void Start()
	{

        ps_Explosion.Stop();            
        psr_Explosion.Stop();
        psr_Sparks.Stop();
        psr_SmokePillar.Stop();

        //Albedo color might not really be important or even visible as the emissive most likely absorbs the albedo color. But, in case we want to remove the emissive texture I added this in.
        //Maybe have a null or false check on emissive to see if this should be set?
        var main = ps_Explosion.main;
		var colorLifeTime = ps_Explosion.colorOverLifetime;

		main.startColor = Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue);
		colorLifeTime.enabled = true;

		//This is to set the albedo color over lifetime. Should start at a bright white then turn quickly yellow and end with a dark red. Todo?? make the different temperature values variables?
		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(blackbodyMaxValue), 0.0f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(blackbodyMaxValue/2), 0.5f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(blackbodyMinValue), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
		colorLifeTime.color = grad;
	}
	private void Update()
	{
		//Checks if the TimeMax is set.
		if (runTimeMax != 0f)	
		{
			TimePassed += Time.deltaTime;

			//make sure that the TimeLeftRatio doesn't drop under 0. Because we don't divide by zero.
			if ((1 - (TimePassed / runTimeMax)) > 0f)              
				TimeLeftRatio = 1 - (TimePassed / runTimeMax);

			//make sure that the TimeLeftRatio ends at 0.
			else
				TimeLeftRatio = 0f;                             

			blackbodyValue = ((blackbodyMaxValue - blackbodyMinValue) * TimeLeftRatio) + blackbodyMinValue;     //this shouldn't go under 500.

			psr_Explosion.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue) * EmissiveStrength); //Just sets the emissive color based on the temperature of the explosion.
			psr_Sparks.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue) * EmissiveStrength);
		}
		
	}
}