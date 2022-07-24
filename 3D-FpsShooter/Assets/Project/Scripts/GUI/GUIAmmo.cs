using UnityEngine;
using TMPro;


public class GUIAmmo : MonoBehaviour
{
    public static GUIAmmo Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI _ammoText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void RenderAmmoData(int value)
    {
        _ammoText.text = value.ToString();
    }
}
