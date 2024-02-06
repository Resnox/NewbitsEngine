namespace NewbitsEngine.Engine.Input.Value;

public interface IInputValue
{
}

public interface IInputValue<T> : IInputValue
{
    public T GetValue();
}