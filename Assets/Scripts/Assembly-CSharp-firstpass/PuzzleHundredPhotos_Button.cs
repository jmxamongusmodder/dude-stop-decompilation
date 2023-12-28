using System;
using System.Collections;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200041F RID: 1055
[EnabledManually]
public class PuzzleHundredPhotos_Button : MonoBehaviour
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0006A4AA File Offset: 0x000688AA
	// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x0006A4B2 File Offset: 0x000688B2
	public int photosMade { get; private set; }

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0006A4BC File Offset: 0x000688BC
	private void Start()
	{
		if (UIControl.self.useBackupFont)
		{
			this.comment.font = UIControl.self.backupFont;
			this.comment.fontSize = Mathf.RoundToInt((float)this.comment.fontSize * UIControl.self.SmallFontScale);
		}
		this.phone = this.GetComponentInPuzzleStats<PuzzleHundredPhotos_Phone>();
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x0006A520 File Offset: 0x00068920
	private void Update()
	{
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x0006A522 File Offset: 0x00068922
	private void OnMouseUp()
	{
		if (!base.enabled || this.activeCoroutine)
		{
			return;
		}
		this.AddPhoto();
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0006A544 File Offset: 0x00068944
	private void AddPhoto()
	{
		if (this.photosMade + this.initialCount >= this.achievementPhotos)
		{
			return;
		}
		base.StartCoroutine(this.PhotoCoroutine());
		Audio.self.playOneShot("19c0d94f-9996-49dd-ac23-75994fb20064", 1f);
		this.photosMade++;
		if (this.photosMade == 1)
		{
			this.phone.photoMade = true;
		}
		else if (this.photosMade == this.amountForReturn)
		{
			this.phone.ReturnToTable();
		}
		else if (this.photosMade + 1 != this.amountForMonster)
		{
			if (this.photosMade == this.amountForMonster)
			{
				this.phone.monster = true;
			}
		}
		if (this.photosMade + this.initialCount >= this.achievementPhotos && !this.achiev)
		{
			this.achiev = true;
			Global.self.GetCup(AwardName.CAT_PHOTO);
			this.crack.SetActive(true);
			Audio.self.playOneShot("14073776-6db7-47d0-b445-c0329dad010b", 1f);
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_HundredPhotos>().takePhoto(this.photosMade + this.initialCount);
		string text = LineTranslator.translateText("HUNDREAD_PHOTOS_" + (this.photosMade + this.initialCount - this.maximum).ToString(), WordTranslationContainer.Theme.PUZZLE, true, string.Empty);
		if (!string.IsNullOrEmpty(text))
		{
			this.comment.text = text;
			Audio.self.playOneShot("cbcae5cd-c1c6-4cd4-8c6f-f267a8d0c3c6", 1f);
		}
		this.counter.GetComponent<Text>().text = string.Format("{0}/{1}", this.initialCount + this.photosMade, this.max);
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0006A724 File Offset: 0x00068B24
	private IEnumerator PhotoCoroutine()
	{
		this.activeCoroutine = true;
		float timer = 0f;
		yield return null;
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

	// Token: 0x040018E2 RID: 6370
	public Transform screen;

	// Token: 0x040018E3 RID: 6371
	public GameObject crack;

	// Token: 0x040018E4 RID: 6372
	public Transform counter;

	// Token: 0x040018E5 RID: 6373
	public int initialCount = 54;

	// Token: 0x040018E6 RID: 6374
	public int maximum = 100;

	// Token: 0x040018E7 RID: 6375
	public int achievementPhotos = 120;

	// Token: 0x040018E8 RID: 6376
	public int amountForReturn = 4;

	// Token: 0x040018E9 RID: 6377
	public int amountForMonster = 5;

	// Token: 0x040018EA RID: 6378
	public Text comment;

	// Token: 0x040018EB RID: 6379
	public float screenTime;

	// Token: 0x040018EC RID: 6380
	public float screenTimeOut;

	// Token: 0x040018EE RID: 6382
	private int max = 100;

	// Token: 0x040018EF RID: 6383
	private bool activeCoroutine;

	// Token: 0x040018F0 RID: 6384
	private PuzzleHundredPhotos_Phone phone;

	// Token: 0x040018F1 RID: 6385
	private bool achiev;
}
