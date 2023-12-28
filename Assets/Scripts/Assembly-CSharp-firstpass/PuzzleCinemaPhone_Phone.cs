using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class PuzzleCinemaPhone_Phone : Draggable
{
	// Token: 0x06001924 RID: 6436 RVA: 0x0005BFA2 File Offset: 0x0005A3A2
	private void Start()
	{
		this.volume = base.GetComponentsInChildren<PuzzleCinemaPhone_Volume>(true)[0];
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x0005BFB3 File Offset: 0x0005A3B3
	private void Update()
	{
		this.CheckMouseClick();
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x0005BFBC File Offset: 0x0005A3BC
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.floating = false;
		this.startingPoint = base.transform.position.y;
		if (this.endingCoroutine != null && this.coroutineCanBeStopped)
		{
			base.StopCoroutine(this.endingCoroutine);
			this.endingCoroutine = null;
		}
		Global.self.currPuzzle.GetComponent<AudioVoice_CinemaPhone>().pullOut();
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x0005C038 File Offset: 0x0005A438
	public override void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		float f = this.startingPoint - base.transform.position.y;
		this.floating = true;
		if (Mathf.Abs(f) < this.threshold)
		{
			this.floatingTarget = ((!this.up) ? this.limit.bottomVal : this.limit.topVal);
			this.EnableButtons(false);
		}
		else if (this.up)
		{
			this.GoDown();
		}
		else
		{
			this.floatingTarget = this.limit.topVal;
			this.up = true;
		}
		base.StartCoroutine(this.SlidingCoroutine(1f));
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x0005C104 File Offset: 0x0005A504
	private void CheckMouseClick()
	{
		if (!base.enabled || !Input.GetMouseButtonDown(0))
		{
			return;
		}
		if (!this.up)
		{
			return;
		}
		if (base.GetComponent<Collider2D>().OverlapPoint(Camera.main.GetMousePosition()))
		{
			return;
		}
		this.floating = true;
		this.GoDown();
		base.StartCoroutine(this.SlidingCoroutine(2f));
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x0005C174 File Offset: 0x0005A574
	private void GoDown()
	{
		this.floatingTarget = this.limit.bottomVal;
		this.up = false;
		if (this.volume.IsOn())
		{
			this.endingCoroutine = base.StartCoroutine(this.MonsterCoroutine());
			Global.self.currPuzzle.GetComponent<AudioVoice_CinemaPhone>().leftUnmuted();
		}
		else if (this.volume.IsOff())
		{
			this.endingCoroutine = base.StartCoroutine(this.VictoryCoroutine());
		}
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x0005C1F8 File Offset: 0x0005A5F8
	private void EnableButtons(bool status)
	{
		foreach (Collider2D collider2D in base.GetComponentsInChildren<Collider2D>())
		{
			if (!(collider2D.transform == base.transform))
			{
				if (collider2D.gameObject.activeInHierarchy)
				{
					collider2D.enabled = status;
				}
			}
		}
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x0005C25C File Offset: 0x0005A65C
	private IEnumerator VictoryCoroutine()
	{
		while (this.floating)
		{
			yield return null;
		}
		Global.LevelFailed(0f, true);
		yield break;
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x0005C278 File Offset: 0x0005A678
	private IEnumerator MonsterCoroutine()
	{
		while (this.floating)
		{
			yield return null;
		}
		yield return new WaitForSeconds(this.waitTime);
		Audio.self.playLoopSound("c9af34bc-e8cf-42d4-a858-e2afb91fe57a", base.transform);
		this.motherCall.gameObject.SetActive(true);
		this.settings.GetComponent<PuzzleCinemaPhone_Settings>().Disable();
		this.coroutineCanBeStopped = false;
		yield return base.StartCoroutine(this.VibrationCoroutine());
		yield return new WaitForSeconds(this.waitBetweenVibrations);
		yield return base.StartCoroutine(this.VibrationCoroutine());
		yield return new WaitForSeconds(this.waitBetweenVibrations);
		yield return base.StartCoroutine(this.VibrationCoroutine());
		this.floating = true;
		base.GetComponent<Collider2D>().enabled = false;
		this.floatingTarget = this.limit.topVal;
		base.StartCoroutine(this.SlidingCoroutine(1f));
		while (this.floating)
		{
			yield return null;
		}
		Global.LevelCompleted(0f, true);
		for (;;)
		{
			yield return new WaitForSeconds(this.waitBetweenVibrations);
			yield return base.StartCoroutine(this.VibrationCoroutine());
		}
		yield break;
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x0005C294 File Offset: 0x0005A694
	private IEnumerator VibrationCoroutine()
	{
		float timer = 0f;
		Vector2 start = base.transform.localPosition;
		while (timer < this.vibrationTime)
		{
			timer = Mathf.MoveTowards(timer, this.vibrationTime, Time.deltaTime);
			bool left = (int)(timer * 10f) % 2 == 1;
			float sign = (float)((!left) ? 1 : -1);
			base.transform.localPosition = new Vector2(start.x + this.vibrationShift * sign, base.transform.localPosition.y);
			yield return null;
		}
		base.transform.localPosition = new Vector3(start.x, base.transform.localPosition.y);
		yield break;
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x0005C2B0 File Offset: 0x0005A6B0
	private IEnumerator SlidingCoroutine(float extraSpeed = 1f)
	{
		while (this.floating)
		{
			float newY = Mathf.MoveTowards(base.transform.position.y, this.floatingTarget, this.movingSpeed * extraSpeed * Time.deltaTime);
			base.transform.SetY(newY, false);
			if (newY == this.floatingTarget)
			{
				this.floating = false;
				this.EnableButtons(true);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400171D RID: 5917
	public float threshold;

	// Token: 0x0400171E RID: 5918
	public float movingSpeed = 1f;

	// Token: 0x0400171F RID: 5919
	private float startingPoint;

	// Token: 0x04001720 RID: 5920
	private bool floating;

	// Token: 0x04001721 RID: 5921
	private float floatingTarget;

	// Token: 0x04001722 RID: 5922
	private PuzzleCinemaPhone_Volume volume;

	// Token: 0x04001723 RID: 5923
	[Header("Level end")]
	public Transform settings;

	// Token: 0x04001724 RID: 5924
	public Transform motherCall;

	// Token: 0x04001725 RID: 5925
	public float waitTime;

	// Token: 0x04001726 RID: 5926
	public float vibrationTime;

	// Token: 0x04001727 RID: 5927
	public float vibrationShift;

	// Token: 0x04001728 RID: 5928
	public float waitBetweenVibrations;

	// Token: 0x04001729 RID: 5929
	private Coroutine endingCoroutine;

	// Token: 0x0400172A RID: 5930
	private bool coroutineCanBeStopped = true;

	// Token: 0x0400172B RID: 5931
	private bool up;
}
