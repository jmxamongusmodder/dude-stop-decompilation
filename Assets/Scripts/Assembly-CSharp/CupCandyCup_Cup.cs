using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class CupCandyCup_Cup : Draggable
{
	// Token: 0x06001476 RID: 5238 RVA: 0x00037109 File Offset: 0x00035509
	private void Start()
	{
		this.body = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x00037118 File Offset: 0x00035518
	private void Update()
	{
		if (this.candies.Count >= this.requiredCandies)
		{
			foreach (Transform transform in this.candies)
			{
				if (!(transform == null))
				{
					transform.GetComponent<Rigidbody2D>().simulated = false;
					transform.GetComponent<PhysicsSound>().enable = false;
					transform.gameObject.layer = LayerMask.NameToLayer("Individual");
					transform.GetComponent<SpriteRenderer>().sortingLayerName = "Top";
					transform.GetComponent<SpriteRenderer>().sortingOrder = 5;
					transform.SetParent(base.transform);
				}
			}
			this.body.simulated = false;
			base.StartCoroutine(this.WallChangingCoroutine(1f));
			base.gameObject.layer = LayerMask.NameToLayer("Individual");
			Global.CupAcquired(base.transform);
		}
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x00037228 File Offset: 0x00035628
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		if (!this.dragged)
		{
			this.body.velocity = Vector3.zero;
		}
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x00037250 File Offset: 0x00035650
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "SuccessCollider")
		{
			return;
		}
		if (!this.wallRemoved)
		{
			base.StartCoroutine(this.WallChangingCoroutine(0f));
			this.wallRemoved = true;
		}
		if (base.enabled)
		{
			this.candies.Add(other.transform);
			Global.self.currPuzzle.GetComponent<AudioVoice_CupCandy>().getCandy();
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000372C7 File Offset: 0x000356C7
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "SuccessCollider")
		{
			return;
		}
		if (base.enabled)
		{
			this.candies.Remove(other.transform);
		}
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000372FC File Offset: 0x000356FC
	private IEnumerator WallChangingCoroutine(float target)
	{
		float timer = 0f;
		Color newColor = this.cupWall.color;
		float startAlpha = newColor.a;
		while (timer != this.wallMeltingTime)
		{
			timer = Mathf.MoveTowards(timer, this.wallMeltingTime, Time.deltaTime);
			newColor.a = Mathf.Lerp(startAlpha, target, timer / this.wallMeltingTime);
			this.cupWall.color = newColor;
			yield return null;
		}
		yield break;
	}

	// Token: 0x040011D7 RID: 4567
	public SpriteRenderer cupWall;

	// Token: 0x040011D8 RID: 4568
	public float wallMeltingTime = 1f;

	// Token: 0x040011D9 RID: 4569
	public float wallShowingTime = 0.5f;

	// Token: 0x040011DA RID: 4570
	public int requiredCandies = 20;

	// Token: 0x040011DB RID: 4571
	private bool wallRemoved;

	// Token: 0x040011DC RID: 4572
	private new Rigidbody2D body;

	// Token: 0x040011DD RID: 4573
	private List<Transform> candies = new List<Transform>();
}
