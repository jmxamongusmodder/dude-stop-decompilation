using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200044D RID: 1101
public class PuzzleSitInEmptyBus_Bus : MonoBehaviour, TransitionProcessor
{
	// Token: 0x06001C33 RID: 7219 RVA: 0x0007681E File Offset: 0x00074C1E
	private void Start()
	{
		this.doorAnimation = this.door.GetComponent<Animator>();
		Audio.self.playOneShot("734bde8f-deef-4fff-945f-2194ef145777", 1f);
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x00076848 File Offset: 0x00074C48
	private void Update()
	{
		if (this.rideTimer == this.rideTime)
		{
			return;
		}
		if (this.rideTimer == 0f)
		{
			this.startingPosition = base.transform.position;
		}
		this.rideTimer = Mathf.MoveTowards(this.rideTimer, this.rideTime, Time.deltaTime);
		float t = Mathf.Sin(this.rideTimer / this.rideTime * 3.1415927f * 0.5f);
		base.transform.position = Vector3.Lerp(this.startingPosition, Vector3.zero, t);
		if (this.rideTimer == this.rideTime)
		{
			Audio.self.playLoopSound("1650b0d8-895c-4418-be77-c465e56acaf8");
			Audio.self.playOneShot("ad4c15f9-23e0-4a37-8981-f8f2a52ef473", 1f);
			this.doorAnimation.Play("busDoorOpen");
		}
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x00076924 File Offset: 0x00074D24
	public void DriveAway(bool monster)
	{
		if (monster)
		{
			Audio.self.playOneShot("b4fd8dd8-b5dd-41ee-aff6-96e9cdcf9d56", 1f);
			this.passenger.GetChild(1).gameObject.SetActive(false);
			this.passenger.GetChild(2).gameObject.SetActive(true);
		}
		base.StartCoroutine(this.DriveAway());
		if (monster)
		{
			Global.LevelCompleted(this.exitTime, true);
		}
		else
		{
			Global.LevelFailed(this.exitTime, true);
		}
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x000769AC File Offset: 0x00074DAC
	private IEnumerator DriveAway()
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		this.stopAnimation = false;
		Audio.self.playOneShot("fa04c052-5669-470c-8f92-db31dc534758", 1f);
		this.doorAnimation.Play("busDoorClose");
		Vector3 targetPosition = Camera.main.ViewportToWorldPoint(new Vector3(3f, 0.5f));
		this.startingPosition = base.transform.position;
		this.rideTimer = 0f;
		yield return null;
		yield return new WaitForSeconds(1f);
		Audio.self.stopLoopSound("1650b0d8-895c-4418-be77-c465e56acaf8", true);
		Audio.self.playOneShot("70b9a388-afad-4f96-b93a-c758975e5ce9", 1f);
		while (this.rideTimer != this.exitTime)
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
			this.rideTimer = Mathf.MoveTowards(this.rideTimer, this.exitTime, Time.deltaTime);
			float t = 1f - Mathf.Cos(this.rideTimer / this.exitTime * 3.1415927f * 0.5f);
			base.transform.localPosition = Vector3.Lerp(this.startingPosition, targetPosition, t);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x000769C7 File Offset: 0x00074DC7
	public void TransitionUpdate()
	{
		this.stopAnimation = true;
	}

	// Token: 0x04001A88 RID: 6792
	public Transform door;

	// Token: 0x04001A89 RID: 6793
	public Transform passenger;

	// Token: 0x04001A8A RID: 6794
	public float rideTime = 1.5f;

	// Token: 0x04001A8B RID: 6795
	public float exitTime = 1f;

	// Token: 0x04001A8C RID: 6796
	private Animator doorAnimation;

	// Token: 0x04001A8D RID: 6797
	private Vector3 startingPosition;

	// Token: 0x04001A8E RID: 6798
	private float rideTimer;

	// Token: 0x04001A8F RID: 6799
	private bool stopAnimation;
}
