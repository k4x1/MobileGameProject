using UnityEngine;
using UnityEngine.InputSystem;

public class LevelMovement : MonoBehaviour
{
    Vector2 moveVector;
    [SerializeField] float rotationSpeed = 60f;
    [SerializeField] float maxRotationAngle = 30f;

    public void InputPlayer(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;


        rotation.x -= moveVector.y * rotationSpeed * Time.deltaTime;
        rotation.z += moveVector.x * rotationSpeed * Time.deltaTime;

        if (rotation.x > 180f) rotation.x -= 360f;
        rotation.x = Mathf.Clamp(rotation.x, -maxRotationAngle, maxRotationAngle);

        if (rotation.z > 180f) rotation.z -= 360f;
        rotation.z = Mathf.Clamp(rotation.z, -maxRotationAngle, maxRotationAngle);

        transform.rotation = Quaternion.Euler(rotation);
    }
}
