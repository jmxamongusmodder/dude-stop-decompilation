using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000545 RID: 1349
public class endTextLine : MonoBehaviour
{
	// Token: 0x06001EED RID: 7917 RVA: 0x00092EF7 File Offset: 0x000912F7
	private void Start()
	{
		this.textBox.gameObject.SetActive(false);
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x00092F0A File Offset: 0x0009130A
	public void animationEnd()
	{
		this.parent.ShowNextLineAfterAnimation();
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x00092F17 File Offset: 0x00091317
	public void setText(string text)
	{
		this.textBox.text = text;
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x00092F25 File Offset: 0x00091325
	public void setParent(endTextControl parent)
	{
		this.parent = parent;
	}

	// Token: 0x04002239 RID: 8761
	public Text textBox;

	// Token: 0x0400223A RID: 8762
	private endTextControl parent;
}
