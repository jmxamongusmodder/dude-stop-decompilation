using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public class PuzzleBrowser : MonoBehaviour
{
	// Token: 0x0600187E RID: 6270 RVA: 0x00056494 File Offset: 0x00054894
	private void Update()
	{
		if (base.transform.localScale != Vector3.one)
		{
			if (this.scaleTimer > 0f)
			{
				this.scaleTimer -= Time.deltaTime;
			}
			else
			{
				base.transform.localScale = Vector3.one;
			}
		}
		if (this.clickTimeout < this.clickTimer)
		{
			this.clicks = 0;
		}
		else if (this.clicks == 2)
		{
			if (base.transform.localScale == Vector3.one)
			{
				if (this.fail)
				{
					Global.LevelFailed(0f, true);
				}
				else
				{
					Global.LevelCompleted(0f, true);
				}
			}
		}
		else
		{
			this.clickTimer += Time.deltaTime;
		}
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x00056574 File Offset: 0x00054974
	private void OnMouseDown()
	{
		if (this.clicks == 2)
		{
			return;
		}
		base.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
		this.scaleTimer = this.scaleTimeout;
		this.clicks++;
		this.clickTimer = 0f;
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000565D2 File Offset: 0x000549D2
	private void OnMouseEnter()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", this.whiten);
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x000565FB File Offset: 0x000549FB
	private void OnMouseExit()
	{
		if (!base.enabled)
		{
			return;
		}
		base.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
	}

	// Token: 0x0400166F RID: 5743
	public float whiten = 0.1f;

	// Token: 0x04001670 RID: 5744
	public float scaleTimeout = 0.08f;

	// Token: 0x04001671 RID: 5745
	public float clickTimeout = 0.5f;

	// Token: 0x04001672 RID: 5746
	public bool fail;

	// Token: 0x04001673 RID: 5747
	private int clicks;

	// Token: 0x04001674 RID: 5748
	private float scaleTimer;

	// Token: 0x04001675 RID: 5749
	private float clickTimer;
}
