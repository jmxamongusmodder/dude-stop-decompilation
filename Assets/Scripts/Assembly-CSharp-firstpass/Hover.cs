using System;
using FMODUnity;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class Hover : MonoBehaviour
{
	// Token: 0x06001698 RID: 5784 RVA: 0x00048E84 File Offset: 0x00047284
	public void OnMouseEnter()
	{
		this.entered = true;
		if (!base.enabled || !this.react)
		{
			return;
		}
		Renderer component = base.GetComponent<Renderer>();
		if (component.material.HasProperty("_Alpha"))
		{
			component.material.SetFloat("_Alpha", this.whiten);
		}
		Audio.self.playOneShot(this.mouseOver, 1f);
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x00048EF8 File Offset: 0x000472F8
	public void OnMouseExit()
	{
		this.entered = false;
		Renderer component = base.GetComponent<Renderer>();
		if (component.material.HasProperty("_Alpha"))
		{
			component.material.SetFloat("_Alpha", this.def);
		}
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x00048F3E File Offset: 0x0004733E
	private void OnMouseDown()
	{
		if (!this.disableOnMouseDown)
		{
			return;
		}
		this.dragged = true;
		this.OnMouseExit();
		this.react = false;
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x00048F60 File Offset: 0x00047360
	private void OnMouseUp()
	{
		if (!this.disableOnMouseDown || !this.dragged)
		{
			return;
		}
		this.dragged = false;
		this.react = true;
		if (this.entered)
		{
			this.OnMouseEnter();
		}
	}

	// Token: 0x04001466 RID: 5222
	public float whiten = 0.3f;

	// Token: 0x04001467 RID: 5223
	public float def;

	// Token: 0x04001468 RID: 5224
	public bool disableOnMouseDown;

	// Token: 0x04001469 RID: 5225
	[EventRef]
	public string mouseOver;

	// Token: 0x0400146A RID: 5226
	private bool dragged;

	// Token: 0x0400146B RID: 5227
	private bool entered;

	// Token: 0x0400146C RID: 5228
	private bool react = true;
}
