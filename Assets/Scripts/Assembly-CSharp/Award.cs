using System;
using FMODUnity;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class Award : MonoBehaviour
{
	// Token: 0x06001395 RID: 5013 RVA: 0x00030618 File Offset: 0x0002EA18
	protected virtual void Start()
	{
		if (base.GetComponent<Animator>())
		{
			base.GetComponent<Animator>().enabled = false;
		}
		foreach (Transform transform in this.mouseOverList)
		{
			transform.GetComponent<Renderer>().material.SetFloat("_Alpha2", 0f);
		}
		this.cupSilhouette.GetComponent<Renderer>().material.SetFloat("_Alpha2", 0f);
		if (this.ps.UIScreenCurr != null)
		{
			this.lp = this.ps.UIScreenCurr.GetComponent<levelPackMenu>();
		}
		this.setText();
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000306CC File Offset: 0x0002EACC
	protected virtual void Update()
	{
		if (this.ps.active && this.showAnimation && Global.self.currentAwardAnimation == AwardName.None)
		{
			base.GetComponent<Animator>().enabled = true;
			base.GetComponent<Animator>().SetTrigger(this.animationTrigger);
			this.showAnimation = false;
			this.canBeWhite = false;
			Global.self.currentAwardAnimation = this.awardName;
			Audio.self.playOneShot(this.animationSound, 1f);
		}
		if (this.whiteAmount > 0f && this.canBeWhite)
		{
			this.whiteAmount = Mathf.Lerp(this.whiteAmount, 0f, Time.deltaTime * 5f);
			if (this.whiteAmount < 0.01f)
			{
				this.whiteAmount = 0f;
			}
			if (this.cup.gameObject.activeInHierarchy)
			{
				foreach (Transform transform in this.mouseOverList)
				{
					if (transform.gameObject.activeInHierarchy)
					{
						transform.GetComponent<Renderer>().material.SetFloat("_Alpha2", this.whiteAmount);
					}
				}
			}
			else
			{
				this.cupSilhouette.GetComponent<Renderer>().material.SetFloat("_Alpha2", this.whiteAmount);
			}
		}
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x00030830 File Offset: 0x0002EC30
	protected virtual void setText()
	{
		if (this.awardName == AwardName.None)
		{
			return;
		}
		AwardData awardData = AwardData.Get(this.awardName, Global.self.currLanguage);
		if (awardData == null)
		{
			this.text = "ERROR";
			this.textHas = "ERROR";
			return;
		}
		this.text = TextFormating.formatNotAcquiredAward(awardData.titleNotAcquired, awardData.descriptionNotAcquired, awardData.good, awardData.reqPercent, awardData.reqCount, awardData.cup100);
		this.textHas = TextFormating.formatAcquiredAward(awardData.titleAcquired, awardData.descriptionAcquired, awardData.good, awardData.reqPercent, awardData.reqCount, awardData.cup100);
		this.text = TextFormating.format(this.text);
		this.textHas = TextFormating.format(this.textHas);
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x000308FC File Offset: 0x0002ECFC
	private static string getNeededCount()
	{
		int num2;
		int num3;
		int num = Global.self.CountPlayedPuzzlesInPack(out num2, out num3);
		return num + "/" + num;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x00030930 File Offset: 0x0002ED30
	public virtual void setCupState()
	{
		CupStatus cupStatus = Global.self.cupList[this.awardName];
		if (cupStatus != CupStatus.Empty)
		{
			if (cupStatus != CupStatus.ShowAnimation)
			{
				if (cupStatus == CupStatus.Exist)
				{
					this.cup.gameObject.SetActive(true);
					this.cupSilhouette.gameObject.SetActive(false);
					base.GetComponent<Collider2D>().enabled = true;
					this.onSecondRun();
				}
			}
			else
			{
				this.cup.gameObject.SetActive(false);
				this.cupSilhouette.gameObject.SetActive(true);
				base.GetComponent<Collider2D>().enabled = true;
				this.showAnimation = true;
				Global.self.cupList[this.awardName] = CupStatus.Exist;
				foreach (Transform transform in this.animations)
				{
					transform.gameObject.SetActive(false);
				}
				Global.self.currentAwardAnimCount++;
				AwardName toUnlockNextPack = this.ps.GetComponent<levelPackControl>().awardControllerScript.toUnlockNextPack;
				AwardName toUnlockNextPack2 = this.ps.GetComponent<levelPackControl>().awardControllerScript.toUnlockNextPack2;
				if ((this.awardName == toUnlockNextPack || (toUnlockNextPack2 != AwardName.None && this.awardName == toUnlockNextPack2)) && ((this.awardName == toUnlockNextPack && (toUnlockNextPack2 == AwardName.None || Global.self.cupList[toUnlockNextPack2] == CupStatus.Empty)) || (this.awardName == toUnlockNextPack2 && Global.self.cupList[toUnlockNextPack] == CupStatus.Empty)))
				{
					Global.self.unlockNextPack = true;
				}
				if (AudioVoice_Pack10.CanUnlockNextPack(null, toUnlockNextPack, toUnlockNextPack2))
				{
					Global.self.unlockNextPack = true;
				}
				if (base.GetComponent<Animator>() == null)
				{
					this.cup.gameObject.SetActive(true);
					this.turnOffAnimator();
					this.showAnimation = false;
				}
			}
		}
		else
		{
			this.cup.gameObject.SetActive(false);
			this.cupSilhouette.gameObject.SetActive(true);
			foreach (Transform transform2 in this.animations)
			{
				transform2.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x00030B88 File Offset: 0x0002EF88
	private void onSecondRun()
	{
		foreach (Transform transform in this.animations)
		{
			transform.gameObject.SetActive(true);
			MonoBehaviour component = transform.GetComponent<MonoBehaviour>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
		if (this.hideSprite)
		{
			this.cup.GetComponent<SpriteRenderer>().enabled = false;
		}
		foreach (Transform transform2 in this.showList)
		{
			transform2.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00030C2C File Offset: 0x0002F02C
	public void turnOffAnimator()
	{
		if (base.GetComponent<Animator>() != null)
		{
			base.GetComponent<Animator>().enabled = false;
		}
		this.cupSilhouette.gameObject.SetActive(false);
		this.canBeWhite = true;
		Global.self.currentAwardAnimation = AwardName.None;
		Global.self.currentAwardAnimCount--;
		this.onSecondRun();
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00030C91 File Offset: 0x0002F091
	private void OnMouseEnter()
	{
		this.updateText();
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x00030C99 File Offset: 0x0002F099
	private void OnMouseDown()
	{
		this.updateText();
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00030CA1 File Offset: 0x0002F0A1
	public void highlightAward()
	{
		if (this.canBeWhite)
		{
			this.whiteAmount = this.whiteAmountMax;
		}
		this.updateText();
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x00030CC0 File Offset: 0x0002F0C0
	protected void updateText()
	{
		if (!this.ps.active)
		{
			return;
		}
		if (this.lp == null)
		{
			return;
		}
		if (this.lp.awardText.text == string.Empty || this.lp.awardSelected != base.transform.GetInstanceID())
		{
			this.lp.setAwardTextAlpha();
			if (this.canBeWhite)
			{
				this.whiteAmount = this.whiteAmountMax;
			}
		}
		if (Global.self.cupList[this.awardName] != CupStatus.Empty)
		{
			this.lp.awardText.text = this.textHas;
		}
		else
		{
			this.lp.awardText.text = this.text;
		}
		this.lp.awardSelected = base.transform.GetInstanceID();
	}

	// Token: 0x04001061 RID: 4193
	[Tooltip("Name for this cup. FIll AwardNameList if it's not here")]
	public AwardName awardName;

	// Token: 0x04001062 RID: 4194
	[Tooltip("Cups sprite")]
	public Transform cup;

	// Token: 0x04001063 RID: 4195
	[Tooltip("Cups silhouette sprite")]
	public Transform cupSilhouette;

	// Token: 0x04001064 RID: 4196
	protected string text;

	// Token: 0x04001065 RID: 4197
	protected string textHas;

	// Token: 0x04001066 RID: 4198
	[Tooltip("What animation to play from the Animator")]
	public string animationTrigger = "Show";

	// Token: 0x04001067 RID: 4199
	[EventRef]
	public string animationSound = "f65150db-0734-46a9-80d0-eb72f8a555b9";

	// Token: 0x04001068 RID: 4200
	[Header("On the second run")]
	public bool hideSprite;

	// Token: 0x04001069 RID: 4201
	public Transform[] showList;

	// Token: 0x0400106A RID: 4202
	[Header("Mouse Over")]
	[Tooltip("List of items to whiten on mouse over")]
	public Transform[] mouseOverList = new Transform[1];

	// Token: 0x0400106B RID: 4203
	[Tooltip("How much to blink this cup on mouse over")]
	public float whiteAmountMax = 0.3f;

	// Token: 0x0400106C RID: 4204
	private float whiteAmount;

	// Token: 0x0400106D RID: 4205
	private bool showAnimation;

	// Token: 0x0400106E RID: 4206
	private bool canBeWhite = true;

	// Token: 0x0400106F RID: 4207
	[HideInInspector]
	public PuzzleStats ps;

	// Token: 0x04001070 RID: 4208
	protected levelPackMenu lp;

	// Token: 0x04001071 RID: 4209
	[Header("Idle animations")]
	[Tooltip("Transorms with idle animations on the cups")]
	public Transform[] animations;
}
