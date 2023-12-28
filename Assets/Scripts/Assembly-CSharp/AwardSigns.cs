using System;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class AwardSigns : MonoBehaviour
{
	// Token: 0x0600141F RID: 5151 RVA: 0x00033890 File Offset: 0x00031C90
	private void Awake()
	{
		foreach (Transform transform in this.showOnChanceSingle)
		{
			transform.gameObject.SetActive(false);
		}
		if (UnityEngine.Random.Range(0f, 1f) <= this.chanceToSpawn)
		{
			foreach (Transform transform2 in this.showOnChance)
			{
				transform2.gameObject.SetActive(true);
			}
			this.showOnChanceSingle[UnityEngine.Random.Range(0, this.showOnChanceSingle.Length)].gameObject.SetActive(true);
		}
	}

	// Token: 0x0400113C RID: 4412
	public float chanceToSpawn;

	// Token: 0x0400113D RID: 4413
	public Transform[] showOnChance;

	// Token: 0x0400113E RID: 4414
	public Transform[] showOnChanceSingle;
}
