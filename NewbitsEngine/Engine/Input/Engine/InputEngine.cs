namespace NewbitsEngine.Engine.Input.Engine;

public abstract class InputEngine
{
    protected InputManager manager;

    public InputEngine(InputManager manager)
    {
        this.manager = manager;
    }

    public abstract void Setup();
    public abstract void Update();
}