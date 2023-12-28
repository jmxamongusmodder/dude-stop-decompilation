using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class PuzzleSwipePhoto_Hand : MonoBehaviour
{
	// Token: 0x06001C9D RID: 7325 RVA: 0x0007A281 File Offset: 0x00078681
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawPoint(this.finalPosition, 0.5f);
	}

	// Token: 0x06001C9E RID: 7326 RVA: 0x0007A293 File Offset: 0x00078693
	private void Start()
	{
		this.phone = base.transform.GetChild(0);
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x0007A2A8 File Offset: 0x000786A8
	private void Update()
	{
		if (this.timer == 0f)
		{
			this.startingPosition = base.transform.position;
		}
		if (this.timer < this.time)
		{
			this.timer = Mathf.MoveTowards(this.timer, this.time, Time.deltaTime);
			base.transform.position = Vector3.Lerp(this.startingPosition, this.finalPosition, this.timer / this.time);
			if (this.timer == this.time)
			{
				this.phone.SetParent(base.transform.parent);
				this.phone.GetComponent<BoxCollider2D>().enabled = true;
			}
		}
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x0007A36C File Offset: 0x0007876C
	public void Shake(int strength)
	{
		if (this.coroutine != null)
		{
			base.StopCoroutine(this.coroutine);
		}
		switch (strength)
		{
		case 0:
			this.coroutine = this.MoveBack();
			base.StartCoroutine(this.coroutine);
			break;
		case 1:
			this.coroutine = this.MoveHand();
			base.StartCoroutine(this.coroutine);
			break;
		case 2:
			this.coroutine = this.ShakeHand();
			base.StartCoroutine(this.coroutine);
			break;
		case 3:
			this.coroutine = this.ShakeHandFuriously();
			base.StartCoroutine(this.coroutine);
			break;
		}
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x0007A424 File Offset: 0x00078824
	private IEnumerator MoveBack()
	{
		float moveTime = 0.3f;
		float moveTimer = 0f;
		yield return null;
		while (moveTimer < moveTime)
		{
			moveTimer = Mathf.MoveTowards(moveTimer, moveTime, Time.deltaTime);
			base.transform.position = Vector2.Lerp(base.transform.position, this.finalPosition, moveTimer / moveTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x0007A440 File Offset: 0x00078840
	private IEnumerator MoveHand()
	{
		bool movedForward = false;
		float moveDistance = -0.43f;
		float moveTime = 0.2f;
		float moveTimer = 0f;
		for (;;)
		{
			if (!movedForward && moveTimer != -1f)
			{
				moveTimer = Mathf.MoveTowards(moveTimer, moveTime, Time.deltaTime);
				base.transform.position = Vector3.Lerp(this.finalPosition, this.finalPosition + new Vector2(0f, moveDistance), moveTimer / moveTime);
				if (moveTimer == moveTime)
				{
					movedForward = true;
				}
			}
			else if (moveTimer > 0f)
			{
				moveTimer = Mathf.MoveTowards(moveTimer, 0f, Time.deltaTime);
				base.transform.position = Vector3.Lerp(this.finalPosition, this.finalPosition + new Vector2(0f, moveDistance), moveTimer / moveTime);
				if (moveTimer == 0f)
				{
					break;
				}
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x0007A45C File Offset: 0x0007885C
	private IEnumerator ShakeHand()
	{
		int shakeCount = 0;
		int maxShakeCount = 7;
		float moveDistance = 0.3f;
		float moveTime = 0.1f;
		float moveTimer = 0f;
		Vector2 left = this.finalPosition - new Vector2(moveDistance, 0f);
		Vector2 right = this.finalPosition + new Vector2(moveDistance, 0f);
		for (;;)
		{
			float target;
			if (shakeCount % 2 == 0)
			{
				target = moveTime;
				moveTimer = Mathf.MoveTowards(moveTimer, moveTime, Time.deltaTime);
				if (shakeCount == 0)
				{
					base.transform.position = Vector3.Lerp(this.finalPosition, left, moveTimer / moveTime);
				}
				else
				{
					base.transform.position = Vector3.Lerp(right, left, moveTimer / moveTime);
				}
			}
			else
			{
				target = 0f;
				moveTimer = Mathf.MoveTowards(moveTimer, 0f, Time.deltaTime);
				if (shakeCount == maxShakeCount)
				{
					base.transform.position = Vector3.Lerp(this.finalPosition, left, moveTimer / moveTime);
				}
				else
				{
					base.transform.position = Vector3.Lerp(right, left, moveTimer / moveTime);
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
		yield break;
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x0007A478 File Offset: 0x00078878
	private IEnumerator ShakeHandFuriously()
	{
		float originalMoveTime = this.moveTime;
		float moveTimer = 0f;
		Vector2 startingPosition = base.transform.localPosition;
		Vector2 left = startingPosition - new Vector2(this.moveDistance, 0f);
		Vector2 right = startingPosition + new Vector2(this.moveDistance, 0f);
		for (;;)
		{
			moveTimer = Mathf.MoveTowards(moveTimer, this.moveTime, Time.deltaTime);
			if (moveTimer == this.moveTime)
			{
				this.moveTime *= -1f;
			}
			base.transform.localPosition = Vector3.Lerp(left, right, Extensions.Between(-originalMoveTime, originalMoveTime, moveTimer, false));
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x0007A494 File Offset: 0x00078894
	private IEnumerator DerpHand()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.derpHandTime);
			int i = UnityEngine.Random.Range(0, 4);
			float angle = UnityEngine.Random.Range(-this.derpHandMaxAngle, this.derpHandMaxAngle);
			Vector2 newPos = Vector2.zero;
			switch (i)
			{
			case 0:
				newPos = Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.Range(0.1f, 0.9f), 0.93f));
				break;
			case 1:
				newPos = Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.Range(0.1f, 0.9f), 0.07f));
				angle += 180f;
				break;
			case 2:
				newPos = Camera.main.ViewportToWorldPoint(new Vector2(0.07f, UnityEngine.Random.Range(0.1f, 0.9f)));
				angle += 90f;
				break;
			case 3:
				newPos = Camera.main.ViewportToWorldPoint(new Vector2(0.93f, UnityEngine.Random.Range(0.1f, 0.9f)));
				angle += 270f;
				break;
			}
			base.transform.localPosition = newPos;
			base.transform.rotation = Quaternion.Euler(0f, 0f, angle);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001B13 RID: 6931
	public Vector2 finalPosition;

	// Token: 0x04001B14 RID: 6932
	[Tooltip("Seconds required for the hand to move out")]
	public float time;

	// Token: 0x04001B15 RID: 6933
	public float derpHandTotalTime = 5f;

	// Token: 0x04001B16 RID: 6934
	public float derpHandTime = 0.05f;

	// Token: 0x04001B17 RID: 6935
	public float derpHandMaxAngle = 30f;

	// Token: 0x04001B18 RID: 6936
	[Header("Furious hand")]
	public float moveTime;

	// Token: 0x04001B19 RID: 6937
	public float moveDistance;

	// Token: 0x04001B1A RID: 6938
	private Vector3 startingPosition;

	// Token: 0x04001B1B RID: 6939
	private Transform phone;

	// Token: 0x04001B1C RID: 6940
	private float timer;

	// Token: 0x04001B1D RID: 6941
	private IEnumerator coroutine;
}
