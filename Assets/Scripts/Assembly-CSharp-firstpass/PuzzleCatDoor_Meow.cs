using System;
using UnityEngine;

// Token: 0x020003DC RID: 988
public class PuzzleCatDoor_Meow : MonoBehaviour
{
	// Token: 0x060018E0 RID: 6368 RVA: 0x00059E04 File Offset: 0x00058204
	public void Set(float time)
	{
		this.timer = time;
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x00059E0D File Offset: 0x0005820D
	private void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer < 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040016E0 RID: 5856
	private float timer;
}
