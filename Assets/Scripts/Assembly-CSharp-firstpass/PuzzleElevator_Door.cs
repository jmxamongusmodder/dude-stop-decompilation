using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000400 RID: 1024
public class PuzzleElevator_Door : MonoBehaviour, TransitionProcessor
{
	// Token: 0x060019FF RID: 6655 RVA: 0x00063B8C File Offset: 0x00061F8C
	private void Awake()
	{
		this.renderer = base.GetComponentInChildren<SpriteRenderer>();
		this.renderer.material.SetFloat("_Top", 0f);
		this.renderer.material.SetFloat("_Distance", 0f);
		if (this.leftDoor)
		{
			this.renderer.material.SetFloat("_Angle", -1.5707964f);
			this.renderer.material.SetFloat("_Left", 0f);
		}
		else
		{
			this.renderer.material.SetFloat("_Angle", 1.5707964f);
			this.renderer.material.SetFloat("_Left", 1f);
		}
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x00063C51 File Offset: 0x00062051
	private void Start()
	{
		this.defaultBehaviour = base.StartCoroutine(this.OpeningCoroutine());
		if (this.leftDoor)
		{
			Audio.self.playLoopSound("b578f498-5629-4407-9cda-acb559786544");
		}
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x00063C7F File Offset: 0x0006207F
	private void Update()
	{
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x00063C81 File Offset: 0x00062081
	private void OnDisable()
	{
		if (this.leftDoor)
		{
		}
		if (this.brokenSoundPlaying && this.leftDoor)
		{
			Audio.self.stopLoopSound("fc6a8429-7782-4f00-9e67-7981f2c6dd1e", base.transform, true);
			this.brokenSoundPlaying = false;
		}
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x00063CC4 File Offset: 0x000620C4
	private IEnumerator OpeningCoroutine()
	{
		if (this.leftDoor)
		{
			Audio.self.playOneShot("69cc5490-f39b-4830-92bb-3942419181d1", 1f);
		}
		this.startPosition = base.transform.localPosition;
		float openingTimer = 0f;
		Vector2 end = new Vector2(this.openPosition, this.startPosition.y);
		while (openingTimer != this.openingTime)
		{
			openingTimer = Mathf.MoveTowards(openingTimer, this.openingTime, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(this.startPosition, end, openingTimer / this.openingTime);
			yield return null;
		}
		this.doorsOpened = true;
		foreach (PuzzleElevator_Button puzzleElevator_Button in this.GetComponentsInPuzzleStats(false))
		{
			puzzleElevator_Button.DoorsOpened();
		}
		yield return new WaitForSeconds(this.waitBeforeClosing);
		if (this.leftDoor)
		{
			Audio.self.playOneShot("20ec1441-b133-443a-bb08-c38c619da9d8", 1f);
		}
		this.cannotBeBroken = true;
		while (openingTimer != 0f)
		{
			openingTimer = Mathf.MoveTowards(openingTimer, 0f, Time.deltaTime);
			base.transform.localPosition = Vector2.Lerp(this.startPosition, end, openingTimer / this.openingTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x00063CE0 File Offset: 0x000620E0
	public void PlayBrokenAnimation()
	{
		if (this.brokenDoor || this.cannotBeBroken)
		{
			return;
		}
		if (this.defaultBehaviour != null)
		{
			base.StopCoroutine(this.defaultBehaviour);
		}
		this.brokenDoor = true;
		base.StartCoroutine(this.BrokenAnimationCoroutine());
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x00063D30 File Offset: 0x00062130
	private IEnumerator BrokenAnimationCoroutine()
	{
		while (!this.doorsOpened)
		{
			yield return null;
		}
		yield return new WaitForSeconds(this.brokenDoorWait);
		if (this.leftDoor)
		{
			Audio.self.playLoopSound("fc6a8429-7782-4f00-9e67-7981f2c6dd1e");
			this.brokenSoundPlaying = true;
		}
		if (this.brokenDoorRotation != 0f)
		{
			float rotationTimer = 0f;
			float rotationTime = 0.8f;
			float angle = base.transform.eulerAngles.z;
			while (rotationTimer != rotationTime)
			{
				rotationTimer = Mathf.MoveTowards(rotationTimer, rotationTime, Time.deltaTime);
				float t = Mathf.Sin(rotationTimer / rotationTime * 3.1415927f * 0.5f);
				float newAngle = Mathf.LerpAngle(angle, this.brokenDoorRotation, t);
				base.transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
				yield return null;
			}
		}
		if (this.brokenDoorTime == 0f)
		{
			yield break;
		}
		Vector2 start = base.transform.localPosition;
		Vector2 end = start + new Vector2(this.brokenDoorOffset, 0f);
		float timer = 0f;
		for (;;)
		{
			while (timer != this.brokenDoorTime)
			{
				timer = Mathf.MoveTowards(timer, this.brokenDoorTime, Time.deltaTime);
				float t2 = Mathf.Sin(timer / this.brokenDoorTime * 3.1415927f * 0.5f);
				base.transform.localPosition = Vector2.Lerp(start, end, t2);
				yield return null;
			}
			timer = this.brokenDoorTime / 2f;
			while (timer != 0f)
			{
				timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime);
				float t3 = Mathf.Sin(timer / (this.brokenDoorTime / 2f) * 3.1415927f * 0.5f);
				base.transform.localPosition = Vector2.Lerp(start, end, t3);
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06001A06 RID: 6662 RVA: 0x00063D4C File Offset: 0x0006214C
	public void TransitionUpdate()
	{
		Vector2 vector = Camera.main.WorldToViewportPoint(this.doorFrame.position);
		this.renderer.material.SetFloat("_Left", vector.x);
	}

	// Token: 0x04001805 RID: 6149
	public Transform doorFrame;

	// Token: 0x04001806 RID: 6150
	public float openPosition;

	// Token: 0x04001807 RID: 6151
	public float openingTime;

	// Token: 0x04001808 RID: 6152
	public bool leftDoor;

	// Token: 0x04001809 RID: 6153
	public float waitBeforeClosing = 3f;

	// Token: 0x0400180A RID: 6154
	private Coroutine defaultBehaviour;

	// Token: 0x0400180B RID: 6155
	private bool cannotBeBroken;

	// Token: 0x0400180C RID: 6156
	private bool brokenSoundPlaying;

	// Token: 0x0400180D RID: 6157
	[Header("Broken door animation")]
	public float brokenDoorWait = 1f;

	// Token: 0x0400180E RID: 6158
	public float brokenDoorOffset;

	// Token: 0x0400180F RID: 6159
	public float brokenDoorTime;

	// Token: 0x04001810 RID: 6160
	public float brokenDoorRotation = 10f;

	// Token: 0x04001811 RID: 6161
	private bool brokenDoor;

	// Token: 0x04001812 RID: 6162
	private Vector2 startPosition;

	// Token: 0x04001813 RID: 6163
	private SpriteRenderer renderer;

	// Token: 0x04001814 RID: 6164
	private bool doorsOpened;
}
