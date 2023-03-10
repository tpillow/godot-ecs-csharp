using System.Collections.Generic;
using System.Reflection;
using GdEcs;
using Godot;

[Tool]
public class godot_ecs_csharp : EditorPlugin
{

    private List<string> registeredTypes = new List<string>();

    public override void _EnterTree()
    {
        base._EnterTree();
        Connect("resource_saved", this, nameof(OnResourceSaved));

        ReloadTypes();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        ResetTypes();
    }

    private void ReloadTypes()
    {
        ResetTypes();

        var asm = Assembly.GetExecutingAssembly();
        foreach (var type in asm.GetTypes())
        {
            var exportCustomAttr = type.GetCustomAttribute<ExportCustomNodeAttribute>();
            if (exportCustomAttr == null)
                continue;

            var script = GD.Load<Script>(exportCustomAttr.ScriptPath);
            // GD.Print($"Registering custom type {type.Name}, script: {script.ResourcePath}");
            Texture? icon = null;
            if (exportCustomAttr.IconName != null)
            {
                var iconName = exportCustomAttr.IconName;
                switch (exportCustomAttr.IconName)
                {
                    case "component":
                        iconName = "KeyBezier";
                        break;
                    case "system":
                        iconName = "KeyAnimation";
                        break;
                    case "entity":
                        iconName = "KeyAudio";
                        break;
                }
                icon = GetEditorInterface().GetBaseControl().GetIcon(iconName, "EditorIcons");
            }
            AddCustomType(type.Name, exportCustomAttr.BaseTypePath ?? type.BaseType.Name, script, icon);
            registeredTypes.Add(type.Name);
        }
    }

    private void ResetTypes()
    {
        foreach (var name in registeredTypes)
            RemoveCustomType(name);
        registeredTypes.Clear();
    }

    private void OnResourceSaved(Resource resource)
    {
        ReloadTypes();
    }

}