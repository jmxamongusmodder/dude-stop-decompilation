using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class AudioVoice_Pack10 : AudioVoice
{
	// Token: 0x0600124E RID: 4686 RVA: 0x0002727E File Offset: 0x0002567E
	private void Awake()
	{
		if (AudioVoice_Pack10.CanUnlockNextPack(new bool?(true), AwardName.None, AwardName.None))
		{
			this.showDuck = true;
			base.StartCoroutine(this.lateAwake());
		}
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x000272A8 File Offset: 0x000256A8
	private IEnumerator lateAwake()
	{
		yield return null;
		base.SetPackStartButton(false);
		yield break;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x000272C4 File Offset: 0x000256C4
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		Global.self.pack10CutsceneActive = false;
		if (this.showDuck)
		{
			this.voice = Audio.self.playVoice(this.badEndSecond);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			SerializableGameStats.self.pack11Unlocked = true;
			Global.self.Save();
		}
		else if (AudioVoice_Pack10.CanUnlockNextPack(new bool?(false), AwardName.None, AwardName.None))
		{
			this.voice = Audio.self.playVoice(this.goodEndSecond);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			SerializableGameStats.self.pack11Unlocked = true;
			Global.self.Save();
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Monster && Global.self.CountPackPlayedTimes(0) == 1)
		{
			this.voice = Audio.self.playVoice(this.badEndFirst);
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Good && Global.self.CountPackPlayedTimes(0) == 1)
		{
			this.voice = Audio.self.playVoice(this.goodEndFirst);
		}
		if (this.voice == null)
		{
			return;
		}
		base.SetPackStartButton(false);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.canExit = true;
			base.SetPackStartButton(true);
		});
		this.voice.start(true);
		this.canExit = false;
		this.unlockPack = Global.self.unlockNextPack;
		Global.self.unlockNextPack = false;
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x0002746C File Offset: 0x0002586C
	private void Update()
	{
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.H))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.badEndSecond);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.start(true);
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000274F8 File Offset: 0x000258F8
	public static bool CanUnlockNextPack(bool? monster, AwardName award = AwardName.None, AwardName award2 = AwardName.None)
	{
		return (award == AwardName.None || award == AwardName.Pack10_Bad || award2 == AwardName.Pack10_Good) && ((monster != true && Global.self.lastPackCompletionState == CompletionState.Good && Global.self.CountPackPlayedTimes(0) >= 2 && Global.self.CountPackPlayedTimes(false, 0) == 1 && !SerializableGameStats.self.pack11Unlocked) || (monster != false && Global.self.lastPackCompletionState == CompletionState.Monster && Global.self.CountPackPlayedTimes(0) >= 2 && Global.self.CountPackPlayedTimes(true, 0) == 1 && !SerializableGameStats.self.pack11Unlocked));
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x000275E0 File Offset: 0x000259E0
	public static bool ThisPackIsLocked(AwardName award, AwardName award2)
	{
		return award == AwardName.Pack10_Bad && (Global.self.cupList[award] == CupStatus.Empty || Global.self.cupList[award2] == CupStatus.Empty) && !SerializableGameStats.self.pack11Unlocked;
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00027634 File Offset: 0x00025A34
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "startTransition"))
			{
				if (!(markerName == "Duck"))
				{
					if (!(markerName == "ShowBlack"))
					{
						if (!(markerName == "HideBlack"))
						{
							if (markerName == "Unlock")
							{
								Global.self.unlockNextPack = this.unlockPack;
							}
						}
						else
						{
							GlitchEffectController.self.startGlitch(0.1f);
							base.SetPackStartButton(true);
							UnityEngine.Object.Destroy(this.blackScreen);
						}
					}
					else
					{
						GlitchEffectController.self.startGlitch(0.1f);
						this.blackScreen = UIControl.self.makeBlackScreen();
					}
				}
				else
				{
					this.duck.SetActive(true);
					Audio.self.playOneShot("3f8864a8-020f-4101-89e6-d9bc373da66f", 1f);
				}
			}
			else
			{
				this.StartCutscene();
			}
		}
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x00027734 File Offset: 0x00025B34
	public void onAnimationEvent(global::AnimationEvent type)
	{
		if (type == global::AnimationEvent.end)
		{
			this.particles.transform.position = this.duck.transform.GetChild(0).position;
			this.particles.SetActive(true);
			Audio.self.playOneShot("aa626233-4379-4159-b838-9f7c4ea4d3b7", 1f);
			this.duck.SetActive(false);
			Global.self.unlockNextPack = this.unlockPack;
		}
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x000277AC File Offset: 0x00025BAC
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.canExit)
		{
			return false;
		}
		if (Global.self.DEBUG && Input.GetKey(KeyCode.LeftControl))
		{
			this.debugForceOn = true;
		}
		if ((click == ClickWhileVoice.start && Global.self.CountPackPlayedTimes(0) <= 1) || this.debugForceOn)
		{
			return this.CheckToStartCutscene();
		}
		base.StartMusic(click);
		return true;
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x00027820 File Offset: 0x00025C20
	private bool CheckToStartCutscene()
	{
		if (this.debugForceOff)
		{
			base.StartMusic(ClickWhileVoice.start);
			return true;
		}
		this.voice = null;
		if (Global.self.CountPackPlayedTimes(0) == 0 && !this.debugForceOn)
		{
			if (!this.debugSkipVoice && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.startFirst.levelVoiceId))
			{
				this.voice = Audio.self.playVoice(this.startFirst);
			}
		}
		else if (!SerializableGameStats.self.pack10CutscenePlayed)
		{
			Global.self.pack10CutsceneActive = true;
			if (!this.debugSkipVoice)
			{
				this.voice = Audio.self.playVoice(this.startSecond);
			}
		}
		if (this.voice == null)
		{
			this.StartCutscene();
			return false;
		}
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		base.SetPackStartButton(false);
		this.canExit = false;
		return false;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00027930 File Offset: 0x00025D30
	private void StartCutscene()
	{
		if (Global.self.pack10CutsceneActive)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.scrollableCanvas);
			transform.GetComponent<Canvas>().worldCamera = Camera.main;
			transform.GetComponent<AudioVoice_ScrollableController>().setActive(true, this.voice);
			transform.SetSiblingIndex(UIControl.self.transform.GetSiblingIndex());
		}
		base.StartMusic(ClickWhileVoice.start);
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startPack();
	}

	// Token: 0x04000F59 RID: 3929
	[Space(10f)]
	public StandaloneLevelVoice badEndFirst;

	// Token: 0x04000F5A RID: 3930
	public StandaloneLevelVoice badEndSecond;

	// Token: 0x04000F5B RID: 3931
	public StandaloneLevelVoice goodEndFirst;

	// Token: 0x04000F5C RID: 3932
	public StandaloneLevelVoice goodEndSecond;

	// Token: 0x04000F5D RID: 3933
	[Space(10f)]
	public StandaloneLevelVoice startFirst;

	// Token: 0x04000F5E RID: 3934
	public StandaloneLevelVoice startSecond;

	// Token: 0x04000F5F RID: 3935
	[Space(10f)]
	public bool debugForceOn;

	// Token: 0x04000F60 RID: 3936
	public bool debugForceOff;

	// Token: 0x04000F61 RID: 3937
	public bool debugSkipVoice;

	// Token: 0x04000F62 RID: 3938
	public Transform scrollableCanvas;

	// Token: 0x04000F63 RID: 3939
	private bool canExit = true;

	// Token: 0x04000F64 RID: 3940
	private bool showDuck;

	// Token: 0x04000F65 RID: 3941
	private bool unlockPack;

	// Token: 0x04000F66 RID: 3942
	public GameObject duck;

	// Token: 0x04000F67 RID: 3943
	public GameObject particles;

	// Token: 0x04000F68 RID: 3944
	private GameObject blackScreen;
}
