using System;
using UnityEngine;

// Token: 0x0200054F RID: 1359
public class HideUIoutsideScreen : MonoBehaviour
{
	// Token: 0x06001F30 RID: 7984 RVA: 0x00094736 File Offset: 0x00092B36
	private void Awake()
	{
		this.listStatus = new HideUIoutsideScreen.ItemStatus[this.list.Length];
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x0009474C File Offset: 0x00092B4C
	private void FixedUpdate()
	{
		for (int i = 0; i < this.list.Length; i++)
		{
			if (this.listStatus[i] != HideUIoutsideScreen.ItemStatus.above)
			{
				float y = Camera.main.WorldToViewportPoint(this.list[i].TransformPoint(Vector3.zero)).y;
				if (y > 0f - this.howFarToShow && this.listStatus[i] == HideUIoutsideScreen.ItemStatus.below)
				{
					this.list[i].gameObject.SetActive(true);
					this.listStatus[i] = HideUIoutsideScreen.ItemStatus.middle;
				}
				else if (y > 1f + this.howFarToHide && this.listStatus[i] == HideUIoutsideScreen.ItemStatus.middle)
				{
					this.list[i].gameObject.SetActive(false);
					this.listStatus[i] = HideUIoutsideScreen.ItemStatus.above;
				}
			}
		}
	}

	// Token: 0x04002266 RID: 8806
	public float howFarToShow;

	// Token: 0x04002267 RID: 8807
	public float howFarToHide;

	// Token: 0x04002268 RID: 8808
	public RectTransform[] list;

	// Token: 0x04002269 RID: 8809
	private HideUIoutsideScreen.ItemStatus[] listStatus;

	// Token: 0x02000550 RID: 1360
	private enum ItemStatus
	{
		// Token: 0x0400226B RID: 8811
		below,
		// Token: 0x0400226C RID: 8812
		middle,
		// Token: 0x0400226D RID: 8813
		above
	}
}
