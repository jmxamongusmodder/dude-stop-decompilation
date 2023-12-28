using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200002F RID: 47
public class PuzzleExamPack_IlluminatiButton : MonoBehaviour
{
	// Token: 0x06000111 RID: 273 RVA: 0x0000A9BC File Offset: 0x00008BBC
	public void AnswerSelected(bool first)
	{
		if (!base.enabled)
		{
			return;
		}
		Animator animator;
		Image image;
		if (first)
		{
			animator = this.correctAnimator1;
			image = this.correctCheckbox1;
		}
		else
		{
			animator = this.correctAnimator2;
			image = this.correctCheckbox2;
		}
		animator.gameObject.SetActive(true);
		animator.enabled = true;
		animator.Play("checkMarkAppear");
		image.color = this.checkedColor;
		base.GetComponentsInChildren<Button>().ToList<Button>().ForEach(delegate(Button x)
		{
			x.enabled = false;
			x.GetComponent<ButtonTemplate>().enabled = false;
		});
		base.enabled = false;
	}

	// Token: 0x0400019A RID: 410
	public Animator correctAnimator1;

	// Token: 0x0400019B RID: 411
	public Image correctCheckbox1;

	// Token: 0x0400019C RID: 412
	public Animator correctAnimator2;

	// Token: 0x0400019D RID: 413
	public Image correctCheckbox2;

	// Token: 0x0400019E RID: 414
	private Color checkedColor = new Color(0.39f, 0.39f, 0.39f);
}
