using UnityEngine;

public enum WeaponType
{
    Melee,
    FireArm,
    Item
}
public enum FireArmType
{
    HandGun,
    SMG,
    ShotGun
}

public abstract class Weapon : MonoBehaviour
{

    public abstract void Use();
}
