using System;

[Serializable]
public class SerializableJigsawPiece
{
	public float x;
	public float y;
	public float z;
	public int order;
	public string name;
	public int group;
	public bool interchangeable;
	public int interchangeableSetStatus;
	public bool finished;
}
