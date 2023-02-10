using System.Diagnostics;
using System.Xml;
using Godot;

namespace GdEcs
{

    public static class GdEcsExtensions
    {

        static GdEcsExtensions() { }

        public static bool IsEntity(this Node node)
        {
            return node is IEntity;
        }

        public static bool IsEntitySystem(this Node node)
        {
            return node is IEntitySystem;
        }

        public static bool IsEntityComponent(this Node node)
        {
            return node is IEntityComponent;
        }

        public static ulong GetEntityId(this IEntity entity)
        {
            return EntityComponentSystem.I.GetEntityId(entity);
        }

        public static EntityComponentStore GetEntityComponentStore(this IEntity entity)
        {
            return EntityComponentSystem.I.GetEntityComponentStore(entity);
        }

        public static void AddEntityComponent(this Node node, IEntityComponent component)
        {
            Debug.Assert(node.IsEntity());
            Debug.Assert(component is Node);
            node.AddChild((Node)component);
        }

        public static void RemoveEntityComponent(this Node node, IEntityComponent component)
        {
            Debug.Assert(node.IsEntity());
            Debug.Assert(component is Node);
            var compNode = (Node)component;
            if (compNode.GetParent() != null)
                compNode.GetParent().RemoveChild(compNode);
        }

    }

}