using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200055D RID: 1373
public class mainMenu : AbstractUIScreen
{
	// Token: 0x06001F8B RID: 8075 RVA: 0x000977C9 File Offset: 0x00095BC9
	private void Awake()
	{
		Global.self.saveFileName = string.Empty;
		Global.self.isGameIntroActive = false;
		Audio.self.StartSoloSnapshot(MusicTypes.MenuMusic, true);
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x000977F4 File Offset: 0x00095BF4
	public override void Update()
	{
		base.Update();
		if (this.active && this.versionColor.a < 1f)
		{
			this.versionColor.a = this.versionColor.a + 0.1f;
			this.versionText.color = this.versionColor;
		}
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x0009784F File Offset: 0x00095C4F
	protected override void cancelPressed()
	{
		this.bExit();
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x00097857 File Offset: 0x00095C57
	private void hideText()
	{
		if (this.versionColor.a > 0f)
		{
			this.versionColor.a = this.versionColor.a - 0.1f;
			this.versionText.color = this.versionColor;
		}
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x00097896 File Offset: 0x00095C96
	public override void removeScreen()
	{
		base.CancelInvoke("hideText");
		base.removeScreen();
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x000978A9 File Offset: 0x00095CA9
	public override void setActive(bool active)
	{
		if (!active)
		{
			base.InvokeRepeating("hideText", 0f, 0.02f);
		}
		base.setActive(active);
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x000978D0 File Offset: 0x00095CD0
	public override void setScreen(Transform item)
	{
		this.versionColor = this.versionText.color;
		this.versionColor.a = 0f;
		this.versionText.color = this.versionColor;
		this.versionText.text = VersionControl.GetVersion() + " (" + VersionControl.GetBuildDate() + ")";
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x00097934 File Offset: 0x00095D34
	public void bPlay()
	{
		if (!this.active)
		{
			return;
		}
		if (SaveLoad.Loading)
		{
			Debug.LogError("Still loading SaveFiles");
			return;
		}
		if (SaveLoad.hasSaveFiles())
		{
			Global.self.makeNewLevel(Global.self.loadMenu, Vector2.right, true);
		}
		else
		{
			loadMenu.StartNewGame();
		}
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x00097990 File Offset: 0x00095D90
	public void bOptions()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.optionsMenu, Vector2.right, true);
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x000979B8 File Offset: 0x00095DB8
	public void bExit()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.exitMenu, Vector2.left, true);
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000979E0 File Offset: 0x00095DE0
	public void bCredits()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.makeNewLevel(Global.self.creditsMenu, Vector2.left, true);
	}

	// Token: 0x040022BF RID: 8895
	public Text versionText;

	// Token: 0x040022C0 RID: 8896
	private Color versionColor;
}
