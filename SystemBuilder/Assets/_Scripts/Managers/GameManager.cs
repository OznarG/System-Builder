using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player References")]
    public FPPlayer player;
    public Inventory playerInventoryScript;

    [Header("----- Menus -----")]
    public bool isPaused;
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject playerInventoryMenu;

    [Header("----- Inventory Management -----")]
    public GameObject previuslySelectedSlot = null;
    public GameObject selectedSlot = null;
    public Color backgroundColor;
    public AudioSource inventoryAud;
    public AudioClip pickup;

    public Image crossHair;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Methods Menus
    public void ESCPressed()
    {
        if(!isPaused)
        {
            PauseGame();
        }
        else
        {
            UnPauseGame();
        }
    }
    public void PauseGame(bool cursorOn = true)
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = cursorOn;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        activeMenu = pauseMenu;

    }
    public void UnPauseGame()
    {
        //if (activeMenu == playerInventoryScript.isOpen)
        //{
        //    //Need replaced with belt
        //    selectedSlot.GetComponentInParent<SlotBackground>().selected = false;
        //    selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();
        //    selectedSlot = previuslySelectedSlot;
        //    selectedSlot.GetComponentInParent<SlotBackground>().selected = true;
        //    selectedSlot.GetComponentInParent<SlotBackground>().UpdateSelection();
        //    playerInventoryScript.isOpen = false;
        //    //instance.CharacterStatsMenu.SetActive(false);
        //    instance.playerInventory.SetActive(false);
        //    //***IMPLEMENT WHEN I ADD WEAPON ****
        //    //if (selectedSlot.GetComponentInParent<SlotBackground>().GetComponentInChildren<Slot>().GetItemType() == 10)
        //    //{
        //    //    playerScript.currentWeapon.SetActive(true);
        //    //}
        //}

        Time.timeScale = 1;
        isPaused = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
        }

        //activeMenu = buttonsMenus.falseMenu;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ToggleInventory(bool cursorOn = true)
    {
        if(!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            Cursor.visible = cursorOn;
            Cursor.lockState = CursorLockMode.Confined;
            activeMenu = playerInventoryMenu;
            activeMenu.SetActive(true);
        }
        else if(isPaused && activeMenu == playerInventoryMenu)
        {
            Time.timeScale = 1;
            isPaused = false;
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            if (activeMenu != null)
            {
                activeMenu.SetActive(false);
            }

            //activeMenu = buttonsMenus.falseMenu;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
   
    #endregion
}
