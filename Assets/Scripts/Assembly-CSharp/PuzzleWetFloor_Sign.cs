using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class PuzzleWetFloor_Sign : Draggable
{
	// Token: 0x06001D11 RID: 7441 RVA: 0x0007F130 File Offset: 0x0007D530
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0007F140 File Offset: 0x0007D540
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.onTheFloor)
		{
			return;
		}
		this.anim.SetTrigger("Pick");
		this.onTheFloor = false;
		this.picked = true;
		Audio.self.playOneShot("9f84a546-939b-4e2e-8893-ec158bdd4d1a", 1f);
		base.OnMouseDown();
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0007F1A0 File Offset: 0x0007D5A0
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.onTheFloor && this.picked)
		{
			this.anim.SetTrigger("Place");
			this.picked = false;
			Audio.self.playOneShot("c96e0f39-5855-4729-8286-5daa1757ad1f", 1f);
		}
		float num = Vector2.Distance(base.transform.position, this.puddle.position);
		if (this.moveAway && num > this.requiredDistance)
		{
			base.StartCoroutine(this.DelayEnd(true));
		}
		else if (!this.moveAway && num < this.requiredDistance)
		{
			base.StartCoroutine(this.DelayEnd(false));
		}
		base.OnMouseUp();
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0007F278 File Offset: 0x0007D678
	private IEnumerator DelayEnd(bool monster)
	{
		UIControl.self.stopTimeLine();
		yield return new WaitForSeconds(0.3f);
		if (monster)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
		yield break;
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x0007F293 File Offset: 0x0007D693
	public void OnTheFloor()
	{
		this.onTheFloor = true;
	}

	// Token: 0x04001BB8 RID: 7096
	public Transform puddle;

	// Token: 0x04001BB9 RID: 7097
	public float requiredDistance;

	// Token: 0x04001BBA RID: 7098
	public bool moveAway;

	// Token: 0x04001BBB RID: 7099
	public float cooldown = 1f;

	// Token: 0x04001BBC RID: 7100
	private Animator anim;

	// Token: 0x04001BBD RID: 7101
	private bool onTheFloor = true;

	// Token: 0x04001BBE RID: 7102
	private bool picked;
}
