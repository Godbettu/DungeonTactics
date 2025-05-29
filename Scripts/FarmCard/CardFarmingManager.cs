using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CardFarmingManager : MonoBehaviour
{
    public GameObject BombCardPrefeb;
    public GameObject HerbCardPrefab;
    public GameObject PotionCardPrefab;
    public GameObject RockCardPrefab;

    private GameObject GetCardPrefabByType(string cardType)
    {
        switch (cardType)
        {
            case "Bomb": return BombCardPrefeb;
            case "Herb": return HerbCardPrefab;
            case "Potion": return PotionCardPrefab;
            case "Rock": return RockCardPrefab;
            default: return null;
        }
    }


    public Transform[] handAreas;
    public Transform[] frameArea;
    public GameObject upgradePanel, resetFramePanel;
    public TextMeshProUGUI upgradeText, resetFrameText;
    public Transform dungeonCardDisplayArea;
    public Button closeButton, drawButton, resetButton, upgradeButton, resetFrameButton, changeSceneButton;

    public GameObject attackCardPrefab, defenseCardPrefab, HealCardPrefab, blockCardPrefab;
    public Inventory inventory;

    private string[] cardTypes = { "Bomb", "Herb", "Potion", "Rock" };
    private int maxHandSize = 5;
    private int maxResetFrameCount = 2;
    private int currentResetFrameCount = 0;

    private bool isUpgrading = false;


    void Start()
    {
        closeButton.onClick.AddListener(CloseUpgradePanel);
        drawButton.onClick.AddListener(DrawCard);
        resetButton.onClick.AddListener(ResetCards);
        upgradeButton.onClick.AddListener(UpgradeCards);
        resetFrameButton.onClick.AddListener(ResetFrame);

        changeSceneButton.onClick.AddListener(() => ChangeScene(0));

        InitializeFrame();
        ResetCards();
        //UpgradeCards();
        UpdateResetFrameUI();
    }

    public void DrawCard()
    {
        Transform availableHandArea = FindAvailableHandArea();
        if (availableHandArea == null)
        {
            Debug.Log("ไม่มีช่องว่างใน HandArea แล้ว!");
            return;
        }

        string randomCardType = cardTypes[Random.Range(0, cardTypes.Length)];
        GameObject prefabToUse = GetCardPrefabByType(randomCardType);

        if (prefabToUse == null)
        {
            Debug.LogError($"❌ ไม่พบ Prefab สำหรับ {randomCardType}");
            return;
        }

        GameObject newCard = Instantiate(prefabToUse, availableHandArea);
        newCard.name = randomCardType;

        // ให้การ์ดอยู่บนสุด
        newCard.transform.SetAsLastSibling();

        EventTrigger trigger = newCard.GetComponent<EventTrigger>();
        if (trigger != null)
        {
            trigger.CardFarmingManager = this;
        }
    }

    private Transform FindAvailableHandArea()
    {
        foreach (Transform handArea in handAreas)
        {
            if (handArea.childCount == 0)
            {
                return handArea;
            }
        }
        return null;
    }

    public void MoveCardToFrame(GameObject card)
    {
        card.transform.SetAsLastSibling(); // ให้คลิกง่ายสุด

        string firstFrameCardType = GetFirstFrameCardType();
        Transform availableFrameArea = FindAvailableFrameArea();

        if (availableFrameArea == null)
        {
            Debug.Log("FrameArea เต็มแล้ว!");
            return;
        }

        if (firstFrameCardType == "")
        {
            PlaceCardInFrame(card, availableFrameArea);
            return;
        }

        if (card.name != firstFrameCardType)
        {
            Debug.Log("การ์ดทั้งหมดใน FrameArea ต้องเป็นประเภทเดียวกับการ์ดในเฟรมแรก!");
            return;
        }

        PlaceCardInFrame(card, availableFrameArea);
    }

    private string GetFirstFrameCardType()
    {
        if (frameArea[0].childCount > 0)
        {
            return frameArea[0].GetChild(0).gameObject.name;
        }
        return ""; 
    }

    private Transform FindAvailableFrameArea()
    {
        foreach (Transform frame in frameArea)
        {
            if (frame.childCount == 0)
            {
                return frame;
            }
        }
        return null;
    }
    private void PlaceCardInFrame(GameObject card, Transform frame)
    {
        string cardType = card.name.Replace("(Clone)", "");

        GameObject prefabToUse = GetCardPrefabByType(cardType);
        if (prefabToUse == null)
        {
            Debug.LogError($"❌ ไม่พบ Prefab สำหรับ {cardType} ใน FrameArea");
            return;
        }

        GameObject newFrameCard = Instantiate(prefabToUse, frame);
        newFrameCard.name = cardType;

        EventTrigger trigger = newFrameCard.GetComponent<EventTrigger>();
        if (trigger != null)
        {
            trigger.CardFarmingManager = this;
        }

        Destroy(card);
    }

    void ResetCards()
    {
        foreach (Transform area in handAreas)
        {
            foreach (Transform child in area)
            {
                Destroy(child.gameObject);
            }
        }
    }
    void UpgradeCards()
    {
        List<GameObject> frameCards = new List<GameObject>();
        string cardType = "";

        foreach (Transform frame in frameArea)
        {
            if (frame.childCount > 0)
            {
                GameObject card = frame.GetChild(0).gameObject;
                if (cardType == "")
                {
                    cardType = card.name;
                }
                if (card.name == cardType)
                {
                    frameCards.Add(card);
                }
            }
        }

        int count = frameCards.Count;
        if (count < 2) return;

        string dungeonCardName = GetDungeonCard(cardType);
        int dungeonCardAmount = count - 1;

        if (dungeonCardAmount > 0)
        {
            ShowUpgradePanel(dungeonCardName, dungeonCardAmount);

            foreach (GameObject card in frameCards)
            {
                Destroy(card);
            }

            if (CardDataManager.Instance != null)
            {
                int oldAmount = CardDataManager.Instance.GetCardCount(dungeonCardName);
                int newAmount = oldAmount + dungeonCardAmount;

                Debug.Log($"💾 ตั้งค่าการ์ด {dungeonCardName} จำนวน {newAmount} ลงใน CardDataManager");

                // ✅ ใช้ตัวแปรที่ถูกต้อง
                CardDataManager.Instance.AddCard(dungeonCardName, dungeonCardAmount);
            }
            else
            {
                Debug.LogError("❌ CardDataManager ยังไม่ถูกสร้างหรือโหลดใน Scene!");
            }
        }
    }


    string GetDungeonCard(string cardType)
    {
        switch (cardType)
        {
            case "Bomb": return "Attack";
            case "Herb": return "Defense";
            case "Potion": return "Heal";
            case "Rock": return "Block";
            default: return "";
        }
    }

    void ShowUpgradePanel(string cardName, int amount)
    {
        upgradeText.text = $"{cardName} x {amount}";
        upgradePanel.gameObject.SetActive(true);

        foreach (Transform child in dungeonCardDisplayArea)
        {
            Destroy(child.gameObject);
        }

        GameObject dungeonCardPrefab = GetDungeonCardPrefab(cardName);
        if (dungeonCardPrefab != null)
        {
            GameObject newDungeonCard = Instantiate(dungeonCardPrefab, dungeonCardDisplayArea);
            newDungeonCard.transform.localScale = Vector3.one;
        }
    }

    void CloseUpgradePanel()
    {
        upgradePanel.gameObject.SetActive(false);
    }

    void InitializeFrame()
    {
        foreach (Transform frame in frameArea)
        {
            foreach (Transform child in frame)
            {
                Destroy(child.gameObject);
            }
        }
    }

    GameObject GetDungeonCardPrefab(string cardName)
    {
        switch (cardName)
        {
            case "Attack": return attackCardPrefab;
            case "Defense": return defenseCardPrefab;
            case "Heal": return HealCardPrefab;
            case "Block": return blockCardPrefab;
            default: return null;
        }
    }

    Sprite GetCardSprite(string cardName)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprites/{cardName}");
        if (sprite == null)
        {
            Debug.LogError($"Sprite {cardName} not found in Resources/Sprites!");
        }
        return sprite;
    }

    // ✅ ฟังก์ชัน ResetFrame (ลบการ์ดใน FrameArea)
    void ResetFrame()
    {
        if (currentResetFrameCount >= maxResetFrameCount)
        {
            Debug.Log("คุณใช้ ResetFrame ครบ 2 ครั้งแล้ว!");
            return;
        }

        foreach (Transform frame in frameArea)
        {
            foreach (Transform child in frame)
            {
                Destroy(child.gameObject);
            }
        }

        currentResetFrameCount++; // อัปเดตจำนวนครั้งที่ใช้ไปแล้ว
        UpdateResetFrameUI();
    }

    void UpdateResetFrameUI()
    {
        resetFrameText.text = $" {currentResetFrameCount}/{maxResetFrameCount}";

        if (currentResetFrameCount >= maxResetFrameCount)
        {
            resetFrameButton.interactable = false;
        }
    }
    public void ChangeScene(int sceneIndex)
    {
        Debug.Log("กำลังเปลี่ยนไป Scene Index: " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}
