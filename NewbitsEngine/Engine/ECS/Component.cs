namespace NewbitsEngine.Engine.ECS;

public abstract class Component
{

	private bool enabled = true;

	protected Entity entity;

	public Component(Entity entity)
	{
		this.entity = entity;
	}
	public Transform Transform
	{
		get
		{
			return entity.Transform;
		}
	}
	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			if (enabled != value)
			{
				switch (value)
				{
					case true:
						OnEnable();
						break;

					case false:
						OnDisable();
						break;
				}

				enabled = value;
			}
			;
		}
	}

	public virtual void OnEnable() {}
	public virtual void OnDisable() {}
}
