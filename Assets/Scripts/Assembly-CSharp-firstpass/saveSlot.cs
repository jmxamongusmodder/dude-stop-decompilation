using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200057C RID: 1404
public class saveSlot : MonoBehaviour
{
	// Token: 0x0600204F RID: 8271 RVA: 0x0009E7D8 File Offset: 0x0009CBD8
	public void setActive(string saveFileName, loadMenu parent)
	{
		this.parent = parent;
		this.saveFileName = saveFileName;
		SaveData saveData = SaveLoad.Load(saveFileName);
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<AwardName, CupStatus> keyValuePair in saveData.cupList)
		{
			if (!Global.self.awardsToSkip.Contains(keyValuePair.Key))
			{
				num++;
				if (keyValuePair.Value != CupStatus.Empty)
				{
					num2++;
				}
			}
		}
		int num3 = Global.self.levelPackMenu.Length - 1;
		int num4 = 0;
		int num5 = 0;
		foreach (SerializablePackSavedStats serializablePackSavedStats in saveData.packSavedStats)
		{
			if (serializablePackSavedStats.completedTimes > 0)
			{
				int num6 = Global.self.levelPackMenu[Mathf.Max(0, num4 - 1)].GetComponent<levelPackControl>().packIndex;
				if (num6 != 11 || serializablePackSavedStats.solvedAsBad != 0)
				{
					num4++;
				}
				if (serializablePackSavedStats.solvedAsBad > 0 || serializablePackSavedStats.solvedAsGood > 0)
				{
					if (num6 == 1)
					{
						num6 = 2;
					}
					num5 = Mathf.Max(num5, num6);
				}
			}
		}
		this.isGameIntro = (num4 == 0);
		num5 = Mathf.Clamp(num5, 0, Global.self.levelPackMenu.Length - 1);
		this.lastPlayedPack = Mathf.Min(num5, Global.self.levelPackMenu.Length - 2);
		levelPackControl component = Global.self.levelPackMenu[num5].GetComponent<levelPackControl>();
		this.title.setTextNoTranslation(component.translateHeader(false));
		this.title.GetComponent<Text>().color = component.headerColor;
		this.deleteButton.setTextNoTranslation("X");
		this.cupProgress.value = (float)num2 / (float)num;
		this.packProgress.value = (float)num4 / (float)num3;
		this.bannerCups.SetActive(false);
		this.bannerPacks.SetActive(false);
		if (this.cupProgress.value == 1f)
		{
			this.bannerCups.SetActive(true);
		}
		else if (this.packProgress.value == 1f)
		{
			this.bannerPacks.SetActive(true);
		}
		string shortTimePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
		string shortDatePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
		this.lastTime.text = saveData.lastPlayed.ToString(shortTimePattern) + " " + saveData.lastPlayed.ToString(shortDatePattern);
		saveData = null;
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x0009EABC File Offset: 0x0009CEBC
	public void bDelete()
	{
		if (!this.parent.isActive())
		{
			return;
		}
		if (this.deleteClicked)
		{
			if (this.deleteCanConfirm)
			{
				Audio.self.playOneShot("66629bbc-5d75-4fde-b1ea-7bd59406962d", 1f);
				this.startButton.GetComponent<ButtonTemplate>().setActive(false);
				this.deleteButton.GetComponent<ButtonTemplate>().setActive(false);
				this.deleteButton.setTextNoTranslation("X");
				this.title.GetComponent<Text>().color = new Color(0.6f, 0.6f, 0.6f);
				this.packProgress.fillRect.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
				this.cupProgress.fillRect.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
				this.removed = true;
				File.Delete(SaveLoad.filePath(this.saveFileName));
			}
		}
		else
		{
			this.deleteButton.setTextToTranslate("DELETE", WordTranslationContainer.Theme.MENU);
			this.deleteClicked = true;
			base.StartCoroutine(this.hideDelete());
		}
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x0009EBF0 File Offset: 0x0009CFF0
	private IEnumerator hideDelete()
	{
		yield return new WaitForSeconds(0.5f);
		this.deleteCanConfirm = true;
		yield return new WaitForSeconds(2.5f);
		if (!this.removed)
		{
			this.deleteClicked = false;
			this.deleteCanConfirm = false;
			this.deleteButton.setTextNoTranslation("X");
		}
		yield break;
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x0009EC0C File Offset: 0x0009D00C
	public void bStart()
	{
		if (!this.parent.isActive())
		{
			return;
		}
		Global.self.LoadGame(this.saveFileName);
		Global.self.isGameIntroActive = (this.isGameIntro && !SerializableGameStats.self.isGameFinished);
		if (Global.self.isGameIntroActive)
		{
			Audio.self.StartSoloSnapshot(MusicTypes.NoMusic, true);
		}
		if (SerializableGameStats.self.isGameFinishedJustNow)
		{
			Global.self.currentLevelPack = 0;
		}
		else
		{
			Global.self.currentLevelPack = this.lastPlayedPack;
		}
		Global.self.makeNewLevel(Global.self.levelPackMenu[Global.self.currentLevelPack], Vector2.right, true);
	}

	// Token: 0x04002395 RID: 9109
	private loadMenu parent;

	// Token: 0x04002396 RID: 9110
	private string saveFileName;

	// Token: 0x04002397 RID: 9111
	public LineTranslator title;

	// Token: 0x04002398 RID: 9112
	public Text lastTime;

	// Token: 0x04002399 RID: 9113
	public Slider packProgress;

	// Token: 0x0400239A RID: 9114
	public Slider cupProgress;

	// Token: 0x0400239B RID: 9115
	public LineTranslator deleteButton;

	// Token: 0x0400239C RID: 9116
	public Button startButton;

	// Token: 0x0400239D RID: 9117
	public GameObject bannerPacks;

	// Token: 0x0400239E RID: 9118
	public GameObject bannerCups;

	// Token: 0x0400239F RID: 9119
	private bool deleteClicked;

	// Token: 0x040023A0 RID: 9120
	private bool deleteCanConfirm;

	// Token: 0x040023A1 RID: 9121
	private bool removed;

	// Token: 0x040023A2 RID: 9122
	private bool isGameIntro;

	// Token: 0x040023A3 RID: 9123
	private int lastPlayedPack;
}
