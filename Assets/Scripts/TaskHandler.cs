using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    //private bool isDone;
    private String taskStr;

    public GameObject taskTextPlace;
    public GameObject taskTimePlace;
    public TMP_Text taskText;
    private TMP_Text taskTime;
    public Button completed;
    public GameObject tick;
    public TaskManager taskManager;
    public GoldManager goldManager;

    private void Start() {
        taskText = GetComponentInChildren<TMP_Text>();
        completed.onClick.AddListener(setIsDone);
        GameObject goldManagerObject = GameObject.FindGameObjectWithTag("GoldManager");
        goldManager = goldManagerObject.GetComponent<GoldManager>();
        GameObject taskManagerObject = GameObject.FindGameObjectWithTag("TaskManager");
        taskManager = taskManagerObject.GetComponent<TaskManager>();
    }

    public void setIsDone(){
        //this.isDone = true;
        goldManager.AddGold(50);
        
        tick.SetActive(true);
        taskManager.CheckTask(this);

    }

    public void setTaskInfo(string task)
    {
        taskText.text = task;
    }

    
}
