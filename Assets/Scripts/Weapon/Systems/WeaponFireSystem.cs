using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.UI.Image;

public class WeaponFireSystem : IEcsRunSystem
{
    private EcsFilter<PlayerComponent, VirtualCameraComponent>.Exclude<DamageState, DeathState> playerFilter;
    private EcsFilter<WeaponComponent>.Exclude<FireContdown, ReloadCountdown> weaponFilter;
    private EcsFilter<FireContdown> fireCountdownFilter;
    private PlayerData playerData;
    private StateService stateService;
    private WeaponService weaponService;

    public void Run()
    {
        foreach (var plr in playerFilter)
        {
            ref var playerEntity = ref playerFilter.GetEntity(plr);
            ref var player = ref playerFilter.Get1(plr);
            ref var virtualCamera = ref playerFilter.Get2(plr);

            foreach (var wpn in weaponFilter)
            {
                ref var weaponEntity = ref weaponFilter.GetEntity(wpn);
                ref var weaponComponent = ref weaponFilter.Get1(wpn);

                var isFire = weaponEntity.Has<TryFire>();
                var isAutoFire = weaponEntity.Has<TryAutoFire>();

                if (isFire || isAutoFire)
                {
                    switch (weaponComponent.weaponData)
                    {
                        case FirearmWeaponData firearm:
                            if (!firearm.isAutomatic && isFire)
                            {
                                FirearmFire(ref weaponComponent, virtualCamera.mainCamera, player.animator, ref weaponEntity);
                                break;
                            }
                            if (firearm.isAutomatic && isAutoFire)
                            {
                                FirearmFire(ref weaponComponent, virtualCamera.mainCamera, player.animator, ref weaponEntity);
                            }
                            break;
                        case MeleeWeaponData melee:
                            if (isAutoFire)
                            {
                                MeleeAttack(ref weaponComponent, virtualCamera.mainCamera, player.animator, ref weaponEntity, ref playerEntity);
                            }
                            break;
                    }
                }
            }
        }

        foreach (var ctd in fireCountdownFilter)
        {
            ref var countdown = ref fireCountdownFilter.Get1(ctd);
            countdown.value -= Time.deltaTime;
            if (countdown.value <= 0f)
            {
                fireCountdownFilter.GetEntity(ctd).Del<FireContdown>();
            }
        }
    }

    private void FirearmFire(ref WeaponComponent weaponCompoent, Camera camera, Animator playerAnimator, ref EcsEntity weaponEntity)
    {
        var weaponItem = weaponCompoent.weaponItem;
        var firearmObject = weaponCompoent.weaponObject as FirearmObject;
        var firearmData = weaponCompoent.weaponData as FirearmWeaponData;
        //логика:
        //если нет патронов - не выполнять
        if (weaponItem.count > 0)
        {
            weaponItem.count--;

            firearmObject.muzzleFlash.Play();
            firearmObject.audioSource.PlayOneShot(firearmData.shootClip);
            weaponEntity.Replace(new FireContdown(60f / firearmData.fireRate));
            playerAnimator.Play(firearmData.reactionAnim, firearmData.reactionLayer);

            var origin = camera.transform.position;
            var direction = camera.transform.forward;
            var distance = firearmData.distance;
            var force = firearmData.force;
            var damage = firearmData.damage;
            var mask = playerData.weaponInteractableMask;

            weaponService.ShootCast(origin, direction, distance, force, damage, mask);
            //var shootCast = new ShootCastEvent()
            //{
            //    origin = camera.transform.position,
            //    direction = camera.transform.forward,
            //    distance = firearmData.distance,
            //    force = firearmData.force,
            //    damage = firearmData.damage,
            //    mask = playerData.weaponInteractableMask
            //};
            //weaponEntity.Replace(shootCast);

            weaponEntity.Get<AmmoUpdateEvent>();
        }
    }

    private void MeleeAttack(ref WeaponComponent weaponCompoent, Camera camera, Animator playerAnimator, ref EcsEntity weaponEntity, ref EcsEntity playerEntity)
    {
        var weaponItem = weaponCompoent.weaponItem;
        var meleeObject = weaponCompoent.weaponObject as MeleeObject;
        var meleeData = weaponCompoent.weaponData as MeleeWeaponData;

        playerAnimator.CrossFade(meleeData.attackAnimationName, .3f, 0);

        weaponEntity.Replace(new FireContdown(meleeData.attackTime));

        stateService.SwitchState<AttackState>(ref playerEntity, meleeData.attackTime);
    }
}
