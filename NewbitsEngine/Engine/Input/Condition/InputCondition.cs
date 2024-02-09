namespace NewbitsEngine.Engine.Input.Condition;

public abstract class InputCondition
{
	protected readonly InputManager inputManager;

	public InputCondition(InputManager inputManager)
	{
		this.inputManager = inputManager;
	}

	public abstract bool IsValid();
}
