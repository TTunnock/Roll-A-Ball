using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    int counter = 0;

    void Start()
    {
        Debug.Log("Hello World"); 
    }

    
    void Update()
    {
        //if the user presses the space button
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //increment the counter
            counter += 1;
            //display the counter to the player
            Debug.Log("Counter is: " + counter);
        }

        
    }
}
