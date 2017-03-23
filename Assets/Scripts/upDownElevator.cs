﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Linq;

public class upDownElevator : MonoBehaviour
{

    //get up down button
    public Button upButton;
    public float speed = 0.2F;
    private float distance = 0;

    public Vector3 endMarkerUp = new Vector3(0F, 69F, 0F);
    public Vector3 endMarkerDown = new Vector3(0F, 0.9F, 0F);

    // Moving flags
    private bool moveUp = false;
    private bool moveDown = false;

    // Voice Recognition
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        //Create keywords for keyword recognizer
        keywords.Add("up", () =>
        {
            // action to be performed when this keyword is spoken
            UpCalled();
        });

        keywords.Add("stop", () =>
        {
            // action to be performed when this keyword is spoken
            StopCalled();
        });

        keywords.Add("down", () =>
        {
            // action to be performed when this keyword is spoken
            DownCalled();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDown = false;
            moveUp = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveUp = false;
            moveDown = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            moveUp = false;
            moveDown = true;
        }

        if (moveUp == true)
        {
            if (distance < endMarkerUp.y)
            {
                transform.Translate(Vector3.up * speed, Space.Self);
                distance = distance + Vector3.up.y * speed;
            }
        }
        else if (moveDown == true)
        {
            if (distance > endMarkerDown.y)
            {
                transform.Translate(Vector3.down * speed, Space.Self);
                distance = distance + Vector3.down.y * speed;
            }
        }
    }

    void UpCalled()
    {
        print("UP");
        moveDown = false;
        moveUp = true;
    }

    void StopCalled()
    {
        print("STOP");
        moveDown = false;
        moveUp = false;
    }

    void DownCalled()
    {
        print("DOWN");
        moveDown = true;
        moveUp = false;
    }

}