using UnityEngine;

public class ItemPickUp : MonoBehaviour, Iinteract
{
    [SerializeField] bool playerIn;
    [SerializeField] Item thisItem;
    [SerializeField] int amounToAdd;

    #region Iinteract Methods
    public void ActionText()
    {
        GameManager.instance.player.playerHUD.buttonsInfo[0].text = "E: To Pick";
        GameManager.instance.player.playerHUD.buttonsInfoBG[0].gameObject.SetActive(true);

    }
    public void HideActionText()
    {
        GameManager.instance.player.playerHUD.buttonsInfoBG[0].gameObject.SetActive(false);
        GameManager.instance.player.playerHUD.buttonsInfoBG[1].gameObject.SetActive(false);
        GameManager.instance.player.playerHUD.buttonsInfoBG[2].gameObject.SetActive(false);
        GameManager.instance.player.playerHUD.buttonsInfoBG[3].gameObject.SetActive(false);

    }
    public void Interact()
    {
        if (GameManager.instance.playerInventoryScript.AddItem(thisItem, thisItem.amountToAdd + amounToAdd))
        {
            //AudioManager.instance.PlaySFX(AudioManager.instance.pickUp);
            Debug.Log("Added Item Function");
            Destroy(gameObject);
            HideActionText();
        }
    }
    #endregion
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerIn == false)
        {
            //Debug.Log("INSIDE");
            playerIn = true;
            //SCRIPT CACA PARA OTRA COSA
            ////Player Belt is not implemented YET, SO it will be added later

            //if (thisItem.type == 5)
            //{
            //    gameManager.instance.inventoryAud.PlayOneShot(gameManager.instance.pickup);
            //    if (thisItem.ID == 50)
            //    {
            //        gameManager.instance.smgAmmo += thisItem.amountToAdd;
            //        Destroy(gameObject);
            //    }
            //    else if (thisItem.ID == 51)
            //    {
            //        gameManager.instance.rifleTotalAmmo += thisItem.amountToAdd;
            //        Destroy(gameObject);
            //    }
            //    else if (thisItem.ID == 100)
            //    {
            //        gameManager.instance.upgradeParts += thisItem.amountToAdd;
            //        Destroy(gameObject);
            //    }
            //    if (gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().GetComponentInChildren<Slot>().GetItemType() == 10)
            //    {

            //        gameManager.instance.playerScript.Weapons[(gameManager.instance.selectedSlot.GetComponentInParent<SlotBackground>().GetComponentInChildren<Slot>().GetID() * -1) - 1].GetComponent<Gun>().UpdateGunUI();
            //    }
            //    return;
            //}
            //if (thisItem.type == 10)
            //{
            //    gameManager.instance.playerScript.Weapons[thisItem.ID * -1 - 1].GetComponent<Gun>().isPicked = true;
            //    Debug.Log(thisItem.ID * -1 - 1);
            //}

            //NEED TO MAKE THIS FOR BOTH, PLAYERCONTROLLER AND THIRD PERSON
            //bool spaceInBelt = gameManager.instance.thirdPersonPlayerController.PlayerBeltHaveSpace(thisItem);

            //if (spaceInBelt)
            //{
            //    if (gameManager.instance.thirdPersonPlayerController.AddItem(thisItem))
            //    {
            //        Destroy(gameObject);
            //    }

            //}

            //else
            //{
            //    //Add this item to the player inventory
            //    if (gameManager.instance.playerInventoryScript.AddItem(thisItem))
            //    {
            //        Destroy(gameObject);
            //    }

            //}
            if (GameManager.instance.playerInventoryScript.AddItem(thisItem, thisItem.amountToAdd + amounToAdd))
            {
                //AudioManager.instance.PlaySFX(AudioManager.instance.pickUp);
                Destroy(gameObject);
            }


        }
    }
    */
}
