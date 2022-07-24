using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIPlayerHealth : MonoBehaviour
{
    [SerializeField] private Health _sourceHealth;
    [SerializeField] private Image _healthImageBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void OnEnable()
    {
        _sourceHealth.OnApplyDamage += RenderHealthData;
        _sourceHealth.OnApplyHealth += RenderHealthData;
    }

    public void OnDisable()
    {
        _sourceHealth.OnApplyDamage -= RenderHealthData;
        _sourceHealth.OnApplyHealth -= RenderHealthData;
    }

    private void Start()
    {
        RenderHealthData();
    }

    private void RenderHealthData()
    {
        float healthValue = (float)_sourceHealth.CurrentHealth / (float)_sourceHealth.TotalHealth;
        _healthImageBar.fillAmount = healthValue;
        _healthText.text = $"{_sourceHealth.CurrentHealth}/{_sourceHealth.TotalHealth}";
    }
}
