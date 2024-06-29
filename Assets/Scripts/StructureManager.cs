using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SVS;
using Unity.VisualScripting;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefabs, specialPrefabs;
    public PlacementManager placementManager;

    private int[] houseWeights, specialWeights;

    public GoldManager goldManager;

    public InputManager inputManager;

    private void Start(){
        houseWeights = housesPrefabs.Select(prefab => prefab.weight).ToArray();    
        specialWeights = specialPrefabs.Select(prefab => prefab.weight).ToArray();
    }

   /*public void PlaceHouse(Vector3Int position){
        if(CheckPointBeforePlacement(position)){
            int randomIndex = GetRandomWeightIndex(houseWeights);
            placementManager.PlaceObjectOnMap(position, housesPrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    public void PlaceSpecial(Vector3Int position){
        if(CheckPointBeforePlacement(position)){
            int randomIndex = GetRandomWeightIndex(specialWeights);
            placementManager.PlaceObjectOnMap(position, specialPrefabs[randomIndex].prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }*/

    public void PlaceHouse(Vector3Int position , GameObject prefab, int houseCost){
        if(CheckPointBeforePlacement(position)){
            //int randomIndex = GetRandomWeightIndex(houseWeights);
            goldManager.UseGold(houseCost);
            placementManager.PlaceObjectOnMap(position, prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
        ClearInputActions();
    }

    public void PlaceSpecial(Vector3Int position, GameObject prefab, int houseCost){
        if(CheckPointBeforePlacement(position)){
            goldManager.UseGold(houseCost);
            placementManager.PlaceObjectOnMap(position, prefab, CellType.Structure);
            AudioPlayer.instance.PlayPlacementSound();
        }
    }

    private int GetRandomWeightIndex(float[] weights)
    {
        float sum = 0f;
        for(int i = 0; i < weights.Length; i++){
            sum += weights[i];
        }

        float randomValue = UnityEngine.Random.Range(0, sum );
        float tempsum = 0;
        for(int i = 0; i  < weights.Length; i++){
            if(randomValue >= tempsum && randomValue < tempsum + weights[i]){
                return i;
            }
            tempsum += weights[i];
        }
        return 0;
    }

    private bool CheckPointBeforePlacement(Vector3Int position)
    {
        if(placementManager.CheckIfPositionInBound(position) == false){
            Debug.Log("position not in bounds");
            return false;
        }
        if(placementManager.CheckIfPositionIsFree(position) == false){
            Debug.Log("position is occupied");
            return false;
        }
        if (placementManager.GetNeighbourOfTypeFor(position, CellType.Road).Count <= 0){
            Debug.Log("no roads around");
            return false;
        }

        return true;
    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }
}

[Serializable]

public struct StructurePrefabWeighted{
    public GameObject prefab;
    //[Range(1,0)]
    public int weight;
}