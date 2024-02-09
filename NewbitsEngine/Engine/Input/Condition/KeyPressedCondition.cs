using Microsoft.Xna.Framework.Input;

using Keyboard = NewbitsEngine.Engine.Input.Engine.Keyboard;

namespace NewbitsEngine.Engine.Input.Condition;

public class KeyPressedCondition : InputCondition
{
	private readonly Keys key;

	public KeyPressedCondition(InputManager inputManager, Keys key) : base(inputManager)
	{
		this.key = key;
	}

	public override bool IsValid()
	{
		return inputManager.GetEngine<Keyboard>().IsKeyPressed(key);
	}
}
