using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonCardData", menuName = "Cards/DungeonCard")]
public class DungeonCardData : ScriptableObject
{
    public enum CardType
    {
        Attack,
        Defense,
        Heal,
        Block,
        Ultimate
    }

    public enum UltimateLevel
    {
        D, // 15 Damage
        C, // 20 Damage
        B, // 25 Damage
        A, // 30 Damage
        S, // 40 Damage
        U  // 50 Damage
    }

    [Header("Basic Info")]
    public string cardName;
    public CardType cardType;
    [TextArea]
    public string description;
    public Sprite cardIcon;

    [Header("Effect Values")]
    public int damage;        // For Attack and Ultimate
    public int healAmount;    // For Heal

    [Header("Ultimate Info")]
    public bool isUltimate;
    public UltimateLevel ultimateLevel;

    public void ApplyEffect(Player player, Monster monster)
    {
        switch (cardType)
        {
            case CardType.Attack:
                monster.TakeDamage(damage);
                break;

            case CardType.Defense:
                player.SetReflectMode(true);
                break;

            case CardType.Heal:
                player.Heal(healAmount);
                break;

            case CardType.Block:
                player.SetBlockMode(true);
                break;

            case CardType.Ultimate:
                int ultimateDamage = GetUltimateDamage();
                monster.TakeDamage(ultimateDamage);
                break;
        }
    }

    private int GetUltimateDamage()
    {
        switch (ultimateLevel)
        {
            case UltimateLevel.D: return 15;
            case UltimateLevel.C: return 20;
            case UltimateLevel.B: return 25;
            case UltimateLevel.A: return 30;
            case UltimateLevel.S: return 40;
            case UltimateLevel.U: return 50;
            default: return 0;
        }
    }
}
