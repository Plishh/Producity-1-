using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    private bool isDone;
    private String taskStr;

    public GameObject taskTextPlace;
    public GameObject taskTimePlace;
    private TMP_Text taskText;
    private TMP_Text taskTime;
    public Button completed;

    private void Start() {
        taskText = GetComponentInChildren<TMP_Text>();
    }

    public void setIsDone(bool isDone){
        this.isDone = isDone;
    }

    internal void setTaskInfo(string task)
    {
        taskText.text = task;
    }

    
}
