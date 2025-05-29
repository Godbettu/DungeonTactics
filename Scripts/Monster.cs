using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    private int currentHP;

    [SerializeField] public Animator animator;

    private bool isDead = false;

    private void Start()
    {
        currentHP = data.maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, data.maxHP);
        Debug.Log($"{data.monsterName} took {amount} damage. Current HP: {currentHP}");

        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            PlayDeadAnimation();
        }
    }

    public void Attack(Player player)
    {
        Debug.Log($"{data.monsterName} attacks for {data.damage} damage!");

        if (player.IsReflecting())
        {
            Debug.Log("Attack was reflected!");
            this.TakeDamage(data.damage);
        }
        else
        {
            player.TakeDamage(data.damage);
        }
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public string GetName()
    {
        return data.monsterName;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
    public void PlayAttackAnimation1()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack1");
        }
        else
        {
            Debug.LogWarning("❌ Animator component is not assigned.");
        }
    }

    public void PlayAttackAnimation2()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack2");
        }
        else
        {
            Debug.LogWarning("❌ Animator component is not assigned.");
        }
    }

    public void PlayDeadAnimation()
    {
        if (currentHP <= 0)
        {
            if (animator != null)
            {
                animator.SetTrigger("Dead");
                Debug.Log($"{data.monsterName} is dead. Playing dead animation.");
            }
            else
            {
                Debug.LogWarning("❌ Animator component is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Tried to play dead animation, but monster is not dead.");
        }
    }

}
