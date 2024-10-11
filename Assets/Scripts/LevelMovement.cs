using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    [SerializeField] private float tiltSpeed = 5f;
    [SerializeField] private float maxTiltAngle = 30f;
    [SerializeField] private InputMode inputMode = InputMode.JOYSTICK;
    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private float maxVelocityMagnitude = 10f;
    [SerializeField] private float surfaceOffset = 0.5f; 

    private Vector3 targetRotation;
    private Vector3 previousPosition;
    private Rigidbody levelRigidbody;

    private void Start()
    {
        levelRigidbody = GetComponent<Rigidbody>();
        ballRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        InputManager.Instance.SetInputMode(inputMode);

        if (ballRigidbody == null)
        {
            Debug.LogError("Ball Rigidbody not assigned!");
        }
        else
        {
            ballRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        levelRigidbody.isKinematic = true;
        levelRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        previousPosition = transform.position;
    }

    private void Update()
    {
        Vector2 moveVector = InputManager.Instance.MovementVector;

        targetRotation.x = -moveVector.y * maxTiltAngle;
        targetRotation.z = moveVector.x * maxTiltAngle;
    }

    private void FixedUpdate()
    {
        Quaternion targetQuaternion = Quaternion.Euler(-targetRotation);
        levelRigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetQuaternion, Time.fixedDeltaTime * tiltSpeed));

        if (ballRigidbody != null)
        {
            Vector3 levelMovement = transform.position - previousPosition;
            previousPosition = transform.position;

            Vector3 projectedPosition = ProjectPointOnPlane(ballRigidbody.position, transform.position, transform.up);
            Vector3 surfacePoint = projectedPosition + transform.up * surfaceOffset;

            Vector3 newPosition = surfacePoint + levelMovement;
            ballRigidbody.MovePosition(newPosition);

            Vector3 tiltDirection = new Vector3(targetRotation.z, 0, -targetRotation.x).normalized;
            ballRigidbody.AddForce(tiltDirection * Physics.gravity.magnitude, ForceMode.Acceleration);

            if (ballRigidbody.velocity.magnitude > maxVelocityMagnitude)
            {
                ballRigidbody.velocity = ballRigidbody.velocity.normalized * maxVelocityMagnitude;
            }
        }
    }

    private Vector3 ProjectPointOnPlane(Vector3 point, Vector3 planePosition, Vector3 planeNormal)
    {
        return point - Vector3.Dot(point - planePosition, planeNormal) * planeNormal;
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
