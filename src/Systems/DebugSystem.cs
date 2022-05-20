using System.Linq;
using RelEcs;
using Zelda.Nodes.Debug;

namespace Zelda.Systems;

public class DebugSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!commands.TryGetElement<Debug>(out var debug)) return;
        if (!commands.TryGetElement<WorldInfo>(out var info)) return;

        var s = $"Entities: {info.EntityCount}\nArchetypes: {info.ArchetypeCount}\nSystems: {info.SystemCount}";
        
        foreach (var (type, time) in info.SystemExecutionTimes)
        {
            s += $"\n{type.Name}: {time}";
        }

        debug.Label.Text = s;
    }
}