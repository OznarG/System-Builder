using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float MaxSpeed = 3.5f;
    public float Acceleration = 15f;

    public Vector3 CurrentVelocity {  get; private set; }
    public float CurrentSpeed { get; private set; }


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

    [Header("Inputs")]
    public Vector2 MoveInput;
    public Vector2 LookInput;

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

        float verticalVelocity = Physics.gravity.y * 20f * Time.deltaTime;

        Vector3 fullVelocity = new Vector3(CurrentVelocity.x, verticalVelocity, CurrentVelocity.z);

        controller.Move(fullVelocity * Time.deltaTime);

        CurrentSpeed = CurrentVelocity.magnitude;
    }
    void LookUpdate()
    {
        Vector2 input = new Vector2(LookInput.x * LookSensitivity.x, LookInput.y * LookSensitivity.y);

        //Looking Up or Right
        CurrentPitch = input.y;

        fpCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

        //Looking Left and Right
        transform.Rotate(Vector3.up * input.x);
    }

    #endregion
}
