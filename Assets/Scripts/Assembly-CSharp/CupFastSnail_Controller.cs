using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class CupFastSnail_Controller : MonoBehaviour
{
	// Token: 0x06000063 RID: 99 RVA: 0x00005E9C File Offset: 0x0000409C
	private void Update()
	{
		if (this.uiIcon.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
		{
			this.fill += this.fillOnClick;
			this.fill = Mathf.Clamp(this.fill, 0f, 1.2f);
			this.uiIcon.localScale = Vector3.one * 1.2f;
		}
		this.uiIcon.localScale = Vector3.Lerp(this.uiIcon.localScale, Vector3.one, Time.deltaTime * this.scaleFallof);
		this.uiIcon.localScale = Vector3.MoveTowards(this.uiIcon.localScale, Vector3.one, Time.deltaTime * 1f);
		if (this.fill > 0f)
		{
			this.fill -= this.fillFallof * Time.deltaTime * this.fill;
			this.UIFill.material.SetFloat("_Fill", Mathf.Min(1f - this.fill, 1f));
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00005FC4 File Offset: 0x000041C4
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(this.firstThreshold, Color.red);
		GizmosExtension.DrawVerticalLine(this.secondThreshold, Color.red);
		GizmosExtension.DrawVerticalLine(this.thirdThreshold, Color.red);
		GizmosExtension.DrawVerticalLine(this.lastThreshold, Color.blue);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00006011 File Offset: 0x00004211
	private void Awake()
	{
		this.icon.gameObject.SetActive(false);
		this.uiIcon.gameObject.SetActive(false);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00006035 File Offset: 0x00004235
	private void Start()
	{
		this.voice = this.GetPuzzleStats().GetComponent<AudioVoice_CupFastSnail>();
		base.StartCoroutine(this.prepSnails());
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00006058 File Offset: 0x00004258
	private IEnumerator prepSnails()
	{
		base.StartCoroutine(this.voice.playVoice(Voices.VoicePack08.CupFastSnail_Start));
		this.voice.subscribeToMarker("StartCountDown", delegate
		{
			this.countDown.gameObject.SetActive(true);
		});
		bool showUI = false;
		this.voice.subscribeToMarker("AllowClick", delegate
		{
			this.showUIIcon();
			showUI = true;
		});
		while (!showUI)
		{
			yield return null;
		}
		float time = 8f;
		while (!Input.GetMouseButton(0))
		{
			if (time > 0f)
			{
				time -= Time.deltaTime;
				if (time <= 0f)
				{
					base.StartCoroutine(this.voice.playVoice(Voices.VoicePack08.CupFastSnail_Tip));
				}
			}
			yield return null;
		}
		this.MoveSnails(-1f);
		base.StartCoroutine(this.CheckFirstThreshold());
		yield break;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00006074 File Offset: 0x00004274
	private IEnumerator CheckFirstThreshold()
	{
		while (this.fastestSnail.position.x < this.firstThreshold)
		{
			yield return null;
		}
		Audio.self.playOneShot("ee2d8a8d-f6c8-4e32-8293-94c0a1842567", 1f);
		this.StopSnails();
		base.StartCoroutine(this.voice.playVoice(Voices.VoicePack08.CupFastSnail_First));
		this.voice.subscribeToMarker("AddWeight", delegate
		{
			this.fastestSnail.GetComponent<CupFastSnail_Snail>().AddWeight();
			Audio.self.playOneShot("f9559cfb-8500-478c-a4b3-d551cd5eb21c", 1f);
		});
		this.voice.subscribeToMarker("AddWeight2", delegate
		{
			this.otherSnail.GetComponent<CupFastSnail_Snail>().AddWeight();
			Audio.self.playOneShot("f9559cfb-8500-478c-a4b3-d551cd5eb21c", 1f);
		});
		bool showUI = false;
		this.voice.subscribeToMarker("AllowClick", delegate
		{
			this.showUIIcon();
			showUI = true;
		});
		while (!showUI)
		{
			yield return null;
		}
		while (!Input.GetMouseButton(0))
		{
			yield return null;
		}
		this.MoveSnails(-1f);
		base.StartCoroutine(this.CheckSecondThreshold());
		Audio.self.playOneShot("1d21119c-76f8-4410-822d-f7912f238a57", 1f);
		yield break;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00006090 File Offset: 0x00004290
	private IEnumerator CheckSecondThreshold()
	{
		while (this.fastestSnail.position.x < this.secondThreshold)
		{
			yield return null;
		}
		Audio.self.playOneShot("ee2d8a8d-f6c8-4e32-8293-94c0a1842567", 1f);
		this.StopSnails();
		base.StartCoroutine(this.voice.playVoice(Voices.VoicePack08.CupFastSnail_Second));
		this.voice.subscribeToMarker("AddTurbo", delegate
		{
			this.playerSnail.GetComponent<CupFastSnail_Player>().AddTurbo();
			Audio.self.playOneShot("973176ef-43b9-4bb3-8844-1db9fef1a017", 1f);
		});
		bool showUI = false;
		this.voice.subscribeToMarker("AllowClick", delegate
		{
			this.showUIIcon();
			showUI = true;
		});
		while (!showUI)
		{
			yield return null;
		}
		while (!Input.GetMouseButton(0))
		{
			yield return null;
		}
		this.MoveSnails(1.5f);
		base.StartCoroutine(this.CheckThirdThreshold());
		Audio.self.playLoopSound("68518306-2ab3-4616-aa17-947d63ea2b2b");
		Audio.self.playOneShot("1d21119c-76f8-4410-822d-f7912f238a57", 1f);
		yield break;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000060AC File Offset: 0x000042AC
	private IEnumerator CheckThirdThreshold()
	{
		while (this.fastestSnail.position.x < this.thirdThreshold)
		{
			yield return null;
		}
		base.StartCoroutine(this.voice.playVoice(Voices.VoicePack08.CupFastSnail_Third));
		this.voice.subscribeToMarker("StopSnail", delegate
		{
			Audio.self.playOneShot("ee2d8a8d-f6c8-4e32-8293-94c0a1842567", 1f);
			Audio.self.stopLoopSound("68518306-2ab3-4616-aa17-947d63ea2b2b", true);
			this.StopSnails();
		});
		this.voice.subscribeToMarker("MoveFinish", delegate
		{
			this.finishLine.position = new Vector3(this.secondFinishLinePosition, this.finishLine.position.y);
			this.playerSnail.GetComponent<CupFastSnail_Player>().Reverse();
		});
		bool showUI = false;
		this.voice.subscribeToMarker("AllowClick", delegate
		{
			this.showUIIcon();
			showUI = true;
		});
		while (!showUI)
		{
			yield return null;
		}
		while (!Input.GetMouseButton(0))
		{
			yield return null;
		}
		this.fastestSnail.localScale = new Vector3(-this.fastestSnail.localScale.x, this.fastestSnail.localScale.y);
		this.otherSnail.localScale = new Vector3(-this.otherSnail.localScale.x, this.otherSnail.localScale.y);
		this.MoveSnails(2f);
		Audio.self.playLoopSound("68518306-2ab3-4616-aa17-947d63ea2b2b");
		Audio.self.playOneShot("1d21119c-76f8-4410-822d-f7912f238a57", 1f);
		base.StartCoroutine(this.CheckPlayerStupidity());
		yield break;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000060C8 File Offset: 0x000042C8
	private IEnumerator CheckPlayerStupidity()
	{
		while (this.fastestSnail.position.x > this.lastThreshold)
		{
			yield return null;
		}
		base.StartCoroutine(this.voice.playVoice(Voices.VoicePack08.CupFastSnail_Last));
		this.voice.subscribeToMarker("Stop", delegate
		{
			Audio.self.stopLoopSound("68518306-2ab3-4616-aa17-947d63ea2b2b", true);
			Audio.self.playOneShot("ee2d8a8d-f6c8-4e32-8293-94c0a1842567", 1f);
			this.StopSnails();
		});
		this.voice.subscribeToMarker("Give", delegate
		{
			Global.CupAcquired(this.playerSnail);
		});
		yield break;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000060E4 File Offset: 0x000042E4
	public void MoveSnails(float sec = -1f)
	{
		this.fastestSnail.GetComponent<CupFastSnail_Snail>().StartMoving();
		this.otherSnail.GetComponent<CupFastSnail_Snail>().StartMoving();
		this.playerSnail.GetComponent<CupFastSnail_Player>().canBeClicked = true;
		this.playerSnail.GetComponent<CupFastSnail_Player>().moveSnail();
		base.StartCoroutine(this.showIcon(sec));
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00006140 File Offset: 0x00004340
	public void StopSnails()
	{
		this.fastestSnail.GetComponent<CupFastSnail_Snail>().StopMoving();
		this.otherSnail.GetComponent<CupFastSnail_Snail>().StopMoving();
		this.playerSnail.GetComponent<CupFastSnail_Player>().StopMoving();
		this.playerSnail.GetComponent<CupFastSnail_Player>().canBeClicked = false;
		this.uiIcon.gameObject.SetActive(false);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000061A0 File Offset: 0x000043A0
	private IEnumerator showIcon(float sec)
	{
		if (sec <= 0f)
		{
			sec = 3f;
		}
		this.icon.gameObject.SetActive(true);
		this.uiIcon.gameObject.SetActive(true);
		yield return new WaitForSeconds(sec);
		this.icon.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000061C2 File Offset: 0x000043C2
	private void showUIIcon()
	{
		this.icon.gameObject.SetActive(true);
		this.uiIcon.gameObject.SetActive(true);
		this.fill = this.fillInitial;
	}

	// Token: 0x040000E1 RID: 225
	[Header("Thresholds")]
	public float firstThreshold;

	// Token: 0x040000E2 RID: 226
	public float secondThreshold;

	// Token: 0x040000E3 RID: 227
	public float thirdThreshold;

	// Token: 0x040000E4 RID: 228
	public float lastThreshold;

	// Token: 0x040000E5 RID: 229
	[Header("Snail stuff")]
	public Transform fastestSnail;

	// Token: 0x040000E6 RID: 230
	public Transform otherSnail;

	// Token: 0x040000E7 RID: 231
	public Transform playerSnail;

	// Token: 0x040000E8 RID: 232
	[Header("Finish line")]
	public Transform finishLine;

	// Token: 0x040000E9 RID: 233
	public float secondFinishLinePosition;

	// Token: 0x040000EA RID: 234
	public float waitBeforeTrophy = 3f;

	// Token: 0x040000EB RID: 235
	[Header("Click icon")]
	public Transform icon;

	// Token: 0x040000EC RID: 236
	public Transform uiIcon;

	// Token: 0x040000ED RID: 237
	public SpriteRenderer UIFill;

	// Token: 0x040000EE RID: 238
	public Transform countDown;

	// Token: 0x040000EF RID: 239
	private float fill = 0.1f;

	// Token: 0x040000F0 RID: 240
	public float fillInitial = 0.1f;

	// Token: 0x040000F1 RID: 241
	public float fillOnClick = 0.1f;

	// Token: 0x040000F2 RID: 242
	public float fillFallof = 10f;

	// Token: 0x040000F3 RID: 243
	public float scaleFallof = 1f;

	// Token: 0x040000F4 RID: 244
	private AudioVoice_CupFastSnail voice;
}
