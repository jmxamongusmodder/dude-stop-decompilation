using UnityEngine;
using System;
using UnityEngine.Events;

public class AnimationEventCallback : MonoBehaviour
{
	[Serializable]
	public class EvnumEvent : UnityEvent<AnimationEvent>
	{
	}

	public EvnumEvent onEvent;
}
