using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DungeonCardSlotUI
{
    public string cardType;
    public GameObject cardPrefab;
    public TextMeshProUGUI countText;
    public Transform cardDisplayArea;
}

public class DungeonUI : MonoBehaviour
{

    public DungeonManager dungeonManager;

    [Header("Card Display Slots")]
    public DungeonCardSlotUI[] slots;

    [Header("Page Navigation")]
    public GameObject[] pagePanels;
    public Button nextButton;
    public Button backButton;

    [Header("Card Selection")]
    public Transform selectedCardArea;
    public Button selectButton;
    private string selectedCardType;

    [Header("Player & Monster")]
    public int playerHP = 100;
    public int monsterHP = 100;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI monsterHPText;
    public TextMeshProUGUI playerDamageText;
    public TextMeshProUGUI monsterDamageText;
    public Animator playerAnimator;
    public Animator monsterAnimator;

    private int currentPage = 0;

    void Start()
    {
        UpdateDungeonCardUI();
        UpdatePageUI();
        UpdateHPUI();
    }

    public void UpdateDungeonCardUI()
    {
        if (CardDataManager.Instance == null)
        {
            Debug.LogWarning("❌ CardDataManager.Instance is null!");
            return;
        }

        Dictionary<string, int> cardData = CardDataManager.Instance.GetAllCardData();

        foreach (var slot in slots)
        {
            int count = cardData.TryGetValue(slot.cardType, out int value) ? value : 0;
            slot.countText.text = $"x {count}";

            foreach (Transform child in slot.cardDisplayArea)
                Destroy(child.gameObject);

            if (slot.cardPrefab != null)
            {
                GameObject card = Instantiate(slot.cardPrefab, slot.cardDisplayArea);
                card.transform.localPosition = Vector3.zero;
                card.transform.localScale = Vector3.one;

                Button btn = card.GetComponent<Button>();
                if (btn != null)
                {
                    string type = slot.cardType;
                    btn.onClick.AddListener(() => SelectCard(type));
                }
            }
        }
    }

    public void SelectCard(string cardType)
    {
        selectedCardType = cardType;

        foreach (Transform child in selectedCardArea)
            Destroy(child.gameObject);

        GameObject prefab = GetPrefabForCard(cardType);
        if (prefab != null)
        {
            Instantiate(prefab, selectedCardArea);
        }

        Debug.Log($"✅ Selected Card: {cardType}");
    }


    private GameObject GetPrefabForCard(string type)
    {
        foreach (var slot in slots)
        {
            if (slot.cardType == type)
                return slot.cardPrefab;
        }
        return null;
    }
    public void OnSelectConfirm()
    {
        if (string.IsNullOrEmpty(selectedCardType))
        {
            Debug.LogWarning("⚠️ No card selected!");
            return;
        }

        DungeonCardData cardData = Resources.Load<DungeonCardData>($"Cards/{selectedCardType}");

        if (cardData != null)
        {
            dungeonManager.OnCardSelected(cardData); // ส่งให้ DungeonManager ใช้งาน
            Debug.Log($"📩 Sent {selectedCardType} to DungeonManager");
        }
        else
        {
            Debug.LogWarning($"⚠️ Could not find DungeonCardData for: {selectedCardType}");
        }

        selectedCardType = null;
    }


    void ApplyCardEffect(string cardType, bool isPlayer)
    {
        int damage = 0;
        int heal = 0;
        string animTrigger = "Idle";

        switch (cardType)
        {
            case "Attack":
                damage = 10;
                animTrigger = "Attack";
                break;
            case "Heal":
                heal = 20;
                animTrigger = "Heal";
                break;
            case "Defense":
                // Defense logic in battle engine later
                animTrigger = "Defense";
                break;
            case "Block":
                animTrigger = "Block";
                break;
        }

        if (isPlayer)
        {
            if (damage > 0)
            {
                monsterHP -= damage;
                monsterAnimator.SetTrigger(animTrigger);
                monsterDamageText.text = $"-{damage}";
            }
            if (heal > 0)
            {
                playerHP += heal;
                playerAnimator.SetTrigger(animTrigger);
                playerDamageText.text = $"+{heal}";
            }
        }
        else
        {
            if (damage > 0)
            {
                playerHP -= damage;
                playerAnimator.SetTrigger(animTrigger);
                playerDamageText.text = $"-{damage}";
            }
            if (heal > 0)
            {
                monsterHP += heal;
                monsterAnimator.SetTrigger(animTrigger);
                monsterDamageText.text = $"+{heal}";
            }
        }

        UpdateHPUI();
        StartCoroutine(ClearDamageText());
    }

    IEnumerator ClearDamageText()
    {
        yield return new WaitForSeconds(1.5f);
        playerDamageText.text = "";
        monsterDamageText.text = "";
    }

    void UpdateHPUI()
    {
        playerHPText.text = playerHP.ToString();
        monsterHPText.text = monsterHP.ToString();
    }

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
            pagePanels[i].SetActive(i == currentPage);

        nextButton.interactable = currentPage < pagePanels.Length - 1;
        backButton.interactable = currentPage > 0;

        Debug.Log($"📄 Showing Page: {currentPage + 1}");
    }
}
