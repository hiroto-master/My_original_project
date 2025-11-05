using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventMessageController : MonoBehaviour
{
    public Image background;
    public Text messageText;
    private bool isNextButtonClicked = false;
    private Coroutine runningCoroutine = null;
    private string currentMessage = null;
    
    public FPMovement movement;
    
    private void Start()
    {
        
    }
    private IEnumerator ShowMessageCoroutine(string message)
    {
        messageText.text = "";
        
        for (int i = 0; i < message.Length; i++)
        {
            messageText.text += message[i];
            yield return new WaitForSecondsRealtime(0.1f);
        }
        runningCoroutine = null;
    }
    public IEnumerator EventCoroutine(string[] message)　//これを実行させる
    {
        movement.isOpenInventory = true;
        Time.timeScale = 0;
        for (int i = 0; i < message.Length; i++)
        { 
            currentMessage = message[i];
            runningCoroutine = StartCoroutine(ShowMessageCoroutine(message[i]));
            yield return new WaitUntil(() => isNextButtonClicked);
            isNextButtonClicked = false;
        }
        background.gameObject.SetActive(false);
        Time.timeScale = 1;
        movement.isOpenInventory = false;
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
