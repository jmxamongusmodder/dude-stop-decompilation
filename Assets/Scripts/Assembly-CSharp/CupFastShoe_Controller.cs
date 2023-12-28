using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class CupFastShoe_Controller : MonoBehaviour
{
	// Token: 0x06001485 RID: 5253 RVA: 0x000377D5 File Offset: 0x00035BD5
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawHorizontalLine(this.firstY, -10f, 10f);
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x000377EC File Offset: 0x00035BEC
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		base.StartCoroutine(this.ShoeControllingCoroutine());
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x0003780C File Offset: 0x00035C0C
	private IEnumerator ShoeControllingCoroutine()
	{
		this.mainShoe = this.CreateShoe();
		this.secondShoe = this.CreateShoe();
		this.thirdShoe = this.CreateShoe();
		base.StartCoroutine(this.ShoeFlightCoroutine(this.mainShoe));
		yield return new WaitForSeconds(this.waitBetweenFirstShoes);
		base.StartCoroutine(this.ShoeFlightCoroutine(this.secondShoe));
		yield return new WaitForSeconds(this.waitBetweenFirstShoes);
		base.StartCoroutine(this.ShoeFlightCoroutine(this.thirdShoe));
		while (this.mainShoe.transform != null || this.secondShoe.transform != null || this.thirdShoe.transform != null)
		{
			yield return new WaitForEndOfFrame();
		}
		if (!base.enabled)
		{
			yield break;
		}
		while (!this.shoeClicked)
		{
			this.mainShoe = this.CreateShoe();
			this.mainShoe.EnableCollider(true);
			yield return base.StartCoroutine(this.ShoePreviewCoroutine(this.mainShoe));
			this.mainShoe.EnableCollider(false);
			if (this.shoeClicked)
			{
				yield return base.StartCoroutine(this.BadShoeFlightCoroutine(this.mainShoe));
			}
			else
			{
				yield return base.StartCoroutine(this.ShoeFlightCoroutine(this.mainShoe));
			}
		}
		yield break;
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x00037828 File Offset: 0x00035C28
	private CupFastShoe_Controller.Shoe CreateShoe()
	{
		CupFastShoe_Controller.Shoe shoe = new CupFastShoe_Controller.Shoe();
		shoe.transform = UnityEngine.Object.Instantiate<Transform>(this.shoePrefab).transform;
		shoe.transform.gameObject.SetActive(true);
		shoe.transform.SetParent(base.transform);
		shoe.currentAngle = 90f;
		this.CreatePoint(shoe, true);
		return shoe;
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x00037888 File Offset: 0x00035C88
	private IEnumerator ShoePreviewCoroutine(CupFastShoe_Controller.Shoe shoe)
	{
		while (shoe.transform.localPosition != this.previewPosition)
		{
			shoe.transform.localPosition = Vector2.MoveTowards(shoe.transform.localPosition, this.previewPosition, this.speedToPreviewPosition * Time.deltaTime);
			yield return null;
		}
		float timer = 0f;
		float target = this.peekabooCurve.GetAnimationLength();
		bool shoeClickProcessed = false;
		while (timer < target)
		{
			timer = Mathf.MoveTowards(timer, this.peekabooCurve.GetAnimationLength(), Time.deltaTime);
			shoe.transform.localPosition = this.previewPosition + Vector2.up * this.peekabooCurve.Evaluate(timer);
			if (this.shoeClicked && !shoeClickProcessed)
			{
				Keyframe key = this.peekabooCurve.NextKey(timer);
				while (key.value > 0f)
				{
					key = this.peekabooCurve.NextKey(key);
				}
				target = key.time;
				shoeClickProcessed = true;
			}
			yield return null;
		}
		if (this.shoeClicked)
		{
			while (shoe.transform.localPosition != this.previewPosition)
			{
				shoe.transform.localPosition = Vector2.MoveTowards(shoe.transform.localPosition, this.previewPosition, this.peekabooReturnSpeed * Time.deltaTime);
				yield return null;
			}
			shoe.transform.GetComponent<CupFastShoe_Shoe>().ActivateBadSprite();
		}
		yield break;
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000378AC File Offset: 0x00035CAC
	private IEnumerator ShoeFlightCoroutine(CupFastShoe_Controller.Shoe shoe)
	{
		bool onScreen = true;
		SpriteRenderer[] sprites = shoe.transform.GetComponentsInChildren<SpriteRenderer>();
		bool overTheBox = false;
		while (onScreen)
		{
			onScreen = this.CheckScreenPosition(sprites, shoe);
			this.CheckPoint(shoe);
			this.MoveShoe(shoe);
			if (!overTheBox && shoe.transform.localPosition.y > this.boxY)
			{
				foreach (SpriteRenderer spriteRenderer in sprites)
				{
					spriteRenderer.sortingLayerName = "Top";
				}
				overTheBox = true;
			}
			yield return null;
		}
		UnityEngine.Object.Destroy(shoe.transform.gameObject);
		yield break;
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x000378D0 File Offset: 0x00035CD0
	private IEnumerator BadShoeFlightCoroutine(CupFastShoe_Controller.Shoe shoe)
	{
		float timer = 0f;
		float maxTime = this.badShoeAppearance.keys[this.badShoeAppearance.length - 1].time;
		Vector2 pos = shoe.transform.localPosition;
		float timeForVoice = Mathf.Min(0.5f, maxTime);
		while (timer != maxTime)
		{
			if (timeForVoice > 0f)
			{
				timeForVoice -= Time.deltaTime;
				if (timeForVoice <= 0f)
				{
					this.GetPuzzleStats().GetComponent<AudioVoice_CupFastShoe>().playEndLine();
				}
			}
			timer = Mathf.MoveTowards(timer, maxTime, Time.deltaTime);
			pos.y = this.badShoeAppearance.Evaluate(timer);
			shoe.transform.localPosition = pos;
			yield return null;
		}
		Global.CupAcquired(shoe.transform);
		yield break;
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x000378F4 File Offset: 0x00035CF4
	private void CheckPoint(CupFastShoe_Controller.Shoe shoe)
	{
		shoe.totalTime += Time.deltaTime;
		shoe.currentTimer = Mathf.MoveTowards(shoe.currentTimer, 0f, Time.deltaTime);
		if (shoe.totalTime >= this.timeToLive)
		{
			this.CreatePoint(shoe, false);
			shoe.currentPoint.y = shoe.currentPoint.y + 5f;
			shoe.totalTime = 0f;
		}
		if (shoe.currentTimer == 0f)
		{
			this.CreatePoint(shoe, false);
		}
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x00037984 File Offset: 0x00035D84
	private void CreatePoint(CupFastShoe_Controller.Shoe shoe, bool first = false)
	{
		if (!first)
		{
			float x = UnityEngine.Random.Range(0.2f, 0.8f);
			Vector2 vector = Camera.main.ViewportToWorldPoint(new Vector3(x, 0f));
			float num = UnityEngine.Random.Range(this.minYIncrease, this.maxYIncrease);
			shoe.currentPoint = new Vector2(vector.x, shoe.currentPoint.y + num);
		}
		else
		{
			shoe.currentPoint = new Vector2(this.firstX, this.firstY);
		}
		shoe.currentTimer = UnityEngine.Random.Range(this.minTimer, this.maxTimer);
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x00037A28 File Offset: 0x00035E28
	private void MoveShoe(CupFastShoe_Controller.Shoe shoe)
	{
		Vector2 vector = shoe.currentPoint - shoe.transform.localPosition;
		float target = 90f - Mathf.Atan2(vector.x, vector.y) * 57.29578f;
		shoe.currentAngle = Mathf.MoveTowardsAngle(shoe.currentAngle, target, this.rotationSpeed * Time.deltaTime);
		Vector2 v = new Vector2(Mathf.Cos(shoe.currentAngle * 0.017453292f), Mathf.Sin(shoe.currentAngle * 0.017453292f));
		Debug.DrawLine(shoe.transform.localPosition, shoe.transform.localPosition + v * this.flightSpeed, Color.red);
		shoe.transform.localPosition += v * this.flightSpeed * Time.deltaTime;
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x00037B20 File Offset: 0x00035F20
	private bool CheckScreenPosition(SpriteRenderer[] sprites, CupFastShoe_Controller.Shoe shoe)
	{
		bool flag = false;
		flag |= (shoe.transform.localPosition.y < 0f);
		if (!flag)
		{
			foreach (SpriteRenderer spriteRenderer in sprites)
			{
				flag |= GeometryUtility.TestPlanesAABB(this.planes, spriteRenderer.bounds);
			}
		}
		return flag;
	}

	// Token: 0x040011EC RID: 4588
	public Transform shoePrefab;

	// Token: 0x040011ED RID: 4589
	public float waitBetweenFirstShoes = 0.2f;

	// Token: 0x040011EE RID: 4590
	[Header("Preview")]
	public Vector2 previewPosition;

	// Token: 0x040011EF RID: 4591
	public float speedToPreviewPosition;

	// Token: 0x040011F0 RID: 4592
	public AnimationCurve peekabooCurve;

	// Token: 0x040011F1 RID: 4593
	public float peekabooReturnSpeed = 2f;

	// Token: 0x040011F2 RID: 4594
	public float boxY;

	// Token: 0x040011F3 RID: 4595
	[Header("Shoe flight")]
	public float timeToLive;

	// Token: 0x040011F4 RID: 4596
	public float firstX;

	// Token: 0x040011F5 RID: 4597
	public float firstY;

	// Token: 0x040011F6 RID: 4598
	public float minYIncrease;

	// Token: 0x040011F7 RID: 4599
	public float maxYIncrease;

	// Token: 0x040011F8 RID: 4600
	public float minTimer;

	// Token: 0x040011F9 RID: 4601
	public float maxTimer;

	// Token: 0x040011FA RID: 4602
	public float flightSpeed;

	// Token: 0x040011FB RID: 4603
	public float rotationSpeed;

	// Token: 0x040011FC RID: 4604
	public AnimationCurve badShoeAppearance;

	// Token: 0x040011FD RID: 4605
	[HideInInspector]
	public bool shoeClicked;

	// Token: 0x040011FE RID: 4606
	private Plane[] planes;

	// Token: 0x040011FF RID: 4607
	private CupFastShoe_Controller.Shoe mainShoe;

	// Token: 0x04001200 RID: 4608
	private CupFastShoe_Controller.Shoe secondShoe;

	// Token: 0x04001201 RID: 4609
	private CupFastShoe_Controller.Shoe thirdShoe;

	// Token: 0x0200034D RID: 845
	private class Shoe
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x00037B8E File Offset: 0x00035F8E
		public void EnableCollider(bool status)
		{
			this.transform.GetComponent<Collider2D>().enabled = status;
		}

		// Token: 0x04001202 RID: 4610
		public Transform transform;

		// Token: 0x04001203 RID: 4611
		public Vector2 currentPoint;

		// Token: 0x04001204 RID: 4612
		public float currentAngle;

		// Token: 0x04001205 RID: 4613
		public float currentTimer;

		// Token: 0x04001206 RID: 4614
		public float totalTime;
	}
}
