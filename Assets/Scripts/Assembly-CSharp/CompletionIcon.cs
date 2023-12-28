using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000520 RID: 1312
public class CompletionIcon : MonoBehaviour
{
	// Token: 0x06001E10 RID: 7696 RVA: 0x00087824 File Offset: 0x00085C24
	private void Start()
	{
		this.bg.gameObject.SetActive(true);
		this.win.gameObject.SetActive(false);
		this.lose.gameObject.SetActive(false);
		this.currState = CompletionState.None;
		this.finalState = this.currState;
		this.curr = this.bg;
		this.banner.gameObject.SetActive(true);
		this.banner.anchoredPosition = new Vector2(this.banner.anchoredPosition.x, this.bannerYLimit.x);
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x000878C2 File Offset: 0x00085CC2
	private void Update()
	{
		this.playAnimation();
		this.animateBanner();
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x000878D0 File Offset: 0x00085CD0
	public void hideBanner()
	{
		this.bannerShowTime = 0f;
		this.bannerShow = false;
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x000878E4 File Offset: 0x00085CE4
	public void resetBanner()
	{
		this.bannerShowTime = 0f;
		this.bannerShow = false;
		this.bannerAnimTime = 0f;
		Vector2 anchoredPosition = this.banner.anchoredPosition;
		anchoredPosition.y = this.bannerYLimit.x;
		this.banner.anchoredPosition = anchoredPosition;
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x00087938 File Offset: 0x00085D38
	private void animateBanner()
	{
		if (this.bannerShowTime > 0f)
		{
			this.bannerShowTime -= Time.deltaTime;
			if (this.bannerShowTime < 0f)
			{
				this.bannerShow = false;
			}
		}
		if (this.bannerShow)
		{
			if (this.bannerAnimTime == this.bannerAnimTimeMax)
			{
				return;
			}
			this.bannerAnimTime = Mathf.MoveTowards(this.bannerAnimTime, this.bannerAnimTimeMax, Time.deltaTime);
		}
		else
		{
			if (this.bannerAnimTime == 0f)
			{
				return;
			}
			this.bannerAnimTime = Mathf.MoveTowards(this.bannerAnimTime, 0f, Time.deltaTime);
		}
		float num = this.bannerYCurve.Evaluate(this.bannerAnimTime / this.bannerAnimTimeMax);
		Vector2 anchoredPosition = this.banner.anchoredPosition;
		anchoredPosition.y = this.bannerYLimit.x - (this.bannerYLimit.x - this.bannerYLimit.y) * num;
		this.banner.anchoredPosition = anchoredPosition;
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x00087A48 File Offset: 0x00085E48
	private void playAnimation()
	{
		if (!this.swapAnimation)
		{
			return;
		}
		Vector3 v = this.next.anchoredPosition;
		v.x = Mathf.Lerp(v.x, 0f, Time.deltaTime * this.speed);
		v.x = Mathf.MoveTowards(v.x, 0f, Time.deltaTime * 4f);
		this.next.anchoredPosition = v;
		this.curr.anchoredPosition = Vector3.right * (this.next.anchoredPosition.x - this.next.sizeDelta.x);
		Image component = this.next.GetComponent<Image>();
		Color color = component.color;
		float num = Mathf.Clamp(1f - v.x / 15f, 0f, 1f);
		color.a = num;
		component.color = color;
		component = this.curr.GetComponent<Image>();
		color = component.color;
		color.a = 1f - num;
		component.color = color;
		if (Mathf.Abs(v.x) < 0.01f)
		{
			this.curr.gameObject.SetActive(false);
			this.curr = this.next;
			this.swapAnimation = false;
			if (this.currState != this.finalState)
			{
				this.setCompletionState(this.finalState);
			}
		}
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x00087BD6 File Offset: 0x00085FD6
	public CompletionState getCompletionState()
	{
		return this.finalState;
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x00087BE0 File Offset: 0x00085FE0
	public void setCompletionState(CompletionState newState)
	{
		this.finalState = newState;
		if (this.currState != newState)
		{
			this.bannerShowTime = this.bannerShowTimeMax;
			this.bannerShow = true;
			Vector2 v = this.particles.transform.position;
			v.x = base.transform.position.x;
			this.particles.transform.position = v;
			this.particles.Play();
		}
		if (this.currState == newState || this.swapAnimation)
		{
			return;
		}
		if (newState != CompletionState.None)
		{
			if (newState != CompletionState.Monster)
			{
				if (newState == CompletionState.Good)
				{
					this.next = this.lose;
					Audio.self.playOneShot("b4fa93b0-92b0-4d07-8409-0066ec4be5b6", 1f);
				}
			}
			else
			{
				this.next = this.win;
				Audio.self.playOneShot("7a647a17-9c6b-41b6-a305-27afe1c1ec73", 1f);
			}
		}
		else
		{
			this.next = this.bg;
		}
		this.currState = newState;
		this.swapAnimation = true;
		this.next.anchoredPosition = Vector3.right * (this.next.sizeDelta.x + this.curr.anchoredPosition.x);
		this.next.gameObject.SetActive(true);
		Image component = this.next.GetComponent<Image>();
		Color color = component.color;
		color.a = 0f;
		component.color = color;
	}

	// Token: 0x0400214F RID: 8527
	[Tooltip("Default icon")]
	public RectTransform bg;

	// Token: 0x04002150 RID: 8528
	[Tooltip("Winning icon")]
	public RectTransform win;

	// Token: 0x04002151 RID: 8529
	[Tooltip("Loosing icon")]
	public RectTransform lose;

	// Token: 0x04002152 RID: 8530
	private CompletionState currState;

	// Token: 0x04002153 RID: 8531
	private CompletionState finalState;

	// Token: 0x04002154 RID: 8532
	private bool swapAnimation;

	// Token: 0x04002155 RID: 8533
	private RectTransform curr;

	// Token: 0x04002156 RID: 8534
	private RectTransform next;

	// Token: 0x04002157 RID: 8535
	[Tooltip("How fast to transition between icons")]
	public float speed = 5f;

	// Token: 0x04002158 RID: 8536
	[Header("Banner")]
	[Tooltip("Banner icon")]
	public RectTransform banner;

	// Token: 0x04002159 RID: 8537
	[Tooltip("Particles behind banner")]
	public ParticleSystem particles;

	// Token: 0x0400215A RID: 8538
	[Tooltip("From where to where move banner")]
	public Vector2 bannerYLimit;

	// Token: 0x0400215B RID: 8539
	[Tooltip("How to move banner")]
	public AnimationCurve bannerYCurve;

	// Token: 0x0400215C RID: 8540
	[Tooltip("How long to wait before hiding banner")]
	public float bannerShowTimeMax = 2f;

	// Token: 0x0400215D RID: 8541
	private float bannerShowTime;

	// Token: 0x0400215E RID: 8542
	[Tooltip("How long to show/hide banner")]
	public float bannerAnimTimeMax = 0.5f;

	// Token: 0x0400215F RID: 8543
	private float bannerAnimTime;

	// Token: 0x04002160 RID: 8544
	private bool bannerShow;
}
