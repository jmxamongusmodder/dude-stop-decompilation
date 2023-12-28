using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class AudioVoice_Queue : AudioVoiceDefault
{
	// Token: 0x060012A8 RID: 4776 RVA: 0x00029550 File Offset: 0x00027950
	public override void setActive(bool on)
	{
		if (Global.self.DEBUG && Input.GetKey(KeyCode.LeftControl))
		{
			Global.self.DuckInPack07IsActive = true;
			Global.self.queuePuzzleIndex = 0;
		}
		if (!Global.self.DuckInPack07IsActive)
		{
			if (on)
			{
				Audio.self.UnloadBanks(this.bankList);
			}
			base.setActive(on);
		}
		else if (on)
		{
			this.active = on;
			global::Console.self.canOpen = false;
			if (Global.self.queuePuzzleIndex == 0)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue1_OnLoad);
				this.voice.start(true);
			}
			if (Global.self.queuePuzzleIndex == 1)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue2_OnLoad);
				this.voice.start(true);
			}
			if (Global.self.queuePuzzleIndex == 2)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue3_OnLoad);
				this.voice.start(true);
				this.unit.disableUnit();
				global::Console.self.showConsole(global::Console.self.queueDuckCkeck);
			}
		}
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x0002968C File Offset: 0x00027A8C
	public IEnumerator playVoice(StandaloneLevelVoice line)
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		this.voice = Audio.self.playVoice(line);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x000296B0 File Offset: 0x00027AB0
	public void onUnitClick()
	{
		if (Global.self.DuckInPack07IsActive && Global.self.queuePuzzleIndex == 2 && !this.glitched)
		{
			this.glitched = true;
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue3_WhatThe);
			this.voice.start(true);
			GlitchEffectController.self.startGlitch(0.2f);
		}
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x00029744 File Offset: 0x00027B44
	public bool trySolveGood()
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			return true;
		}
		if (Global.self.queuePuzzleIndex == 0 && !this.goodEndPlayed)
		{
			this.goodEndPlayed = true;
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue1_GoodEnd);
			this.voice.start(true);
			return false;
		}
		if (Global.self.queuePuzzleIndex == 1 && !this.goodEndPlayed)
		{
			this.goodEndPlayed = true;
			if (this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue2_ForceBad);
			this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
			this.voice.start(true);
			this.unit.disableUnit();
			return false;
		}
		if (Global.self.queuePuzzleIndex == 2 && !this.goodEndPlayed)
		{
			this.goodEndPlayed = true;
			this.unit.disableUnit();
			this.unit.forceMoveToBad();
			return false;
		}
		return false;
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0002988C File Offset: 0x00027C8C
	public bool trySolveBad()
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			return true;
		}
		if (Global.self.queuePuzzleIndex == 0)
		{
			return true;
		}
		if (Global.self.queuePuzzleIndex == 1 && !this.badEndPlayed)
		{
			this.badEndPlayed = true;
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue2_BadEnd);
			this.voice.start(true);
			base.StartCoroutine(this.BackReminder());
			return false;
		}
		if (Global.self.queuePuzzleIndex == 2 && !this.badEndPlayed)
		{
			this.badEndPlayed = true;
			return true;
		}
		return false;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x00029934 File Offset: 0x00027D34
	private IEnumerator BackReminder()
	{
		while (this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		if (this.goodEndPlayed)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue2_BadEnd_wait);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x0002994F File Offset: 0x00027D4F
	public void closeConsole()
	{
		this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.Queue3_ShouldWork);
		this.voice.start(true);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x0002998C File Offset: 0x00027D8C
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (Global.self.queuePuzzleIndex == 1 && markerName == "move")
		{
			this.unit.forceMoveToBad();
			this.badEndPlayed = true;
		}
		if (Global.self.queuePuzzleIndex == 2 && markerName == "Enable")
		{
			base.StartCoroutine(this.closingConsole());
			this.showLastLine = true;
		}
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x00029A08 File Offset: 0x00027E08
	private IEnumerator closingConsole()
	{
		while (!Input.GetButtonDown("Cancel"))
		{
			yield return null;
		}
		global::Console.self.hideConsole();
		this.unit.enableUnit();
		yield break;
		yield break;
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x00029A24 File Offset: 0x00027E24
	public void playDrag()
	{
		if (!this.active || Global.self.DuckInPack07IsActive)
		{
			return;
		}
		float num = (float)SerializablePuzzleStats.Get(base.transform.name).loadedTimes;
		if (UnityEngine.Random.value > num * 0.2f)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.onDrag);
			this.voice.start(true);
		}
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x00029ABC File Offset: 0x00027EBC
	public bool isPlaying()
	{
		return this.voice != null && this.voice.isPlaying();
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x00029AD8 File Offset: 0x00027ED8
	public override void subsctibeToEnding(endTextControl item)
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			base.subsctibeToEnding(item);
		}
		else
		{
			if (Global.self.queuePuzzleIndex == 0)
			{
				base.playSpecificEnd(Voices.VoicePack07_Duck.Queue1_BadEnd, item);
				Global.self.makeSameLevel();
				Global.self.queuePuzzleIndex++;
				return;
			}
			if (Global.self.queuePuzzleIndex == 1)
			{
				base.playSpecificEnd(Voices.VoicePack07_Duck.Queue2_BadEndForced, item);
				Global.self.makeSameLevel();
				Global.self.queuePuzzleIndex++;
				return;
			}
			if (Global.self.queuePuzzleIndex == 2)
			{
				if (this.goodEndPlayed)
				{
					base.playSpecificEnd(Voices.VoicePack07_Duck.Queue3_GoodEnd, item);
				}
				else
				{
					base.playSpecificEnd(Voices.VoicePack07_Duck.Queue3_BadEnd, item);
				}
				return;
			}
		}
	}

	// Token: 0x04000FA9 RID: 4009
	[Space(10f)]
	public StandaloneLevelVoice onDrag;

	// Token: 0x04000FAA RID: 4010
	[BankRef]
	public string[] bankList;

	// Token: 0x04000FAB RID: 4011
	public PuzzleQueue_Scumbag unit;

	// Token: 0x04000FAC RID: 4012
	private bool goodEndPlayed;

	// Token: 0x04000FAD RID: 4013
	private bool badEndPlayed;

	// Token: 0x04000FAE RID: 4014
	private bool glitched;

	// Token: 0x04000FAF RID: 4015
	[HideInInspector]
	public bool showLastLine;
}
