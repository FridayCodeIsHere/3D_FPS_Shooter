using UnityEngine;
using UnityEngine.SceneManagement;

public enum CheckPoints
{
    Point1,
    Point2,
    Point3,
    Point4,
    Point5,
    Point6,
    Point7,
    Point8
}

public delegate void CheckpointEnter();
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPoints _checkPoint;
    [SerializeField] private PlayerMovement _player;
    public event CheckpointEnter OnCheckpointEnter;

    private void OnEnable()
    {
        OnCheckpointEnter += SaveCheckPoint;
    }

    private void OnDisable()
    {
        OnCheckpointEnter -= SaveCheckPoint;
    }

    private void Start()
    {
        InitializationCheckpoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement player))
        {
            OnCheckpointEnter?.Invoke();
        }
    }

    private void SaveCheckPoint()
    {
        string save = JsonUtility.ToJson(_checkPoint);
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_checkpoint", save);
    }

    private void InitializationCheckpoint()
    {
        string nameKey = SceneManager.GetActiveScene().name + "_checkpoint";
        if (PlayerPrefs.HasKey(nameKey))
        {
            CheckPoints checkPoint = JsonUtility.FromJson<CheckPoints>(PlayerPrefs.GetString(nameKey));
            if (checkPoint == _checkPoint)
            {
                _player.transform.position = transform.position;
            }
        }
    }
}
