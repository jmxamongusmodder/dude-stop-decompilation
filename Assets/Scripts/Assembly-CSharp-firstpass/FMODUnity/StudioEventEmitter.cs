using UnityEngine;

namespace FMODUnity
{
	public class StudioEventEmitter : MonoBehaviour
	{
		public string Event;
		public EmitterGameEvent PlayEvent;
		public EmitterGameEvent StopEvent;
		public string CollisionTag;
		public bool AllowFadeout;
		public bool TriggerOnce;
		public bool Preload;
		public ParamRef[] Params;
		public bool OverrideAttenuation;
		public float OverrideMinDistance;
		public float OverrideMaxDistance;
	}
}
