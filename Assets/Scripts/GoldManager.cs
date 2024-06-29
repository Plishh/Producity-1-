using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GoldManager : MonoBehaviour
{
  public int Gold {get; set;}

 [SerializeField] public GameObject goldBox;
  public TMP_Text goldText;

    private void Start() {
        Gold = 100;
        
        //sgoldText = GetComponent<TMP_Text>();
        
        
    }

    private void Update() {
        goldText.text = "Gold" + Gold.ToString(); 
    }

    public void UseGold(int cost) {
        Gold -= cost;
    }

    public void AddGold(int reward) {
        Gold += reward;
    }

}
