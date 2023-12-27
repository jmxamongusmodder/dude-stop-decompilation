using UnityEngine;

public class Ghost : MonoBehaviour
{
	public ParticleSystem particles;
	public float maxAliveTime;
	public float speed;
	public float hideSpeed;
	public float rotationSpeed;
	public float maxAllowedDist;
	public float firstTargetDist;
	public float secondTargetDist;
	public Sprite[] list;
}
