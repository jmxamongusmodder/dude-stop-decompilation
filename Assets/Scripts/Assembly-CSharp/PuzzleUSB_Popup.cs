using System;
using System.Collections;
using ExcelData;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000463 RID: 1123
public class PuzzleUSB_Popup : MonoBehaviour
{
	// Token: 0x06001CE0 RID: 7392 RVA: 0x0007D338 File Offset: 0x0007B738
	private void Awake()
	{
		if (UIControl.self.useBackupFont)
		{
			this.text.font = UIControl.self.backupFont;
			this.text.fontSize = Mathf.RoundToInt((float)this.text.fontSize * UIControl.self.SmallFontScale);
			this.text.lineSpacing = 1f;
		}
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x0007D3A0 File Offset: 0x0007B7A0
	private void OnMouseDown()
	{
		if (!this.shown || !base.enabled)
		{
			return;
		}
		this.drive.RemovedCorrectly();
		if (!this.removedCorreclty)
		{
			string text = string.Empty;
			if (this.messageIndex <= 1)
			{
				text = LineTranslator.translateText("USB_MESSAGE_REMOVED_RIGHT", WordTranslationContainer.Theme.PUZZLE, true, string.Empty);
			}
			else
			{
				text = LineTranslator.translateText("USB_MESSAGE_REMOVED_AFTER", WordTranslationContainer.Theme.PUZZLE, true, string.Empty);
			}
			this.text.text = text;
			this.removedCorreclty = true;
			this.hideMessageAfterTime();
			Audio.self.playOneShot("22de8ed3-45ec-4ab0-b2d4-3f5950abe3d2", 1f);
		}
		else
		{
			this.Close();
		}
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x0007D44E File Offset: 0x0007B84E
	public void Close()
	{
		Audio.self.playOneShot("22de8ed3-45ec-4ab0-b2d4-3f5950abe3d2", 1f);
		this.SetChildrenTo(false);
		this.shown = false;
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x0007D474 File Offset: 0x0007B874
	public bool Next()
	{
		this.icon.enabled = false;
		string value = LineTranslator.translateText("USB_MESSAGE_" + this.messageIndex.ToString(), WordTranslationContainer.Theme.PUZZLE, true, string.Empty);
		if (!this.lastShowen && string.IsNullOrEmpty(value))
		{
			value = LineTranslator.translateText("USB_MESSAGE_END", WordTranslationContainer.Theme.PUZZLE, true, string.Empty);
			this.lastShowen = true;
			this.hideMessageAfterTime();
		}
		if (!string.IsNullOrEmpty(value))
		{
			if (this.messageIndex == 1)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_USB>().playLine();
			}
			this.shown = true;
			this.text.text = value;
			this.messageIndex++;
			Audio.self.playOneShot("9344b1c1-c217-41a8-9f9e-374ea530d566", 1f);
			this.SetChildrenTo(true);
			return !this.lastShowen;
		}
		return false;
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x0007D55D File Offset: 0x0007B95D
	private void hideMessageAfterTime()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.removeAfter);
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x0007D570 File Offset: 0x0007B970
	private void SetChildrenTo(bool active)
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(active);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x04001B76 RID: 7030
	public PuzzleUSB_Drive drive;

	// Token: 0x04001B77 RID: 7031
	public PuzzleUSB_TrayIcon icon;

	// Token: 0x04001B78 RID: 7032
	[HideInInspector]
	public bool shown;

	// Token: 0x04001B79 RID: 7033
	public Text text;

	// Token: 0x04001B7A RID: 7034
	public float removeAfter = 3f;

	// Token: 0x04001B7B RID: 7035
	private int messageIndex;

	// Token: 0x04001B7C RID: 7036
	private bool lastShowen;

	// Token: 0x04001B7D RID: 7037
	private bool removedCorreclty;
}
