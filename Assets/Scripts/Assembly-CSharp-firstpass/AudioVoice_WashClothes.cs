using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class AudioVoice_WashClothes : AudioVoiceDefault
{
	// Token: 0x0600132A RID: 4906 RVA: 0x0002EA20 File Offset: 0x0002CE20
	public static IEnumerator showDuck()
	{
		DuckPopup duck = UIControl.self.makePopupDuck(false);
		duck.setDuck(false);
		UIControl.self.StartCoroutine(UIControl.self.killDuckOnTransition(duck));
		string str = WordTranslationContainer.Get(WordTranslationContainer.Theme.CONSOLE, "DUCK_POPUP_LEARNING_PROTOCOL", Global.self.currLanguage);
		yield return UIControl.self.StartCoroutine(duck.setTextSize(str.Replace("0%", "100%")));
		string[] list = str.Split(new char[]
		{
			'\n'
		});
		yield return UIControl.self.StartCoroutine(duck.setOneLine(list[0], new DuckPopupSettings[0]));
		yield return new WaitForSeconds(0.2f);
		yield return UIControl.self.StartCoroutine(duck.addNewLine(list[1], new DuckPopupSettings[]
		{
			DuckPopupSettings.ShowProcLoading
		}));
		yield return new WaitForSeconds(0.5f);
		yield return UIControl.self.StartCoroutine(duck.addNewLine(list[2], new DuckPopupSettings[]
		{
			DuckPopupSettings.ShowProcLoading
		}));
		yield return new WaitForSeconds(0.5f);
		yield return UIControl.self.StartCoroutine(duck.addNewLine(list[3], new DuckPopupSettings[0]));
		yield break;
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0002EA34 File Offset: 0x0002CE34
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "showDuck")
		{
			base.StartCoroutine(AudioVoice_WashClothes.showDuck());
		}
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x0002EA5C File Offset: 0x0002CE5C
	public override void subsctibeToEnding(endTextControl item)
	{
		if (base.ps.solvedAsBad == true && !SerializableGameStats.self.pack09DuckShowed)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.duckEnd);
			base.subscribeToMarkers(item, true);
			this.voice.start(true);
			SerializableGameStats.self.pack09DuckShowed = true;
			return;
		}
		if (this.onlyRed)
		{
			if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.onlyRedLine.levelVoiceId))
			{
				base.playSpecificEnd(this.onlyRedLine, item);
				return;
			}
		}
		else if (this.onlyWhite)
		{
			if (this.voice != null)
			{
				this.voice.stop();
				this.voice = null;
			}
			VoiceToPlay voiceTypeToPlay = AudioVoice.getVoiceTypeToPlay(SerializablePuzzleStats.Get(base.transform.name).loadedTimes);
			if (voiceTypeToPlay != VoiceToPlay.silent)
			{
				this.voice = Audio.self.playVoice(AudioVoice.getNotRepeatingVoice(base.transform.name, this.onlyWhiteLine, LevelVoice.Type.End, new bool?(true)));
				item.SetEnding(LevelVoice.getEndText(this.voice.info, new bool?(true), Global.self.currLanguage, null), false);
				Global.self.canExitEndScreen = false;
				this.voice.subscribeToStopped(this, delegate(VoiceLine line)
				{
					Global.self.canExitEndScreen = true;
				});
				this.voice.start(true);
			}
			else
			{
				item.SetEnding(LevelVoice.getEndText(this.onlyWhiteTextOnly, Global.self.currLanguage), false);
			}
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x04001021 RID: 4129
	public StandaloneLevelVoice onlyRedLine;

	// Token: 0x04001022 RID: 4130
	public StandaloneLevelVoice onlyWhiteLine;

	// Token: 0x04001023 RID: 4131
	public StandaloneLevelVoice onlyWhiteTextOnly;

	// Token: 0x04001024 RID: 4132
	[HideInInspector]
	public bool onlyRed;

	// Token: 0x04001025 RID: 4133
	[HideInInspector]
	public bool onlyWhite;

	// Token: 0x04001026 RID: 4134
	[Space(10f)]
	public StandaloneLevelVoice duckEnd;
}
