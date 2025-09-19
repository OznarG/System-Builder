using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacer : MonoBehaviour
{
    [Header("Placement Parameters")]
    [SerializeField] private GameObject placeableObjectPrefab;
    [SerializeField] private GameObject previewObjectPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementSurfaceLayerMask;

    [Header("Preview Materials")]
    [SerializeField] private Material previewMaterial;
    [SerializeField] private Color valisColor;
    [SerializeField] private Color invalidColor;

    [Header("Raycast Parameters")]
    [SerializeField] private float objectDistantceFromPlayer;
    [SerializeField] private float raycastStartVerticalOffset;
    [SerializeField] private float raycastDistance;

    private GameObject _previewObject = null;
    private Vector3 _currentPlacementPosition = Vector3.zero;
    private bool _inplacementMode = false;
    private bool _valistBuildState = false;

    #region Inputs
    void OnTESTTHREE(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            GameManager.instance.ToggleBuilderMenu();
        }
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
    //This needs to be moved or replaced
    void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            PlaceObject();
        }
    }
    #endregion

    private void Update()
    {
        if(_inplacementMode)
        {
            UpdateCurrentPlacementPosition();

            if(CanPlaceObject())
            {
                SetValidPreviewState();
            }
            else
                SetInvalidPreviewState();
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
    #region Placement Functions
    private void PlaceObject()
    {
        if (!_inplacementMode || !_valistBuildState)
        {
            return;
        }
        Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
        Instantiate(placeableObjectPrefab, _currentPlacementPosition, rotation, transform);

        ExitPlacementMode();
    }
    public void EnterPlacementMode()
    {
        if (_inplacementMode)
        {
            return;
        }
        if(GameManager.instance.isPaused && GameManager.instance.activeMenu == GameManager.instance.BuilderMenu)
        {
            GameManager.instance.ToggleBuilderMenu();
        }
        Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
        _previewObject = Instantiate(previewObjectPrefab, _currentPlacementPosition, rotation, transform);
        _inplacementMode = true;    
    }
    
    private void ExitPlacementMode()
    {

        _inplacementMode = false;
        Destroy(_previewObject);
        _previewObject= null;
    }
    private bool CanPlaceObject()
    {
        if(_previewObject == null)
            return false;
        return _previewObject.GetComponentInChildren<PrevValidPlacement>().IsValid;
    }
    private void SetValidPreviewState()
    {
        previewMaterial.color = valisColor;
        _valistBuildState = true;
    }
    private void SetInvalidPreviewState()
    {
        previewMaterial.color = invalidColor;
        _valistBuildState = false;
    }

    public void SetObjectTo(GameObject obj, GameObject objPreview)
    {
        placeableObjectPrefab = obj;
        previewObjectPrefab = objPreview;
    }
    #endregion
}
