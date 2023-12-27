using UnityEngine;
using System;
using System.Collections.Generic;

public class Draggable : MonoBehaviour
{
	[Serializable]
	public class Limit
	{
		public bool limit;
		public bool disableDragOnBorder;
		public Vector3 newMousePosition;
		public bool top;
		public bool bottom;
		public bool left;
		public bool right;
		public float topVal;
		public float rightVal;
		public float bottomVal;
		public float leftVal;
		public bool topScreen;
		public bool bottomScreen;
		public bool leftScreen;
		public bool rightScreen;
		public bool unfolded;
	}

	public enum Snap
	{
		None = 0,
		X = 1,
		Y = 2,
		XY = 3,
	}

	public string pickUpSound;
	public Limit limit;
	public bool dragEnabled;
	public bool useRigidbody;
	public float throwQuotient;
	public bool allowThrow;
	public float moveLimit;
	public bool rotateDelta;
	public bool lockX;
	public bool lockY;
	public List<SnapPoint> snapPoints;
}
