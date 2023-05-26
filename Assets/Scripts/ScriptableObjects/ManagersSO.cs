using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Manager", menuName = "ScriptableObjects/Manager")]

public class ManagersSO : ScriptableObject
{
    public Sprite managersIcon;
    public Sprite managersBackIcon;
    public string managersName;
    public string managersPublicName;
    public string managersClass;
    public string managersCost;
}
