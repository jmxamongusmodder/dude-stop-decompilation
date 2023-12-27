using UnityEngine;

namespace FMODUnity
{
	public class RuntimeManager : MonoBehaviour
	{
		[SerializeField]
		private FMODPlatform fmodPlatform;
		[SerializeField]
		private long[] cachedPointers;
	}
}
