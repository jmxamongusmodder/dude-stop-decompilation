using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class AudioVoice_CinemaPhone_DUCK : AudioVoice
{
	// Token: 0x060010A2 RID: 4258 RVA: 0x00019D74 File Offset: 0x00018174
	private void Awake()
	{
		this.canBeActive = (SerializableGameStats.self.pack09DuckShowed && !SerializableGameStats.self.pack09CinemaDuckPlayed);
		if (this.debugForceOn)
		{
			this.canBeActive = true;
		}
		if (this.canBeActive)
		{
			this.smileOrigin.SetActive(false);
			this.smileCopy.SetActive(true);
			SerializableGameStats.self.pack09CinemaDuckPlayed = true;
			Global.self.Save();
		}
		Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x00019DFC File Offset: 0x000181FC
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active || !this.canBeActive)
		{
			this.active = false;
			return;
		}
		global::Console.self.canOpen = false;
		this.voice = Audio.self.playVoice(this.onLoad);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x00019E74 File Offset: 0x00018274
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "showDuck":
			this.duck = UIControl.self.makePopupDuck(false);
			this.duck.setDuck(false);
			break;
		case "nextLine":
			base.StartCoroutine(this.duck.setTextSize(this.getLine(0)));
			base.StartCoroutine(this.duck.setOneLine(this.getLine(0), new DuckPopupSettings[]
			{
				DuckPopupSettings.ShowDoneLoading
			}));
			break;
		case "showDone":
			this.duck.stopLoadingDots();
			this.duck.setLine(this.getLine(0));
			break;
		case "showYes":
			base.StartCoroutine(this.duck.setTextSize(this.getLine(1)));
			base.StartCoroutine(this.duck.setOneLine(this.getLine(1), new DuckPopupSettings[]
			{
				DuckPopupSettings.Yes
			}));
			this.duck.subscribeToYes(new Action(this.onButtonYesClick));
			break;
		case "showPhone":
			base.StartCoroutine(this.showPhoneAnimation());
			this.screenSprite.transform.parent.GetComponent<PuzzleCinemaPhone_Screen>().StartGlowing();
			Audio.self.playLoopSound("b9edb8a8-88b6-466e-a486-a2ec0b526360");
			base.StartCoroutine(this.showScreenGlow());
			break;
		case "showVideo":
			this.phoneRecScreen.SetActive(true);
			Audio.self.playOneShot("3295a9b2-026e-4df1-9613-4589e76a097f", 1f);
			break;
		case "startRec":
			this.phoneRecIcon.SetActive(true);
			Audio.self.playOneShot("c3104b2c-e181-4846-bded-a6b422d6af52", 1f);
			break;
		case "endLevel":
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			this.duck.stopLoadingDots();
			base.StartCoroutine(this.duck.setTextSize(this.getLine(3)));
			base.StartCoroutine(this.duck.setOneLine(this.getLine(3), new DuckPopupSettings[0]));
			Global.self.canExitEndScreen = false;
			Global.LevelCompleted(0f, false);
			break;
		case "showRating":
			base.StartCoroutine(this.duck.setTextSize(this.getLine(4)));
			base.StartCoroutine(this.duck.setOneLine(this.getLine(4), new DuckPopupSettings[]
			{
				DuckPopupSettings.StarRating
			}));
			this.duck.subscribeToRating(new Action<int>(this.onButtonRatingSelect));
			break;
		}
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0001A188 File Offset: 0x00018588
	private IEnumerator showPhoneAnimation()
	{
		this.phone.SetActive(true);
		Vector2 dest = new Vector2(this.phone.transform.position.x, this.phoneMaxY);
		while (this.phone.transform.position.y < this.phoneMaxY)
		{
			this.phone.transform.position = Vector2.MoveTowards(this.phone.transform.position, dest, Time.deltaTime * this.phoneSpeed);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0001A1A4 File Offset: 0x000185A4
	private IEnumerator showScreenGlow()
	{
		while (base.enabled)
		{
			float c = this.screenSprite.color.a + this.phoneGlowShift;
			foreach (SpriteRenderer spriteRenderer in this.phoneGlow)
			{
				spriteRenderer.color = new Color(c, c, c, 1f);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0001A1C0 File Offset: 0x000185C0
	private void onButtonRatingSelect(int index)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		if (index != 0)
		{
			if (index != 1)
			{
				if (index == 2)
				{
					this.voice = Audio.self.playVoice(this.threeStars);
				}
			}
			else
			{
				this.voice = Audio.self.playVoice(this.twoStars);
			}
		}
		else
		{
			this.voice = Audio.self.playVoice(this.oneStar);
		}
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			Global.self.canExitEndScreen = true;
		});
		this.voice.start(true);
		base.StartCoroutine(this.duck.setTextSize(this.getLine(5)));
		base.StartCoroutine(this.duck.setOneLine(this.getLine(5), new DuckPopupSettings[0]));
		UIControl.self.StartCoroutine(UIControl.self.killDuckOnTransition(this.duck));
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0001A2E8 File Offset: 0x000186E8
	private void onButtonYesClick()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.onYesClick);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		base.StartCoroutine(this.duck.setTextSize(this.getLine(2)));
		base.StartCoroutine(this.duck.setOneLine(this.getLine(2), new DuckPopupSettings[]
		{
			DuckPopupSettings.ShowDoneLoading
		}));
		Cursor.SetCursor(this.cursor, Vector2.zero, CursorMode.Auto);
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0001A3A4 File Offset: 0x000187A4
	private string getLine(int index)
	{
		return WordTranslationContainer.Get(WordTranslationContainer.Theme.CONSOLE, "DUCK_POPUP_CINEMAPHONE_" + index.ToString(), Global.self.currLanguage);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0001A3CD File Offset: 0x000187CD
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subsctibeToEnding(item);
		item.SetEnding(LevelVoice.getEndText(this.endText, Global.self.currLanguage), false);
	}

	// Token: 0x04000DA2 RID: 3490
	private bool canBeActive;

	// Token: 0x04000DA3 RID: 3491
	public bool debugForceOn;

	// Token: 0x04000DA4 RID: 3492
	[Space(10f)]
	public StandaloneLevelVoice onLoad;

	// Token: 0x04000DA5 RID: 3493
	public StandaloneLevelVoice onYesClick;

	// Token: 0x04000DA6 RID: 3494
	public StandaloneLevelVoice oneStar;

	// Token: 0x04000DA7 RID: 3495
	public StandaloneLevelVoice twoStars;

	// Token: 0x04000DA8 RID: 3496
	public StandaloneLevelVoice threeStars;

	// Token: 0x04000DA9 RID: 3497
	public StandaloneLevelVoice endText;

	// Token: 0x04000DAA RID: 3498
	[Header("Puzzle obj")]
	public GameObject smileOrigin;

	// Token: 0x04000DAB RID: 3499
	public GameObject smileCopy;

	// Token: 0x04000DAC RID: 3500
	public GameObject phone;

	// Token: 0x04000DAD RID: 3501
	public float phoneMaxY;

	// Token: 0x04000DAE RID: 3502
	public float phoneSpeed = 1f;

	// Token: 0x04000DAF RID: 3503
	public GameObject phoneRecScreen;

	// Token: 0x04000DB0 RID: 3504
	public GameObject phoneRecIcon;

	// Token: 0x04000DB1 RID: 3505
	public SpriteRenderer screenSprite;

	// Token: 0x04000DB2 RID: 3506
	public SpriteRenderer[] phoneGlow;

	// Token: 0x04000DB3 RID: 3507
	public float phoneGlowShift = 0.3f;

	// Token: 0x04000DB4 RID: 3508
	public Texture2D cursor;

	// Token: 0x04000DB5 RID: 3509
	private DuckPopup duck;
}
