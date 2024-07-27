using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement, OnStructureDestroy;
    public Button placeRoadButton, placeHouseButton, placeSpecialButton, todoButton, deleteButton;
    public Color outlineColor;
    List<Button> buttonList;

    public GameObject todoMenu;

    public TaskManager taskManager;



    private void Start(){

        todoMenu.SetActive(true);
        taskManager.HideTodo();

        buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();

        });
        placeHouseButton.onClick.AddListener(() =>
        {

            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            //buildHouseMenu.SetActive(true);
            OnHousePlacement?.Invoke();

        });
        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            //buildSpecialMenu.SetActive(true);
            OnSpecialPlacement?.Invoke();

        });

        deleteButton.onClick.AddListener(() =>
            {
                ResetButtonColor();
                ModifyOutline(deleteButton);
                OnStructureDestroy?.Invoke();
            }
        );

        todoButton.onClick.AddListener(() =>
        {
            taskManager.ShowTodo();
        }
        );
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (Button button in buttonList){
            button.GetComponent<Outline>().enabled = false;
        }
    }
}

