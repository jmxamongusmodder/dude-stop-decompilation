using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public class PuzzleFoodPhoto_Button : MonoBehaviour
{
	// Token: 0x06001A0E RID: 6670 RVA: 0x000644B9 File Offset: 0x000628B9
	private void Start()
	{
		this.phone = this.GetComponentInPuzzleStats<PuzzleFoodPhoto_Phone>();
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x000644C7 File Offset: 0x000628C7
	private void Update()
	{
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x000644CC File Offset: 0x000628CC
	private void OnMouseUp()
	{
		if (!base.enabled || this.activeCoroutine)
		{
			return;
		}
		this.GetPuzzleStats().goBadAfterTime = true;
		this.phone.Move(this.screenTime + this.screenTimeOut);
		base.StartCoroutine(this.PhotoCoroutine());
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x00064524 File Offset: 0x00062924
	private IEnumerator PhotoCoroutine()
	{
		this.activeCoroutine = true;
		float timer = 0f;
		Audio.self.playOneShot("7f64efa4-cd3a-4366-98cf-5cf3a54065e6", 1f);
		while (timer != this.screenTime)
		{
			timer = Mathf.MoveTowards(timer, this.screenTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.screenTime * 3.1415927f * 0.5f);
			this.screen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, t);
			yield return null;
		}
		timer = 0f;
		while (timer != this.screenTimeOut)
		{
			timer = Mathf.MoveTowards(timer, this.screenTimeOut, Time.deltaTime);
			float t2 = Mathf.Cos(timer / this.screenTime * 3.1415927f * 0.5f);
			this.screen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, t2);
			yield return null;
		}
		this.activeCoroutine = false;
		yield break;
	}

	// Token: 0x04001817 RID: 6167
	public Transform screen;

	// Token: 0x04001818 RID: 6168
	public float screenTime;

	// Token: 0x04001819 RID: 6169
	public float screenTimeOut;

	// Token: 0x0400181A RID: 6170
	private PuzzleFoodPhoto_Phone phone;

	// Token: 0x0400181B RID: 6171
	private bool activeCoroutine;
}
