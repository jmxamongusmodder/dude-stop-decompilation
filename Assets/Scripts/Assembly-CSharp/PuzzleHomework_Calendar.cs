using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class PuzzleHomework_Calendar : MonoBehaviour
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00068D99 File Offset: 0x00067199
	// (set) Token: 0x06001A98 RID: 6808 RVA: 0x00068DA1 File Offset: 0x000671A1
	public bool friday { get; private set; }

	// Token: 0x06001A99 RID: 6809 RVA: 0x00068DAA File Offset: 0x000671AA
	private void OnMouseDown()
	{
		if (!this.active)
		{
			return;
		}
		base.StartCoroutine(this.DateMovingCoroutine());
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x00068DC5 File Offset: 0x000671C5
	public void MoveToMonday()
	{
		this.square.localPosition = this.mondayPosition;
		UnityEngine.Object.Destroy(base.GetComponent<Collider2D>());
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x00068DE8 File Offset: 0x000671E8
	private void OnMouseEnter()
	{
		if (!this.penHeld && !this.friday && this.active)
		{
			this.forwardButton.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x00068E1C File Offset: 0x0006721C
	private void OnMouseExit()
	{
		this.forwardButton.gameObject.SetActive(false);
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x00068E30 File Offset: 0x00067230
	public IEnumerator MoveToFriday()
	{
		if (this.friday)
		{
			yield break;
		}
		Audio.self.playOneShot("af578c57-523b-40a6-995e-91387823bb47", 1f);
		base.GetComponent<Collider2D>().enabled = false;
		for (int i = this.currentDay; i < 4; i++)
		{
			float timer = 0f;
			Vector2 start = this.square.localPosition;
			Vector2 end = start + Vector2.right * this.moveDistance;
			while (timer != this.fastForwardTime)
			{
				timer = Mathf.MoveTowards(timer, this.fastForwardTime, Time.deltaTime);
				this.square.localPosition = Vector2.Lerp(start, end, timer / this.fastForwardTime);
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x00068E4C File Offset: 0x0006724C
	private IEnumerator DateMovingCoroutine()
	{
		if (this.friday)
		{
			yield break;
		}
		Global.self.canBePaused = false;
		base.GetComponent<Collider2D>().enabled = false;
		float timer = 0f;
		Vector2 start = this.square.localPosition;
		Vector2 end = start + Vector2.right * this.moveDistance;
		if (this.currentDay < 3)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_HomeWork>().onCalendarClick(false);
		}
		else
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_HomeWork>().onCalendarClick(true);
		}
		Audio.self.playOneShot("6b3b08ae-a30c-4ad7-b396-a653ea085495", 1f);
		while (timer != this.moveTime)
		{
			timer = Mathf.MoveTowards(timer, this.moveTime, Time.deltaTime);
			this.square.localPosition = Vector2.Lerp(start, end, timer / this.moveTime);
			yield return null;
		}
		yield return new WaitForSeconds(this.waitBeforeEnabling);
		this.currentDay++;
		if (this.currentDay < 4)
		{
			base.GetComponent<Collider2D>().enabled = true;
		}
		else
		{
			UnityEngine.Object.Destroy(base.GetComponent<Collider2D>());
			this.friday = true;
		}
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x040018B5 RID: 6325
	public Transform forwardButton;

	// Token: 0x040018B7 RID: 6327
	[Header("Moving the square")]
	public Transform square;

	// Token: 0x040018B8 RID: 6328
	public float moveDistance;

	// Token: 0x040018B9 RID: 6329
	public float moveTime;

	// Token: 0x040018BA RID: 6330
	public float fastForwardTime;

	// Token: 0x040018BB RID: 6331
	public float waitBeforeEnabling = 1f;

	// Token: 0x040018BC RID: 6332
	public Vector2 mondayPosition;

	// Token: 0x040018BD RID: 6333
	private int currentDay;

	// Token: 0x040018BE RID: 6334
	[HideInInspector]
	public bool penHeld;

	// Token: 0x040018BF RID: 6335
	[HideInInspector]
	public bool active = true;
}
