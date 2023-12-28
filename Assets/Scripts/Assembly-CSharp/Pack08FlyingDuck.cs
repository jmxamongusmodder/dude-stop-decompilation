using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000565 RID: 1381
public class Pack08FlyingDuck : MonoBehaviour
{
	// Token: 0x06001FD0 RID: 8144 RVA: 0x0009943C File Offset: 0x0009783C
	private void Start()
	{
		base.StartCoroutine(this.AppearAnimation(this.appearCurve, false));
		base.StartCoroutine(this.NightAnimation());
		base.StartCoroutine(this.MoveAnimation());
		this.GetPuzzleStats().UIScreenCurr.GetComponent<levelPackMenu>().awardText.text = string.Empty;
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x00099498 File Offset: 0x00097898
	private IEnumerator NightAnimation()
	{
		float time = 0f;
		float timeMax = this.showNight.GetAnimationLength() * this.showTime;
		this.nightSprite.transform.GetChild(0).gameObject.SetActive(false);
		this.nightSprite.gameObject.SetActive(true);
		while (time < timeMax)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime);
			this.nightSprite.color = new Color(0f, 0f, 0f, this.showNight.Evaluate(time / timeMax));
			yield return null;
		}
		this.nightSprite.transform.GetChild(0).gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x000994B4 File Offset: 0x000978B4
	private IEnumerator AppearAnimation(AnimationCurve curve, bool killAfter = false)
	{
		float time = 0f;
		float timeMax = curve.GetAnimationLength();
		while (time < timeMax)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime);
			float scale = curve.Evaluate(time);
			base.transform.localScale = Vector2.one * scale;
			yield return null;
		}
		if (killAfter)
		{
			this.particles.transform.SetParent(base.transform.parent);
			this.particles.transform.position = base.transform.position;
			this.particles.transform.localScale = Vector3.one;
			this.particles.SetActive(true);
			Audio.self.playOneShot("aa626233-4379-4159-b838-9f7c4ea4d3b7", 1f);
			base.gameObject.SetActive(false);
			this.nightSprite.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x000994E0 File Offset: 0x000978E0
	private IEnumerator MoveAnimation()
	{
		Vector3 origin = base.transform.position;
		float time = 0f;
		float x = Camera.main.ViewportToWorldPoint(Vector2.right).x - 0.5f;
		Vector3 destination = new Vector3(x, this.destinationY, base.transform.position.z);
		while (time < this.timeToFly)
		{
			time = Mathf.MoveTowards(time, this.timeToFly, Time.deltaTime);
			base.transform.position = origin + (destination - origin) * (time / this.timeToFly);
			base.transform.Rotate(0f, 0f, this.rotationSpeed);
			yield return null;
		}
		if (!this.hidding)
		{
			base.StartCoroutine(this.AppearAnimation(this.hideCurve, true));
			this.hidding = true;
			this.GetPuzzleStats().GetComponent<AudioVoice_Pack08>().killDuck();
		}
		yield break;
	}

	// Token: 0x040022F4 RID: 8948
	public float destinationY;

	// Token: 0x040022F5 RID: 8949
	public float timeToFly = 12f;

	// Token: 0x040022F6 RID: 8950
	public float rotationSpeed;

	// Token: 0x040022F7 RID: 8951
	public AnimationCurve appearCurve;

	// Token: 0x040022F8 RID: 8952
	public AnimationCurve hideCurve;

	// Token: 0x040022F9 RID: 8953
	public GameObject particles;

	// Token: 0x040022FA RID: 8954
	[Space(10f)]
	public AnimationCurve showNight;

	// Token: 0x040022FB RID: 8955
	public float showTime = 5f;

	// Token: 0x040022FC RID: 8956
	public SpriteRenderer nightSprite;

	// Token: 0x040022FD RID: 8957
	[HideInInspector]
	public bool allowClick;

	// Token: 0x040022FE RID: 8958
	private bool hidding;
}
