using System;
using UnityEngine;

// Token: 0x02000527 RID: 1319
public class Console_PauseMenu : MonoBehaviour
{
	// Token: 0x06001E56 RID: 7766 RVA: 0x00089CAD File Offset: 0x000880AD
	public void bCloseConsole()
	{
		global::Console.self.mouseOverClick();
		global::Console.self.hideConsole();
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x00089CC3 File Offset: 0x000880C3
	public void bAudioMenu()
	{
		global::Console.self.mouseOverClick();
		global::Console.self.switchMenu(global::Console.self.audioMenu);
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x00089CE3 File Offset: 0x000880E3
	public void bExit()
	{
		global::Console.self.mouseOverClick();
		global::Console.self.switchMenu(global::Console.self.confirmExitMenu);
	}
}
