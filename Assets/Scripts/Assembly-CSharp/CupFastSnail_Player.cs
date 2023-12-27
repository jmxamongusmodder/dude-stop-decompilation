using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class CupFastSnail_Player : CupFastSnail_Snail
{
	// Token: 0x06000071 RID: 113 RVA: 0x00007058 File Offset: 0x00005258
	public override void Start()
	{
		base.Start();
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00007060 File Offset: 0x00005260
	public override void Update()
	{
		base.Update();
		if (Input.GetMouseButtonDown(0) && this.canBeClicked)
		{
			this.moveSnail();
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00007084 File Offset: 0x00005284
	public void moveSnail()
	{
		Audio.self.playOneShot("f55c86bf-ddfd-45dc-9796-b6388da89b98", 1f);
		this.anim.SetBool("noLoop", false);
		if (!this.moving)
		{
			base.StartMoving();
			this.completeStop = true;
		}
		else
		{
			this.completeStop = false;
		}
		if (this.needToReverse)
		{
			base.transform.localScale = new Vector3(-base.transform.localScale.x, base.transform.localScale.y);
			this.needToReverse = false;
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00007124 File Offset: 0x00005324
	public void AddTurbo()
	{
		this.turbo.gameObject.SetActive(true);
		this.needToReverse = true;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000713E File Offset: 0x0000533E
	public void Reverse()
	{
		this.needToReverse = true;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00007147 File Offset: 0x00005347
	public override void StoppedAnimation()
	{
		if (this.completeStop)
		{
			this.anim.SetBool("noLoop", true);
			this.moving = false;
			this.StopSound();
		}
		this.completeStop = true;
	}

	// Token: 0x040000F5 RID: 245
	public float turboSpeed;

	// Token: 0x040000F6 RID: 246
	public Transform turbo;

	// Token: 0x040000F7 RID: 247
	public bool canBeClicked;

	// Token: 0x040000F8 RID: 248
	private bool completeStop;

	// Token: 0x040000F9 RID: 249
	private bool needToReverse;
}