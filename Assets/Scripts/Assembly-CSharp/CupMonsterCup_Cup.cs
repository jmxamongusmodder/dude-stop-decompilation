using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class CupMonsterCup_Cup : MonoBehaviour
{
	// Token: 0x0600153D RID: 5437 RVA: 0x0003DE4C File Offset: 0x0003C24C
	private void OnDrawGizmosSelected()
	{
		GizmosExtension.DrawPoint(new Vector2(base.transform.position.x, this.endPosition), Color.magenta, 0.5f);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0003DE88 File Offset: 0x0003C288
	private void Awake()
	{
		this.spriteList = base.transform.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer spriteRenderer in this.spriteList)
		{
			spriteRenderer.color = this.cupTint;
		}
		this.boilPartMax = this.boilParticles[0].GetEmissionRate();
		foreach (ParticleSystem particleSystem in this.boilParticles)
		{
			particleSystem.SetEmissionRate(0f);
			particleSystem.gameObject.SetActive(true);
		}
		this.dropsParticles.EnableEmmision(false);
		this.cupObj.SetActive(false);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x0003DF3A File Offset: 0x0003C33A
	private void Update()
	{
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0003DF3C File Offset: 0x0003C33C
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (!this.clickingEnabled)
		{
			return;
		}
		base.GetComponent<Animator>().enabled = false;
		Audio.self.playOneShot("476fe132-6fdd-4073-b05d-1f46289bd217", 1f);
		Global.CupAcquired(base.transform);
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x0003DF8D File Offset: 0x0003C38D
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			base.StartCoroutine(this.RemoveTintColor());
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0003DFB1 File Offset: 0x0003C3B1
	public void AllowCupPickUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.clickingEnabled = true;
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x0003DFC6 File Offset: 0x0003C3C6
	public void Arise()
	{
		base.gameObject.SetActive(true);
		base.StartCoroutine(this.MovingOutCoroutine());
		base.StartCoroutine(this.ShakeAngle());
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0003DFF0 File Offset: 0x0003C3F0
	private IEnumerator RemoveTintColor()
	{
		float tintTime = 0f;
		float emis = this.dropsParticles.GetEmissionRate();
		this.dropsParticles.EnableEmmision(true);
		while (tintTime < this.removeTintTime)
		{
			tintTime += Time.deltaTime;
			Color newColor = Color.Lerp(this.cupTint, Color.white, tintTime / this.removeTintTime);
			foreach (SpriteRenderer spriteRenderer in this.spriteList)
			{
				spriteRenderer.color = newColor;
			}
			this.dropsParticles.SetEmissionRate(emis * (1f - tintTime / this.removeTintTime));
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0003E00C File Offset: 0x0003C40C
	private IEnumerator MovingOutCoroutine()
	{
		Audio.self.playLoopSound("e1478ed4-62d5-4d3b-992a-1f904f22b11d");
		while (this.waitBefore < this.waitBeforeAppearing)
		{
			foreach (ParticleSystem particles in this.boilParticles)
			{
				particles.SetEmissionRate(this.waitBefore / this.waitBeforeAppearing * this.boilPartMax);
			}
			this.waitBefore += Time.deltaTime;
			yield return null;
		}
		this.cupObj.SetActive(true);
		base.GetComponent<Rigidbody2D>().SetKinematic();
		base.GetComponent<Animator>().enabled = true;
		yield break;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x0003E028 File Offset: 0x0003C428
	private IEnumerator ShakeAngle()
	{
		Transform obj = this.potSprites;
		float side = 1f;
		Quaternion target = Quaternion.identity;
		this.burstSlow = UnityEngine.Random.Range(this.burstSlowMin, this.burstSlowMax);
		base.StartCoroutine(this.shakeBurstDelay());
		while (this.shakeTime < this.shakeTimeMax)
		{
			this.timer += Time.deltaTime;
			if (this.timer > this.timerMaxCurr)
			{
				this.timerMaxCurr = UnityEngine.Random.Range(this.timerMin, this.timerMax) + this.burstSlow;
				this.timer = 0f;
				side *= -1f;
				target = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(this.amountMin, this.amountMax) * side * (this.shakeTime / this.shakeTimeMax));
			}
			obj.localRotation = Quaternion.Lerp(obj.localRotation, target, this.timer / this.timerMaxCurr);
			this.shakeTime += Time.deltaTime;
			yield return null;
		}
		while (this.boilParticles[0].GetEmissionRate() > 0f)
		{
			foreach (ParticleSystem particles in this.boilParticles)
			{
				particles.IncrementEmissionRate(-Time.deltaTime * 30f);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x0003E044 File Offset: 0x0003C444
	private IEnumerator shakeBurstDelay()
	{
		this.burstDelay = UnityEngine.Random.Range(this.burstDelayMin, this.burstDelayMax);
		while (this.burstDelay > 0f)
		{
			this.burstDelay -= Time.deltaTime;
			yield return null;
		}
		base.StartCoroutine(this.shakeBurstDecrease());
		yield break;
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x0003E060 File Offset: 0x0003C460
	private IEnumerator shakeBurstDecrease()
	{
		while (this.burstSlow > 0f)
		{
			this.burstSlow = Mathf.MoveTowards(this.burstSlow, 0f, Time.deltaTime * this.burstSlowDecrease);
			yield return null;
		}
		base.StartCoroutine(this.shakeBurstTime());
		yield break;
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0003E07C File Offset: 0x0003C47C
	private IEnumerator shakeBurstTime()
	{
		this.burstTime = UnityEngine.Random.Range(this.burstTimeMin, this.burstTimeMax);
		while (this.burstTime > 0f)
		{
			this.burstTime -= Time.deltaTime;
			yield return null;
		}
		base.StartCoroutine(this.shakeBurstIncrease());
		yield break;
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x0003E098 File Offset: 0x0003C498
	private IEnumerator shakeBurstIncrease()
	{
		this.burstSlowMaxCurr = UnityEngine.Random.Range(this.burstSlowMin, this.burstSlowMax);
		while (this.burstSlow < this.burstSlowMaxCurr)
		{
			this.burstSlow = Mathf.MoveTowards(this.burstSlow, this.burstSlowMaxCurr, Time.deltaTime * this.burstSlowIncrease);
			yield return null;
		}
		base.StartCoroutine(this.shakeBurstDelay());
		yield break;
	}

	// Token: 0x040012CE RID: 4814
	public Transform collider;

	// Token: 0x040012CF RID: 4815
	public float endPosition;

	// Token: 0x040012D0 RID: 4816
	public float risingSpeed;

	// Token: 0x040012D1 RID: 4817
	public float waitBeforeAppearing = 1f;

	// Token: 0x040012D2 RID: 4818
	private float waitBefore;

	// Token: 0x040012D3 RID: 4819
	private bool clickingEnabled;

	// Token: 0x040012D4 RID: 4820
	public Color cupTint;

	// Token: 0x040012D5 RID: 4821
	public float removeTintTime = 1f;

	// Token: 0x040012D6 RID: 4822
	public ParticleSystem[] boilParticles;

	// Token: 0x040012D7 RID: 4823
	public ParticleSystem dropsParticles;

	// Token: 0x040012D8 RID: 4824
	private float boilPartMax;

	// Token: 0x040012D9 RID: 4825
	public GameObject cupObj;

	// Token: 0x040012DA RID: 4826
	private SpriteRenderer[] spriteList;

	// Token: 0x040012DB RID: 4827
	public Transform potSprites;

	// Token: 0x040012DC RID: 4828
	[Header("Rotation Shake")]
	public float shakeTimeMax = 10f;

	// Token: 0x040012DD RID: 4829
	private float shakeTime;

	// Token: 0x040012DE RID: 4830
	private float timer;

	// Token: 0x040012DF RID: 4831
	private float timerMaxCurr;

	// Token: 0x040012E0 RID: 4832
	public float timerMin = 0.1f;

	// Token: 0x040012E1 RID: 4833
	public float timerMax = 1f;

	// Token: 0x040012E2 RID: 4834
	public float amountMin = 1f;

	// Token: 0x040012E3 RID: 4835
	public float amountMax = 10f;

	// Token: 0x040012E4 RID: 4836
	private float burstDelay;

	// Token: 0x040012E5 RID: 4837
	public float burstDelayMin = 0.1f;

	// Token: 0x040012E6 RID: 4838
	public float burstDelayMax = 1f;

	// Token: 0x040012E7 RID: 4839
	private float burstSlow;

	// Token: 0x040012E8 RID: 4840
	private float burstSlowMaxCurr;

	// Token: 0x040012E9 RID: 4841
	public float burstSlowMin = 0.5f;

	// Token: 0x040012EA RID: 4842
	public float burstSlowMax = 1f;

	// Token: 0x040012EB RID: 4843
	public float burstSlowIncrease = 2f;

	// Token: 0x040012EC RID: 4844
	public float burstSlowDecrease = 0.5f;

	// Token: 0x040012ED RID: 4845
	private float burstTime;

	// Token: 0x040012EE RID: 4846
	public float burstTimeMin = 0.5f;

	// Token: 0x040012EF RID: 4847
	public float burstTimeMax = 1f;
}
