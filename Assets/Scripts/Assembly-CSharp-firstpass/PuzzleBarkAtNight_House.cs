using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020003C2 RID: 962
public class PuzzleBarkAtNight_House : MonoBehaviour
{
	// Token: 0x060017FE RID: 6142 RVA: 0x00052B68 File Offset: 0x00050F68
	private void Start()
	{
		PuzzleBarkAtNight_Dog.OnBark += this.ProcessBark;
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x00052B7C File Offset: 0x00050F7C
	private void Update()
	{
		if (this.clickCount == 0)
		{
			return;
		}
		if (this.finished)
		{
			return;
		}
		this.windowCooldownTimer = Mathf.MoveTowards(this.windowCooldownTimer, this.windowCooldownTime, Time.deltaTime);
		if (this.windowCooldownTimer == this.windowCooldownTime)
		{
			int[] source = new int[]
			{
				this.leftWindowClicks,
				this.rightWindowClicks,
				this.topWindowClicks
			};
			int num = source.Min();
			int num2 = (from z in source
			orderby z descending
			select z).Skip(1).First<int>();
			int num3 = source.Max();
			if (this.clickCount >= num3)
			{
				this.clickCount = num2;
			}
			else if (this.clickCount >= num2)
			{
				this.clickCount = num;
			}
			else
			{
				this.clickCount = 0;
			}
			this.windowCooldownTimer = 0f;
			this.UpdateWindows();
		}
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x00052C78 File Offset: 0x00051078
	private void OnDisable()
	{
		foreach (string guidStr in this.activeSounds)
		{
			Audio.self.stopLoopSound(guidStr, true);
		}
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x00052CDC File Offset: 0x000510DC
	private void UpdateWindows()
	{
		if (!PuzzleBarkAtNight_CelestialBody.isNight)
		{
			return;
		}
		this.UpdateWindow(this.leftWindow, this.leftWindowClicks, "11bf0d9a-9d39-44c4-9b02-f7063326571f");
		this.UpdateWindow(this.rightWindow, this.rightWindowClicks, "8e885a86-64a3-45a7-8fa0-de60d802a695");
		this.UpdateWindow(this.topWindow, this.topWindowClicks, "220bea43-b8b7-47c7-979c-f349d656071b");
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x00052D3C File Offset: 0x0005113C
	private void UpdateWindow(Transform t, int requiredClicks, string sound)
	{
		if (this.clickCount >= requiredClicks && !t.gameObject.activeSelf)
		{
			t.gameObject.SetActive(true);
			Audio.self.playLoopSound(sound);
			this.activeSounds.Add(sound);
		}
		else if (this.clickCount < requiredClicks && t.gameObject.activeSelf)
		{
			t.gameObject.SetActive(false);
			Audio.self.stopLoopSound(sound, true);
			this.activeSounds.Remove(sound);
		}
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x00052DD0 File Offset: 0x000511D0
	private void ThrowGarbage()
	{
		if (this.clickCount < this.garbageMinClicks)
		{
			return;
		}
		float num = UnityEngine.Random.Range(0f, 1f);
		if (num >= this.doubleGarbageProbability && num >= this.singleGarbageProbability)
		{
			return;
		}
		this.CreateGarbage();
		if (num < this.doubleGarbageProbability)
		{
			base.StartCoroutine(this.CreateDelayedGarbage());
		}
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x00052E38 File Offset: 0x00051238
	private void CreateGarbage()
	{
		Audio.self.playOneShot("9da01329-7657-4dbd-8d7b-26beab80fbe3", 1f);
		Transform child = this.garbage.GetChild(UnityEngine.Random.Range(0, this.garbage.childCount));
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(child.gameObject);
		Vector2 v = child.position;
		v.y += UnityEngine.Random.Range(-this.garbageScatter, this.garbageScatter);
		gameObject.transform.position = v;
		gameObject.transform.SetParent(base.transform);
		Vector2 force = this.garbageBaseForce;
		force.x += UnityEngine.Random.Range(-this.garbageRandomForce.x, this.garbageRandomForce.x);
		force.y += UnityEngine.Random.Range(0f, this.garbageRandomForce.y);
		gameObject.GetComponent<Rigidbody2D>().AddForce(force);
		base.StartCoroutine(this.DestroyGarbage(gameObject.transform));
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x00052F44 File Offset: 0x00051344
	private IEnumerator DestroyGarbage(Transform garbage)
	{
		yield return new WaitForSeconds(this.garbageTimeToLive);
		Vector2 scale = garbage.localScale;
		float timer = 0f;
		while (timer != this.garbageDisappearTime)
		{
			timer = Mathf.MoveTowards(timer, this.garbageDisappearTime, Time.deltaTime);
			garbage.localScale = Vector2.Lerp(scale, Vector2.zero, timer / this.garbageDisappearTime);
			yield return null;
		}
		UnityEngine.Object.Destroy(garbage.gameObject);
		yield break;
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x00052F68 File Offset: 0x00051368
	private IEnumerator CreateDelayedGarbage()
	{
		yield return new WaitForSeconds(this.doubleGarbageWait);
		this.CreateGarbage();
		yield break;
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x00052F84 File Offset: 0x00051384
	private void ProcessBark()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (!PuzzleBarkAtNight_CelestialBody.isNight)
		{
			return;
		}
		this.clickCount++;
		this.windowCooldownTimer = 0f;
		this.UpdateWindows();
		this.ThrowGarbage();
		int[] source = new int[]
		{
			this.leftWindowClicks,
			this.rightWindowClicks,
			this.topWindowClicks,
			this.maxClicks
		};
		if (this.clickCount >= source.Max())
		{
			base.StartCoroutine(this.EndLevel());
		}
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x0005301C File Offset: 0x0005141C
	private IEnumerator EndLevel()
	{
		if (this.finished)
		{
			yield break;
		}
		this.finished = true;
		this.GetComponentInPuzzleStats<PuzzleBarkAtNight_Dog>().MoveLine();
		AudioVoice_DogBarkAtNight voice = Global.self.currPuzzle.GetComponent<AudioVoice_DogBarkAtNight>();
		voice.end(true);
		while (!voice.canExitPuzzle)
		{
			yield return null;
		}
		Global.LevelCompleted(0f, true);
		Global.self.gotoNextLevel(false, null);
		yield break;
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x00053037 File Offset: 0x00051437
	private void OnDestroy()
	{
		PuzzleBarkAtNight_Dog.OnBark -= this.ProcessBark;
	}

	// Token: 0x040015EB RID: 5611
	public float windowCooldownTime;

	// Token: 0x040015EC RID: 5612
	public int maxClicks;

	// Token: 0x040015ED RID: 5613
	public Transform topWindow;

	// Token: 0x040015EE RID: 5614
	public int topWindowClicks;

	// Token: 0x040015EF RID: 5615
	public Transform leftWindow;

	// Token: 0x040015F0 RID: 5616
	public int leftWindowClicks;

	// Token: 0x040015F1 RID: 5617
	public Transform rightWindow;

	// Token: 0x040015F2 RID: 5618
	public int rightWindowClicks;

	// Token: 0x040015F3 RID: 5619
	[Header("Garbage")]
	public Transform garbage;

	// Token: 0x040015F4 RID: 5620
	public int garbageMinClicks;

	// Token: 0x040015F5 RID: 5621
	public float garbageTimeToLive;

	// Token: 0x040015F6 RID: 5622
	public float garbageDisappearTime;

	// Token: 0x040015F7 RID: 5623
	[Range(0f, 1f)]
	public float singleGarbageProbability;

	// Token: 0x040015F8 RID: 5624
	[Range(0f, 1f)]
	public float doubleGarbageProbability;

	// Token: 0x040015F9 RID: 5625
	public float doubleGarbageWait;

	// Token: 0x040015FA RID: 5626
	public float garbageScatter;

	// Token: 0x040015FB RID: 5627
	public Vector2 garbageBaseForce;

	// Token: 0x040015FC RID: 5628
	public Vector2 garbageRandomForce;

	// Token: 0x040015FD RID: 5629
	[HideInInspector]
	public bool finished;

	// Token: 0x040015FE RID: 5630
	private int clickCount;

	// Token: 0x040015FF RID: 5631
	private float windowCooldownTimer;

	// Token: 0x04001600 RID: 5632
	private List<string> activeSounds = new List<string>();
}
