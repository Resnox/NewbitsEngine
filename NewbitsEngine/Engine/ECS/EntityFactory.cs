using Ninject;
using Ninject.Parameters;

namespace NewbitsEngine.Engine.ECS;

public class EntityFactory
{
	private readonly IKernel kernel;

	public EntityFactory(IKernel kernel)
	{
		this.kernel = kernel;
	}

	public Entity NewEntity(Scene scene)
	{
		return kernel.Get<Entity>(parameters: new ConstructorArgument("scene", scene, true));
	}
}
