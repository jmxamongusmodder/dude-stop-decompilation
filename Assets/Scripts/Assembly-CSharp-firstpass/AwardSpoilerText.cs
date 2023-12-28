using System;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000332 RID: 818
[RequireComponent(typeof(Text))]
public class AwardSpoilerText : MonoBehaviour
{
	// Token: 0x06001424 RID: 5156 RVA: 0x00033A08 File Offset: 0x00031E08
	private void Awake()
	{
		Text component = base.GetComponent<Text>();
		int num = Mathf.RoundToInt(Time.time / 60f);
		if (UIControl.self.useBackupFont)
		{
			component.font = UIControl.self.backupFont;
			component.fontSize = Mathf.RoundToInt((float)component.fontSize * UIControl.self.BigFontScale);
		}
		if (Global.self.lastSpoilerCupTextID == string.Empty || num > Global.self.lastSpoilerCupUpdateTime + this.minToUpdateText)
		{
			string lastSpoilerCupTextID = Global.self.lastSpoilerCupTextID;
			string randomGuid;
			if (this.useLineGUID == string.Empty)
			{
				int num2 = 0;
				do
				{
					randomGuid = WordTranslationContainer.GetRandomGuid(this.type, Global.self.currLanguage);
					num2++;
				}
				while (lastSpoilerCupTextID == randomGuid && num2 < 10);
			}
			else
			{
				randomGuid = this.useLineGUID;
			}
			Global.self.lastSpoilerCupTextID = randomGuid.ToUpper();
			Global.self.lastSpoilerCupUpdateTime = num;
		}
		component.text = WordTranslationContainer.Get(this.type, Global.self.lastSpoilerCupTextID, Global.self.currLanguage);
	}

	// Token: 0x04001144 RID: 4420
	[Tooltip("Empty - use random on Awake, or specify to use line all the time")]
	public string useLineGUID = string.Empty;

	// Token: 0x04001145 RID: 4421
	[Tooltip("Theme of lines to show")]
	public WordTranslationContainer.Theme type;

	// Token: 0x04001146 RID: 4422
	[Tooltip("How much minutes to show each line for")]
	public int minToUpdateText = 15;
}
