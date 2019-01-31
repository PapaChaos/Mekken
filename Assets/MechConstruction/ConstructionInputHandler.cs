//Created by Dan Wad. 26.01-19 to handle UI input.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionInputHandler : MonoBehaviour
{
	public GameObject Section;
	public Button SectionStart;
	private Selectable SectionCurrentTarget;

	//Inactive and highlighted sprite
	public Sprite nothighlighted;
	public Sprite highlighted;
	public Sprite Checkmark;

	public enum activeController { Keyboard1, Keyboard2, GamePad1, GamePad2 };
	public activeController playerController;

	public float HoverScale = 1.2f;
	public Vector3 HoveredScale;

	//mainly only for check mark. Might want to find alternative way to do this? Maybe check for buttons in the grids instead?
	public MechConstructor mc;

	#region InputManager input names in lists.
	//Keyboard Inputs	| Todo: add these to inputs instead of prewritten strings?
	public float axisDeadZone;

	public List<string> Keyboard1Inputs;
	public List<string> Keyboard2Inputs;

	//Gamepad Inputs	| Todo: add these to inputs instead of prewritten strings?
	public List<string> Gamepad1Inputs;
	public List<string> Gamepad2Inputs;

	public List<string> currentActiveInputs;
	#endregion

	//Is one input done? If so needs to be reset to register new input.
	private bool directionalInputDone = false;
	private bool TimerStarted = false;
	private float ResetTimer = 0f;
	private enum UIDirection { Up, Down, Left, Right};

	void Start()
    {
		HoveredScale = new Vector3(HoverScale, HoverScale, HoverScale);
		//Sets the section starts as current targets.
		SectionCurrentTarget = SectionStart;
		SectionCurrentTarget.image.sprite = highlighted;
		SectionCurrentTarget.transform.localScale = HoveredScale;

		switch (playerController)
		{
			case activeController.Keyboard1:
				foreach(string s in Keyboard1Inputs)
				{
					currentActiveInputs.Add(s);
				}
				//currentActiveInputs.AddRange(Keyboard1Inputs);
				break;

			case activeController.Keyboard2:
				foreach (string s in Keyboard2Inputs)
				{
					currentActiveInputs.Add(s);
				}
				//currentActiveInputs.AddRange(Keyboard2Inputs);
				break;
			case activeController.GamePad1:
				currentActiveInputs.AddRange(Keyboard1Inputs);
				break;

			case activeController.GamePad2:
				currentActiveInputs.AddRange(Keyboard2Inputs);
				break;
			default:
				print("something messed up in getting inputs for UI Construction menu.");
				break;
		}
	}

	private void Update()
	{
		GetInput();
	}

	//The shortest I could get the menu controls was in this way.
	void PlayerDirection(UIDirection dir)
	{
		SectionCurrentTarget.image.sprite = nothighlighted;
		SectionCurrentTarget.transform.localScale = new Vector3(1f, 1f, 1f);

		//SectionCurrentTarget.GetComponentInParent<GridLayoutGroup>().cellSize = new Vector2(75f,75f);

		switch (dir)
		{
			case UIDirection.Up:
				if (SectionCurrentTarget.navigation.selectOnUp)
					{
						SectionCurrentTarget = SectionCurrentTarget.navigation.selectOnUp;
						directionalInputDone = true;
					}
					break;

			case UIDirection.Down:
				if (SectionCurrentTarget.navigation.selectOnDown)
				{
					SectionCurrentTarget = SectionCurrentTarget.navigation.selectOnDown;
					directionalInputDone = true;
				}
				break;

			case UIDirection.Right:
				if (SectionCurrentTarget.navigation.selectOnRight)
				{
					SectionCurrentTarget = SectionCurrentTarget.navigation.selectOnRight;
					directionalInputDone = true;
				}
				break;

			case UIDirection.Left:
				if (SectionCurrentTarget.navigation.selectOnLeft)
				{
					SectionCurrentTarget = SectionCurrentTarget.navigation.selectOnLeft;
					directionalInputDone = true;
				}
				break;
			}
				SectionCurrentTarget.image.sprite = highlighted;
		SectionCurrentTarget.transform.localScale = HoveredScale;
		//SectionCurrentTarget.GetComponentInParent<GridLayoutGroup>().cellSize = new Vector2(85f, 85f);
	}

	#region Player Controls
	void GetInput()
	{
		if (!directionalInputDone)
		{
			if (Input.GetAxis(currentActiveInputs[0]) > axisDeadZone)
			{
				PlayerDirection(UIDirection.Up);
			}

			else if (Input.GetAxis(currentActiveInputs[0]) < -axisDeadZone)
			{
				PlayerDirection(UIDirection.Down);
			}

			if (Input.GetAxis(currentActiveInputs[1]) > axisDeadZone)
			{
				PlayerDirection(UIDirection.Right);
			}

			else if (Input.GetAxis(currentActiveInputs[1]) < -axisDeadZone)
			{
				PlayerDirection(UIDirection.Left);
			}
		}
		else if (directionalInputDone)
		{
			//this makes it so that the player must let go of the directional buttons or analog stick to get to move again.
			/*if (Input.GetAxis(currentActiveInputs[1]) < (axisDeadZone/3) && Input.GetAxis(currentActiveInputs[1]) > -(axisDeadZone / 3) && Input.GetAxis(currentActiveInputs[0]) < (axisDeadZone / 3) && Input.GetAxis(currentActiveInputs[0]) > -(axisDeadZone / 3))
			{
				directionalInputDone = false;
			}*/

			//This one is to make the player able to hold a button direction and make it automatically move in that direction every 0.3 second. It also feels more fluid.
			
			if (!TimerStarted)
			{
				ResetTimer = 0f;
				TimerStarted = true;
			}

			ResetTimer += Time.deltaTime;

			if (ResetTimer > 0.35f || Input.GetAxis(currentActiveInputs[1]) < 0.10f && Input.GetAxis(currentActiveInputs[1]) > -0.10f && Input.GetAxis(currentActiveInputs[0]) < 0.10f && Input.GetAxis(currentActiveInputs[0]) > -0.1f)
			{
				directionalInputDone = false;
				TimerStarted = false;
				ResetTimer = 0f;
			}
			
		}
		if (Input.GetButtonDown(currentActiveInputs[3]))
		{
			Button bt = SectionCurrentTarget.gameObject.GetComponent<Button>();

			//really naughty way of adding and removing checkmarks. 
			//It checks the button's transform parent (that is a grid) and get all the children with button components and checks if the buttons is the target button. 
			//If not then set the buttons to have a transparent image if true then set the checkmark.

			foreach (Button b in bt.transform.parent.GetComponentsInChildren<Button>())
			{
				Debug.Log(b);
				//this is only to check if the button has an child and that child has an image (Mainly to check if it's the fight button or not).
				if (b.transform.GetChild(0).GetChild(0).GetComponent<Image>())
				{
					if (b != bt)
					{
						b.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = nothighlighted;
					}
					else
						b.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Checkmark;
				}
			}
			bt.onClick.Invoke();
		}
	}
	#endregion
}
