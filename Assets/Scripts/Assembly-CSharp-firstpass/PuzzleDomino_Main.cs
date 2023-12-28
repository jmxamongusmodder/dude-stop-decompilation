using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class PuzzleDomino_Main : MonoBehaviour
{
	// Token: 0x060019CA RID: 6602 RVA: 0x00062384 File Offset: 0x00060784
	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform item = (Transform)obj;
				this.slides.Add(item);
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
		this.coll = base.GetComponent<BoxCollider2D>();
		this.body = base.GetComponent<Rigidbody2D>();
		this.defaultCenterOfMass = this.body.centerOfMass;
		base.StartCoroutine(this.SlideCoroutine());
		base.StartCoroutine(this.VelocityLimitCoroutine());
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x00062434 File Offset: 0x00060834
	private void Update()
	{
		this.CalculateAngle();
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x0006243C File Offset: 0x0006083C
	private void OnMouseDown()
	{
		if (!base.enabled || this.dragged)
		{
			return;
		}
		this.dragged = true;
		this.body.centerOfMass = Vector2.zero;
		Audio.self.playOneShot("b77a4f33-10c5-431b-8251-995a2b6733cd", 1f);
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x0006248C File Offset: 0x0006088C
	private void OnMouseUp()
	{
		this.dragged = false;
		this.ResetPhysics();
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x0006249B File Offset: 0x0006089B
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform == this.secondDomino)
		{
			this.OnMouseUp();
			base.enabled = false;
			this.CheckAchievement(other.relativeVelocity);
		}
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x000624CC File Offset: 0x000608CC
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider" && base.enabled)
		{
			Global.LevelCompleted(this.timeToWait, true);
			this.ResetPhysics();
		}
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x00062500 File Offset: 0x00060900
	private void OnDisable()
	{
		this.ResetPhysics();
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x00062508 File Offset: 0x00060908
	private void CalculateAngle()
	{
		if (!this.dragged)
		{
			return;
		}
		this.body.velocity = Vector2.zero;
		Vector3 mousePosition = Input.mousePosition;
		Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.localPosition);
		Vector2 vector2 = new Vector2(mousePosition.x - vector.x, mousePosition.y - vector.y);
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		num -= 90f;
		float? num2 = this.delta;
		if (num2 == null)
		{
			this.delta = new float?(num);
		}
		else
		{
			float num3 = num;
			float? num4 = this.delta;
			num = num3 - num4.Value;
			this.body.MoveRotation(num);
		}
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x000625DA File Offset: 0x000609DA
	private void ResetPhysics()
	{
		if (this.body != null)
		{
			this.body.centerOfMass = this.defaultCenterOfMass;
		}
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x00062600 File Offset: 0x00060A00
	private IEnumerator SlideCoroutine()
	{
		for (;;)
		{
			int slide = Mathf.Clamp((int)base.transform.rotation.eulerAngles.z / 15, 1, 7);
			if (slide == 6)
			{
				slide = 5;
			}
			if (slide == 7)
			{
				slide = 1;
			}
			if (slide != this.currentSlide)
			{
				this.coll.offset = new Vector2((float)(slide - 3) * 0.08f, this.coll.offset.y);
				foreach (Transform transform in this.slides)
				{
					transform.gameObject.SetActive(false);
				}
				this.slides[slide - 1].gameObject.SetActive(true);
				this.currentSlide = slide;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x0006261C File Offset: 0x00060A1C
	private IEnumerator VelocityLimitCoroutine()
	{
		for (;;)
		{
			if (this.body.velocity.magnitude > this.magnitudeLimit)
			{
				this.body.velocity = this.body.velocity.normalized * this.magnitudeLimit;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x060019D5 RID: 6613 RVA: 0x00062637 File Offset: 0x00060A37
	private void CheckAchievement(Vector2 relativeVelocity)
	{
		if (Mathf.Abs(relativeVelocity.x) < this.achievementVelocity)
		{
			return;
		}
		if (Global.self.GetCup(AwardName.DOMINO))
		{
			this.GetPuzzleStats().GetComponent<AudioVoiceEndAchievement>().getTrophy();
		}
	}

	// Token: 0x040017DA RID: 6106
	public Transform secondDomino;

	// Token: 0x040017DB RID: 6107
	public float timeToWait = 1f;

	// Token: 0x040017DC RID: 6108
	public float achievementVelocity = 20f;

	// Token: 0x040017DD RID: 6109
	public float magnitudeLimit = 8f;

	// Token: 0x040017DE RID: 6110
	private Rigidbody2D body;

	// Token: 0x040017DF RID: 6111
	private BoxCollider2D coll;

	// Token: 0x040017E0 RID: 6112
	private List<Transform> slides = new List<Transform>();

	// Token: 0x040017E1 RID: 6113
	private int currentSlide = 1;

	// Token: 0x040017E2 RID: 6114
	private bool dragged;

	// Token: 0x040017E3 RID: 6115
	private float? delta;

	// Token: 0x040017E4 RID: 6116
	private Vector2 defaultCenterOfMass;
}
