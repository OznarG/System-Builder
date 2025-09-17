using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTESTKEY(InputValue value)
    {
        if(value.isPressed)
        {
            EnterPlacementMode();
        }
    }
    void OnTESTKEYTWO(InputValue value)
    {
        if (value.isPressed)
        {
            ExitPlacementMode();
        }
    }

    private void EnterPlacementMode()
    {
        Debug.Log("Entering Placement Mode");
    }
    
    private void ExitPlacementMode()
    {
        Debug.Log("Exiting Placement Mode");
    }
}
