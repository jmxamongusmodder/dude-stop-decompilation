using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000515 RID: 1301
public class AllLevelList : AbstractUIScreen
{
	// Token: 0x06001DE5 RID: 7653 RVA: 0x00086D5E File Offset: 0x0008515E
	protected override void cancelPressed()
	{
		this.bContinue();
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x00086D68 File Offset: 0x00085168
	private void makePuzzleButtons()
	{
		if (this.puzzleList.Length == 0)
		{
			Debug.LogError("No puzzles in the list");
			return;
		}
		Transform child = this.buttonList[0].GetChild(0);
		this.prepButton(child, 0, this.puzzleList[0].name);
		for (int i = 1; i < this.puzzleList.Length; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(child);
			transform.SetParent(this.buttonList[(int)Mathf.Floor((float)(i / this.amountInColumn))]);
			transform.localScale = Vector2.one;
			this.prepButton(transform, i, this.puzzleList[i].name);
		}
		foreach (Transform transform2 in this.buttonList)
		{
			RectTransform component = transform2.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component.sizeDelta.x, (float)transform2.childCount * 22.5f);
		}
		child.gameObject.SetActive(false);
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x00086E76 File Offset: 0x00085276
	private void prepButton(Transform button, int ind, string name)
	{
		button.name = ind.ToString();
		button.GetComponent<Text>().text = name;
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00086E97 File Offset: 0x00085297
	public override void setScreen(Transform item)
	{
		this.makePuzzleButtons();
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x00086E9F File Offset: 0x0008529F
	public void bContinue()
	{
		if (!this.active)
		{
			return;
		}
		Global.self.unpauseGame();
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x00086EB8 File Offset: 0x000852B8
	public void bPuzzle()
	{
		if (!this.active)
		{
			return;
		}
		int num = int.Parse(EventSystem.current.currentSelectedGameObject.name);
		Global.self.loopFirstPuzzle = true;
		Global.self.firstPuzzle = this.puzzleList[num].transform;
		Global.self.makeNewLevel(this.puzzleList[num].transform, Vector2.right, true);
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x00086F25 File Offset: 0x00085325
	private void OnValidate()
	{
	}

	// Token: 0x04002131 RID: 8497
	public Transform[] buttonList;

	// Token: 0x04002132 RID: 8498
	public int amountInColumn = 30;

	// Token: 0x04002133 RID: 8499
	public bool update;

	// Token: 0x04002134 RID: 8500
	public GameObject[] puzzleList;
}
