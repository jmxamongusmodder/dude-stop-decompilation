using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000577 RID: 1399
public class pauseMenu : AbstractUIScreen
{
	// Token: 0x06002032 RID: 8242 RVA: 0x0009DD34 File Offset: 0x0009C134
	public override void Update()
	{
		base.Update();
		if (Input.GetButtonDown("Vertical") && this.active && EventSystem.current.currentSelectedGameObject == null)
		{
			EventSystem.current.SetSelectedGameObject(this.buttonList.GetChild(0).gameObject);
		}
	}

	// Token: 0x06002033 RID: 8243 RVA: 0x0009DD91 File Offset: 0x0009C191
	protected override void cancelPressed()
	{
		this.bContinue();
	}

	// Token: 0x06002034 RID: 8244 RVA: 0x0009DD99 File Offset: 0x0009C199
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.unpauseGame();
	}

	// Token: 0x06002035 RID: 8245 RVA: 0x0009DDB1 File Offset: 0x0009C1B1
	public void bOptions()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.optionsMenu, Vector2.right, true);
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x0009DDDC File Offset: 0x0009C1DC
	public void bAbort()
	{
		if (!this.active)
		{
			return;
		}
		if (Global.self.lastPlayed != null)
		{
		}
		Global.self.previousPuzzleSolvedAsMonster = null;
		Global.self.endScrollablePack();
		UIControl.self.endCompletionPack();
		InventoryControl.self.removeInventory();
		Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.down, true);
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x0009DE65 File Offset: 0x0009C265
	public override void setScreen(Transform item)
	{
	}

	// Token: 0x0400237A RID: 9082
	public Transform buttonList;
}
