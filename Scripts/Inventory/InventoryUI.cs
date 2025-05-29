using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventorySlotUI
{
    public string cardType;
    public GameObject cardPrefab;
    public TextMeshProUGUI countText;
    public Transform cardDisplayArea;
}

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    public InventorySlotUI[] slots;
    private Dictionary<string, int> cardData;

    [Header("Page Navigation")]
    public GameObject[] pagePanels; // เช่น PanelPage1, PanelPage2, PanelPage3
    public Button nextButton;
    public Button backButton;

    private int currentPage = 0;

    void Start()
    {
        UpdatePageUI(); // เริ่มจากหน้าแรก
    }

    public void SetInventoryData(Dictionary<string, int> data)
    {
        cardData = data;
        UpdateUI();
    }

    public void UpdateUI()
    {
        Debug.Log("📦 Updating Inventory UI");

        foreach (var slot in slots)
        {
            int count = cardData != null && cardData.TryGetValue(slot.cardType, out int value) ? value : 0;
            Debug.Log($"🔍 {slot.cardType} = {count}");

            if (slot.countText == null)
            {
                Debug.LogWarning($"⚠️ countText for {slot.cardType} is null!");
            }
            else
            {
                slot.countText.text = $"{slot.cardType} x {count}";
            }

            foreach (Transform child in slot.cardDisplayArea)
            {
                Destroy(child.gameObject);
            }

            if (slot.cardPrefab != null)
            {
                GameObject card = Instantiate(slot.cardPrefab, slot.cardDisplayArea);
                card.transform.localPosition = Vector3.zero;
                card.transform.localScale = Vector3.one;
            }
        }
    }

    // ======================
    // 📄 ระบบเปลี่ยนหน้า Panel
    // ======================

    public void OnNextButtonPressed()
    {
        if (currentPage < pagePanels.Length - 1)
        {
            currentPage++;
            UpdatePageUI();
        }
    }

    public void OnBackButtonPressed()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePageUI();
        }
    }

    private void UpdatePageUI()
    {
        for (int i = 0; i < pagePanels.Length; i++)
        {
            pagePanels[i].SetActive(i == currentPage);
        }

        // ควบคุมปุ่ม Next / Back
        if (nextButton != null)
            nextButton.interactable = currentPage < pagePanels.Length - 1;

        if (backButton != null)
            backButton.interactable = currentPage > 0;

        Debug.Log($"📄 Showing Page: {currentPage + 1}");
    }
}
