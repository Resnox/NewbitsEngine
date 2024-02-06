using System;
using System.Collections.Generic;

using NewbitsEngine.Engine.Input.Value;

namespace NewbitsEngine.Engine.Input;

public class ActionsMap
{
    private readonly Dictionary<string, InputAction> actions = new();
    private readonly InputManager manager;

    public ActionsMap(InputManager inputManager)
    {
        manager = inputManager;
    }

    public InputAction GetAction(string name)
    {
        if (actions.TryGetValue(name, out var value)) return value;

        throw new ArgumentException($"{name} unknown");
    }

    public ActionsMap RegisterAction(string name, params IInputValue[] inputBindings)
    {
        foreach (var inputBinding in inputBindings)
            if (actions.TryGetValue(name, out var action))
                action.Add(inputBinding);
            else
                actions[name] = new InputAction(inputBinding);

        return this;
    }
}