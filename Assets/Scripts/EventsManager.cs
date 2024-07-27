using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{

    public GameObject firePrefab;
    public StructureManager structureManager;

    public PlacementManager placementManager;
   public void DestroyBuildings(ArrayList elementsToProcess){

        

        // ArrayList buildingList = structureManager.GetBuildingList();
        
        //  int totalSize = buildingList.Count;
        // if (totalSize == 0)
        // {
        //     return;
        // }

        // int countToTake = Math.Max(1, (int)Math.Ceiling(totalSize * 0.2));

        //  if (countToTake > 0 && buildingList.Count > 0)
        // {
        //     ArrayList elementsToProcess = new ArrayList();

        //     for (int i = 0; i < buildingList.Count && elementsToProcess.Count < countToTake; i += 2)
        //     {
        //         elementsToProcess.Add(buildingList[i]);
        //     }

        if(elementsToProcess == null){
            return;
        }

            foreach (var element in elementsToProcess)
            {
                // Destroy structure on each position
                placementManager.DestroyStructure((Vector3Int) element);
                placementManager.DestroyFire((Vector3Int) element);
                
            }

            // Recalculate the number of elements to take for the next iteration
            // totalSize = buildingList.Count;
            // countToTake = Math.Max(1, (int)Math.Ceiling(totalSize * 0.2));
        //}
   }

    internal ArrayList makeList()
    {
                ArrayList buildingList = structureManager.GetBuildingList();
        
         int totalSize = buildingList.Count;
        if (totalSize == 0)
        {
            return null;
        }

        int countToTake = Math.Max(1, (int)Math.Ceiling(totalSize * 0.2));

         
            ArrayList elementsToProcess = new ArrayList();

            for (int i = 0; i < buildingList.Count && elementsToProcess.Count < countToTake; i += 2)
            {
                elementsToProcess.Add(buildingList[i]);
            };

            foreach (var element in elementsToProcess)
            {
                // remove already burning structures so they wouldnt be considered for next task
                buildingList.Remove(element);
            }

            return elementsToProcess;
        
        
    }

    internal void SetOnFire(ArrayList toBurn)
    {
        if (toBurn == null){
            return;
        }
        Debug.Log("set fire now");
        foreach (Vector3Int pos in toBurn){
            placementManager.PlaceFire(pos, firePrefab, "fire");
        }
    }
}
