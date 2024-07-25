using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseMenuHandler : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefabs;
    public InputManager inputManager;

    public StructureManager structureManager;

    public Button house1, house2, house3, house4, house5, close;

    public GoldManager goldManager;



    private void Start()
    {
        house1.onClick.AddListener(() =>
            {
                BuildHouse(1);
            }

        );

         house2.onClick.AddListener(() =>
            {
                int houseCost = housesPrefabs[1].weight;
                Debug.Log(goldManager.Gold > houseCost);
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, housesPrefabs[1].prefab, houseCost);
                }
            }

        );

         house3.onClick.AddListener(() =>
            {
                int houseCost = housesPrefabs[2].weight;
                
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, housesPrefabs[2].prefab, houseCost);
                }
            }

        );

         house4.onClick.AddListener(() =>
            {
                int houseCost = housesPrefabs[3].weight;
                
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, housesPrefabs[3].prefab, houseCost);
                }
            }

        );

         house5.onClick.AddListener(() =>
            {
                int houseCost = housesPrefabs[4].weight;
                
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, housesPrefabs[4].prefab, houseCost);
                }
            }

        );

        close.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                ClearInputActions();
            }
        );


    }

    private void ClearInputActions()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }

    private void BuildHouse(int x){
        
                int houseCost = housesPrefabs[x-1].weight;
                
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, housesPrefabs[x-1].prefab, houseCost);
                }
            
    }

}
