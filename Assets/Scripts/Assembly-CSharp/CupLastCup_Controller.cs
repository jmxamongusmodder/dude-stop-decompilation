using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class CupLastCup_Controller : MonoBehaviour
{
	// Token: 0x060014DD RID: 5341 RVA: 0x0003AD58 File Offset: 0x00039158
	private void Start()
	{
		GlobalCollider componentInPuzzleStats = this.GetComponentInPuzzleStats<GlobalCollider>();
		this.rightBorder = componentInPuzzleStats.GetComponents<Collider2D>().First((Collider2D x) => x.offset.y == 0f && x.offset.x > 0f);
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x0003AD9A File Offset: 0x0003919A
	private void Update()
	{
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x0003AD9C File Offset: 0x0003919C
	public void showSign()
	{
		base.StartCoroutine(this.LowerSign());
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x0003ADAB File Offset: 0x000391AB
	public void dropSign()
	{
		this.sign.transform.GetComponent<CupLastCup_Sign>().DropSign();
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x0003ADC2 File Offset: 0x000391C2
	public void enableSignClicks()
	{
		this.sign.GetComponent<CupLastCup_Sign>().checkJoints = true;
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x0003ADD8 File Offset: 0x000391D8
	private IEnumerator LowerSign()
	{
		this.sign.AddForce(Vector2.left * this.signForce);
		float time = 0f;
		float totalTime = this.signCurve.GetAnimationLength();
		float startY = this.signParent.position.y;
		while (time < totalTime)
		{
			time = Mathf.MoveTowards(time, totalTime, Time.deltaTime);
			this.signParent.SetY(startY + this.signCurve.Evaluate(time), false);
			yield return null;
		}
		this.sign.AddForce(Vector2.right * this.signForce);
		this.sign.gameObject.layer = LayerMask.NameToLayer("Back");
		yield break;
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x0003ADF3 File Offset: 0x000391F3
	public void EnableCup()
	{
		if (!this.cupCanBeEnabled)
		{
			return;
		}
		this.cup3.GetComponent<CupLastCup_AwardCup>().canBeAcquired = true;
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x0003AE12 File Offset: 0x00039212
	public void DisableCup()
	{
		this.cupCanBeEnabled = false;
		this.cup3.GetComponent<CupLastCup_AwardCup>().canBeAcquired = false;
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x0003AE2C File Offset: 0x0003922C
	public void ShowCup(int index)
	{
		switch (index)
		{
		case 1:
			base.StartCoroutine(this.ThrowCup(this.cup1, this.cup1force, this.torque));
			break;
		case 2:
			base.StartCoroutine(this.ThrowCup(this.cup2, this.cup2force, this.torque));
			break;
		case 3:
		case 4:
		case 5:
			base.StartCoroutine(this.ThrowCup(this.cupList[index - 3], this.cup2force, Extensions.Random(this.rndTorque)));
			break;
		}
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x0003AED0 File Offset: 0x000392D0
	private IEnumerator ThrowCup(Transform cup, float force, float torque)
	{
		this.rightBorder.enabled = false;
		Vector2 screenSize = Camera.main.ViewportToWorldPoint(Vector2.one);
		cup.gameObject.SetActive(true);
		cup.SetX(screenSize.x + this.cupOffset.x);
		cup.SetY(screenSize.y - this.cupOffset.y, false);
		cup.GetComponent<Rigidbody2D>().AddForce(force * Vector2.left);
		cup.GetComponent<Rigidbody2D>().AddTorque(torque);
		yield return new WaitForSeconds(0.5f);
		this.rightBorder.enabled = true;
		yield break;
	}

	// Token: 0x04001268 RID: 4712
	[Header("Sign stuff")]
	public Transform signParent;

	// Token: 0x04001269 RID: 4713
	public Rigidbody2D sign;

	// Token: 0x0400126A RID: 4714
	public AnimationCurve signCurve;

	// Token: 0x0400126B RID: 4715
	public float signForce = 150f;

	// Token: 0x0400126C RID: 4716
	[Header("Cups appear")]
	public Transform cup1;

	// Token: 0x0400126D RID: 4717
	public float cup1force;

	// Token: 0x0400126E RID: 4718
	public Transform cup2;

	// Token: 0x0400126F RID: 4719
	public float cup2force;

	// Token: 0x04001270 RID: 4720
	public Transform cup3;

	// Token: 0x04001271 RID: 4721
	public AnimationCurve cup3animation;

	// Token: 0x04001272 RID: 4722
	public float cup3force;

	// Token: 0x04001273 RID: 4723
	public Vector2 cupOffset;

	// Token: 0x04001274 RID: 4724
	private bool cupCanBeEnabled = true;

	// Token: 0x04001275 RID: 4725
	public float torque;

	// Token: 0x04001276 RID: 4726
	[Space(10f)]
	public Transform[] cupList;

	// Token: 0x04001277 RID: 4727
	public Vector2 rndTorque;

	// Token: 0x04001278 RID: 4728
	[Header("Cups disappear")]
	public float scaleDownTime = 1f;

	// Token: 0x04001279 RID: 4729
	private Collider2D rightBorder;
}
