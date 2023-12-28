using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class AudioVoice_StepOnCracks : AudioVoiceDefault
{
	// Token: 0x060012FD RID: 4861 RVA: 0x0002D900 File Offset: 0x0002BD00
	public override void setActive(bool on)
	{
		if (Global.self.DEBUG && Input.GetKey(KeyCode.LeftControl))
		{
			Global.self.DuckInPack07IsActive = true;
		}
		if (!Global.self.DuckInPack07IsActive)
		{
			base.setActive(on);
			if (!this.active)
			{
				return;
			}
			base.StartCoroutine(this.waiting());
		}
		else if (on)
		{
			this.active = on;
			global::Console.self.canOpen = false;
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.StepOnCrack_OnLoad);
			this.voice.start(true);
		}
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x0002D9A3 File Offset: 0x0002BDA3
	public bool canMakeGoodSolution()
	{
		if (!this.active)
		{
			return false;
		}
		if (Global.self.DuckInPack07IsActive)
		{
			this.goodSolution = true;
			return false;
		}
		return true;
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x0002D9CC File Offset: 0x0002BDCC
	public void stepOnCrack()
	{
		if (!this.active || Global.self.DuckInPack07IsActive)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.onStep.levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.onStep);
			this.voice.start(true);
		}
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x0002DA58 File Offset: 0x0002BE58
	private IEnumerator waiting()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(15f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (this.stillWaiting)
		{
			this.voice = Audio.self.playVoice(this.wait);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x0002DA74 File Offset: 0x0002BE74
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName == "move")
		{
			this.shoe.forceMoveShoeBack();
			base.StartCoroutine(this.showDuck());
		}
		if (markerName == "Interrupt" && !this.unpaused)
		{
			this.voice.pause();
		}
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x0002DAD8 File Offset: 0x0002BED8
	private IEnumerator showDuck()
	{
		yield return new WaitForSeconds(0.1f);
		int index = 0;
		for (;;)
		{
			int num;
			index = (num = index) + 1;
			if (num >= 4)
			{
				break;
			}
			this.duckSprite.gameObject.SetActive(!this.duckSprite.gameObject.activeInHierarchy);
			this.duckSprite.position = Extensions.GetRandomPointOnScreen(Vector2.one * 0.8f);
			this.duckSprite.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-60f, 60f));
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.2f));
		}
		this.duckSprite.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x0002DAF3 File Offset: 0x0002BEF3
	public void stepOn()
	{
		this.voice.unPause(true);
		this.unpaused = true;
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x0002DB08 File Offset: 0x0002BF08
	public override void subsctibeToEnding(endTextControl item)
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			base.subsctibeToEnding(item);
			this.stillWaiting = false;
		}
		else
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			if (this.goodSolution)
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.StepOnCrack_GoodEnd);
				base.subscribeToMarkers(item, true);
			}
			else
			{
				this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.StepOnCrack_BadEnd);
				base.subscribeToMarkers(item, true);
			}
			this.voice.start(true);
			Global.self.DuckEnabled = true;
		}
	}

	// Token: 0x04000FFE RID: 4094
	[Space(10f)]
	public StandaloneLevelVoice onStep;

	// Token: 0x04000FFF RID: 4095
	public StandaloneLevelVoice wait;

	// Token: 0x04001000 RID: 4096
	private bool stillWaiting = true;

	// Token: 0x04001001 RID: 4097
	private bool goodSolution;

	// Token: 0x04001002 RID: 4098
	public PuzzleStepOnCracks_Boot shoe;

	// Token: 0x04001003 RID: 4099
	public Transform duckSprite;

	// Token: 0x04001004 RID: 4100
	private bool unpaused;
}
