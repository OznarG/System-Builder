using UnityEngine;

public enum WeaponType
{
    Melee,
    FireArm,
    Item
}


public abstract class Weapon : MonoBehaviour
{
    public abstract void Use();
}
