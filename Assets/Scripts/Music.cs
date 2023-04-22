using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static int mode;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "gameaudio")
        {
            if (mode == 0)
            {
                source.volume = 0;
            }
            if (mode == 1)
            {
                source.volume = 0.15f;
            }
            if (mode == 2)
            {
                source.volume = 0.3f;
            }
            if (mode == 3)
            {
                source.volume = 0.45f;
            }
        }
        else
        {
            if (mode == 0)
            {
                source.volume = 0;
            }
            if (mode == 1)
            {
                source.volume = 0.2f;
            }
            if (mode == 2)
            {
                source.volume = 0.4f;
            }
            if (mode == 3)
            {
                source.volume = 0.6f;
            }
        }
    }
}
