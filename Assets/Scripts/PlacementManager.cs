using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> temporaryRoadObjects = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structureDictionary = new Dictionary<Vector3Int,StructureModel>();
    private Dictionary<Vector3Int, StructureModel> fireDictionary = new Dictionary<Vector3Int, StructureModel>();

    void Start()
    {
     placementGrid = new Grid(width, height );
    }
    internal bool CheckIfPositionInBound(Vector3Int position)
    {
        if(position.x>= 0 && position.x <= width && position.z >= 0 && position.z <= height){
            return true;
        }
        return false;
    }

    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    internal void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
     {
        placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateANewStructure(position, structurePrefab, type); 
        temporaryRoadObjects.Add(position, structure);
        //GameObject structure = Instantiate(structurePrefab, position, Quaternion.identity); 
     }

    private StructureModel CreateANewStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject structure = new GameObject(type.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var StructureModel = structure.AddComponent<StructureModel>();
        StructureModel.CreatModel(structurePrefab);
        return StructureModel;
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation){
        if(temporaryRoadObjects.ContainsKey(position)){
            temporaryRoadObjects[position].SwapModel(newModel, rotation);
        } else if(structureDictionary.ContainsKey(position)){
            structureDictionary[position].SwapModel(newModel, rotation);
        }
    }

 
    

    internal CellType[] GetNeighbourTypesFor(Vector3Int temporaryPosition)
    {
       return placementGrid.GetAllAdjacentCellTypes(temporaryPosition.x, temporaryPosition.z);
    }

    internal List<Vector3Int> GetNeighbourOfTypeFor(Vector3Int temporaryPosition, CellType type)
    {
        var neighbourVertices = placementGrid.GetAdjacentCellsOfType(temporaryPosition.x , temporaryPosition.z, type);
        List<Vector3Int> neighbours = new List<Vector3Int>();
        foreach(var neighbour in neighbourVertices){
            neighbours.Add(new Vector3Int(neighbour.X, 0, neighbour.Y));
        }
        return neighbours;
    }

    internal void RemoveAllTemporaryStructures()
    {
         foreach(var structure in temporaryRoadObjects.Values){
            var position = Vector3Int.RoundToInt(structure.transform.position);
            placementGrid[position.x, position.z] = CellType.Empty; 
            Destroy(structure.gameObject);
         }
         temporaryRoadObjects.Clear(); 
    }

    internal List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.z), new Point(endPosition.x, endPosition.z));
        List<Vector3Int> result = new List<Vector3Int>();
        foreach(Point point in resultPath){
            result.Add(new Vector3Int(point.X, 0 , point.Y));
        }
        return result;
    }

    internal void AddTemporaryStructureToStructureDictionary()
    {
        foreach(var structure in temporaryRoadObjects){
            structureDictionary.Add(structure.Key, structure.Value);
            DestroyNatureAt(structure.Key);
        }
        temporaryRoadObjects.Clear();
    }

    internal void PlaceObjectOnMap(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        Debug.Log(position.ToString());
        placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateANewStructure(position, structurePrefab, type); 
        structureDictionary.Add(position,  structure);
        DestroyNatureAt(position);
    }

    private void DestroyNatureAt(Vector3Int position)
    {
        RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f),
         transform.up, Quaternion.identity, 1f, 1<<LayerMask.NameToLayer("Nature"));
         foreach(var hit in hits){
            //raycast hit is a gameobject with collider as boxcast get all gameobject with a collider this can access the gameobject back to destroy it
            Destroy(hit.collider.gameObject);
         }
    }

    public void DestroyStructure(Vector3Int position){
        //StructureModel Destroyed but structure still there
        StructureModel structure = structureDictionary[position];
        structureDictionary.Remove(position);
        structure.DestroyStructure();
        placementGrid[position.x, position.z] = CellType.Empty;
    }

    public void PlaceFire(Vector3Int position, GameObject structurePrefab, String name)
    {
        GameObject fire = new GameObject(name);
        
        fire.transform.SetParent(transform);
        fire.transform.localPosition = position;
        var StructureModel = fire.AddComponent<StructureModel>();
        StructureModel.CreatModel(structurePrefab);
        fireDictionary.Add(position, StructureModel);
        
    }

    public void DestroyFire(Vector3Int pos){
        
        StructureModel fire = fireDictionary[pos];
        fireDictionary.Remove(pos);
        fire.DestroyStructure();
    }
}
