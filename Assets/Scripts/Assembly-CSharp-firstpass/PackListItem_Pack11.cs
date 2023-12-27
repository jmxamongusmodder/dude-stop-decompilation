using UnityEngine;
using System;

public class PackListItem_Pack11 : MonoBehaviour
{
	[Serializable]
	public class PuzzleList
	{
		public int requiresBadCount;
		public Transform puzzle;
	}

	public PuzzleList[] list;
}
