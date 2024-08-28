using System;
using UnityEngine;

[Serializable]
public sealed class Health
{
  [SerializeField] private int _maxHealth;

  private int currentHealth;

  public int CurrentHealth
  {
    get => currentHealth;
    private set
    {
      currentHealth = value;
      OnChangeHealth?.Invoke();
    }
  }

  public int MaxHealth => _maxHealth;

  public event Action OnChangeHealth;

  public event Action<int> OnAddHealth;

  public event Action<int> OnTakeHealth;

  public event Action OnInstantlyKill;

  public void Initialize()
  {
    CurrentHealth = _maxHealth;
  }

  public void SetHealth(int parHealth)
  {
    if (parHealth < 0)
      throw new ArgumentOutOfRangeException(nameof(parHealth));

    CurrentHealth = parHealth;
  }

  public void SetMaxHealth(int parHealth)
  {
    if (parHealth < 0)
      throw new ArgumentOutOfRangeException(nameof(parHealth));

    _maxHealth = parHealth;
  }

  public void AddHealth(int parHealth)
  {
    if (parHealth < 0)
      throw new ArgumentOutOfRangeException(nameof(parHealth));

    int healthBefore = CurrentHealth;
    CurrentHealth += parHealth;

    if (currentHealth > _maxHealth)
      CurrentHealth = _maxHealth;

    int healthAmount = CurrentHealth - healthBefore;
    if (healthAmount > 0)
      OnAddHealth?.Invoke(healthAmount);
  }

  public void TakeHealth(int parHealth)
  {
    if (parHealth < 0)
      throw new ArgumentOutOfRangeException(nameof(parHealth));

    int healthBefore = CurrentHealth;
    CurrentHealth -= parHealth;

    if (currentHealth < 0)
      CurrentHealth = 0;

    int damageAmount = healthBefore - CurrentHealth;
    if (damageAmount > 0)
      OnTakeHealth?.Invoke(damageAmount);

    if (currentHealth <= 0)
      OnInstantlyKill?.Invoke();
  }

  public void InstantlyKill()
  {
    CurrentHealth = 0;

    OnTakeHealth?.Invoke(_maxHealth);

    OnInstantlyKill?.Invoke();
  }
}