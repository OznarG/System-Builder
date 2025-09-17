using System.Collections.Generic;
using UnityEngine;

public class PrevValidPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask invalidLayers;
    public bool IsValid { get; private set; } = true;
    private HashSet<Collider> _colldingObjects = new HashSet<Collider>();

    // bitwise left shift opperator (<<) and bitwise and operator (&) ---> needs research
    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            _colldingObjects.Add(other);
            IsValid = false;
            Debug.Log(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            _colldingObjects.Remove(other);
            IsValid = _colldingObjects.Count <= 0;
        }
    }
}
