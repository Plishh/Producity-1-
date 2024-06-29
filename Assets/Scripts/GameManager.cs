using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SVS;
using System;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public InputManager inputManager;

    public RoadManager roadManager;

    public UIController uIController;

    public StructureManager structureManager;

    public void Start(){
        uIController.OnRoadPlacement += roadPlacementHandler;
        uIController.OnHousePlacement += HousePlacementHandler;
        uIController.OnSpecialPlacement += SpecialPlacementHandler;
       
        //Debug.Log("working");
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceSpecial;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += structureManager.PlaceHouse;
    }

    private void roadPlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad; 
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }

    private void HandleMouseClick(Vector3Int position){
        //Debug.Log("handleMouseClick");
        //Debug.Log(position);
        roadManager.PlaceRoad(position);
    }

    private void Update(){
        cameraMovement.MoveCamera(new Vector3(inputManager.cameraMovementVector.x, 0,
        inputManager.cameraMovementVector.y));
    }
}
