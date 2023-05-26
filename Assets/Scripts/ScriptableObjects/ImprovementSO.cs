using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Improvement", menuName = "ScriptableObjects/Improvement")]
public class ImprovementSO: ScriptableObject
{
    public Sprite improvementsIcon;
    public Sprite cardBackImage;
    public string improvementsName;
    public string improvementsPublicName;
    public string improvementsCost;
    public string improvementsValue;
    public int improvementsType;    // 0 - Множитель цены
                                    // 1 - Множитель времени

    public int improvementsTargetIndex;

}
