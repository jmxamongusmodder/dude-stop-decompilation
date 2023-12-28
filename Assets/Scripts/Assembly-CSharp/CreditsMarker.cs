using System;
using UnityEngine;

// Token: 0x0200053C RID: 1340
public class CreditsMarker : MonoBehaviour
{
	// Token: 0x06001EA3 RID: 7843 RVA: 0x0008E678 File Offset: 0x0008CA78
	private void Awake()
	{
		RectTransform component = base.GetComponent<RectTransform>();
		RectTransform component2 = UnityEngine.Object.Instantiate<GameObject>(this.creditsPrefab, base.transform.parent).GetComponent<RectTransform>();
		component2.localScale = Vector3.one;
		component2.anchoredPosition = component.anchoredPosition;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040021D3 RID: 8659
	public GameObject creditsPrefab;
}
