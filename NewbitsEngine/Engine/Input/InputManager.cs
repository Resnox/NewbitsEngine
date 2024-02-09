using System;
using System.Collections.Generic;

using NewbitsEngine.Engine.Input.Engine;

namespace NewbitsEngine.Engine.Input;

public class InputManager
{
	private readonly Dictionary<string, ActionsMap> actionsMaps = new();
	private readonly Dictionary<Type, InputEngine> inputEngines = new();

	public void Update()
	{
		foreach (InputEngine inputEngine in inputEngines.Values) inputEngine.Update();
	}

	public ActionsMap RegisterActionMap(string name)
	{
		actionsMaps[name] = new ActionsMap(this);
		return actionsMaps[name];
	}

	public ActionsMap GetActionMap(string name)
	{
		if (actionsMaps.TryGetValue(name, out ActionsMap value)) return value;

		throw new ArgumentException($"{name} unknown");
	}

	public void RegisterEngine<T>() where T : InputEngine
	{
		if (Activator.CreateInstance(typeof(T), this) is not InputEngine engine) return;

		engine.Setup();
		inputEngines[typeof(T)] = engine;
	}

	public T GetEngine<T>() where T : InputEngine
	{
		if (inputEngines.TryGetValue(typeof(T), out InputEngine value)) return (T) value;

		RegisterEngine<T>();
		if (inputEngines.TryGetValue(typeof(T), out InputEngine valueSecondTry)) return (T) valueSecondTry;

		throw new ArgumentException($"{typeof(T)} unknown");
	}
}
