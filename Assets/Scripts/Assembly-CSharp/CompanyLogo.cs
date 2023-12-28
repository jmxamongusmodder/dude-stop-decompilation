using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public class CompanyLogo : AbstractUIScreen
{
	// Token: 0x06001E0B RID: 7691 RVA: 0x000876B9 File Offset: 0x00085AB9
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x000876BB File Offset: 0x00085ABB
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.HideAnimation());
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x000876D8 File Offset: 0x00085AD8
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

	// Token: 0x06001E0E RID: 7694 RVA: 0x000876F3 File Offset: 0x00085AF3
	protected override void cancelPressed()
	{
	}

	// Token: 0x0400214D RID: 8525
	public float showTime = 1f;

	// Token: 0x0400214E RID: 8526
	public Transform nextPuzzle;
}
