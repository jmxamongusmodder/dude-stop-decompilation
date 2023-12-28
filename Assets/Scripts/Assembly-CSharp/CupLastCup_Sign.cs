using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class CupLastCup_Sign : MonoBehaviour
{
	// Token: 0x060014F0 RID: 5360 RVA: 0x0003B4E4 File Offset: 0x000398E4
	private void Start()
	{
		this.weakJoint = this.weakLink.GetComponents<HingeJoint2D>().First((HingeJoint2D x) => x.connectedBody == base.GetComponent<Rigidbody2D>());
		this.weakLinkFirstPosition = this.weakJoint.connectedAnchor.y;
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x0003B52C File Offset: 0x0003992C
	private void Update()
	{
		this.canDragEachCurr = Mathf.MoveTowards(this.canDragEachCurr, 0f, Time.deltaTime);
		this.CheckReleaseDistance();
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x0003B550 File Offset: 0x00039950
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.canDragEachCurr > 0f)
		{
			return;
		}
		if (Vector2.Distance(base.transform.position, this.allowedPosition) > this.allowedDistance)
		{
			return;
		}
		this.moved = false;
		this.dragged = true;
		this.startMousePosition = Camera.main.GetMousePosition();
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x0003B5C4 File Offset: 0x000399C4
	private void OnMouseUp()
	{
		if (!base.enabled || !this.dragged)
		{
			return;
		}
		this.dragged = false;
		Vector2 direction;
		if (this.moved)
		{
			direction = (Camera.main.GetMousePosition() - this.startMousePosition).normalized;
		}
		else
		{
			direction = Vector2.up;
		}
		this.DerpSign(direction);
		this.canDragEachCurr = this.canDragEach;
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x0003B63B File Offset: 0x00039A3B
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!this.firstCupCollided)
		{
			this.OnFirstCupCollision(coll);
		}
		else if (coll.transform.tag == "SuccessCollider")
		{
			this.OnSecondCupCollision(coll);
		}
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0003B675 File Offset: 0x00039A75
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "FailCollider")
		{
			this.BreakGlass();
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x0003B692 File Offset: 0x00039A92
	public void ClickLink()
	{
		this.OnMouseDown();
		this.OnMouseUp();
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x0003B6A0 File Offset: 0x00039AA0
	private void DerpSign(Vector2 direction)
	{
		direction.x *= this.releaseForce;
		direction.y *= this.releaseForceY;
		base.GetComponent<Rigidbody2D>().AddForce(direction);
		if (!this.MoveToNextWeakJointPosition(true))
		{
			Audio.self.playOneShot("8fa348e0-905a-4159-894d-c4eefcee9727", 1f);
		}
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x0003B702 File Offset: 0x00039B02
	private void CheckReleaseDistance()
	{
		if (!this.dragged)
		{
			return;
		}
		if (Vector2.Distance(Camera.main.GetMousePosition(), this.startMousePosition) > this.releaseDistance)
		{
			this.moved = true;
			this.OnMouseUp();
		}
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x0003B744 File Offset: 0x00039B44
	private void BreakGlass()
	{
		Audio.self.playOneShot("6fef0e50-f429-44a7-9ce9-273479197b2c", 1f);
		this.frontGlass.gameObject.SetActive(false);
		this.backGlass.gameObject.SetActive(false);
		this.brokenGlass.gameObject.SetActive(true);
		int num = 0;
		IEnumerator enumerator = this.brokenGlass.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				Vector2 force = new Vector2(UnityEngine.Random.Range(-this.brokenGlassForce.x, this.brokenGlassForce.x), UnityEngine.Random.Range(0f, this.brokenGlassForce.y));
				if (transform.GetComponent<Rigidbody2D>() != null)
				{
					transform.GetComponent<Rigidbody2D>().AddForce(force);
					transform.GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(-this.brokenGlassTorque, this.brokenGlassTorque));
					if (num % 3 == 0)
					{
						transform.gameObject.layer = LayerMask.NameToLayer("Front");
					}
					else if (num % 3 == 1)
					{
						transform.gameObject.layer = LayerMask.NameToLayer("Prlx 1 - No touching");
					}
					else if (num % 3 == 2)
					{
						transform.gameObject.layer = LayerMask.NameToLayer("Prlx 2 - No Touching");
					}
					num++;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.awardCup.GetComponent<Collider2D>().enabled = true;
		this.awardCup.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x0003B8EC File Offset: 0x00039CEC
	private bool MoveToNextWeakJointPosition(bool playSound = true)
	{
		if (!this.checkJoints)
		{
			return false;
		}
		bool result = false;
		float num = this.weakLinkPositions[this.currentWeakLinkPosition];
		if (num > 0f)
		{
			this.sparks.Emit();
			if (playSound)
			{
				result = true;
				Audio.self.playOneShot("aeed03d9-dd5d-44af-961a-ce7da52ccb15", 1f);
			}
		}
		num = Mathf.Lerp(this.weakLinkFirstPosition, this.weakLinkLastPosition, num);
		this.weakJoint.connectedAnchor = new Vector2(this.weakJoint.connectedAnchor.x, num);
		this.currentWeakLinkPosition++;
		if (this.weakLinkPositions[this.currentWeakLinkPosition] == 999f)
		{
			this.checkJoints = false;
		}
		if (this.currentWeakLinkPosition == this.weakLinkPositions.Length - 1)
		{
			this.breakSign();
		}
		return result;
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x0003B9C8 File Offset: 0x00039DC8
	public void breakSign()
	{
		if (!Global.self.currPuzzle.GetComponent<AudioVoice_LastCup>().breakSign())
		{
			return;
		}
		UnityEngine.Object.Destroy(this.weakJoint);
		base.enabled = false;
		this.sparks.Emit();
		Audio.self.playOneShot("f3e973e9-14cc-42bb-b6ef-9aa8d276d270", 1f);
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x0003BA24 File Offset: 0x00039E24
	public void DropSign()
	{
		foreach (Transform transform in this.deletedJoints)
		{
			foreach (HingeJoint2D hingeJoint2D in transform.GetComponents<HingeJoint2D>())
			{
				hingeJoint2D.enabled = false;
			}
		}
		Audio.self.playOneShot("f3e973e9-14cc-42bb-b6ef-9aa8d276d270", 1f);
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x0003BA94 File Offset: 0x00039E94
	private void OnFirstCupCollision(Collision2D coll)
	{
		this.firstCupCollided = true;
		coll.gameObject.layer = LayerMask.NameToLayer("Front");
		coll.rigidbody.velocity = new Vector2(Mathf.Abs(coll.rigidbody.velocity.x) * 0.5f, coll.rigidbody.velocity.y);
		for (int i = 0; i < this.weakLinkPositions.Length; i++)
		{
			if (this.weakLinkPositions[i] == 999f)
			{
				this.currentWeakLinkPosition = i + 1;
				break;
			}
		}
		this.checkJoints = true;
		this.MoveToNextWeakJointPosition(false);
		this.weakLinkSprite.enabled = false;
		this.weakLinkSprite.transform.GetChild(0).gameObject.SetActive(true);
		Audio.self.playOneShot("28103a79-db13-4072-80d6-2023d8bdb834", 1f);
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x0003BB84 File Offset: 0x00039F84
	private void OnSecondCupCollision(Collision2D coll)
	{
		base.gameObject.layer = LayerMask.NameToLayer("Front");
		this.controller.EnableCup();
		coll.rigidbody.velocity = Vector2.zero;
		coll.rigidbody.AddForce(this.awardCupCollisionForce);
	}

	// Token: 0x0400127F RID: 4735
	public ParticleSystem sparks;

	// Token: 0x04001280 RID: 4736
	public CupLastCup_Controller controller;

	// Token: 0x04001281 RID: 4737
	[Header("Weak link")]
	public Transform weakLink;

	// Token: 0x04001282 RID: 4738
	public float weakLinkLastPosition;

	// Token: 0x04001283 RID: 4739
	public float[] weakLinkPositions;

	// Token: 0x04001284 RID: 4740
	private float weakLinkFirstPosition;

	// Token: 0x04001285 RID: 4741
	public SpriteRenderer weakLinkSprite;

	// Token: 0x04001286 RID: 4742
	[Header("Sign release")]
	public float releaseDistance;

	// Token: 0x04001287 RID: 4743
	public float releaseForce;

	// Token: 0x04001288 RID: 4744
	public float releaseForceY;

	// Token: 0x04001289 RID: 4745
	private Vector2 startPosition;

	// Token: 0x0400128A RID: 4746
	private HingeJoint2D weakJoint;

	// Token: 0x0400128B RID: 4747
	private int currentWeakLinkPosition;

	// Token: 0x0400128C RID: 4748
	private Vector2 startMousePosition;

	// Token: 0x0400128D RID: 4749
	private bool dragged;

	// Token: 0x0400128E RID: 4750
	private bool moved;

	// Token: 0x0400128F RID: 4751
	public bool checkJoints;

	// Token: 0x04001290 RID: 4752
	public float canDragEach = 0.1f;

	// Token: 0x04001291 RID: 4753
	private float canDragEachCurr;

	// Token: 0x04001292 RID: 4754
	public Vector2 allowedPosition;

	// Token: 0x04001293 RID: 4755
	public float allowedDistance = 1.5f;

	// Token: 0x04001294 RID: 4756
	[Header("Sign <--> Award cup")]
	public Transform awardCup;

	// Token: 0x04001295 RID: 4757
	public Transform frontGlass;

	// Token: 0x04001296 RID: 4758
	public Transform backGlass;

	// Token: 0x04001297 RID: 4759
	public Transform brokenGlass;

	// Token: 0x04001298 RID: 4760
	public Vector2 brokenGlassForce;

	// Token: 0x04001299 RID: 4761
	public float brokenGlassTorque;

	// Token: 0x0400129A RID: 4762
	public Vector2 awardCupCollisionForce;

	// Token: 0x0400129B RID: 4763
	private bool firstCupCollided;

	// Token: 0x0400129C RID: 4764
	[Header("Full sign drop")]
	public Transform[] deletedJoints;
}
