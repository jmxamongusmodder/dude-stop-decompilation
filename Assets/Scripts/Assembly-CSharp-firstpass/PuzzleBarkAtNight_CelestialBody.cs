using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003BE RID: 958
public class PuzzleBarkAtNight_CelestialBody : MonoBehaviour
{
	// Token: 0x060017D0 RID: 6096 RVA: 0x00050EAB File Offset: 0x0004F2AB
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		GizmosExtension.DrawHorizontalLine(this.horizon, -10f, 10f);
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x00050ECC File Offset: 0x0004F2CC
	private void Awake()
	{
		this.topPoint = new Vector2(this.sun.position.x, this.peakPosition);
		this.bottomPoint = new Vector2(this.sun.position.x, this.minPosition);
		this.sun.position = this.topPoint;
		this.nightRenderer = this.night.GetComponent<SpriteRenderer>();
		Vector3 vector = Camera.main.WorldToViewportPoint(new Vector3(0f, this.horizon));
		foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.material.SetFloat("_Top", vector.y);
			spriteRenderer.material.SetFloat("_Angle", 0f);
			spriteRenderer.material.SetFloat("_Distance", 0f);
		}
		this.currentBody = this.sun;
		PuzzleBarkAtNight_CelestialBody.isNight = false;
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x00050FE0 File Offset: 0x0004F3E0
	private void Start()
	{
		if (PuzzleBarkAtNight_Dog_animation.showFlyingDog == true)
		{
			base.StartCoroutine(this.WaitForFlyingDog());
		}
		else
		{
			base.StartCoroutine(this.WaitForFirstDayStart());
		}
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x0005102B File Offset: 0x0004F42B
	private void OnDisable()
	{
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x00051030 File Offset: 0x0004F430
	private void Update()
	{
		this.MoveBodies();
		if (this.currentBody == this.sun && this.nightLerpTimer != 0f)
		{
			this.nightLerpTimer = Mathf.MoveTowards(this.nightLerpTimer, 0f, Time.deltaTime);
			Color color = this.nightRenderer.color;
			color.a = Mathf.Lerp(0f, this.nightAlpha, this.nightLerpTimer / this.nightLerpTime);
			this.nightRenderer.color = color;
			if (this.nightLerpTimer == 0f)
			{
				PuzzleBarkAtNight_CelestialBody.isNight = false;
				PuzzleBarkAtNight_House component = this.house.GetComponent<PuzzleBarkAtNight_House>();
				component.topWindow.gameObject.SetActive(false);
				component.leftWindow.gameObject.SetActive(false);
				component.rightWindow.gameObject.SetActive(false);
			}
			color.a = 1f;
			color = Color.Lerp(Color.white, color, this.nightAlpha / 1f);
			Color color2 = Color.Lerp(Color.white, color, this.nightLerpTimer / this.nightLerpTime);
			this.dog.color = color2;
			this.dogMouth.color = color2;
		}
		else if (this.currentBody == this.moon && this.nightLerpTimer != this.nightLerpTime)
		{
			this.nightLerpTimer = Mathf.MoveTowards(this.nightLerpTimer, this.nightLerpTime, Time.deltaTime);
			Color color3 = this.nightRenderer.color;
			color3.a = Mathf.Lerp(0f, this.nightAlpha, this.nightLerpTimer / this.nightLerpTime);
			this.nightRenderer.color = color3;
			if (this.nightLerpTimer == this.nightLerpTime)
			{
				PuzzleBarkAtNight_CelestialBody.isNight = true;
			}
			color3.a = 1f;
			color3 = Color.Lerp(Color.white, color3, this.nightAlpha / 1f);
			Color color4 = Color.Lerp(Color.white, color3, this.nightLerpTimer / this.nightLerpTime);
			this.dog.color = color4;
			this.dogMouth.color = color4;
		}
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x00051260 File Offset: 0x0004F660
	private IEnumerator EndLevel()
	{
		base.enabled = false;
		this.GetComponentInPuzzleStats<PuzzleBarkAtNight_Dog>().MoveLine();
		if (this.GetComponentInPuzzleStats<PuzzleBarkAtNight_House>().finished)
		{
			yield break;
		}
		AudioVoice_DogBarkAtNight voice = Global.self.currPuzzle.GetComponent<AudioVoice_DogBarkAtNight>();
		while (!voice.canExitPuzzle)
		{
			yield return null;
		}
		Global.LevelFailed(0f, true);
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x0005127C File Offset: 0x0004F67C
	private IEnumerator WaitForFlyingDog()
	{
		base.enabled = false;
		float time = 0f;
		this.PlaySound();
		while (time < this.flyingDogWait)
		{
			time = Mathf.MoveTowards(time, this.flyingDogWait, Time.deltaTime);
			yield return null;
		}
		base.enabled = true;
		base.StartCoroutine(this.WaitForFirstDayStart());
		yield break;
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x00051298 File Offset: 0x0004F698
	private IEnumerator WaitForFirstDayStart()
	{
		base.enabled = false;
		AudioVoice_DogBarkAtNight voice = Global.self.currPuzzle.GetComponent<AudioVoice_DogBarkAtNight>();
		this.PlaySound();
		while (!voice.startFirstDay)
		{
			yield return null;
		}
		yield return new WaitForSeconds(4f);
		base.enabled = true;
		yield break;
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000512B4 File Offset: 0x0004F6B4
	private void PlaySound()
	{
		if (this.currentBody == this.moon)
		{
			Audio.self.playLoopSound("cf1cf15d-908c-4082-a7bf-fa6e0ace4801", base.transform);
		}
		else
		{
			Audio.self.playLoopSound("abf18a0d-27d4-4612-81bf-4f4a0a85d5ba", base.transform);
		}
	}

	// Token: 0x060017D9 RID: 6105 RVA: 0x00051308 File Offset: 0x0004F708
	private void StopSound()
	{
		if (this.currentBody == this.sun)
		{
			Audio.self.stopLoopSound("abf18a0d-27d4-4612-81bf-4f4a0a85d5ba", base.transform, true);
		}
		else
		{
			Audio.self.stopLoopSound("cf1cf15d-908c-4082-a7bf-fa6e0ace4801", base.transform, true);
		}
	}

	// Token: 0x060017DA RID: 6106 RVA: 0x0005135C File Offset: 0x0004F75C
	private void MoveBodies()
	{
		if (!this.moving)
		{
			return;
		}
		if (this.movingUp)
		{
			this.animationTime = Mathf.MoveTowards(this.animationTime, this.risingAnimation[this.risingAnimation.length - 1].time, Time.deltaTime);
			this.currentBody.position = Vector2.Lerp(this.bottomPoint, this.topPoint, this.risingAnimation.Evaluate(this.animationTime));
			if (this.animationTime == this.risingAnimation[this.risingAnimation.length - 1].time)
			{
				this.animationTime = 0f;
				this.movingUp = false;
				if (this.currentBody == this.sun)
				{
					this.peakTimer = this.sunPeakTime;
				}
				else if (Global.self.CountPackPlayedTimes(0) > 0)
				{
					this.peakTimer = this.secondMoonPeakTime;
				}
				else
				{
					this.peakTimer = this.moonPeakTime;
				}
			}
		}
		else if (this.peakTimer != 0f)
		{
			this.peakTimer = Mathf.MoveTowards(this.peakTimer, 0f, Time.deltaTime);
			if (this.peakTimer == 0f && this.currentBody == this.sun && ++this.dayCounter == this.days)
			{
				base.StartCoroutine(this.EndLevel());
			}
		}
		else
		{
			this.animationTime = Mathf.MoveTowards(this.animationTime, this.settingAnimation[this.settingAnimation.length - 1].time, Time.deltaTime);
			this.currentBody.position = Vector2.Lerp(this.bottomPoint, this.topPoint, this.settingAnimation.Evaluate(this.animationTime));
			if (this.animationTime == this.settingAnimation[this.settingAnimation.length - 1].time)
			{
				this.animationTime = 0f;
				this.movingUp = true;
				this.StopSound();
				this.currentBody = ((!(this.currentBody == this.sun)) ? this.sun : this.moon);
				this.PlaySound();
				if (this.currentBody == this.sun)
				{
					this.GetComponentInPuzzleStats<PuzzleBarkAtNight_Dog>().canBark = true;
				}
			}
		}
	}

	// Token: 0x040015A8 RID: 5544
	public static bool isNight;

	// Token: 0x040015A9 RID: 5545
	[Header("Moon/sun stuff")]
	public AnimationCurve risingAnimation;

	// Token: 0x040015AA RID: 5546
	public AnimationCurve settingAnimation;

	// Token: 0x040015AB RID: 5547
	public float sunPeakTime;

	// Token: 0x040015AC RID: 5548
	public float moonPeakTime;

	// Token: 0x040015AD RID: 5549
	public float secondMoonPeakTime;

	// Token: 0x040015AE RID: 5550
	public int days;

	// Token: 0x040015AF RID: 5551
	public Transform night;

	// Token: 0x040015B0 RID: 5552
	public Transform sun;

	// Token: 0x040015B1 RID: 5553
	public Transform moon;

	// Token: 0x040015B2 RID: 5554
	[Header("Dog stuff")]
	public SpriteRenderer dog;

	// Token: 0x040015B3 RID: 5555
	public SpriteRenderer dogMouth;

	// Token: 0x040015B4 RID: 5556
	public float flyingDogWait = 5f;

	// Token: 0x040015B5 RID: 5557
	public Transform house;

	// Token: 0x040015B6 RID: 5558
	[Range(0f, 1f)]
	public float nightAlpha;

	// Token: 0x040015B7 RID: 5559
	public float nightLerpTime;

	// Token: 0x040015B8 RID: 5560
	public float horizon;

	// Token: 0x040015B9 RID: 5561
	public float minPosition;

	// Token: 0x040015BA RID: 5562
	public float peakPosition;

	// Token: 0x040015BB RID: 5563
	private SpriteRenderer nightRenderer;

	// Token: 0x040015BC RID: 5564
	private Transform currentBody;

	// Token: 0x040015BD RID: 5565
	private Vector2 topPoint;

	// Token: 0x040015BE RID: 5566
	private Vector2 bottomPoint;

	// Token: 0x040015BF RID: 5567
	private bool movingUp;

	// Token: 0x040015C0 RID: 5568
	private int dayCounter = -1;

	// Token: 0x040015C1 RID: 5569
	private float peakTimer;

	// Token: 0x040015C2 RID: 5570
	private float nightLerpTimer;

	// Token: 0x040015C3 RID: 5571
	private float animationTime;

	// Token: 0x040015C4 RID: 5572
	private bool moving = true;
}
