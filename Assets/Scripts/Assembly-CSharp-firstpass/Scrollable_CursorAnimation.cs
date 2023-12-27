using UnityEngine;
using System;
using UnityEngine.Events;

public class Scrollable_CursorAnimation : MonoBehaviour
{
	[Serializable]
	public class EvnumEvent : UnityEvent<MouseAnimationEvent>
	{
	}

	public EvnumEvent onEvent;
}
