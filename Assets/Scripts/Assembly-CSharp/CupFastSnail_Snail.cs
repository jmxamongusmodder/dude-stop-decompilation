using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class CupFastSnail_Snail : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000078 RID: 120 RVA: 0x00006EC9 File Offset: 0x000050C9
	// (set) Token: 0x06000079 RID: 121 RVA: 0x00006ED1 File Offset: 0x000050D1
	protected float speed
	{
		get
		{
			return this._speed;
		}
		set
		{
			this._speed = value;
			this.anim.SetFloat("speed", value * this.animatorMultiplier);
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00006EF2 File Offset: 0x000050F2
	public virtual void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.speed = this.originalSpeed;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00006F0C File Offset: 0x0000510C
	public virtual void Update()
	{
		if (this.moving)
		{
			base.transform.position += Vector3.right * base.transform.localScale.x * this.speed * Time.deltaTime;
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00006F6C File Offset: 0x0000516C
	private void OnDisable()
	{
		if (this.moving)
		{
			this.StopSound();
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00006F7F File Offset: 0x0000517F
	public void AddWeight()
	{
		this.weight.gameObject.SetActive(true);
		this.speed = this.weightSpeed;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00006F9E File Offset: 0x0000519E
	public void StartMoving()
	{
		this.anim.SetTrigger("play");
		this.moving = true;
		base.GetComponent<Animator>().enabled = true;
		this.StartSound();
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00006FC9 File Offset: 0x000051C9
	public void StopMoving()
	{
		this.moving = false;
		base.GetComponent<Animator>().enabled = false;
		this.StopSound();
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00006FE4 File Offset: 0x000051E4
	public virtual void StoppedAnimation()
	{
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00006FE6 File Offset: 0x000051E6
	protected virtual void StartSound()
	{
		Audio.self.playLoopSound(this.TypeToSound());
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00006FF8 File Offset: 0x000051F8
	protected virtual void StopSound()
	{
		Audio.self.stopLoopSound(this.TypeToSound(), true);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000700C File Offset: 0x0000520C
	private string TypeToSound()
	{
		switch (this.type)
		{
		case CupFastSnail_Snail.SnailType.player:
			return "63418f2e-bbad-42b0-b33e-01f09d3cb48e";
		case CupFastSnail_Snail.SnailType.faster:
			return "85b7189f-b317-4fb1-ac02-919ffee94dd4";
		case CupFastSnail_Snail.SnailType.slower:
			return "69fc5938-9f69-453e-a940-63c871f88638";
		default:
			return string.Empty;
		}
	}

	// Token: 0x040000FA RID: 250
	public float animatorMultiplier;

	// Token: 0x040000FB RID: 251
	public float originalSpeed;

	// Token: 0x040000FC RID: 252
	public float weightSpeed;

	// Token: 0x040000FD RID: 253
	public Transform weight;

	// Token: 0x040000FE RID: 254
	protected float _speed;

	// Token: 0x040000FF RID: 255
	protected bool moving;

	// Token: 0x04000100 RID: 256
	protected Animator anim;

	// Token: 0x04000101 RID: 257
	public CupFastSnail_Snail.SnailType type;

	// Token: 0x02000015 RID: 21
	public enum SnailType
	{
		// Token: 0x04000103 RID: 259
		none,
		// Token: 0x04000104 RID: 260
		player,
		// Token: 0x04000105 RID: 261
		faster,
		// Token: 0x04000106 RID: 262
		slower
	}
}
