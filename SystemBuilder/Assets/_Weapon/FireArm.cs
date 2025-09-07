using UnityEngine;
public class FireArm : Weapon
{
    WeaponType weaponType;
    FireArmType fireArmType;
    [SerializeField] bool automaticAvailable;
    [SerializeField] bool semi_automaticAvailable;
    [SerializeField] bool rafagaAvailable;

    float shootRate;

    public override void Use()
    {
        Debug.Log("Shooting "+ fireArmType.ToString());
    }

}
