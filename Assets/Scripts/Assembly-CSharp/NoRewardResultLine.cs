using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200055F RID: 1375
public class NoRewardResultLine : MonoBehaviour
{
	// Token: 0x06001F9D RID: 8093 RVA: 0x00097A9C File Offset: 0x00095E9C
	public void setActive(int count, int cupPos, int cup100Pos, int bestPos)
	{
		this.cupPos = cupPos;
		this.cup100Pos = cup100Pos;
		this.bestPos = bestPos;
		this.leftPrefab.transform.GetChild(0).gameObject.SetActive(false);
		this.midPrefab.transform.GetChild(0).gameObject.SetActive(false);
		this.rightPrefab.transform.GetChild(0).gameObject.SetActive(false);
		for (int i = 1; i < count - 2; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.midPrefab.transform);
			transform.SetParent(base.transform);
			transform.SetSiblingIndex(2);
			transform.localScale = Vector2.one;
		}
		for (int j = 0; j < count; j++)
		{
			Transform child = base.transform.GetChild(j).GetChild(0);
			if (j < bestPos)
			{
				child.gameObject.SetActive(true);
				this.setAlpha(child, this.iconAlpha);
			}
		}
		this.midPrefab.SetActive(count > 2);
		base.StartCoroutine(this.latePos());
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x00097BC4 File Offset: 0x00095FC4
	public IEnumerator showIcon(float speed)
	{
		if (this.cup100Pos > this.bestPos && this.currIndex + 1 == this.cup100Pos)
		{
			base.StartCoroutine(this.showCup(this.cup100, speed));
		}
		if (this.cupPos > this.bestPos && this.currIndex + 1 == this.cupPos)
		{
			base.StartCoroutine(this.showCup(this.cup, speed));
		}
		Transform item = base.transform.GetChild(this.currIndex).GetChild(0);
		if (Global.self.currPuzzle != null)
		{
			ParticleSystem componentInChildren = Global.self.currPuzzle.GetComponentInChildren<ParticleSystem>();
			componentInChildren.transform.position = item.position;
			componentInChildren.Play(true);
		}
		Audio.self.playOneShot("a8b7ba64-62a4-4b81-93ad-df9c1db4433d", 1f);
		this.setAlpha(item, 1f);
		item.gameObject.SetActive(true);
		yield return base.StartCoroutine(this.animateIcon(item, speed));
		yield return new WaitForSeconds(this.delay);
		this.currIndex++;
		yield break;
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x00097BE8 File Offset: 0x00095FE8
	private IEnumerator showCup(Transform item, float speed)
	{
		yield return new WaitForSeconds(this.delay);
		this.setAlpha(item, Color.white, 1f);
		Audio.self.playOneShot("3b09bb25-ce54-42af-8cd2-5ae82eda0b7a", 1f);
		yield return base.StartCoroutine(this.animateIcon(item, speed));
		yield break;
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x00097C14 File Offset: 0x00096014
	private IEnumerator animateIcon(Transform item, float speed)
	{
		item.localScale = Vector2.zero;
		float time = 0f;
		float timeMax = this.showCurve.GetAnimationLength();
		while (time < timeMax)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime * speed);
			item.localScale = Vector2.one * this.showCurve.Evaluate(time / timeMax);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x00097C40 File Offset: 0x00096040
	public IEnumerator latePos()
	{
		yield return null;
		Vector2 pos = this.cup.position;
		pos.x = base.transform.GetChild(this.cupPos - 1).position.x;
		this.cup.position = pos;
		if (this.cupPos > this.bestPos)
		{
			this.setAlpha(this.cup, Color.black, 0.5f);
		}
		if (this.cup100Pos == 0)
		{
			this.cup100.gameObject.SetActive(false);
		}
		else
		{
			pos = this.cup100.position;
			pos.x = base.transform.GetChild(this.cup100Pos - 1).position.x;
			this.cup100.position = pos;
			if (this.cup100Pos == this.cupPos)
			{
				pos = this.cup100.anchoredPosition;
				pos.y += this.cup.sizeDelta.y * Mathf.Sign(this.cup100.anchoredPosition.y);
				this.cup100.anchoredPosition = pos;
			}
			if (this.cup100Pos > this.bestPos)
			{
				this.setAlpha(this.cup100, Color.black, 0.5f);
			}
		}
		yield break;
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x00097C5C File Offset: 0x0009605C
	private void setAlpha(Transform item, float alpha)
	{
		Image component = item.GetComponent<Image>();
		Color color = component.color;
		color.a = alpha;
		component.color = color;
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x00097C88 File Offset: 0x00096088
	private void setAlpha(Transform item, Color color, float alpha)
	{
		Image component = item.GetComponent<Image>();
		color.a = alpha;
		component.color = color;
	}

	// Token: 0x040022C3 RID: 8899
	public GameObject leftPrefab;

	// Token: 0x040022C4 RID: 8900
	public GameObject midPrefab;

	// Token: 0x040022C5 RID: 8901
	public GameObject rightPrefab;

	// Token: 0x040022C6 RID: 8902
	[Space(10f)]
	public AnimationCurve showCurve;

	// Token: 0x040022C7 RID: 8903
	public float delay = 0.15f;

	// Token: 0x040022C8 RID: 8904
	public float iconAlpha = 0.5f;

	// Token: 0x040022C9 RID: 8905
	[Space(10f)]
	public RectTransform cup;

	// Token: 0x040022CA RID: 8906
	public RectTransform cup100;

	// Token: 0x040022CB RID: 8907
	private int cupPos;

	// Token: 0x040022CC RID: 8908
	private int bestPos;

	// Token: 0x040022CD RID: 8909
	private int cup100Pos;

	// Token: 0x040022CE RID: 8910
	private int currIndex;
}
