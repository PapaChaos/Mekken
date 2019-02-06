using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;



public class ColorChange : Button
{


    // <JoakimR> Control the color and font size between button states....

    public void Update()
    {

      
        Button button = this.GetComponent<Button>();
        Text text = (Text)button.GetComponentInChildren<Text>();

        ColorBlock colorBlock = this.colors;
      
        if (this.currentSelectionState == SelectionState.Normal)
        {
            text.color = colorBlock.normalColor;
            text.fontSize = 22;
        }
        else if (this.currentSelectionState == SelectionState.Highlighted)
        {
            text.color = colorBlock.highlightedColor;
            text.fontSize = 27;
        }
        else if (this.currentSelectionState == SelectionState.Pressed)
        {
            text.color = colorBlock.pressedColor;
            text.fontSize = 22;
        }
        else if (this.currentSelectionState == SelectionState.Disabled)
        {
            text.color = colorBlock.disabledColor;
        }



    }
}