using UnityEngine;

public class BuilderButton : MonoBehaviour
{
    [SerializeField] GameObject preview;
    [SerializeField] GameObject obj;

    public void SetPlacerObject()
    {
        GameManager.instance.objctPlacer.SetObjectTo(obj, preview);
        GameManager.instance.objctPlacer.EnterPlacementMode();
    }
}
