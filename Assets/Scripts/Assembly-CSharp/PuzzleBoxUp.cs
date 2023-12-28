using System;
using UnityEngine;

// Token: 0x020003D0 RID: 976
public class PuzzleBoxUp : EnhancedDraggable
{
	// Token: 0x06001876 RID: 6262 RVA: 0x0005625D File Offset: 0x0005465D
	private void Start()
	{
		this.voice = this.GetPuzzleStats().GetComponent<AudioVoice_BoxUp>();
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x00056270 File Offset: 0x00054670
	private void Update()
	{
		this.CheckUpsideDown();
		if (this.dragged || base.body.velocity.magnitude > this.maximumVelocityMagnitude || Mathf.Abs(base.body.angularVelocity) > this.maximumAngularVelocity || Mathf.Abs(Mathf.DeltaAngle(base.transform.rotation.eulerAngles.z, 180f)) > this.maximumAngle)
		{
			this.timerGoing = false;
			return;
		}
		if (!this.timerGoing)
		{
			this.timerGoing = true;
			this.timer = this.waitBeforeEnd;
		}
		else
		{
			this.timer = Mathf.MoveTowards(this.timer, 0f, Time.deltaTime);
			if (this.timer == 0f)
			{
				this.voice.finishLevel();
				Global.LevelCompleted(0f, true);
			}
		}
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x00056368 File Offset: 0x00054768
	protected override void MouseDowned()
	{
		this.voice.boxTouched();
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x00056378 File Offset: 0x00054778
	private void CheckUpsideDown()
	{
		if (!this.dragged || this.upsideDownTimerFinished)
		{
			return;
		}
		if (Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 180f)) < this.upsideDownDeltaAngle)
		{
			this.upsideDownTime = Mathf.MoveTowards(this.upsideDownTime, this.upsideDownVoiceTime, Time.deltaTime);
			if (Mathf.Abs(this.upsideDownTime - this.upsideDownVoiceTime) < Mathf.Epsilon)
			{
				this.voice.pointingDown();
				this.upsideDownTimerFinished = true;
			}
		}
		else
		{
			this.upsideDownTime = 0f;
		}
	}

	// Token: 0x04001664 RID: 5732
	public float maximumVelocityMagnitude = 1f;

	// Token: 0x04001665 RID: 5733
	public float maximumAngularVelocity = 1f;

	// Token: 0x04001666 RID: 5734
	public float maximumAngle = 2.5f;

	// Token: 0x04001667 RID: 5735
	public float waitBeforeEnd;

	// Token: 0x04001668 RID: 5736
	private bool timerGoing;

	// Token: 0x04001669 RID: 5737
	private float timer;

	// Token: 0x0400166A RID: 5738
	public float upsideDownVoiceTime = 2f;

	// Token: 0x0400166B RID: 5739
	public float upsideDownDeltaAngle = 5f;

	// Token: 0x0400166C RID: 5740
	private bool upsideDownTimerFinished;

	// Token: 0x0400166D RID: 5741
	private float upsideDownTime;

	// Token: 0x0400166E RID: 5742
	private AudioVoice_BoxUp voice;
}
