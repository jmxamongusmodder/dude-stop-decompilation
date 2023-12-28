using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class CupLifeGift_Hammer : Draggable
{
	// Token: 0x060000BD RID: 189 RVA: 0x00008C38 File Offset: 0x00006E38
	private void Start()
	{
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.leftSnapPoint.position, this.snapDistance, this.leftSnapPoint), false);
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.XY, this.rightSnapPoint.position, this.snapDistance, this.rightSnapPoint), false);
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00008C97 File Offset: 0x00006E97
	private void Update()
	{
		if (this.dragged)
		{
			this.CheckRotation();
			this.CheckLock();
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00008CB0 File Offset: 0x00006EB0
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		this.nailSoundPlayed = false;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00008CC0 File Offset: 0x00006EC0
	public void Enable()
	{
		this.dragEnabled = true;
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.blinkLeft = base.StartCoroutine(this.playBlinkAnimation(this.leftNail.GetChild(0).GetComponent<ParticleSystem>()));
		this.blinkRight = base.StartCoroutine(this.playBlinkAnimation(this.rightNail.GetChild(0).GetComponent<ParticleSystem>()));
		Global.self.currPuzzle.GetComponent<AudioVoice_CupLifeGift>().startWaitHammer();
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00008D3C File Offset: 0x00006F3C
	private IEnumerator playBlinkAnimation(ParticleSystem item)
	{
		for (;;)
		{
			yield return new WaitForSeconds(Extensions.Random(this.blinkEach));
			item.Emit(1);
		}
		yield break;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00008D60 File Offset: 0x00006F60
	private void CheckRotation()
	{
		if (!base.Snapped())
		{
			return;
		}
		float num = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, -270f, this.rotationTime * Time.deltaTime);
		if (Mathf.Abs(Mathf.DeltaAngle(num, -270f)) < 1f)
		{
			num = -270f;
			base.transform.rotation = Quaternion.Euler(0f, 0f, num);
			this.LockOnNail();
		}
		base.transform.rotation = Quaternion.Euler(0f, 0f, num);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00008E00 File Offset: 0x00007000
	private void LockOnNail()
	{
		if (this.lockX)
		{
			return;
		}
		if (base.snapPoint.transform == this.leftSnapPoint)
		{
			this.targetNail = this.leftNail;
			base.StopCoroutine(this.blinkLeft);
		}
		else
		{
			this.targetNail = this.rightNail;
			base.StopCoroutine(this.blinkRight);
		}
		this.targetNail.SetParent(base.transform);
		this.targetNail.rotation = Quaternion.Euler(Vector3.zero);
		base.GetComponent<Rigidbody2D>().isKinematic = true;
		this.useRigidbody = false;
		base.RemoveSnapPoint(base.snapPoint);
		this.limit.bottom = true;
		this.limit.bottomScreen = false;
		this.limit.bottomVal = Mathf.Max(base.snapPoint.coord2D.y, base.transform.position.y);
		this.limit.limit = true;
		this.limit.top = true;
		this.limit.topScreen = false;
		this.limit.topVal = base.snapPoint.coord2D.y + this.pullOutDistance * 0.34f;
		this.lockX = true;
		this.pullOutStartY = base.snapPoint.coord2D.y;
		base.snapPoint = null;
		Global.self.currPuzzle.GetComponent<AudioVoice_CupLifeGift>().useHammer();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00008F80 File Offset: 0x00007180
	private void CheckLock()
	{
		if (!this.lockX || !this.dragged)
		{
			return;
		}
		float num = Extensions.Between(this.pullOutStartY, this.pullOutStartY + this.pullOutDistance, base.transform.position.y, true);
		if (num > this.lastT)
		{
			if (!this.nailSoundPlayed)
			{
				this.nailSoundPlayed = true;
				Audio.self.playOneShot("1e5093d4-079b-4246-9232-89e22cdec095", 1f);
			}
			this.lastT = num;
			this.limit.bottomVal = base.transform.position.y;
		}
		if (num > 0.33f && !this.uppedAt33)
		{
			this.limit.topVal += this.pullOutDistance * 0.34f;
			this.uppedAt33 = true;
			this.OnMouseUp();
			return;
		}
		if (num > 0.66f && !this.uppedAt66)
		{
			this.limit.top = false;
			this.uppedAt66 = true;
			this.OnMouseUp();
			return;
		}
		if (num < 1f)
		{
			return;
		}
		Audio.self.playOneShot("0e421ca6-86b4-4e7a-bfb5-cfd0f10bbc8b", 1f);
		Transform child = base.transform.GetChild(0);
		child.gameObject.layer = LayerMask.NameToLayer("Individual");
		child.GetComponent<Rigidbody2D>().isKinematic = false;
		child.SetParent(this.GetPuzzleStats().transform);
		child.GetComponent<SpriteRenderer>().sortingLayerName = "Top";
		UnityEngine.Object.Destroy(child.gameObject, 5f);
		base.GetComponent<Rigidbody2D>().isKinematic = false;
		this.useRigidbody = true;
		this.lockX = false;
		this.limit.limit = false;
		this.lastT = 0f;
		this.uppedAt33 = false;
		this.uppedAt66 = false;
		if (++this.nailsRemoved == 2)
		{
			this.ThrowLemons();
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000917C File Offset: 0x0000737C
	public void ThrowLemons()
	{
		this.OnMouseUp();
		this.dragEnabled = false;
		Audio.self.playOneShot("543f9ee0-cfa6-4769-b9a2-bdeb8e7531fa", 1f);
		Global.self.currPuzzle.GetComponent<AudioVoice_CupLifeGift>().OpenBox();
		this.cover.GetComponent<Rigidbody2D>().isKinematic = false;
		foreach (Rigidbody2D rigidbody2D in this.lemonParent.GetComponentsInChildren<Rigidbody2D>())
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.GetComponent<Collider2D>().isTrigger = false;
		}
		this.cupLemons.GetComponent<Rigidbody2D>().isKinematic = false;
		this.cupLemons.GetComponent<Rigidbody2D>().MovePosition(Vector2.zero);
		this.GetComponentInPuzzleStats<CupLifeGift_Cup>().canBeAcquired = true;
		base.StartCoroutine(this.LemonStoppingCoroutine());
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00009248 File Offset: 0x00007448
	private IEnumerator LemonStoppingCoroutine()
	{
		yield return new WaitForFixedUpdate();
		this.cover.GetComponent<Rigidbody2D>().velocity *= this.coverMultiplier;
		foreach (Rigidbody2D rigidbody2D in this.lemonParent.GetComponentsInChildren<Rigidbody2D>())
		{
			float num = UnityEngine.Random.Range(this.minMultiplier, this.maxMultiplier);
			rigidbody2D.velocity *= num;
			rigidbody2D.angularVelocity *= num;
		}
		this.lemonParent.GetComponent<Collider2D>().enabled = false;
		this.cupLemons.GetComponent<Rigidbody2D>().isKinematic = true;
		yield break;
	}

	// Token: 0x0400013B RID: 315
	[Header("Hammer snap")]
	public Transform leftSnapPoint;

	// Token: 0x0400013C RID: 316
	public Transform rightSnapPoint;

	// Token: 0x0400013D RID: 317
	public float snapDistance;

	// Token: 0x0400013E RID: 318
	public float pullOutDistance;

	// Token: 0x0400013F RID: 319
	public float rotationTime;

	// Token: 0x04000140 RID: 320
	[Header("Nails")]
	public Transform leftNail;

	// Token: 0x04000141 RID: 321
	public Transform rightNail;

	// Token: 0x04000142 RID: 322
	public Vector2 blinkEach = new Vector2(0.2f, 1f);

	// Token: 0x04000143 RID: 323
	private Coroutine blinkLeft;

	// Token: 0x04000144 RID: 324
	private Coroutine blinkRight;

	// Token: 0x04000145 RID: 325
	private float lastT;

	// Token: 0x04000146 RID: 326
	private bool uppedAt33;

	// Token: 0x04000147 RID: 327
	private bool uppedAt66;

	// Token: 0x04000148 RID: 328
	private float pullOutStartY;

	// Token: 0x04000149 RID: 329
	private Transform targetNail;

	// Token: 0x0400014A RID: 330
	private int nailsRemoved;

	// Token: 0x0400014B RID: 331
	[Header("Bursting lemons")]
	public Transform cup;

	// Token: 0x0400014C RID: 332
	public Transform cupLemons;

	// Token: 0x0400014D RID: 333
	public Transform cover;

	// Token: 0x0400014E RID: 334
	public Transform lemonParent;

	// Token: 0x0400014F RID: 335
	public float coverMultiplier = 0.5f;

	// Token: 0x04000150 RID: 336
	public float minMultiplier = 0.09f;

	// Token: 0x04000151 RID: 337
	public float maxMultiplier = 0.3f;

	// Token: 0x04000152 RID: 338
	private bool nailSoundPlayed;
}
