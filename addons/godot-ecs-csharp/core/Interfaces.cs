using Godot;

namespace GdEcs
{

    public delegate void EntityDelegate(IEntity entity);
    public delegate void EntitySystemDelegate(IEntitySystem system);
    public delegate void NodeDelegate(Node node);

    public interface IEntityComponent { }

    public interface IEntity
    {
        ulong EntityId { get; set; }

        EntityComponentStore ComponentStore { get; }

        void AddComponent(IEntityComponent component);
        void RemoveComponent(IEntityComponent component);
    }

    public interface IEntitySystem
    {
        int EntitySystemPriority { get; }

        void RefreshProcessesEntity(IEntity entity);

        void DoProcess(float delta);
        void DoPhysicsProcess(float delta);
    }

}