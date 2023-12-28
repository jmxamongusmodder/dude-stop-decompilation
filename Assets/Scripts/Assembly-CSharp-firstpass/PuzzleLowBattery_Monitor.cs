using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200042B RID: 1067
public class PuzzleLowBattery_Monitor : MonoBehaviour
{
	// Token: 0x06001B33 RID: 6963 RVA: 0x0006EDF9 File Offset: 0x0006D1F9
	private void Start()
	{
		this.puzzleStarted = true;
		Audio.self.playLoopSound("cc802c83-c74f-4e3a-b821-ae28bf3bb099");
		base.StartCoroutine(this.PopupCoroutine());
		base.StartCoroutine(this.BatteryBlinkingCoroutine());
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x0006EE2B File Offset: 0x0006D22B
	private void OnDisable()
	{
		if (!this.puzzleStarted)
		{
			return;
		}
		Audio.self.stopLoopSound("cc802c83-c74f-4e3a-b821-ae28bf3bb099", true);
		base.StartCoroutine(this.BlackScreenCoroutine());
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x0006EE56 File Offset: 0x0006D256
	private void OnMouseEnter()
	{
		this.saveHover.gameObject.SetActive(true);
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x0006EE69 File Offset: 0x0006D269
	private void OnMouseExit()
	{
		this.saveHover.gameObject.SetActive(false);
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x0006EE7C File Offset: 0x0006D27C
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		Audio.self.playOneShot("ca433ac7-d6c2-4cc8-9bac-1c31badeed57", 1f);
		Global.LevelFailed(0f, true);
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x0006EEAC File Offset: 0x0006D2AC
	private IEnumerator PopupCoroutine()
	{
		yield return new WaitForSeconds(this.waitBeforePopup);
		Audio.self.playOneShot("9c7ef7ee-7ea2-451a-9413-a68e720d5e5d", 1f);
		this.popup.gameObject.SetActive(true);
		Dictionary<SpriteRenderer, float> alphas = new Dictionary<SpriteRenderer, float>();
		SpriteRenderer[] sprites = this.popup.GetComponentsInChildren<SpriteRenderer>(true);
		CanvasGroup group = this.popup.GetComponentInChildren<CanvasGroup>();
		foreach (SpriteRenderer spriteRenderer in sprites)
		{
			alphas.Add(spriteRenderer, spriteRenderer.color.a);
		}
		float timer = 0f;
		while (timer != this.popupAlphaTime)
		{
			timer = Mathf.MoveTowards(timer, this.popupAlphaTime, Time.deltaTime);
			float t = Mathf.Sin(timer / this.popupAlphaTime * 3.1415927f * 0.5f);
			foreach (SpriteRenderer spriteRenderer2 in sprites)
			{
				Color color = spriteRenderer2.color;
				color.a = t * alphas[spriteRenderer2];
				spriteRenderer2.color = color;
			}
			group.alpha = t;
			yield return null;
		}
		this.popup.GetComponent<Collider2D>().enabled = true;
		yield break;
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x0006EEC8 File Offset: 0x0006D2C8
	private IEnumerator BatteryBlinkingCoroutine()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.batteryBlinkTime);
			this.battery.gameObject.SetActive(false);
			yield return new WaitForSeconds(this.batteryBlinkTime);
			this.battery.gameObject.SetActive(true);
		}
		yield break;
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x0006EEE4 File Offset: 0x0006D2E4
	private IEnumerator BlackScreenCoroutine()
	{
		this.blackScreen.gameObject.SetActive(true);
		SpriteRenderer screen = this.blackScreen.GetComponent<SpriteRenderer>();
		SpriteRenderer hotspot = this.blackScreen.GetChild(0).GetComponent<SpriteRenderer>();
		screen.color = Extensions.Color.SetAlpha(screen.color, 0f);
		hotspot.color = Extensions.Color.SetAlpha(hotspot.color, 0f);
		float hotspotTime = this.whiteToBlackTime * (1f - this.hotspotEnablePercent);
		float hotspotTimer = 0f;
		float timer = 0f;
		Audio.self.playOneShot("a8f21442-b54d-4bf3-a6a2-9352c9983b7c", 1f);
		do
		{
			timer = Mathf.MoveTowards(timer, this.whiteToBlackTime, Time.deltaTime);
			float t = timer / this.whiteToBlackTime;
			screen.color = Extensions.Color.SetAlpha(screen.color, t);
			if (t > this.hotspotEnablePercent)
			{
				hotspotTimer = Mathf.MoveTowards(hotspotTimer, hotspotTime, Time.deltaTime);
				hotspot.color = Extensions.Color.SetAlpha(hotspot.color, hotspotTimer / hotspotTime);
			}
			yield return null;
		}
		while (timer < this.whiteToBlackTime);
		yield break;
	}

	// Token: 0x0400196B RID: 6507
	[Header("Popup")]
	public float control;

	// Token: 0x0400196C RID: 6508
	public Transform popup;

	// Token: 0x0400196D RID: 6509
	public float waitBeforePopup;

	// Token: 0x0400196E RID: 6510
	public float popupAlphaTime;

	// Token: 0x0400196F RID: 6511
	[Header("Battery")]
	public Transform blackScreen;

	// Token: 0x04001970 RID: 6512
	public float whiteToBlackTime = 0.5f;

	// Token: 0x04001971 RID: 6513
	public float hotspotEnablePercent = 0.95f;

	// Token: 0x04001972 RID: 6514
	public Transform battery;

	// Token: 0x04001973 RID: 6515
	public float batteryBlinkTime;

	// Token: 0x04001974 RID: 6516
	private bool puzzleStarted;

	// Token: 0x04001975 RID: 6517
	[Header("Save")]
	public Transform saveHover;
}
