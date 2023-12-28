using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class PuzzleEatingOthersFood_Plate : MonoBehaviour
{
	// Token: 0x060019EC RID: 6636 RVA: 0x00063236 File Offset: 0x00061636
	private void OnDrawGizmos()
	{
	}

	// Token: 0x060019ED RID: 6637 RVA: 0x00063238 File Offset: 0x00061638
	private void Start()
	{
	}

	// Token: 0x060019EE RID: 6638 RVA: 0x0006323A File Offset: 0x0006163A
	private void Update()
	{
		this.Raycast();
	}

	// Token: 0x060019EF RID: 6639 RVA: 0x00063244 File Offset: 0x00061644
	private void Raycast()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		List<Transform> list = new List<Transform>();
		float num = 0.15f;
		foreach (Vector2 vector in this.PreparePoints(base.transform, 0.15f, 5))
		{
			Debug.DrawRay(vector, base.transform.up * num, Color.magenta);
			RaycastHit2D[] array = Physics2D.RaycastAll(vector, base.transform.up, num, mask);
			foreach (RaycastHit2D raycastHit2D in array)
			{
				if (!list.Contains(raycastHit2D.transform))
				{
					list.Add(raycastHit2D.transform);
				}
			}
		}
		bool flag = false;
		foreach (Draggable draggable in this.GetComponentsInPuzzleStats(false))
		{
			if (draggable.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > this.minVelocity)
			{
				flag = true;
				break;
			}
		}
		if (!this.draggablesDisabled && flag)
		{
			return;
		}
		if (!this.draggablesDisabled)
		{
			foreach (Transform transform in list)
			{
				if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z % 180f, 0f)) <= 2f && !transform.GetComponent<Draggable>().IsDragged())
				{
					foreach (Draggable draggable2 in base.transform.parent.parent.GetComponentsInChildren<Draggable>())
					{
						draggable2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						draggable2.dragEnabled = false;
						this.draggablesDisabled = true;
					}
					break;
				}
			}
		}
		else if (this.waitTimer < this.waitTime)
		{
			this.waitTimer = Mathf.MoveTowards(this.waitTimer, this.waitTime, Time.deltaTime);
		}
		else
		{
			List<Transform> list2 = new List<Transform>();
			foreach (Transform sandwich in list)
			{
				this.CheckSandwich(list2, sandwich);
			}
			if (list2.Count > 1)
			{
				Global.LevelCompleted(0f, true);
				if (list2.Count == 3)
				{
				}
			}
			else if (list2.Count == 1)
			{
				if (list2[0].tag == "SuccessCollider")
				{
					Global.LevelCompleted(0f, true);
					this.ReleaseDraggables();
				}
				else if (list2[0].tag == "FailCollider")
				{
					Global.LevelFailed(0f, true);
					this.ReleaseDraggables();
				}
				base.StartCoroutine(this.SnappingCoroutine(list2[0]));
			}
		}
	}

	// Token: 0x060019F0 RID: 6640 RVA: 0x000635E8 File Offset: 0x000619E8
	private void CheckSandwich(List<Transform> sandwiches, Transform sandwich)
	{
		if (sandwiches.Contains(sandwich))
		{
			return;
		}
		sandwiches.Add(sandwich);
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		sandwich.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		float distance = 0.25f;
		foreach (Vector2 origin in this.PreparePoints(sandwich, 0.175f, 2))
		{
			RaycastHit2D[] array = Physics2D.RaycastAll(origin, sandwich.up, distance, mask);
			foreach (RaycastHit2D raycastHit2D in array)
			{
				this.CheckSandwich(sandwiches, raycastHit2D.transform);
			}
			array = Physics2D.RaycastAll(origin, -sandwich.up, distance, mask);
			foreach (RaycastHit2D raycastHit2D2 in array)
			{
				this.CheckSandwich(sandwiches, raycastHit2D2.transform);
			}
		}
		sandwich.gameObject.layer = LayerMask.NameToLayer("Front");
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0006374C File Offset: 0x00061B4C
	private List<Vector2> PreparePoints(Transform t, float dist, int raycasts)
	{
		List<Vector2> list = new List<Vector2>();
		list.Add(t.position);
		for (int i = 1; i <= raycasts; i++)
		{
			list.Add(t.position - dist * (float)i * t.right);
			list.Add(t.position + dist * (float)i * t.right);
		}
		return list;
	}

	// Token: 0x060019F2 RID: 6642 RVA: 0x000637D0 File Offset: 0x00061BD0
	private IEnumerator SnappingCoroutine(Transform sandwich)
	{
		for (;;)
		{
			Vector2 end = new Vector2(base.transform.position.x, sandwich.position.y);
			Vector2 newPos = Vector2.MoveTowards(sandwich.position, end, this.snapSpeed * Time.deltaTime);
			sandwich.position = newPos;
			yield return null;
		}
		yield break;
	}

	// Token: 0x060019F3 RID: 6643 RVA: 0x000637F4 File Offset: 0x00061BF4
	private void ReleaseDraggables()
	{
		foreach (Draggable draggable in this.GetComponentsInPuzzleStats(false))
		{
			draggable.OnMouseUp();
		}
	}

	// Token: 0x040017FA RID: 6138
	public float waitTime = 1f;

	// Token: 0x040017FB RID: 6139
	public float snapSpeed;

	// Token: 0x040017FC RID: 6140
	public float minVelocity;

	// Token: 0x040017FD RID: 6141
	private bool draggablesDisabled;

	// Token: 0x040017FE RID: 6142
	private float waitTimer;
}
