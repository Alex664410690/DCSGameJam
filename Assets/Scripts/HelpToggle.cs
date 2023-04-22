using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpToggle : MonoBehaviour
{
    private Image image;
    private bool pressed;
    public GameObject main;
    public GameObject help;
    public GameObject toggle;

    void Start()
    {
        image = toggle.GetComponent<Image>();
        pressed = false;
    }

    public void Press()
    {
        if (pressed == false)
        {
            image.color = new Color32(140, 140, 140, 255);
            pressed = true;
            main.SetActive(false);
            help.SetActive(true);
        }
        else
        {
            image.color = new Color32(255, 255, 255, 255);
            pressed = false;
            main.SetActive(true);
            help.SetActive(false);
        }
    }
}
