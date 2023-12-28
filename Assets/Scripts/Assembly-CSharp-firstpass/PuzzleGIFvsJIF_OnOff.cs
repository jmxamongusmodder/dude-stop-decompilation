using System;
using UnityEngine;

// Token: 0x02000411 RID: 1041
[EnabledManually]
public class PuzzleGIFvsJIF_OnOff : Draggable
{
	// Token: 0x06001A6B RID: 6763 RVA: 0x00067ABC File Offset: 0x00065EBC
	private void Update()
	{
		if (base.WasMoved() && !this.moved)
		{
			this.moved = true;
			this.OnMouseUp();
			this.dragEnabled = false;
			this.firstButton.enabled = false;
			this.secondButton.enabled = false;
		}
		if (this.moved)
		{
			float num = Mathf.MoveTowards(base.transform.position.x, this.otherPosition, Time.deltaTime);
			base.transform.position = new Vector3(num, base.transform.position.y);
			if (num == this.otherPosition)
			{
				Audio.self.playOneShot("bf01e844-9f51-4d98-9e52-9274f854a26e", 1f);
				Global.LevelFailed(0f, true);
				foreach (GameObject gameObject in this.toHide)
				{
					gameObject.SetActive(false);
				}
				this.toShow.SetActive(true);
			}
		}
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x00067BC4 File Offset: 0x00065FC4
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
	}

	// Token: 0x04001882 RID: 6274
	[Header("Button stuff")]
	public Transform bomb;

	// Token: 0x04001883 RID: 6275
	public float otherPosition;

	// Token: 0x04001884 RID: 6276
	public float moveSpeed = 5f;

	// Token: 0x04001885 RID: 6277
	private float startPosition;

	// Token: 0x04001886 RID: 6278
	public GameObject[] toHide;

	// Token: 0x04001887 RID: 6279
	public GameObject toShow;

	// Token: 0x04001888 RID: 6280
	public PuzzleGIFvsJIF_Button firstButton;

	// Token: 0x04001889 RID: 6281
	public PuzzleGIFvsJIF_Button secondButton;

	// Token: 0x0400188A RID: 6282
	private bool moved;
}
