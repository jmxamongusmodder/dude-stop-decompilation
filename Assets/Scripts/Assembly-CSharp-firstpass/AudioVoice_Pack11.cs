using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class AudioVoice_Pack11 : AudioVoice
{
	// Token: 0x0600125B RID: 4699 RVA: 0x00027A5C File Offset: 0x00025E5C
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		if (Global.self.lastPackCompletionState == CompletionState.Monster && Global.self.CountPackPlayedTimes(true, 0) == 1)
		{
			this.voice = Audio.self.playVoice(this.badEnd);
			if (Global.self.CountPackPlayedTimes(false, 0) == 1)
			{
				this.voice.setParameter(1f);
			}
		}
		else if (Global.self.lastPackCompletionState == CompletionState.Good && Global.self.CountPackPlayedTimes(true, 0) == 0 && Global.self.CountPackPlayedTimes(false, 0) == 1)
		{
			this.voice = Audio.self.playVoice(this.goodEnd);
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
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		base.StartCoroutine(this.waitForAnimations());
		this.canExit = false;
		this.unlockPack = Global.self.unlockNextPack;
		Global.self.unlockNextPack = false;
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x00027B98 File Offset: 0x00025F98
	private IEnumerator waitForAnimations()
	{
		while (Global.self.currentAwardAnimCount > 0)
		{
			yield return null;
		}
		this.voice.start(true);
		yield break;
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x00027BB3 File Offset: 0x00025FB3
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "unlock")
		{
			Global.self.unlockNextPack = this.unlockPack;
			base.StartCoroutine(this.showDuck());
		}
	}

	// Token: 0x0600125E RID: 4702 RVA: 0x00027BEC File Offset: 0x00025FEC
	private IEnumerator showDuck()
	{
		DuckPopup duck = UIControl.self.makePopupDuck(false);
		duck.setDuck(false);
		UIControl.self.StartCoroutine(UIControl.self.killDuckOnTransition(duck));
		string str = WordTranslationContainer.Get(WordTranslationContainer.Theme.CONSOLE, "PACK11_DUCK_IN_MENU", Global.self.currLanguage);
		yield return UIControl.self.StartCoroutine(duck.setTextSize(str));
		yield return UIControl.self.StartCoroutine(duck.setOneLine(str, new DuckPopupSettings[0]));
		yield break;
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x00027C00 File Offset: 0x00026000
	public override bool isClickAllowed(ClickWhileVoice click)
	{
		if (!this.canExit)
		{
			return false;
		}
		base.StartMusic(click);
		return true;
	}

	// Token: 0x04000F69 RID: 3945
	[Space(10f)]
	public StandaloneLevelVoice badEnd;

	// Token: 0x04000F6A RID: 3946
	public StandaloneLevelVoice goodEnd;

	// Token: 0x04000F6B RID: 3947
	private bool canExit = true;

	// Token: 0x04000F6C RID: 3948
	private bool unlockPack;
}
