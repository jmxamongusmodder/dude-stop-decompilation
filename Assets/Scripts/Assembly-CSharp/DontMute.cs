using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200053F RID: 1343
public class DontMute : AbstractUIScreen
{
	// Token: 0x06001EAE RID: 7854 RVA: 0x0008E95F File Offset: 0x0008CD5F
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x0008E961 File Offset: 0x0008CD61
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.HideAnimation());
	}

	// Token: 0x06001EB0 RID: 7856 RVA: 0x0008E980 File Offset: 0x0008CD80
	private IEnumerator HideAnimation()
	{
		float time = 0f;
		while (time < this.showTime)
		{
			time = Mathf.MoveTowards(time, this.showTime, Time.deltaTime);
			if (Input.anyKeyDown && Global.self.NoCurrentTransition)
			{
				break;
			}
			yield return null;
		}
		Global.self.makeNewLevel(this.nextPuzzle, Vector2.down, true);
		yield break;
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x0008E99B File Offset: 0x0008CD9B
	protected override void cancelPressed()
	{
	}

	// Token: 0x040021D8 RID: 8664
	public float showTime = 1f;

	// Token: 0x040021D9 RID: 8665
	public Transform nextPuzzle;
}
