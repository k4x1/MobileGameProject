using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public PlayerControls playerInput;

    [HideInInspector] public Vector2 MovementVector;
    [HideInInspector] public InputMode CurrentInputMode = InputMode.JOYSTICK;

    private Vector2 touchStartPosition;
    private bool isTouching = false;

    private Quaternion gyroInitialRotation;
    private bool gyroInitialized = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerInput = new PlayerControls();
        InitializeGyroscope();
    }

    private void Update()
    {
        switch (CurrentInputMode)
        {
            case InputMode.JOYSTICK:
                MovementVector = playerInput.Movement.Joystick.ReadValue<Vector2>();
                break;
            case InputMode.RELATIVE_TOUCH:
                HandleRelativeTouchInput();
                break;
            case InputMode.GYROSCOPE:
                HandleGyroscopeInput();
                break;
        }
    }
    private void HandleRelativeTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == UnityEngine.TouchPhase.Moved || touch.phase == UnityEngine.TouchPhase.Stationary)
            {
                Vector2 touchDelta = (touch.position - touchStartPosition) / Screen.height;
                MovementVector = Vector2.ClampMagnitude(touchDelta, 1f);
            }
            else if (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled)
            {
                isTouching = false;
                MovementVector = Vector2.zero;
            }
        }
        else if (!isTouching)
        {
            MovementVector = Vector2.zero;
        }
    }
    private void InitializeGyroscope()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogWarning("Gyroscope not supported on this device.");
        }
    }
    private void HandleGyroscopeInput()
    {
        if (!gyroInitialized)
        {
            ResetGyroscope();
        }

        Quaternion gyroRotation = Input.gyro.attitude;
        Quaternion rotationDifference = gyroRotation * Quaternion.Inverse(gyroInitialRotation);

        Vector3 rotationEuler = rotationDifference.eulerAngles;


        float x = Mathf.Clamp((rotationEuler.x > 180 ? rotationEuler.x - 360 : rotationEuler.x) / 90f, -1f, 1f);
        float z = Mathf.Clamp((rotationEuler.z > 180 ? rotationEuler.z - 360 : rotationEuler.z) / 90f, -1f, 1f);

        MovementVector = new Vector2(z, -x);
    }
    public void ResetGyroscope()
    {
        gyroInitialRotation = Input.gyro.attitude;
        gyroInitialized = true;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void DisablePlayerInput()
    {
        playerInput.Movement.Disable();
    }

    public void EnablePlayerInput()
    {
        playerInput.Movement.Enable();
    }

    public void SetInputMode(InputMode mode)
    {
        CurrentInputMode = mode;
        if (mode == InputMode.GYROSCOPE)
        {
            ResetGyroscope();
        }
    }
}
