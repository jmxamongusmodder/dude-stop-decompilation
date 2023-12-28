using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class AudioVoice_NonWaitingBus : AudioVoiceDefault
{
	// Token: 0x060011D0 RID: 4560 RVA: 0x000231D0 File Offset: 0x000215D0
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
			this.waitingCor = base.StartCoroutine(this.sayOpenDoor());
		}
		else if (on)
		{
			this.active = on;
			global::Console.self.canOpen = false;
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.NonWaiting_OnLoad);
			this.voice.start(true);
		}
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x00023278 File Offset: 0x00021678
	public void OnDoorOpen()
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			if (this.waitingCor != null)
			{
				base.StopCoroutine(this.waitingCor);
				this.waitingCor = null;
			}
			base.StartCoroutine(this.sayCloseDoor());
			this.doorOpened = true;
			return;
		}
		if (this.guyEnteredTimes == 0 && !this.waitForPlayed)
		{
			this.waitForPlayed = true;
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.NonWaiting_WaitFor);
			this.voice.start(true);
		}
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x0002332C File Offset: 0x0002172C
	public void OnDoorClose()
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			if (this.waitingCor != null)
			{
				base.StopCoroutine(this.waitingCor);
				this.waitingCor = null;
			}
			this.doorOpened = false;
			return;
		}
		if (!this.isEveyroneIn)
		{
			return;
		}
		int num = this.guyEnteredTimes;
		if (num != 0)
		{
			if (num != 1)
			{
				if (num == 2)
				{
					if (this.voice != null && this.voice.isPlaying())
					{
						this.voice.stop();
					}
					this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.NonWaiting_ForgetIt);
					this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
					this.voice.start(true);
					this.unit.moveAgain();
					this.canMoveGuy = true;
				}
			}
			else
			{
				base.StartCoroutine(this.delayUnitMove(Voices.VoicePack07_Duck.NonWaiting_ClosedDoor2));
			}
		}
		else
		{
			base.StartCoroutine(this.delayUnitMove(Voices.VoicePack07_Duck.NonWaiting_ClosedDoor));
		}
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x00023440 File Offset: 0x00021840
	public void OnStuckOutside()
	{
		if (!Global.self.DuckInPack07IsActive || this.noBadSolutionPlayed)
		{
			return;
		}
		if (this.guyEnteredTimes == 0 || this.guyEnteredTimes == 1)
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			this.voice = Audio.self.playVoice(Voices.VoicePack07_Duck.NonWaiting_OpenDoor);
			this.voice.start(true);
			this.noBadSolutionPlayed = true;
		}
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000234CD File Offset: 0x000218CD
	public void OnEveryoneIsInside()
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			return;
		}
		this.isEveyroneIn = true;
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000234E6 File Offset: 0x000218E6
	public void MoveGuy()
	{
		if (!Global.self.DuckInPack07IsActive)
		{
			return;
		}
		this.isEveyroneIn = false;
		this.guyEnteredTimes++;
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x00023510 File Offset: 0x00021910
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (!Global.self.DuckInPack07IsActive)
		{
			return;
		}
		if (markerName == "moveBus")
		{
			this.unit.moveAgain();
			this.canMoveGuy = true;
		}
		if (markerName == "goto")
		{
			this.unit.enabled = false;
			if (AwardController.self != null)
			{
				AwardController.self.solveAsBad(base.ps.transform);
			}
			base.ps.setSolvedAsMonster(true);
			Global.self.gotoNextLevel(false, null);
		}
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x000235B8 File Offset: 0x000219B8
	private IEnumerator delayUnitMove(StandaloneLevelVoice line)
	{
		yield return new WaitForSeconds(1f);
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(line);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x000235DA File Offset: 0x000219DA
	public bool canEnd()
	{
		return !Global.self.DuckInPack07IsActive;
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000235EC File Offset: 0x000219EC
	private IEnumerator sayOpenDoor()
	{
		yield return new WaitForSeconds(5f);
		if (this.doorOpened)
		{
			yield break;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.waiting);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x00023608 File Offset: 0x00021A08
	private IEnumerator sayCloseDoor()
	{
		yield return new WaitForSeconds(8f);
		if (!this.doorOpened)
		{
			yield break;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		this.voice = Audio.self.playVoice(this.closeItLine);
		this.voice.start(true);
		yield break;
	}

	// Token: 0x04000EDF RID: 3807
	[Space(10f)]
	public StandaloneLevelVoice waiting;

	// Token: 0x04000EE0 RID: 3808
	public StandaloneLevelVoice closeItLine;

	// Token: 0x04000EE1 RID: 3809
	public PuzzleNonWaitingBus_Door unit;

	// Token: 0x04000EE2 RID: 3810
	private bool doorOpened;

	// Token: 0x04000EE3 RID: 3811
	private int guyEnteredTimes = -1;

	// Token: 0x04000EE4 RID: 3812
	private bool isEveyroneIn;

	// Token: 0x04000EE5 RID: 3813
	[HideInInspector]
	public bool canMoveGuy;

	// Token: 0x04000EE6 RID: 3814
	private bool waitForPlayed;

	// Token: 0x04000EE7 RID: 3815
	private Coroutine waitingCor;

	// Token: 0x04000EE8 RID: 3816
	private bool noBadSolutionPlayed;
}
