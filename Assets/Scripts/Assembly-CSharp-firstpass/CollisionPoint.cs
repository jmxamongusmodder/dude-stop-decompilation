using System;
using UnityEngine;

public struct CollisionPoint
{
	public CollisionPoint(Vector2 pos, Vector2 norm, float t, Vector2 posOnObj, float ang, Vector2 lastPos) : this()
	{
	}

	public Vector2 point;
	public Vector2 normal;
	public Vector2 pointOnObj;
	public float angle;
	public float time;
	public float distance;
	public float distanceOnObj;
	public float distanceAngle;
	public int pointCount;
}
