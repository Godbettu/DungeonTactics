using UnityEngine;

public enum UltimateCardTier { D, C, B, A, S, U }

[CreateAssetMenu(fileName = "UltimateCard", menuName = "Card/UltimateCard")]
public class UltimateCard : ScriptableObject
{
    public UltimateCardTier tier;
    public string cardName;
    public Sprite artwork;
    public int damage;
}