using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject previewObjectPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementSurfaceLayerMask;

    [SerializeField] private float objectDistantceFromPlayer;
    [SerializeField] private float raycastStartVerticalOffset;
    [SerializeField] private float raycastDistance;

    private GameObject _previewObject = null;
    private Vector3 _currentPlacementPosition = Vector3.zero;
    private bool _inplacementMode = false;

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

    private void Update()
    {
        if(_inplacementMode)
        {
            UpdateCurrentPlacementPosition();
        }
    }
    private void UpdateCurrentPlacementPosition()
    {
        Vector3 cameraForward = new Vector3(playerCamera.transform.forward.x, 0F, playerCamera.transform.forward.z);
        cameraForward.Normalize();

        Vector3 startPos = playerCamera.transform.position + (cameraForward * objectDistantceFromPlayer);
        startPos.y += raycastStartVerticalOffset;

        RaycastHit hitinfo;
        if(Physics.Raycast(startPos, Vector3.down, out hitinfo, raycastDistance, placementSurfaceLayerMask)) 
        {
            _currentPlacementPosition = hitinfo.point;
        }

        Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
        _previewObject.transform.position = _currentPlacementPosition;
        _previewObject.transform.rotation = rotation;
        Debug.DrawRay(startPos, Vector3.down * raycastDistance, Color.red);
    }
    private void EnterPlacementMode()
    {
        Debug.Log("Entering Placement Mode");

        Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
        _previewObject = Instantiate(previewObjectPrefab, _currentPlacementPosition, rotation);
        _inplacementMode = true;    
    }
    
    private void ExitPlacementMode()
    {
        Debug.Log("Exiting Placement Mode");
        _inplacementMode = false;
    }
}
