using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class AudioVoice_Nutella : AudioVoiceReceive
{
	// Token: 0x060011E2 RID: 4578 RVA: 0x00023C27 File Offset: 0x00022027
	private void Update()
	{
		if (Global.self.DEBUG && Input.GetKeyDown(KeyCode.H))
		{
			base.StartCoroutine(this.throwBread());
		}
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00023C51 File Offset: 0x00022051
	protected override void setActiveAfterVoice()
	{
		base.setActiveAfterVoice();
		if (!this.active)
		{
			return;
		}
		this.canBoo = (UnityEngine.Random.value > 0.9f);
		base.StartCoroutine(this.wait());
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x00023C84 File Offset: 0x00022084
	private IEnumerator wait()
	{
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!base.enabled)
		{
			yield break;
		}
		if (this.nutellaTaken == 0)
		{
			this.voice = Audio.self.playVoice(this.wait1);
			this.voice.start(true);
			while (this.voice != null && this.voice.isPlaying())
			{
				yield return null;
			}
			yield return new WaitForSeconds(5f);
		}
		while (this.nutellaTaken == 0)
		{
			yield return null;
		}
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!base.enabled)
		{
			yield break;
		}
		if (this.spreadInd == 0 && !this.spreadStarted)
		{
			this.voice = Audio.self.playVoice(this.wait2);
			this.voice.start(true);
			while (this.voice != null && this.voice.isPlaying())
			{
				yield return null;
			}
			yield return new WaitForSeconds(5f);
		}
		while (this.spreadInd < 3)
		{
			yield return null;
		}
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		if (!base.enabled)
		{
			yield break;
		}
		yield return new WaitForSeconds(4f);
		if (this.nutellaTaken >= 3 && !this.snapped)
		{
			this.voice = Audio.self.playVoice(this.wait3);
			this.voice.start(true);
		}
		yield break;
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x00023C9F File Offset: 0x0002209F
	public void takeNutella()
	{
		this.nutellaTaken++;
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x00023CAF File Offset: 0x000220AF
	public void spreadPiece()
	{
		this.spreadStarted = true;
		this.canBoo = false;
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x00023CC0 File Offset: 0x000220C0
	public void spreadFullPiece()
	{
		if (this.spreadInd >= this.spreadedLines.Length)
		{
			return;
		}
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.spreadedLines[this.spreadInd].levelVoiceId))
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				return;
			}
			this.voice = Audio.self.playVoice(this.spreadedLines[this.spreadInd]);
			this.voice.start(true);
		}
		this.spreadInd++;
		this.spreadStarted = false;
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x00023D68 File Offset: 0x00022168
	public void onKnifePickUp()
	{
		if (this.canBoo && !this.booed)
		{
			if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.booLine.levelVoiceId))
			{
				if (this.voice != null && this.voice.isPlaying())
				{
					this.voice.stop();
				}
				this.voice = Audio.self.playVoice(this.booLine);
				this.voice.start(true);
			}
			this.booed = true;
		}
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x00023E00 File Offset: 0x00022200
	public void onJarSnapToBin()
	{
		if (this.nutellaTaken >= 3)
		{
			this.snapped = true;
			return;
		}
		if (this.snapedTimes > 1 || this.snapIsPlaying)
		{
			return;
		}
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		StandaloneLevelVoice random = this.snapLines.GetRandom<StandaloneLevelVoice>();
		if (this.prevSnapLine != string.Empty)
		{
			random = (from x in this.snapLines
			where x.levelVoiceId != this.prevSnapLine
			select x).ToList<StandaloneLevelVoice>().GetRandom<StandaloneLevelVoice>();
		}
		this.prevSnapLine = random.levelVoiceId;
		this.voice = Audio.self.playVoice(random);
		this.voice.subscribeToStopped(this, delegate(VoiceLine line)
		{
			this.snapIsPlaying = false;
		});
		this.voice.start(true);
		this.snapIsPlaying = true;
		this.snapedTimes++;
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x00023EF8 File Offset: 0x000222F8
	public void throwKnifeAway()
	{
		if (this.triggerFlyBread)
		{
			return;
		}
		this.triggerFlyBread = true;
		if (SerializablePuzzleStats.Get(base.transform.name).tryUseOneTime(this.knifeEnd.levelVoiceId))
		{
			base.StartCoroutine(this.throwBread());
		}
		else
		{
			if (this.voice != null && this.voice.isPlaying())
			{
				this.voice.stop();
			}
			Global.LevelCompleted(0f, true);
		}
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x00023F80 File Offset: 0x00022380
	private IEnumerator throwBread()
	{
		if (this.voice != null && this.voice.isPlaying())
		{
			this.voice.stop();
		}
		global::Console.self.canOpen = false;
		this.voice = Audio.self.playVoice(this.knifeEnd);
		this.voice.subscribeToMarker(this, new Action<VoiceLine, string>(this.markerReached));
		this.voice.start(true);
		while (!this.breadFlew)
		{
			yield return null;
		}
		Vector2 origPos = this.breadSprite.localPosition;
		Animator anim = this.breadParent.GetComponent<Animator>();
		anim.enabled = true;
		Audio.self.playOneShot("993add3f-bffe-495c-bcf9-0d337347dd9a", 1f);
		this.breadJumping = true;
		while (this.breadJumping)
		{
			yield return null;
		}
		anim.enabled = false;
		Vector2 targetPos = Camera.main.ViewportToWorldPoint(Vector2.zero);
		targetPos.y = this.breadEndY;
		targetPos.x += this.breadLeftX;
		Vector2 pos = this.breadParent.transform.position;
		float totalRot = 270f;
		float dist = Vector2.Distance(this.breadParent.transform.position, targetPos);
		bool mirror = false;
		while (this.breadParent.transform.position != targetPos)
		{
			this.breadParent.transform.position = Vector2.MoveTowards(this.breadParent.transform.position, targetPos, dist * (Time.deltaTime / this.breadFlyTime));
			float currDist = Vector2.Distance(this.breadParent.transform.position, targetPos);
			this.breadSprite.rotation = Quaternion.Euler(0f, 0f, totalRot * (1f - currDist / dist));
			float currInDist = Vector2.Distance(this.breadSprite.localPosition, origPos);
			this.breadSprite.localPosition = Vector2.MoveTowards(this.breadSprite.localPosition, origPos, currInDist * (Time.deltaTime / (this.breadFlyTime / 5f)));
			if (!mirror && dist / currDist > 2f)
			{
				this.breadSprite.localScale = new Vector3(-this.breadSprite.localScale.x, this.breadSprite.localScale.y, this.breadSprite.localScale.z);
				mirror = true;
				this.breadSprite.GetChild(1).gameObject.SetActive(false);
				this.breadSprite.GetChild(2).gameObject.SetActive(false);
				this.breadSprite.GetChild(3).gameObject.SetActive(false);
			}
			yield return null;
		}
		Audio.self.playOneShot("180fddee-80a9-4dbf-b94f-cae97cf7912a", 1f);
		anim.SetTrigger("onwall");
		yield return new WaitForSeconds(0.35f);
		anim.enabled = true;
		while (this.voice != null && this.voice.isPlaying())
		{
			yield return null;
		}
		global::Console.self.canOpen = true;
		this.knifeInTheBin = true;
		Global.LevelCompleted(0f, true);
		yield break;
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x00023F9B File Offset: 0x0002239B
	public void breadJumpEnded()
	{
		this.breadJumping = false;
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x00023FA4 File Offset: 0x000223A4
	protected override void markerReached(VoiceLine line, string markerName)
	{
		base.markerReached(line, markerName);
		if (markerName != null)
		{
			if (!(markerName == "throw"))
			{
				if (markerName == "swap")
				{
					Animator component = this.breadParent.GetComponent<Animator>();
					component.enabled = true;
					component.SetTrigger("swap");
					Audio.self.playOneShot("b66c022d-339c-438b-9684-41e8f7f35f90", 1f);
				}
			}
			else
			{
				this.breadFlew = true;
			}
		}
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x00024028 File Offset: 0x00022428
	public override void subsctibeToEnding(endTextControl item)
	{
		if (this.knifeInTheBin)
		{
			item.SetEnding(LevelVoice.getEndText(this.knifeEnd, Global.self.currLanguage), false);
			return;
		}
		base.subsctibeToEnding(item);
	}

	// Token: 0x04000EEE RID: 3822
	[Space(10f)]
	public StandaloneLevelVoice booLine;

	// Token: 0x04000EEF RID: 3823
	public StandaloneLevelVoice[] spreadedLines;

	// Token: 0x04000EF0 RID: 3824
	public StandaloneLevelVoice[] snapLines;

	// Token: 0x04000EF1 RID: 3825
	public StandaloneLevelVoice knifeEnd;

	// Token: 0x04000EF2 RID: 3826
	public StandaloneLevelVoice wait1;

	// Token: 0x04000EF3 RID: 3827
	public StandaloneLevelVoice wait2;

	// Token: 0x04000EF4 RID: 3828
	public StandaloneLevelVoice wait3;

	// Token: 0x04000EF5 RID: 3829
	private bool booed;

	// Token: 0x04000EF6 RID: 3830
	private bool canBoo;

	// Token: 0x04000EF7 RID: 3831
	private int spreadInd;

	// Token: 0x04000EF8 RID: 3832
	private int snapedTimes;

	// Token: 0x04000EF9 RID: 3833
	private string prevSnapLine = string.Empty;

	// Token: 0x04000EFA RID: 3834
	private bool snapIsPlaying;

	// Token: 0x04000EFB RID: 3835
	private int nutellaTaken;

	// Token: 0x04000EFC RID: 3836
	private bool spreadStarted;

	// Token: 0x04000EFD RID: 3837
	private bool snapped;

	// Token: 0x04000EFE RID: 3838
	private bool knifeInTheBin;

	// Token: 0x04000EFF RID: 3839
	[Header("Bread fly")]
	public Transform breadParent;

	// Token: 0x04000F00 RID: 3840
	public Transform breadSprite;

	// Token: 0x04000F01 RID: 3841
	public float breadLeftX = 0.5f;

	// Token: 0x04000F02 RID: 3842
	public float breadEndY;

	// Token: 0x04000F03 RID: 3843
	public float breadFlyTime = 0.5f;

	// Token: 0x04000F04 RID: 3844
	private bool breadFlew;

	// Token: 0x04000F05 RID: 3845
	[Space(10f)]
	public GameObject breadLog;

	// Token: 0x04000F06 RID: 3846
	private bool breadJumping;

	// Token: 0x04000F07 RID: 3847
	private bool triggerFlyBread;
}
