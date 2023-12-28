using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public class PuzzleBurger_Bun : MonoBehaviour
{
	// Token: 0x06001883 RID: 6275 RVA: 0x00056658 File Offset: 0x00054A58
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		float d = (!this.gotPickles) ? (this.castDistance - 0.2f) : this.castDistance;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.up * d);
	}

	// Token: 0x06001884 RID: 6276 RVA: 0x000566C3 File Offset: 0x00054AC3
	private void Update()
	{
		this.Raycast();
		this.CheckVictoryConditions();
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x000566D1 File Offset: 0x00054AD1
	public void RecordDistance(float dist)
	{
		if (this.recordedFoodDistance > this.foodWarningDistance)
		{
			return;
		}
		this.recordedFoodDistance += dist;
		if (this.recordedFoodDistance > this.foodWarningDistance)
		{
			this.GetComponentInPuzzleStats<AudioVoice_NoPickles>().onDistance();
		}
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x00056710 File Offset: 0x00054B10
	private void CheckVictoryConditions()
	{
		this.gotPickles = this.currentIngredients.Contains(this.pickles);
		if (this.gotPickles && this.currentIngredients.Contains(this.bun) && this.currentIngredients.Count == 2)
		{
			this.GetComponentInPuzzleStats<AudioVoice_NoPickles>().OnlyPickles();
			return;
		}
		int num = (!this.gotPickles) ? (this.ingredients.Count - 1) : this.ingredients.Count;
		bool flag = false;
		if (!this.currentIngredients.Contains(this.meat))
		{
			num--;
			flag = true;
		}
		if (this.currentIngredients.Count == num)
		{
			bool flag2 = false;
			bool flag3 = true;
			int num2 = 0;
			if (flag)
			{
				this.GetComponentInPuzzleStats<AudioVoice_NoPickles>().noMeat();
				return;
			}
			for (int i = 0; i < this.ingredients.Count; i++)
			{
				if (this.ingredients[i] == this.pickles && !this.gotPickles)
				{
					num2 = 1;
				}
				else
				{
					flag3 &= (this.currentIngredients[i - num2] == this.ingredients[i]);
					flag2 |= this.currentIngredients[i - num2].GetComponent<Draggable>().IsDragged();
				}
			}
			if (!flag3)
			{
				this.GetComponentInPuzzleStats<AudioVoice_NoPickles>().completelyWrong();
			}
			else if (flag3 && !flag2)
			{
				if (this.gotPickles)
				{
					Global.LevelCompleted(0f, true);
				}
				else
				{
					Global.LevelFailed(0f, true);
				}
			}
		}
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x000568C0 File Offset: 0x00054CC0
	private void Raycast()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		float distance = (!this.gotPickles) ? (this.castDistance - 0.2f) : this.castDistance;
		RaycastHit2D[] array = (from h in Physics2D.RaycastAll(base.transform.position, base.transform.up, distance, mask)
		orderby h.distance
		select h).ToArray<RaycastHit2D>();
		this.currentIngredients.Clear();
		foreach (RaycastHit2D raycastHit2D in array)
		{
			if (!raycastHit2D.collider.isTrigger)
			{
				if (!this.currentIngredients.Contains(raycastHit2D.collider.gameObject.transform))
				{
					this.currentIngredients.Add(raycastHit2D.collider.gameObject.transform);
				}
			}
		}
	}

	// Token: 0x04001676 RID: 5750
	public List<Transform> ingredients = new List<Transform>();

	// Token: 0x04001677 RID: 5751
	public Transform bun;

	// Token: 0x04001678 RID: 5752
	public Transform pickles;

	// Token: 0x04001679 RID: 5753
	public Transform meat;

	// Token: 0x0400167A RID: 5754
	public float castDistance = 1f;

	// Token: 0x0400167B RID: 5755
	public float foodWarningDistance = 500f;

	// Token: 0x0400167C RID: 5756
	private float recordedFoodDistance;

	// Token: 0x0400167D RID: 5757
	private List<Transform> currentIngredients = new List<Transform>();

	// Token: 0x0400167E RID: 5758
	private bool gotPickles;
}
