using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using UnityEngine;

namespace Rts.Ecs.Systems.Unit.Command.States
{
    public abstract class ACommand
    {
        protected EcsEntity _entity;
        private readonly float _interactDistance;
        protected Vector3 _position;
        protected EcsEntity _target;

        public EcsEntity Target => _target;

        protected ACommand(EcsEntity entity, float interactDistance, Vector3 position)
        {
            _entity = entity;
            _interactDistance = interactDistance;
            _position = position;
        }

        protected ACommand(EcsEntity entity, float interactDistance, EcsEntity target)
        {
            _entity = entity;
            _interactDistance = interactDistance;
            _target = target;
        }


        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Stop()
        {
        }

        protected bool IsInteractDistance()
        {
            var transform = _entity.Get<TransformComponent>().Value;
            var distance = 0f;
            if (_target.IsNull())
                distance = Vector3.Distance(transform.position, _position);
            else
                distance = Vector3.Distance(transform.position, _target.Get<TransformComponent>().Value.position);
            return distance <= _interactDistance;
        }

        public void Complete() => _entity.Get<CommandStopAction>();
    }
}