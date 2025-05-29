using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public Player player;
    public Monster monster;

    public GameObject cardSelectionUI;
    public TMP_Text timerText;

    public float turnTimeLimit = 30f;
    private float timer;
    private bool isPlayerTurn = true;

    private DungeonCardData selectedCard;
    private int monsterAttackAnimIndex = 0;

    [Header("Ultimate Drop Settings")]
    public GameObject[] allUltimateCardPrefabs;

    private void Start()
    {
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        isPlayerTurn = true;
        selectedCard = null;
        timer = turnTimeLimit;

        ShowCardSelectionUI();

        while (timer > 0f && selectedCard == null)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI(timer);
            yield return null;
        }

        HideCardSelectionUI();

        if (selectedCard != null)
        {
            Debug.Log($"Player used card: {selectedCard.cardName}");
            selectedCard.ApplyEffect(player, monster);
        }
        else
        {
            Debug.Log("⏰ Player ran out of time! Skipped turn.");
        }

        yield return new WaitForSeconds(1f);

        if (monster.IsDead())
        {
            StartCoroutine(HandleVictory());
        }
        else
        {
            StartCoroutine(MonsterTurn());
        }
    }

    private IEnumerator MonsterTurn()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);

        PlayMonsterAttackAnimation();
        yield return new WaitForSeconds(1.2f);

        monster.Attack(player);
        yield return new WaitForSeconds(1f);

        if (player.currentHP <= 0)
        {
            Debug.Log("☠️ Player is dead. Game Over.");
        }
        else
        {
            StartCoroutine(PlayerTurn());
        }
    }

    private IEnumerator HandleVictory()
    {
        Debug.Log("🏆 Monster defeated!");
        monster.PlayDeadAnimation();

        yield return new WaitForSeconds(2f);

        int dropCount = Random.Range(2, 5);
        List<GameObject> droppedCards = new List<GameObject>();

        foreach (var prefab in allUltimateCardPrefabs)
        {
            var cardInstance = prefab.GetComponent<CardInstance>();
            if (cardInstance != null)
            {
                DungeonCardData cardData = cardInstance.cardData;
                if (cardData != null && cardData.isUltimate && cardData.ultimateLevel == monster.data.ultimateLevel)
                {
                    for (int i = 0; i < dropCount; i++)
                    {
                        GameObject drop = Instantiate(prefab, transform.position + Vector3.right * i, Quaternion.identity);
                        droppedCards.Add(drop);
                    }
                    break;
                }
            }
        }

        Debug.Log($"🎁 Dropped {dropCount} Ultimate cards of level {monster.data.ultimateLevel}");
        yield break;
    }

    private void PlayMonsterAttackAnimation()
    {
        if (monsterAttackAnimIndex % 2 == 0)
            monster.PlayAttackAnimation1();
        else
            monster.PlayAttackAnimation2();

        monsterAttackAnimIndex++;
    }

    public void OnCardSelected(DungeonCardData card)
    {
        selectedCard = card;
    }

    private void ShowCardSelectionUI()
    {
        cardSelectionUI.SetActive(true);

        var cardData = CardDataManager.Instance.GetAllCardData();
        foreach (Transform child in cardSelectionUI.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var kvp in cardData)
        {
            string cardType = kvp.Key;
            int count = kvp.Value;

            if (count > 0)
            {
                GameObject cardButton = new GameObject(cardType);
                cardButton.transform.SetParent(cardSelectionUI.transform);

                Button btn = cardButton.AddComponent<Button>();
                Text txt = cardButton.AddComponent<Text>();
                txt.text = $"{cardType} x{count}";
                txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

                DungeonCardData card = Resources.Load<DungeonCardData>($"Cards/{cardType}");
                btn.onClick.AddListener(() => OnCardSelected(card));
            }
        }
    }

    private void HideCardSelectionUI()
    {
        cardSelectionUI.SetActive(false);
    }

    private void UpdateTimerUI(float timeRemaining)
    {
        if (timerText != null)
        {
            timerText.text = $"⏳ {Mathf.CeilToInt(timeRemaining)}s";
        }
    }
}
