using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventMessageController : MonoBehaviour
{
    private Text messageText;
    private bool isNextButtonClicked = false;
    private Coroutine runningCoroutine = null;
    private string currentMessage = null;
    
    private void Start()
    {
        messageText = GetComponent<Text>();
        
        string[] conversation = new string[]
        {
            "こんにちは！",
            "これはサンプルメッセージです。",
            "コルーチンは正常に起動しました。"
        };
        StartCoroutine(EventCoroutine(conversation));
    }
    private IEnumerator ShowMessageCoroutine(string message)
    {
        messageText.text = "";
        
        for (int i = 0; i < message.Length; i++)
        {
            messageText.text += message[i];
            yield return new WaitForSeconds(0.1f);
        }
        runningCoroutine = null;
    }
    private IEnumerator EventCoroutine(string[] message)　//これを実行させる
    {
        for (int i = 0; i < message.Length; i++)
        { 
            currentMessage = message[i];
            runningCoroutine = StartCoroutine(ShowMessageCoroutine(message[i]));
            yield return new WaitUntil(() => isNextButtonClicked);
            isNextButtonClicked = false;
        }
 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                messageText.text = currentMessage;
                runningCoroutine = null;
            }
            else
            {
                isNextButtonClicked = true;
            }
        }

    }
}
