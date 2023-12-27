using UnityEngine;
using System.Collections.Generic;

public class PuzzleDoNotStack_Controller : MonoBehaviour
{
	public Transform boxPrefab;
	public List<PuzzleDoNotStack_Box> boxes;
	public float magnitudeThreshold;
	public float waitTime;
	public float xShift;
	public float raycastLength;
}
