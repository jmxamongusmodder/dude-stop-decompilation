using UnityEngine;
using System;

public class GlobalCollider : MonoBehaviour
{
	[Serializable]
	public class Collider
	{
		public GlobalCollider.ColliderType type;
		public float position;
		public float width;
		public float height;
		public BoxCollider2D collider;
	}

	public enum ColliderType
	{
		None = 0,
		Screen = 1,
		Point = 2,
		Both = 3,
	}

	public Collider top;
	public Collider right;
	public Collider bottom;
	public Collider left;
}
