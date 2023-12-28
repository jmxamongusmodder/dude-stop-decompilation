using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003E8 RID: 1000
public class PuzzleCinemaPhone_Settings : MonoBehaviour
{
	// Token: 0x06001942 RID: 6466 RVA: 0x0005D73D File Offset: 0x0005BB3D
	private void Update()
	{
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x0005D73F File Offset: 0x0005BB3F
	private void OnMouseDown()
	{
		if (this.activeCoroutine || !base.enabled)
		{
			return;
		}
		base.StartCoroutine(this.SlidingCoroutine());
		Audio.self.playOneShot("3295a9b2-026e-4df1-9613-4589e76a097f", 1f);
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x0005D77A File Offset: 0x0005BB7A
	public void Click()
	{
		this.OnMouseDown();
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x0005D782 File Offset: 0x0005BB82
	public void Disable()
	{
		this.options.gameObject.SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x0005D7A4 File Offset: 0x0005BBA4
	private IEnumerator SlidingCoroutine()
	{
		this.activeCoroutine = true;
		bool slideIn = !this.options.gameObject.activeSelf;
		yield return null;
		float target;
		if (slideIn)
		{
			this.options.gameObject.SetActive(true);
			SpriteRenderer component = this.top.GetComponent<SpriteRenderer>();
			Vector2 vector = Camera.main.WorldToViewportPoint(component.bounds.min + new Vector3(0f, 0.1f));
			foreach (SpriteRenderer spriteRenderer in this.options.GetComponentsInChildren<SpriteRenderer>(true))
			{
				spriteRenderer.material.SetFloat("_Angle", 3.1415927f);
				spriteRenderer.material.SetFloat("_Top", vector.y);
			}
			if (this.startY == -10f)
			{
				this.startY = this.options.localPosition.y;
			}
			target = this.finalY;
		}
		else
		{
			target = this.startY;
		}
		Vector2 start = this.options.localPosition;
		Vector2 end = new Vector2(this.options.localPosition.x, target);
		float timer = 0f;
		this.EnableButtons(false);
		while (timer != this.time)
		{
			timer = Mathf.MoveTowards(timer, this.time, Time.deltaTime);
			float t = Mathf.Sin(timer / this.time * 3.1415927f * 0.5f);
			this.options.localPosition = Vector2.Lerp(start, end, t);
			yield return null;
		}
		if (slideIn)
		{
			this.EnableButtons(true);
		}
		else
		{
			this.options.gameObject.SetActive(false);
		}
		this.activeCoroutine = false;
		yield break;
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x0005D7C0 File Offset: 0x0005BBC0
	private void EnableButtons(bool status)
	{
		foreach (Collider2D collider2D in this.options.GetComponentsInChildren<Collider2D>())
		{
			collider2D.enabled = status;
		}
	}

	// Token: 0x04001743 RID: 5955
	public Transform top;

	// Token: 0x04001744 RID: 5956
	public Transform options;

	// Token: 0x04001745 RID: 5957
	public float finalY;

	// Token: 0x04001746 RID: 5958
	public float time;

	// Token: 0x04001747 RID: 5959
	private float startY = -10f;

	// Token: 0x04001748 RID: 5960
	private bool activeCoroutine;
}
