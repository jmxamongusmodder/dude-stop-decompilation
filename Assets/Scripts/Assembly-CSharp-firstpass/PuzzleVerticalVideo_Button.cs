using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class PuzzleVerticalVideo_Button : MonoBehaviour
{
	// Token: 0x06001CEC RID: 7404 RVA: 0x0007D7E8 File Offset: 0x0007BBE8
	private void Update()
	{
		if (this.screen != null && this.flash)
		{
			if (this.dimOut)
			{
				this.screenTimeLeft -= Time.deltaTime;
				if (this.screenTimeLeft < 0f)
				{
					this.dimOut = false;
					this.flash = false;
				}
				float a = Mathf.Lerp(0f, 1f, this.screenTimeLeft / this.screenTimeOut);
				this.screen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a);
			}
			else
			{
				this.screenTimeLeft += Time.deltaTime;
				if (this.screenTimeLeft >= this.screenTime)
				{
					this.screenTimeLeft = this.screenTime;
					this.dimOut = true;
				}
				float a2 = Mathf.Lerp(0f, 1f, this.screenTimeLeft / this.screenTime);
				this.screen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, a2);
			}
		}
	}

	// Token: 0x06001CED RID: 7405 RVA: 0x0007D90C File Offset: 0x0007BD0C
	private void OnDisable()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
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

	// Token: 0x06001CEE RID: 7406 RVA: 0x0007D9A0 File Offset: 0x0007BDA0
	private void OnMouseOver()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
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

	// Token: 0x06001CEF RID: 7407 RVA: 0x0007DA40 File Offset: 0x0007BE40
	private void OnMouseExit()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
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

	// Token: 0x06001CF0 RID: 7408 RVA: 0x0007DAD4 File Offset: 0x0007BED4
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.recording)
		{
			Audio.self.playOneShot("c3104b2c-e181-4846-bded-a6b422d6af52", 1f);
			base.transform.parent.GetComponent<PuzzleVerticalVideo_Phone>().recording = true;
			foreach (PuzzleVerticalVideo_Button puzzleVerticalVideo_Button in this.GetComponentsInPuzzleStats(false))
			{
				puzzleVerticalVideo_Button.enabled = false;
			}
		}
		else
		{
			Audio.self.playOneShot("fc5ff9c4-f18f-40fb-a57f-305a87610971", 1f);
			this.flash = true;
			this.screenTimeLeft = 0f;
			this.GetPuzzleStats().GetComponent<AudioVoice_VerticalVideo>().takePhoto();
		}
	}

	// Token: 0x04001B82 RID: 7042
	public bool recording;

	// Token: 0x04001B83 RID: 7043
	[Tooltip("Leave this blank if recording")]
	public Transform screen;

	// Token: 0x04001B84 RID: 7044
	public float screenTime = 0.08f;

	// Token: 0x04001B85 RID: 7045
	public float screenTimeOut = 0.16f;

	// Token: 0x04001B86 RID: 7046
	public float whiten = 0.15f;

	// Token: 0x04001B87 RID: 7047
	private bool flash;

	// Token: 0x04001B88 RID: 7048
	private bool dimOut;

	// Token: 0x04001B89 RID: 7049
	private float screenTimeLeft;
}
