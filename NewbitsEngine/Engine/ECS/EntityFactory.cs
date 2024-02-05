using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;

namespace NewbitsEngine.Engine.ECS;

public class EntityFactory
{
	private IKernel kernel;
	
	public EntityFactory(IKernel kernel)
	{
		this.kernel = kernel;
	}
	
	public Entity NewEntity(Scene scene)
	{
		return kernel.Get<Entity>(parameters: new ConstructorArgument("scene", scene, true));
	}
}
