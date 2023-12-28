using System;
using UnityEngine;

// Token: 0x02000519 RID: 1305
public class BlinkAnimation : MonoBehaviour
{
	// Token: 0x06001DF0 RID: 7664 RVA: 0x00086F58 File Offset: 0x00085358
	private void Update()
	{
		this.time -= Time.deltaTime;
		if (this.time <= 0f)
		{
			this.objectToBlink.SetActive(!this.objectToBlink.activeInHierarchy);
			this.time = this.timeBetweenBlinks;
		}
	}

	// Token: 0x0400213A RID: 8506
	public GameObject objectToBlink;

	// Token: 0x0400213B RID: 8507
	public float timeBetweenBlinks = 1f;

	// Token: 0x0400213C RID: 8508
	private float time;
}
