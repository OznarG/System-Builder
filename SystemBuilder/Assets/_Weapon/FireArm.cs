using UnityEngine;
using EnumsWeapon;
public class FireArm : Weapon
{
    [SerializeField] WeaponType weaponType;
    [SerializeField] FireArmType fireArmType;
    [SerializeField] ShootMode shootateType;

    [SerializeField] bool automaticAvailable;
    [SerializeField] bool semi_automaticAvailable;
    [SerializeField] bool rafagaAvailable;


    float shootRate = 0.2f;

    public override bool CanUse(float timer)
    {
        switch(shootateType)
        {
            case ShootMode.AUTOMATIC:
                Debug.Log(shootateType.ToString());
                return timer > shootRate;

            case ShootMode.RAFAGA:
                Debug.Log(shootateType.ToString());
                return timer > shootRate;

            case ShootMode.SEMI_AUTO:
                break;
            default:
                break;
        }
        return false;
    }

    public override void Use()
    {
        Debug.Log("Shooting "+ fireArmType.ToString());
    }

    public void ChangeShootRate()
    {

    }
}
