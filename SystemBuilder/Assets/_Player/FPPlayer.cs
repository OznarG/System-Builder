using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class FPPlayer : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] PlayerController playerController;
    InputAction inp;
  
    void OnMove(InputValue value)
    {
        playerController.MoveInput = value.Get<Vector2>();
        Debug.Log("hi");
    }

    void OnLook(InputValue value)
    {
        playerController.LookInput = value.Get<Vector2>();
    }

    private void OnValidate()
    {
        if(playerController  == null) playerController = GetComponent<PlayerController>();
    }
}
