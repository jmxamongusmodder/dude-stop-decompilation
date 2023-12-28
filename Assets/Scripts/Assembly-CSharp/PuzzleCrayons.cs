using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EE RID: 1006
public class PuzzleCrayons : MonoBehaviour
{
	// Token: 0x06001965 RID: 6501 RVA: 0x0005E3C0 File Offset: 0x0005C7C0
	private void Update()
	{
		if (this.crayonsInside.Count != this.crayons.Count)
		{
			return;
		}
		bool flag = false;
		foreach (Transform transform in this.crayons)
		{
			flag |= transform.GetComponent<Draggable>().IsDragged();
		}
		if (flag)
		{
			return;
		}
		float num = -999f;
		bool flag2 = true;
		for (int i = 0; i < this.crayons.Count; i++)
		{
			if (this.crayons[i].position.x > num)
			{
				num = this.crayons[i].position.x;
			}
			else
			{
				flag2 = false;
			}
		}
		this.CheckCrayonRotation();
		if (flag2)
		{
			Global.LevelFailed(0f, true);
		}
		else
		{
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x0005E4E0 File Offset: 0x0005C8E0
	private void CheckCrayonRotation()
	{
		bool flag = true;
		foreach (Transform transform in this.crayonsInside)
		{
			flag &= (Mathf.DeltaAngle(transform.eulerAngles.z, 180f) < 10f);
		}
		if (flag && Global.self.GetCup(AwardName.WHITE_CRAYONS))
		{
			this.GetPuzzleStats().GetComponent<AudioVoiceEndAchievement>().getTrophy();
		}
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x0005E580 File Offset: 0x0005C980
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!this.crayonsInside.Contains(other.transform))
		{
			this.crayonsInside.Add(other.transform);
		}
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x0005E5A9 File Offset: 0x0005C9A9
	private void OnTriggerExit2D(Collider2D other)
	{
		this.crayonsInside.Remove(other.transform);
	}

	// Token: 0x04001767 RID: 5991
	[Tooltip("Crayons in the list must be in correct order, from left to right")]
	public List<Transform> crayons = new List<Transform>();

	// Token: 0x04001768 RID: 5992
	private List<Transform> crayonsInside = new List<Transform>();
}
