using System;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class BGLineAttacher : MonoBehaviour
{
	// Token: 0x06001456 RID: 5206 RVA: 0x00034F50 File Offset: 0x00033350
	private void OnValidate()
	{
		if (base.transform.parent != null)
		{
			Transform parent = base.transform.parent;
			while (parent.parent != null)
			{
				parent = parent.parent;
			}
			Debug.LogError("BGLines can't be on the scene or in any prefab. Remove BGLinesAttacher from: " + parent.name, parent.gameObject);
		}
	}
}
