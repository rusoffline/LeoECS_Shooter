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
            .Add(new ElectrocityInitSystem())
            .Add(new BridgeInitSystem())
            //game
            .Add(new GameOverSystem())
            //interact:
            .Add(new PlayerInteractiveSystem())
            //invetory:
            .Add(new InventoryUISystem())
            .Add(new InventoryPickupSystem())
            .Add(new InventoryRequestSystem())
            .Add(new InventoryUseItemSystem())
            .Add(new InventorySyncSystem())
            //weapons:
            //.Add(new WeaponTestSystem())
            .Add(new WeaponEquipSystem())
            .Add(new WeaponInputSystem())
            .Add(new WeaponFireSystem())
            .Add(new WeaponReloadSystem())
            //.Add(new WeaponCastSystem())
            //animation hitting:
            .Add(new MeleeHitSystem())
            .Add(new EnemyHitSystem())
            //damage:
            .Add(new HealthSystem())
            //player:
            .Add(new StateLifetimeSystem())
            .Add(new GroundCheckSystem())
            .Add(new PlayerAnimatorInputSystem())
            .Add(new PlayerControlSystem())
            .Add(new PlayerStateSystem())
            .Add(new PlayerVoiceSystem())
            //enemy:
            .Add(new EnemyDetectionSystem())
            .Add(new TestEnemyControlSystem())
            .Add(new TestEnemyStateSystem())
            .Add(new EnemyVoiceSystem())
            .Add(new EnemyHitAudioSystem())
            //gui:
            .Add(new AmmoCounterSystem())
            .Add(new PlayerHealthBarSystem())
            .Add(new PickupNotificationSystem())
            .Add(new InteractNotificationSystem())
            //events:
            .OneFrame<PickupItemEvent>()
            .OneFrame<RequestItemEvent>()
            .OneFrame<UseItemEvent>()
            .OneFrame<InventorySyncEvent>()
            .OneFrame<EnterState>()
            .OneFrame<TryFire>()
            .OneFrame<TryReload>()
            .OneFrame<TryAutoFire>()
            .OneFrame<ShootCastEvent>()
            .OneFrame<ExplosionCastEvent>()
            .OneFrame<AmmoUpdateEvent>()
            .OneFrame<HealthUpdateEvent>()
            .OneFrame<PickupNotifEvent>()
            .OneFrame<InteractNotifEvent>()
            .OneFrame<AttackEvent>()
            .OneFrame<DamageEvent>()
            .OneFrame<ImpactEvent>()
            .OneFrame<TryUseElectropowerEvent>()
            .OneFrame<AddElectropowerEvent>()
            .OneFrame<GameOverEvent>()
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
