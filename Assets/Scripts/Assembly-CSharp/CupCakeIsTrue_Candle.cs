using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class CupCakeIsTrue_Candle : PivotDraggable
{
	// Token: 0x0600004C RID: 76 RVA: 0x00004CE1 File Offset: 0x00002EE1
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.snap.position + Vector3.up * this.offset, this.snapDistance, this.snap), false);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00004D21 File Offset: 0x00002F21
	private void Update()
	{
		this.CheckRotation();
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00004D29 File Offset: 0x00002F29
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped())
		{
			base.StartCoroutine(this.GoingIntoCakeCoroutine());
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00004D58 File Offset: 0x00002F58
	public void LightUp()
	{
		if (!base.Snapped() || !this.inCake)
		{
			return;
		}
		Audio.self.playOneShot("cb864290-0dd2-499c-8a1a-a02e3422abaa", 1f);
		base.transform.GetChild(1).gameObject.SetActive(true);
		base.transform.GetChild(2).gameObject.SetActive(true);
		this.GetComponentInPuzzleStats<CupCakeIsTrue_Cake>().enabled = true;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00004DCB File Offset: 0x00002FCB
	public void SwitchLights()
	{
		base.transform.GetChild(1).gameObject.SetActive(false);
		base.transform.GetChild(3).gameObject.SetActive(true);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00004DFC File Offset: 0x00002FFC
	private void CheckRotation()
	{
		if (!base.Snapped())
		{
			return;
		}
		float z = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.rotationSpeed * Time.deltaTime);
		base.transform.rotation = Quaternion.Euler(0f, 0f, z);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00004E5A File Offset: 0x0000305A
	private bool AllBeriesPlaced()
	{
		return this.GetComponentsInPuzzleStats(false).Length == 0;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00004E68 File Offset: 0x00003068
	private IEnumerator GoingIntoCakeCoroutine()
	{
		Global.self.currPuzzle.GetComponent<AudioVoice_CupCake>().candleIn();
		base.enabled = false;
		this.dragEnabled = false;
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		foreach (Collider2D collider2D in base.GetComponentsInChildren<Collider2D>())
		{
			collider2D.enabled = false;
		}
		base.GetComponent<SpriteRenderer>().sortingOrder -= 30;
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder -= 30;
		while (Mathf.Abs(base.transform.eulerAngles.z) > 0.1f)
		{
			float angle = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.rotationSpeed * Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, angle);
			yield return null;
		}
		float time = this.curve.keys[this.curve.length - 1].time;
		float timer = 0f;
		Vector2 start = base.transform.localPosition;
		Vector2 end = start + Vector2.down * this.offset;
		while (timer != time)
		{
			timer = Mathf.MoveTowards(timer, time, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(start, end, this.curve.Evaluate(timer));
			yield return null;
		}
		Audio.self.playOneShot("b0936aeb-6288-4d65-9c15-d57e21e4bc0e", 1f);
		base.snapPoint.transform.gameObject.SetActive(true);
		base.transform.SetParent(base.snapPoint.transform);
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.enabled = false;
		}
		this.inCake = true;
		if (this.AllBeriesPlaced())
		{
			this.LightUp();
		}
		yield break;
	}

	// Token: 0x040000BA RID: 186
	public Transform snap;

	// Token: 0x040000BB RID: 187
	public float snapDistance;

	// Token: 0x040000BC RID: 188
	[Header("Moving down")]
	public AnimationCurve curve;

	// Token: 0x040000BD RID: 189
	public float offset;

	// Token: 0x040000BE RID: 190
	public float rotationSpeed;

	// Token: 0x040000BF RID: 191
	private bool inCake;
}
