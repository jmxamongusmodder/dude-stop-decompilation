using System;
using UnityEngine;

// Token: 0x0200055E RID: 1374
public class newVersionMenu : AbstractUIScreen
{
	// Token: 0x06001F97 RID: 8087 RVA: 0x00097A10 File Offset: 0x00095E10
	public override void setScreen(Transform item)
	{
		this.errorText.gameObject.SetActive(false);
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x00097A23 File Offset: 0x00095E23
	public void bExit()
	{
		if (!this.active)
		{
			return;
		}
		Application.Quit();
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x00097A36 File Offset: 0x00095E36
	public void bSkip()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.right, true);
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x00097A5E File Offset: 0x00095E5E
	public void bGoto()
	{
		if (!this.active)
		{
			return;
		}
		Application.OpenURL("http://www.patomkin.com");
		Application.Quit();
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x00097A7B File Offset: 0x00095E7B
	protected override void cancelPressed()
	{
	}

	// Token: 0x040022C1 RID: 8897
	public Transform skipButton;

	// Token: 0x040022C2 RID: 8898
	public Transform errorText;
}
