using UnityEngine;
using static DungeonCardData;

public class CardEffectPlayer : MonoBehaviour
{
    public GameObject effectAttackPrefab;
    public GameObject effectDefensePrefab;
    public GameObject effectHealPrefab;
    public GameObject effectBlockPrefab;

    public Transform playerEffectPoint;  // จุดแสดงเอฟเฟกต์สำหรับผู้เล่น
    public Transform monsterEffectPoint; // จุดแสดงเอฟเฟกต์สำหรับมอนสเตอร์

    public void PlayEffect(CardType cardType, bool isTargetMonster)
    {
        GameObject effectPrefab = null;
        Transform targetPoint = isTargetMonster ? monsterEffectPoint : playerEffectPoint;

        switch (cardType)
        {
            case CardType.Attack:
                effectPrefab = effectAttackPrefab;
                break;
            case CardType.Defense:
                effectPrefab = effectDefensePrefab;
                break;
            case CardType.Heal:
                effectPrefab = effectHealPrefab;
                break;
            case CardType.Block:
                effectPrefab = effectBlockPrefab;
                break;
        }

        if (effectPrefab != null && targetPoint != null)
        {
            GameObject effect = Instantiate(effectPrefab, targetPoint.position, Quaternion.identity);
            Destroy(effect, 2f); // ลบเอฟเฟกต์หลัง 2 วินาที (ตามความยาว Animation)
        }
    }
}
