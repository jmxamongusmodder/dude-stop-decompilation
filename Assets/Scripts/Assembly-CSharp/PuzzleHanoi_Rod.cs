using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000417 RID: 1047
public class PuzzleHanoi_Rod : MonoBehaviour
{
	// Token: 0x06001A8D RID: 6797 RVA: 0x00068954 File Offset: 0x00066D54
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(base.transform.position - base.transform.up * 1.8f, base.transform.position + base.transform.up * 0.8f);
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x000689BC File Offset: 0x00066DBC
	private void Update()
	{
		this.Raycast();
		this.CheckDiscs();
		if (Global.self.DEBUG)
		{
			if (Input.GetKeyDown(KeyCode.Keypad4))
			{
				Global.self.GetCup(AwardName.HANOI_TOWER);
			}
			if (Input.GetKeyDown(KeyCode.Keypad5))
			{
				Global.self.GetCup(AwardName.HANOI_PERFECT);
			}
			if (Input.GetKeyDown(KeyCode.Keypad6))
			{
				Global.self.GetCup(AwardName.HANOI_CHEATING);
			}
		}
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x00068A38 File Offset: 0x00066E38
	private void CheckDiscs()
	{
		if (this.discs.Count < this.totalDiscs)
		{
			return;
		}
		int num = 0;
		bool flag = true;
		bool flag2 = true;
		int num2 = 999;
		foreach (Transform transform in this.discs)
		{
			PuzzleHanoi_Disc component = transform.GetComponent<PuzzleHanoi_Disc>();
			num += component.moves;
			flag &= component.IsCorrect();
			if (component.value > num2)
			{
				flag2 = false;
			}
			num2 = component.value;
		}
		if (!flag2)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			if (flag)
			{
				Global.self.GetCup(AwardName.HANOI_TOWER);
				if (num <= 31)
				{
					Global.self.GetCup(AwardName.HANOI_PERFECT);
					if (num < 10)
					{
						Global.self.GetCup(AwardName.HANOI_CHEATING);
					}
				}
			}
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x00068B44 File Offset: 0x00066F44
	private void Raycast()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		RaycastHit2D[] array = (from h in Physics2D.RaycastAll(base.transform.position - base.transform.up * 1.8f, base.transform.up, 2.6f, mask)
		orderby h.distance
		select h).ToArray<RaycastHit2D>();
		if (array.Length == this.discs.Count)
		{
			return;
		}
		this.discs.Clear();
		foreach (RaycastHit2D raycastHit2D in array)
		{
			this.discs.Add(raycastHit2D.collider.gameObject.transform);
		}
	}

	// Token: 0x040018AC RID: 6316
	public int totalDiscs = 5;

	// Token: 0x040018AD RID: 6317
	private List<Transform> discs = new List<Transform>();
}
