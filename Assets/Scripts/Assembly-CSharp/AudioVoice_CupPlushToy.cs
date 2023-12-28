using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class AudioVoice_CupPlushToy : AudioVoice
{
	// Token: 0x0600111B RID: 4379 RVA: 0x0001D8C5 File Offset: 0x0001BCC5
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.playIntro());
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0001D8E8 File Offset: 0x0001BCE8
	private IEnumerator playIntro()
	{
		this.introPlaying = true;
		yield return new WaitForSeconds(2f);
		this.voice = Audio.self.playVoice(this.startVoice);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, delegate(VoiceLine line2)
		{
			this.introPlaying = false;
		});
		this.voice.start(true);
		base.StartCoroutine(this.hintToUseMachine());
		yield break;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0001D904 File Offset: 0x0001BD04
	private IEnumerator hintToUseMachine()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(8f);
		if (this.cupTouched)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.hintToUseLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0001D91F File Offset: 0x0001BD1F
	public void touchCup()
	{
		MonoBehaviour.print("going to touch");
		this.cupTouched = true;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0001D934 File Offset: 0x0001BD34
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "Interrupt1"))
			{
				if (!(markerName == "Interrupt2"))
				{
					if (markerName == "Drop")
					{
						this.canDropClaw = true;
					}
				}
				else if (this.cupDropped)
				{
					this.voice.pause();
					VoiceLine voiceLine = Audio.self.playVoice(this.interrupt2);
					voiceLine.subscribeToStopped(this, delegate(VoiceLine line2)
					{
						this.voice.unPause(false);
					});
					voiceLine.start(true);
				}
			}
			else if (this.cupDropped)
			{
				this.voice.pause();
				VoiceLine voiceLine2 = Audio.self.playVoice(this.interrupt1);
				voiceLine2.subscribeToStopped(this, delegate(VoiceLine line2)
				{
					this.voice.unPause(false);
				});
				voiceLine2.start(true);
			}
		}
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0001DA1C File Offset: 0x0001BE1C
	public void dropCup()
	{
		if (!this.introPlaying)
		{
			if (this.droppedCount <= 1)
			{
				if (this.voice != null && this.voice.isPlaying())
				{
					this.voice.stop();
				}
				if (this.droppedCount == 0)
				{
					this.voice = Audio.self.playVoice(this.droppedLine1);
				}
				else if (this.droppedCount == 1)
				{
					this.voice = Audio.self.playVoice(this.droppedLine2);
				}
				this.voice.start(true);
			}
			this.droppedCount++;
		}
		else
		{
			this.cupDropped = true;
		}
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0001DAD4 File Offset: 0x0001BED4
	public void sayLastLine()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.droppedLineLast);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.subscribeToStopped(this, delegate(VoiceLine line2)
		{
			base.StartCoroutine(this.hintToPickCup());
		});
		this.voice.start(true);
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0001DB5C File Offset: 0x0001BF5C
	private IEnumerator hintToPickCup()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		if (!base.enabled)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.hintToPickLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0001DB78 File Offset: 0x0001BF78
	public void missCup()
	{
		if (this.introPlaying)
		{
			return;
		}
		if (this.missedCount <= 1)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			if (this.missedCount == 0)
			{
				this.voice = Audio.self.playVoice(this.missedLine1);
			}
			else if (this.missedCount == 1)
			{
				this.voice = Audio.self.playVoice(this.missedLine2);
			}
			this.voice.start(true);
		}
		this.missedCount++;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0001DC28 File Offset: 0x0001C028
	public override void subsctibeToEnding(endTextControl item)
	{
		string newLine = LevelVoice.getEndText(this.endText, Global.self.currLanguage);
		item.SetEnding(newLine, false);
	}

	// Token: 0x04000E1C RID: 3612
	[Space(10f)]
	public StandaloneLevelVoice startVoice;

	// Token: 0x04000E1D RID: 3613
	public StandaloneLevelVoice hintToUseLine;

	// Token: 0x04000E1E RID: 3614
	public StandaloneLevelVoice hintToPickLine;

	// Token: 0x04000E1F RID: 3615
	public StandaloneLevelVoice endText;

	// Token: 0x04000E20 RID: 3616
	public StandaloneLevelVoice interrupt1;

	// Token: 0x04000E21 RID: 3617
	public StandaloneLevelVoice interrupt2;

	// Token: 0x04000E22 RID: 3618
	public StandaloneLevelVoice droppedLine1;

	// Token: 0x04000E23 RID: 3619
	public StandaloneLevelVoice droppedLine2;

	// Token: 0x04000E24 RID: 3620
	public StandaloneLevelVoice droppedLineLast;

	// Token: 0x04000E25 RID: 3621
	public StandaloneLevelVoice missedLine1;

	// Token: 0x04000E26 RID: 3622
	public StandaloneLevelVoice missedLine2;

	// Token: 0x04000E27 RID: 3623
	private bool introPlaying;

	// Token: 0x04000E28 RID: 3624
	private int droppedCount;

	// Token: 0x04000E29 RID: 3625
	private bool cupDropped;

	// Token: 0x04000E2A RID: 3626
	private int missedCount;

	// Token: 0x04000E2B RID: 3627
	private bool cupTouched;

	// Token: 0x04000E2C RID: 3628
	[HideInInspector]
	public bool canDropClaw;
}
