using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Debugger : MonoBehaviour
{
    [SerializeField] TMP_Text debugText;
    [SerializeField] TMP_Text stackText;
    private void OnEnable()
    {
        Application.logMessageReceivedThreaded += LogHandler;
    }

    private void LogHandler(string condition, string stackTrace, LogType type)
    {
        debugText.text = condition;
        stackText.text = stackTrace;
    }
}
