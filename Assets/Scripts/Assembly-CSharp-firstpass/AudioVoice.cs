using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class AudioVoice : MonoBehaviour
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06001044 RID: 4164 RVA: 0x0001508C File Offset: 0x0001348C
	// (set) Token: 0x06001045 RID: 4165 RVA: 0x000150B1 File Offset: 0x000134B1
	protected PuzzleStats ps
	{
		get
		{
			if (this._ps == null)
			{
				this._ps = base.GetComponent<PuzzleStats>();
			}
			return this._ps;
		}
		set
		{
			this._ps = value;
		}
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x000150BA File Offset: 0x000134BA
	public virtual void setActive(bool on)
	{
		this.active = on;
		this.active = this.checkCondition();
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x000150CF File Offset: 0x000134CF
	public bool isActive()
	{
		return this.active;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x000150D8 File Offset: 0x000134D8
	protected bool checkCondition()
	{
		if (!this.active)
		{
			return false;
		}
		switch (this.enableCondition)
		{
		case ifCondition.Always:
			this.active = true;
			goto IL_F6;
		case ifCondition.Never:
			this.active = false;
			goto IL_F6;
		case ifCondition.onlyOnFirst:
			this.active = (Global.self.CountPackPlayedTimes(0) == 0);
			goto IL_F6;
		case ifCondition.IfNoOther:
			this.active = true;
			goto IL_F6;
		case ifCondition.FirstTimeLoading:
			this.active = (SerializablePuzzleStats.Get(base.transform.name).loadedTimes == 0);
			goto IL_F6;
		case ifCondition.onlyOnSecond:
			this.active = (Global.self.CountPackPlayedTimes(0) == 1);
			goto IL_F6;
		case ifCondition.onlyGameIntro:
			this.active = Global.self.isGameIntroActive;
			goto IL_F6;
		}
		Debug.LogError("No such ifCondition found in AudioVoice: " + this.enableCondition.ToString());
		IL_F6:
		return this.active;
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000151E4 File Offset: 0x000135E4
	protected bool playVoice(StandaloneLevelVoice line, bool stopOld, bool playOnce)
	{
		if (!this.active)
		{
			return false;
		}
		if (!stopOld && this.voice != null && this.voice.isPlaying())
		{
			return false;
		}
		if (!playOnce || SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(line.levelVoiceId))
		{
			if (stopOld && this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(line);
			this.voice.start(true);
			return true;
		}
		return false;
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x00015293 File Offset: 0x00013693
	protected virtual void markerReached(VoiceLine line, string markerName)
	{
		if (!markerName.StartsWith("end"))
		{
			return;
		}
		if (this.endTextUI == null)
		{
			base.StartCoroutine(this.waitOneFrame(line, markerName));
			return;
		}
		this.callEndTextUI(line, markerName);
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x000152D0 File Offset: 0x000136D0
	private void callEndTextUI(VoiceLine line, string markerName)
	{
		if (this.endTextUI == null)
		{
			return;
		}
		if (markerName == "end")
		{
			this.endTextUI.SetEnding(this.getEndTextFromVoicedEnding(line, null), true);
		}
		else if (markerName.Contains(":"))
		{
			string endingID = markerName.Substring(4);
			this.endTextUI.SetEnding(this.getEndTextFromVoicedEnding(line, endingID), true);
		}
		else if (markerName == "endN")
		{
			this.endTextUI.ShowNextLineOnMarker();
		}
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x00015364 File Offset: 0x00013764
	private string getEndTextFromVoicedEnding(VoiceLine line, string endingID = null)
	{
		PuzzleStats component = base.GetComponent<PuzzleStats>();
		string endText;
		if (string.IsNullOrEmpty(endingID))
		{
			endText = LevelVoice.getEndText(line.info, null, component.solvedAsBad == true, Global.self.currLanguage);
		}
		else
		{
			endText = LevelVoice.getEndText(line.info, new bool?(component.solvedAsBad == true), Global.self.currLanguage, endingID);
		}
		return endText;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x000153F2 File Offset: 0x000137F2
	protected void endVoicedEnding(VoiceLine line)
	{
		if (this.endTextUI == null)
		{
			return;
		}
		this.endTextUI.StopVoiceEnding();
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x00015414 File Offset: 0x00013814
	private IEnumerator waitOneFrame(VoiceLine line, string markerName)
	{
		yield return null;
		this.callEndTextUI(line, markerName);
		yield break;
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0001543D File Offset: 0x0001383D
	public virtual void subsctibeToEnding(endTextControl item)
	{
		this.endTextUI = item;
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x00015446 File Offset: 0x00013846
	public virtual bool isClickAllowed(ClickWhileVoice click)
	{
		return true;
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0001544C File Offset: 0x0001384C
	protected void StartMusic(ClickWhileVoice click = ClickWhileVoice.start)
	{
		if (click != ClickWhileVoice.start)
		{
			return;
		}
		Audio.self.StartSoloSnapshot(MusicTypes.InGameMusic, true);
		if (Global.self.CountPackPlayedTimes(true, 0) <= 0 || Global.self.CountPackPlayedTimes(false, 0) <= 0)
		{
			Audio.self.RestartMusic("ab175daa-8759-4af9-b3b0-74df51ee0d24");
			Audio.self.RestartMusic("4b4f2e0b-ba15-4c73-a792-849131845350");
		}
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x000154B0 File Offset: 0x000138B0
	protected static StandaloneLevelVoiceGuid getGuidToPlay(string name, StandaloneLevelVoice firstLoad, StandaloneLevelVoice otherLoad, LevelVoice.Type type, bool? monster, int loadedTimesMod = 0)
	{
		StandaloneLevelVoiceGuid standaloneLevelVoiceGuid = null;
		SerializablePuzzleStats serializablePuzzleStats = SerializablePuzzleStats.Get(name);
		if (Global.self.currentLevelPack == 0 && AwardController.self != null)
		{
			loadedTimesMod = -1;
		}
		if (serializablePuzzleStats.loadedTimes + loadedTimesMod == 0 && firstLoad != null)
		{
			standaloneLevelVoiceGuid = AudioVoice.getNotRepeatingVoice(name, firstLoad, type, monster);
			if (standaloneLevelVoiceGuid != null)
			{
				return standaloneLevelVoiceGuid;
			}
		}
		VoiceToPlay voiceToPlay = AudioVoice.getVoiceTypeToPlay(serializablePuzzleStats.getLoadedTimes(monster) + loadedTimesMod);
		if (voiceToPlay == VoiceToPlay.unique)
		{
			standaloneLevelVoiceGuid = AudioVoice.getNotRepeatingVoice(name, otherLoad, type, monster);
			if (standaloneLevelVoiceGuid != null)
			{
				return standaloneLevelVoiceGuid;
			}
			voiceToPlay = VoiceToPlay.random;
		}
		if (voiceToPlay == VoiceToPlay.random)
		{
			if (Global.self.packHasTimeLine)
			{
				standaloneLevelVoiceGuid = AudioVoice.getNotRepeatingVoice(name, Voices.VoicePack08.Generic, type, monster);
			}
			else
			{
				standaloneLevelVoiceGuid = AudioVoice.getNotRepeatingVoice(name, AudioVoiceIfNoOther.self.DefaultLoad, type, monster);
			}
		}
		return standaloneLevelVoiceGuid;
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x00015580 File Offset: 0x00013980
	protected static bool getEndingToPlay(string name, StandaloneLevelVoice firstLoad, StandaloneLevelVoice otherLoad, StandaloneLevelVoice textEnding, bool monster, out VoiceLine voice, out string endText)
	{
		int num = 0;
		if (Global.self.currentLevelPack == 0 && AwardController.self != null)
		{
			num = -1;
		}
		StandaloneLevelVoiceGuid guidToPlay = AudioVoice.getGuidToPlay(name, firstLoad, otherLoad, LevelVoice.Type.End, new bool?(monster), 0);
		SerializablePuzzleStats serializablePuzzleStats = SerializablePuzzleStats.Get(name);
		SerializablePuzzleStats serializablePuzzleStats2 = SerializablePuzzleStats.Get("PreviousPuzzleStats");
		string[] exclude = new string[]
		{
			serializablePuzzleStats.getPrevEnding(monster),
			serializablePuzzleStats2.getPrevEnding(monster)
		};
		float num2 = Mathf.Max(0.1f, 1f - (float)(serializablePuzzleStats.getLoadedTimes(new bool?(monster)) + num) * 0.2f);
		bool flag = UnityEngine.Random.value > num2;
		voice = null;
		endText = null;
		if (guidToPlay == null)
		{
			string endText2 = LevelVoice.getEndText(textEnding, exclude, monster, Global.self.currLanguage, flag);
			if (string.IsNullOrEmpty(endText2))
			{
				Debug.LogError(string.Concat(new object[]
				{
					textEnding,
					",",
					serializablePuzzleStats.getPrevEnding(monster),
					",",
					serializablePuzzleStats2.getPrevEnding(monster),
					",",
					monster,
					",",
					Global.self.currLanguage,
					",",
					flag
				}));
			}
			serializablePuzzleStats.savePrevEnding(endText2, monster);
			serializablePuzzleStats2.savePrevEnding(endText2, monster);
			endText = endText2;
			return false;
		}
		voice = Audio.self.playVoice(guidToPlay);
		if (!guidToPlay.fmodName.Contains("event:/"))
		{
			string endText3 = LevelVoice.getEndText(guidToPlay, exclude, monster, Global.self.currLanguage);
			if (string.IsNullOrEmpty(endText3))
			{
				endText3 = LevelVoice.getEndText(textEnding, exclude, monster, Global.self.currLanguage, flag);
			}
			serializablePuzzleStats.savePrevEnding(endText3, monster);
			serializablePuzzleStats2.savePrevEnding(endText3, monster);
			endText = endText3;
			return false;
		}
		if (LevelVoice.HasEndText(guidToPlay, monster, Global.self.currLanguage))
		{
			return true;
		}
		if (guidToPlay.levelVoiceId != "OneShot" || guidToPlay.bankName != "VoiceEndings")
		{
			Debug.LogError(string.Concat(new object[]
			{
				"THIS SHOULDN'T HAPPEN!! Only Global ending are allowed to be withour end lines. Call Patomkin!! ",
				guidToPlay.bankName,
				"\n",
				guidToPlay.levelVoiceId,
				"\n",
				guidToPlay.fmodName,
				"\n",
				guidToPlay.guid
			}));
		}
		string endText4 = LevelVoice.getEndText(monster, Global.self.currLanguage);
		serializablePuzzleStats.savePrevEnding(endText4, monster);
		serializablePuzzleStats2.savePrevEnding(endText4, monster);
		endText = endText4;
		return false;
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0001582C File Offset: 0x00013C2C
	protected static StandaloneLevelVoiceGuid getNotRepeatingVoice(string puzzleName, StandaloneLevelVoice item, LevelVoice.Type type, bool? monster)
	{
		if (string.IsNullOrEmpty(item.bankName))
		{
			return null;
		}
		LevelVoice entry = null;
		List<LevelVoice> voiceList = LevelVoice.getVoiceList(item, type, monster);
		if (voiceList != null && voiceList.Count != 0)
		{
			entry = voiceList.GetRandom<LevelVoice>();
			SerializablePuzzleStats serializablePuzzleStats = SerializablePuzzleStats.Get(puzzleName);
			SerializablePuzzleStats serializablePuzzleStats2 = SerializablePuzzleStats.Get("PreviousPuzzleStats");
			if (serializablePuzzleStats.isFmodNameUsed(entry.fmodName, type, monster) || serializablePuzzleStats2.isFmodNameUsed(entry.fmodName, type, monster))
			{
				if (voiceList.Count <= 1)
				{
					return null;
				}
				entry = (from x in voiceList
				where x.fmodName != entry.fmodName
				select x).ToList<LevelVoice>().GetRandom<LevelVoice>();
				serializablePuzzleStats.isFmodNameUsed(entry.fmodName, type, monster);
				serializablePuzzleStats2.isFmodNameUsed(entry.fmodName, type, monster);
			}
		}
		if (entry == null)
		{
			return null;
		}
		return new StandaloneLevelVoiceGuid(item, new Guid(entry.voiceGuid), entry.fmodName);
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0001594C File Offset: 0x00013D4C
	protected static VoiceToPlay getVoiceTypeToPlay(int loadedTimes)
	{
		float num = Mathf.Max(-0.1f, 1f - (float)loadedTimes * 0.3f);
		if (UnityEngine.Random.value <= num)
		{
			return VoiceToPlay.unique;
		}
		if (loadedTimes <= 1)
		{
			return VoiceToPlay.random;
		}
		num = Mathf.Max(0.1f, 0.8f - ((float)loadedTimes - 2f) * 0.1f);
		if (UnityEngine.Random.value <= num)
		{
			return VoiceToPlay.random;
		}
		return VoiceToPlay.silent;
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x000159B5 File Offset: 0x00013DB5
	protected void subscribeToMarkers(endTextControl item, bool subToMarker = true)
	{
		this.endTextUI = item;
		this.voice.subscribeToStopped(this, new Action<VoiceLine>(this.endVoicedEnding));
		if (subToMarker)
		{
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		}
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x000159F8 File Offset: 0x00013DF8
	protected void lockExitUntillVoiceStops(endTextControl item)
	{
		item.LockUntillVoiceHasEnded(true);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			item.LockUntillVoiceHasEnded(false);
		});
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x00015A36 File Offset: 0x00013E36
	protected void SetPackStartButton(bool on)
	{
		base.GetComponent<PuzzleStats>().UIScreenCurr.GetComponent<levelPackMenu>().startButton.GetComponent<ButtonTemplate>().setActive(on);
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x00015A58 File Offset: 0x00013E58
	private void OnValidate()
	{
		AudioVoice[] components = base.GetComponents<AudioVoice>();
		bool flag = false;
		foreach (AudioVoice audioVoice in components)
		{
			if (audioVoice.enableCondition == ifCondition.IfNoOther)
			{
				if (flag)
				{
					Debug.LogError("PuzzleStats can't have 2 IfNoOther scripts");
				}
				flag = true;
			}
		}
	}

	// Token: 0x04000D57 RID: 3415
	public ifCondition enableCondition;

	// Token: 0x04000D58 RID: 3416
	public StandaloneLevelVoice voiceLine;

	// Token: 0x04000D59 RID: 3417
	protected bool active;

	// Token: 0x04000D5A RID: 3418
	protected VoiceLine voice;

	// Token: 0x04000D5B RID: 3419
	private endTextControl endTextUI;

	// Token: 0x04000D5C RID: 3420
	private PuzzleStats _ps;
}
