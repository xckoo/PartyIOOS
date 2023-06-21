﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAudio : MonoBehaviour
{
    AudioSource wheelAudio;
    // Start is called before the first frame update
    void Start()
    {
        wheelAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick")
        {
            wheelAudio.Play();

        }
    }
}
