using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductsSO : ScriptableObject
{
    public Sprite icon;
    public Sprite cardsBackground;

    public string productName;
    public string initialCost;
    public float costMultiplier;
    public string initialTime;
    public int initialRevenue;
}
