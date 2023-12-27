using UnityEngine;
using System.Collections.Generic;

namespace FMODUnity
{
	public class StudioBankLoader : MonoBehaviour
	{
		public LoaderGameEvent LoadEvent;
		public LoaderGameEvent UnloadEvent;
		public List<string> Banks;
		public string CollisionTag;
		public bool PreloadSamples;
	}
}
