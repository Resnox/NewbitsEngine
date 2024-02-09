using Ninject;
using Ninject.Parameters;

namespace NewbitsEngine.Engine.ECS;

public class ComponentFactory
{
	private readonly IKernel kernel;

	public ComponentFactory(IKernel kernel)
	{
		this.kernel = kernel;
	}

	public T NewComponent<T>(Entity entity) where T : Component
	{
		return kernel.Get<T>(parameters: new ConstructorArgument("entity", entity, true));
	}
}
