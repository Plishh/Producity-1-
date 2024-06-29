using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseMenuHandler : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefabs;
    public InputManager inputManager;

    public StructureManager structureManager;

    public Button house1, house2, house3,  house4, house5;

    private void Start() {
        house1.onClick.AddListener(()=>{
            gameObject.SetActive(false);
            inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, housesPrefabs[0].prefab);
            //Vector3Int pos = inputManager.OnMouseClick; 
            //structureManager.PlaceHouse(pos,0);



        }
        );
    }

    

}
