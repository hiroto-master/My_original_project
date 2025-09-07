using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMessageController : MonoBehaviour
{
    private Text messageText;

    private void Start()
    {
        messageText = GetComponent<Text>();
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(ShowMessageCoroutine(message));
    }
    private IEnumerator ShowMessageCoroutine(string message)
    {
        messageText.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            messageText.text += message[i];
            yield return null;
        }
    }
}
