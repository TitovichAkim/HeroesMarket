using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Manager", menuName = "ScriptableObjects/Manager")]

public class ManagersSO : ScriptableObject
{
    public Sprite managersIcon;
    public string managersName;
    public float managersCost;

}
