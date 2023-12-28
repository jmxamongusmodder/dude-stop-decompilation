using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000581 RID: 1409
public class Scrollable_CursorAnimation : MonoBehaviour
{
	// Token: 0x06002068 RID: 8296 RVA: 0x0009F2DD File Offset: 0x0009D6DD
	public void OnEvent(MouseAnimationEvent type)
	{
		this.onEvent.Invoke(type);
	}

	// Token: 0x040023B1 RID: 9137
	public Scrollable_CursorAnimation.EvnumEvent onEvent;

	// Token: 0x02000582 RID: 1410
	[Serializable]
	public class EvnumEvent : UnityEvent<MouseAnimationEvent>
	{
	}
}
