using System;
using UnityEngine;

// Token: 0x02000525 RID: 1317
public class Console_ConfirmToExitMenu : MonoBehaviour
{
	// Token: 0x06001E49 RID: 7753 RVA: 0x000897E1 File Offset: 0x00087BE1
	public void bBack()
	{
		global::Console.self.mouseOverClick();
		global::Console.self.switchMenu(global::Console.self.pauseMenu);
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x00089804 File Offset: 0x00087C04
	public void bExitPack()
	{
		if (!global::Console.self.active)
		{
			return;
		}
		if (Global.self.currentLevelPack == 5)
		{
			Audio.self.StopMusic("9034fc39-bcf6-4bc0-acc9-a51016e48790");
			Audio.self.playOneShot(LevelVoice.getVoice(Voices.VoicePack06.CatDoor_EndCassette).guid.ToString(), 1f);
		}
		global::Console.self.mouseOverClick();
		global::Console.self.hideConsole();
		if (Global.self.currPuzzle != null)
		{
			if (Global.self.lastPackCompletionState == CompletionState.None)
			{
				AnalyticsComponent.PuzzleAborted(Global.self.currPuzzle.name, Global.self.currentLevelPack);
			}
			else
			{
				AnalyticsComponent.CupPuzzleAborted(Global.self.currPuzzle.name);
			}
			Global.TellAnalyticsLevelAborted();
		}
		else
		{
			Debug.LogWarning(string.Concat(new string[]
			{
				"Aborted puzzle, but no currentPuzzle? Tell Patomkin!\n",
				Global.self.currPuzzle.name,
				"\n",
				Global.self.lastPlayed.name,
				"\n",
				Global.self.getNextPuzzleToChangeVoiceParent().name
			}));
		}
		Global.self.previousPuzzleSolvedAsMonster = null;
		Global.self.endScrollablePack();
		UIControl.self.endCompletionPack();
		UIControl.self.SetSubtitlesYellow(false);
		InventoryControl.self.removeInventory();
		Audio.self.stopAllVoices();
		if (Global.self.isGameIntroActive)
		{
			Audio.self.StopMusic("757e3a0a-c20a-4728-ab16-74dc9cf91a6b");
			Global.self.makeNewLevel(Global.self.mainMenu, Vector2.left, true);
		}
		else
		{
			Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.left, true);
		}
	}
}
