using System;
using UnityEngine;

// Token: 0x0200057B RID: 1403
public class rewardScreen : AbstractUIScreen
{
	// Token: 0x06002047 RID: 8263 RVA: 0x0009E310 File Offset: 0x0009C710
	public override void Update()
	{
		if (this.canExit && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit")))
		{
			Global.self.gotoNextLevel(false, null);
			this.canExit = false;
			this.animate = false;
		}
		this.ShakeText();
		this.MoveReward();
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x0009E380 File Offset: 0x0009C780
	protected void MoveReward()
	{
		if (!this.animate)
		{
			return;
		}
		this.cup.position = Vector2.Lerp(this.cup.position, Vector2.zero, Time.deltaTime * 3f);
		this.cup.localScale = Vector2.Lerp(this.cup.localScale, Vector2.one * this.cupScript.maxScale, Time.deltaTime * 2f);
		if (Vector2.Distance(this.cup.position, Vector2.zero) < 1f && this.minTurns <= 0)
		{
			this.cupFly = false;
		}
		if (this.cupAngle > 360f)
		{
			this.cupAngle = 360f - this.cupAngle;
			this.minTurns--;
		}
		if (this.cupFly && this.minTurns > 0)
		{
			this.angleSpeed = Mathf.Lerp(this.angleSpeed, this.angleSpeedMax, Time.deltaTime * 5f);
			this.cupAngle += this.angleSpeed;
		}
		else
		{
			this.angleSpeed = Mathf.Min(this.angleSpeedMax, (360f - this.cupAngle) * 0.08f);
			this.cupAngle += this.angleSpeed;
			if (this.cupAngle > 355f && this.cupScript.ShowStars() && !this.hintIcon.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(base.showHintIcon(this.hintIcon, new Action(this.CanClickOn)));
			}
		}
		this.targetCupAngle = Quaternion.Lerp(this.targetCupAngle, Quaternion.Euler(Vector3.forward * this.cupAngle), Time.deltaTime * 10f);
		this.cup.rotation = this.targetCupAngle;
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x0009E598 File Offset: 0x0009C998
	protected void ShakeText()
	{
		if (!this.animate)
		{
			return;
		}
		this.freq = Mathf.MoveTowards(this.freq, 0f, Time.deltaTime);
		if (this.freq == 0f)
		{
			this.freq = this.freqMax;
			this.shakeAmount = Mathf.Lerp(this.shakeAmount, this.shakeMax, Time.deltaTime * 1f);
			this.targetAngle = Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(-this.shakeAmount, this.shakeAmount));
			this.targetPos = UnityEngine.Random.insideUnitCircle * this.shakeAmount * 1f + this.originalPos;
		}
		this.textToShake.localRotation = Quaternion.Lerp(this.textToShake.localRotation, this.targetAngle, Time.deltaTime * this.shakeSpeed);
		this.textToShake.position = Vector2.Lerp(this.textToShake.position, this.targetPos, Time.deltaTime * this.shakeSpeed);
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x0009E6C0 File Offset: 0x0009CAC0
	public void EndAnimation()
	{
		base.GetComponent<Animator>().enabled = false;
		this.originalPos = this.textToShake.position;
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x0009E6E4 File Offset: 0x0009CAE4
	public void CanClickOn()
	{
		this.canExit = true;
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x0009E6F0 File Offset: 0x0009CAF0
	public override void setScreen(Transform item)
	{
		foreach (Transform transform in this.rotateTextRnd)
		{
			transform.Rotate(Vector3.forward * UnityEngine.Random.Range(-5f, 5f));
		}
		this.originalPos = this.textToShake.position;
		this.cup = Global.self.awardOnLevel;
		Global.self.awardOnLevel = null;
		this.cupScript = this.cup.GetComponent<PuzzleCup>();
		Debug.LogError("WHY IS THIS SCRIPT RUNNING??? CALL PATOMKIN");
		this.cupAngle = this.cup.eulerAngles.z;
		this.targetCupAngle = Quaternion.Euler(Vector3.forward * this.cupAngle);
		this.hintIcon.gameObject.SetActive(false);
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x0009E7CC File Offset: 0x0009CBCC
	protected override void cancelPressed()
	{
	}

	// Token: 0x04002380 RID: 9088
	private bool canExit;

	// Token: 0x04002381 RID: 9089
	private bool animate = true;

	// Token: 0x04002382 RID: 9090
	[Tooltip("Add rnd rotation to this texts")]
	public Transform[] rotateTextRnd;

	// Token: 0x04002383 RID: 9091
	[Tooltip("Shake this text")]
	public Transform textToShake;

	// Token: 0x04002384 RID: 9092
	[Tooltip("Hint icon")]
	public RectTransform hintIcon;

	// Token: 0x04002385 RID: 9093
	private float freq;

	// Token: 0x04002386 RID: 9094
	private float freqMax = 0.05f;

	// Token: 0x04002387 RID: 9095
	private float shakeAmount;

	// Token: 0x04002388 RID: 9096
	private float shakeMax = 5f;

	// Token: 0x04002389 RID: 9097
	public float shakeSpeed = 5f;

	// Token: 0x0400238A RID: 9098
	private Quaternion targetAngle = Quaternion.identity;

	// Token: 0x0400238B RID: 9099
	private Vector2 targetPos = Vector2.zero;

	// Token: 0x0400238C RID: 9100
	private Vector2 originalPos;

	// Token: 0x0400238D RID: 9101
	private Transform cup;

	// Token: 0x0400238E RID: 9102
	private PuzzleCup cupScript;

	// Token: 0x0400238F RID: 9103
	private Quaternion targetCupAngle;

	// Token: 0x04002390 RID: 9104
	private float cupAngle;

	// Token: 0x04002391 RID: 9105
	private float angleSpeed;

	// Token: 0x04002392 RID: 9106
	private float angleSpeedMax = 8f;

	// Token: 0x04002393 RID: 9107
	private bool cupFly = true;

	// Token: 0x04002394 RID: 9108
	private int minTurns = 1;
}
