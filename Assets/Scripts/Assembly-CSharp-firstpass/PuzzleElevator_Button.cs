using System;
using System.Linq;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public class PuzzleElevator_Button : MonoBehaviour
{
	// Token: 0x060019F5 RID: 6645 RVA: 0x0006394B File Offset: 0x00061D4B
	private void Start()
	{
		this.clickedAtStart = base.transform.GetChild(1).gameObject.activeSelf;
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x00063969 File Offset: 0x00061D69
	private void Update()
	{
		this.CheckHover();
		this.CheckClick();
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x00063977 File Offset: 0x00061D77
	public void DoorsOpened()
	{
		if (this.clickedAtStart && !this.pushed)
		{
			base.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x000639A8 File Offset: 0x00061DA8
	private void CheckHover()
	{
		Vector2 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float num = Vector2.Distance(a, base.transform.position);
		if (!this.hovered && num < this.mouseDistance)
		{
			this.EnableHover(true);
		}
		else if (this.hovered && num > this.mouseDistance)
		{
			this.EnableHover(false);
		}
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x00063A22 File Offset: 0x00061E22
	private void CheckClick()
	{
		if (!this.hovered || !Input.GetMouseButton(0) || this.pushed)
		{
			return;
		}
		this.Push();
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x00063A4C File Offset: 0x00061E4C
	public void Push()
	{
		this.pushed = true;
		base.transform.GetChild(1).gameObject.SetActive(true);
		Audio.self.playOneShot("38c449ca-6b6a-46e3-9920-05c15c268474", 1f);
		if (this.ButtonsPushed() == 2)
		{
			this.GetPuzzleStats().goBadAfterTime = true;
		}
		else if (this.ButtonsPushed() == 10)
		{
			foreach (PuzzleElevator_Door puzzleElevator_Door in this.GetComponentsInPuzzleStats(false))
			{
				puzzleElevator_Door.PlayBrokenAnimation();
			}
			Global.LevelCompleted(this.waitBeforeEnd, true);
			Global.self.GetCup(AwardName.ELEVATOR);
			GameObject gameObject = this.GetComponentInPuzzleStats(true).gameObject;
			gameObject.SetActive(true);
		}
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x00063B09 File Offset: 0x00061F09
	private void EnableHover(bool status)
	{
		this.hovered = status;
		base.transform.GetChild(0).gameObject.SetActive(status);
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x00063B29 File Offset: 0x00061F29
	private int ButtonsPushed()
	{
		return (from x in this.GetComponentsInPuzzleStats(false)
		where x.pushed
		select x).Count<PuzzleElevator_Button>();
	}

	// Token: 0x040017FF RID: 6143
	public float mouseDistance = 0.2f;

	// Token: 0x04001800 RID: 6144
	public float waitBeforeEnd = 3.5f;

	// Token: 0x04001801 RID: 6145
	private bool hovered;

	// Token: 0x04001802 RID: 6146
	private bool pushed;

	// Token: 0x04001803 RID: 6147
	private bool clickedAtStart;
}
