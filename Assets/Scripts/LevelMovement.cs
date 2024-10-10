using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    [SerializeField] private float tiltSpeed = 5f;
    [SerializeField] private float maxTiltAngle = 30f;
    [SerializeField] private InputMode inputMode = InputMode.JOYSTICK;
    [SerializeField] private Rigidbody ballRigidbody;
    private Vector3 targetRotation;
    private void Start()
    {

        ballRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        InputManager.Instance.SetInputMode(inputMode);
        if (ballRigidbody == null)
        {
            Debug.LogError("Ball Rigidbody not assigned!");
        }
    }

    private void Update()
    {
        Vector2 moveVector = InputManager.Instance.MovementVector;

        targetRotation.x = -moveVector.y * maxTiltAngle;
        targetRotation.z = moveVector.x * maxTiltAngle;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-targetRotation), Time.deltaTime * tiltSpeed);
    }
    private void FixedUpdate()
    {
        if (ballRigidbody != null)
        {
            Vector3 tiltDirection = new Vector3(targetRotation.z, 0, -targetRotation.x).normalized;
            ballRigidbody.AddForce(tiltDirection * Physics.gravity.magnitude, ForceMode.Acceleration);
        }
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
