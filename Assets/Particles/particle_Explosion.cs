//Created by Dan Wad. 22. Jan.
//Updated by Dan Wad. 23. Jan. Added comments and made variable names more readable. Added sparks
//Updated by Dan Wad. 01. Feb. removed unnecessary code. Added a variable (particleRunTime) for total particle life to destroy game object when done playing.
//todo: add burst shockwave? Needs HLSL coding to make a nice distortion effect.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_Explosion : MonoBehaviour
{
	public ParticleSystem ps_Explosion;				//Reference the Explosion Particle System.
    public ParticleSystem ps_Sparks;				//Reference the Sparks Particle System.
    public ParticleSystem ps_SmokePillar;			//Reference the Smoke Particle System.

    public float blackbodyMaxValue = 5000;			//the blackbody starting value.
	public float blackbodyMinValue = 500;			//the blackbody end value.
	public float blackbodyValue = 5000;             //Blackbody color temperature. More info: http://www.giangrandi.ch/optics/blackbody/blackbody.shtml

	public Gradient grad = new Gradient();			//Albedo Color over time Gradient.

	public float EmissiveStrength = 5;				//The strength of the Emissive Texture.

	private float timePassed;						//Shouldn't be editable.
	public float timeLeftRatio;						//this goes from 1 to 0 and should help	setting the colors of the black body value.				
	public float timeMax;							//Couldn't find an variable in the Particle System to get the duration of the particle emitter. Only duration I found was the one spawning the particles not their life time.
													//this was actually ment for the explosion particle for the blackbody color to 

    public float particleRunTime = 0;				//this is for making sure that the blackbody colors change good and shouldn't be set to the time max as the time max is the actual duration of the particle.

	public bool explodeWhenInitialized = false;		//if true this will explode the moment it's initialized.
	public bool testPlaySystem = false;				//just to test the function call
	private bool particleRunning = false;
	public bool destroyOnComplete = true;


	public void PlaySystem()						//function to activate the particle.
    {
        ps_Explosion.Play();
        ps_Sparks.Play();
        ps_SmokePillar.Play();
		particleRunning = true;
	}

    private void Awake()							//stops the particle from playing unless explodeWhenInitialized is activated.
    {
		if (!explodeWhenInitialized)
		{
			ps_Explosion.Stop();
			ps_Sparks.Stop();
			ps_SmokePillar.Stop();
		}
		else
		{
			PlaySystem();
		}
	}


	void Start()
	{
		//Albedo color might not really be important or even visible as the emissive most likely absorbs the albedo color. But, in case we want to remove the emissive texture I added this in.
		//Maybe have a null or false check on emissive to see if this should be set?
		var main = ps_Explosion.main;
		var colorLifeTime = ps_Explosion.colorOverLifetime;

		//this is to set the starting color.
		main.startColor = Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue);
		colorLifeTime.enabled = true;

		//This is to set the albedo color over lifetime. Should start at a bright white then turn quickly yellow and end with a dark red. Todo?? make the different temperature values variables?
		grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(blackbodyMaxValue), 0.0f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(blackbodyMaxValue/2), 0.5f), new GradientColorKey(Mathf.CorrelatedColorTemperatureToRGB(blackbodyMinValue), 1f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
		colorLifeTime.color = grad;
	}
	private void Update()
	{
		//activates the PlaySystem function when activating the bool. the particleRunning check is to make sure it only happens once.
		if (testPlaySystem == true && !particleRunning)
			PlaySystem();

		//Checks if the TimeMax and if the particle is running is set.
		if (timeMax != 0f && particleRunning)	
		{
			timePassed += Time.deltaTime;

			//make sure that the TimeLeftRatio doesn't drop under 0. Because we don't divide by zero.
			if ((1 - (timePassed / timeMax)) > 0f)              
				timeLeftRatio = 1 - (timePassed / timeMax);

			//make sure that the TimeLeftRatio ends at 0.
			else
				timeLeftRatio = 0f;

			//One shot particles should always delete themselves after they are done.
			if (timePassed > particleRunTime && destroyOnComplete)
				Destroy(gameObject);
			
			//reset timePassed if destroyOnComplete is false.
			else if (timePassed > particleRunTime && !destroyOnComplete)
			{
				timePassed = 0f;
				particleRunning = false;
			}

			blackbodyValue = ((blackbodyMaxValue - blackbodyMinValue) * timeLeftRatio) + blackbodyMinValue;     //This calculates the explosion and ember color value for each frame and shouldn't go under the blackbodyMinValue.

			ps_Explosion.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue) * EmissiveStrength); //Just sets the emissive color based on the temperature for the explosion.
			ps_Sparks.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", Mathf.CorrelatedColorTemperatureToRGB(blackbodyValue) * EmissiveStrength); //Just sets the emissive color based on the temperature for the sparks.
		}
	}
}