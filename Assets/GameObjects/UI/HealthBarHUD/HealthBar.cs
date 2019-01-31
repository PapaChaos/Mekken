using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthBar : MonoBehaviour
{
    public Transform bar;
    public float TotalHp = 100f;
    public float CurrentHp;

    // Start is called before the first frame update
    void Start()
    {
        // Transform bar = gameObject.transform;
        //Transform bar = transform.Find("Bar"); //either use this to find the bar or use the one above if it's attached to an image.

        CurrentHp = TotalHp;
        //bar.localScale = new Vector3(.4f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage();
        }
    }
    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    void TakeDamage()
    {
        CurrentHp -= 5;
        transform.localScale = new Vector3((CurrentHp / TotalHp), 1, 1);
    }
}