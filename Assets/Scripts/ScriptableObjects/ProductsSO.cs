using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductsSO : ScriptableObject
{
    public Sprite icon;
    public Sprite cardsBackground;

    public string productName;
    public float initialCost;
    public float costMultiplier;
    public float initialTime;
    public int initialRevenue;
}
