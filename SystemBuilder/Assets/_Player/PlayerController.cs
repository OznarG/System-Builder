using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float MaxSpeed = 3.5f;

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

        controller.Move(motion * MaxSpeed * Time.deltaTime);
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
