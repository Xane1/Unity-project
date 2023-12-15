using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private IntroMessage _introMessage;
    [SerializeField] private int controlMessage;
    // Start is called before the first frame update
    void Start()
    {
        _introMessage = FindObjectOfType<IntroMessage>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _introMessage.ShowControlMessage(controlMessage);
    }
}
