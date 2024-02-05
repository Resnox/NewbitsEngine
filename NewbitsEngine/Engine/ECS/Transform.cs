using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace NewbitsEngine.Engine.ECS;

public sealed class Transform
{

	private readonly List<Transform> children;
	public readonly Entity entity;

	private DirtyFlags dirtyFlags;

	private Vector2 localPosition;

	private float localRotation;

	private Vector2 localScale;

	private Matrix localTransform;

	private Transform parent;

	private Vector2 position;

	private float rotation;

	private Matrix rotationMatrix;

	private Vector2 scale;

	private Matrix scaleMatrix;

	private Matrix translationMatrix;

	private Matrix worldInverseTransform = Matrix.Identity;

	private Matrix worldToLocalTransform = Matrix.Identity;

	private Matrix worldTransform = Matrix.Identity;

	public Transform(Entity entity)
	{
		this.entity = entity;
		position = localPosition = Vector2.Zero;
		scale = localScale = Vector2.One;
		rotation = localRotation = 0f;
		children = new List<Transform>();
	}

	public Transform Parent
	{
		get
		{
			return parent;
		}
		set
		{
			SetParent(value);
		}
	}

	public Vector2 Position
	{
		get
		{
			UpdateTransform();
			if (!dirtyFlags.HasFlag(DirtyFlags.PositionDirty))
				return position;

			if (Parent == null)
			{
				position = localPosition;
			}
			else
			{
				Parent.UpdateTransform();
				Vector2.Transform(localPosition, Parent.worldTransform);
			}

			return position;
		}
		set
		{
			position = value;
		}
	}

	public Vector2 LocalPosition
	{
		get
		{
			UpdateTransform();
			return localPosition;
		}
		set
		{
			SetLocalPosition(value);
		}
	}

	public Vector2 Scale
	{
		get
		{
			UpdateTransform();
			return scale;
		}
		set
		{
			SetScale(value);
		}
	}

	public Vector2 LocalScale
	{
		get
		{
			UpdateTransform();
			return localScale;
		}
		set
		{
			SetLocalScale(value);
		}
	}

	public float Rotation
	{
		get
		{
			UpdateTransform();
			return rotation;
		}
		set
		{
			SetRotation(value);
		}
	}

	public float RotationDegrees
	{
		get
		{
			return MathHelper.ToDegrees(rotation);
		}
		set
		{
			SetRotationDegrees(value);
		}
	}

	public float LocalRotation
	{
		get
		{
			UpdateTransform();
			return localRotation;
		}
		set
		{
			SetLocalRotation(value);
		}
	}

	public float LocalRotationDegrees
	{
		get
		{
			return MathHelper.ToDegrees(localRotation);
		}
		set
		{
			SetLocalRotationDegrees(value);
		}
	}

	public Matrix LocalTransform
	{
		get
		{
			return localTransform;
		}
		set
		{
			localTransform = value;
		}
	}

	public Matrix WorldTransform
	{
		get
		{
			UpdateTransform();
			return worldTransform;
		}
	}

	public Matrix WorldToLocalTransform
	{
		get
		{
			if (!dirtyFlags.HasFlag(DirtyFlags.WorldToLocalDirty)) return worldToLocalTransform;

			if (Parent == null)
			{
				worldToLocalTransform = Matrix.Identity;
			}
			else
			{
				Parent.UpdateTransform();
				Matrix.Invert(ref Parent.worldTransform, out worldToLocalTransform);
			}

			dirtyFlags &= ~DirtyFlags.WorldToLocalDirty;

			return worldToLocalTransform;
		}
	}

	public Matrix WorldInverseTransform
	{
		get
		{
			UpdateTransform();
			Matrix.Invert(ref worldTransform, out worldInverseTransform);

			return worldInverseTransform;
		}
	}

	public Matrix RotationMatrix
	{
		get
		{
			return rotationMatrix;
		}
		set
		{
			rotationMatrix = value;
		}
	}

	public Matrix TranslationMatrix
	{
		get
		{
			return translationMatrix;
		}
		set
		{
			translationMatrix = value;
		}
	}

	public Matrix ScaleMatrix
	{
		get
		{
			return scaleMatrix;
		}
		set
		{
			scaleMatrix = value;
		}
	}

