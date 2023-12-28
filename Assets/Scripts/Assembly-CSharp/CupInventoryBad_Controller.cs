using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class CupInventoryBad_Controller : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x0000720C File Offset: 0x0000540C
	private void Start()
	{
		base.StartCoroutine(this.AddingToInventoryCoroutine());
	}

	// Token: 0x06000088 RID: 136 RVA: 0x0000721B File Offset: 0x0000541B
	private void OnDisable()
	{
		if (this.duckCoroutine != null)
		{
			base.StopCoroutine(this.duckCoroutine);
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00007234 File Offset: 0x00005434
	public void TrophyGrabbed(CupInventoryBad_Trophy trophy)
	{
		CupInventoryBad_Controller.Effects effects = this.availableEffects[0];
		this.availableEffects.Remove(effects);
		if (effects != CupInventoryBad_Controller.Effects.BelongsToDuck)
		{
			if (effects != CupInventoryBad_Controller.Effects.Dropped)
			{
				if (effects == CupInventoryBad_Controller.Effects.Dirty)
				{
					base.StartCoroutine(this.DirtyCoroutine(trophy));
				}
			}
			else
			{
				base.StartCoroutine(this.DroppedCoroutine(trophy));
			}
		}
		else
		{
			this.duckCoroutine = base.StartCoroutine(this.DuckCoroutine(trophy));
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000072B4 File Offset: 0x000054B4
	public void PotatoGrabbed()
	{
		if (this.availableEffects.Count == 0)
		{
			this.potato.returnToInventory = false;
			this.potato.EmulateMouseUp();
			this.potato.OnMouseUp();
			Global.CupAcquired(this.potato.transform);
			InventoryControl.self.hideInventory();
		}
		else
		{
			this.GetPuzzleStats().GetComponent<AudioVoice_CupInventoryBad>().pickPotato(this.potatoGrabs++ == 0);
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00007338 File Offset: 0x00005538
	private void SetClickStatusTo(bool status)
	{
		foreach (CupInventory_InventoryItem cupInventory_InventoryItem in this.trophies)
		{
			cupInventory_InventoryItem.lockMouse = !status;
		}
		this.potato.GetComponent<CupInventory_InventoryItem>().lockMouse = !status;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00007384 File Offset: 0x00005584
	private IEnumerator DuckCoroutine(CupInventoryBad_Trophy trophy)
	{
		Global.self.canBePaused = false;
		this.SetClickStatusTo(false);
		InventoryControl.self.hideInventory();
		trophy.ignoreMouseUp = true;
		trophy.transform.Find("duck_sign").gameObject.SetActive(true);
		AudioVoice_CupInventoryBad voice = this.GetPuzzleStats().GetComponent<AudioVoice_CupInventoryBad>();
		voice.pickCupDuck();
		while (!voice.showDuckNow)
		{
			yield return null;
		}
		GlitchEffectController.self.startGlitch(10f, 2f);
		Audio.self.playOneShot("166c4970-71ed-4f64-97bf-6a09331f1602", 1f);
		Vector2 screen = Camera.main.ViewportToWorldPoint(Vector2.one);
		Vector2 start = new Vector2(screen.x + this.duckStartOffset, this.duck.localPosition.y);
		Vector3 end = new Vector2(screen.x + this.duckEndOffset, this.duck.localPosition.y);
		end = base.transform.InverseTransformPoint(end);
		this.duck.position = start;
		this.duck.gameObject.SetActive(true);
		float timer = 0f;
		while (!voice.showDuckBlack)
		{
			timer = Mathf.MoveTowards(timer, this.duckMoveTime, Time.deltaTime);
			this.duck.localPosition = Vector2.Lerp(start, end, timer / this.duckMoveTime);
			yield return null;
		}
		this.blackScreen.gameObject.SetActive(true);
		GlitchEffectController.self.stopGlitch();
		while (!voice.hideDuckBlack)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(trophy.gameObject);
		this.duck.gameObject.SetActive(false);
		SpriteRenderer rend = this.blackScreen.GetComponent<SpriteRenderer>();
		Color rendColor = rend.color;
		timer = 0f;
		while (timer != this.blackScreenLerpTime)
		{
			timer = Mathf.MoveTowards(timer, this.blackScreenLerpTime, Time.deltaTime);
			rendColor.a = 1f - timer / this.blackScreenLerpTime;
			rend.color = rendColor;
			yield return null;
		}
		while (!voice.duckEnded)
		{
			yield return null;
		}
		this.SetClickStatusTo(true);
		if (base.enabled)
		{
			InventoryControl.self.showInventory();
		}
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000073A8 File Offset: 0x000055A8
	private IEnumerator DroppedCoroutine(CupInventoryBad_Trophy trophy)
	{
		this.SetClickStatusTo(false);
		InventoryControl.self.hideInventory();
		AudioVoice_CupInventoryBad voice = this.GetPuzzleStats().GetComponent<AudioVoice_CupInventoryBad>();
		voice.pickCupDrop();
		while (!voice.dropCupNow)
		{
			yield return null;
		}
		Audio.self.playOneShot("8fee2f6e-bdbd-4e1b-b013-754e93c5988f", 1f);
		trophy.Discarded();
		Rigidbody2D trophyBody = trophy.GetComponent<Rigidbody2D>();
		trophyBody.bodyType = RigidbodyType2D.Dynamic;
		trophyBody.simulated = true;
		int sign = ((double)UnityEngine.Random.value >= 0.5) ? -1 : 1;
		trophyBody.velocity = Vector2.ClampMagnitude(trophyBody.velocity, this.maxDropVelocity);
		trophyBody.AddTorque(this.dropTorque * (float)sign);
		this.SetClickStatusTo(true);
		UnityEngine.Object.Destroy(trophy.gameObject, 20f);
		if (base.enabled)
		{
			InventoryControl.self.showInventory();
		}
		yield break;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000073CC File Offset: 0x000055CC
	private IEnumerator DirtyCoroutine(CupInventoryBad_Trophy trophy)
	{
		this.SetClickStatusTo(false);
		InventoryControl.self.hideInventory();
		trophy.transform.Find("dirt").gameObject.SetActive(true);
		AudioVoice_CupInventoryBad voice = this.GetPuzzleStats().GetComponent<AudioVoice_CupInventoryBad>();
		voice.pickCupDirty();
		while (!voice.dirtyCupNow)
		{
			yield return null;
		}
		Audio.self.playOneShot("5f149889-02cd-4ab5-bd8d-098e212bc183", 1f);
		trophy.Discarded();
		float timer = 0f;
		float endTime = this.dirtyCupMovement.keys[this.dirtyCupMovement.length - 1].time;
		Vector2 startPosition = trophy.transform.localPosition;
		while (timer < endTime)
		{
			timer = Mathf.MoveTowards(timer, endTime, Time.deltaTime);
			trophy.transform.localPosition = startPosition + Vector2.up * this.dirtyCupMovement.Evaluate(timer);
			yield return null;
		}
		UnityEngine.Object.Destroy(trophy.gameObject);
		this.SetClickStatusTo(true);
		if (base.enabled)
		{
			InventoryControl.self.showInventory();
		}
		yield break;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000073F0 File Offset: 0x000055F0
	private IEnumerator AddingToInventoryCoroutine()
	{
		Global.self.canBePaused = false;
		InventoryControl.self.removeInventory();
		AudioVoice_CupInventoryBad sc = this.GetPuzzleStats().GetComponent<AudioVoice_CupInventoryBad>();
		while (!sc.showCups)
		{
			yield return null;
		}
		yield return new WaitForSeconds(this.cupAppearanceTime);
		InventoryControl.self.showInventory();
		this.potato.GetComponent<InventoryItem>().moveBackToInventory();
		yield return new WaitForSeconds(this.cupAppearanceTime);
		this.trophies = this.GetComponentsInPuzzleStats(false);
		foreach (CupInventory_InventoryItem trophy in this.trophies)
		{
			trophy.moveBackToInventory();
			yield return new WaitForSeconds(this.cupAppearanceTime);
		}
		foreach (CupInventory_InventoryItem cupInventory_InventoryItem in this.trophies)
		{
			cupInventory_InventoryItem.lockMouse = false;
		}
		this.potato.GetComponent<CupInventory_InventoryItem>().lockMouse = false;
		Global.self.canBePaused = true;
		yield break;
	}

	// Token: 0x04000108 RID: 264
	public CupInventoryBad_Potato potato;

	// Token: 0x04000109 RID: 265
	public float cupAppearanceTime = 0.33f;

	// Token: 0x0400010A RID: 266
	private CupInventory_InventoryItem[] trophies;

	// Token: 0x0400010B RID: 267
	[Header("Duck")]
	public Transform duck;

	// Token: 0x0400010C RID: 268
	public float waitBeforeDuck = 2f;

	// Token: 0x0400010D RID: 269
	public float duckStartOffset;

	// Token: 0x0400010E RID: 270
	public float duckEndOffset;

	// Token: 0x0400010F RID: 271
	public float duckMoveTime;

	// Token: 0x04000110 RID: 272
	public Transform blackScreen;

	// Token: 0x04000111 RID: 273
	public float blackScreenTime;

	// Token: 0x04000112 RID: 274
	public float blackScreenLerpTime;

	// Token: 0x04000113 RID: 275
	private Coroutine duckCoroutine;

	// Token: 0x04000114 RID: 276
	[Header("Dropping cup")]
	public float waitBeforeDropping = 1f;

	// Token: 0x04000115 RID: 277
	public float maxDropVelocity = 100f;

	// Token: 0x04000116 RID: 278
	public float dropTorque = 30f;

	// Token: 0x04000117 RID: 279
	[Header("Dirty cup")]
	public AnimationCurve dirtyCupMovement;

	// Token: 0x04000118 RID: 280
	private int potatoGrabs;

	// Token: 0x04000119 RID: 281
	private List<CupInventoryBad_Controller.Effects> availableEffects = new List<CupInventoryBad_Controller.Effects>
	{
		CupInventoryBad_Controller.Effects.Dropped,
		CupInventoryBad_Controller.Effects.Dirty,
		CupInventoryBad_Controller.Effects.BelongsToDuck
	};

	// Token: 0x02000018 RID: 24
	private enum Effects
	{
		// Token: 0x0400011B RID: 283
		Unknown,
		// Token: 0x0400011C RID: 284
		Dropped,
		// Token: 0x0400011D RID: 285
		Dirty,
		// Token: 0x0400011E RID: 286
		BelongsToDuck
	}
}
