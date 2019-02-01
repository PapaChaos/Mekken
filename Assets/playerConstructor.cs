using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerConstructor : MonoBehaviour
{

    //TODO: this would need total re-write for multi-user, but we just hack it in for now


    bool isInitialized = false;
    public int playerNumber = 0;

    public PlayerMech mechConstruct;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized)
        {

            GameObject builder = GameObject.Find("PlayerMecha" + playerNumber);
            if(builder)
                Debug.Log("found player " + playerNumber);
            else
                Debug.Log("cant find player " + playerNumber);
        }
        
    }
}
