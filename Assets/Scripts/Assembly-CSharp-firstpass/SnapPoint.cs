using System;
using UnityEngine;

[Serializable]
public class SnapPoint
{
	public SnapPoint(Draggable.Snap type, Vector2 point, float distance)
	{
	}

	public Draggable.Snap type;
	public float coord;
	public Vector2 coord2D;
	public float distance;
	public Transform transform;
	public bool enabled;
}
