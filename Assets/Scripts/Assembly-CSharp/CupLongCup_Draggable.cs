using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class CupLongCup_Draggable : Draggable
{
	// Token: 0x06001531 RID: 5425 RVA: 0x0003D778 File Offset: 0x0003BB78
	private void Awake()
	{
		this.renderers = base.GetComponentsInChildren<SpriteRenderer>(true);
		Vector2 vector = Camera.main.WorldToViewportPoint(this.letter.position);
		foreach (SpriteRenderer spriteRenderer in this.renderers)
		{
			spriteRenderer.material.SetFloat("_Top", vector.y);
			spriteRenderer.material.SetFloat("_Left", 0f);
			spriteRenderer.material.SetFloat("_Angle", 0f);
			spriteRenderer.material.SetFloat("_Distance", 0f);
		}
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x0003D822 File Offset: 0x0003BC22
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		base.StartCoroutine(this.MovingOutCoroutine());
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x0003D841 File Offset: 0x0003BC41
	private void Update()
	{
		if (this.removedFromLetter)
		{
			this.CheckBounds();
		}
		this.PlayDraggingSound();
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x0003D85C File Offset: 0x0003BC5C
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "SuccessCollider")
		{
			return;
		}
		foreach (SpriteRenderer spriteRenderer in this.renderers)
		{
			spriteRenderer.material.SetFloat("_Top", 0f);
			spriteRenderer.sortingLayerName = "Top";
		}
		this.removedFromLetter = true;
		this.limit.bottomVal = this.bottomLimit;
		this.limit.bottomScreen = true;
		this.lockX = false;
		base.GetComponent<Collider2D>().enabled = false;
		Audio.self.playOneShot("c612d4a2-197a-484f-9e96-b08732adb6ad", 1f);
		this.particles.Emit();
		if (this.nextItem != null)
		{
			this.nextItem.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x0003D938 File Offset: 0x0003BD38
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (Camera.main.GetMousePosition().y < this.letter.position.y)
		{
			return;
		}
		this.prevYPos = base.transform.position.y;
		this.soundPlayed = false;
		base.OnMouseDown();
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x0003D9A2 File Offset: 0x0003BDA2
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (this.removedFromLetter && base.GetComponent<Rigidbody2D>().isKinematic)
		{
			base.GetComponent<Rigidbody2D>().isKinematic = false;
			base.GetComponent<Collider2D>().enabled = false;
		}
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x0003D9E0 File Offset: 0x0003BDE0
	private void CheckBounds()
	{
		foreach (SpriteRenderer spriteRenderer in this.renderers)
		{
			if (GeometryUtility.TestPlanesAABB(this.planes, spriteRenderer.bounds))
			{
				return;
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0003DA30 File Offset: 0x0003BE30
	private void PlayDraggingSound()
	{
		if (!this.dragged || this.removedFromLetter)
		{
			return;
		}
		if (!this.takeOut && base.transform.position.y - this.limit.bottomVal > 4f)
		{
			this.GetPuzzleStats().GetComponent<AudioVoice_LongCup>().takeOut();
			this.takeOut = true;
		}
		if (!this.soundPlayed && base.transform.position.y != this.prevYPos)
		{
			Audio.self.playOneShot("8903ed23-8402-4ba2-965f-896419200341", 1f);
			this.soundPlayed = true;
		}
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x0003DAE4 File Offset: 0x0003BEE4
	private IEnumerator MovingOutCoroutine()
	{
		AudioVoice_LongCup sc = this.GetPuzzleStats().GetComponent<AudioVoice_LongCup>();
		while (!sc.showNextObj)
		{
			yield return null;
		}
		while (base.transform.position.y != this.visiblePosition)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.localPosition, new Vector3(base.transform.localPosition.x, this.visiblePosition), this.slideOutSpeed * Time.deltaTime);
			yield return null;
		}
		base.GetComponent<Collider2D>().enabled = true;
		this.bottomLimit = this.limit.bottomVal;
		this.limit.bottomVal = base.transform.position.y;
		this.limit.bottomScreen = false;
		yield break;
	}

	// Token: 0x040012BD RID: 4797
	public Transform nextItem;

	// Token: 0x040012BE RID: 4798
	public Transform letter;

	// Token: 0x040012BF RID: 4799
	public float visiblePosition;

	// Token: 0x040012C0 RID: 4800
	public float slideOutSpeed;

	// Token: 0x040012C1 RID: 4801
	public float topPosition;

	// Token: 0x040012C2 RID: 4802
	private SpriteRenderer[] renderers;

	// Token: 0x040012C3 RID: 4803
	private Plane[] planes;

	// Token: 0x040012C4 RID: 4804
	private float bottomLimit;

	// Token: 0x040012C5 RID: 4805
	protected bool removedFromLetter;

	// Token: 0x040012C6 RID: 4806
	public ParticleSystem particles;

	// Token: 0x040012C7 RID: 4807
	private bool takeOut;

	// Token: 0x040012C8 RID: 4808
	private bool soundPlayed;

	// Token: 0x040012C9 RID: 4809
	private float prevYPos;
}
