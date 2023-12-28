using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class CupLineShader : MonoBehaviour
{
	// Token: 0x06001467 RID: 5223 RVA: 0x00033C6C File Offset: 0x0003206C
	public virtual void Awake()
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		component.material.SetFloat("_Left", component.sprite.textureRect.min.x);
		component.material.SetFloat("_Bottom", component.sprite.textureRect.min.y);
		component.material.SetFloat("_Right", component.sprite.textureRect.max.x);
		component.material.SetFloat("_Top", component.sprite.textureRect.max.y);
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x00033D31 File Offset: 0x00032131
	public virtual void OnValidate()
	{
		this.Awake();
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x00033D39 File Offset: 0x00032139
	[ContextMenu("Refresh")]
	private void Refresh()
	{
		this.Awake();
	}
}
