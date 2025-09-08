using UnityEngine;
namespace EnumsWeapon
{

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
    public enum ShootMode
    {
        AUTOMATIC,
        SEMI_AUTO,
        RAFAGA
    }
}

public abstract class Weapon : MonoBehaviour
{
    public abstract void Use();
    public abstract bool CanUse(float timer, bool state);
}
