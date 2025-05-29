using UnityEngine;

public class UltimateCardDropManager : MonoBehaviour
{
    [System.Serializable]
    public class UltimateCard
    {
        public string name;
        public int tier; // D (1) -> U (6)
        public int damage;
    }

    public UltimateCard[] ultimateCards;

    public string DropRandomUltimateCard()
    {
        UltimateCard drop = ultimateCards[Random.Range(0, ultimateCards.Length)];
        CardDataManager.Instance.AddCard(drop.name, 1);
        Debug.Log($"🎁 Monster ดรอป Ultimate Card: {drop.name}");
        return drop.name;
    }
}
