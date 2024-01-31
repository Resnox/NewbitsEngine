using System;

namespace NewbitsEngine.Core;

public abstract class Component
{
    protected Entity entity;
    private bool _enabled = true;

    public bool Enabled
    {
        get => _enabled;
        set {
            if (_enabled != value)
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

                _enabled = value;
            }; 
        }
    }

    public Component(Entity entity)
    {
        this.entity = entity;
    }
    
    public virtual void OnEnable() {}
    public virtual void OnDisable() {}
}