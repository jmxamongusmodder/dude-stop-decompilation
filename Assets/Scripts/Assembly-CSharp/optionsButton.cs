using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000563 RID: 1379
public class optionsButton : MonoBehaviour
{
	// Token: 0x06001FAD RID: 8109 RVA: 0x0009865C File Offset: 0x00096A5C
	public void bSelect()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<ISelectHandler>(this.secondButton, eventData, ExecuteEvents.selectHandler);
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x00098688 File Offset: 0x00096A88
	public void bDeselect()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<IDeselectHandler>(this.secondButton, eventData, ExecuteEvents.deselectHandler);
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x000986B4 File Offset: 0x00096AB4
	public void bSubmit()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<ISubmitHandler>(this.secondButton, eventData, ExecuteEvents.submitHandler);
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x000986E0 File Offset: 0x00096AE0
	public void bEnter()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<IPointerEnterHandler>(this.secondButton, eventData, ExecuteEvents.pointerEnterHandler);
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x0009870C File Offset: 0x00096B0C
	public void bExit()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		ExecuteEvents.Execute<IPointerExitHandler>(this.secondButton, eventData, ExecuteEvents.pointerExitHandler);
	}

	// Token: 0x040022D7 RID: 8919
	public GameObject secondButton;
}
