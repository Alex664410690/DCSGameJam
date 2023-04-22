using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float speed = 10f;
    private float timer;
    public float time = 0.1f;

    private int mode = 1;
    private float timerFive = 5f;
    public GameObject arrow;

    void Start()
    {
        arrow = GameObject.FindGameObjectWithTag("arrow");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mode == 1)
        {
            if (Input.GetKey("w"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("s"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("a"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(1, 0, 0);
                    timer = time;
                }
            }
            if (Input.GetKey("d"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    timer = time;
                }
            }
        }
        if (mode == 2)
        {
            if (Input.GetKey("a"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("d"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("s"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(1, 0, 0);
                    timer = time;
                }
            }
            if (Input.GetKey("w"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    timer = time;
                }
            }
        }
        if (mode == 3)
        {
            if (Input.GetKey("s"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("w"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("d"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(1, 0, 0);
                    timer = time;
                }
            }
            if (Input.GetKey("a"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    timer = time;
                }
            }
        }
        if (mode == 4)
        {
            if (Input.GetKey("d"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("a"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(0, 0, 1);
                    timer = time;
                }
            }
            if (Input.GetKey("w"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position - new Vector3(1, 0, 0);
                    timer = time;
                }
            }
            if (Input.GetKey("s"))
            {
                if (timer < 0)
                {
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    timer = time;
                }
            }
        }
        timer -= Time.deltaTime;
        timerFive -= Time.deltaTime;
        if (timerFive < 0)
        {
            timerFive = 5;
            mode = mode + 1;
            arrow.transform.Rotate(new Vector3(90, 0));
        }
        if (mode == 5)
        {
            mode = 1;
        }
    }
}
