using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstructionPanel : MonoBehaviour
{
    public GameObject instructionPanel;

    IEnumerator Start()
    {
        //Turn on instruction panel
        instructionPanel.SetActive(true);
        //Wait for 5 secs.
        yield return new WaitForSeconds(5);
        //Turn instruction panel off
        instructionPanel.SetActive(false);

    }
}

   

