using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager Instance;

    // ✅ เก็บจำนวนการ์ดแต่ละประเภท
    private Dictionary<string, int> cardInventory = new Dictionary<string, int>();

    // ✅ Map วัตถุดิบเป็นการ์ด
    public Dictionary<string, string> materialToCardMap = new Dictionary<string, string>
    {
        { "Bomb", "Attack" },
        { "Herb", "Defense" },
        { "Potion", "Heal" },
        { "Rock", "Block" }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCardData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ✅ สร้างชนิดการ์ดเริ่มต้นทั้งหมดไว้ก่อน
    void InitializeCardData()
    {
        foreach (string type in new[] { "Attack", "Defense", "Heal", "Block" })
        {
            cardInventory[type] = 0;
        }
    }

    // ✅ เพิ่มจำนวนการ์ด
    public void AddCard(string cardType, int amount)
    {
        if (!cardInventory.ContainsKey(cardType))
        {
            cardInventory[cardType] = 0;
        }
        cardInventory[cardType] += amount;
        Debug.Log($"✅ เพิ่มการ์ด {cardType} +{amount} = {cardInventory[cardType]}");
    }

    // ✅ ตั้งค่าจำนวนการ์ดโดยตรง
    public void SetCardAmount(string cardType, int amount)
    {
        if (!cardInventory.ContainsKey(cardType))
        {
            cardInventory[cardType] = 0;
        }
        cardInventory[cardType] = amount;
        Debug.Log($"✅ ตั้งค่าการ์ด {cardType} = {amount}");
    }

    // ✅ ดึงจำนวนการ์ด
    public int GetCardCount(string cardType)
    {
        return cardInventory.TryGetValue(cardType, out int count) ? count : 0;
    }

    // ✅ ลบการ์ดเมื่อใช้งาน
    public void RemoveCard(string cardType, int amount)
    {
        if (cardInventory.ContainsKey(cardType))
        {
            cardInventory[cardType] = Mathf.Max(0, cardInventory[cardType] - amount);
            Debug.Log($"🗑️ ใช้การ์ด {cardType} -{amount} = {cardInventory[cardType]}");
        }
    }

    // ✅ ดึงข้อมูลการ์ดทั้งหมด (ใช้ใน UI ได้)
    public Dictionary<string, int> GetAllCardData()
    {
        return new Dictionary<string, int>(cardInventory);
    }
}
