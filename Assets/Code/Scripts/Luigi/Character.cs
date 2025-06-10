using UnityEngine;

// Struct per la salute
public struct Health
{
    public int MaxHealth;
    public int CurrentHealth;
    public bool IsInvulnerable;

    public Health(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        IsInvulnerable = false;
    }

    public void TakeDamage(int amount)
    {
        if (!IsInvulnerable)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Max(CurrentHealth, 0);
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
    }
}

// Classe base astratta
public abstract class Character : MonoBehaviour
{
    [Header("Character Settings")]
    public Character_Data characterData;

    protected Rigidbody2D rb;
    //protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected Health health;
    protected float moveSpeed;
    protected int damage;

    // Enum per macchina a stati
    protected enum CharacterState
    {
        Idle,
        Moving,
        Jumping,
        Attacking
    }

    protected CharacterState currentState = CharacterState.Idle;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        if (characterData != null)
        {
            moveSpeed = characterData.moveSpeed;
            damage = characterData.damage;
            health = new Health(characterData.maxHealth);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} non ha un CharacterData assegnato!");
            health = new Health(100); // default
        }
    }

    // Movimento base
    public virtual void Move(Vector2 direction, float speed)
    {
        rb.linearVelocity = direction.normalized * speed;

        if (direction.magnitude > 0.1f)
            currentState = CharacterState.Moving;
        else
            currentState = CharacterState.Idle;
    }

    public virtual void Jump()
    {
        currentState = CharacterState.Jumping;
    }

    public virtual void Attack()
    {
        currentState = CharacterState.Attacking;
    }

    // Gestione salute
    public virtual void UpdateHealth(int amount)
    {
        if (amount < 0)
        {
            health.TakeDamage(-amount);
        }
        else
        {
            health.Heal(amount);
        }
    }
}
