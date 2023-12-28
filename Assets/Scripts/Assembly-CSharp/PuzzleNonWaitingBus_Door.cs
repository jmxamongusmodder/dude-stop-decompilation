using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000437 RID: 1079
[EnabledManually]
public class PuzzleNonWaitingBus_Door : MonoBehaviour
{
	// Token: 0x06001B7D RID: 7037 RVA: 0x00071BFA File Offset: 0x0006FFFA
	private void Awake()
	{
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x00071BFC File Offset: 0x0006FFFC
	private void Start()
	{
		this.anim = base.transform.parent.GetComponent<Animator>();
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x00071C14 File Offset: 0x00070014
	private void Update()
	{
	}

	// Token: 0x06001B80 RID: 7040 RVA: 0x00071C16 File Offset: 0x00070016
	private void OnMouseDown()
	{
		this.ActivateDoor();
	}

	// Token: 0x06001B81 RID: 7041 RVA: 0x00071C20 File Offset: 0x00070020
	private void CheckForDeparture()
	{
		if (this.everybodyIsIn)
		{
			base.transform.parent.parent.GetComponent<PuzzleNonWaitingBus_Bus>().DriveAway(false, true);
		}
		else if (this.twoAreIn)
		{
			base.transform.parent.parent.GetComponent<PuzzleNonWaitingBus_Bus>().DriveAway(true, true);
		}
		else
		{
			Debug.LogWarning("Closed door before two people came inside! This should not happen");
		}
	}

	// Token: 0x06001B82 RID: 7042 RVA: 0x00071C8F File Offset: 0x0007008F
	public void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.hoverWhiten);
	}

	// Token: 0x06001B83 RID: 7043 RVA: 0x00071CB8 File Offset: 0x000700B8
	public void OnMouseExit()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x06001B84 RID: 7044 RVA: 0x00071CD4 File Offset: 0x000700D4
	public void ResetMyDoor()
	{
		if (this.anim == null)
		{
			return;
		}
		this.anim.SetTrigger("reset");
		this.isOpen = false;
		this.blocked = false;
		this.waitingOutside = false;
		this.everybodyIsIn = false;
		this.passengersMoving = false;
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x00071D28 File Offset: 0x00070128
	public void ActivateDoor()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("New State") || this.blocked)
		{
			return;
		}
		if (!this.isOpen && this.everybodyIsIn)
		{
			return;
		}
		if (!this.passengersMoving)
		{
			this.passenger.SetTrigger("moveAll");
			this.passengersMoving = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>().MoveGuy();
		}
		if (this.isOpen)
		{
			Audio.self.playOneShot("ebf6a6d3-0c2b-4f2c-ba9d-87bf89287c45", 1f);
			this.anim.SetTrigger("close");
			this.CheckForDeparture();
			Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>().OnDoorClose();
		}
		else
		{
			Audio.self.playOneShot("f681a02d-6e43-488f-99e1-bccc6be7ccdf", 1f);
			this.anim.SetTrigger("open");
			if (this.waitingOutside)
			{
				this.passenger.enabled = true;
				this.blocked = true;
				this.waitingOutside = false;
			}
			Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>().OnDoorOpen();
		}
		this.isOpen = !this.isOpen;
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x00071E74 File Offset: 0x00070274
	public void moveAgain()
	{
		base.StartCoroutine(this.movePassengerAgain());
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x00071E84 File Offset: 0x00070284
	private IEnumerator movePassengerAgain()
	{
		GlitchEffectController.self.startGlitch(0.5f);
		Audio.self.playOneShot("9a0f1142-2899-4c5a-99c1-2fe91c7c0b12", 1f);
		this.GetComponentInPuzzleStats<PuzzleNonWaitingBus_Bus>().DriveAwayKidnOf();
		yield return new WaitForSeconds(1f);
		AudioVoice_NonWaitingBus audio = Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>();
		this.GetComponentInPuzzleStats<PuzzleNonWaitingBus_Bus>().ResetMyRide();
		int index = 0;
		while (!audio.canMoveGuy)
		{
			if (index < 4)
			{
				this.duckSprite.gameObject.SetActive(!this.duckSprite.gameObject.activeInHierarchy);
				this.duckSprite.position = Extensions.GetRandomPointOnScreen(Vector2.one * 0.8f);
				this.duckSprite.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-60f, 60f));
				index++;
			}
			yield return new WaitForSeconds(0.1f);
		}
		this.duckSprite.gameObject.SetActive(false);
		this.passenger.enabled = true;
		this.everybodyIsIn = false;
		yield break;
	}

	// Token: 0x040019C4 RID: 6596
	[HideInInspector]
	public bool isOpen;

	// Token: 0x040019C5 RID: 6597
	public Animator passenger;

	// Token: 0x040019C6 RID: 6598
	public Transform latePassenger;

	// Token: 0x040019C7 RID: 6599
	public bool blocked;

	// Token: 0x040019C8 RID: 6600
	public bool waitingOutside;

	// Token: 0x040019C9 RID: 6601
	public bool twoAreIn;

	// Token: 0x040019CA RID: 6602
	public bool everybodyIsIn;

	// Token: 0x040019CB RID: 6603
	public float hoverWhiten = 0.3f;

	// Token: 0x040019CC RID: 6604
	private Animator anim;

	// Token: 0x040019CD RID: 6605
	private bool passengersMoving;

	// Token: 0x040019CE RID: 6606
	public Transform duckSprite;
}
