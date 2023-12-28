using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class PuzzleStepOnCracks_Boot : Draggable
{
	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0007867F File Offset: 0x00076A7F
	// (set) Token: 0x06001C63 RID: 7267 RVA: 0x00078687 File Offset: 0x00076A87
	private bool crack
	{
		get
		{
			return this._crack;
		}
		set
		{
			this._crack = value;
			if (this.dragged)
			{
				this.cracks.glow = value;
			}
		}
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x000786A8 File Offset: 0x00076AA8
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		GizmosExtension.DrawHorizontalLine(this.maxY, -10f, 10f);
		GizmosExtension.DrawHorizontalLine(this.minY, -10f, 10f);
		GizmosExtension.DrawVerticalLine(this.maxX);
		GizmosExtension.DrawVerticalLine(this.minX);
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x000786FF File Offset: 0x00076AFF
	private void Start()
	{
		this.otherBoot = base.transform.parent.GetComponentsInChildren<PuzzleStepOnCracks_Boot>().First((PuzzleStepOnCracks_Boot x) => x != this);
		this.coll = base.GetComponent<PolygonCollider2D>();
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x00078734 File Offset: 0x00076B34
	private void Update()
	{
		if (this.dragged)
		{
			this.UpdateCrackStatus();
			this.UpdateCrackPosition();
			this.UpdateSortingOrder();
			float num = base.transform.position.x - this.otherBoot.transform.position.x;
			float z = num / this.maxDistance * this.maxAngle;
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
		else
		{
			base.body.velocity = Vector3.zero;
			if (this.rotationTimer < this.rotationTime && this.rotationTimer != -1f)
			{
				this.rotationTimer = Mathf.MoveTowards(this.rotationTimer, this.rotationTime, Time.deltaTime);
				float num2 = this.rotationTimer / this.rotationTime;
				num2 = Mathf.LerpAngle(this.startingRotation, 0f, num2);
				base.transform.rotation = Quaternion.Euler(0f, 0f, num2);
				if (!this.onPavement || !this.otherBoot.onPavement)
				{
					this.otherBoot.transform.rotation = Quaternion.Euler(0f, 0f, num2 - this.startingRotation);
				}
				if (this.rotationTime == this.rotationTimer)
				{
					Transform transform = UnityEngine.Object.Instantiate<Transform>(this.footprint, base.transform.position - new Vector3(0f, this.footprintShift), Quaternion.Euler(0f, 0f, 0f));
					transform.SetParent(base.transform.parent);
					transform.gameObject.SetActive(true);
					if (this.onPavement && this.otherBoot.onPavement)
					{
						if (this.noCracks && this.otherBoot.noCracks)
						{
							if (!Global.self.currPuzzle.GetComponent<AudioVoice_StepOnCracks>().canMakeGoodSolution())
							{
								Global.LevelCompleted(0f, true);
							}
							else
							{
								Global.LevelFailed(0f, true);
							}
						}
						else
						{
							Global.LevelCompleted(0f, true);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x0007897C File Offset: 0x00076D7C
	private void UpdateSortingOrder()
	{
		if (base.transform.position.y < this.otherBoot.transform.position.y)
		{
			base.GetComponent<SpriteRenderer>().sortingOrder = 1;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, -1f);
			this.otherBoot.GetComponent<SpriteRenderer>().sortingOrder = 0;
			this.otherBoot.transform.position = new Vector3(this.otherBoot.transform.position.x, this.otherBoot.transform.position.y, 0f);
		}
		else
		{
			base.GetComponent<SpriteRenderer>().sortingOrder = 0;
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
			this.otherBoot.GetComponent<SpriteRenderer>().sortingOrder = 1;
			this.otherBoot.transform.position = new Vector3(this.otherBoot.transform.position.x, this.otherBoot.transform.position.y, -1f);
		}
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x00078B08 File Offset: 0x00076F08
	private void UpdateCrackPosition()
	{
		this.cracks.SetPosition(base.transform.position);
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00078B28 File Offset: 0x00076F28
	private void UpdateCrackStatus()
	{
		this.footCollider.transform.position = base.transform.position + this.offset;
		bool flag = false;
		foreach (Collider2D collider in this.crackColliders.GetComponents<Collider2D>())
		{
			flag |= this.footCollider.IsTouching(collider);
		}
		this.crack = flag;
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x00078B9C File Offset: 0x00076F9C
	public override void OnMouseDown()
	{
		if (this.locked || !base.enabled)
		{
			return;
		}
		base.OnMouseDown();
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x00078BBC File Offset: 0x00076FBC
	public override void OnMouseUp()
	{
		if (this.locked || !base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		base.body.velocity = Vector3.zero;
		this.startingRotation = base.transform.rotation.eulerAngles.z;
		this.rotationTimer = 0f;
		this.locked = true;
		this.coll.enabled = false;
		Audio.self.playOneShot("4c7911e4-175d-4bd1-8246-fefe01341667", 1f);
		if (this.onPavement && this.otherBoot.onPavement)
		{
			this.otherBoot.enabled = false;
		}
		else
		{
			this.otherBoot.locked = false;
			this.otherBoot.coll.enabled = true;
		}
		if (this.crack)
		{
			this.stepOnCrack();
		}
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x00078CA9 File Offset: 0x000770A9
	public void stepOnCrack()
	{
		this.cracks.GlowAll();
		this.noCracks = false;
		Global.self.currPuzzle.GetComponent<AudioVoice_StepOnCracks>().stepOnCrack();
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x00078CD4 File Offset: 0x000770D4
	protected override Vector3 ProcessMousePosition(Vector3 mouse, Vector3 delta)
	{
		mouse -= delta;
		if (mouse.y > this.maxY)
		{
			mouse.y = this.maxY;
		}
		else if (mouse.y < this.minY)
		{
			mouse.y = this.minY;
		}
		if (mouse.x < this.minX)
		{
			mouse.x = this.minX;
		}
		else if (mouse.x > this.maxX)
		{
			mouse.x = this.maxX;
		}
		mouse += delta;
		if (Vector3.Distance(mouse, this.otherBoot.transform.position) <= this.maxDistance)
		{
			return mouse;
		}
		Vector3 vector = mouse - this.otherBoot.transform.position;
		float f = Mathf.Atan2(vector.y, vector.x);
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		Vector3 a = new Vector2((this.maxDistance - 0.1f) * num, (this.maxDistance - 0.1f) * num2);
		return a + this.otherBoot.transform.position;
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x00078E18 File Offset: 0x00077218
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onPavement = true;
		}
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x00078E36 File Offset: 0x00077236
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onPavement = true;
		}
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x00078E54 File Offset: 0x00077254
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onPavement = false;
		}
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x00078E74 File Offset: 0x00077274
	public void forceMoveShoeBack()
	{
		Audio.self.playOneShot("3bef2cf1-8fa3-4a03-9b04-4152439e7141", 1f);
		this.shoeAnimaton.position = base.transform.position - this.shoeAnimaton.GetChild(0).localPosition;
		this.shoeAnimaton.rotation = base.transform.rotation;
		this.shoeAnimaton.localScale = base.transform.localScale;
		this.shoeAnimaton.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = base.GetComponent<SpriteRenderer>().sortingOrder;
		this.shoeAnimaton.gameObject.SetActive(true);
		base.gameObject.SetActive(false);
		GlitchEffectController.self.startGlitch();
	}

	// Token: 0x04001AC4 RID: 6852
	[Header("Cracks")]
	public Transform crackColliders;

	// Token: 0x04001AC5 RID: 6853
	public Collider2D footCollider;

	// Token: 0x04001AC6 RID: 6854
	public PuzzleStepOnCracks_Cracks cracks;

	// Token: 0x04001AC7 RID: 6855
	public Vector2 offset;

	// Token: 0x04001AC8 RID: 6856
	[Header("Boot stuff")]
	[Tooltip("The max distance between this boot and other boot")]
	public float maxDistance;

	// Token: 0x04001AC9 RID: 6857
	public float maxAngle;

	// Token: 0x04001ACA RID: 6858
	public float rotationTime = 0.3f;

	// Token: 0x04001ACB RID: 6859
	public float maxX;

	// Token: 0x04001ACC RID: 6860
	public float minX;

	// Token: 0x04001ACD RID: 6861
	public float maxY;

	// Token: 0x04001ACE RID: 6862
	public float minY;

	// Token: 0x04001ACF RID: 6863
	public Transform footprint;

	// Token: 0x04001AD0 RID: 6864
	public float footprintShift = 0.3f;

	// Token: 0x04001AD1 RID: 6865
	private bool locked;

	// Token: 0x04001AD2 RID: 6866
	private bool _crack;

	// Token: 0x04001AD3 RID: 6867
	private bool noCracks = true;

	// Token: 0x04001AD4 RID: 6868
	private bool onPavement;

	// Token: 0x04001AD5 RID: 6869
	private PuzzleStepOnCracks_Boot otherBoot;

	// Token: 0x04001AD6 RID: 6870
	private PolygonCollider2D coll;

	// Token: 0x04001AD7 RID: 6871
	private float startingRotation;

	// Token: 0x04001AD8 RID: 6872
	private float targetRotation;

	// Token: 0x04001AD9 RID: 6873
	private float rotationTimer = -1f;

	// Token: 0x04001ADA RID: 6874
	[Space(10f)]
	public Transform shoeAnimaton;
}
