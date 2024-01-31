using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace NewbitsEngine.Core;

public sealed class Transform
{
    public readonly Entity entity;

    private readonly List<Transform> _children;

    private DirtyFlags _dirtyFlags;

    private Vector2 _localPosition;

    private float _localRotation;

    private Vector2 _localScale;

    private Matrix _localTransform;

    private Transform _parent;

    private Vector2 _position;

    private float _rotation;

    private Matrix _rotationMatrix;

    private Vector2 _scale;

    private Matrix _scaleMatrix;

    private Matrix _translationMatrix;

    private Matrix _worldInverseTransform = Matrix.Identity;

    private Matrix _worldToLocalTransform = Matrix.Identity;

    private Matrix _worldTransform = Matrix.Identity;

    public Transform(Entity entity)
    {
        this.entity = entity;
        _position = _localPosition = Vector2.Zero;
        _scale = _localScale = Vector2.One;
        _rotation = _localRotation = 0f;
        _children = new List<Transform>();
    }

    public Transform Parent
    {
        get => _parent;
        set => SetParent(value);
    }

    public Vector2 Position
    {
        get
        {
            UpdateTransform();
            if (!_dirtyFlags.HasFlag(DirtyFlags.PositionDirty))
                return _position;

            if (Parent == null)
            {
                _position = _localPosition;
            }
            else
            {
                Parent.UpdateTransform();
                Vector2.Transform(_localPosition, Parent._worldTransform);
            }

            return _position;
        }
        set => _position = value;
    }

    public Vector2 LocalPosition
    {
        get
        {
            UpdateTransform();
            return _localPosition;
        }
        set => SetLocalPosition(value);
    }

    public Vector2 Scale
    {
        get
        {
            UpdateTransform();
            return _scale;
        }
        set => SetScale(value);
    }

    public Vector2 LocalScale
    {
        get
        {
            UpdateTransform();
            return _localScale;
        }
        set => SetLocalScale(value);
    }

    public float Rotation
    {
        get
        {
            UpdateTransform();
            return _rotation;
        }
        set => SetRotation(value);
    }

    public float RotationDegrees
    {
        get => MathHelper.ToDegrees(_rotation);
        set => SetRotationDegrees(value);
    }

    public float LocalRotation
    {
        get
        {
            UpdateTransform();
            return _localRotation;
        }
        set => SetLocalRotation(value);
    }

    public float LocalRotationDegrees
    {
        get => MathHelper.ToDegrees(_localRotation);
        set => SetLocalRotationDegrees(value);
    }

    public Matrix LocalTransform
    {
        get => _localTransform;
        set => _localTransform = value;
    }

    public Matrix WorldTransform
    {
        get
        {
            UpdateTransform();
            return _worldTransform;
        }
    }

    public Matrix WorldToLocalTransform
    {
        get
        {
            if (!_dirtyFlags.HasFlag(DirtyFlags.WorldToLocalDirty)) return _worldToLocalTransform;

            if (Parent == null)
            {
                _worldToLocalTransform = Matrix.Identity;
            }
            else
            {
                Parent.UpdateTransform();
                Matrix.Invert(ref Parent._worldTransform, out _worldToLocalTransform);
            }

            _dirtyFlags &= ~DirtyFlags.WorldToLocalDirty;

            return _worldToLocalTransform;
        }
    }

    public Matrix WorldInverseTransform
    {
        get
        {
            UpdateTransform();
            Matrix.Invert(ref _worldTransform, out _worldInverseTransform);

            return _worldInverseTransform;
        }
    }

    public Matrix RotationMatrix
    {
        get => _rotationMatrix;
        set => _rotationMatrix = value;
    }

    public Matrix TranslationMatrix
    {
        get => _translationMatrix;
        set => _translationMatrix = value;
    }

    public Matrix ScaleMatrix
    {
        get => _scaleMatrix;
        set => _scaleMatrix = value;
    }

    public int ChildCount => _children.Count;


    public Transform GetChild(int index)
    {
        return _children[index];
    }

    public Transform SetParent(Transform parent)
    {
        Parent?._children.Remove(this);
        parent?._children.Add(this);
        Parent = parent;

        return this;
    }

    public Transform SetPosition(Vector2 position)
    {
        if (_position == position)
            return this;

        _position = position;
        _localPosition = Parent != null ? Vector2.Transform(_position, _worldToLocalTransform) : _position;

        return this;
    }

