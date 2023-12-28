using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000576 RID: 1398
public class packSelectUI : AbstractUIScreen
{
	// Token: 0x06002023 RID: 8227 RVA: 0x0009CE54 File Offset: 0x0009B254
	public override void Update()
	{
		base.Update();
		if (Input.GetAxisRaw("Vertical") > 0f)
		{
			this.onIconClick(Global.self.currentLevelPack - 1);
		}
		else if (Input.GetAxisRaw("Vertical") < 0f)
		{
			this.onIconClick(Global.self.currentLevelPack + 1);
		}
		if (Global.self.DEBUG)
		{
			if (Input.GetKeyDown(KeyCode.U))
			{
				this.unlcokNum = ++this.toUnlock;
				Global.self.unlockNextPack = true;
				this.positionPackIcons();
			}
			if (Input.GetKeyDown(KeyCode.I))
			{
				this.toUnlock += 2;
				this.unlcokNum = this.toUnlock;
				Global.self.unlockNextPack = true;
				this.positionPackIcons();
			}
		}
		if (Input.mouseScrollDelta.y != 0f)
		{
			if (Input.mouseScrollDelta.y > 0f)
			{
				this.scrollPackSelectLeft();
			}
			else
			{
				this.scrollPackSelectRight();
			}
		}
		this.movePackSelect();
		this.unlockNextPack();
		this.updateIconPos();
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x0009CF84 File Offset: 0x0009B384
	private void updateIconPos()
	{
		IEnumerator enumerator = this.container.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				RectTransform rectTransform = (RectTransform)obj;
				if (!rectTransform.gameObject.activeInHierarchy)
				{
					break;
				}
				Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;
				if (Mathf.Abs(anchoredPosition3D.z) != 0f)
				{
					anchoredPosition3D.x = Mathf.Lerp(anchoredPosition3D.x, 0f, Time.deltaTime * 5f);
					anchoredPosition3D.x = Mathf.MoveTowards(anchoredPosition3D.x, 0f, Time.deltaTime * 2f);
					rectTransform.anchoredPosition3D = anchoredPosition3D;
					RectTransform rectTransform2 = null;
					foreach (Transform transform in this.newIconList)
					{
						if (transform.parent == rectTransform.transform)
						{
							rectTransform2 = transform.GetComponent<RectTransform>();
							break;
						}
					}
					if (rectTransform2 == null)
					{
						break;
					}
					Vector2 anchoredPosition = rectTransform2.anchoredPosition;
					anchoredPosition.x = Mathf.Lerp(anchoredPosition.x, 0f, Time.deltaTime * 5f);
					anchoredPosition.x = Mathf.MoveTowards(anchoredPosition.x, 0f, Time.deltaTime * 4f);
					rectTransform2.anchoredPosition = anchoredPosition;
					CanvasGroup component = rectTransform2.GetComponent<CanvasGroup>();
					if (component.alpha <= 0f)
					{
						Audio.self.playOneShot("f9d99b50-926a-4f5b-a428-53cd37a76b7f", 1f);
					}
					component.alpha += Time.deltaTime * 2f;
					if (component.alpha >= 1f && anchoredPosition3D.x == 0f)
					{
						anchoredPosition3D.z = 0f;
						rectTransform.anchoredPosition3D = anchoredPosition3D;
					}
					break;
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
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x0009D1D4 File Offset: 0x0009B5D4
	private void unlockNextPack()
	{
		if (!this.active)
		{
			return;
		}
		if (!Global.self.unlockNextPack || Global.self.currentAwardAnimCount != 0)
		{
			return;
		}
		Global.self.unlockNextPack = false;
		int num = this.unlockedPackCount;
		this.showPackIcons();
		IEnumerator enumerator = this.container.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.GetSiblingIndex() > num && transform.GetSiblingIndex() <= this.unlockedPackCount)
				{
					this.addNewIcon(transform, true);
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
		if (this.unlockedPackCount < this.iconsCountInLine)
		{
			this.destinationY = this.iconSize * (float)(this.unlockedPackCount + 1) * 0.5f;
		}
		else
		{
			this.selectedMiddlePack = this.unlockedPackCount - Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f);
			this.selectedMiddlePack = Mathf.Clamp(this.selectedMiddlePack, 0, this.unlockedPackCount);
			this.destinationMiddlePack = num + 1 - Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f);
			this.destinationMiddlePack = Mathf.Clamp(this.destinationMiddlePack, 0, this.unlockedPackCount);
			this.destinationY = (float)this.destinationMiddlePack * this.iconSize + this.iconSize * 0.5f;
			if (this.destinationMiddlePack < this.selectedMiddlePack)
			{
				this.moveWithSteps = true;
			}
			this.setArrows();
		}
		this.moveContainer = true;
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x0009D380 File Offset: 0x0009B780
	private void addNewIcon(Transform child, bool moveOffscreen = true)
	{
		RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.newIconPref);
		rectTransform.SetParent(child);
		rectTransform.localScale = Vector3.one;
		rectTransform.anchoredPosition = Vector3.zero;
		rectTransform.gameObject.SetActive(true);
		RectTransform component = child.GetComponent<RectTransform>();
		Vector3 vector = component.anchoredPosition3D;
		vector.z = 1f;
		if (moveOffscreen)
		{
			vector.x = 100f;
		}
		else
		{
			vector.x = 0f;
		}
		component.anchoredPosition3D = vector;
		vector = rectTransform.anchoredPosition;
		if (moveOffscreen)
		{
			vector.x = -150f;
		}
		else
		{
			vector.x = 0f;
		}
		rectTransform.anchoredPosition = vector;
		this.newIconList.Add(rectTransform);
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x0009D454 File Offset: 0x0009B854
	private void movePackSelect()
	{
		if (!this.moveContainer)
		{
			return;
		}
		Vector2 anchoredPosition = this.container.anchoredPosition;
		anchoredPosition.y = Mathf.Lerp(anchoredPosition.y, this.destinationY, Time.deltaTime * 5f);
		anchoredPosition.y = Mathf.MoveTowards(anchoredPosition.y, this.destinationY, Time.deltaTime * 2f);
		this.container.anchoredPosition = anchoredPosition;
		if (Mathf.Abs(anchoredPosition.y - this.destinationY) < 1f)
		{
			this.moveContainer = false;
			if (this.moveWithSteps)
			{
				this.moveWithSteps = false;
				this.destinationMiddlePack++;
				if (this.destinationMiddlePack <= this.selectedMiddlePack)
				{
					this.destinationY = (float)this.destinationMiddlePack * this.iconSize + this.iconSize * 0.5f;
					this.moveContainer = true;
					this.moveWithSteps = true;
				}
			}
		}
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x0009D554 File Offset: 0x0009B954
	private void showPackIcons()
	{
		this.unlockedPackCount = 0;
		foreach (Transform transform in Global.self.levelPackMenu)
		{
			AwardName toUnlockNextPack = transform.GetComponent<levelPackControl>().awardControllerScript.toUnlockNextPack;
			AwardName toUnlockNextPack2 = transform.GetComponent<levelPackControl>().awardControllerScript.toUnlockNextPack2;
			if (toUnlockNextPack != AwardName.None || toUnlockNextPack2 != AwardName.None)
			{
				if (!AudioVoice_Pack10.ThisPackIsLocked(toUnlockNextPack, toUnlockNextPack2))
				{
					if (Global.self.cupList[toUnlockNextPack] == CupStatus.Empty && (toUnlockNextPack2 == AwardName.None || Global.self.cupList[toUnlockNextPack2] == CupStatus.Empty))
					{
						break;
					}
					this.unlockedPackCount++;
				}
			}
		}
		if (this.unlcokNum != 0)
		{
			this.unlockedPackCount = this.unlcokNum;
			this.unlcokNum = 0;
		}
		if (this.unlockedPackCount == 0)
		{
			this.packSelectParent.SetActive(false);
			return;
		}
		this.packSelectParent.SetActive(true);
		if (Global.self.unlockNextPack)
		{
			this.unlockedPackCount = Global.self.currentLevelPack;
		}
		else
		{
			this.unlockedPackCount = Mathf.Min(this.unlockedPackCount, Global.self.levelPackMenu.Length - 1);
		}
		IEnumerator enumerator = this.container.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				int siblingIndex = transform2.GetSiblingIndex();
				if (siblingIndex > this.unlockedPackCount)
				{
					transform2.gameObject.SetActive(false);
				}
				else
				{
					transform2.gameObject.SetActive(true);
					if (siblingIndex > 0 && !SerializablePackSavedStats.Get(siblingIndex).packClickedOn)
					{
						this.addNewIcon(transform2, !SerializablePackSavedStats.Get(siblingIndex).packShowedOnce);
						SerializablePackSavedStats.Get(siblingIndex).packShowedOnce = true;
					}
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
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x0009D76C File Offset: 0x0009BB6C
	private void positionPackIcons()
	{
		this.moveContainer = false;
		Vector2 anchoredPosition = this.container.anchoredPosition;
		if (this.unlockedPackCount < this.iconsCountInLine)
		{
			anchoredPosition.y = this.iconSize * (float)(this.unlockedPackCount + 1) * 0.5f;
			this.upArrow.hideArrow();
			this.downArrow.hideArrow();
		}
		else
		{
			this.selectedMiddlePack = Mathf.Min(this.selectedMiddlePack, this.unlockedPackCount - Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f));
			this.selectedMiddlePack = Mathf.Max(this.selectedMiddlePack, Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f));
			anchoredPosition.y = (float)this.selectedMiddlePack * this.iconSize + this.iconSize * 0.5f;
			this.setArrows();
		}
		this.container.anchoredPosition = anchoredPosition;
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x0009D858 File Offset: 0x0009BC58
	public void onIconClick(int index)
	{
		if (!this.active)
		{
			return;
		}
		if (Global.self.currentAwardAnimCount > 0)
		{
			return;
		}
		if (Global.self.unlockNextPack || this.moveWithSteps)
		{
			return;
		}
		index = Mathf.Clamp(index, 0, this.unlockedPackCount);
		if (Global.self.currentLevelPack == index)
		{
			return;
		}
		PuzzleStats component = Global.self.currPuzzle.GetComponent<PuzzleStats>();
		if (!component.isClickAllowed(ClickWhileVoice.pack))
		{
			return;
		}
		Transform child = this.container.GetChild(Global.self.currentLevelPack);
		child.GetComponent<Button>().interactable = true;
		child = this.container.GetChild(index);
		child.GetComponent<Button>().interactable = false;
		foreach (Transform transform in this.newIconList)
		{
			if (transform.parent == child)
			{
				transform.GetComponent<packSelectIconAnimation>().remove = true;
				this.newIconList.Remove(transform);
				SerializablePackSavedStats.Get(index).packClickedOn = true;
				break;
			}
		}
		if (index == 2)
		{
			SerializablePackSavedStats.Get(index - 1).packClickedOn = true;
		}
		Global.self.lastPackCompletionState = CompletionState.None;
		bool flag = Global.self.currentLevelPack > index;
		Vector2 dir = (!flag) ? Vector2.down : Vector2.up;
		Global.self.makeNewLevel(Global.self.levelPackMenu[index], dir, true);
		Global.self.scrollMultipleScreens(Global.self.levelPackMenu, Global.self.currentLevelPack, index, dir, true);
		Global.self.currentLevelPack = index;
		if (this.unlockedPackCount > this.iconsCountInLine)
		{
			this.selectedMiddlePack = Mathf.Min(Global.self.currentLevelPack, this.unlockedPackCount - Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f));
			this.selectedMiddlePack = Mathf.Max(this.selectedMiddlePack, Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f));
			this.destinationY = (float)this.selectedMiddlePack * this.iconSize + this.iconSize * 0.5f;
			this.moveContainer = true;
			this.setArrows();
		}
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x0009DAB8 File Offset: 0x0009BEB8
	public void scrollPackSelectLeft()
	{
		if (!this.active || !this.upArrow.active)
		{
			return;
		}
		if (this.moveWithSteps)
		{
			return;
		}
		if (this.selectedMiddlePack > 0)
		{
			this.selectedMiddlePack--;
			this.selectedMiddlePack = Mathf.Clamp(this.selectedMiddlePack, 0, this.unlockedPackCount);
			this.destinationY = (float)this.selectedMiddlePack * this.iconSize + this.iconSize * 0.5f;
			this.moveContainer = true;
			this.setArrows();
		}
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x0009DB50 File Offset: 0x0009BF50
	public void scrollPackSelectRight()
	{
		if (!this.active || !this.downArrow.active)
		{
			return;
		}
		if (this.moveWithSteps)
		{
			return;
		}
		if (this.selectedMiddlePack < this.unlockedPackCount)
		{
			this.selectedMiddlePack++;
			this.selectedMiddlePack = Mathf.Clamp(this.selectedMiddlePack, 0, this.unlockedPackCount);
			this.destinationY = (float)this.selectedMiddlePack * this.iconSize + this.iconSize * 0.5f;
			this.moveContainer = true;
			this.setArrows();
		}
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x0009DBEC File Offset: 0x0009BFEC
	private void setArrows()
	{
		int num = Mathf.FloorToInt((float)this.iconsCountInLine * 0.5f);
		if (this.selectedMiddlePack <= num)
		{
			this.upArrow.hideArrow();
		}
		if (this.selectedMiddlePack > num)
		{
			this.upArrow.showArrow();
		}
		if (this.selectedMiddlePack >= this.unlockedPackCount - num)
		{
			this.downArrow.hideArrow();
		}
		if (this.selectedMiddlePack < this.unlockedPackCount - num)
		{
			this.downArrow.showArrow();
		}
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x0009DC76 File Offset: 0x0009C076
	protected override void cancelPressed()
	{
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x0009DC78 File Offset: 0x0009C078
	public override void setScreen(Transform item)
	{
		this.puzzle = item;
		RectTransform component = this.packSelectParent.GetComponent<RectTransform>();
		Vector2 sizeDelta = component.sizeDelta;
		sizeDelta.y = (float)this.iconsCountInLine * this.iconSize - 10f;
		component.sizeDelta = sizeDelta;
		this.showPackIcons();
		this.selectedMiddlePack = Global.self.currentLevelPack;
		this.container.GetChild(this.selectedMiddlePack).GetComponent<Button>().interactable = false;
		this.positionPackIcons();
	}

	// Token: 0x06002030 RID: 8240 RVA: 0x0009DCF9 File Offset: 0x0009C0F9
	public override void setActive(bool active)
	{
		base.setActive(active);
		if (active && this.puzzle)
		{
			this.puzzle.GetComponent<PuzzleStats>().UIScreenSecondary = null;
		}
	}

	// Token: 0x04002369 RID: 9065
	private Transform puzzle;

	// Token: 0x0400236A RID: 9066
	[Header("Pack selection")]
	[Tooltip("Whole object with mask and icons and everything.")]
	public GameObject packSelectParent;

	// Token: 0x0400236B RID: 9067
	[Tooltip("Parent with all icons only")]
	public RectTransform container;

	// Token: 0x0400236C RID: 9068
	[Tooltip("Scroll through list to the left")]
	public packSelectArrowAnimation upArrow;

	// Token: 0x0400236D RID: 9069
	[Tooltip("Scroll through list to the right")]
	public packSelectArrowAnimation downArrow;

	// Token: 0x0400236E RID: 9070
	[Tooltip("Icon to show on new pack appear")]
	public RectTransform newIconPref;

	// Token: 0x0400236F RID: 9071
	private List<Transform> newIconList = new List<Transform>();

	// Token: 0x04002370 RID: 9072
	[Tooltip("Size of the icon (width)")]
	public float iconSize = 60f;

	// Token: 0x04002371 RID: 9073
	[Tooltip("How much icons to show in the line")]
	public int iconsCountInLine = 7;

	// Token: 0x04002372 RID: 9074
	private int unlockedPackCount;

	// Token: 0x04002373 RID: 9075
	private int selectedMiddlePack;

	// Token: 0x04002374 RID: 9076
	private int destinationMiddlePack;

	// Token: 0x04002375 RID: 9077
	private bool moveWithSteps;

	// Token: 0x04002376 RID: 9078
	private float destinationY;

	// Token: 0x04002377 RID: 9079
	private bool moveContainer;

	// Token: 0x04002378 RID: 9080
	private int unlcokNum;

	// Token: 0x04002379 RID: 9081
	private int toUnlock;
}
