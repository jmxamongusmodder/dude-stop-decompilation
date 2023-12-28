using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036F RID: 879
public class CupToyMachine_Claw : MonoBehaviour
{
	// Token: 0x06001598 RID: 5528 RVA: 0x00041F38 File Offset: 0x00040338
	private void OnDrawGizmosSelected()
	{
		GizmosExtension.DrawVerticalLine(this.minX);
		GizmosExtension.DrawVerticalLine(this.maxX);
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x00041F50 File Offset: 0x00040350
	private void Start()
	{
		this.noClaw = base.transform.GetChild(0);
		this.cable = base.transform.GetChild(2);
		this.clawBase = base.transform.GetChild(3);
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x00041F88 File Offset: 0x00040388
	private void Update()
	{
		this.CheckMovement();
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x00041F90 File Offset: 0x00040390
	public void Move(bool moveLeft)
	{
		if (this.grappling)
		{
			return;
		}
		this.movementSign = ((!moveLeft) ? 1 : -1);
		this.moving = true;
		Audio.self.playLoopSound("1a454485-77ae-4f21-8141-8fc38803fb94");
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x00041FC8 File Offset: 0x000403C8
	public void Stop()
	{
		if (!this.moving)
		{
			return;
		}
		this.moving = false;
		if (!this.grappling)
		{
			float num = Mathf.Abs(base.transform.position.x - this.cup.position.x);
			if (num < this.cupSnapDistance)
			{
				if ((base.transform.position.x < this.cup.position.x && this.movementSign == -1) || (base.transform.position.x > this.cup.position.x && this.movementSign == 1))
				{
					float targetX = Mathf.Clamp(this.cup.position.x + this.cupSnapDistance * (float)this.movementSign, this.minX, this.maxX);
					this.noGrapple = true;
					base.StartCoroutine(this.ClawMovingCoroutine(targetX, false, 0f));
				}
				else
				{
					base.StartCoroutine(this.ClawMovingCoroutine(this.cup.position.x, false, 0f));
				}
			}
			else
			{
				Audio.self.stopLoopSound("1a454485-77ae-4f21-8141-8fc38803fb94", true);
			}
		}
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x00042130 File Offset: 0x00040530
	public void Go()
	{
		if (!base.enabled || this.grappling || this.noGrapple)
		{
			return;
		}
		base.StartCoroutine(this.GrapplingCoroutine());
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x00042164 File Offset: 0x00040564
	private void CheckMovement()
	{
		if (!this.moving || this.grappling)
		{
			return;
		}
		float num = base.transform.position.x + this.movementSpeed * (float)this.movementSign * Time.deltaTime;
		num = Mathf.Clamp(num, this.minX, this.maxX);
		base.transform.position = new Vector3(num, base.transform.position.y);
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000421E8 File Offset: 0x000405E8
	private IEnumerator GrapplingCoroutine()
	{
		this.grappling = true;
		Audio.self.playOneShot("13b1bdae-18a3-484c-8c8f-1f15d91023dc", 1f);
		yield return base.StartCoroutine(this.ClawRotationCoroutine(this.clawRotation));
		float initialPosition = this.clawBase.localPosition.y;
		float sign = Mathf.Cos(this.cup.eulerAngles.z * 0.017453292f);
		Audio.self.playOneShot("89a23654-be40-4eae-9fcd-c1b59cf0301f", 1f);
		yield return base.StartCoroutine(this.ClawExtendingCoroutine(base.transform.InverseTransformPoint(this.cup.position).y + this.cupChangingOffset * sign + this.cupConstantOffset, false));
		bool moveCup = Mathf.Abs(base.transform.position.x - this.cup.position.x) < this.cupSnapDistance;
		float angle = this.clawRotation / 2f;
		if (moveCup)
		{
			this.grapples++;
			this.cupPickedUp = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_CupPlushToy>().touchCup();
		}
		else
		{
			angle = 0f;
			this.cupPickedUp = false;
			Global.self.currPuzzle.GetComponent<AudioVoice_CupPlushToy>().missCup();
		}
		Audio.self.playOneShot("8b6a6ef3-1ea8-41c7-973b-7bd4fef458fc", 1f);
		yield return base.StartCoroutine(this.ClawRotationCoroutine(angle));
		this.cup.GetComponent<Rigidbody2D>().isKinematic = true;
		Audio.self.playOneShot("833aa332-752d-4ba9-884f-edddf6cc4dd7", 1f);
		yield return base.StartCoroutine(this.ClawExtendingCoroutine(initialPosition, moveCup));
		Audio.self.playLoopSound("1a454485-77ae-4f21-8141-8fc38803fb94");
		yield return base.StartCoroutine(this.ClawMovingCoroutine(this.boxX, moveCup, this.cupFallWait));
		Audio.self.playOneShot("13b1bdae-18a3-484c-8c8f-1f15d91023dc", 1f);
		yield return base.StartCoroutine(this.ClawRotationCoroutine(this.clawRotation));
		yield return new WaitForSeconds(this.waitAboveBox);
		if (this.grapples < 3)
		{
			Audio.self.playOneShot("8b6a6ef3-1ea8-41c7-973b-7bd4fef458fc", 1f);
			yield return base.StartCoroutine(this.ClawRotationCoroutine(0f));
		}
		else
		{
			Audio.self.playOneShot("fa620f22-164e-407c-b320-7688fa6d9b9b", 1f);
			yield return base.StartCoroutine(this.ClawFallingCoroutine());
		}
		this.grappling = false;
		yield return null;
		yield break;
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x00042204 File Offset: 0x00040604
	private IEnumerator ClawFallingCoroutine()
	{
		AudioVoice_CupPlushToy voice = Global.self.currPuzzle.GetComponent<AudioVoice_CupPlushToy>();
		voice.sayLastLine();
		while (!voice.canDropClaw)
		{
			yield return null;
		}
		this.clawBase.GetComponent<Rigidbody2D>().isKinematic = false;
		this.noClaw.gameObject.SetActive(true);
		while (this.clawBase.position.y > this.minY)
		{
			yield return null;
		}
		this.clawCup.gameObject.SetActive(true);
		UnityEngine.Object.Destroy(this.clawBase.gameObject);
		UnityEngine.Object.Destroy(this);
		yield break;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x00042220 File Offset: 0x00040620
	private IEnumerator ClawMovingCoroutine(float targetX, bool moveCup = false, float dropCupTime = 0f)
	{
		Vector2 end = new Vector2(targetX, base.transform.localPosition.y);
		float timer = 0f;
		bool cupDropped = false;
		while (base.transform.localPosition.x != targetX)
		{
			timer += Time.deltaTime;
			if (!cupDropped && dropCupTime != 0f && timer > dropCupTime)
			{
				this.cup.GetComponent<Rigidbody2D>().isKinematic = false;
				this.cup.GetComponent<Rigidbody2D>().AddTorque(this.cupTorque);
				cupDropped = true;
				base.StartCoroutine(this.ClawRotationCoroutine(0f));
				if (this.cupPickedUp)
				{
					Global.self.currPuzzle.GetComponent<AudioVoice_CupPlushToy>().dropCup();
				}
			}
			Vector3 delta = base.transform.localPosition;
			base.transform.localPosition = Vector2.MoveTowards(base.transform.localPosition, end, this.movementSpeed * Time.deltaTime);
			delta -= base.transform.localPosition;
			if (moveCup && !cupDropped)
			{
				this.cup.localPosition -= delta;
			}
			if (this.moving && !this.grappling)
			{
				break;
			}
			yield return null;
		}
		Audio.self.stopLoopSound("1a454485-77ae-4f21-8141-8fc38803fb94", true);
		this.noGrapple = false;
		yield break;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x00042250 File Offset: 0x00040650
	private IEnumerator ClawRotationCoroutine(float targetAngle)
	{
		float timer = 0f;
		Transform leftClaw = this.clawBase.GetChild(0);
		Transform rightClaw = this.clawBase.GetChild(1);
		float currAngle = rightClaw.eulerAngles.z;
		while (timer != this.clawOpeningTime)
		{
			timer = Mathf.MoveTowards(timer, this.clawOpeningTime, Time.deltaTime);
			float rotation = Mathf.LerpAngle(currAngle, targetAngle, timer / this.clawOpeningTime);
			leftClaw.rotation = Quaternion.Euler(0f, 0f, -rotation);
			rightClaw.rotation = Quaternion.Euler(0f, 0f, rotation);
			yield return null;
		}
		yield break;
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x00042274 File Offset: 0x00040674
	private IEnumerator ClawExtendingCoroutine(float targetPosition, bool moveCup = false)
	{
		float timer = 0f;
		Vector2 startPosition = this.clawBase.localPosition;
		Vector2 endPosition = new Vector2(startPosition.x, targetPosition);
		Vector2 cableScale = this.cable.localScale;
		Vector2 cablePos = this.cable.localPosition;
		Vector3 deltaPos = Vector2.zero;
		while (timer != this.clawExtendingTime)
		{
			timer = Mathf.MoveTowards(timer, this.clawExtendingTime, Time.deltaTime);
			Vector3 newPos = Vector2.Lerp(startPosition, endPosition, timer / this.clawExtendingTime);
			deltaPos = newPos - this.clawBase.localPosition;
			this.clawBase.localPosition = newPos;
			if (moveCup)
			{
				this.cup.transform.localPosition += deltaPos;
			}
			cableScale.y = Mathf.Abs(base.transform.position.y - this.clawBase.position.y) * 1.5f;
			cablePos.x = this.clawBase.position.x;
			cablePos.y = base.transform.position.y - (base.transform.position.y - this.clawBase.position.y) / 2f;
			this.cable.localScale = cableScale;
			this.cable.position = cablePos;
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001349 RID: 4937
	public Transform clawCup;

	// Token: 0x0400134A RID: 4938
	public float minY;

	// Token: 0x0400134B RID: 4939
	[Header("Toy cup")]
	public Transform cup;

	// Token: 0x0400134C RID: 4940
	public float cupChangingOffset;

	// Token: 0x0400134D RID: 4941
	public float cupConstantOffset;

	// Token: 0x0400134E RID: 4942
	public float cupTorque;

	// Token: 0x0400134F RID: 4943
	public float cupFallWait;

	// Token: 0x04001350 RID: 4944
	public float cupSnapDistance;

	// Token: 0x04001351 RID: 4945
	[Header("Claw stuff")]
	public float boxX;

	// Token: 0x04001352 RID: 4946
	public float waitAboveBox = 0.5f;

	// Token: 0x04001353 RID: 4947
	public float minX;

	// Token: 0x04001354 RID: 4948
	public float maxX;

	// Token: 0x04001355 RID: 4949
	public float movementSpeed;

	// Token: 0x04001356 RID: 4950
	public float clawRotation = 16f;

	// Token: 0x04001357 RID: 4951
	public float clawOpeningTime = 1f;

	// Token: 0x04001358 RID: 4952
	public float clawExtendingTime = 1f;

	// Token: 0x04001359 RID: 4953
	private int movementSign = 1;

	// Token: 0x0400135A RID: 4954
	private bool moving;

	// Token: 0x0400135B RID: 4955
	private bool grappling;

	// Token: 0x0400135C RID: 4956
	private bool noGrapple;

	// Token: 0x0400135D RID: 4957
	private int grapples;

	// Token: 0x0400135E RID: 4958
	private bool cupPickedUp;

	// Token: 0x0400135F RID: 4959
	private Transform noClaw;

	// Token: 0x04001360 RID: 4960
	private Transform clawBase;

	// Token: 0x04001361 RID: 4961
	private Transform cable;
}
