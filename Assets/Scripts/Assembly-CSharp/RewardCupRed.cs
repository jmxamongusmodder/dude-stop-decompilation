using System;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class RewardCupRed : EnhancedDraggable
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060015AF RID: 5551 RVA: 0x0004338C File Offset: 0x0004178C
	// (set) Token: 0x060015B0 RID: 5552 RVA: 0x00043394 File Offset: 0x00041794
	private int stack
	{
		get
		{
			return this._stack;
		}
		set
		{
			if (this.cover != null)
			{
				this.cover.GetComponent<RewardCupRed>().stack = value + 1;
			}
			this._stack = value;
		}
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000433C1 File Offset: 0x000417C1
	private void Start()
	{
		this.sprite = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x000433CF File Offset: 0x000417CF
	private void Update()
	{
		if (!this.sprite)
		{
			return;
		}
		this.Raycast();
		this.sprite.sortingOrder = this.stack;
	}

	// Token: 0x060015B3 RID: 5555 RVA: 0x000433F9 File Offset: 0x000417F9
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		this.GetPuzzleStats().GetComponent<AudioVoice_CupSmallest>().touch(base.transform.name);
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x0004341C File Offset: 0x0004181C
	private void OnTriggerEnter2D()
	{
		if (!base.enabled)
		{
			this.Update();
		}
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x0004342F File Offset: 0x0004182F
	private void OnTriggerStay2D()
	{
		if (!base.enabled)
		{
			this.Update();
		}
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x00043444 File Offset: 0x00041844
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Vector3 b = new Vector3(0.3f * base.transform.up.y, -0.3f * base.transform.up.x, 0f);
		Gizmos.DrawLine(base.transform.position + base.transform.up + b, base.transform.position + base.transform.up * 1.2f + b);
		Gizmos.DrawLine(base.transform.position + base.transform.up - b, base.transform.position + base.transform.up * 1.2f - b);
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x0004353C File Offset: 0x0004193C
	private void Raycast()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		LayerMask mask = 1 << LayerMask.NameToLayer("UI");
		bool flag = false;
		Vector3 b = new Vector3(0.3f * base.transform.up.y, -0.3f * base.transform.up.x, 0f);
		RaycastHit2D[] array = Physics2D.RaycastAll(base.transform.position + base.transform.up + b, base.transform.up, 0.2f, mask);
		Transform transform = null;
		foreach (RaycastHit2D raycastHit2D in array)
		{
			if (!raycastHit2D.collider || Vector3.Angle(base.transform.up, raycastHit2D.transform.up) > 90f)
			{
				return;
			}
			transform = raycastHit2D.transform;
			if (this.cover != null && transform == this.cover)
			{
				flag = true;
			}
		}
		if (transform == null)
		{
			array = Physics2D.RaycastAll(base.transform.position + base.transform.up - b, base.transform.up, 0.2f, mask);
			foreach (RaycastHit2D raycastHit2D2 in array)
			{
				if (!raycastHit2D2.collider || Vector3.Angle(base.transform.up, raycastHit2D2.transform.up) > 90f)
				{
					return;
				}
				transform = raycastHit2D2.transform;
				if (this.cover != null && transform == this.cover)
				{
					flag = true;
				}
			}
		}
		if (!flag)
		{
			if (this.cover != null)
			{
				this.cover.GetComponent<RewardCupRed>().stack -= this.stack + 1;
			}
			this.cover = transform;
			if (transform != null)
			{
				transform.GetComponent<RewardCupRed>().stack = this.stack + 1;
			}
		}
		base.gameObject.layer = LayerMask.NameToLayer("UI");
	}

	// Token: 0x0400136D RID: 4973
	private int _stack;

	// Token: 0x0400136E RID: 4974
	private Transform cover;

	// Token: 0x0400136F RID: 4975
	private SpriteRenderer sprite;
}
