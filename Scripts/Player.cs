using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    private bool isBlocking = false;
    private bool isReflecting = false;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (isBlocking)
        {
            Debug.Log("Player blocked the attack!");
            isBlocking = false;
            return;
        }

        if (isReflecting)
        {
            Debug.Log("Player reflected the attack!");
            isReflecting = false;
            // Reflection logic should be handled externally, because we need a reference to the attacker (monster)
            return;
        }

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log($"Player took {amount} damage. Current HP: {currentHP}");
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log($"Player healed for {amount}. Current HP: {currentHP}");
    }

    public void SetBlockMode(bool value)
    {
        isBlocking = value;
        Debug.Log("Player is now blocking.");
    }

    public void SetReflectMode(bool value)
    {
        isReflecting = value;
        Debug.Log("Player is now reflecting.");
    }

    public bool IsReflecting() => isReflecting;
    public bool IsBlocking() => isBlocking;
}
