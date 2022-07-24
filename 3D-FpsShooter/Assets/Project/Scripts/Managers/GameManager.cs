using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private float _waitAfterDying;
    public static GameManager Instance { get; private set; }

    private void OnEnable()
    {
        _playerHealth.OnDead += RestartLevel;
    }

    private void OnDisable()
    {
        _playerHealth.OnDead -= RestartLevel;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RestartLevel()
    {
        StartCoroutine(RestartLevelCoroutine());
    }

    private IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(_waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
