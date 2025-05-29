using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public string monsterName;
    public int maxHP;
    public int damage;
    public GameObject monsterPrefab;
    public DungeonCardData.UltimateLevel ultimateLevel;
}
