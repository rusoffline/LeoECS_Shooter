using UnityEngine;

//[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponData : ItemData
{
    [Header("Weapon Data:")]
    public int weaponIndex;
    public int damage;
    public float distance;
    public float force;
    //public int maxAmmo;
    public WeaponObject weaponObject;
}
