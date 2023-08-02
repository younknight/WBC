using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    void Start()
    {
        GetComponent<DialogueTextManager>().Talk();       
    }

}
