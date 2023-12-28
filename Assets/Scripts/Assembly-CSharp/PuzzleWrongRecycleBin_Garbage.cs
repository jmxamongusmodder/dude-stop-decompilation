using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class PuzzleWrongRecycleBin_Garbage : PivotDraggable
{
	// Token: 0x0600015F RID: 351 RVA: 0x0000D2AC File Offset: 0x0000B4AC
	private void Start()
	{
		this.horizontalSnap = new SnapPoint(Draggable.Snap.X, this.bin.position.x, this.snapDist);
		base.AddSnapPoint(this.horizontalSnap, false);
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.bin.position, this.snapDist), false);
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000D30E File Offset: 0x0000B50E
	private void Update()
	{
		this.CheckHorizontalSnapPoint();
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000D316 File Offset: 0x0000B516
	protected override void OnSnap(SnapPoint point)
	{
		base.OnSnap(point);
		this.GetPuzzleStats().GetComponent<AudioVoice_WrongRecycleBin>().snapObject();
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000D330 File Offset: 0x0000B530
	protected override void MouseUpped()
	{
		base.MouseUpped();
		if (!base.Snapped())
		{
			return;
		}
		if (base.snapPoint.type == Draggable.Snap.XY)
		{
			base.StartCoroutine(this.InsertionCoroutine());
		}
		else
		{
			this.SwitchSprites();
		}
		base.transform.SetX(base.snapPoint.coord2D.x);
		this.DisableInput();
		base.StartCoroutine(this.RotationCoroutine());
		base.StartCoroutine(this.DestructionCoroutine());
		this.GetPuzzleStats().GetComponent<AudioVoice_WrongRecycleBin>().throwObject();
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
	private void CheckHorizontalSnapPoint()
	{
		if (!base.enabled || !this.dragged)
		{
			return;
		}
		this.horizontalSnap.enabled = (base.transform.position.y > this.horizontalSnapMin);
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000D410 File Offset: 0x0000B610
	private IEnumerator InsertionCoroutine()
	{
		base.GetComponent<Rigidbody2D>().AddForce(Vector2.up * this.forceUp);
		yield return new WaitForSeconds(0.1f);
		while (base.GetComponent<Rigidbody2D>().velocity.y > 0f)
		{
			yield return null;
		}
		this.SwitchSprites();
		yield break;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000D42C File Offset: 0x0000B62C
	private IEnumerator RotationCoroutine()
	{
		if (this.rotationTime == 0f)
		{
			base.GetComponent<Rigidbody2D>().AddTorque(this.torque);
			yield break;
		}
		float timer = 0f;
		float start = base.transform.eulerAngles.z;
		while (timer < this.rotationTime)
		{
			timer = Mathf.MoveTowards(timer, this.rotationTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.rotationTime * 0.5f * 3.1415927f);
			float angle = Mathf.LerpAngle(start, this.targetAngle, t);
			base.transform.SetAngle(angle);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000D448 File Offset: 0x0000B648
	private IEnumerator DestructionCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		while (base.transform.position.y > this.bin.position.y)
		{
			yield return null;
		}
		Audio.self.playOneShot(this.inBinSound, 1f);
		this.CheckVictory();
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000D463 File Offset: 0x0000B663
	private void DisableInput()
	{
		this.dragged = false;
		this.dragEnabled = false;
		base.GetComponent<Collider2D>().isTrigger = true;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000D48F File Offset: 0x0000B68F
	private void SwitchSprites()
	{
		base.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
	private void CheckVictory()
	{
		int num = this.GetComponentsInPuzzleStats(false).Length;
		if (num > 1)
		{
			return;
		}
		Global.LevelCompleted(0f, true);
	}

	// Token: 0x04000206 RID: 518
	public Transform bin;

	// Token: 0x04000207 RID: 519
	public float snapDist;

	// Token: 0x04000208 RID: 520
	public float horizontalSnapMin;

	// Token: 0x04000209 RID: 521
	public float forceUp = 2f;

	// Token: 0x0400020A RID: 522
	public float rotationTime = 0.5f;

	// Token: 0x0400020B RID: 523
	public float targetAngle;

	// Token: 0x0400020C RID: 524
	public float torque = 10f;

	// Token: 0x0400020D RID: 525
	private SnapPoint horizontalSnap;

	// Token: 0x0400020E RID: 526
	[EventRef]
	public string inBinSound;
}
