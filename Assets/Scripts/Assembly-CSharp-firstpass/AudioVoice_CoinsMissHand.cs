using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class AudioVoice_CoinsMissHand : AudioVoiceReceive
{
	// Token: 0x060010B4 RID: 4276 RVA: 0x0001A949 File Offset: 0x00018D49
	protected override void setActiveAfterVoice()
	{
		base.setActiveAfterVoice();
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.waiting());
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0001A96C File Offset: 0x00018D6C
	private IEnumerator waiting()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(8f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!this.canWait || !base.enabled || !this.active)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.wait);
		this.voice.start(true);
		this.canWait = false;
		yield break;
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0001A988 File Offset: 0x00018D88
	public void onThrowCoins()
	{
		this.canWait = false;
		if (this.voice != null && this.voice.isPlaying())
		{
			this.coinsInd++;
			return;
		}
		if (this.coinsInd >= this.coinsOutLines.Length)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.coinsOutLines[this.coinsInd].levelVoiceId))
		{
			this.voice = Audio.self.playVoice(this.coinsOutLines[this.coinsInd]);
			this.voice.start(true);
		}
		this.coinsInd++;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0001AA40 File Offset: 0x00018E40
	public void placeOnTable()
	{
		if (!this.placedOnce)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(this.placeCoinsFirst);
			this.voice.start(true);
			this.placedOnce = true;
			return;
		}
		if (this.wrongIsPlaying)
		{
			return;
		}
		this.voice = Audio.self.playVoice(this.placeCoins);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.wrongIsPlaying = false;
		});
		this.voice.start(true);
		this.wrongIsPlaying = true;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0001AAF5 File Offset: 0x00018EF5
	public void tipped()
	{
		this.tipAdded = true;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0001AB00 File Offset: 0x00018F00
	public override void subsctibeToEnding(endTextControl item)
	{
		base.subsctibeToEnding(item);
		if (!this.tipAdded)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.setParameter("Tipped", 1f);
		}
	}

	// Token: 0x04000DC0 RID: 3520
	[Space(10f)]
	public StandaloneLevelVoice wait;

	// Token: 0x04000DC1 RID: 3521
	public StandaloneLevelVoice[] coinsOutLines;

	// Token: 0x04000DC2 RID: 3522
	public StandaloneLevelVoice placeCoinsFirst;

	// Token: 0x04000DC3 RID: 3523
	public StandaloneLevelVoice placeCoins;

	// Token: 0x04000DC4 RID: 3524
	private int coinsInd;

	// Token: 0x04000DC5 RID: 3525
	private bool canWait = true;

	// Token: 0x04000DC6 RID: 3526
	private bool placedOnce;

	// Token: 0x04000DC7 RID: 3527
	private bool tipAdded;

	// Token: 0x04000DC8 RID: 3528
	private bool wrongIsPlaying;
}
