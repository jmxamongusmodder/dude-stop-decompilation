using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class BackgroundControl : MonoBehaviour
{
	// Token: 0x06001449 RID: 5193 RVA: 0x00034878 File Offset: 0x00032C78
	private void Start()
	{
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x0003487A File Offset: 0x00032C7A
	private void Update()
	{
		if (Global.self.transitionManualSpeed <= 0f)
		{
			return;
		}
		this.playAnimation();
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x00034898 File Offset: 0x00032C98
	private void playAnimation()
	{
		if (!this.animInProgress)
		{
			return;
		}
		this.hideColorSpeedCur += this.hideColorSpeed;
		this.hideAnimation();
		if (this.delayShowMaxCur > 0f)
		{
			this.delayShowMaxCur -= Time.deltaTime * Global.self.transitionManualSpeed;
		}
		else
		{
			this.animInProgress = this.showAnimation();
		}
		if (this.bgColorDelayCur > 0f)
		{
			this.bgColorDelayCur -= Time.deltaTime * Global.self.transitionManualSpeed;
		}
		else
		{
			this.curBGScript.defaultColor = Color.Lerp(this.curBGScript.defaultColor, this.nextBGScript.defaultColor, Time.deltaTime * Global.self.transitionManualSpeed * this.colorLerpSpeed * 0.5f);
			this.backgroundColor.color = this.curBGScript.defaultColor;
		}
		if (!this.animInProgress)
		{
			this.endAnimation();
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x000349A4 File Offset: 0x00032DA4
	private void endAnimation()
	{
		this.animInProgress = false;
		UnityEngine.Object.Destroy(this.curBG.gameObject);
		this.curBG = this.nextBG;
		IEnumerator enumerator = this.curBG.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				SpriteRenderer component = transform.GetComponent<SpriteRenderer>();
				component.sortingOrder -= this.shiftLayerOrder;
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

	// Token: 0x0600144D RID: 5197 RVA: 0x00034A40 File Offset: 0x00032E40
	private bool showAnimation()
	{
		bool result = false;
		IEnumerator enumerator = this.nextBG.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				SpriteRenderer component = transform.GetComponent<SpriteRenderer>();
				BackgroundLines component2 = transform.GetComponent<BackgroundLines>();
				if (component2.delayShow >= 0f)
				{
					component2.delayShow -= Time.deltaTime * Global.self.transitionManualSpeed;
					if (component2.delayShow < 0f)
					{
						if (component2.customStartingColor)
						{
							component.color = component2.startingColor;
						}
						else
						{
							component.color = Color.Lerp(this.curBGScript.defaultColor, component2.color, 0.3f);
						}
					}
					result = true;
				}
				else
				{
					transform.localScale = Vector3.Lerp(transform.localScale, component2.scale, Time.deltaTime * Global.self.transitionManualSpeed * this.scaleSpeed);
					component.color = Color.Lerp(component.color, component2.color, Time.deltaTime * Global.self.transitionManualSpeed * this.colorLerpSpeed);
					if (transform.localScale.y + 0.01f < component2.scale.y)
					{
						result = true;
					}
				}
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
		return result;
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x00034BCC File Offset: 0x00032FCC
	private void hideAnimation()
	{
		IEnumerator enumerator = this.curBG.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				SpriteRenderer component = transform.GetComponent<SpriteRenderer>();
				BackgroundLines component2 = transform.GetComponent<BackgroundLines>();
				if (component2.delayHide >= 0f)
				{
					component2.delayHide -= Time.deltaTime * Global.self.transitionManualSpeed;
					component.color = Color.Lerp(component.color, this.curBGScript.defaultColor, Time.deltaTime * Global.self.transitionManualSpeed * this.colorLerpSpeed * this.hideColorSpeedCur * 0.5f);
				}
				else
				{
					Vector2 v = transform.localScale;
					v.y = Mathf.Lerp(v.y, 0f, Time.deltaTime * Global.self.transitionManualSpeed * this.scaleSpeed);
					transform.localScale = v;
					component.color = Color.Lerp(component.color, this.curBGScript.defaultColor, Time.deltaTime * Global.self.transitionManualSpeed * this.colorLerpSpeed * this.hideColorSpeedCur);
				}
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

	// Token: 0x0600144F RID: 5199 RVA: 0x00034D3C File Offset: 0x0003313C
	public void startBGSwap(Transform newBG)
	{
		if (newBG == null)
		{
			Debug.LogError("Can't find BG transform for the nextPuzzle");
			return;
		}
		if (this.animInProgress)
		{
			this.endAnimation();
		}
		Transform transform = this.cloneBG(newBG);
		newBG.gameObject.SetActive(false);
		this.nextBG = transform;
		this.animInProgress = true;
		this.bgColorDelayCur = this.bgColorDelayMax;
		this.delayShowMaxCur = this.delayShowMax;
		this.hideColorSpeedCur = 0f;
		this.curBGScript = this.curBG.GetComponent<Background>();
		this.nextBGScript = this.nextBG.GetComponent<Background>();
		IEnumerator enumerator = this.nextBG.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform2 = (Transform)obj;
				SpriteRenderer component = transform2.GetComponent<SpriteRenderer>();
				component.sortingOrder += this.shiftLayerOrder;
				transform2.localScale = Vector3.Scale(new Vector3(1f, 0f, 1f), transform2.localScale);
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
		this.nextBG.gameObject.SetActive(true);
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x00034E7C File Offset: 0x0003327C
	public void swapInstantly(Transform newBG)
	{
		if (newBG == null)
		{
			Debug.LogError("Can't find BG transform for the nextPuzzle");
			return;
		}
		UnityEngine.Object.Destroy(this.curBG.gameObject);
		this.curBG = this.cloneBG(newBG);
		this.backgroundColor.color = this.curBG.GetComponent<Background>().defaultColor;
	}

	// Token: 0x06001451 RID: 5201 RVA: 0x00034ED8 File Offset: 0x000332D8
	private Transform cloneBG(Transform newBG)
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(newBG);
		transform.gameObject.SetActive(true);
		transform.SetParent(base.transform);
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.forward * this.backgroundPosZ;
		return transform;
	}

	// Token: 0x0400117C RID: 4476
	[Header("Objects")]
	[Tooltip("Sprite on the background")]
	public SpriteRenderer backgroundColor;

	// Token: 0x0400117D RID: 4477
	[Tooltip("Set first background to appear on the stage")]
	public Transform curBG;

	// Token: 0x0400117E RID: 4478
	private Transform nextBG;

	// Token: 0x0400117F RID: 4479
	private Background curBGScript;

	// Token: 0x04001180 RID: 4480
	private Background nextBGScript;

	// Token: 0x04001181 RID: 4481
	[Header("Basic parameters")]
	[Tooltip("Where to place all the lines")]
	public int layerOrderMin = -70;

	// Token: 0x04001182 RID: 4482
	[Tooltip("Move fresh lines this much closer to camera, to overlap old ones")]
	public int shiftLayerOrder = 20;

	// Token: 0x04001183 RID: 4483
	[Tooltip("Where to place lines from the camera")]
	public float backgroundPosZ = 29f;

	// Token: 0x04001184 RID: 4484
	[Header("Animation parameters")]
	[Tooltip("Wait between each layer this much, then show next layer")]
	public float timeBetweenLayers = 0.25f;

	// Token: 0x04001185 RID: 4485
	private bool animInProgress;

	// Token: 0x04001186 RID: 4486
	[Tooltip("How fast to lerp color on lines/background")]
	public float colorLerpSpeed = 3.5f;

	// Token: 0x04001187 RID: 4487
	[Tooltip("How fast to scale Y scale on lines")]
	public float scaleSpeed = 3.5f;

	// Token: 0x04001188 RID: 4488
	[Tooltip("Change background color after this long. Wait till hide animation is done, basically")]
	public float bgColorDelayMax = 0.4f;

	// Token: 0x04001189 RID: 4489
	private float bgColorDelayCur;

	// Token: 0x0400118A RID: 4490
	[Tooltip("Start show animation after this long. Wait till hide animation is done, basically")]
	public float delayShowMax = 0.8f;

	// Token: 0x0400118B RID: 4491
	private float delayShowMaxCur;

	// Token: 0x0400118C RID: 4492
	[Tooltip("How fast to lerp color on hide animation")]
	public float hideColorSpeed = 0.1f;

	// Token: 0x0400118D RID: 4493
	private float hideColorSpeedCur;
}
