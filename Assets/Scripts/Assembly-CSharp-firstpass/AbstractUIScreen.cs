using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000511 RID: 1297
public abstract class AbstractUIScreen : MonoBehaviour
{
	// Token: 0x06001DD0 RID: 7632 RVA: 0x000863BA File Offset: 0x000847BA
	public virtual void Update()
	{
		if (Input.GetButtonDown("Cancel") && this.active)
		{
			this.cancelPressed();
		}
	}

	// Token: 0x06001DD1 RID: 7633
	protected abstract void cancelPressed();

	// Token: 0x06001DD2 RID: 7634
	public abstract void setScreen(Transform item);

	// Token: 0x06001DD3 RID: 7635 RVA: 0x000863DC File Offset: 0x000847DC
	public virtual void setActive(bool active)
	{
		this.active = active;
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x000863E5 File Offset: 0x000847E5
	public virtual void removeScreen()
	{
		base.enabled = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x000863FC File Offset: 0x000847FC
	protected IEnumerator showHintIcon(RectTransform hintText, Action callback)
	{
		hintText.anchoredPosition = Vector2.zero + Vector2.right * hintText.sizeDelta.x;
		hintText.gameObject.SetActive(true);
		Vector2 maxPos = Vector2.zero + Vector2.left * 6f;
		bool allowed = false;
		while (hintText.anchoredPosition.x > maxPos.x)
		{
			hintText.anchoredPosition = Vector2.Lerp(hintText.anchoredPosition, maxPos, Time.deltaTime * 10f);
			hintText.anchoredPosition = Vector2.MoveTowards(hintText.anchoredPosition, maxPos, Time.deltaTime * 10f);
			if (!allowed && hintText.anchoredPosition.x < hintText.sizeDelta.x * 0.5f)
			{
				if (callback != null)
				{
					callback();
				}
				allowed = true;
			}
			yield return null;
		}
		while (hintText.anchoredPosition.x < 0f)
		{
			hintText.anchoredPosition = Vector2.Lerp(hintText.anchoredPosition, Vector2.zero, Time.deltaTime * 10f);
			hintText.anchoredPosition = Vector2.MoveTowards(hintText.anchoredPosition, Vector2.zero, Time.deltaTime * 10f);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04002125 RID: 8485
	protected bool active;
}
