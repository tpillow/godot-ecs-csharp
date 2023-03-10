using System;
using GdEcs;
using Godot;

public class Example1 : Node
{

    public override void _Ready()
    {
        base._Ready();

        EntityComponentSystem.I.Initialize(GetTree());
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("ui_accept"))
        {
            var dude = GetNode<KinematicBody2DEntity>("%DudeEntity");
            if (dude.GetEntityComponentStore().HasComponentsOfType<FourDirectionalUserInputComponent>())
            {
                dude.RemoveEntityComponent(dude.GetEntityComponentStore().GetFirstComponentOfType<FourDirectionalUserInputComponent>()!);
            }
            else
            {
                dude.AddEntityComponent(new FourDirectionalUserInputComponent());
            }
        }

        if (Input.IsActionJustPressed("ui_cancel"))
        {
            var player = GetNode<KinematicBody2DEntity>("%PlayerEntity");
            var dude = GetNode<KinematicBody2DEntity>("%DudeEntity");
            var entities = GetNode<Node>("%Entities");

            var newDude = (KinematicBody2DEntity)dude.Duplicate();
            newDude.Position = player.Position + new Vector2(75, 0);
            newDude.RemoveEntityComponentsOfType<FourDirectionalUserInputComponent>();
            entities.AddChild(newDude);
        }
    }

}
