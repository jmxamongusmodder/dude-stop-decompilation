using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class PuzzleCup : MonoBehaviour
{
	// Token: 0x06001432 RID: 5170 RVA: 0x00034088 File Offset: 0x00032488
	public virtual void SetCup(Banner banner)
	{
		Rigidbody2D component = base.GetComponent<Rigidbody2D>();
		if (component != null)
		{
			component.bodyType = RigidbodyType2D.Kinematic;
			component.velocity = Vector2.zero;
		}
		if (this.turnOffColliders)
		{
			PolygonCollider2D[] components = base.GetComponents<PolygonCollider2D>();
			foreach (PolygonCollider2D polygonCollider2D in components)
			{
				polygonCollider2D.enabled = false;
			}
		}
		int num = banner.cupLayer;
		SpriteRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<SpriteRenderer>(true);
		foreach (SpriteRenderer spriteRenderer in componentsInChildren)
		{
			spriteRenderer.sortingLayerName = "Front";
			spriteRenderer.sortingOrder += banner.cupLayer;
			num = Mathf.Min(num, spriteRenderer.sortingOrder);
		}
		ParticleSystemRenderer component2 = this.stars.GetComponent<ParticleSystemRenderer>();
		component2.sortingLayerName = "Front";
		component2.sortingOrder = num - 1;
	}

	// Token: 0x06001433 RID: 5171 RVA: 0x00034180 File Offset: 0x00032580
	public bool ShowStars()
	{
		if (this.particlesPlayed)
		{
			return false;
		}
		this.stars.gameObject.SetActive(true);
		this.particlesPlayed = true;
		this.PlayStarSound();
		return true;
	}

	// Token: 0x06001434 RID: 5172 RVA: 0x000341AE File Offset: 0x000325AE
	protected virtual void PlayStarSound()
	{
		Audio.self.playOneShot("9ee7acd2-c13f-4587-94f6-be36fae7004e", 1f);
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000341C5 File Offset: 0x000325C5
	private void OnValidate()
	{
		if (this.stars == null && base.transform.childCount >= 1)
		{
			this.stars = base.transform.GetChild(0).GetComponent<ParticleSystem>();
		}
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x00034200 File Offset: 0x00032600
	[ContextMenu("AddShadow")]
	private void AddShadow()
	{
		if (this.spritesForShadows == null)
		{
			return;
		}
		foreach (SpriteRenderer spriteRenderer in this.spritesForShadows)
		{
			Transform transform = new GameObject("Mask").transform;
			transform.SetParent(spriteRenderer.transform, false);
			transform.localPosition = this.SHADOW_SHIFT;
			transform.rotation = spriteRenderer.transform.rotation;
			SpriteMask spriteMask = transform.gameObject.AddComponent<SpriteMask>();
			spriteMask.isCustomRangeActive = true;
			spriteMask.sprite = spriteRenderer.sprite;
			spriteMask.frontSortingLayerID = SortingLayer.NameToID("Front");
			spriteMask.frontSortingOrder = 106;
			spriteMask.backSortingLayerID = SortingLayer.NameToID("Front");
			spriteMask.backSortingOrder = 104;
		}
	}

	// Token: 0x04001160 RID: 4448
	[Tooltip("TRUE - this is a BAD cup, show bad animations")]
	public bool monsterCup;

	// Token: 0x04001161 RID: 4449
	[Space(10f)]
	[Tooltip("Scale cup this much after cup is accuired")]
	public float maxScale = 3f;

	// Token: 0x04001162 RID: 4450
	[Tooltip("Star particle to show behind cup after scaling")]
	public ParticleSystem stars;

	// Token: 0x04001163 RID: 4451
	private bool particlesPlayed;

	// Token: 0x04001164 RID: 4452
	[Space(10f)]
	[Tooltip("Turn of colliders on this object before animation?")]
	public bool turnOffColliders = true;

	// Token: 0x04001165 RID: 4453
	[Header("Banner")]
	public Color bannerColor;

	// Token: 0x04001166 RID: 4454
	public SpriteRenderer[] spritesForShadows;

	// Token: 0x04001167 RID: 4455
	private Vector2 SHADOW_SHIFT = new Vector2(0.084f, -0.084f);
}
