using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class CupLifeGift_Box : MonoBehaviour
{
	// Token: 0x060000AF RID: 175 RVA: 0x000084A1 File Offset: 0x000066A1
	private void Update()
	{
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.H))
		{
			base.StartCoroutine(this.OpeningCoroutine());
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000084CB File Offset: 0x000066CB
	private void OnMouseDown()
	{
		if (!this.canBeClicked)
		{
			return;
		}
		base.StartCoroutine(this.OpeningCoroutine());
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000084E8 File Offset: 0x000066E8
	public void Enable()
	{
		this.canBeClicked = true;
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(true);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		Audio.self.playOneShot("12c7346d-2eae-47b7-a4d6-0efaf67f6915", 1f);
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00008570 File Offset: 0x00006770
	private IEnumerator OpeningCoroutine()
	{
		this.canBeClicked = false;
		Audio.self.playOneShot("12885c03-8e16-4140-bbcf-06f221c3d697", 1f);
		float topHeight = this.top.GetComponent<SpriteRenderer>().bounds.extents.y;
		float fallTime = this.fallCurve.GetAnimationLength();
		float timer = 0f;
		while (timer != fallTime)
		{
			timer = Mathf.MoveTowards(timer, fallTime, Time.deltaTime);
			float angle = Mathf.Lerp(0f, 90f, this.fallCurve.Evaluate(timer));
			this.left.rotation = Quaternion.Euler(0f, 0f, angle);
			this.right.rotation = Quaternion.Euler(0f, 0f, -angle);
			base.transform.localScale = new Vector2(1f, 1f - this.fallCurve.Evaluate(timer));
			float spriteTop = base.GetComponent<SpriteRenderer>().bounds.max.y;
			this.top.position = new Vector3(this.top.position.x, Mathf.Clamp(spriteTop - topHeight, base.transform.position.y + topHeight, 10f));
			yield return null;
		}
		this.GetComponentInPuzzleStats<CupLifeGift_Hammer>().Enable();
		yield break;
	}

	// Token: 0x0400012F RID: 303
	public AnimationCurve fallCurve;

	// Token: 0x04000130 RID: 304
	public Transform left;

	// Token: 0x04000131 RID: 305
	public Transform right;

	// Token: 0x04000132 RID: 306
	public Transform top;

	// Token: 0x04000133 RID: 307
	private bool canBeClicked;
}
