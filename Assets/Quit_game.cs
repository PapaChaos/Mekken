using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Katarina & Knut
public class Quit_game : MonoBehaviour
{

    public Image mainMenu;
    public Image background;
    public Image me;


    float timer = -1;

    byte  cval = 255;
    float fval = 1.0f;

    private void Start()
    {

        //great hack!!
        Color nextColor = new Color(cval, cval, cval);
        mainMenu.color = nextColor;
        background.color = nextColor;
        me.color = nextColor;

    }

    private void Update()
    {

        if (Time.time > timer && timer > 0)
        {
            timer = -1;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }
        else if (timer > 0)
        {
           
            fval -= Time.deltaTime;

            Color color = new Color(fval, fval, fval, 1);

            Color32 nextColor = (Color32)color; //new Color32(cval, cval, cval, 100);
            mainMenu.color = nextColor;
            background.color = nextColor;
            me.color = nextColor;
        }

    }

    public void Quit()
    {

               
        timer = Time.time + 3.0f;

        
    }
}
