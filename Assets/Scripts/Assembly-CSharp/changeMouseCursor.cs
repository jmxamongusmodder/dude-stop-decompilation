using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200051D RID: 1309
public class changeMouseCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06001E03 RID: 7683 RVA: 0x00087502 File Offset: 0x00085902
	public void OnPointerEnter(PointerEventData eventData)
	{
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x00087510 File Offset: 0x00085910
	public void OnPointerExit(PointerEventData eventData)
	{
		if (base.enabled)
		{
			Cursor.SetCursor(this.cursor, Vector2.zero, CursorMode.Auto);
		}
	}

	// Token: 0x04002148 RID: 8520
	public Texture2D cursor;
}
