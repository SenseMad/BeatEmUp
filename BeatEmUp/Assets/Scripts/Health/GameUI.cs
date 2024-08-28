using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUI : MonoBehaviour
{
  [Space(), SerializeField] private Image _healthBar;

  private Character character;

  [Inject]
  private void Construct(Character parCharacter)
  {
    character = parCharacter;
  }

  private void Start()
  {
    Health_OnChangeHealth();
  }

  private void OnEnable()
  {
    character.Health.OnChangeHealth += Health_OnChangeHealth;
  }

  private void OnDisable()
  {
    character.Health.OnChangeHealth -= Health_OnChangeHealth;
  }

  private void Health_OnChangeHealth()
  {
    _healthBar.fillAmount = (float)character.Health.CurrentHealth / (float)character.Health.MaxHealth;
  }
}