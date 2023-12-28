using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000573 RID: 1395
[SelectionBase]
public class PackListToolItem : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x17000083 RID: 131
	// (get) Token: 0x06002013 RID: 8211 RVA: 0x0009C500 File Offset: 0x0009A900
	private PackListToolControl pc
	{
		get
		{
			if (this._pc == null)
			{
				this._pc = UnityEngine.Object.FindObjectOfType<PackListToolControl>();
			}
			return this._pc;
		}
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x0009C524 File Offset: 0x0009A924
	public void OnPointerClick(PointerEventData eventData)
	{
		if (!Global.self.NoCurrentTransition)
		{
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Global.self.loopFirstPuzzle = true;
			this.OnMouseClick();
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Global.self.loopFirstPuzzle = false;
			this.OnMouseClick();
		}
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x0009C57C File Offset: 0x0009A97C
	private void OnMouseClick()
	{
		Transform[] array;
		if (Global.self.loopFirstPuzzle)
		{
			array = new Transform[]
			{
				base.transform
			};
		}
		else
		{
			int siblingIndex = base.transform.GetSiblingIndex();
			int num = base.transform.parent.childCount - siblingIndex;
			array = new Transform[num];
			for (int i = siblingIndex; i < base.transform.parent.childCount; i++)
			{
				array[i - siblingIndex] = base.transform.parent.GetChild(i);
			}
		}
		bool flag = false;
		for (int j = 0; j < array.Length; j++)
		{
			foreach (GameObject gameObject in this.pc.puzzleList)
			{
				if (!(gameObject == null))
				{
					if (string.Equals(gameObject.name, array[j].name))
					{
						array[j] = gameObject.transform;
						flag = true;
						break;
					}
				}
			}
		}
		if (flag)
		{
			UIControl.self.packsGraph.SetActive(false);
			this.loadLevels(array);
		}
	}

	// Token: 0x06002016 RID: 8214 RVA: 0x0009C6B0 File Offset: 0x0009AAB0
	private void loadLevels(Transform[] levels)
	{
		Global.self.resetPackLevelUI();
		if (Global.self.loopFirstPuzzle)
		{
			Global.self.firstPuzzle = levels[0].transform;
			Global.self.makeNewLevel(levels[0].transform, Vector2.right, true);
		}
		else
		{
			Global.self.makeLevelList(levels);
		}
		Audio.self.stopAllVoices();
	}

	// Token: 0x06002017 RID: 8215 RVA: 0x0009C71A File Offset: 0x0009AB1A
	private void OnValidate()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.setColor(PackListToolColors.none, true);
		this.setText();
		this.setPackParent(0);
	}

	// Token: 0x06002018 RID: 8216 RVA: 0x0009C744 File Offset: 0x0009AB44
	public void setPackOrder(int amount)
	{
		int num = base.transform.GetSiblingIndex();
		num += amount;
		num = Mathf.Clamp(num, 0, base.transform.parent.childCount - 1);
		base.transform.SetSiblingIndex(num);
		base.transform.parent.GetComponent<GridLayoutGroup>().enabled = false;
		base.transform.parent.GetComponent<GridLayoutGroup>().enabled = true;
	}

	// Token: 0x06002019 RID: 8217 RVA: 0x0009C7B4 File Offset: 0x0009ABB4
	public void setPackParent(int amount = 0)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.packNumber += amount;
		this.packNumber = Mathf.Clamp(this.packNumber, 0, this.pc.listParent.Length - 1);
		if (base.transform.parent.name != this.pc.listParent[this.packNumber].name)
		{
			base.transform.SetParent(this.pc.listParent[this.packNumber]);
			this.pc.updateParentListGroups();
		}
	}

	// Token: 0x0600201A RID: 8218 RVA: 0x0009C85C File Offset: 0x0009AC5C
	public void setColor(PackListToolColors type, bool last = false)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.image.color = Color.white;
		if (last)
		{
			type = this.pc.lastSelectedColor;
		}
		switch (type)
		{
		case PackListToolColors.difficulty:
			this.image.color = Color.Lerp(Color.white, this.pc.selectColor, (float)this.difficulty / 5f);
			break;
		case PackListToolColors.achievement:
			this.image.color = Color.Lerp(Color.white, this.pc.selectColor, (float)this.achievement / 3f);
			break;
		case PackListToolColors.inventory:
			if (this.inventory)
			{
				this.image.color = this.pc.selectColor;
			}
			break;
		case PackListToolColors.jigsaw:
			if (this.jigsaw)
			{
				this.image.color = this.pc.selectColor;
			}
			break;
		case PackListToolColors.completion:
			this.image.color = Color.Lerp(Color.white, this.pc.selectColor, (float)this.completion / 10f);
			break;
		case PackListToolColors.cupLevel:
			if (this.goodCup)
			{
				this.image.color = Color.Lerp(Color.white, this.pc.selectColor, 0.5f);
			}
			else if (this.badCup)
			{
				this.image.color = this.pc.selectColor;
			}
			break;
		}
		if ((this.badCup || this.goodCup) && type != PackListToolColors.cupLevel)
		{
			Color color = this.image.color;
			color.r -= (1f - color.r) * 3f;
			color.g = 1f;
			color.b = 0.5f;
			this.image.color = color;
		}
		this.image.enabled = !this.image.enabled;
		this.image.enabled = !this.image.enabled;
	}

	// Token: 0x0600201B RID: 8219 RVA: 0x0009CAA0 File Offset: 0x0009AEA0
	public void setText()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.customText != string.Empty)
		{
			this.text.text = "<b>" + this.customText + "</b>";
			return;
		}
		string text = string.Empty;
		text = "<b>" + this.puzzleName + "</b>";
		if (this.goodCup || this.badCup)
		{
			text = "<b>*" + this.puzzleName + "</b>";
		}
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"<size=",
			this.subTextSize,
			">\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Dif: ",
			this.difficulty,
			"; "
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Ach: ",
			this.achievement,
			"; "
		});
		text = text + "Inv: " + ((!this.inventory) ? "no" : "yes") + "\n";
		text = text + "Jig: " + ((!this.jigsaw) ? "no" : "yes") + "; ";
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"Done: ",
			this.completion * 10,
			"%; "
		});
		text += "</size>";
		this.text.text = text;
	}

	// Token: 0x04002351 RID: 9041
	[Header("Pack")]
	[Range(0f, 15f)]
	public int packNumber = 1;

	// Token: 0x04002352 RID: 9042
	[Header("Stats")]
	public string puzzleName;

	// Token: 0x04002353 RID: 9043
	[Range(0f, 5f)]
	public int difficulty;

	// Token: 0x04002354 RID: 9044
	[Range(0f, 3f)]
	public int achievement;

	// Token: 0x04002355 RID: 9045
	public bool jigsaw;

	// Token: 0x04002356 RID: 9046
	public bool inventory;

	// Token: 0x04002357 RID: 9047
	public string customText = string.Empty;

	// Token: 0x04002358 RID: 9048
	[Range(0f, 10f)]
	public int completion;

	// Token: 0x04002359 RID: 9049
	public bool goodCup;

	// Token: 0x0400235A RID: 9050
	public bool badCup;

	// Token: 0x0400235B RID: 9051
	[Header("Links")]
	public Image image;

	// Token: 0x0400235C RID: 9052
	public Text text;

	// Token: 0x0400235D RID: 9053
	public int subTextSize = 10;

	// Token: 0x0400235E RID: 9054
	private PackListToolControl _pc;
}
