using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class CupSpoiler_CurtainController : MonoBehaviour
{
	// Token: 0x06001565 RID: 5477 RVA: 0x0003F498 File Offset: 0x0003D898
	private void Awake()
	{
		this.back.gameObject.SetActive(false);
		this.left.gameObject.SetActive(false);
		this.right.gameObject.SetActive(false);
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		Global.self.canBePaused = false;
		base.StartCoroutine(this.CurtainCoroutine());
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x0003F500 File Offset: 0x0003D900
	private void Start()
	{
		this.nextStep = true;
		Audio.self.playOneShot("4710301f-9b92-44a7-99da-f174d958cc37", 1f);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x0003F520 File Offset: 0x0003D920
	private IEnumerator CurtainCoroutine()
	{
		while (!this.nextStep)
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.MoveHorizontalCurtainDownwardsCoroutine());
		yield return new WaitForSeconds(this.timeAfterFirstCurtain);
		this.cup.gameObject.SetActive(true);
		while (this.iterations < 4)
		{
			yield return base.StartCoroutine(this.WaitForNextStep());
			if (this.iterations % 2 == 0)
			{
				yield return base.StartCoroutine(this.HorizontalCurtainCoroutine());
			}
			else
			{
				yield return base.StartCoroutine(this.VerticalCurtainCoroutine());
			}
			this.StopColorCoroutines();
			this.iterations++;
		}
		yield return new WaitForSeconds(this.timeBeforeFirstLegenDary);
		yield return base.StartCoroutine(this.WaitForNextStep());
		yield return base.StartCoroutine(this.DoubleHorizontalCurtainCoroutine());
		this.StopColorCoroutines();
		yield return new WaitForSeconds(this.timeBeforeSecondLegenDary);
		yield return base.StartCoroutine(this.CloseHorizontalCurtainCoroutine());
		this.StopColorCoroutines();
		yield return new WaitForSeconds(this.timeAfterLegenDary);
		yield return base.StartCoroutine(this.MoveHorizontalCurtainUpwardsCoroutine());
		this.StopColorCoroutines();
		yield return new WaitForSeconds(this.timeBeforeLast);
		yield return base.StartCoroutine(this.HorizontalCurtainCoroutine());
		yield return new WaitForSeconds(this.timeBeforeLastCurve);
		yield return base.StartCoroutine(this.LastVerticalCurtainCoroutine());
		UnityEngine.Object.Destroy(this.currentLeft.gameObject);
		UnityEngine.Object.Destroy(this.currentRight.gameObject);
		UnityEngine.Object.Destroy(this.currentBack.gameObject);
		yield break;
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x0003F53C File Offset: 0x0003D93C
	private Transform InitCurtain(Transform source)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(source.gameObject);
		gameObject.transform.SetParent(base.transform);
		gameObject.gameObject.SetActive(true);
		return gameObject.transform;
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x0003F578 File Offset: 0x0003D978
	private void InitHorizontalCurtains()
	{
		if (this.currentLeft == null)
		{
			this.currentLeft = this.InitCurtain(this.left);
		}
		else
		{
			this.currentLeft.position = this.left.position;
			this.currentLeft.position += Vector3.up * (float)(this.iterations + 1) * this.elevation;
		}
		if (this.currentRight == null)
		{
			this.currentRight = this.InitCurtain(this.right);
		}
		else
		{
			this.currentRight.position = this.right.position;
			this.currentRight.position += Vector3.up * (float)(this.iterations + 1) * this.elevation;
		}
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x0003F66C File Offset: 0x0003DA6C
	private void InitVerticalCurtains()
	{
		if (this.currentBack == null)
		{
			this.currentBack = this.InitCurtain(this.back);
		}
		else
		{
			this.currentBack.position = this.back.position;
			this.currentBack.position += Vector3.up * (float)this.iterations * this.elevation;
		}
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x0003F6EC File Offset: 0x0003DAEC
	private IEnumerator HorizontalCurtainCoroutine()
	{
		Vector2 screen = Camera.main.ViewportToWorldPoint(Vector2.one);
		screen /= 2f;
		float leftEnd = -screen.x - this.curtainWidth;
		float rightEnd = screen.x + this.curtainWidth;
		this.InitVerticalCurtains();
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.currentBack, this.colorChangeWaitTop)));
		this.SetSpriteSortingLayer(this.currentBack, "Default");
		this.SetSpriteSortingLayer(this.currentLeft, "Top");
		this.SetSpriteSortingLayer(this.currentRight, "Top");
		float timer = 0f;
		float startLeft = this.currentLeft.position.x;
		float startRight = this.currentRight.position.x;
		while (timer != this.horizontalCurveTime)
		{
			timer = Mathf.MoveTowards(timer, this.horizontalCurveTime, Time.deltaTime);
			this.currentLeft.SetX(Mathf.Lerp(startLeft, leftEnd, this.horizontalMovement.Evaluate(timer / this.horizontalCurveTime)));
			this.currentRight.SetX(Mathf.Lerp(startRight, rightEnd, this.horizontalMovement.Evaluate(timer / this.horizontalCurveTime)));
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x0003F708 File Offset: 0x0003DB08
	private IEnumerator VerticalCurtainCoroutine()
	{
		Vector2 topEnd = new Vector2(this.currentBack.position.x, this.topEndPos);
		this.InitHorizontalCurtains();
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.currentLeft, this.colorChangeWaitSides)));
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.currentRight, this.colorChangeWaitSides)));
		this.SetSpriteSortingLayer(this.currentBack, "Top");
		this.SetSpriteSortingLayer(this.currentLeft, "Default");
		this.SetSpriteSortingLayer(this.currentRight, "Default");
		float timer = 0f;
		float startTop = this.currentBack.position.y;
		while (timer != this.verticalCurveTime)
		{
			timer = Mathf.MoveTowards(timer, this.verticalCurveTime, Time.deltaTime);
			this.currentBack.SetY(Mathf.Lerp(startTop, topEnd.y, this.verticalMovement.Evaluate(timer / this.verticalCurveTime)), false);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x0003F724 File Offset: 0x0003DB24
	private IEnumerator DoubleHorizontalCurtainCoroutine()
	{
		Vector2 screen = Camera.main.ViewportToWorldPoint(Vector2.one);
		screen /= 2f;
		float leftEnd = -screen.x - this.curtainWidth;
		float rightEnd = screen.x + this.curtainWidth;
		this.secondLeft = this.InitCurtain(this.left);
		this.secondRight = this.InitCurtain(this.right);
		this.secondLeft.position += Vector3.up * (float)(this.iterations + 1) * this.elevation;
		this.secondRight.position += Vector3.up * (float)(this.iterations + 1) * this.elevation;
		this.SetSpriteSortingLayer(this.secondLeft, "Default");
		this.SetSpriteSortingLayer(this.secondRight, "Default");
		this.SetSpriteSortingLayer(this.currentLeft, "Top");
		this.SetSpriteSortingLayer(this.currentRight, "Top");
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.secondLeft, this.colorChangeWaitTop)));
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.secondRight, this.colorChangeWaitTop)));
		float timer = 0f;
		float timerMax = this.firstLegenDaryCurve.GetAnimationLength();
		float startLeft = this.currentLeft.position.x;
		float startRight = this.currentRight.position.x;
		while (timer < timerMax)
		{
			timer = Mathf.MoveTowards(timer, timerMax, Time.deltaTime);
			this.currentLeft.SetX(Mathf.Lerp(startLeft, leftEnd, this.firstLegenDaryCurve.Evaluate(timer)));
			this.currentRight.SetX(Mathf.Lerp(startRight, rightEnd, this.firstLegenDaryCurve.Evaluate(timer)));
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x0003F740 File Offset: 0x0003DB40
	private IEnumerator CloseHorizontalCurtainCoroutine()
	{
		Vector2 leftEnd = new Vector2(this.left.position.x, this.currentLeft.position.y);
		Vector2 rightEnd = new Vector2(this.right.position.x, this.currentRight.position.y);
		this.coroutines.Add(base.StartCoroutine(this.ReversedSpriteColorValueChangingCoroutine(this.secondLeft)));
		this.coroutines.Add(base.StartCoroutine(this.ReversedSpriteColorValueChangingCoroutine(this.secondRight)));
		float timer = 0f;
		float timerMax = this.secondLegenDaryCurve.GetAnimationLength();
		float startLeft = this.currentLeft.position.x;
		float startRight = this.currentRight.position.x;
		while (timer != timerMax)
		{
			timer = Mathf.MoveTowards(timer, timerMax, Time.deltaTime);
			this.currentLeft.SetX(Mathf.Lerp(startLeft, leftEnd.x, this.secondLegenDaryCurve.Evaluate(timer)));
			this.currentRight.SetX(Mathf.Lerp(startRight, rightEnd.x, this.secondLegenDaryCurve.Evaluate(timer)));
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x0003F75C File Offset: 0x0003DB5C
	private IEnumerator MoveHorizontalCurtainUpwardsCoroutine()
	{
		List<SpriteRenderer> sprites = this.AssembleSprites(new Transform[]
		{
			this.secondLeft,
			this.secondRight
		});
		Vector2 leftEnd = new Vector2(this.currentLeft.position.x, this.topEndPos);
		Vector2 rightEnd = new Vector2(this.currentRight.position.x, this.topEndPos);
		this.secondLeft.position = this.currentLeft.position;
		this.secondRight.position = this.currentRight.position;
		this.currentLeft.position += Vector3.up * this.elevation;
		this.currentRight.position += Vector3.up * this.elevation;
		this.iterations++;
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.currentLeft, this.colorChangeWaitTop)));
		this.coroutines.Add(base.StartCoroutine(this.SpriteColorValueChangingCoroutine(this.currentRight, this.colorChangeWaitTop)));
		this.SetSpriteSortingLayer(this.secondLeft, "Top");
		this.SetSpriteSortingLayer(this.secondRight, "Top");
		this.SetSpriteSortingLayer(this.currentLeft, "Default");
		this.SetSpriteSortingLayer(this.currentRight, "Default");
		this.ResetSpriteColor(sprites);
		float timer = 0f;
		float startLeft = this.secondLeft.position.y;
		float startRight = this.secondRight.position.y;
		while (timer != this.verticalCurveTime)
		{
			timer = Mathf.MoveTowards(timer, this.verticalCurveTime, Time.deltaTime);
			this.secondLeft.SetY(Mathf.Lerp(startLeft, leftEnd.y, this.verticalMovement.Evaluate(timer / this.verticalCurveTime)), false);
			this.secondRight.SetY(Mathf.Lerp(startRight, rightEnd.y, this.verticalMovement.Evaluate(timer / this.verticalCurveTime)), false);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x0003F778 File Offset: 0x0003DB78
	private IEnumerator MoveHorizontalCurtainDownwardsCoroutine()
	{
		this.InitHorizontalCurtains();
		Vector2 leftEnd = this.currentLeft.position;
		Vector2 rightEnd = this.currentRight.position;
		this.currentLeft.position = new Vector2(this.currentLeft.position.x, this.topEndPos);
		this.currentRight.position = new Vector2(this.currentRight.position.x, this.topEndPos);
		float timer = 0f;
		float startLeft = this.currentLeft.position.y;
		float startRight = this.currentRight.position.y;
		while (timer != this.verticalCurveTime)
		{
			timer = Mathf.MoveTowards(timer, this.verticalCurveTime, Time.deltaTime);
			this.currentLeft.SetY(Mathf.Lerp(startLeft, leftEnd.y, this.verticalMovement.Evaluate(timer / this.verticalCurveTime)), false);
			this.currentRight.SetY(Mathf.Lerp(startRight, rightEnd.y, this.verticalMovement.Evaluate(timer / this.verticalCurveTime)), false);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x0003F794 File Offset: 0x0003DB94
	private IEnumerator LastVerticalCurtainCoroutine()
	{
		float lastTime = this.lastCurve.keys[this.lastCurve.length - 1].time;
		float timer = 0f;
		while (timer < lastTime)
		{
			timer = Mathf.MoveTowards(timer, lastTime, Time.deltaTime);
			this.currentBack.position = new Vector2(this.currentBack.position.x, this.lastCurve.Evaluate(timer));
			yield return null;
		}
		this.cup.GetComponent<Collider2D>().enabled = true;
		this.confetty.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x0003F7B0 File Offset: 0x0003DBB0
	private IEnumerator SpriteColorValueChangingCoroutine(Transform t, float wait)
	{
		Dictionary<SpriteRenderer, ColorExtension.ColorHSV> sprites = new Dictionary<SpriteRenderer, ColorExtension.ColorHSV>();
		foreach (SpriteRenderer spriteRenderer in t.GetComponentsInChildren<SpriteRenderer>())
		{
			sprites.Add(spriteRenderer, ColorExtension.RGBToHSV(spriteRenderer.color));
			sprites[spriteRenderer].value = this.minimalColorValue;
			spriteRenderer.color = ColorExtension.HSVToRGB(sprites[spriteRenderer]);
		}
		yield return new WaitForSeconds(wait);
		bool finished = false;
		while (!finished)
		{
			finished = true;
			foreach (KeyValuePair<SpriteRenderer, ColorExtension.ColorHSV> keyValuePair in sprites)
			{
				keyValuePair.Value.value = Mathf.MoveTowards(keyValuePair.Value.value, 1f, this.colorChangeSpeed * Time.deltaTime);
				keyValuePair.Key.color = keyValuePair.Value.ToRGB();
				finished &= (Mathf.Abs(keyValuePair.Value.value - 1f) < Mathf.Epsilon);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x0003F7DC File Offset: 0x0003DBDC
	private IEnumerator ReversedSpriteColorValueChangingCoroutine(Transform t)
	{
		yield return new WaitForSeconds(this.reverseColorWait);
		Dictionary<SpriteRenderer, ColorExtension.ColorHSV> sprites = new Dictionary<SpriteRenderer, ColorExtension.ColorHSV>();
		foreach (SpriteRenderer spriteRenderer in t.GetComponentsInChildren<SpriteRenderer>())
		{
			sprites.Add(spriteRenderer, ColorExtension.RGBToHSV(spriteRenderer.color));
		}
		bool finished = false;
		while (!finished)
		{
			finished = true;
			foreach (KeyValuePair<SpriteRenderer, ColorExtension.ColorHSV> keyValuePair in sprites)
			{
				keyValuePair.Value.value = Mathf.MoveTowards(keyValuePair.Value.value, this.minimalColorValue, this.colorChangeSpeed * Time.deltaTime);
				keyValuePair.Key.color = keyValuePair.Value.ToRGB();
				float num = Mathf.Abs(keyValuePair.Value.value - this.minimalColorValue);
				finished &= (num < Mathf.Epsilon);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x0003F800 File Offset: 0x0003DC00
	private void ResetSpriteColor(List<SpriteRenderer> sprites)
	{
		foreach (SpriteRenderer spriteRenderer in sprites)
		{
			ColorExtension.ColorHSV colorHSV = ColorExtension.RGBToHSV(spriteRenderer.color);
			colorHSV.value = 1f;
			spriteRenderer.color = colorHSV.ToRGB();
		}
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x0003F878 File Offset: 0x0003DC78
	private void SetSpriteSortingLayer(Transform t, string sortingLayerName)
	{
		foreach (SpriteRenderer spriteRenderer in t.GetComponentsInChildren<SpriteRenderer>())
		{
			spriteRenderer.sortingLayerName = sortingLayerName;
		}
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x0003F8AC File Offset: 0x0003DCAC
	private List<SpriteRenderer> AssembleSprites(params Transform[] transforms)
	{
		List<SpriteRenderer> list = new List<SpriteRenderer>();
		foreach (Transform transform in transforms)
		{
			list.AddRange(transform.GetComponentsInChildren<SpriteRenderer>());
		}
		return list;
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x0003F8E8 File Offset: 0x0003DCE8
	private bool CheckSpriteVisibility(List<SpriteRenderer> sprites)
	{
		bool flag = false;
		foreach (SpriteRenderer spriteRenderer in sprites)
		{
			flag |= GeometryUtility.TestPlanesAABB(this.planes, spriteRenderer.bounds);
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x0003F95C File Offset: 0x0003DD5C
	private void StopColorCoroutines()
	{
		foreach (Coroutine coroutine in this.coroutines)
		{
			if (coroutine != null)
			{
				base.StopCoroutine(coroutine);
			}
		}
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x0003F9C0 File Offset: 0x0003DDC0
	private IEnumerator WaitForNextStep()
	{
		yield return new WaitForSeconds(this.timeBetweenCurtains);
		yield break;
	}

	// Token: 0x04001310 RID: 4880
	public Transform cup;

	// Token: 0x04001311 RID: 4881
	public Transform confetty;

	// Token: 0x04001312 RID: 4882
	[Header("Curtains")]
	public Transform back;

	// Token: 0x04001313 RID: 4883
	public Transform left;

	// Token: 0x04001314 RID: 4884
	public Transform right;

	// Token: 0x04001315 RID: 4885
	[Header("Captain curve!")]
	public AnimationCurve horizontalMovement;

	// Token: 0x04001316 RID: 4886
	public float horizontalCurveTime;

	// Token: 0x04001317 RID: 4887
	public AnimationCurve verticalMovement;

	// Token: 0x04001318 RID: 4888
	public float verticalCurveTime;

	// Token: 0x04001319 RID: 4889
	public float curtainWidth;

	// Token: 0x0400131A RID: 4890
	public AnimationCurve firstLegenDaryCurve;

	// Token: 0x0400131B RID: 4891
	public AnimationCurve secondLegenDaryCurve;

	// Token: 0x0400131C RID: 4892
	[Header("Curtain movement")]
	public float timeAfterFirstCurtain = 0.5f;

	// Token: 0x0400131D RID: 4893
	public float timeBetweenCurtains = 0.2f;

	// Token: 0x0400131E RID: 4894
	public float timeBeforeFirstLegenDary = 1f;

	// Token: 0x0400131F RID: 4895
	public float timeBeforeSecondLegenDary = 3f;

	// Token: 0x04001320 RID: 4896
	public float timeAfterLegenDary = 0.5f;

	// Token: 0x04001321 RID: 4897
	public float timeBeforeLast = 0.5f;

	// Token: 0x04001322 RID: 4898
	public float topEndPos = 9.5f;

	// Token: 0x04001323 RID: 4899
	public AnimationCurve lastCurve;

	// Token: 0x04001324 RID: 4900
	public float timeBeforeLastCurve = 0.5f;

	// Token: 0x04001325 RID: 4901
	[Header("Height")]
	public float elevation = 0.7f;

	// Token: 0x04001326 RID: 4902
	private int iterations;

	// Token: 0x04001327 RID: 4903
	[Header("Colors")]
	public float colorChangeWaitSides = 0.2f;

	// Token: 0x04001328 RID: 4904
	public float colorChangeWaitTop = 0.4f;

	// Token: 0x04001329 RID: 4905
	public float colorChangeSpeed = 0.1f;

	// Token: 0x0400132A RID: 4906
	public float minimalColorValue = 0.3f;

	// Token: 0x0400132B RID: 4907
	public float reverseColorWait = 1f;

	// Token: 0x0400132C RID: 4908
	[HideInInspector]
	private bool nextStep;

	// Token: 0x0400132D RID: 4909
	private Plane[] planes;

	// Token: 0x0400132E RID: 4910
	private Transform currentLeft;

	// Token: 0x0400132F RID: 4911
	private Transform currentRight;

	// Token: 0x04001330 RID: 4912
	private Transform currentBack;

	// Token: 0x04001331 RID: 4913
	private Transform secondLeft;

	// Token: 0x04001332 RID: 4914
	private Transform secondRight;

	// Token: 0x04001333 RID: 4915
	private List<Coroutine> coroutines = new List<Coroutine>();
}
