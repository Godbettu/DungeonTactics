using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, int> cardInventory = new Dictionary<string, int>();

    [SerializeField] InventoryUI inventoryUI;

    void Start()
    {
        LoadFromCardDataManager();
        inventoryUI?.SetInventoryData(cardInventory);
    }
    public void OpenInventory()
    {
        SceneTracker.SetLastScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Inventory");
    }

    void LoadFromCardDataManager()
    {
        Dictionary<string, int> data = CardDataManager.Instance?.GetAllCardData();
        if (data == null) return;

        cardInventory.Clear();

        foreach (var kvp in data)
        {
            cardInventory[kvp.Key] = kvp.Value;
        }
    }

    public void AddCard(string cardType, int amount)
    {
        if (cardInventory.ContainsKey(cardType))
        {
            cardInventory[cardType] += amount;
        }
        else
        {
            cardInventory[cardType] = amount;
        }

        Debug.Log($"✅ Inventory เพิ่ม {cardType} จำนวน {amount} (รวมเป็น {cardInventory[cardType]})");

        inventoryUI?.UpdateUI();
    }

    public int GetCardCount(string cardType)
    {
        return cardInventory.TryGetValue(cardType, out int count) ? count : 0;
    }

    public void BackToLastScene()
    {
        int lastSceneIndex = SceneTracker.GetLastScene();

        if (lastSceneIndex != -1)
        {
            SceneManager.LoadScene(lastSceneIndex);
        }
        else
        {
            Debug.LogWarning("No previous scene index stored!");
        }
    }
}
