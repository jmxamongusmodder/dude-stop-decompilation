using System;
using UnityEngine;

// Token: 0x02000575 RID: 1397
public class packSelectIconAnimation : MonoBehaviour
{
	// Token: 0x06002020 RID: 8224 RVA: 0x0009CCFD File Offset: 0x0009B0FD
	private void Start()
	{
		this.rt = base.GetComponent<RectTransform>();
		this.cg = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x0009CD18 File Offset: 0x0009B118
	private void Update()
	{
		if (this.delay > 0f)
		{
			this.delay -= Time.deltaTime;
			return;
		}
		Vector2 anchoredPosition = this.rt.anchoredPosition;
		if (anchoredPosition.x >= 0f)
		{
			this.speedCurr = -this.speedMax;
		}
		if (this.remove)
		{
			this.speedCurr = -this.speedMax;
			anchoredPosition.x += this.speedCurr * Time.deltaTime * this.removeSpeed;
			this.cg.alpha -= Time.deltaTime * this.alphaSpeed;
			if (this.cg.alpha < 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		else
		{
			anchoredPosition.x += this.speedCurr * Time.deltaTime;
			this.speedCurr += this.speedAcc * Time.deltaTime;
		}
		this.rt.anchoredPosition = anchoredPosition;
	}

	// Token: 0x04002360 RID: 9056
	[Tooltip("Wait this much before move")]
	public float delay = 0.5f;

	// Token: 0x04002361 RID: 9057
	[Tooltip("Fastest speed that this object can achieve")]
	public float speedMax = 1f;

	// Token: 0x04002362 RID: 9058
	[Tooltip("Change current speed this much all the time")]
	public float speedAcc = 0.1f;

	// Token: 0x04002363 RID: 9059
	[Header("Remove")]
	[Tooltip("How fast to change alpha on remove")]
	public float alphaSpeed = 2f;

	// Token: 0x04002364 RID: 9060
	[Tooltip("How fast to move on remove")]
	public float removeSpeed = 5f;

	// Token: 0x04002365 RID: 9061
	private float speedCurr;

	// Token: 0x04002366 RID: 9062
	public bool remove;

	// Token: 0x04002367 RID: 9063
	private RectTransform rt;

	// Token: 0x04002368 RID: 9064
	private CanvasGroup cg;
}
