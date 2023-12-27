using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class Banner : MonoBehaviour
{
	// Token: 0x0600146B RID: 5227 RVA: 0x0003589C File Offset: 0x00033C9C
	public void SetActive(bool monster, Color color)
	{
		if (monster)
		{
			this.bannerSprite.sprite = this.bannerBad;
			this.rollSprite.sprite = this.rollBad;
			this.lineSprite.sprite = this.lineBad;
			this.particlesGood.SetActive(false);
			this.particlesBad.SetActive(true);
			Vector3 position = base.transform.position;
			position.x += this.shiftBadPosX;
			this.rollStartPosition.x = position.x;
			base.transform.position = position;
		}
		else
		{
			this.bannerSprite.sprite = this.bannerGood;
			this.rollSprite.sprite = this.rollGood;
			this.lineSprite.sprite = this.lineGood;
			this.particlesGood.SetActive(true);
			this.particlesBad.SetActive(false);
		}
		this.shadowSprite.sprite = this.bannerSprite.sprite;
		this.shadowSprite.gameObject.SetActive(false);
		if (color.a > 0f)
		{
			this.bannerSprite.color = color;
			this.rollSprite.color = color;
		}
		if (monster)
		{
			Audio.self.playOneShot("77f039bb-ae31-447c-be77-373c0f74acea", 1f);
		}
		else
		{
			Audio.self.playOneShot("24b96d35-ef65-448c-8e9e-b22d56f57139", 1f);
		}
		base.StartCoroutine(this.Show());
		base.StartCoroutine(this.Bounce());
		base.StartCoroutine(this.Scale());
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x00035A3C File Offset: 0x00033E3C
	private IEnumerator Show()
	{
		this.bannerRoll.position = this.rollStartPosition;
		float dist = this.rollStartPosition.y - this.rollEndPosition.y;
		float time = 0f;
		float timeMax = this.showCurve.GetAnimationLength();
		while (time < timeMax)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime * this.curveSpeed);
			this.bannerRoll.position = this.rollStartPosition + Vector2.down * dist * this.showCurve.Evaluate(time);
			if (this.bannerRoll.position.y < this.showShadowMarkY)
			{
				this.shadowSprite.gameObject.SetActive(true);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x00035A58 File Offset: 0x00033E58
	private IEnumerator Bounce()
	{
		float posY = base.transform.position.y;
		float time = 0f;
		float timeMax = this.bannerCurve.GetAnimationLength();
		while (time < timeMax)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime * this.curveSpeed);
			Vector2 pos = base.transform.position;
			pos.y = posY + this.bannerCurve.Evaluate(time) * this.bannerDist;
			base.transform.position = pos;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x00035A74 File Offset: 0x00033E74
	private IEnumerator Scale()
	{
		float time = 0f;
		float timeMax = this.rollScaleCurve.GetAnimationLength();
		while (time < timeMax)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime * this.curveSpeed);
			this.rollContainer.localScale = new Vector3(1f, this.rollScaleCurve.Evaluate(time), 1f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x040011AE RID: 4526
	[Header("Objects")]
	public SpriteRenderer bannerSprite;

	// Token: 0x040011AF RID: 4527
	public SpriteRenderer rollSprite;

	// Token: 0x040011B0 RID: 4528
	public SpriteRenderer lineSprite;

	// Token: 0x040011B1 RID: 4529
	public SpriteRenderer shadowSprite;

	// Token: 0x040011B2 RID: 4530
	public Transform bannerRoll;

	// Token: 0x040011B3 RID: 4531
	public Transform rollContainer;

	// Token: 0x040011B4 RID: 4532
	[Header("Sprites")]
	public Sprite bannerGood;

	// Token: 0x040011B5 RID: 4533
	public Sprite rollGood;

	// Token: 0x040011B6 RID: 4534
	public Sprite lineGood;

	// Token: 0x040011B7 RID: 4535
	public Sprite bannerBad;

	// Token: 0x040011B8 RID: 4536
	public Sprite rollBad;

	// Token: 0x040011B9 RID: 4537
	public Sprite lineBad;

	// Token: 0x040011BA RID: 4538
	public GameObject particlesGood;

	// Token: 0x040011BB RID: 4539
	public GameObject particlesBad;

	// Token: 0x040011BC RID: 4540
	[Header("Settings")]
	public Vector2 rollStartPosition;

	// Token: 0x040011BD RID: 4541
	public Vector2 rollEndPosition;

	// Token: 0x040011BE RID: 4542
	[Space(10f)]
	public float showShadowMarkY;

	// Token: 0x040011BF RID: 4543
	[Space(10f)]
	public float shiftBadPosX = -0.15f;

	// Token: 0x040011C0 RID: 4544
	[Space(10f)]
	public float curveSpeed = 1f;

	// Token: 0x040011C1 RID: 4545
	public AnimationCurve showCurve;

	// Token: 0x040011C2 RID: 4546
	public AnimationCurve bannerCurve;

	// Token: 0x040011C3 RID: 4547
	public float bannerDist = 0.5f;

	// Token: 0x040011C4 RID: 4548
	public AnimationCurve rollScaleCurve;

	// Token: 0x040011C5 RID: 4549
	[Space(10f)]
	[Tooltip("Add this to the cup, to move it above banner")]
	public int cupLayer = 200;
}
