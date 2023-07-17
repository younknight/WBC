using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggler : MonoBehaviour
{
    [SerializeField] GameObject popup;
    Toggle toggle;
    private void Start()
    {
        toggle = GetComponent<Toggle>();
        Toggle();
    }
    public void Toggle()
    {
        popup.SetActive(toggle.isOn);
    }
}
