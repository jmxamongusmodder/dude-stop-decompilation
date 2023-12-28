using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000513 RID: 1299
public class AchievementPopupItem : MonoBehaviour
{
	// Token: 0x06001DDA RID: 7642 RVA: 0x0008670C File Offset: 0x00084B0C
	public void SetAchievement(AwardName name)
	{
		AwardData awardData = AwardData.Get(name, Global.self.currLanguage);
		if (UIControl.self.useBackupFont)
		{
			this.titleText.font = UIControl.self.backupFont;
			this.titleText.fontSize = Mathf.RoundToInt((float)this.titleText.fontSize * UIControl.self.BigFontScale);
			this.descriptionText.font = UIControl.self.backupFont;
			this.descriptionText.fontSize = Mathf.RoundToInt((float)this.descriptionText.fontSize * UIControl.self.SmallFontScale);
		}
		if (string.IsNullOrEmpty(awardData.titleAcquired))
		{
			this.titleText.text = awardData.titleNotAcquired;
		}
		else
		{
			this.titleText.text = awardData.titleAcquired;
		}
		if (string.IsNullOrEmpty(awardData.shortDescriptionAcquired))
		{
			this.descriptionText.text = awardData.descriptionNotAcquired;
		}
		else
		{
			this.descriptionText.text = awardData.shortDescriptionAcquired;
		}
		Audio.self.playOneShot("b4183154-b891-4e69-bcbe-a059f84f420a", 1f);
		this.rt = base.GetComponent<RectTransform>();
		this.height = this.rt.sizeDelta.y;
		base.StartCoroutine(this.Animate());
		base.StartCoroutine(this.ShiftByChild());
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x00086874 File Offset: 0x00084C74
	private IEnumerator Animate()
	{
		float time = 0f;
		float timeMax = this.animationCurve.GetAnimationLength();
		while (time < timeMax)
		{
			time += Time.deltaTime;
			float prog = this.animationCurve.Evaluate(time / timeMax);
			this.shiftY = this.height * (1f - prog) + this.rt.anchoredPosition.x;
			yield return null;
		}
		yield return new WaitForSeconds(this.lifeTime);
		time = this.hideTime;
		CanvasGroup cg = base.GetComponent<CanvasGroup>();
		while (time > 0f)
		{
			time -= Time.deltaTime;
			cg.alpha = time / this.hideTime;
			yield return null;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x00086890 File Offset: 0x00084C90
	private IEnumerator ShiftByChild()
	{
		for (;;)
		{
			int ind = this.rt.GetSiblingIndex();
			if (base.transform.parent.childCount < 2)
			{
				yield return null;
			}
			else
			{
				Vector2 pos = this.rt.anchoredPosition;
				if (ind < base.transform.parent.childCount - 1)
				{
					RectTransform component = base.transform.parent.GetChild(ind + 1).GetComponent<RectTransform>();
					pos.y = component.anchoredPosition.y + this.height - this.shiftY;
				}
				else
				{
					pos.y = -this.shiftY;
				}
				this.rt.anchoredPosition = pos;
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x04002128 RID: 8488
	[Header("Text")]
	public Text titleText;

	// Token: 0x04002129 RID: 8489
	public Text descriptionText;

	// Token: 0x0400212A RID: 8490
	[Header("Show animation")]
	public AnimationCurve animationCurve;

	// Token: 0x0400212B RID: 8491
	private RectTransform rt;

	// Token: 0x0400212C RID: 8492
	[Space(10f)]
	public float lifeTime = 4f;

	// Token: 0x0400212D RID: 8493
	public float hideTime = 1f;

	// Token: 0x0400212E RID: 8494
	private float shiftY;

	// Token: 0x0400212F RID: 8495
	private float height;
}
