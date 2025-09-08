using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Weapon weapon;

    //Varibles used for Movements Mainly
    [Header("Movement Parameters")]                                                      //---------------------------------//
    //This Set the velocity based if is sprintign or walking
    public float MaxSpeed => SprintInput? SprintSpeed : WalkSpeed;
    public float Acceleration = 15f;

    [SerializeField] float WalkSpeed = 3.5f;
    [SerializeField] float SprintSpeed = 8f;

    //Space sets space in the inspector and ToolTip shows message when shadowed by the cursor
    [Space(15)]
    [Tooltip("This is how high the character can jump.")]
    [SerializeField] float JumpHeight = 2f;

    private int timesJumped = 0;
    [SerializeField] int JumpTimes = 1;
    //this return that the player is sprinting only if is moving and the input is true because it will be used with FOV and 
    //you don't want the camera moving while standign right?
    public bool Sprinting
    {
        get
        {
            return SprintInput && CurrentSpeed > 0.1f;
        }
    }


    [Header("Looking Parameters")]                                                      //---------------------------------//
    public Vector2 LookSensitivity = new Vector2(0.1f, 0.1f);

    //How low or high the head can twist
    public float PitchLimit = 85f;

    [SerializeField] float currentPitch = 0f;
    //Make sure the value is never set above
    public float CurrentPitch
    {
        get => currentPitch;
        set
        {
            currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
        }
    }

    [Header("Camera Parameters")]                                                      //---------------------------------//
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

    [Header("Physics Parameters")]                                                      //---------------------------------//
    [SerializeField] float GravityScale = 3f;
    public float VerticalVelocity = 0f;
    public Vector3 CurrentVelocity {  get; private set; }
    public float CurrentSpeed { get; private set; }
    private bool wasGrounded = false;
    public bool IsGrounded => controller.isGrounded;


    [Header("Inputs")]                                                      //---------------------------------//
    public Vector2 MoveInput;
    public Vector2 LookInput;
    public bool SprintInput;
    public bool hasFired;

    public bool shootPressed = false;

    [Header("Component")]                                                      //---------------------------------//
    [SerializeField] CharacterController controller;
    [SerializeField] CinemachineCamera fpCamera;

    [Header("Events")]                                                        //---------------------------------//
    public UnityEvent Landed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    float timer;
    void Update()
    {
        timer +=  Time.deltaTime;
        MoveUpdate();
        LookUpdate();
        CameraUpdate();

        if(!wasGrounded && IsGrounded)
        {
            timesJumped = 0;
            Landed?.Invoke();
        }
        wasGrounded = IsGrounded;

        if (shootPressed && weapon.CanUse(timer, hasFired))
        {
            weapon.Use();
            hasFired = true;
            timer = 0;
        }
        hasFired = shootPressed;        
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
        //Move input.y is for W and S since they are set up and down
        Vector3 motion = transform.forward * MoveInput.y + transform.right * MoveInput.x;
        motion.y = 0f;
        motion.Normalize();
        //if is moving then aceletare/lerp to that velocity
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

        CollisionFlags flags =  controller.Move(fullVelocity * Time.deltaTime);
        if((flags & CollisionFlags.Above) != 0 && VerticalVelocity > 0.01f)
        {
            Debug.Log("Hit Something above!");
            VerticalVelocity = 0f;
        }

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
    public void TryJump()
    {
        if (JumpTimes <= timesJumped && VerticalVelocity > 0.01f)
        {
            return;
        }
        VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y * GravityScale);
        timesJumped++;
    }

    #endregion
}
