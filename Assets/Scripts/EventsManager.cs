using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{

    public StructureManager structureManager;

    public PlacementManager placementManager;
   public void SetFire(){

        Debug.Log("set fire now");

        ArrayList buildingList = structureManager.GetBuildingList();
        
         int totalSize = buildingList.Count;
        if (totalSize == 0)
        {
            return;
        }

        int countToTake = Math.Max(1, (int)Math.Ceiling(totalSize * 0.2));

         if (countToTake > 0 && buildingList.Count > 0)
        {
            ArrayList elementsToProcess = new ArrayList();

            for (int i = 0; i < buildingList.Count && elementsToProcess.Count < countToTake; i += 2)
            {
                elementsToProcess.Add(buildingList[i]);
            }

            foreach (var element in elementsToProcess)
            {
                // Destroy structure on each position
                placementManager.DestroyStructure((Vector3Int) element);
                buildingList.Remove(element);
            }

            // Recalculate the number of elements to take for the next iteration
            // totalSize = buildingList.Count;
            // countToTake = Math.Max(1, (int)Math.Ceiling(totalSize * 0.2));
        }
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
