using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class AudioVoice_LastCup : AudioVoice
{
	// Token: 0x060011A2 RID: 4514 RVA: 0x00021DB4 File Offset: 0x000201B4
	public override void setActive(bool on)
	{
		base.setActive(on);
		if (!this.active)
		{
			return;
		}
		Global.self.canExitEndScreen = false;
		global::Console.self.canOpen = false;
		base.ps.UIScreenSecondary = base.ps.UIScreenCurr;
		base.ps.UIScreen = null;
		base.ps.UIScreenCurr = null;
		base.ps.loadUIOnStart = false;
		base.StartCoroutine(this.AwkwardSilence());
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x00021E34 File Offset: 0x00020234
	private IEnumerator AwkwardSilence()
	{
		if (AwardController.self == null)
		{
			Audio.self.StartMusic("f24b6c30-feac-4181-b42d-3c1f8242383d");
			yield return null;
		}
		Audio.self.ChangeMusicParameter("f24b6c30-feac-4181-b42d-3c1f8242383d", "ToHappyMusic", 1f);
		if (!this.skipSilence)
		{
			float time = 0f;
			while (time < this.awkwardSilenceMax)
			{
				if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.L))
				{
					Audio.self.StopMusic("f24b6c30-feac-4181-b42d-3c1f8242383d");
					break;
				}
				time += Time.deltaTime;
				yield return null;
			}
		}
		if (this.cheatUsed)
		{
			yield break;
		}
		this.voice = Audio.self.playVoice(this.voiceLine);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		base.StartCoroutine(this.fireFireworks());
		Audio.self.StartMusic("a22c5ac2-b062-4a18-830a-de691bff84d4");
		yield break;
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x00021E50 File Offset: 0x00020250
	private void Update()
	{
		if (Global.self.DEBUG)
		{
			if (Input.GetKeyDown(KeyCode.K))
			{
				Audio.self.StopMusic("f24b6c30-feac-4181-b42d-3c1f8242383d");
				this.cheatUsed = true;
				if (this.voice != null && this.voice.isPlaying())
				{
					this.voice.stop();
				}
				this.ShowCredits();
			}
			if (Input.GetKeyDown(KeyCode.L) && this.voice != null && this.voice.isPlaying())
			{
				this.voice.setParameter("DebugSkip", 1f);
				this.celebration = false;
			}
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x00021F00 File Offset: 0x00020300
	private void ShowCredits()
	{
		Audio.self.StopMusic("f24b6c30-feac-4181-b42d-3c1f8242383d");
		Audio.self.StopMusic("a22c5ac2-b062-4a18-830a-de691bff84d4");
		Camera.main.GetComponent<BackgroundControl>().swapInstantly(this.newBG);
		if (base.ps.UIScreenCurr != null)
		{
			UnityEngine.Object.Destroy(base.ps.UIScreenCurr.gameObject);
		}
		base.ps.UIScreenSecondary.GetComponent<EndCredits>().showCredits();
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.gameObject.SetActive(false);
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
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x00021FE0 File Offset: 0x000203E0
	private IEnumerator fireFireworks()
	{
		this.firework.EnableEmmision(true);
		base.StartCoroutine(this.particlesSound());
		this.confety.EnableEmmision(true);
		yield return new WaitForSeconds(0.5f);
		this.controller.showSign();
		float time = 0f;
		float timeMax = this.fireworkIntensity.GetAnimationLength();
		while (this.celebration)
		{
			time = Mathf.MoveTowards(time, timeMax, Time.deltaTime);
			this.firework.SetEmissionRate(this.fireworkIntensity.Evaluate(time) * this.fireworkAmount);
			yield return null;
		}
		this.confety.EnableEmmision(false);
		this.firework.EnableEmmision(false);
		yield return new WaitForSeconds(2f);
		this.controller.enableSignClicks();
		yield break;
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x00021FFC File Offset: 0x000203FC
	private IEnumerator particlesSound()
	{
		float count = 0f;
		float prevCount = 0f;
		while (this.firework.emission.enabled || prevCount > 0f)
		{
			count = (float)this.firework.particleCount;
			if (prevCount > count)
			{
				Audio.self.playOneShot("4f07b846-8c36-4bd4-b76c-1b5a28525d64", 1f);
			}
			else if (prevCount < count)
			{
				Audio.self.playOneShot("aa4532b5-5ab4-469b-a806-e76a5dd5be49", 1f);
			}
			prevCount = count;
			yield return null;
		}
		yield break;
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x00022018 File Offset: 0x00020418
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		switch (markerName)
		{
		case "stopCelebration":
			this.celebration = false;
			break;
		case "BreakSign":
			if (this.signBroken)
			{
				this.controller.dropSign();
			}
			break;
		case "spawnCup1":
			this.canRemoveCup = false;
			this.controller.ShowCup(1);
			Audio.self.playOneShot("806fda87-176a-4046-b210-a31d15a89b61", 1f);
			break;
		case "spawnCup2":
			this.canRemoveCup = false;
			this.controller.ShowCup(2);
			Audio.self.playOneShot("abe92d56-87e4-4d5f-9dbc-f66e86801759", 1f);
			break;
		case "spawnCup3":
			this.controller.ShowCup(3);
			Audio.self.playOneShot("806fda87-176a-4046-b210-a31d15a89b61", 1f);
			break;
		case "spawnCup4":
			this.controller.ShowCup(4);
			Audio.self.playOneShot("806fda87-176a-4046-b210-a31d15a89b61", 1f);
			break;
		case "spawnCup5":
			this.controller.ShowCup(5);
			Audio.self.playOneShot("806fda87-176a-4046-b210-a31d15a89b61", 1f);
			break;
		case "dropCup":
			this.canRemoveCup = true;
			break;
		case "DI":
			this.controller.DisableCup();
			this.enableInterructions = false;
			break;
		case "showCredits":
			this.ShowCredits();
			break;
		}
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x00022240 File Offset: 0x00020640
	public bool breakSign()
	{
		if (!this.enableInterructions)
		{
			return false;
		}
		this.signBroken = true;
		this.voice.setParameter(1f);
		return true;
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x00022267 File Offset: 0x00020667
	public void pickUpCup()
	{
		this.voice.setParameter(2f);
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x00022279 File Offset: 0x00020679
	public override void subsctibeToEnding(endTextControl item)
	{
		Global.self.canExitEndScreen = false;
		item.SetEnding(LevelVoice.getEndText(this.endLine, Global.self.currLanguage), false);
	}

	// Token: 0x04000EB5 RID: 3765
	public StandaloneLevelVoice endLine;

	// Token: 0x04000EB6 RID: 3766
	public bool DebugSkipLine;

	// Token: 0x04000EB7 RID: 3767
	[Space(10f)]
	public ParticleSystem firework;

	// Token: 0x04000EB8 RID: 3768
	public ParticleSystem confety;

	// Token: 0x04000EB9 RID: 3769
	public Color[] fireworkColors;

	// Token: 0x04000EBA RID: 3770
	public AnimationCurve fireworkIntensity;

	// Token: 0x04000EBB RID: 3771
	public float fireworkAmount = 1f;

	// Token: 0x04000EBC RID: 3772
	private bool celebration = true;

	// Token: 0x04000EBD RID: 3773
	public CupLastCup_Controller controller;

	// Token: 0x04000EBE RID: 3774
	private bool enableInterructions = true;

	// Token: 0x04000EBF RID: 3775
	private bool signBroken;

	// Token: 0x04000EC0 RID: 3776
	[HideInInspector]
	public bool canRemoveCup;

	// Token: 0x04000EC1 RID: 3777
	[Header("Credits")]
	public Transform newBG;

	// Token: 0x04000EC2 RID: 3778
	[Space(10f)]
	public bool skipSilence;

	// Token: 0x04000EC3 RID: 3779
	private bool cheatUsed;

	// Token: 0x04000EC4 RID: 3780
	public float awkwardSilenceMax = 5f;
}
