using System.Diagnostics;
using Godot;

namespace GdEcs
{

    [ExportCustomNode("system", "Node")]
    public class FourDirectionalUserInputSystem : EntitySystemNode
    {

        [Export]
        public override int EntitySystemPriority { get; set; } = 0;

        protected override void ProcessEntity(IEntity entity, float delta)
        {
            base.ProcessEntity(entity, delta);

            var compStore = entity.GetEntityComponentStore();
            var inputComp = compStore.GetFirstComponentOfType<FourDirectionalUserInputComponent>()!;
            var dirComp = compStore.GetFirstComponentOfType<Directional2DComponent>()!;

            dirComp.RawDirVec = inputComp.RawDirVec;
        }

        protected override bool ShouldProcessEntity(IEntity entity)
        {
            return entity.GetEntityComponentStore().HasComponentsOfAllTypes(
                typeof(FourDirectionalUserInputComponent),
                typeof(Directional2DComponent)
            );
        }

    }

}