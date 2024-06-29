using System;
using System.Collections;
using System.Collections.Generic;
using SVS;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;

    public List<Vector3Int> temporaryPlacementPositions = new List<Vector3Int>();
    public List<Vector3Int> roadPositionsToRecheck = new List<Vector3Int>();


    public GameObject roadStraight;

    public RoadFixer roadFixer;

    private Vector3Int startPosition;
    private bool placementMode = false;

    public void Start(){
        roadFixer = GetComponent<RoadFixer>();
    }



     public void PlaceRoad(Vector3Int position){

        Debug.Log("placeroad code in roadmanager");

        if(placementManager.CheckIfPositionInBound(position) == false){
            return;
        }

        if(placementManager.CheckIfPositionIsFree(position) == false){
            return;
        }


       if(placementMode == false){
        temporaryPlacementPositions.Clear();
        roadPositionsToRecheck.Clear();

        placementMode = true;
        startPosition = position;

        temporaryPlacementPositions.Add(position);
        
        placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);
        
       } else {
        placementManager.RemoveAllTemporaryStructures();
        temporaryPlacementPositions.Clear();

        foreach(var positionsToFix in roadPositionsToRecheck){
            roadFixer.FixRoadAtPosition(placementManager, positionsToFix);
        }
        roadPositionsToRecheck.Clear();

        temporaryPlacementPositions = placementManager.GetPathBetween(startPosition, position);
        Debug.Log("temp pos count:" + temporaryPlacementPositions.Count);

        int tempPlace = 0;
        foreach(var temporaryPosition in temporaryPlacementPositions) {
            if(placementManager.CheckIfPositionIsFree(temporaryPosition) == false){
                roadPositionsToRecheck.Add(temporaryPosition);
                Debug.Log("continued");
                continue;
            }
            placementManager.PlaceTemporaryStructure(temporaryPosition, roadFixer.deadEnd, CellType.Road);
            tempPlace++;
            Debug.Log("add temp" + tempPlace);
        }
       }
        
        FixRoadPrefabs();
        //Debug.Log("FixRoad");
     }

    private void FixRoadPrefabs(){
        foreach(var temporaryPosition in temporaryPlacementPositions){
            roadFixer.FixRoadAtPosition(placementManager, temporaryPosition);

            var neighbours = placementManager.GetNeighbourOfTypeFor(temporaryPosition, CellType.Road);

            foreach(var roadPosition in neighbours){
                if(roadPositionsToRecheck.Contains(roadPosition) == false){
                roadPositionsToRecheck.Add(roadPosition);
                }
            }
        }

        foreach(var positionToFix in roadPositionsToRecheck){
            roadFixer.FixRoadAtPosition(placementManager, positionToFix);
        }
    }

    public void FinishPlacingRoad(){
        placementMode = false;
        placementManager.AddTemporaryStructureToStructureDictionary();
        if(temporaryPlacementPositions.Count > 0){
            AudioPlayer.instance.PlayPlacementSound();
        }
        temporaryPlacementPositions.Clear();
        startPosition = Vector3Int.zero;
    }
}
