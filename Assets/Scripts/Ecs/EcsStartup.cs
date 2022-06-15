using Leopotam.Ecs;
using Rts.Ecs.Components;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using Rts.Ecs.Components.Unit.Command;
using Rts.Ecs.Systems;
using Rts.Ecs.Systems.Character;
using Rts.Ecs.Systems.Common;
using Rts.Ecs.Systems.Input;
using Rts.Ecs.Systems.Input.Actions;
using Rts.Ecs.Systems.Resources;
using Rts.Ecs.Systems.Structure;
using Rts.Ecs.Systems.Structure.Building;
using Rts.Ecs.Systems.Unit;
using Rts.Ecs.Systems.Unit.Command;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Services;
using Rts.Services.Control;
using Rts.View;
using Rts.View.Ui;
using Rts.View.Ui.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rts.Ecs
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private CommonConfig commonConfig;
        [SerializeField] private BuildConfig buildConfig;
        [SerializeField] private SceneData sceneData;
        [SerializeField] private CanvasView canvasView;
        [SerializeField] private CanvasCharacterView canvasCharacterView;

        private EcsWorld _ecsWorld;
        private EcsSystems _systems;
        private GameData _gameData;
        private void Start()
        {
            _ecsWorld = new EcsWorld();
            _systems = new EcsSystems(_ecsWorld);

            _gameData = new GameData();
            var commandService = new CommandService(commonConfig);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // init
                .Add(new InitGameDataSystem())
                .Add(new InitBuildingUiSystem())
                .Add(new InitUnitSystem())
                .Add(new InitStructuresSystem())
                .Add(new InitMinesSystem())
                
                // run
                // input
                .Add(new MouseRaycastSystem())
                .Add(new ControlCameraSystem())
                // unit
                .Add(new HoverSystem())
                .Add(new SelectSystem())
                .Add(new MiningSystem())
                .Add(new DamageSystem())
                .Add(new ShootSystem())
                .Add(new DeadSystem())
                // build
                .Add(new SelectBuildPlaceSystem())

                // one-frame systems
                // unit
                .Add(new OnRemoveSelectSystem())
                .Add(new OnSelectSystem())
                .Add(new OnHoverSystem())
                .Add(new OnRemoveHoverActionSystem())
                .Add(new OnPickupSystem())
                .Add(new DestroyUnitSystem())
                // build
                .Add(new StartSelectBuildPlaceSystem())
                .Add(new SendBuildersSystem())
                .Add(new BuildingCompleteSystem())

                // command
                .Add(new CommandStartSystem())
                .Add(new CommandUpdateSystem())
                .Add(new CommandStopSystem())
                
                // resources
                .Add(new UpdateResourceSystem())
                
                // one-frame components
                .OneFrame<SelectAction>()
                .OneFrame<RemoveSelectAction>()
                .OneFrame<HoverAction>()
                .OneFrame<RemoveHoverAction>()
                .OneFrame<PickupActionComponent>()
                .OneFrame<SelectBuildPlaceAction>()
                .OneFrame<UpdateResourceAction>()
                .OneFrame<SendBuildersAction>()
                .OneFrame<BuildingCompleteAction>()
                .OneFrame<CommandStartAction>()
                .OneFrame<CommandStopAction>()

                // inject service instances here (order doesn't important), for example:
                .Inject(commandService)
                .Inject(commonConfig)
                .Inject(buildConfig)
                .Inject(sceneData)
                .Inject(canvasView)
                .Inject(_gameData)

                // services
                .Inject(new BuildingService(_ecsWorld, canvasView, buildConfig, sceneData, commonConfig))
                .Inject(new ControlService(commandService, commonConfig))
                .Inject(new SelectService(canvasView, sceneData, _ecsWorld))
                
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }
    }
}