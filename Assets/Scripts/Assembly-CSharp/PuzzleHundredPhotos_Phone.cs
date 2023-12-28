using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000422 RID: 1058
public class PuzzleHundredPhotos_Phone : Draggable
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0006B834 File Offset: 0x00069C34
	private SpringJoint2D spring
	{
		get
		{
			return base.GetComponent<SpringJoint2D>();
		}
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0006B83C File Offset: 0x00069C3C
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawPoint(this.tablePosition, 0.5f);
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0006B84E File Offset: 0x00069C4E
	private void Start()
	{
		base.StartCoroutine(this.SlidingOutCoroutine());
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0006B860 File Offset: 0x00069C60
	private void Update()
	{
		this.FollowCat();
		float num = base.transform.position.y - this.tablePosition.y;
		if (num > 0.5f && this.phoneBelow)
		{
			Audio.self.playOneShot("3f525994-81fc-4e72-8390-9fb9ad57de0f", 1f);
			this.phoneBelow = false;
		}
		else if (num < 0.5f && !this.phoneBelow)
		{
			Audio.self.playOneShot("3f525994-81fc-4e72-8390-9fb9ad57de0f", 1f);
			this.phoneBelow = true;
		}
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x0006B8FC File Offset: 0x00069CFC
	public void disablePuzzle()
	{
		this.ReturnToTable();
		base.enabled = false;
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x0006B90C File Offset: 0x00069D0C
	private void OnDisable()
	{
		if (!this.levelFinished)
		{
			this.spring.enabled = false;
		}
		if (this.onTable && base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.TableFollowingCoroutine());
		}
		if (this.waitingCouroutine != null)
		{
			base.StopCoroutine(this.waitingCouroutine);
		}
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x0006B970 File Offset: 0x00069D70
	public override void OnMouseDown()
	{
		if (!base.enabled || this.activeCoroutine)
		{
			return;
		}
		base.OnMouseDown();
		this.spring.enabled = false;
		if (this.waitingCouroutine != null)
		{
			base.StopCoroutine(this.waitingCouroutine);
		}
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0006B9C0 File Offset: 0x00069DC0
	public override void OnMouseUp()
	{
		if (!base.enabled || !this.dragged || this.activeCoroutine)
		{
			return;
		}
		this.spring.enabled = true;
		float num = Vector2.Distance(base.transform.position, this.tablePosition);
		float num2 = Vector2.Distance(base.transform.position, this.centerPosition);
		if (num > num2)
		{
			this.spring.connectedAnchor = this.cat.position;
			base.StartCoroutine(this.FadingCoroutine(false));
			this.onTable = false;
			this.photoButton.enabled = true;
			this.SetTriggerCollider(false);
		}
		else
		{
			this.spring.connectedAnchor = this.tablePosition;
			base.StartCoroutine(this.FadingCoroutine(true));
			this.onTable = true;
			this.photoButton.enabled = false;
			this.SetTriggerCollider(true);
			if (this.photoMade)
			{
				this.levelFinished = true;
				if (this.monster)
				{
					this.waitingCouroutine = base.StartCoroutine(this.WaitingBeforeEndCoroutine(this.shortWait, true));
				}
				else
				{
					this.waitingCouroutine = base.StartCoroutine(this.WaitingBeforeEndCoroutine(this.shortWait, false));
				}
			}
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0006BB25 File Offset: 0x00069F25
	public void ReturnToTable()
	{
		this.onTable = true;
		this.dragged = false;
		this.spring.enabled = false;
		base.StartCoroutine(this.FadingCoroutine(true));
		base.StartCoroutine(this.ReturningCoroutine());
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0006BB5C File Offset: 0x00069F5C
	private void FollowCat()
	{
		if (this.onTable || this.activeCoroutine)
		{
			return;
		}
		this.spring.connectedAnchor = this.cat.position;
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x0006BB90 File Offset: 0x00069F90
	private void SetTriggerCollider(bool p)
	{
		foreach (Collider2D collider2D in base.GetComponents<Collider2D>())
		{
			if (collider2D.isTrigger)
			{
				collider2D.enabled = p;
			}
			else
			{
				collider2D.enabled = !p;
			}
		}
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0006BBE0 File Offset: 0x00069FE0
	private IEnumerator SlidingOutCoroutine()
	{
		Vector2 start = base.transform.position;
		float slidingTime = 1f;
		float timer = 0f;
		this.activeCoroutine = true;
		if (this.photoButton.photosMade == this.photoButton.amountForReturn)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_HundredPhotos>().onPickPhoneAfter4();
		}
		while (timer != slidingTime)
		{
			timer = Mathf.MoveTowards(timer, slidingTime, Time.deltaTime);
			float t = Mathf.Sin(timer / slidingTime * 3.1415927f * 0.5f);
			base.transform.position = Vector2.Lerp(start, this.tablePosition, t);
			yield return null;
		}
		this.spring.connectedAnchor = this.tablePosition;
		this.spring.enabled = true;
		this.activeCoroutine = false;
		this.onTable = true;
		base.StartCoroutine(this.ScalingCoroutine());
		yield break;
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x0006BBFC File Offset: 0x00069FFC
	private IEnumerator ScalingCoroutine()
	{
		float initialDistance = Vector2.Distance(this.tablePosition, this.centerPosition);
		for (;;)
		{
			float dist = Vector2.Distance(base.transform.localPosition, this.tablePosition);
			Vector2 tScale = new Vector2(this.tableScale, this.tableScale);
			Vector2 cScale = Vector2.one;
			float t = 1f - dist / initialDistance;
			base.transform.localScale = Vector2.Lerp(cScale, tScale, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x0006BC18 File Offset: 0x0006A018
	private IEnumerator TableFollowingCoroutine()
	{
		for (;;)
		{
			this.spring.connectedAnchor = base.transform.parent.TransformPoint(this.tablePosition);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x0006BC34 File Offset: 0x0006A034
	private IEnumerator ReturningCoroutine()
	{
		this.dragEnabled = false;
		this.activeCoroutine = true;
		this.photoButton.enabled = false;
		Vector2 start = base.transform.localPosition;
		float timer = 0f;
		while (timer != this.returnTime)
		{
			timer = Mathf.MoveTowards(timer, this.returnTime, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(start, this.tablePosition, timer / this.returnTime);
			yield return null;
		}
		this.activeCoroutine = false;
		this.dragEnabled = true;
		if (base.enabled)
		{
			this.waitingCouroutine = base.StartCoroutine(this.WaitingBeforeEndCoroutine(this.waitBeforeEnd, false));
		}
		yield break;
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x0006BC50 File Offset: 0x0006A050
	private IEnumerator FadingCoroutine(bool fadeIn)
	{
		float fadeTimer = 0f;
		SpriteRenderer rend = this.blackScreen.GetComponent<SpriteRenderer>();
		Color color = rend.color;
		Color newColor = color;
		newColor.a = (float)((!fadeIn) ? 0 : 1);
		yield return null;
		while (fadeTimer != this.screenFadeTime)
		{
			fadeTimer = Mathf.MoveTowards(fadeTimer, this.screenFadeTime, Time.deltaTime);
			float t = Mathf.Sin(fadeTimer / this.screenFadeTime * 3.1415927f * 0.5f);
			this.blackScreen.GetComponent<SpriteRenderer>().color = Color.Lerp(color, newColor, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x0006BC74 File Offset: 0x0006A074
	private IEnumerator WaitingBeforeEndCoroutine(float waitTime, bool monster)
	{
		yield return new WaitForSeconds(waitTime);
		if (monster)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
		yield break;
	}

	// Token: 0x04001913 RID: 6419
	public Transform cat;

	// Token: 0x04001914 RID: 6420
	public PuzzleHundredPhotos_Button photoButton;

	// Token: 0x04001915 RID: 6421
	public Transform blackScreen;

	// Token: 0x04001916 RID: 6422
	public Vector2 centerPosition;

	// Token: 0x04001917 RID: 6423
	public Vector2 tablePosition;

	// Token: 0x04001918 RID: 6424
	public float tableScale = 0.413f;

	// Token: 0x04001919 RID: 6425
	public float screenFadeTime = 0.3f;

	// Token: 0x0400191A RID: 6426
	public float returnTime = 1f;

	// Token: 0x0400191B RID: 6427
	public float waitBeforeEnd = 3f;

	// Token: 0x0400191C RID: 6428
	public float shortWait = 1f;

	// Token: 0x0400191D RID: 6429
	[HideInInspector]
	public bool onTable = true;

	// Token: 0x0400191E RID: 6430
	[HideInInspector]
	public bool monster;

	// Token: 0x0400191F RID: 6431
	[HideInInspector]
	public bool photoMade;

	// Token: 0x04001920 RID: 6432
	private bool levelFinished;

	// Token: 0x04001921 RID: 6433
	private bool activeCoroutine;

	// Token: 0x04001922 RID: 6434
	private Coroutine waitingCouroutine;

	// Token: 0x04001923 RID: 6435
	private bool phoneBelow = true;
}
