using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;



public class HasFocus : MonoBehaviour, ISelectHandler
{
    public static bool hasFocus;
    public void OnSelect(BaseEventData eventData)
    {
        hasFocus = true;
        Debug.Log(this.gameObject.name + " was selected");
    }
}