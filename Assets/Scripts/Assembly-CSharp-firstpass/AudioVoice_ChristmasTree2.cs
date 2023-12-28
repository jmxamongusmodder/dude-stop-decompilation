using System;
using System.Collections;
using ExcelData;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class AudioVoice_ChristmasTree2 : AudioVoiceDefault
{
	// Token: 0x06001093 RID: 4243 RVA: 0x0001977A File Offset: 0x00017B7A
	public void setDecember()
	{
		this.afterDecember = true;
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x00019783 File Offset: 0x00017B83
	public void mouseDownOnTree()
	{
		if (!this.afterDecember)
		{
			base.playVoice(this.mouseDownLine, true, true);
		}
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x0001979F File Offset: 0x00017B9F
	public void outsideBox()
	{
		if (!this.active || this.waitStarted)
		{
			return;
		}
		this.canWait = true;
		this.waitStarted = true;
		base.StartCoroutine(this.waitCor());
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x000197D3 File Offset: 0x00017BD3
	public bool setAustralianEnd()
	{
		this.australian = true;
		return true;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x000197E0 File Offset: 0x00017BE0
	private IEnumerator waitCor()
	{
		yield return new WaitForSeconds(15f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!this.canWait)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.waitLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x000197FC File Offset: 0x00017BFC
	public override void subsctibeToEnding(endTextControl item)
	{
		this.canWait = false;
		if (base.ps.solvedAsBad == true && SerializablePuzzleStats.Get(base.transform.name).solvedAsBad == 1)
		{
			base.playSpecificEnd(this.firstBad, item);
			base.StartCoroutine(this.showDuck());
		}
		else if (this.australian && SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.australianEnd.levelVoiceId))
		{
			if (this.voice != null)
			{
				this.voice.stop();
				this.voice = null;
			}
			StandaloneLevelVoiceEnd voice = LevelVoice.getVoice(this.australianEnd, LevelVoice.Type.End, new bool?(true), Global.self.currLanguage);
			this.voice = Audio.self.playVoice(voice.entry);
			string endText = voice.endText;
			item.SetEnding(endText, false);
			Global.self.canExitEndScreen = false;
			this.voice.subscribeToStopped(this, delegate(VoiceLine line)
			{
				Global.self.canExitEndScreen = true;
			});
			this.voice.start(true);
		}
		else
		{
			base.subsctibeToEnding(item);
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0001994C File Offset: 0x00017D4C
	private IEnumerator showDuck()
	{
		DuckPopup duck = UIControl.self.makePopupDuck(false);
		duck.setDuck(false);
		UIControl.self.StartCoroutine(UIControl.self.killDuckOnTransition(duck));
		string txt = WordTranslationContainer.Get(WordTranslationContainer.Theme.PUZZLE, "PACK12_CHRISTMASTREE2_DUCK", Global.self.currLanguage);
		string succ = WordTranslationContainer.Get(WordTranslationContainer.Theme.MENU, "SCROLLABLE_ERROR_MESSAGE_SUCCESS", Global.self.currLanguage);
		yield return UIControl.self.StartCoroutine(duck.setTextSize(txt + "... " + succ));
		yield return UIControl.self.StartCoroutine(duck.setOneLine(txt, new DuckPopupSettings[0]));
		int dot = 0;
		while (dot <= 3)
		{
			duck.setLine(txt + new string('.', dot));
			dot++;
			yield return new WaitForSeconds(0.5f);
		}
		duck.setLine(txt + "... " + succ);
		yield break;
	}

	// Token: 0x04000D95 RID: 3477
	[Space(10f)]
	public StandaloneLevelVoice mouseDownLine;

	// Token: 0x04000D96 RID: 3478
	public StandaloneLevelVoice waitLine;

	// Token: 0x04000D97 RID: 3479
	public StandaloneLevelVoice australianEnd;

	// Token: 0x04000D98 RID: 3480
	public StandaloneLevelVoice firstBad;

	// Token: 0x04000D99 RID: 3481
	private bool canWait = true;

	// Token: 0x04000D9A RID: 3482
	private bool waitStarted;

	// Token: 0x04000D9B RID: 3483
	private bool australian;

	// Token: 0x04000D9C RID: 3484
	private bool afterDecember;
}
