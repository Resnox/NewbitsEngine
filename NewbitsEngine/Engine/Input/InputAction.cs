using System.Collections.Generic;

using NewbitsEngine.Engine.Input.Value;

namespace NewbitsEngine.Engine.Input;

public class InputAction
{
    private readonly List<IInputValue> values = new();

    public InputAction(params IInputValue[] values)
    {
        this.values.AddRange(values);
    }

    public void Add(IInputValue value)
    {
        values.Add(value);
    }

    public T GetValue<T>()
    {
        foreach (var value in values)
            if (value is IInputValue<T> inputValue &&
                !EqualityComparer<T>.Default.Equals(inputValue.GetValue(), default))
                return inputValue.GetValue();

        return default;
    }
}