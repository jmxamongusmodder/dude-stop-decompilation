using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class AudioVoice_Exam : AudioVoice
{
	// Token: 0x0600115F RID: 4447 RVA: 0x0001FFB3 File Offset: 0x0001E3B3
	private void Awake()
	{
		base.StartCoroutine(this.LateAwake());
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x0001FFC4 File Offset: 0x0001E3C4
	private IEnumerator LateAwake()
	{
		yield return null;
		this.skipIntroScene = false;
		if (Global.self.DEBUG && Input.GetKey(KeyCode.LeftShift))
		{
			this.forceIntroScene = true;
		}
		this.stats = SerializablePuzzleStats.Get(base.transform.name);
		Transform book = base.GetComponent<PuzzleStats>().UIScreenCurr;
		if ((this.stats.loadedTimes == 0 && !this.skipIntroScene) || this.forceIntroScene)
		{
			book.gameObject.SetActive(false);
			this.storyBoard.gameObject.SetActive(true);
		}
		else
		{
			book.gameObject.SetActive(true);
			this.storyBoard.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0001FFE0 File Offset: 0x0001E3E0
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		bool flag = false;
		if ((this.stats.loadedTimes == 0 && !this.skipIntroScene) || this.forceIntroScene)
		{
			this.voice = Audio.self.playVoice(this.examStory);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			Audio.self.playOneShot("891f0b76-4fd7-4fb0-b700-547c4f9768fe", 1f);
			Global.self.canBePaused = false;
			flag = true;
		}
		else
		{
			if (Global.self.CountPackPlayedTimes(false, 0) == 0 && Global.self.CountPackPlayedTimes(true, 0) > 0)
			{
				this.voice = this.playOnce(this.examLastBad);
			}
			if (Global.self.CountPackPlayedTimes(false, 0) > 0)
			{
				this.voice = this.playOnce(this.examLastGood);
			}
			if (Global.self.CountPackPlayedTimes(0) == 0)
			{
				this.voice = this.playOnce(this.examLastAbort);
			}
			else if (Global.self.CountPackPlayedTimes(false, 0) == 0 && Global.self.CountPackPlayedTimes(true, 0) == 0)
			{
				this.voice = this.playOnce(this.examLastMix);
			}
		}
		if (this.voice == null)
		{
			this.voice = Audio.self.playVoice(this.examOnOtherLoad);
		}
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.allowFocusLost));
		this.voice.start(true);
		if (!flag)
		{
			Audio.self.playLoopSound("2f254396-e064-48ff-88c6-d43494f1435b");
		}
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0002018C File Offset: 0x0001E58C
	private VoiceLine playOnce(StandaloneLevelVoice line)
	{
		StandaloneLevelVoiceGuid voice = LevelVoice.getVoice(line);
		if (this.stats.tryUseOneTime(voice.fmodName))
		{
			return Audio.self.playVoice(voice);
		}
		return null;
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000201C3 File Offset: 0x0001E5C3
	private void allowFocusLost(VoiceLine line)
	{
		this.canLooseFocus = true;
		Global.self.canBePaused = true;
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000201D8 File Offset: 0x0001E5D8
	private void OnApplicationFocus(bool hasFocus)
	{
		if (!this.active || !this.canLooseFocus || this.alreadyLostFocus || hasFocus)
		{
			return;
		}
		if (this.stats.tryUseOneTime(LevelVoice.getVoice(this.lostFocus).fmodName))
		{
			if (this.voice != null)
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.lostFocus);
			this.voice.start(true);
			this.alreadyLostFocus = true;
		}
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0002026C File Offset: 0x0001E66C
	public IEnumerator levelFinished()
	{
		if (!this.active)
		{
			yield break;
		}
		this.canLooseFocus = false;
		if (this.voice != null)
		{
			this.voice.stop();
			this.voice = null;
		}
		AwardTimed at = AwardController.self.GetComponent<AwardTimed>();
		if (at != null && at.inTime)
		{
			StandaloneLevelVoiceGuid voice = LevelVoice.getVoice(this.achievFirst);
			if (this.stats.tryUseOneTime(voice.fmodName))
			{
				this.voice = Audio.self.playVoice(voice);
				this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
				this.voice.start(true);
				at.inTime = false;
				this.canEndLevel = false;
			}
			else
			{
				voice = LevelVoice.getVoice(this.achievSecond);
				if (this.stats.tryUseOneTime(voice.fmodName))
				{
					this.voice = Audio.self.playVoice(voice);
					this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
					this.voice.start(true);
					at.inTime = false;
					this.canEndLevel = false;
				}
				else
				{
					voice = LevelVoice.getVoice(this.achievLast);
					if (this.stats.tryUseOneTime(voice.fmodName))
					{
						this.voice = Audio.self.playVoice(voice);
						this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
						this.voice.start(true);
						at.inTime = false;
						this.canEndLevel = false;
					}
				}
			}
		}
		if (this.voice != null)
		{
			Global.self.canBePaused = false;
			while (!this.canEndLevel)
			{
				yield return null;
			}
			Global.self.canBePaused = true;
		}
		else
		{
			this.voice = Audio.self.playVoice(this.examEnd);
			this.voice.start(true);
			yield return new WaitForSeconds(1f);
		}
		yield break;
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x00020288 File Offset: 0x0001E688
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "Slide1":
			this.storyBoard.showSlide(0);
			break;
		case "Slide2":
			this.storyBoard.showSlide(1);
			break;
		case "Slide3":
			this.storyBoard.showSlide(2);
			break;
		case "StartCrossing":
			this.storyBoard.startCrossing();
			break;
		case "ShowExam":
			this.storyBoard.showExam();
			Audio.self.playLoopSound("2f254396-e064-48ff-88c6-d43494f1435b");
			break;
		case "getCup":
			AwardController.self.GetComponent<AwardTimed>().giveCup();
			break;
		case "gotoNext":
			this.canEndLevel = true;
			break;
		}
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x000203CC File Offset: 0x0001E7CC
	public void playAnswer(bool monster)
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			return;
		}
		if (monster)
		{
			StandaloneLevelVoiceGuid voice = LevelVoice.getVoice(this.badAnswer, this.lastBadAnswer);
			this.lastBadAnswer = voice.fmodName;
			this.voice = Audio.self.playVoice(voice);
		}
		else
		{
			StandaloneLevelVoiceGuid voice2 = LevelVoice.getVoice(this.goodAnswer, this.lastGoodAnswer);
			this.lastGoodAnswer = voice2.fmodName;
			this.voice = Audio.self.playVoice(voice2);
		}
		this.voice.start(true);
	}

	// Token: 0x04000E5B RID: 3675
	public bool skipIntroScene;

	// Token: 0x04000E5C RID: 3676
	public bool forceIntroScene;

	// Token: 0x04000E5D RID: 3677
	[Space(10f)]
	public PuzzleExam_StoryBoard storyBoard;

	// Token: 0x04000E5E RID: 3678
	[Space(10f)]
	public StandaloneLevelVoice examStory;

	// Token: 0x04000E5F RID: 3679
	[Space(10f)]
	public StandaloneLevelVoice examLastBad;

	// Token: 0x04000E60 RID: 3680
	public StandaloneLevelVoice examLastGood;

	// Token: 0x04000E61 RID: 3681
	public StandaloneLevelVoice examLastMix;

	// Token: 0x04000E62 RID: 3682
	public StandaloneLevelVoice examLastAbort;

	// Token: 0x04000E63 RID: 3683
	public StandaloneLevelVoice examOnOtherLoad;

	// Token: 0x04000E64 RID: 3684
	[Space(10f)]
	public StandaloneLevelVoice lostFocus;

	// Token: 0x04000E65 RID: 3685
	private bool canLooseFocus;

	// Token: 0x04000E66 RID: 3686
	private bool alreadyLostFocus;

	// Token: 0x04000E67 RID: 3687
	[Space(10f)]
	public StandaloneLevelVoice badAnswer;

	// Token: 0x04000E68 RID: 3688
	public StandaloneLevelVoice goodAnswer;

	// Token: 0x04000E69 RID: 3689
	private string lastBadAnswer;

	// Token: 0x04000E6A RID: 3690
	private string lastGoodAnswer;

	// Token: 0x04000E6B RID: 3691
	[Space(10f)]
	public StandaloneLevelVoice achievFirst;

	// Token: 0x04000E6C RID: 3692
	public StandaloneLevelVoice achievSecond;

	// Token: 0x04000E6D RID: 3693
	public StandaloneLevelVoice achievLast;

	// Token: 0x04000E6E RID: 3694
	[Space(10f)]
	public StandaloneLevelVoice examEnd;

	// Token: 0x04000E6F RID: 3695
	private SerializablePuzzleStats stats;

	// Token: 0x04000E70 RID: 3696
	private bool canEndLevel = true;
}
