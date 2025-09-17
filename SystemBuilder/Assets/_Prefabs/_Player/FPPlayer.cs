using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class FPPlayer : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] PlayerController playerController;

    [Header("Weapons")]
    public GameObject currentWeapon;
    public Collider currentCollider;
    public GameObject[] playerWeapons;
    public Collider[] weaponsColliders;
    public Image equipOneImage;
    public Image equipTwoImage;
    public Slot equipSlotOne;
    public Slot equipSlotTwo;
    public int weaponIndex;

    [Header("Inventories")]
    public int currentBeltSelection;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Inventory Method
    public void UpdateEquipSlot()
    {
        equipOneImage.sprite = equipSlotOne.GetItemIcon();
        equipTwoImage.sprite = equipSlotTwo.GetItemIcon();
    }
    #endregion
    #region Input Functions    
    void OnMenuOpen(InputValue value)
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameManager.instance.ESCPressed();
        }
        else if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            GameManager.instance.ToggleInventory();
        }
    }
    void OnMove(InputValue value)
    {
        playerController.MoveInput = value.Get<Vector2>();
        Debug.Log("hi");
    }
    void OnLook(InputValue value)
    {
        playerController.LookInput = value.Get<Vector2>();
    }
    void OnSprint(InputValue value)
    {
        playerController.SprintInput = value.isPressed;
    }
    private void OnValidate()
    {
        if(playerController  == null) playerController = GetComponent<PlayerController>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            playerController.TryJump();
        }
    }

    void OnFire(InputValue value)
    {
        playerController.shootPressed = value.isPressed;

    }
    void OnSwitchRate(InputValue value)
    {
        playerController.changeWeaponRate();
    }

    void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            if(playerController.interactableObject != null)
            {
                Iinteract obj = playerController.interactableObject.GetComponent<Iinteract>();
                if(obj != null)
                {
                     obj.Interact();
                }

            }
        }
    }
    #endregion

}
