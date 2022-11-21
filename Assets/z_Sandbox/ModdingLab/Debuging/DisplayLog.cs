using DataDriven;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLog : MonoBehaviour
{

    public Button openButton;

    public Text logText;
    public GameObject logPanel;

    void Awake()
    {
        Debugger.OnLogPrinting += PrintLog;
    }
    void Start()
    {
        logText.text = "";
        openButton.onClick.AddListener(ShowDebugPanel);
    }

    void PrintLog(string message)
    {
        logText.text += message;
    }
    void ShowDebugPanel()
    {
        logPanel.SetActive(!logPanel.activeSelf);
    }

}
