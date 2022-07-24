using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField, Range(0f, 40f)] private float _mouseSensitivity;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_mouseSensitivity < 0f) _mouseSensitivity = 0f;
    }
    #endregion

    private void Update()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * (_mouseSensitivity / 10);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        _cameraTransform.rotation = Quaternion.Euler(_cameraTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
}
