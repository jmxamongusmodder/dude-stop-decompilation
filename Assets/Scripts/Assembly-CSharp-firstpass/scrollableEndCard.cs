using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000584 RID: 1412
public class scrollableEndCard : AbstractUIScreen
{
	// Token: 0x0600206B RID: 8299 RVA: 0x0009F2FB File Offset: 0x0009D6FB
	protected override void cancelPressed()
	{
		this.bBack();
	}

	// Token: 0x0600206C RID: 8300 RVA: 0x0009F303 File Offset: 0x0009D703
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
		Global.self.stopScrollablePack();
		this.checkPackStatus();
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x0009F31C File Offset: 0x0009D71C
	public override void removeScreen()
	{
		base.removeScreen();
		UnityEngine.Object.Destroy(this.puzzle.gameObject);
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x0009F334 File Offset: 0x0009D734
	public void bBack()
	{
		if (!this.active || this.exitInProcess)
		{
			return;
		}
		if (!this.puzzle.GetComponent<AudioVoice_ScrollableEnd>().canExit)
		{
			return;
		}
		Global.self.scrollableEndReached = false;
		Global.self.gotoNextLevel(true, null);
		Global.self.scrollableUI.gameObject.SetActive(true);
		InventoryControl.self.turnOnMouseInteractions();
	}

	// Token: 0x0600206F RID: 8303 RVA: 0x0009F3AC File Offset: 0x0009D7AC
	public void bNext()
	{
		if (!this.active)
		{
			return;
		}
		if (AudioVoice_ScrollableController.self != null)
		{
			base.StartCoroutine(this.pressNextAfterDelay());
			return;
		}
		if (Global.self.pack10CutsceneActive)
		{
			return;
		}
		if (!this.puzzle.GetComponent<AudioVoice_ScrollableEnd>().canExit)
		{
			return;
		}
		UIControl.self.calculateCompletionProgress();
		Global.self.endScrollablePack();
		UIControl.self.endCompletionPack();
		Global.self.gotoNextLevel(false, null);
	}

	// Token: 0x06002070 RID: 8304 RVA: 0x0009F43C File Offset: 0x0009D83C
	private IEnumerator pressNextAfterDelay()
	{
		global::Console.self.canOpen = false;
		this.next.GetComponent<ButtonTemplate>().soundSubmit();
		this.next.GetComponent<ButtonTemplate>().setActive(false);
		this.back.GetComponent<ButtonTemplate>().setActive(false);
		this.exitInProcess = true;
		while (AudioVoice_ScrollableController.self != null)
		{
			yield return null;
		}
		global::Console.self.canOpen = true;
		this.bNext();
		yield break;
	}

	// Token: 0x06002071 RID: 8305 RVA: 0x0009F458 File Offset: 0x0009D858
	private void checkPackStatus()
	{
		bool flag = this.copyCompletion();
		bool flag2 = false;
		if (InventoryControl.self.transform.childCount > 0)
		{
			this.copyInventory();
			flag2 = true;
		}
		this.completionGoodText.SetActive(false);
		this.completionBadText.SetActive(false);
		this.warningTitle.SetActive(false);
		this.normalTitle.SetActive(false);
		this.inventoryContainer.SetActive(false);
		this.completionHasItemsText.SetActive(false);
		this.next.gameObject.SetActive(false);
		Vector3 v = this.back.GetComponent<RectTransform>().anchoredPosition;
		float x = v.x;
		v.x = 0f;
		if (flag2)
		{
			this.inventoryContainer.SetActive(true);
			this.warningTitle.SetActive(true);
		}
		if (flag)
		{
			if (!flag2)
			{
				this.completionGoodText.SetActive(true);
				this.normalTitle.SetActive(true);
				this.next.gameObject.SetActive(true);
				this.setNextButton();
				v.x = x;
			}
			else
			{
				this.completionHasItemsText.SetActive(true);
			}
		}
		else
		{
			this.completionBadText.SetActive(true);
			this.warningTitle.SetActive(true);
		}
		this.back.GetComponent<RectTransform>().anchoredPosition = v;
		this.puzzle.GetComponent<AudioVoice_ScrollableEnd>().setCompletion(flag, !flag2);
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x0009F5C8 File Offset: 0x0009D9C8
	private bool copyCompletion()
	{
		List<CompletionState> completionList = UIControl.self.getCompletionList();
		bool result = true;
		int num = 0;
		int num2 = 0;
		float iconSize = UIControl.self.completionLine.iconSize;
		this.completionIcon.gameObject.SetActive(false);
		foreach (CompletionState completionState in completionList)
		{
			this.completionIconBad.SetActive(false);
			this.completionIconGood.SetActive(false);
			this.completionIconNone.SetActive(false);
			if (completionState == CompletionState.Monster)
			{
				this.completionIconBad.SetActive(true);
				num2++;
			}
			else if (completionState == CompletionState.Good)
			{
				this.completionIconGood.SetActive(true);
			}
			else
			{
				this.completionIconNone.SetActive(true);
				result = false;
			}
			RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.completionIcon);
			rectTransform.SetParent(this.completionParent);
			rectTransform.localScale = Vector3.one;
			rectTransform.gameObject.SetActive(true);
			num++;
		}
		Vector3 v = this.completionParent.sizeDelta;
		v.x = (float)num * iconSize;
		this.completionParent.sizeDelta = v;
		this.puzzle.GetComponent<AudioVoice_ScrollableEnd>().setBadProgress((float)num2 / (float)num);
		return result;
	}

	// Token: 0x06002073 RID: 8307 RVA: 0x0009F734 File Offset: 0x0009DB34
	private void copyInventory()
	{
		InventoryControl.self.hideInventory();
		Transform transform = null;
		IEnumerator enumerator = this.puzzle.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				if (transform2.tag == "Respawn")
				{
					transform = transform2;
				}
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
		Transform child = transform.GetChild(0);
		child.gameObject.SetActive(false);
		int num = 0;
		float slotSize = InventoryControl.self.slotSize;
		IEnumerator enumerator2 = InventoryControl.self.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				Transform transform3 = (Transform)obj2;
				Transform transform4 = UnityEngine.Object.Instantiate<Transform>(child);
				transform4.SetParent(transform);
				transform4.localScale = Vector3.one;
				transform4.localPosition = Vector3.zero + Vector3.right * slotSize * (float)num;
				transform4.gameObject.SetActive(true);
				InventoryItem componentInChildren = transform3.GetComponentInChildren<InventoryItem>();
				Transform transform5 = UnityEngine.Object.Instantiate<Transform>(componentInChildren.itemSprite);
				transform5.SetParent(transform4);
				transform5.localScale = Vector3.one * componentInChildren.scale;
				transform5.localPosition = componentInChildren.itemSprite.localPosition;
				transform5.localRotation = componentInChildren.itemSprite.rotation;
				num++;
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		Vector3 localPosition = transform.localPosition;
		localPosition.x = (float)(-(float)num) * slotSize / 2f + slotSize * 0.5f;
		transform.localPosition = localPosition;
	}

	// Token: 0x06002074 RID: 8308 RVA: 0x0009F914 File Offset: 0x0009DD14
	public void setNextButton()
	{
		if (this.exitInProcess)
		{
			return;
		}
		ButtonTemplate component = this.next.GetComponent<ButtonTemplate>();
		component.setActive(AudioVoice_ScrollableController.self == null || AudioVoice_ScrollableController.self.enableNextButton);
	}

	// Token: 0x040023C3 RID: 9155
	private Transform puzzle;

	// Token: 0x040023C4 RID: 9156
	[Tooltip("Button to go to the next screen")]
	public Button next;

	// Token: 0x040023C5 RID: 9157
	[Tooltip("Button to go back")]
	public Button back;

	// Token: 0x040023C6 RID: 9158
	private bool exitInProcess;

	// Token: 0x040023C7 RID: 9159
	[Header("Warning texts")]
	[Tooltip("Contains completion text, icons and stuff")]
	public GameObject completionContainer;

	// Token: 0x040023C8 RID: 9160
	[Tooltip("Good text, when everything is completed")]
	public GameObject completionGoodText;

	// Token: 0x040023C9 RID: 9161
	[Tooltip("Text when something is not completed")]
	public GameObject completionBadText;

	// Token: 0x040023CA RID: 9162
	[Tooltip("Text when all completed but has some items still")]
	public GameObject completionHasItemsText;

	// Token: 0x040023CB RID: 9163
	[Space(10f)]
	[Tooltip("Contains everything related to inventory - text and icons and so on")]
	public GameObject inventoryContainer;

	// Token: 0x040023CC RID: 9164
	[Space(10f)]
	[Tooltip("Title to say something is wrong")]
	public GameObject warningTitle;

	// Token: 0x040023CD RID: 9165
	[Tooltip("Title to say everything is good")]
	public GameObject normalTitle;

	// Token: 0x040023CE RID: 9166
	[Space(10f)]
	[Tooltip("Icon prefab to show completiong")]
	public RectTransform completionIcon;

	// Token: 0x040023CF RID: 9167
	[Tooltip("Contains all icons")]
	public RectTransform completionParent;

	// Token: 0x040023D0 RID: 9168
	[Tooltip("Bad sprite")]
	public GameObject completionIconBad;

	// Token: 0x040023D1 RID: 9169
	[Tooltip("Good sprite")]
	public GameObject completionIconGood;

	// Token: 0x040023D2 RID: 9170
	[Tooltip("None sprite")]
	public GameObject completionIconNone;
}
