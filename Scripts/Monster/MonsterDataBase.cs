using System.Collections.Generic;
using UnityEngine;

public class MonsterDatabase : MonoBehaviour
{
    public static MonsterDatabase Instance;

    public List<MonsterData> monsterList = new List<MonsterData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public MonsterData GetMonsterByName(string name)
    {
        return monsterList.Find(m => m.monsterName == name);
    }

    public MonsterData GetRandomMonster()
    {
        return monsterList[Random.Range(0, monsterList.Count)];
    }
}
