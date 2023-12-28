using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class CupExamGood_Controller : MonoBehaviour
{
	// Token: 0x06000055 RID: 85 RVA: 0x0000528C File Offset: 0x0000348C
	private void Start()
	{
		base.StartCoroutine(this.LevelCoroutine());
		Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.one);
		this.leftFanfares.localPosition = new Vector2(-(vector.x - this.fanfareDistance), this.leftFanfares.localPosition.y);
		this.rightFanfares.localPosition = new Vector2(vector.x - this.fanfareDistance, this.rightFanfares.localPosition.y);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000532D File Offset: 0x0000352D
	private void Update()
	{
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00005330 File Offset: 0x00003530
	private IEnumerator LevelCoroutine()
	{
		yield return new WaitForSeconds(this.waitOnStart);
		this.topCollider.gameObject.SetActive(false);
		this.mainHat.gameObject.SetActive(true);
		yield return base.StartCoroutine(this.WaitForNextStep(0f));
		Audio.self.playOneShot("1bba8170-292a-4231-a496-39093cacb728", 1f);
		AudioVoice_CupExamGood voice = this.GetPuzzleStats().GetComponent<AudioVoice_CupExamGood>();
		voice.hatThrown();
		IEnumerator enumerator = this.secondaryHats.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(true);
				transform.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", this.secondaryHatBlend);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.confetty.gameObject.SetActive(true);
		yield return base.StartCoroutine(this.WaitForNextStep(this.waitBeforeTertiaryCaps));
		List<int> numbers = new List<int>();
		for (int j = 0; j < this.tertiaryHats.childCount; j++)
		{
			numbers.Add(j);
		}
		numbers.Shuffle<int>();
		for (int i = 0; i < numbers.Count; i++)
		{
			Transform t = this.tertiaryHats.GetChild(i);
			t.gameObject.SetActive(true);
			t.GetComponent<SpriteRenderer>().material.SetFloat("_Alpha", this.tertiaryHatBlend);
			yield return new WaitForSeconds(this.timeBetweenTertiaryHats);
		}
		this.leftFanfares.gameObject.SetActive(true);
		this.rightFanfares.gameObject.SetActive(true);
		while (!voice.hideAll)
		{
			yield return null;
		}
		IEnumerator enumerator2 = this.secondaryHats.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				Transform transform2 = (Transform)obj2;
				transform2.gameObject.SetActive(false);
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		IEnumerator enumerator3 = this.tertiaryHats.GetEnumerator();
		try
		{
			while (enumerator3.MoveNext())
			{
				object obj3 = enumerator3.Current;
				Transform transform3 = (Transform)obj3;
				transform3.gameObject.SetActive(false);
			}
		}
		finally
		{
			IDisposable disposable3;
			if ((disposable3 = (enumerator3 as IDisposable)) != null)
			{
				disposable3.Dispose();
			}
		}
		this.confetty.gameObject.SetActive(false);
		this.leftFanfares.gameObject.SetActive(false);
		this.rightFanfares.gameObject.SetActive(false);
		this.mainHat.GetComponent<Draggable>().dragEnabled = false;
		while (!voice.voiceStopped)
		{
			yield return null;
		}
		Global.CupAcquired(this.mainHat);
		yield break;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000534C File Offset: 0x0000354C
	private IEnumerator WaitForNextStep(float maxWait = 0f)
	{
		float timer = 0f;
		while (!this.nextStep)
		{
			timer = Mathf.MoveTowards(timer, maxWait, Time.deltaTime);
			if (maxWait != 0f && timer == maxWait)
			{
				break;
			}
			yield return null;
		}
		this.nextStep = false;
		yield break;
	}

	// Token: 0x040000C0 RID: 192
	public float waitOnStart;

	// Token: 0x040000C1 RID: 193
	[Header("Stuff")]
	public Transform confetty;

	// Token: 0x040000C2 RID: 194
	public Transform leftFanfares;

	// Token: 0x040000C3 RID: 195
	public Transform rightFanfares;

	// Token: 0x040000C4 RID: 196
	public float fanfareDistance = 0.4f;

	// Token: 0x040000C5 RID: 197
	[Header("Hats")]
	public Transform mainHat;

	// Token: 0x040000C6 RID: 198
	public Transform secondaryHats;

	// Token: 0x040000C7 RID: 199
	public float secondaryHatBlend = 0.1f;

	// Token: 0x040000C8 RID: 200
	public Transform tertiaryHats;

	// Token: 0x040000C9 RID: 201
	public float tertiaryHatBlend = 0.2f;

	// Token: 0x040000CA RID: 202
	public float timeBetweenTertiaryHats = 0.5f;

	// Token: 0x040000CB RID: 203
	public float waitBeforeTertiaryCaps = 3f;

	// Token: 0x040000CC RID: 204
	public float waitBeforeAcquiring = 4f;

	// Token: 0x040000CD RID: 205
	public Transform topCollider;

	// Token: 0x040000CE RID: 206
	[HideInInspector]
	public bool nextStep;
}
