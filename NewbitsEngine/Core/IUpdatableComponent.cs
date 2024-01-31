using System;

namespace NewbitsEngine.Core;

public interface IUpdatableComponent
{
    public void Update(TimeSpan time);
}