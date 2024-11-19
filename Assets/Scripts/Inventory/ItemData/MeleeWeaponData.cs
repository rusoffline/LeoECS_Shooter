using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Items/Weapons/Melee")]
public class MeleeWeaponData : WeaponData
{
    [Header("Melee Data:")]
    public float attackTime;
    public string attackAnimationName = "Light_Melee_Attack";
    public AudioClip hitClip;
}