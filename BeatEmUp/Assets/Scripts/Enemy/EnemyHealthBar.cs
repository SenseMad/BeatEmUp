using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
  [SerializeField] private Enemy _enemy;
  [SerializeField] private GameObject _healthBarObject;
  [SerializeField] private bool _visibleFullHealth;

  [Space(), SerializeField] private Image _healthBar;

  private Camera mainCamera;

  private void Start()
  {
    mainCamera = Camera.main;

    Health_OnChangeHealth();
  }

  private void OnEnable()
  {
    _enemy.Health.OnChangeHealth += Health_OnChangeHealth;
  }

  private void OnDisable()
  {
    _enemy.Health.OnChangeHealth -= Health_OnChangeHealth;
  }

  private void LateUpdate()
  {
    //transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
  }

  private void Health_OnChangeHealth()
  {
    _healthBarObject.SetActive(!(_enemy.Health.CurrentHealth >= _enemy.Health.MaxHealth && !_visibleFullHealth));

    _healthBar.fillAmount = (float)_enemy.Health.CurrentHealth / (float)_enemy.Health.MaxHealth;
  }
}