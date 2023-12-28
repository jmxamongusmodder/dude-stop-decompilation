using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000455 RID: 1109
public class PuzzleStepOnCracks_Cracks : MonoBehaviour
{
	// Token: 0x06001C74 RID: 7284 RVA: 0x00078F74 File Offset: 0x00077374
	private void Awake()
	{
		this.rend = base.GetComponent<SpriteRenderer>();
		this.rend.material.SetFloat("_Alpha", 1f);
		this.rend.material.SetFloat("_Range", 0f);
		this.UpdatePixelSize();
		this.CheckGlow();
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x00078FCD File Offset: 0x000773CD
	private void Update()
	{
		this.UpdatePixelSize();
		this.CheckGlow();
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x00078FDB File Offset: 0x000773DB
	public void GlowAll()
	{
		if (this.noMoreGlow)
		{
			return;
		}
		this.noMoreGlow = true;
		base.StartCoroutine(this.PermanentGlowingCoroutine());
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x00079000 File Offset: 0x00077400
	public void SetPosition(Vector2 position)
	{
		if (this.noMoreGlow)
		{
			return;
		}
		this.rend.material.SetVector("_Positions", Camera.main.WorldToViewportPoint(position) + this.offset);
	}

	// Token: 0x06001C78 RID: 7288 RVA: 0x00079054 File Offset: 0x00077454
	[ContextMenu("Update Pixel Size")]
	private void UpdatePixelSize()
	{
		Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.one) * 2f;
		Vector2 vector2 = this.rend.sprite.bounds.size;
		Vector2 vector3 = new Vector2(vector2.x / vector.x, vector2.y / vector.y);
		Vector2 vector4 = new Vector2(vector3.x / this.spriteSize.x, vector3.y / this.spriteSize.y);
		this.rend.material.SetFloat("_PixelSizeX", vector4.x * base.transform.localScale.x);
		this.rend.material.SetFloat("_PixelSizeY", vector4.y * base.transform.localScale.y);
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x00079158 File Offset: 0x00077558
	private void CheckGlow()
	{
		if (this.noMoreGlow)
		{
			return;
		}
		if (this.glow && this.currentRange != this.range)
		{
			this.currentRange = Mathf.MoveTowards(this.currentRange, this.range, Time.deltaTime * this.glowChangeSpeed);
		}
		else if (!this.glow && this.range != 0f)
		{
			this.currentRange = Mathf.MoveTowards(this.currentRange, 0f, Time.deltaTime * this.glowChangeSpeed);
		}
		this.rend.material.SetFloat("_Range", this.currentRange);
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x00079210 File Offset: 0x00077610
	private IEnumerator PermanentGlowingCoroutine()
	{
		Audio.self.playOneShot("45f2fcb3-5f7a-4919-b66d-34335bcf7c01", 1f);
		while (this.currentRange != this.maxRange)
		{
			this.currentRange = Mathf.MoveTowards(this.currentRange, this.maxRange, Time.deltaTime * this.maxRangeSpeed);
			this.rend.material.SetFloat("_Range", this.currentRange);
			yield return null;
		}
		Audio.self.playLoopSound("7a330eb7-487f-46e1-9f70-890885d0d889");
		this.currentAlpha = this.rend.material.GetFloat("_Alpha");
		for (;;)
		{
			while (this.currentAlpha != this.softGlowAlpha)
			{
				this.MoveAlphaTo(this.softGlowAlpha, this.softGlowSpeed);
				yield return null;
			}
			while (this.currentAlpha != 1f)
			{
				this.MoveAlphaTo(1f, this.softGlowSpeed);
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06001C7B RID: 7291 RVA: 0x0007922B File Offset: 0x0007762B
	private void MoveAlphaTo(float target, float speed)
	{
		this.currentAlpha = Mathf.MoveTowards(this.currentAlpha, target, Time.deltaTime * speed);
		this.rend.material.SetFloat("_Alpha", this.currentAlpha);
	}

	// Token: 0x04001ADB RID: 6875
	public bool glow;

	// Token: 0x04001ADC RID: 6876
	public float range = 0.08f;

	// Token: 0x04001ADD RID: 6877
	public float glowChangeSpeed;

	// Token: 0x04001ADE RID: 6878
	public Vector2 offset;

	// Token: 0x04001ADF RID: 6879
	public Vector2 spriteSize = new Vector2(106f, 13f);

	// Token: 0x04001AE0 RID: 6880
	[Header("Final Glow")]
	public float softGlowAlpha;

	// Token: 0x04001AE1 RID: 6881
	public float softGlowSpeed;

	// Token: 0x04001AE2 RID: 6882
	public float maxRangeSpeed;

	// Token: 0x04001AE3 RID: 6883
	public float maxRange = 5f;

	// Token: 0x04001AE4 RID: 6884
	private float currentRange;

	// Token: 0x04001AE5 RID: 6885
	private float currentAlpha;

	// Token: 0x04001AE6 RID: 6886
	private bool noMoreGlow;

	// Token: 0x04001AE7 RID: 6887
	private SpriteRenderer rend;
}
