using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class loadingMenu : AbstractUIScreen
{
	// Token: 0x06001F7C RID: 8060 RVA: 0x00096E9C File Offset: 0x0009529C
	public override void Update()
	{
		if (this.canExit && Input.anyKeyDown)
		{
			this.bContinue();
			this.canExit = false;
		}
		if (this.active && this.animationEnded && !this.hintShowen)
		{
			this.hintShowen = true;
			base.StartCoroutine(base.showHintIcon(this.hintIcon, delegate
			{
				this.canExit = true;
			}));
		}
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x00096F12 File Offset: 0x00095312
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (!active)
		{
			return;
		}
		base.StartCoroutine(this.showConsole());
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x00096F30 File Offset: 0x00095330
	private IEnumerator showConsole()
	{
		global::Console.self.showConsole(global::Console.self.gameLoading);
		if (!Global.self.firstTimeLoadingGame)
		{
			yield return new WaitForSeconds(this.waitOnSecondLoad);
			AudioVoice_loadingScreen av = Global.self.currPuzzle.GetComponent<AudioVoice_loadingScreen>();
			while (av.isPlaying)
			{
				yield return null;
			}
			this.animationEnded = true;
		}
		else
		{
			UIControl.self.showSubtitles = true;
			SaveLoad.setInt("Subtitles", 1);
		}
		yield break;
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x00096F4B File Offset: 0x0009534B
	public override void setScreen(Transform item)
	{
		this.hintIcon.gameObject.SetActive(false);
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x00096F5E File Offset: 0x0009535E
	public void bContinue()
	{
		global::Console.self.hideConsole();
		Global.self.makeNewLevel(Global.self.mainMenu, Vector2.down, true);
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x00096F84 File Offset: 0x00095384
	protected override void cancelPressed()
	{
	}

	// Token: 0x040022B6 RID: 8886
	public float waitOnSecondLoad = 2f;

	// Token: 0x040022B7 RID: 8887
	private bool animationEnded;

	// Token: 0x040022B8 RID: 8888
	public RectTransform hintIcon;

	// Token: 0x040022B9 RID: 8889
	private bool hintShowen;

	// Token: 0x040022BA RID: 8890
	private bool canExit;
}
