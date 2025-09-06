using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float MaxSpeed => SprintInput? SprintSpeed : WalkSpeed;
    public float Acceleration = 15f;

    [SerializeField] float WalkSpeed = 3.5f;
    [SerializeField] float SprintSpeed = 8f;

    [Space(15)]
    [Tooltip("This is how high the character can jump.")]
    [SerializeField] float JumpHeight = 2f;
    public bool Sprinting
    {
        get
        {
            return SprintInput && CurrentSpeed > 0.1f;
        }
    }

    [Header("Looking Parameters")]
    public Vector2 LookSensitivity = new Vector2(0.1f, 0.1f);

    public float PitchLimit = 85f;

    [SerializeField] float currentPitch = 0f;

    public float CurrentPitch
    {
        get => currentPitch;
        set
        {
            currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
        }
    }
    [Header("Camera Parameters")]
    [SerializeField] float CameraNormalFOV = 60f;
    [SerializeField] float CameraSprintFOV = 80f;
    [SerializeField] float CameraFOVSmoothing = 1f;

    float TargetCameraFOV
    {
        get
        {
            return Sprinting ? CameraSprintFOV : CameraNormalFOV;
        }
    }
    [Header("Physics Parameters")]
    [SerializeField] float GravityScale = 3f;

    public float VerticalVelocity = 0f;

    public Vector3 CurrentVelocity {  get; private set; }
    public float CurrentSpeed { get; private set; }

    public bool IsGrounded => controller.isGrounded;


    [Header("Inputs")]
    public Vector2 MoveInput;
    public Vector2 LookInput;
    public bool SprintInput;

    [Header("Component")]
    [SerializeField] CharacterController controller;
    [SerializeField] CinemachineCamera fpCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        LookUpdate();
        CameraUpdate();
    }
    #region Methods

    private void OnValidate()
    {
        if(controller == null) 
        {
            controller = GetComponent<CharacterController>();
        }
    }

    void MoveUpdate()
    {
        Vector3 motion = transform.forward * MoveInput.y + transform.right * MoveInput.x;
        motion.y = 0f;
        motion.Normalize();

        if(motion.sqrMagnitude >= 0.01f)
        {
            CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, motion * MaxSpeed, Acceleration * Time.deltaTime);
        }
        else
        {
            CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, Vector3.zero, Acceleration * Time.deltaTime);    
        }
        if(IsGrounded && VerticalVelocity <= 0.01f)
        {
            VerticalVelocity = -3;
        }
        else
        {
            VerticalVelocity += Physics.gravity.y * GravityScale * Time.deltaTime;
        }


        Vector3 fullVelocity = new Vector3(CurrentVelocity.x, VerticalVelocity, CurrentVelocity.z);

        controller.Move(fullVelocity * Time.deltaTime);

        CurrentSpeed = CurrentVelocity.magnitude;
    }
    void LookUpdate()
    {
        Vector2 input = new Vector2(LookInput.x * LookSensitivity.x, LookInput.y * LookSensitivity.y);

        //Looking Up or Right
        CurrentPitch -= input.y;

        fpCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        //Looking Left and Right
        transform.Rotate(Vector3.up * input.x);
    }

    void CameraUpdate()
    {
        float targetFOV = CameraNormalFOV;
        if (Sprinting)
        {
            float speedRatio = CurrentSpeed / SprintSpeed;

            targetFOV = Mathf.Lerp(CameraNormalFOV, CameraSprintFOV, speedRatio);
        }

        fpCamera.Lens.FieldOfView = Mathf.Lerp(fpCamera.Lens.FieldOfView, targetFOV, CameraFOVSmoothing * Time.deltaTime);
    }
    #endregion
}
