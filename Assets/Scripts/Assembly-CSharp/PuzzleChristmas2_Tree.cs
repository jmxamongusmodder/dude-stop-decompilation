using System;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class PuzzleChristmas2_Tree : EnhancedDraggable
{
	// Token: 0x06001903 RID: 6403 RVA: 0x0005B9A8 File Offset: 0x00059DA8
	private void Update()
	{
		this.Raycast();
		this.CheckVictoryConditions();
		if (base.transform.position.y > 0.5f)
		{
			this.GetComponentInPuzzleStats<AudioVoice_ChristmasTree2>().outsideBox();
		}
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x0005B9E9 File Offset: 0x00059DE9
	protected override void MouseDowned()
	{
		this.GetComponentInPuzzleStats<AudioVoice_ChristmasTree2>().mouseDownOnTree();
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x0005B9F6 File Offset: 0x00059DF6
	protected override void MouseUpped()
	{
		this.waitTimer = 0f;
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x0005BA03 File Offset: 0x00059E03
	public void TimePassed()
	{
		this.timePassed = true;
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x0005BA0C File Offset: 0x00059E0C
	private void Raycast()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		this.inTheBox = (Physics2D.Raycast(base.transform.position, base.transform.up, 2.2f, mask).collider != null);
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x0005BA74 File Offset: 0x00059E74
	private void CheckVictoryConditions()
	{
		if (this.inTheBox || this.dragged || this.Moving() || !this.IsStraight())
		{
			return;
		}
		if (base.body.angularVelocity > Mathf.Epsilon || base.body.velocity.sqrMagnitude > Mathf.Epsilon)
		{
			return;
		}
		this.waitTimer = Mathf.MoveTowards(this.waitTimer, this.waitTime, Time.deltaTime);
		if (this.waitTimer == this.waitTime)
		{
			if (Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 180f)) < 1f && !this.australianChristmasDone && Global.self.currPuzzle.GetComponent<AudioVoice_ChristmasTree2>().setAustralianEnd())
			{
				Global.self.GetCup(AwardName.CHRISTMAS);
				this.australianChristmasDone = true;
			}
			base.body.bodyType = RigidbodyType2D.Kinematic;
			if (this.timePassed)
			{
				Global.LevelFailed(0f, true);
			}
			else
			{
				Global.LevelCompleted(0f, true);
			}
		}
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x0005BBA4 File Offset: 0x00059FA4
	private bool Moving()
	{
		return base.GetComponent<Rigidbody2D>().velocity.magnitude > Mathf.Epsilon * 10f;
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x0005BBD4 File Offset: 0x00059FD4
	private bool IsStraight()
	{
		return Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 0f)) < 10f || Mathf.Abs(Mathf.DeltaAngle(base.transform.eulerAngles.z, 180f)) < 1f;
	}

	// Token: 0x040016FB RID: 5883
	public float waitTime = 0.5f;

	// Token: 0x040016FC RID: 5884
	private float waitTimer;

	// Token: 0x040016FD RID: 5885
	private bool inTheBox;

	// Token: 0x040016FE RID: 5886
	private bool timePassed;

	// Token: 0x040016FF RID: 5887
	private bool australianChristmasDone;
}
