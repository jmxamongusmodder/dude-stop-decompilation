using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class PuzzleFriendsPen_Hand : MonoBehaviour
{
	// Token: 0x06001A49 RID: 6729 RVA: 0x00066561 File Offset: 0x00064961
	private void Start()
	{
		this.pen = base.transform.GetChild(0).GetComponent<PuzzleFriendsPen_BlackPen>();
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x0006657A File Offset: 0x0006497A
	private void Update()
	{
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x0006657C File Offset: 0x0006497C
	private void OnEnable()
	{
		if (this.movedOut && !this.activeCoroutine && Global.getCompletionState(null) == CompletionState.Monster)
		{
			base.StartCoroutine(this.ShakeHand());
		}
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000665AD File Offset: 0x000649AD
	public void MoveOut()
	{
		if (!this.movedOut)
		{
			this.movedOut = true;
			base.StartCoroutine(this.MovingOutCoroutine());
		}
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x000665CE File Offset: 0x000649CE
	public void MoveIn()
	{
		this.movedOut = false;
		base.StartCoroutine(this.MovingInCoroutine());
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x000665E4 File Offset: 0x000649E4
	private IEnumerator MovingInCoroutine()
	{
		Vector2 start = base.transform.localPosition;
		float timer = 0f;
		yield return null;
		Global.self.canBePaused = false;
		Global.PauseArrows(this.movingOutTime);
		while (timer != this.movingOutTime)
		{
			timer = Mathf.MoveTowards(timer, this.movingOutTime, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(start, this.originalPosition, timer / this.movingOutTime);
			yield return null;
		}
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x00066600 File Offset: 0x00064A00
	private IEnumerator MovingOutCoroutine()
	{
		Global.self.canBePaused = false;
		Global.PauseArrows(this.waitBeforeMovingOut + this.movingOutTime + 0.1f);
		yield return new WaitForSeconds(this.waitBeforeMovingOut);
		this.originalPosition = base.transform.localPosition;
		Vector2 end = new Vector2(base.transform.localPosition.x, this.finalY);
		float timer = 0f;
		yield return null;
		while (timer != this.movingOutTime)
		{
			timer = Mathf.MoveTowards(timer, this.movingOutTime, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(this.originalPosition, end, timer / this.movingOutTime);
			yield return null;
		}
		if (this.firstMovingOut)
		{
			this.pen.transform.SetParent(base.transform.parent);
			this.originalPenPosition = this.pen.transform.position;
			this.pen.enabled = true;
			this.pen.checkBounds = true;
			this.pen.hand = this.originalPenPosition;
			this.pen.snapEnabled = false;
			this.firstMovingOut = false;
		}
		else
		{
			this.pen.snapEnabled = true;
		}
		this.pen.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.originalEndPosition = base.transform.localPosition;
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x0006661C File Offset: 0x00064A1C
	private IEnumerator ShakeHand()
	{
		this.activeCoroutine = true;
		int shakeCount = 0;
		int maxShakeCount = 7;
		float moveDistance = 0.3f;
		float moveTime = 0.1f;
		float moveTimer = 0f;
		Vector2 left = this.originalEndPosition - new Vector2(moveDistance, 0f);
		Vector2 right = this.originalEndPosition + new Vector2(moveDistance, 0f);
		Audio.self.playOneShot("9f77e2b4-4388-4ff7-9663-5b4c9ee057cd", 1f);
		for (;;)
		{
			float target;
			if (shakeCount % 2 == 0)
			{
				target = moveTime;
				moveTimer = Mathf.MoveTowards(moveTimer, moveTime, Time.deltaTime);
				if (shakeCount == 0)
				{
					base.transform.localPosition = Vector3.Lerp(this.originalEndPosition, left, moveTimer / moveTime);
				}
				else
				{
					base.transform.localPosition = Vector3.Lerp(right, left, moveTimer / moveTime);
				}
			}
			else
			{
				target = 0f;
				moveTimer = Mathf.MoveTowards(moveTimer, 0f, Time.deltaTime);
				if (shakeCount == maxShakeCount)
				{
					base.transform.localPosition = Vector3.Lerp(this.originalEndPosition, left, moveTimer / moveTime);
				}
				else
				{
					base.transform.localPosition = Vector3.Lerp(right, left, moveTimer / moveTime);
				}
			}
			if (moveTimer == target)
			{
				int num;
				shakeCount = (num = shakeCount) + 1;
				if (num == maxShakeCount)
				{
					break;
				}
			}
			yield return null;
		}
		this.activeCoroutine = false;
		yield break;
	}

	// Token: 0x0400184D RID: 6221
	[Header("Moving out!")]
	public float finalY;

	// Token: 0x0400184E RID: 6222
	public float waitBeforeMovingOut;

	// Token: 0x0400184F RID: 6223
	public float movingOutTime;

	// Token: 0x04001850 RID: 6224
	public float movedOutScale = 1f;

	// Token: 0x04001851 RID: 6225
	public float movedInScale = 0.5f;

	// Token: 0x04001852 RID: 6226
	private bool firstMovingOut = true;

	// Token: 0x04001853 RID: 6227
	private Vector2 originalPosition;

	// Token: 0x04001854 RID: 6228
	private Vector2 originalEndPosition;

	// Token: 0x04001855 RID: 6229
	private PuzzleFriendsPen_BlackPen pen;

	// Token: 0x04001856 RID: 6230
	private Vector2 originalPenPosition;

	// Token: 0x04001857 RID: 6231
	private bool activeCoroutine;

	// Token: 0x04001858 RID: 6232
	private bool movedOut;
}
