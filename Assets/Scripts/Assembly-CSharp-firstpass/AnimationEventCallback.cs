using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000516 RID: 1302
public class AnimationEventCallback : MonoBehaviour
{
	// Token: 0x06001DED RID: 7661 RVA: 0x00086F2F File Offset: 0x0008532F
	public void OnEvent(global::AnimationEvent type)
	{
		this.onEvent.Invoke(type);
	}

	// Token: 0x04002135 RID: 8501
	public AnimationEventCallback.EvnumEvent onEvent;

	// Token: 0x02000517 RID: 1303
	[Serializable]
	public class EvnumEvent : UnityEvent<global::AnimationEvent>
	{
	}
}
