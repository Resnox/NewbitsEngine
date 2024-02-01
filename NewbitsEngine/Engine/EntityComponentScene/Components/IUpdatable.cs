using System;

namespace NewbitsEngine.Core;

public interface IUpdatable
{
    public void Update(TimeSpan time);
}