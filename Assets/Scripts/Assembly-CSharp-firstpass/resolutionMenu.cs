using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200057A RID: 1402
public class resolutionMenu : scrollMenuClass
{
	// Token: 0x06002042 RID: 8258 RVA: 0x0009E157 File Offset: 0x0009C557
	protected override void setList(int count)
	{
		this.list = Screen.resolutions;
		base.setList(this.list.Length);
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x0009E174 File Offset: 0x0009C574
	protected override void setListItem(Transform item, int index)
	{
		string textNoTranslation = string.Concat(new object[]
		{
			this.list[index].width,
			" x ",
			this.list[index].height,
			" @",
			this.list[index].refreshRate
		});
		item.GetComponent<LineTranslator>().setTextNoTranslation(textNoTranslation);
		item.name = index.ToString();
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x0009E208 File Offset: 0x0009C608
	public void bSelect()
	{
		if (EventSystem.current.currentSelectedGameObject.name.Length > 3)
		{
			return;
		}
		int num = int.Parse(EventSystem.current.currentSelectedGameObject.name);
		Resolution resolution = Screen.resolutions[num];
		Screen.SetResolution(resolution.width, resolution.height, Global.self.fullscreen, resolution.refreshRate);
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x0009E27A File Offset: 0x0009C67A
	public override void bBack()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.optionsMenu, Vector2.left, true);
	}

	// Token: 0x0400237F RID: 9087
	private Resolution[] list;
}
