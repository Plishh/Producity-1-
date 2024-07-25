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

    public TMP_InputField deadline;
    public TMP_Text feedbackText;

    public Button deleteButton;

    private DateTime alarmTime;

    

    private void Start() {
        taskText = GetComponentInChildren<TMP_Text>();
        completed.onClick.AddListener(setIsDone);
        GameObject goldManagerObject = GameObject.FindGameObjectWithTag("GoldManager");
        goldManager = goldManagerObject.GetComponent<GoldManager>();
        GameObject taskManagerObject = GameObject.FindGameObjectWithTag("TaskManager");
        taskManager = taskManagerObject.GetComponent<TaskManager>();
        deleteButton.onClick.AddListener(() => Destroy(gameObject));
        if (deadline != null)
        {
            deadline.onEndEdit.AddListener(OnTimeInputEnd);
        }
    }

     void OnTimeInputEnd(string input)
    {
        // Check if input is a valid time format (HHMM)
        if (input.Length == 4 && int.TryParse(input, out int timeOfDay))
        {
            int hours = timeOfDay / 100;
            int minutes = timeOfDay % 100;

            if (hours >= 0 && hours < 24 && minutes >= 0 && minutes < 60)
            {
                // Valid time
                feedbackText.text = "";
                //$"Valid Time: {hours:D2}:{minutes:D2}";
                DateTime now = DateTime.Now;
                alarmTime = new DateTime(now.Year, now.Month, now.Day, hours, minutes, 0);

                if (alarmTime < now)
                {
                    // If the time has already passed today, set for the next day
                    alarmTime = alarmTime.AddDays(1);
                }
                StartCoroutine(AlarmCoroutine(alarmTime));

            }
            else
            {
                // Invalid time range
                feedbackText.text = "Invalid Time: Enter a time between 0000 and 2359";
            }
        }
        else
        {
            // Invalid format
            feedbackText.text = "Invalid Format: Enter time in HHMM format";
        }
    }


 IEnumerator AlarmCoroutine(DateTime targetTime)
    {
        Debug.Log(DateTime.Now);
        while (DateTime.Now < targetTime.AddHours(-2))
        {
            yield return null; // Wait until the next frame
             
        }

        // Alarm goes off
        feedbackText.text = "complete this or there will be consequences";

        while (DateTime.Now < targetTime.AddHours(-1))
        {
            yield return null; // Wait until the next frame

        }
        
        //
    }

    public void setIsDone(){
        //this.isDone = true;
        goldManager.AddGold(50);
        
        tick.SetActive(true);
        taskManager.CheckTask(this);
        feedbackText.text = "";

    }

    public void setTaskInfo(string task)
    {
        taskText.text = task;
    }

    
}
