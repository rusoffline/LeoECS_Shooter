using UnityEngine;

[CreateAssetMenu(fileName = "New Firearm Weapon", menuName = "Items/Weapons/Firearm")]
public class FirearmWeaponData : WeaponData
{
    [Header("Firearm Data:")]
    public bool isAutomatic;
    [Tooltip("выстрелов в минуту (RPM)")]
    public float fireRate;
    public float reloadTime;           
    public int magazineCapacity;
    public AmmoData requiredAmmo;
    public AudioClip shootClip;
    public AudioClip reloadClip;
    [Header("Body Reaction:")]
    public string reactionAnim = "Shoot";
    public int reactionLayer = 2;
}
