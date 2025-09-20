using UnityEngine;

public class FirePlace : MonoBehaviour, Iinteract
{
    [SerializeField] GameObject spawnObj;
    [SerializeField] bool builderMode;

    public void ActionText()
    {
        if(builderMode)
        {
            GameManager.instance.player.playerHUD.buttonsInfo[0].text = "E To Build";
        }
        else
        {
            GameManager.instance.player.playerHUD.buttonsInfo[0].text = "E To Open";
        }
        GameManager.instance.player.playerHUD.buttonsInfoBG[0].gameObject.SetActive(true);

    }

    public void HideActionText()
    {
        GameManager.instance.player.playerHUD.buttonsInfoBG[0].gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (builderMode)
        {

            Instantiate(spawnObj, gameObject.transform.position, transform.rotation, transform);
        }
        else
        {
            Debug.Log("Whenever this is implemented");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
