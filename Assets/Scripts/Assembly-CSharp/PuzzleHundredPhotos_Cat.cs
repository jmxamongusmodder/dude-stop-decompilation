using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000420 RID: 1056
public class PuzzleHundredPhotos_Cat : MonoBehaviour
{
	// Token: 0x06001ACA RID: 6858 RVA: 0x0006A9DC File Offset: 0x00068DDC
	private void Start()
	{
		this.standing = base.transform.GetChild(0);
		this.sleeping = base.transform.GetChild(1);
		base.StartCoroutine(this.FlippingAndRotatingCoroutine());
		Audio.self.playLoopSound("7f551fdd-6768-4d15-8a48-906aaad9d05b", base.transform);
		this.movingLoop = true;
		this.actionTimer = this.waitBetweenActions;
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x0006AA42 File Offset: 0x00068E42
	private void OnDisable()
	{
		if (this.movingLoop)
		{
			Audio.self.stopLoopSound("7f551fdd-6768-4d15-8a48-906aaad9d05b", base.transform, true);
		}
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x0006AA65 File Offset: 0x00068E65
	private void Update()
	{
		this.DoSomething();
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x0006AA70 File Offset: 0x00068E70
	private void DoSomething()
	{
		if (this.activeCoroutine)
		{
			return;
		}
		this.actionTimer = Mathf.MoveTowards(this.actionTimer, this.waitBetweenActions, Time.deltaTime);
		if (this.actionTimer < this.waitBetweenActions)
		{
			return;
		}
		this.actionTimer = 0f;
		switch (UnityEngine.Random.Range(0, Enum.GetNames(typeof(PuzzleHundredPhotos_Cat.Actions)).Length))
		{
		case 0:
			base.StartCoroutine(this.SleepingCoroutine());
			break;
		case 1:
			base.StartCoroutine(this.WalkingCoroutine(-1));
			break;
		case 2:
			base.StartCoroutine(this.WalkingCoroutine(1));
			break;
		case 3:
			base.StartCoroutine(this.SomersaultCoroutine());
			break;
		}
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0006AB40 File Offset: 0x00068F40
	private IEnumerator FlippingAndRotatingCoroutine()
	{
		for (;;)
		{
			this.FlipAnimal();
			this.RotateAnimal();
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x0006AB5C File Offset: 0x00068F5C
	private IEnumerator SleepingCoroutine()
	{
		this.activeCoroutine = true;
		this.movingLoop = false;
		Audio.self.stopLoopSound("7f551fdd-6768-4d15-8a48-906aaad9d05b", base.transform, true);
		this.standing.gameObject.SetActive(false);
		this.sleeping.gameObject.SetActive(true);
		yield return new WaitForSeconds(this.sleepingTime);
		this.activeCoroutine = false;
		this.standing.gameObject.SetActive(true);
		this.sleeping.gameObject.SetActive(false);
		this.movingLoop = true;
		Audio.self.playLoopSound("7f551fdd-6768-4d15-8a48-906aaad9d05b", base.transform);
		yield break;
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x0006AB78 File Offset: 0x00068F78
	private IEnumerator WalkingCoroutine(int sign)
	{
		float dist = UnityEngine.Random.Range(this.minWalk, this.maxWalk) * (float)sign;
		if (base.transform.localPosition.x + dist < -this.maxDistance || base.transform.localPosition.x + dist > this.maxDistance)
		{
			dist *= -1f;
			sign *= -1;
		}
		this.facingLeft = (sign < 0);
		float target = base.transform.localPosition.x + dist;
		this.activeCoroutine = true;
		this.walking = true;
		yield return null;
		while (base.transform.localPosition.x != target)
		{
			float newX = Mathf.MoveTowards(base.transform.localPosition.x, target, this.walkingSpeed * Time.deltaTime);
			base.transform.localPosition = new Vector3(newX, base.transform.localPosition.y);
			yield return null;
		}
		this.activeCoroutine = false;
		this.walking = false;
		yield break;
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0006AB9C File Offset: 0x00068F9C
	private IEnumerator SomersaultCoroutine()
	{
		this.activeCoroutine = true;
		Vector3 start = base.transform.localPosition;
		Vector3 end = base.transform.localPosition;
		float heightTimer = 0f;
		float somersaultTimer = 0f;
		this.movingLoop = false;
		Audio.self.stopLoopSound("7f551fdd-6768-4d15-8a48-906aaad9d05b", base.transform, true);
		Audio.self.playOneShot("fd88a1b6-6c41-475c-a2f0-5471d3bc6ba8", 1f);
		while (somersaultTimer != this.somersaultTime)
		{
			if (heightTimer < this.heightTime)
			{
				heightTimer = Mathf.MoveTowards(heightTimer, this.heightTime, Time.deltaTime);
				float t = (heightTimer + somersaultTimer) / (this.somersaultTime + this.heightTime);
				Vector3 localPosition = Vector3.Lerp(start, new Vector3(start.x, start.y + this.height, start.z), heightTimer / this.heightTime);
				float num = Mathf.Cos((2f * heightTimer / this.heightTime - 1f) * 3.1415927f * 0.5f) * this.height;
				localPosition.y += num;
				base.transform.localPosition = localPosition;
				base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, -this.degrees, t));
				if (heightTimer == this.heightTime)
				{
					start = base.transform.localPosition;
				}
			}
			else
			{
				somersaultTimer = Mathf.MoveTowards(somersaultTimer, this.somersaultTime, Time.deltaTime);
				float t2 = (heightTimer + somersaultTimer) / (this.somersaultTime + this.heightTime);
				base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, -this.degrees, t2));
				base.transform.localPosition = Vector3.Lerp(start, end + new Vector3(0f, -0.3f), somersaultTimer / this.somersaultTime);
			}
			yield return null;
		}
		yield return new WaitForSeconds(this.rollOverWait);
		somersaultTimer = 0f;
		start = base.transform.localPosition;
		float scale = base.transform.localScale.y;
		while (somersaultTimer != this.rollOverTime)
		{
			somersaultTimer = Mathf.MoveTowards(somersaultTimer, this.rollOverTime, Time.deltaTime);
			base.transform.localPosition = Vector3.Lerp(start, end, somersaultTimer / this.rollOverTime);
			base.transform.localScale = Vector3.Lerp(new Vector3(base.transform.localScale.x, scale), new Vector3(base.transform.localScale.x, -scale), somersaultTimer / this.rollOverTime);
			yield return null;
		}
		base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		base.transform.localScale = new Vector3(-base.transform.localScale.x, Mathf.Abs(base.transform.localScale.y));
		this.facingLeft = !this.facingLeft;
		this.activeCoroutine = false;
		this.movingLoop = true;
		Audio.self.playLoopSound("7f551fdd-6768-4d15-8a48-906aaad9d05b", base.transform);
		yield break;
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0006ABB8 File Offset: 0x00068FB8
	private void FlipAnimal()
	{
		if (((this.facingLeft && base.transform.localScale.x > 0f) || (!this.facingLeft && base.transform.localScale.x < 0f)) && this.scaleTimer == 0f)
		{
			this.scaleTimer = this.scaleTime;
			this.startingScale = base.transform.localScale.x;
		}
		else if (this.scaleTimer > 0f)
		{
			this.scaleTimer = Mathf.MoveTowards(this.scaleTimer, 0f, Time.deltaTime);
			float num = this.scaleTimer / this.scaleTime;
			num = Mathf.Cos(num * 3.1415927f * 0.5f);
			num = Mathf.Lerp(this.startingScale, -this.startingScale, num);
			base.transform.localScale = new Vector3(num, base.transform.localScale.y, base.transform.localScale.z);
		}
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x0006ACE8 File Offset: 0x000690E8
	private void RotateAnimal()
	{
		if (this.walking)
		{
			float num = (base.transform.localPosition.x - this.lastPosition.x) / this.length;
			float z = Mathf.Sin(num * 3.1415927f * 0.5f) * this.amplitude;
			base.transform.localRotation = Quaternion.Euler(0f, 0f, z);
			this.rotationTimer = 0f;
			this.startingZ = base.transform.localEulerAngles.z;
		}
		else if (this.rotationTimer != this.rotationTime)
		{
			this.rotationTimer = Mathf.MoveTowards(this.rotationTimer, this.rotationTime, Time.deltaTime);
			float num2 = this.rotationTimer / this.rotationTime;
			num2 = Mathf.Sin(num2 * 3.1415927f * 0.5f);
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingZ, 0f, num2));
		}
		else
		{
			this.lastPosition = base.transform.localPosition;
		}
	}

	// Token: 0x040018F2 RID: 6386
	public float waitBetweenActions = 1f;

	// Token: 0x040018F3 RID: 6387
	private float actionTimer;

	// Token: 0x040018F4 RID: 6388
	private bool walking;

	// Token: 0x040018F5 RID: 6389
	private bool activeCoroutine;

	// Token: 0x040018F6 RID: 6390
	[Header("Walking")]
	public float minWalk = 1f;

	// Token: 0x040018F7 RID: 6391
	public float maxWalk = 3f;

	// Token: 0x040018F8 RID: 6392
	public float walkingSpeed = 5f;

	// Token: 0x040018F9 RID: 6393
	public float maxDistance = 4f;

	// Token: 0x040018FA RID: 6394
	[Header("Sleeping")]
	public float sleepingTime = 3f;

	// Token: 0x040018FB RID: 6395
	private Transform standing;

	// Token: 0x040018FC RID: 6396
	private Transform sleeping;

	// Token: 0x040018FD RID: 6397
	[Header("Somersault")]
	public float degrees;

	// Token: 0x040018FE RID: 6398
	public float height;

	// Token: 0x040018FF RID: 6399
	public float heightTime;

	// Token: 0x04001900 RID: 6400
	public float somersaultTime;

	// Token: 0x04001901 RID: 6401
	public float rollOverWait;

	// Token: 0x04001902 RID: 6402
	public float rollOverTime;

	// Token: 0x04001903 RID: 6403
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x04001904 RID: 6404
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x04001905 RID: 6405
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x04001906 RID: 6406
	public float scaleTime = 0.3f;

	// Token: 0x04001907 RID: 6407
	private Vector3 lastPosition;

	// Token: 0x04001908 RID: 6408
	private float rotationTimer;

	// Token: 0x04001909 RID: 6409
	private float startingZ;

	// Token: 0x0400190A RID: 6410
	private bool facingLeft;

	// Token: 0x0400190B RID: 6411
	private float scaleTimer;

	// Token: 0x0400190C RID: 6412
	private float startingScale;

	// Token: 0x0400190D RID: 6413
	private bool movingLoop;

	// Token: 0x02000421 RID: 1057
	private enum Actions
	{
		// Token: 0x0400190F RID: 6415
		Sleep,
		// Token: 0x04001910 RID: 6416
		WalkLeft,
		// Token: 0x04001911 RID: 6417
		WalkRight,
		// Token: 0x04001912 RID: 6418
		Somersault
	}
}
