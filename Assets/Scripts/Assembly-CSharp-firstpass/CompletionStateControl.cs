using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000521 RID: 1313
public class CompletionStateControl : MonoBehaviour
{
	// Token: 0x06001E19 RID: 7705 RVA: 0x00087DD8 File Offset: 0x000861D8
	private void Start()
	{
		this.rt = base.GetComponent<RectTransform>();
		Vector2 anchoredPosition = this.rt.anchoredPosition;
		anchoredPosition.y = this.rt.sizeDelta.y;
		this.rt.anchoredPosition = anchoredPosition;
		this.icon.gameObject.SetActive(false);
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x00087E34 File Offset: 0x00086234
	private void Update()
	{
		if (this.selectionDelayCurr > 0f)
		{
			this.selectionDelayCurr -= Time.deltaTime * Global.self.transitionManualSpeed;
			if (this.selectionDelayCurr <= 0f)
			{
				this.moveIconSelect = true;
				this.selectionScaleOnce = true;
				this.destX = this.parent.GetChild(this.indexCurrent).position.x;
			}
		}
		this.moveSelectIcon();
		this.moveLine();
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x00087EBC File Offset: 0x000862BC
	public void calculateProgress()
	{
		foreach (KeyValuePair<string, CompletionIcon> keyValuePair in this.list)
		{
			CompletionState completionState = keyValuePair.Value.getCompletionState();
			if (completionState != CompletionState.Monster)
			{
				if (completionState == CompletionState.Good)
				{
					AwardController.self.solveAsGood(null);
				}
			}
			else
			{
				AwardController.self.solveAsBad(null);
			}
		}
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x00087F54 File Offset: 0x00086354
	public List<CompletionState> getProgressList()
	{
		List<CompletionState> list = new List<CompletionState>();
		foreach (KeyValuePair<string, CompletionIcon> keyValuePair in this.list)
		{
			list.Add(keyValuePair.Value.getCompletionState());
		}
		return list;
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x00087FC4 File Offset: 0x000863C4
	public int getProgress()
	{
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<string, CompletionIcon> keyValuePair in this.list)
		{
			num2++;
			if (keyValuePair.Value.getCompletionState() != CompletionState.None)
			{
				num++;
			}
		}
		return Mathf.RoundToInt((float)num / (float)num2 * 100f);
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x00088048 File Offset: 0x00086448
	public void setIconState(string puzzleName, CompletionState state)
	{
		if (this.list.ContainsKey(puzzleName))
		{
			this.list[puzzleName].setCompletionState(state);
		}
		else
		{
			Debug.LogError("Puzzle doesn't exist in the list");
		}
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x0008807C File Offset: 0x0008647C
	public CompletionState getIconState(string puzzleName)
	{
		if (this.list.ContainsKey(puzzleName))
		{
			return this.list[puzzleName].getCompletionState();
		}
		Debug.LogError("Puzzle doesn't exist in the list");
		return CompletionState.None;
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x000880AC File Offset: 0x000864AC
	private void moveSelectIcon()
	{
		if (!this.moveIconSelect)
		{
			return;
		}
		if (Global.self.transitionManualSpeed <= 0f)
		{
			return;
		}
		Vector3 vector = this.iconSelect.position;
		vector.x = Mathf.Lerp(vector.x, this.destX, Time.deltaTime * Global.self.transitionManualSpeed * this.selectionLerpSpeed);
		vector.x = Mathf.MoveTowards(vector.x, this.destX, Time.deltaTime * Global.self.transitionManualSpeed * this.selectionMinSpeed);
		float num = Mathf.Max(1f, Mathf.Abs(vector.x - this.destX) * this.selectionScaleAmount);
		if (this.selectionScaleOnce)
		{
			Vector3 b = Vector3.right * this.iconSelect.GetComponent<RectTransform>().sizeDelta.x * (num - 1f) * 0.5f * UIControl.self.transform.localScale.x;
			if (this.destX < vector.x)
			{
				b.x *= -1f;
			}
			vector += b;
			this.selectionScaleOnce = false;
		}
		this.iconSelect.localScale = new Vector3(num, 1f, 1f);
		this.iconSelect.position = vector;
		if (Mathf.Abs(this.iconSelect.position.x - this.destX) < 0.001f)
		{
			this.iconSelect.localScale = Vector3.one;
			this.moveIconSelect = false;
		}
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x00088268 File Offset: 0x00086668
	public void changeIndex(int value)
	{
		if (value == -1)
		{
			this.indexCurrent++;
		}
		else
		{
			this.indexCurrent = value;
		}
		this.selectionDelayCurr = this.selectionDelay;
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x00088298 File Offset: 0x00086698
	private void moveLine()
	{
		if (!this.showLine && !this.hideLine)
		{
			return;
		}
		Vector2 anchoredPosition = this.rt.anchoredPosition;
		float num = 0f;
		if (this.showLine)
		{
			if (Mathf.Abs(anchoredPosition.y) < 1f)
			{
				this.showLine = false;
			}
		}
		else if (this.hideLine)
		{
			num = this.rt.sizeDelta.y + 5f;
			if (Mathf.Abs(anchoredPosition.y - num) < 1f)
			{
				this.hideLine = false;
				base.gameObject.SetActive(false);
				this.iconParticles.SetActive(false);
				if (this.resetAfterHide)
				{
					UIControl.self.endCompletionPack();
				}
			}
		}
		anchoredPosition.y = Mathf.Lerp(anchoredPosition.y, num, Time.deltaTime * 6f) + Time.deltaTime * 2f;
		this.rt.anchoredPosition = anchoredPosition;
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x000883A4 File Offset: 0x000867A4
	public void resetAll()
	{
		while (this.parent.childCount > 0)
		{
			Transform child = this.parent.GetChild(0);
			child.SetParent(null);
			UnityEngine.Object.Destroy(child.gameObject);
		}
		this.showLine = false;
		this.hideLine = false;
		Vector2 anchoredPosition = this.rt.anchoredPosition;
		anchoredPosition.y = this.rt.sizeDelta.y;
		this.rt.anchoredPosition = anchoredPosition;
		base.gameObject.SetActive(false);
		this.list.Clear();
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x00088440 File Offset: 0x00086840
	public void setCompletionUI(List<Transform> list)
	{
		foreach (Transform transform in list)
		{
			Transform transform2 = UnityEngine.Object.Instantiate<Transform>(this.icon);
			transform2.SetParent(this.parent);
			transform2.localScale = Vector3.one;
			transform2.gameObject.SetActive(true);
			transform2.GetComponent<CompletionIcon>().resetBanner();
			this.list.Add(transform.name, transform2.GetComponent<CompletionIcon>());
		}
		RectTransform component = this.parent.GetComponent<RectTransform>();
		Vector3 v = component.sizeDelta;
		v.x = (float)this.parent.childCount * this.iconSize;
		component.sizeDelta = v;
		this.indexCurrent = -1;
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x00088528 File Offset: 0x00086928
	public void showCompletionUI()
	{
		if (base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.gameObject.SetActive(true);
		this.iconParticles.SetActive(true);
		base.Invoke("setSelectAtStart", 0.05f);
		foreach (KeyValuePair<string, CompletionIcon> keyValuePair in this.list)
		{
			keyValuePair.Value.resetBanner();
		}
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x000885C4 File Offset: 0x000869C4
	private void setSelectAtStart()
	{
		this.iconSelect.position = this.parent.GetChild(this.indexCurrent).position;
		this.hideLine = false;
		this.showLine = true;
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x000885F8 File Offset: 0x000869F8
	public void hideCompletionUI(bool remove = false)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.showLine = false;
		this.hideLine = true;
		this.resetAfterHide = remove;
		foreach (KeyValuePair<string, CompletionIcon> keyValuePair in this.list)
		{
			keyValuePair.Value.hideBanner();
		}
	}

	// Token: 0x04002161 RID: 8545
	private RectTransform rt;

	// Token: 0x04002162 RID: 8546
	private bool showLine;

	// Token: 0x04002163 RID: 8547
	private bool hideLine;

	// Token: 0x04002164 RID: 8548
	[Tooltip("Transform with icons for levels")]
	public Transform parent;

	// Token: 0x04002165 RID: 8549
	[Tooltip("Prefab for the icons")]
	public Transform icon;

	// Token: 0x04002166 RID: 8550
	[Tooltip("Icon to show which level is current")]
	public Transform iconSelect;

	// Token: 0x04002167 RID: 8551
	[Tooltip("Particles to play when showing Icon. Located in Inventory object")]
	public GameObject iconParticles;

	// Token: 0x04002168 RID: 8552
	private int indexCurrent;

	// Token: 0x04002169 RID: 8553
	private float destX;

	// Token: 0x0400216A RID: 8554
	[Tooltip("How big icon is")]
	public float iconSize = 30f;

	// Token: 0x0400216B RID: 8555
	[Header("Current selection")]
	[Tooltip("move selection after delay, to match transition animation")]
	public float selectionDelay = 1f;

	// Token: 0x0400216C RID: 8556
	private float selectionDelayCurr;

	// Token: 0x0400216D RID: 8557
	[Tooltip("How fast to lerp selection icon")]
	public float selectionLerpSpeed = 2f;

	// Token: 0x0400216E RID: 8558
	[Tooltip("Minimal speed of the lerp")]
	public float selectionMinSpeed = 2f;

	// Token: 0x0400216F RID: 8559
	[Tooltip("How much to scale on movement start")]
	public float selectionScaleAmount = 0.01f;

	// Token: 0x04002170 RID: 8560
	private bool moveIconSelect;

	// Token: 0x04002171 RID: 8561
	private bool selectionScaleOnce;

	// Token: 0x04002172 RID: 8562
	private Dictionary<string, CompletionIcon> list = new Dictionary<string, CompletionIcon>();

	// Token: 0x04002173 RID: 8563
	private bool resetAfterHide;
}
