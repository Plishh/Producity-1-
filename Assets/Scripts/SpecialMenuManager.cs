using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpecialMenuManager : MonoBehaviour
{
     public StructurePrefabWeighted[] specialPrefabs;
    public InputManager inputManager;

    public StructureManager structureManager;

    public Button house1, house2, close;

    public GoldManager goldManager;

     private void Start()
    {
        house1.onClick.AddListener(() =>
            {
                int houseCost = specialPrefabs[0].weight;
                
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, specialPrefabs[0].prefab, houseCost);
                }
            }

        );

         house2.onClick.AddListener(() =>
            {
                int houseCost = specialPrefabs[1].weight;
                Debug.Log(goldManager.Gold > houseCost);
                if (goldManager.Gold >= houseCost)
                {
                    gameObject.SetActive(false);
                    inputManager.OnMouseClick += pos => structureManager.PlaceHouse(pos, specialPrefabs[1].prefab, houseCost);
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

}
