using UnityEngine;

public class FirePlace : MonoBehaviour, Iinteract
{
    [SerializeField] GameObject spawnObj;
    public bool builderMode = true;

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
        Debug.Log("Supposed To Hide Text");
    }

    public void Interact()
    {
        if (builderMode)
        {
            Debug.Log("Spawned");
            Instantiate(spawnObj, gameObject.transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Whenever this is implemented");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Spawn " + builderMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
