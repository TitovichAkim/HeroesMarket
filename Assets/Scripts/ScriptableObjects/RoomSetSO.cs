using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSet", menuName = "ScriptableObjects/RoomSet")]

public class RoomSetSO : ScriptableObject
{
    public string roomName;
    public Sprite openButtonSprite;
    public Sprite closedButtonSprite;
    
    public ProductsSO[] products;
    public ManagersSO[] managers;
    public ImprovementSO[] improvements;
}
