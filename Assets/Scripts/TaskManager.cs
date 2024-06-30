using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Transform contentNotDone;
    public Transform contentDone;

    public GameObject taskBoxPrefab;
    
    //public TMP_InputField inputField;

    public TMP_InputField inputField;

    public Button close;

    private List<TaskHandler> taskList = new List<TaskHandler>();

    public GoldManager goldManager;
    
    void Start()
    {
        close.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                //ClearInputActions();
            }
        );

        inputField.onEndEdit.AddListener(MakeNewTaskBox);

        
    }

    private void MakeNewTaskBox(String task)
    {
        
        //Instantiate<TaskBoxModel>();
        GameObject item = Instantiate(taskBoxPrefab);
        item.transform.SetParent(contentNotDone);

        //how to get the taskhandler that is inside the
        TaskHandler itemObject = item.GetComponentInChildren<TaskHandler>();
        itemObject.setTaskInfo(task);
        taskList.Add(itemObject);
    }

    public void CheckTask(TaskHandler taskHandler){
        //goldManager.AddGold(50);
        taskHandler.transform.SetParent(contentDone, false);
        taskList.Remove(taskHandler);
        
    }


}