    public Transform SetPosition(float x, float y)
    {
        return SetPosition(new Vector2(x, y));
    }

    public Transform SetLocalPosition(Vector2 localPosition)
    {
        if (localPosition == _localPosition)
            return this;

        _localPosition = localPosition;

        return this;
    }

    public Transform SetRotation(float radians)
    {
        _rotation = radians;
        _localRotation = Parent != null ? Parent._rotation + radians : radians;

        return this;
    }

    public Transform SetRotationDegrees(float degrees)
    {
        return SetRotation(MathHelper.ToRadians(degrees));
    }

    public Transform SetLocalRotation(float radians)
    {
        _localRotation = radians;

        return this;
    }

    public Transform SetLocalRotationDegrees(float degrees)
    {
        return SetLocalRotation(MathHelper.ToRadians(degrees));
    }

    public Transform SetScale(Vector2 scale)
    {
        _scale = scale;
        if (Parent != null)
            _localScale = scale / Parent._scale;
        else
            _localScale = scale;

        return this;
    }

    public Transform SetScale(float scale)
    {
        return SetScale(new Vector2(scale));
    }

    public Transform SetLocalScale(Vector2 scale)
    {
        _localScale = scale;

        return this;
    }

    public Transform SetLocalScale(float scale)
    {
        return SetLocalScale(new Vector2(scale));
    }

    private void UpdateTransform()
    {
        if (_dirtyFlags == DirtyFlags.None) return;

        Parent?.UpdateTransform();
        if ((_dirtyFlags & DirtyFlags.LocalDirty) != 0)
        {
            if (_dirtyFlags.HasFlag(DirtyFlags.LocalPositionDirty))
            {
                Matrix.CreateTranslation(_localPosition.X, _localPosition.Y, 0, out _translationMatrix);
                _dirtyFlags &= ~DirtyFlags.LocalPositionDirty;
            }

            if (_dirtyFlags.HasFlag(DirtyFlags.LocalRotationDirty))
            {
                Matrix.CreateRotationZ(_localRotation, out _rotationMatrix);
                _dirtyFlags &= ~DirtyFlags.LocalRotationDirty;
            }

            if (_dirtyFlags.HasFlag(DirtyFlags.LocalScaleDirty))
            {
                Matrix.CreateScale(_localScale.X, _localScale.Y, 1, out _scaleMatrix);
                _dirtyFlags &= ~DirtyFlags.LocalScaleDirty;
            }

            Matrix.Multiply(ref _scaleMatrix, ref _rotationMatrix, out _localTransform);
            Matrix.Multiply(ref _localTransform, ref _translationMatrix, out _localTransform);
        }

        if (Parent != null)
        {
            Matrix.Multiply(ref _localTransform, ref Parent._worldTransform, out _worldTransform);

            _rotation = _localRotation + Parent._rotation;
            _scale = Parent._scale * _localScale;
            _dirtyFlags |= DirtyFlags.WorldInverseDirty;
        }

        _dirtyFlags |= DirtyFlags.WorldToLocalDirty;
    }

    private void SetDirty(DirtyFlags dirtyFlag)
    {
        if ((_dirtyFlags & dirtyFlag) != 0) return;

        _dirtyFlags |= dirtyFlag;

        // if (dirtyFlag.HasFlag(DirtyFlags.PositionDirty))
        //     Entity.OnTransformChanged(Component.Position);
        // else if (dirtyFlag.HasFlag(DirtyFlags.RotationDirty))
        //     Entity.OnTransformChanged(Component.Rotation);
        // else if (dirtyFlag.HasFlag(DirtyFlags.ScaleDirty))
        //     Entity.OnTransformChanged(Component.Scale);

        foreach (var t in _children)
            t.SetDirty(dirtyFlag);
    }

    #region Nested type: DirtyFlags

    [Flags]
    private enum DirtyFlags
    {
        None = 0,
        PositionDirty = 1 << 0,
        ScaleDirty = 1 << 1,
        RotationDirty = 1 << 2,
        LocalPositionDirty = 1 << 3,
        LocalScaleDirty = 1 << 4,
        LocalRotationDirty = 1 << 5,
        LocalDirty = LocalPositionDirty | LocalScaleDirty | LocalRotationDirty,
        WorldToLocalDirty = 1 << 6,
        WorldInverseDirty = 1 << 7
    }

    #endregion
}