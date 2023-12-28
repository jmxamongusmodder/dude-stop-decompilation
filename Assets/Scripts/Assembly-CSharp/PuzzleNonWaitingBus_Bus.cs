using System;
using System.Collections;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000436 RID: 1078
public class PuzzleNonWaitingBus_Bus : MonoBehaviour
{
	// Token: 0x06001B74 RID: 7028 RVA: 0x00071541 File Offset: 0x0006F941
	private void Start()
	{
		Audio.self.playOneShot("82970c99-7c9e-4a25-ab8c-ac52b2e0b31a", 1f);
		this.startingPosition = base.transform.localPosition;
		base.StartCoroutine(this.MoveBusIn(0f));
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x0007157B File Offset: 0x0006F97B
	private void Update()
	{
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x00071580 File Offset: 0x0006F980
	public void ResetMyRide()
	{
		if (this.drivingAwayCoroutine != null)
		{
			base.StopCoroutine(this.drivingAwayCoroutine);
			this.drivingAwayCoroutine = null;
			this.stopAnimation = true;
			if (this.busSound.isValid())
			{
				this.busSound.stop(STOP_MODE.IMMEDIATE);
			}
		}
		base.StartCoroutine(this.MoveBusIn(this.rideTime));
		this.passengers.GetComponent<Animator>().SetTrigger("reset");
		this.door.ResetMyDoor();
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x00071604 File Offset: 0x0006FA04
	private IEnumerator MoveBusIn(float timer)
	{
		this.rideTimer = timer;
		while (this.rideTime != this.rideTimer)
		{
			this.rideTimer = Mathf.MoveTowards(this.rideTimer, this.rideTime, Time.deltaTime);
			float t = Mathf.Sin(this.rideTimer / this.rideTime * 3.1415927f * 0.5f);
			base.transform.localPosition = Vector3.Lerp(this.startingPosition, Vector3.zero, t);
			yield return null;
		}
		base.transform.localPosition = Vector3.zero;
		foreach (PuzzleNonWaitingBus_Driver puzzleNonWaitingBus_Driver in this.GetComponentsInPuzzleStats(false))
		{
			puzzleNonWaitingBus_Driver.enabled = true;
		}
		this.GetComponentInPuzzleStats<PuzzleNonWaitingBus_Door>().enabled = true;
		Audio.self.playLoopSound("1650b0d8-895c-4418-be77-c465e56acaf8", base.transform);
		yield break;
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x00071628 File Offset: 0x0006FA28
	public void DriveAway(bool monster, bool end = true)
	{
		if (end && !Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>().canEnd())
		{
			return;
		}
		this.drivingAwayCoroutine = base.StartCoroutine(this.DriveAway(false));
		this.passengers.GetChild(0).SetParent(base.transform, true);
		this.passengers.GetChild(0).SetParent(base.transform, true);
		if (!end)
		{
			this.passengers.GetChild(0).SetParent(base.transform, true);
			return;
		}
		if (monster)
		{
			Global.LevelCompleted(this.rideTime + 1f, true);
		}
		else
		{
			this.passengers.GetChild(0).SetParent(base.transform, true);
			Global.LevelFailed(this.rideTime + 1f, true);
		}
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x00071700 File Offset: 0x0006FB00
	public void DriveAwayKidnOf()
	{
		this.drivingAwayCoroutine = base.StartCoroutine(this.DriveAway(true));
		this.passengers.GetChild(0).SetParent(base.transform, true);
		this.passengers.GetChild(0).SetParent(base.transform, true);
		this.passengers.GetChild(0).SetParent(base.transform, true);
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x00071768 File Offset: 0x0006FB68
	private IEnumerator DriveAway(bool shorty = false)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		this.stopAnimation = false;
		Vector3 targetPosition = Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 0.5f));
		this.startingPosition = base.transform.position;
		this.rideTimer = 0f;
		if (!shorty)
		{
			yield return new WaitForSeconds(1f);
		}
		Audio.self.stopLoopSound("1650b0d8-895c-4418-be77-c465e56acaf8", base.transform, true);
		this.busSound = Audio.self.playOneShot("a10a6e56-2759-4ca4-a9ea-e89790acbe5b", 1f);
		while (this.rideTimer != this.rideTime)
		{
			if (this.stopAnimation)
			{
				break;
			}
			if (!GeometryUtility.TestPlanesAABB(planes, base.transform.GetChild(0).GetComponent<Renderer>().bounds))
			{
				base.gameObject.SetActive(false);
				break;
			}
			if (shorty && this.rideTimer > this.rideTime * 0.5f)
			{
				break;
			}
			this.rideTimer = Mathf.MoveTowards(this.rideTimer, this.rideTime, Time.deltaTime);
			float t = 1f - Mathf.Cos(this.rideTimer / this.rideTime * 3.1415927f * 0.5f);
			base.transform.localPosition = Vector3.Lerp(this.startingPosition, targetPosition, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x0007178A File Offset: 0x0006FB8A
	public void TransitionUpdate()
	{
		this.stopAnimation = true;
	}

	// Token: 0x040019BB RID: 6587
	public float rideTime = 1.5f;

	// Token: 0x040019BC RID: 6588
	public Transform passengers;

	// Token: 0x040019BD RID: 6589
	public PuzzleNonWaitingBus_Door door;

	// Token: 0x040019BE RID: 6590
	[Range(0f, 1f)]
	public float startRidePosition;

	// Token: 0x040019BF RID: 6591
	private Vector3 startingPosition;

	// Token: 0x040019C0 RID: 6592
	private float rideTimer;

	// Token: 0x040019C1 RID: 6593
	private bool stopAnimation;

	// Token: 0x040019C2 RID: 6594
	private Coroutine drivingAwayCoroutine;

	// Token: 0x040019C3 RID: 6595
	private EventInstance busSound;
}
