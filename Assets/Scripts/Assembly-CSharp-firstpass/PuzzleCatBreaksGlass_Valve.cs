using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Token: 0x020003D9 RID: 985
public class PuzzleCatBreaksGlass_Valve : MonoBehaviour
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060018BB RID: 6331 RVA: 0x0005836C File Offset: 0x0005676C
	// (set) Token: 0x060018BC RID: 6332 RVA: 0x00058374 File Offset: 0x00056774
	private float currentPosition
	{
		get
		{
			return this._currentPosition;
		}
		set
		{
			Audio.self.playLoopSound("0cca6798-c242-4f26-8a51-9ad57bc4e249", "Waterflow", value);
			this.water.localScale = new Vector2(this.scaleCurve.Evaluate(value), this.water.localScale.y);
			this.turnedOff = (value == 0f);
			this._currentPosition = value;
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060018BD RID: 6333 RVA: 0x000583DF File Offset: 0x000567DF
	// (set) Token: 0x060018BE RID: 6334 RVA: 0x000583E8 File Offset: 0x000567E8
	private bool turnedOff
	{
		get
		{
			return this._turnedOff;
		}
		set
		{
			if (value && !this._turnedOff)
			{
				this.cat.WaterIsOff();
				foreach (ParticleSystem particleSystem in this.water.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.EnableEmmision(false);
				}
			}
			else if (this._turnedOff && !value)
			{
				this.cat.WaterIsOn();
				foreach (ParticleSystem particleSystem2 in this.water.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem2.EnableEmmision(true);
				}
			}
			this._turnedOff = value;
		}
	}

	// Token: 0x060018BF RID: 6335 RVA: 0x00058497 File Offset: 0x00056897
	private void Start()
	{
		if (!this.loopStarted)
		{
			this.loopStarted = true;
			base.StartCoroutine(this.lates());
		}
		this.cat = this.GetComponentInPuzzleStats<PuzzleCatBreaksGlass_Cat>();
	}

	// Token: 0x060018C0 RID: 6336 RVA: 0x000584C4 File Offset: 0x000568C4
	private IEnumerator lates()
	{
		Audio.self.playLoopSound("0cca6798-c242-4f26-8a51-9ad57bc4e249", "Waterflow", 0.95f);
		yield return new WaitForSeconds(0.5f);
		Audio.self.playLoopSound("0cca6798-c242-4f26-8a51-9ad57bc4e249", "Waterflow", 1f);
		yield break;
	}

	// Token: 0x060018C1 RID: 6337 RVA: 0x000584D8 File Offset: 0x000568D8
	private void Update()
	{
		this.Raycast();
	}

	// Token: 0x060018C2 RID: 6338 RVA: 0x000584E0 File Offset: 0x000568E0
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.StartCoroutine(this.TurningCoroutine());
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x000584FC File Offset: 0x000568FC
	private void Raycast()
	{
		if (this.turnedOff)
		{
			return;
		}
		Vector2 vector = this.water.position + new Vector3(0.11f * this.water.localScale.x, 0.3f);
		Vector2 vector2 = this.water.position - new Vector3(0.11f * this.water.localScale.x, -0.3f);
		Debug.DrawRay(vector, this.water.transform.up, Color.black);
		Debug.DrawRay(vector2, this.water.transform.up, Color.black);
		RaycastHit2D[] source = Physics2D.RaycastAll(vector, base.transform.up, 1f);
		if ((from x in source
		where x.transform == this.cat.transform
		select x).Count<RaycastHit2D>() > 0)
		{
			this.PlayCatAnimation();
			return;
		}
		source = Physics2D.RaycastAll(vector2, base.transform.up, 1f);
		if ((from x in source
		where x.transform == this.cat.transform
		select x).Count<RaycastHit2D>() > 0)
		{
			this.PlayCatAnimation();
			return;
		}
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x0005864B File Offset: 0x00056A4B
	private void PlayCatAnimation()
	{
		if (this.justTurnedOn)
		{
			this.justTurnedOn = false;
			this.cat.WaterJump();
		}
		else
		{
			this.cat.PushBack();
		}
		this.particles.Emit();
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x00058688 File Offset: 0x00056A88
	private IEnumerator TurningCoroutine()
	{
		Audio.self.playOneShot("7870e75f-c97c-4e6c-b978-7b7ad07b80a1", 1f);
		float timer = 0f;
		Coroutine valveCoroutine = base.StartCoroutine(this.ValveTurningCoroutine());
		if (this.turnedOff)
		{
			this.justTurnedOn = true;
			while (timer < this.timePerClick)
			{
				timer = Mathf.MoveTowards(timer, this.timePerClick, Time.deltaTime);
				this.currentPosition = timer / this.timePerClick;
				yield return null;
			}
		}
		else
		{
			float endPosition = Mathf.Clamp(this.currentPosition - 1f / (float)this.clicksToOff, 0f, 1f);
			while (timer < this.timePerClick)
			{
				timer = Mathf.MoveTowards(timer, this.timePerClick, Time.deltaTime);
				this.currentPosition = Mathf.Lerp(this.currentPosition, endPosition, timer / this.timePerClick);
				yield return null;
			}
			if (this.currentPosition < 0.001f)
			{
				this.currentPosition = 0f;
			}
		}
		base.StopCoroutine(valveCoroutine);
		yield break;
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x000586A4 File Offset: 0x00056AA4
	private IEnumerator ValveTurningCoroutine()
	{
		float timer = 0f;
		float target = this.timePerValveSprite;
		GameObject valveSprite = base.transform.GetChild(1).gameObject;
		for (;;)
		{
			timer = Mathf.MoveTowards(timer, target, Time.deltaTime);
			if (timer == target)
			{
				valveSprite.SetActive(!valveSprite.activeSelf);
				target = this.timePerValveSprite - target;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x040016AA RID: 5802
	public Transform water;

	// Token: 0x040016AB RID: 5803
	public AnimationCurve scaleCurve;

	// Token: 0x040016AC RID: 5804
	public int clicksToOff;

	// Token: 0x040016AD RID: 5805
	public float timePerClick;

	// Token: 0x040016AE RID: 5806
	public float timePerValveSprite = 0.12f;

	// Token: 0x040016AF RID: 5807
	private float _currentPosition = 1f;

	// Token: 0x040016B0 RID: 5808
	private bool justTurnedOn;

	// Token: 0x040016B1 RID: 5809
	private bool _turnedOff;

	// Token: 0x040016B2 RID: 5810
	public ParticleSystem particles;

	// Token: 0x040016B3 RID: 5811
	private PuzzleCatBreaksGlass_Cat cat;

	// Token: 0x040016B4 RID: 5812
	private bool loopStarted;
}
