using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float maxRotationAngle = 30f;
    [SerializeField] private InputMode inputMode = InputMode.JOYSTICK;

    private void Start()
    {
        InputManager.Instance.SetInputMode(inputMode);
    }

    private void Update()
    {
        Vector2 moveVector = InputManager.Instance.MovementVector;

        Vector3 rotation = transform.rotation.eulerAngles;

        rotation.x -= moveVector.y * rotationSpeed * Time.deltaTime;
        rotation.z += moveVector.x * rotationSpeed * Time.deltaTime;

        if (rotation.x > 180f) rotation.x -= 360f;
        rotation.x = Mathf.Clamp(rotation.x, -maxRotationAngle, maxRotationAngle);

        if (rotation.z > 180f) rotation.z -= 360f;
        rotation.z = Mathf.Clamp(rotation.z, -maxRotationAngle, maxRotationAngle);

        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetInputMode(InputMode mode)
    {
        inputMode = mode;
        InputManager.Instance.SetInputMode(mode);
    }

    public void ResetGyroscope()
    {
        if (inputMode == InputMode.GYROSCOPE)
        {
            InputManager.Instance.ResetGyroscope();
        }
    }
}
