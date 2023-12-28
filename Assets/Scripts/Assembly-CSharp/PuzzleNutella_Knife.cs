using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class PuzzleNutella_Knife : Draggable
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600013E RID: 318 RVA: 0x0000BA88 File Offset: 0x00009C88
	public bool noCream
	{
		get
		{
			foreach (SpriteRenderer spriteRenderer in this.creamSprites)
			{
				if (spriteRenderer.gameObject.activeSelf)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600013F RID: 319 RVA: 0x0000BAC7 File Offset: 0x00009CC7
	public bool hasCream
	{
		get
		{
			return !this.noCream;
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000BAD4 File Offset: 0x00009CD4
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(this.verticalPosition, Color.blue);
		GizmosExtension.DrawVerticalLine(this.horizontalPosition, Color.blue);
		GizmosExtension.DrawHorizontalLine(this.bottomJarLimit, this.jarEntryLimits.xMin - 1f, this.jarEntryLimits.xMax + 1f);
		GizmosExtension.DrawRect(this.jarEntryLimits, Color.red);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000BB40 File Offset: 0x00009D40
	private void Start()
	{
		this.defaultLimit = this.limit;
		this.jarLimit = new Draggable.Limit();
		this.jarLimit.limit = true;
		this.jarLimit.disableDragOnBorder = false;
		this.jarLimit.left = true;
		this.jarLimit.right = true;
		this.jarLimit.bottom = true;
		this.jarLimit.leftScreen = false;
		this.jarLimit.rightScreen = false;
		this.jarLimit.bottomScreen = false;
		this.jarLimit.leftVal = this.jarEntryLimits.xMin;
		this.jarLimit.rightVal = this.jarEntryLimits.xMax;
		this.jarLimit.bottomVal = this.bottomJarLimit;
		this.mouseMovementsLeft = this.mouseMovements;
		base.snapEnabled = false;
		base.AddSnapPoint(new SnapPoint(Draggable.Snap.X, this.bin.transform.position.x, this.snapDist), false);
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000BC3F File Offset: 0x00009E3F
	private void Update()
	{
		this.CheckBinSprites();
		this.CheckRotation();
		this.CheckJarCollider();
		this.CheckMouseMovementInJar();
		this.CheckBreadColliders();
	}

	// Token: 0x06000143 RID: 323 RVA: 0x0000BC5F File Offset: 0x00009E5F
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (!this.dragEnabled || !base.enabled)
		{
			return;
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_Nutella>().onKnifePickUp();
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000BC94 File Offset: 0x00009E94
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		if (base.Snapped())
		{
			this.thrownOut = true;
			this.dragEnabled = false;
			this.jar.gameObject.SetActive(false);
			base.GetComponent<PolygonCollider2D>().isTrigger = true;
			base.body.SetDynamic();
		}
		else if (!this.inTheJar)
		{
			base.StartCoroutine(this.ReturningCoroutine());
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x0000BD14 File Offset: 0x00009F14
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform == this.bin.transform && base.body.bodyType == RigidbodyType2D.Dynamic && this.canBeThrownOut)
		{
			base.GetComponent<PolygonCollider2D>().isTrigger = false;
			Global.self.GetCup(AwardName.KNIFE);
			Global.self.currPuzzle.GetComponent<AudioVoice_Nutella>().throwKnifeAway();
			base.enabled = false;
			if (!this.hitPlayed)
			{
				Audio.self.playOneShot("ad5c43a7-0349-464b-b85e-cef8aa879ae7", 1f);
				this.hitPlayed = true;
			}
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000BDB4 File Offset: 0x00009FB4
	private void CheckBinSprites()
	{
		if (!this.dragged || !this.canBeThrownOut)
		{
			return;
		}
		base.snapEnabled = (base.transform.position.y > this.minimalSnapLine);
		this.bin.sortingOrder = ((!base.snapEnabled) ? -2 : 4);
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000BE18 File Offset: 0x0000A018
	private void CheckRotation()
	{
		if (this.thrownOut)
		{
			return;
		}
		if (base.transform.position.x < this.verticalPosition)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
		}
		else if (base.transform.position.x > this.horizontalPosition)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		}
		else
		{
			float z = Mathf.Lerp(180f, 90f, (base.transform.position.x - this.verticalPosition) / (this.horizontalPosition - this.verticalPosition));
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000BF08 File Offset: 0x0000A108
	private void CheckJarCollider()
	{
		bool flag = true;
		flag &= (base.transform.position.x >= this.jarEntryLimits.xMin);
		flag &= (base.transform.position.x <= this.jarEntryLimits.xMax);
		bool flag2 = flag;
		flag &= (base.transform.position.y >= this.jarEntryLimits.yMin);
		flag &= (base.transform.position.y <= this.jarEntryLimits.yMax);
		if (this.inTheZone && !flag && flag2)
		{
			this.limit = this.jarLimit;
			this.inTheJar = true;
			this.startedScrapingCream = false;
			this.jar.GetComponent<Collider2D>().enabled = false;
			this.lastPosition = base.transform.position.x;
			this.knifeSprite.sortingOrder = -3;
			foreach (SpriteRenderer spriteRenderer in this.creamSprites)
			{
				spriteRenderer.sortingOrder = -2;
			}
		}
		if (this.inTheJar && flag)
		{
			this.limit = this.defaultLimit;
			this.inTheJar = false;
			this.jar.GetComponent<Collider2D>().enabled = true;
			this.knifeSprite.sortingOrder = 2;
			foreach (SpriteRenderer spriteRenderer2 in this.creamSprites)
			{
				spriteRenderer2.sortingOrder = 3;
			}
		}
		this.inTheZone = flag;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
	private void CheckMouseMovementInJar()
	{
		if (!this.inTheJar || this.hasCream || this.currentJarCream == this.jarCreams.Length)
		{
			return;
		}
		float num = 0.2f;
		if (!this.startedScrapingCream)
		{
			if (Mathf.Abs(base.transform.position.x - this.lastPosition) > num)
			{
				this.lastPosition = ((Mathf.Abs(base.transform.position.x - this.jarEntryLimits.xMin) >= Mathf.Abs(base.transform.position.x - this.jarEntryLimits.xMax)) ? this.jarEntryLimits.xMax : this.jarEntryLimits.xMin);
				this.startedScrapingCream = true;
			}
		}
		else if (Mathf.Abs(base.transform.position.x - this.lastPosition) > num * 2f)
		{
			this.lastPosition = base.transform.position.x;
			this.mouseMovementsLeft--;
			Audio.self.playOneShot("6568ff03-9f3f-47e2-b279-08eb3e90de40", 1f);
		}
		if (this.mouseMovementsLeft == 0)
		{
			this.mouseMovementsLeft = this.mouseMovements;
			this.jarCreams[this.currentJarCream++].gameObject.SetActive(false);
			foreach (SpriteRenderer spriteRenderer in this.creamSprites)
			{
				spriteRenderer.gameObject.SetActive(true);
			}
			Global.self.currPuzzle.GetComponent<AudioVoice_Nutella>().takeNutella();
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
	private void CheckBreadColliders()
	{
		if (this.noCream || !this.dragged)
		{
			return;
		}
		bool flag = false;
		if (this.knifeBlade.IsTouching(this.bread) && !this.onTheBread)
		{
			this.onTheBread = true;
		}
		else if (!this.knifeBlade.IsTouching(this.bread) && this.onTheBread)
		{
			Audio.self.playOneShot("969e4edc-a3f3-4484-bd65-25bd662a6405", 1f);
			this.breadCreams[this.currentBreadCream++].gameObject.SetActive(true);
			int num = 2 - (this.currentBreadCream - 1) % 3;
			if (num == 0)
			{
				this.bread.offset += this.colliderOffset;
				Global.self.currPuzzle.GetComponent<AudioVoice_Nutella>().spreadFullPiece();
				if (this.canBeThrownOut)
				{
					this.canBeThrownOut = false;
					flag = true;
				}
			}
			else
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_Nutella>().spreadPiece();
			}
			this.creamSprites[num].gameObject.SetActive(false);
			this.onTheBread = false;
		}
		if (flag)
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
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
		yield break;
	}

	// Token: 0x040001CF RID: 463
	[Header("Bin")]
	public SpriteRenderer bin;

	// Token: 0x040001D0 RID: 464
	public float minimalSnapLine;

	// Token: 0x040001D1 RID: 465
	public float snapDist;

	// Token: 0x040001D2 RID: 466
	[HideInInspector]
	public bool canBeThrownOut;

	// Token: 0x040001D3 RID: 467
	private bool thrownOut;

	// Token: 0x040001D4 RID: 468
	[Header("Jar")]
	public Transform jar;

	// Token: 0x040001D5 RID: 469
	public Rect jarEntryLimits;

	// Token: 0x040001D6 RID: 470
	public float bottomJarLimit;

	// Token: 0x040001D7 RID: 471
	public SpriteRenderer[] jarCreams;

	// Token: 0x040001D8 RID: 472
	public int mouseMovements = 5;

	// Token: 0x040001D9 RID: 473
	private int currentJarCream;

	// Token: 0x040001DA RID: 474
	private int mouseMovementsLeft;

	// Token: 0x040001DB RID: 475
	private float lastPosition;

	// Token: 0x040001DC RID: 476
	private bool startedScrapingCream;

	// Token: 0x040001DD RID: 477
	private Draggable.Limit defaultLimit;

	// Token: 0x040001DE RID: 478
	private Draggable.Limit jarLimit;

	// Token: 0x040001DF RID: 479
	private bool inTheJar;

	// Token: 0x040001E0 RID: 480
	private bool inTheZone;

	// Token: 0x040001E1 RID: 481
	[Header("Knife")]
	public float verticalPosition;

	// Token: 0x040001E2 RID: 482
	public float horizontalPosition;

	// Token: 0x040001E3 RID: 483
	public float returnTime;

	// Token: 0x040001E4 RID: 484
	public SpriteRenderer[] creamSprites;

	// Token: 0x040001E5 RID: 485
	public SpriteRenderer knifeSprite;

	// Token: 0x040001E6 RID: 486
	private const int HORIZONTAL = 90;

	// Token: 0x040001E7 RID: 487
	private const int VERTICAL = 180;

	// Token: 0x040001E8 RID: 488
	private bool hitPlayed;

	// Token: 0x040001E9 RID: 489
	[Header("Bread")]
	public Collider2D knifeBlade;

	// Token: 0x040001EA RID: 490
	public Collider2D bread;

	// Token: 0x040001EB RID: 491
	public Vector2 colliderOffset;

	// Token: 0x040001EC RID: 492
	public SpriteRenderer[] breadCreams;

	// Token: 0x040001ED RID: 493
	private int currentBreadCream;

	// Token: 0x040001EE RID: 494
	private bool onTheBread;
}
