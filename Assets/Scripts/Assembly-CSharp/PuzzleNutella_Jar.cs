using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class PuzzleNutella_Jar : Draggable
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000131 RID: 305 RVA: 0x0000B434 File Offset: 0x00009634
	private bool overTheBin
	{
		get
		{
			return base.transform.position.y > this.minimalSnapLine;
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000B45C File Offset: 0x0000965C
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(this.snapLine);
		GizmosExtension.DrawHorizontalLine(this.minimalSnapLine, this.snapLine - 2f, this.snapLine + 2f);
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000B48C File Offset: 0x0000968C
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.X, this.snapLine, this.snapDist), false);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000B4A7 File Offset: 0x000096A7
	private void Update()
	{
		this.CheckBinSprites();
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000B4B0 File Offset: 0x000096B0
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.dragged || this.movingUp)
		{
			return;
		}
		if (other.transform == this.binBottom && !this.hitPlayed)
		{
			Audio.self.playOneShot("4a04f1ee-3214-4dbf-a28b-1d7357ccc52a", 1f);
			this.hitPlayed = true;
			return;
		}
		if (other.transform != this.bin.transform || base.body.bodyType != RigidbodyType2D.Dynamic)
		{
			return;
		}
		if (!base.enabled)
		{
			return;
		}
		if (!this.hitPlayed)
		{
			Audio.self.playOneShot("4a04f1ee-3214-4dbf-a28b-1d7357ccc52a", 1f);
			this.hitPlayed = true;
		}
		base.GetComponent<PolygonCollider2D>().isTrigger = false;
		if (base.GetComponentsInChildren<SpriteRenderer>(false).Length > 1)
		{
			Global.LevelCompleted(0f, true);
		}
		else if (!this.knifeCream.activeSelf)
		{
			Global.LevelFailed(0f, true);
		}
		else
		{
			this.knife.canBeThrownOut = true;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000B5C8 File Offset: 0x000097C8
	public override void OnMouseDown()
	{
		if (!base.enabled || !this.dragEnabled)
		{
			return;
		}
		base.OnMouseDown();
		foreach (SpriteRenderer spriteRenderer in this.knife.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingOrder -= 5;
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000B624 File Offset: 0x00009824
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragEnabled || !this.dragged)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped())
		{
			this.dragEnabled = false;
			base.body.SetDynamic();
			if (this.overTheBin)
			{
				this.bin.sortingOrder = 0;
				this.ChangeSprites();
			}
			else
			{
				base.StartCoroutine(this.ThrowingUpCoroutine());
			}
		}
		else
		{
			base.StartCoroutine(this.ReturningCoroutine());
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000B6B7 File Offset: 0x000098B7
	private void CheckBinSprites()
	{
		if (!this.dragged)
		{
			return;
		}
		this.bin.sortingOrder = ((!this.overTheBin) ? -2 : 2);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000B6E4 File Offset: 0x000098E4
	private void ChangeSprites()
	{
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingOrder -= 5;
		}
		foreach (SpriteRenderer spriteRenderer2 in this.knife.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer2.sortingOrder += 5;
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000B758 File Offset: 0x00009958
	private IEnumerator ReturningCoroutine()
	{
		base.body.velocity = Vector2.zero;
		float timer = 0f;
		Vector2 start = base.transform.position;
		Vector2 end = this.startingPosition;
		this.dragEnabled = false;
		while (timer != this.returnTime)
		{
			timer = Mathf.MoveTowards(timer, this.returnTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.returnTime * 3.1415927f * 0.5f);
			base.transform.position = Vector2.Lerp(start, end, t);
			yield return null;
		}
		this.dragEnabled = true;
		foreach (SpriteRenderer spriteRenderer in this.knife.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingOrder += 5;
		}
		yield break;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000B773 File Offset: 0x00009973
	protected override void OnSnap(SnapPoint point)
	{
		base.OnSnap(point);
		Global.self.currPuzzle.GetComponent<AudioVoice_Nutella>().onJarSnapToBin();
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000B790 File Offset: 0x00009990
	private IEnumerator ThrowingUpCoroutine()
	{
		this.movingUp = true;
		base.body.AddForce(Vector2.up * this.upForce);
		while (!this.overTheBin)
		{
			yield return null;
		}
		this.bin.sortingOrder = 2;
		this.movingUp = false;
		this.ChangeSprites();
		yield break;
	}

	// Token: 0x040001C4 RID: 452
	public PuzzleNutella_Knife knife;

	// Token: 0x040001C5 RID: 453
	public GameObject knifeCream;

	// Token: 0x040001C6 RID: 454
	public SpriteRenderer bin;

	// Token: 0x040001C7 RID: 455
	public Transform binBottom;

	// Token: 0x040001C8 RID: 456
	public float snapLine;

	// Token: 0x040001C9 RID: 457
	public float snapDist;

	// Token: 0x040001CA RID: 458
	public float minimalSnapLine;

	// Token: 0x040001CB RID: 459
	public float returnTime;

	// Token: 0x040001CC RID: 460
	public float upForce;

	// Token: 0x040001CD RID: 461
	private bool hitPlayed;

	// Token: 0x040001CE RID: 462
	private bool movingUp;
}