	public int ChildCount
	{
		get
		{
			return children.Count;
		}
	}


	public Transform GetChild(int index)
	{
		return children[index];
	}

	public Transform SetParent(Transform parent)
	{
		Parent?.children.Remove(this);
		parent?.children.Add(this);
		this.parent = parent;

		return this;
	}

	public Transform SetPosition(Vector2 position)
	{
		if (this.position == position)
			return this;

		this.position = position;
		localPosition = Parent != null ? Vector2.Transform(this.position, worldToLocalTransform) : this.position;

		return this;
	}

	public Transform SetPosition(float x, float y)
	{
		return SetPosition(new Vector2(x, y));
	}

	public Transform SetLocalPosition(Vector2 localPosition)
	{
		if (localPosition == this.localPosition)
			return this;

		this.localPosition = localPosition;

		return this;
	}

	public Transform SetRotation(float radians)
	{
		rotation = radians;
		localRotation = Parent != null ? Parent.rotation + radians : radians;

		return this;
	}

	public Transform SetRotationDegrees(float degrees)
	{
		return SetRotation(MathHelper.ToRadians(degrees));
	}

	public Transform SetLocalRotation(float radians)
	{
		localRotation = radians;

		return this;
	}

	public Transform SetLocalRotationDegrees(float degrees)
	{
		return SetLocalRotation(MathHelper.ToRadians(degrees));
	}

	public Transform SetScale(Vector2 scale)
	{
		this.scale = scale;
		if (Parent != null)
			localScale = scale / Parent.scale;
		else
			localScale = scale;

		return this;
	}

	public Transform SetScale(float scale)
	{
		return SetScale(new Vector2(scale));
	}

	public Transform SetLocalScale(Vector2 scale)
	{
		localScale = scale;

		return this;
	}

	public Transform SetLocalScale(float scale)
	{
		return SetLocalScale(new Vector2(scale));
	}

	private void UpdateTransform()
	{
		if (dirtyFlags == DirtyFlags.None) return;

		Parent?.UpdateTransform();
		if ((dirtyFlags & DirtyFlags.LocalDirty) != 0)
		{
			if (dirtyFlags.HasFlag(DirtyFlags.LocalPositionDirty))
			{
				Matrix.CreateTranslation(localPosition.X, localPosition.Y, 0, out translationMatrix);
				dirtyFlags &= ~DirtyFlags.LocalPositionDirty;
			}

			if (dirtyFlags.HasFlag(DirtyFlags.LocalRotationDirty))
			{
				Matrix.CreateRotationZ(localRotation, out rotationMatrix);
				dirtyFlags &= ~DirtyFlags.LocalRotationDirty;
			}

			if (dirtyFlags.HasFlag(DirtyFlags.LocalScaleDirty))
			{
				Matrix.CreateScale(localScale.X, localScale.Y, 1, out scaleMatrix);
				dirtyFlags &= ~DirtyFlags.LocalScaleDirty;
			}

			Matrix.Multiply(ref scaleMatrix, ref rotationMatrix, out localTransform);
			Matrix.Multiply(ref localTransform, ref translationMatrix, out localTransform);
		}

		if (Parent != null)
		{
			Matrix.Multiply(ref localTransform, ref Parent.worldTransform, out worldTransform);

			rotation = localRotation + Parent.rotation;
			scale = Parent.scale * localScale;
			dirtyFlags |= DirtyFlags.WorldInverseDirty;
		}

		dirtyFlags |= DirtyFlags.WorldToLocalDirty;
	}

	private void SetDirty(DirtyFlags dirtyFlag)
	{
		if ((dirtyFlags & dirtyFlag) != 0) return;

		dirtyFlags |= dirtyFlag;

		// if (dirtyFlag.HasFlag(DirtyFlags.PositionDirty))
		//     Entity.OnTransformChanged(Component.Position);
		// else if (dirtyFlag.HasFlag(DirtyFlags.RotationDirty))
		//     Entity.OnTransformChanged(Component.Rotation);
		// else if (dirtyFlag.HasFlag(DirtyFlags.ScaleDirty))
		//     Entity.OnTransformChanged(Component.Scale);

		foreach (Transform t in children)
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
