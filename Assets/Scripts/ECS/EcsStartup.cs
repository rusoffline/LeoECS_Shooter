using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    private EcsWorld world;
    private EcsSystems updateSystem;
    private EcsSystems fixUpdateSystem;
    private EcsSystems lateUpdateSystem;

    public UIManager uiManager;
    public PlayerSpawner playerSpawner;
    public PlayerData playerData;
    public EnemyData enemyData;

    private void Start()
    {
        world = new EcsWorld();
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
#endif  
        updateSystem = new EcsSystems(world);
        fixUpdateSystem = new EcsSystems(world);
        lateUpdateSystem = new EcsSystems(world);

        updateSystem
            .Inject(playerSpawner)
            .Inject(playerData)
            .Inject(enemyData)
            .Inject(uiManager)
            .Inject(new GameModeService())
            .Inject(new StateService())
            .Inject(new WeaponService())
            //inits
            .Add(new GameModeInitSystem())
            .Add(new InputSystem())
            .Add(new PlayerInitSystem())
            .Add(new InventoryInitSystem())
            .Add(new InteractableInitSystem())
            .Add(new EnemyInitSystem())
            //interact:
            .Add(new PlayerInteractiveSystem())
            //invetory:
            .Add(new InventoryUISystem())
            .Add(new InventoryPickupSystem())
            .Add(new InventoryRequestSystem())
            .Add(new InventorySyncSystem())
            //weapons:
            //.Add(new WeaponTestSystem())
            .Add(new WeaponEquipSystem())
            .Add(new WeaponInputSystem())
            .Add(new WeaponFireSystem())
            .Add(new WeaponReloadSystem())
            .Add(new WeaponCastSystem())
            .Add(new MeleeHitSystem())
            //damage:
            //player:
            .Add(new StateLifetimeSystem())
            .Add(new GroundCheckSystem())
            .Add(new PlayerAnimatorInputSystem())
            .Add(new PlayerControlSystem())
            .Add(new PlayerStateSystem())
            //enemy:
            .Add(new EnemyDetectionSystem())
            .Add(new TestEnemyControlSystem())
            .Add(new TestEnemyStateSystem())
            //hud:
            .Add(new AmmoCounterSystem())
            .Add(new PickupNotificationSystem())
            .Add(new InteractNotificationSystem())
            //events:
            .OneFrame<PickupItemEvent>()
            .OneFrame<UseItemEvent>()
            .OneFrame<InventorySyncEvent>()
            .OneFrame<EnterState>()
            .OneFrame<TryFire>()
            .OneFrame<TryReload>()
            .OneFrame<TryAutoFire>()
            .OneFrame<ShootCastEvent>()
            .OneFrame<ExplosionCastEvent>()
            .OneFrame<AmmoUpdateEvent>()
            .OneFrame<PickupNotifEvent>()
            .OneFrame<IteractNotifEvent>()
            .OneFrame<AttackEvent>()
            .Init();

        fixUpdateSystem
            .Inject(playerData)
            .Add(new PlayerRotationSystem())
            .Add(new CameraRotationSystem())
            .Add(new WeaponAimSystem())
            .Init();

        lateUpdateSystem
            .Inject(playerData)
            .Init();

        //#if UNITY_EDITOR
        //        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystem);
        //        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixUpdateSystem);
        //#endif
    }

    private void Update()
    {
        updateSystem?.Run();
    }

    private void FixedUpdate()
    {
        fixUpdateSystem?.Run();
    }

    private void LateUpdate()
    {
        lateUpdateSystem?.Run();
    }
    private void OnDestroy()
    {
        updateSystem?.Destroy();
        world?.Destroy();
    }
}
