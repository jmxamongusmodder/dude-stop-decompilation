using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class PuzzleMotherCall_Button : MonoBehaviour
{
	// Token: 0x06001B4D RID: 6989 RVA: 0x00070280 File Offset: 0x0006E680
	private void OnDrawGizmos()
	{
		if (this.fail)
		{
			return;
		}
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector2(this.maxX, base.transform.position.y + 1f), new Vector2(this.maxX, base.transform.position.y - 1f));
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x000702FC File Offset: 0x0006E6FC
	private void Start()
	{
		if (this.endScreen != null)
		{
			this.count = this.endScreen.Find("Timer").Find("Count");
		}
		foreach (PuzzleMotherCall_Button x in base.transform.parent.GetComponentsInChildren<PuzzleMotherCall_Button>())
		{
			if (!(x == this))
			{
				this.otherButton = x;
			}
		}
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x0007037C File Offset: 0x0006E77C
	private void Update()
	{
		if (this.fail && this.state == PuzzleMotherCall_Button.State.showingTimer)
		{
			this.phone.GetComponent<PuzzleMotherCall_Phone>().StopVibrating();
			this.timer += Time.deltaTime;
			if (this.smsCounter == 2 && !this.stateSet)
			{
				Global.setCompletionState(CompletionState.Good, null);
				base.StartCoroutine(this.DetachPhone());
				Audio.self.stopLoopSound("5f536f7d-2bd1-415a-8f88-bc9822ccce1e", this.phone, true);
				this.stateSet = true;
			}
			if (this.timer > 1f && ++this.smsCounter < this.count.childCount)
			{
				IEnumerator enumerator = this.count.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						transform.gameObject.SetActive(false);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this.count.GetChild(this.smsCounter).gameObject.SetActive(true);
				this.timer = 0f;
			}
		}
		if (this.fail)
		{
			return;
		}
		if (this.state == PuzzleMotherCall_Button.State.toBlackScreen)
		{
			this.timer = Mathf.MoveTowards(this.timer, this.fadeOutTime, Time.deltaTime);
			Color color = this.blackScreen.GetComponent<SpriteRenderer>().color;
			color.a = this.timer / this.fadeOutTime;
			this.blackScreen.GetComponent<SpriteRenderer>().color = color;
			if (this.timer == this.fadeOutTime)
			{
				this.callScreen.gameObject.SetActive(false);
				this.newSms.gameObject.SetActive(true);
				this.setRecievedSmsCount(0);
				this.state = PuzzleMotherCall_Button.State.onBlackScreen;
				this.timer = 0f;
				base.StartCoroutine(this.animateSms());
				Global.setCompletionState(CompletionState.Monster, null);
			}
		}
		else if (this.dragged)
		{
			Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			a.z = 0f;
			if (this.delta == -Vector3.one)
			{
				this.delta = a;
			}
			else
			{
				a -= this.delta;
				a.x += this.startingPosition.x;
				a.x = Mathf.Clamp(a.x, this.startingPosition.x, this.maxX);
				base.transform.localPosition = new Vector3(a.x, base.transform.localPosition.y);
			}
		}
		else if (base.transform.localPosition != this.startingPosition && this.startingPosition != -Vector3.one)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, this.startingPosition, Time.deltaTime * this.returnSpeed);
		}
		else if (this.swipe.gameObject.activeSelf)
		{
			this.otherButton.GetComponent<CircleCollider2D>().enabled = true;
			this.swipe.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000706FC File Offset: 0x0006EAFC
	private void OnDisable()
	{
		if (this.dragged)
		{
			this.dragged = false;
		}
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x00070710 File Offset: 0x0006EB10
	private void OnMouseOver()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00070739 File Offset: 0x0006EB39
	private void OnMouseExit()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x00070758 File Offset: 0x0006EB58
	private void OnMouseDown()
	{
		if (this.fail)
		{
			return;
		}
		if (this.startingPosition == -Vector3.one)
		{
			this.startingPosition = base.transform.localPosition;
		}
		this.dragged = true;
		this.otherButton.GetComponent<CircleCollider2D>().enabled = false;
		this.swipe.gameObject.SetActive(true);
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000707C8 File Offset: 0x0006EBC8
	private void OnMouseUp()
	{
		this.dragged = false;
		this.delta = -Vector3.one;
		if (this.fail)
		{
			this.otherButton.GetComponent<SpriteRenderer>().enabled = false;
			this.otherButton.GetComponent<CircleCollider2D>().enabled = false;
			base.GetComponent<SpriteRenderer>().enabled = false;
			base.GetComponent<CircleCollider2D>().enabled = false;
			this.endScreen.gameObject.SetActive(true);
			this.state = PuzzleMotherCall_Button.State.showingTimer;
			Audio.self.playOneShot("28f766c8-8ee3-4004-b71e-bcda8202a524", 1f);
			Audio.self.playLoopSound("5f536f7d-2bd1-415a-8f88-bc9822ccce1e", this.phone);
			Global.self.currPuzzle.GetComponent<AudioVoice_MamaCall>().answerPhone();
		}
		else if (Mathf.Abs(base.transform.localPosition.x - this.maxX) < 0.1f)
		{
			this.state = PuzzleMotherCall_Button.State.toBlackScreen;
			Audio.self.playOneShot("d4404031-b197-478d-b7fe-c37cf92b5fef", 1f);
			this.otherButton.GetComponent<SpriteRenderer>().enabled = false;
			this.otherButton.GetComponent<CircleCollider2D>().enabled = false;
			base.GetComponent<SpriteRenderer>().enabled = false;
			base.GetComponent<CircleCollider2D>().enabled = false;
			this.swipe.gameObject.SetActive(false);
			this.phone.GetComponent<PuzzleMotherCall_Phone>().StopVibrating();
			Global.PauseArrows(1f);
			Global.self.currPuzzle.GetComponent<AudioVoice_MamaCall>().dropCall();
		}
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x0007094C File Offset: 0x0006ED4C
	private IEnumerator animateSms()
	{
		Global.self.canBePaused = false;
		Global.PauseArrows(this.waitTime + this.fadeInTime + 1f);
		int recievedSmsCount = 0;
		this.timer = 0f;
		for (;;)
		{
			PuzzleMotherCall_Button.State state = this.state;
			if (state != PuzzleMotherCall_Button.State.onBlackScreen)
			{
				if (state != PuzzleMotherCall_Button.State.offBlackScreen)
				{
					if (state == PuzzleMotherCall_Button.State.showingSms)
					{
						this.timer = Mathf.MoveTowards(this.timer, 0f, Time.deltaTime);
						if (this.timer <= 0f)
						{
							this.setRecievedSmsCount(++recievedSmsCount);
							this.timer = this.timeBetweenSms + UnityEngine.Random.Range(0f, 1f);
							Global.PauseArrows(this.timer + 0.5f);
						}
						if (recievedSmsCount == this.maxSmsCount)
						{
							this.state = PuzzleMotherCall_Button.State.completedSms;
						}
					}
				}
				else
				{
					this.timer = Mathf.MoveTowards(this.timer, this.fadeInTime, Time.deltaTime);
					Color color = this.blackScreen.GetComponent<SpriteRenderer>().color;
					color.a = 1f - this.timer / this.fadeInTime;
					this.blackScreen.GetComponent<SpriteRenderer>().color = color;
					if (this.timer == this.fadeInTime)
					{
						this.state = PuzzleMotherCall_Button.State.showingSms;
						this.timer = 0f;
					}
				}
			}
			else
			{
				this.timer = Mathf.MoveTowards(this.timer, this.waitTime, Time.deltaTime);
				if (this.timer == this.waitTime)
				{
					this.timer = 0f;
					this.state = PuzzleMotherCall_Button.State.offBlackScreen;
				}
			}
			if (this.state == PuzzleMotherCall_Button.State.completedSms)
			{
				break;
			}
			yield return null;
		}
		Global.self.canBePaused = true;
		base.StartCoroutine(this.DetachPhone());
		yield break;
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x00070968 File Offset: 0x0006ED68
	private void setRecievedSmsCount(int num)
	{
		string text = LineTranslator.translateText(this.smsCount.guid, this.smsCount.type, false, string.Empty);
		text = text.Replace("(#)", num.ToString());
		this.smsCount.setTextNoTranslation(text);
		if (!Global.self.pack10CutsceneActive)
		{
			Audio.self.playOneShot("488b15d0-5e77-464c-bcc5-8dbc38d4e737", 1f);
		}
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x000709E0 File Offset: 0x0006EDE0
	private IEnumerator DetachPhone()
	{
		Global.self.canBePaused = false;
		Global.PauseArrows(this.fadeOutTime + 0.5f);
		SpriteRenderer rend = this.blackScreen.GetComponent<SpriteRenderer>();
		Color c = rend.color;
		this.phone.GetComponent<PuzzleMotherCall_Phone>().StopVibrating();
		float timer = 0f;
		while (timer != this.fadeOutTime)
		{
			timer = Mathf.MoveTowards(timer, this.fadeOutTime, Time.deltaTime);
			c.a = Mathf.Lerp(0f, 1f, timer / this.fadeOutTime);
			rend.color = c;
			yield return null;
		}
		Global.self.canBePaused = true;
		this.phone.GetComponent<PuzzleMotherCall_Phone>().Move();
		UnityEngine.Object.Destroy(this.newSms.gameObject);
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
		yield break;
	}

	// Token: 0x0400197A RID: 6522
	public bool fail;

	// Token: 0x0400197B RID: 6523
	public float whiten = 0.25f;

	// Token: 0x0400197C RID: 6524
	[Header("Stuff for Answer button")]
	public Transform endScreen;

	// Token: 0x0400197D RID: 6525
	[Header("Stuff for Decline button")]
	public Transform callScreen;

	// Token: 0x0400197E RID: 6526
	public Transform blackScreen;

	// Token: 0x0400197F RID: 6527
	public float fadeOutTime = 0.3f;

	// Token: 0x04001980 RID: 6528
	public float waitTime = 1f;

	// Token: 0x04001981 RID: 6529
	public Transform newSms;

	// Token: 0x04001982 RID: 6530
	public LineTranslator smsCount;

	// Token: 0x04001983 RID: 6531
	public int maxSmsCount = 5;

	// Token: 0x04001984 RID: 6532
	public float fadeInTime = 0.2f;

	// Token: 0x04001985 RID: 6533
	public float timeBetweenSms = 1f;

	// Token: 0x04001986 RID: 6534
	public Transform swipe;

	// Token: 0x04001987 RID: 6535
	public float returnSpeed;

	// Token: 0x04001988 RID: 6536
	public float maxX;

	// Token: 0x04001989 RID: 6537
	[Header("Stuff for scrollable pack")]
	public Transform phone;

	// Token: 0x0400198A RID: 6538
	private PuzzleMotherCall_Button.State state;

	// Token: 0x0400198B RID: 6539
	private PuzzleMotherCall_Button otherButton;

	// Token: 0x0400198C RID: 6540
	private Transform count;

	// Token: 0x0400198D RID: 6541
	private Vector3 startingPosition = -Vector3.one;

	// Token: 0x0400198E RID: 6542
	private Vector3 delta = -Vector3.one;

	// Token: 0x0400198F RID: 6543
	private bool dragged;

	// Token: 0x04001990 RID: 6544
	private float timer;

	// Token: 0x04001991 RID: 6545
	private int smsCounter;

	// Token: 0x04001992 RID: 6546
	private bool stateSet;

	// Token: 0x02000431 RID: 1073
	private enum State
	{
		// Token: 0x04001994 RID: 6548
		none,
		// Token: 0x04001995 RID: 6549
		showingTimer,
		// Token: 0x04001996 RID: 6550
		toBlackScreen,
		// Token: 0x04001997 RID: 6551
		onBlackScreen,
		// Token: 0x04001998 RID: 6552
		offBlackScreen,
		// Token: 0x04001999 RID: 6553
		showingSms,
		// Token: 0x0400199A RID: 6554
		completedSms
	}
}
