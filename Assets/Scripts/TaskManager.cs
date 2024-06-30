using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Transform content;

    public GameObject taskBoxPrefab;
    
    //public TMP_InputField inputField;

    public TMP_InputField inputField;

    public Button close;

    private List<TaskHandler> taskList = new List<TaskHandler>();
    
    void Start()
    {
        close.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                //ClearInputActions();
            }
        );

        inputField.onSubmit.AddListener(str =>
        {
            MakeNewTaskBox(str);
        }
        );

        
    }

    private void MakeNewTaskBox(String task)
    {
        //Instantiate<TaskBoxModel>();
        GameObject item = Instantiate(taskBoxPrefab);
        item.transform.SetParent(content);
        TaskHandler itemObject = item.GetComponent<TaskHandler>();
        itemObject.setTaskInfo(task);
        taskList.Add(itemObject);
    }


}


