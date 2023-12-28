using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003BC RID: 956
public class Puzzle6amNoise_Drill : Draggable
{
	// Token: 0x060017C0 RID: 6080 RVA: 0x00050312 File Offset: 0x0004E712
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector3(this.maxX, 6f), new Vector3(this.maxX, -6f));
	}

	// Token: 0x060017C1 RID: 6081 RVA: 0x00050344 File Offset: 0x0004E744
	private void Update()
	{
		if (this.animationPlaying)
		{
			return;
		}
		if (base.WasMoved())
		{
			this.particles.position = new Vector3(this.particles.position.x, base.transform.position.y + this.particleOffset);
			if (base.transform.position.x < this.maxX)
			{
				this.particles.GetComponent<ParticleSystem>().EnableEmmision(true);
				this.dragged = false;
				base.transform.parent.GetComponentInChildren<Puzzle6amNoise_Switch>().enabled = false;
				base.GetComponent<Collider2D>().enabled = false;
				this.dragEnabled = false;
				this.OnMouseUp();
				UnityEngine.Object.Destroy(base.body);
				base.GetComponent<Animator>().enabled = true;
				this.animationPlaying = true;
				if (base.transform.position.y > this.YForSparkls.x && base.transform.position.y < this.YForSparkls.y)
				{
					this.sparklsParticles.transform.position = this.particles.position;
					this.sparklsParticles.SetActive(true);
					base.StartCoroutine(this.flickrLight());
				}
				if (!this.evilTime)
				{
					base.GetComponent<Animator>().SetTrigger("good");
					Audio.self.playOneShot("0415049d-2b23-4522-a101-5fe40e1a0e16", 1f);
				}
				else
				{
					Audio.self.playOneShot("f7c5fd9d-35af-4a69-9c92-6ce8c8caab22", 1f);
				}
			}
		}
	}

	// Token: 0x060017C2 RID: 6082 RVA: 0x000504F0 File Offset: 0x0004E8F0
	private IEnumerator flickrLight()
	{
		yield return new WaitForSeconds(1f);
		float time = 0f;
		Color color = this.nightSprite.color;
		while (Global.self.NoCurrentTransition)
		{
			time -= Time.deltaTime;
			if (time <= 0f)
			{
				time = Extensions.Random(this.flickrEach);
				color.a = this.flickrBlackAmount;
				this.nightSprite.color = color;
				Audio.self.playOneShot("58c2ff8f-712d-423d-87b9-7f7dcdd1d6a5", 1f);
				yield return new WaitForSeconds(Extensions.Random(this.flickrFor));
				color.a = 0f;
				this.nightSprite.color = color;
			}
			yield return null;
		}
		color.a = 0f;
		this.nightSprite.color = color;
		yield break;
	}

	// Token: 0x060017C3 RID: 6083 RVA: 0x0005050C File Offset: 0x0004E90C
	public void endAnimation()
	{
		base.GetComponent<Animator>().enabled = false;
		this.particles.GetComponent<ParticleSystem>().EnableEmmision(false);
		if (!base.enabled)
		{
			return;
		}
		if (this.evilTime)
		{
			Global.LevelCompleted(0f, true);
		}
		else
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x00050568 File Offset: 0x0004E968
	public override void FixedUpdate()
	{
		if (this.dragged || this.animationPlaying)
		{
			this.one.gameObject.SetActive(!this.one.gameObject.activeSelf);
			this.two.gameObject.SetActive(!this.two.gameObject.activeSelf);
		}
		base.FixedUpdate();
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000505D8 File Offset: 0x0004E9D8
	public override void OnMouseDown()
	{
		if (this.animationPlaying || !base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		Audio.self.playLoopSound("cbe6b22e-e2cd-438e-bc05-9301de79edec", "Stop", 0f);
		if (this.evilTime)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_6AmNoise>().onDrillPickUp();
		}
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x0005063A File Offset: 0x0004EA3A
	public override void OnMouseUp()
	{
		if (this.animationPlaying || !base.enabled)
		{
			return;
		}
		base.OnMouseUp();
		Audio.self.playLoopSound("cbe6b22e-e2cd-438e-bc05-9301de79edec", "Stop", 1f);
	}

	// Token: 0x0400158A RID: 5514
	public GameObject sparklsParticles;

	// Token: 0x0400158B RID: 5515
	public Vector2 YForSparkls;

	// Token: 0x0400158C RID: 5516
	[Space(10f)]
	public SpriteRenderer nightSprite;

	// Token: 0x0400158D RID: 5517
	public Vector2 flickrEach;

	// Token: 0x0400158E RID: 5518
	[Range(0f, 1f)]
	public float flickrBlackAmount;

	// Token: 0x0400158F RID: 5519
	public Vector2 flickrFor;

	// Token: 0x04001590 RID: 5520
	[Space(20f)]
	public Transform particles;

	// Token: 0x04001591 RID: 5521
	public float particleOffset = 0.6f;

	// Token: 0x04001592 RID: 5522
	public float maxX;

	// Token: 0x04001593 RID: 5523
	[HideInInspector]
	public bool evilTime;

	// Token: 0x04001594 RID: 5524
	public Transform one;

	// Token: 0x04001595 RID: 5525
	public Transform two;

	// Token: 0x04001596 RID: 5526
	private bool animationPlaying;
}
