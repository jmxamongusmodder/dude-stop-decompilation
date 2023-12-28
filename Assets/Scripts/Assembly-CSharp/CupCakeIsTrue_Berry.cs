using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class CupCakeIsTrue_Berry : Draggable
{
	// Token: 0x0600003F RID: 63 RVA: 0x00004823 File Offset: 0x00002A23
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.CreateSnapPoints();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000483D File Offset: 0x00002A3D
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped())
		{
			base.StartCoroutine(this.GoingIntoCreamCoroutine());
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000486C File Offset: 0x00002A6C
	private void CreateSnapPoints()
	{
		base.RemoveAllSnapPoints();
		foreach (Transform transform in (from x in this.berrySnaps
		where !x.gameObject.activeSelf
		select x).ToList<Transform>())
		{
			base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, transform.position + Vector3.up * this.offset, this.snapDistance, transform), false);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00004924 File Offset: 0x00002B24
	private IEnumerator GoingIntoCreamCoroutine()
	{
		base.enabled = false;
		base.GetComponent<SpriteRenderer>().sortingOrder -= 30;
		base.snapPoint.transform.GetComponent<SpriteRenderer>().enabled = false;
		base.snapPoint.transform.gameObject.SetActive(true);
		float time = this.curve.keys[this.curve.length - 1].time;
		float timer = 0f;
		Vector2 start = base.transform.localPosition;
		Vector2 end = start + Vector2.down * this.offset;
		float startAngle = base.transform.eulerAngles.z;
		while (timer != time)
		{
			timer = Mathf.MoveTowards(timer, time, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(start, end, timer / time);
			float angle = Mathf.LerpAngle(startAngle, 0f, timer / time);
			base.transform.rotation = Quaternion.Euler(0f, 0f, angle);
			yield return null;
		}
		Audio.self.playOneShot("e23f5d9f-bcee-4619-91a7-ef69edd73c02", 1f);
		base.snapPoint.transform.GetComponent<SpriteRenderer>().enabled = true;
		if (this.GetComponentsInPuzzleStats(false).Count<CupCakeIsTrue_Berry>() == 1)
		{
			this.GetComponentInPuzzleStats<CupCakeIsTrue_Candle>().LightUp();
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040000B2 RID: 178
	public Transform[] berrySnaps;

	// Token: 0x040000B3 RID: 179
	public float snapDistance;

	// Token: 0x040000B4 RID: 180
	[Header("Moving down")]
	public AnimationCurve curve;

	// Token: 0x040000B5 RID: 181
	public float offset;
}
