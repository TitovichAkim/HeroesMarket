using UnityEngine;

[CreateAssetMenu(fileName = "Improvement", menuName = "ScriptableObjects/Improvement")]
public class ImprovementSO: ScriptableObject
{
    public Sprite improvementsIcon;
    public Sprite cardBackImage;
    public string improvementsName;
    public string improvementsPublicName;
    public float improvementsCost;
    public float improvementsValue;
    public int improvementsType;    // 0 - Множитель цены
                                    // 1 - Множитель времени

    public int improvementsTargetIndex;

}
