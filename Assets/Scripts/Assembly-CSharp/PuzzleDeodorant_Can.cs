using System;
using UnityEngine;

// Token: 0x020003F0 RID: 1008
public class PuzzleDeodorant_Can : Draggable
{
	// Token: 0x06001971 RID: 6513 RVA: 0x0005EF94 File Offset: 0x0005D394
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		Color color = Gizmos.color;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(base.transform.position + new Vector3(0f, 0.3f), base.transform.position + new Vector3(0f, 0.3f) - this.distance * base.transform.right * Mathf.Sign(base.transform.localScale.x));
		Gizmos.color = color;
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x0005F038 File Offset: 0x0005D438
	private void Update()
	{
		this.CheckMovement();
		this.CheckRotation();
		this.CheckSpraySeconds();
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x0005F04C File Offset: 0x0005D44C
	private void OnDisable()
	{
		this.particles.EnableEmmision(false);
		if (this.dragged)
		{
			this.dragged = false;
		}
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x0005F06C File Offset: 0x0005D46C
	private void CheckMovement()
	{
		if (this.dragged || !base.WasMoved())
		{
			return;
		}
		base.transform.position = Vector3.Lerp(base.transform.position, this.startingPosition, Time.deltaTime * this.lerpSpeed);
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.startingPosition, Time.deltaTime * this.moveSpeed);
		if (this.layersChanged && (!base.WasMoved() || Vector2.Distance(base.transform.position, this.startingPosition) < 0.1f))
		{
			foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.sortingOrder -= 15;
			}
			this.layersChanged = false;
		}
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x0005F160 File Offset: 0x0005D560
	private void CheckRotation()
	{
		if (!this.dragged || !this.enableTracking)
		{
			return;
		}
		float f = base.transform.position.x - this.human.position.x;
		if (Mathf.Sign(f) != Mathf.Sign(base.transform.localScale.x))
		{
			Vector2 v = base.transform.localScale;
			this.particles.transform.rotation = Quaternion.Euler(0f, 0f, (this.particles.transform.eulerAngles.z + 180f) % 360f);
			v.x *= -1f;
			base.transform.localScale = v;
		}
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x0005F24C File Offset: 0x0005D64C
	private void CheckSpraySeconds()
	{
		if (this.dragged && this.Raycast())
		{
			this.seconds += Time.deltaTime;
		}
		if (this.seconds > this.secondsForBad && !this.isBad)
		{
			this.isBad = true;
			this.human.GetComponent<PuzzleDeodorant_Human>().BadHuman();
			this.OnMouseUp();
			base.GetComponent<Collider2D>().enabled = false;
			Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().sprayGuyBad();
		}
		else if (this.seconds > this.secondsForStench)
		{
			this.stenchParticles.SetEmissionRate(Mathf.Lerp(this.minStench, this.maxStench, (this.seconds - this.secondsForStench) / (this.secondsForBad - this.secondsForStench)));
			this.stenchParticles.EnableEmmision(true);
		}
		else if (this.seconds > this.secondsForNormal && !this.isGood)
		{
			this.GetComponentInPuzzleStats<PuzzleDeodorant_Door>().Open();
			Audio.self.playOneShot("3a3b8df2-6182-464b-b312-5755cdd54d5a", 1f);
			this.isGood = true;
			this.human.GetComponent<PuzzleDeodorant_Human>().GoodHuman();
			this.OnMouseUp();
			Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().sprayGuyGood();
		}
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x0005F3A8 File Offset: 0x0005D7A8
	public override void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		base.OnMouseDown();
		this.particles.EnableEmmision(true);
		if (!this.layersChanged)
		{
			foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.sortingOrder += 15;
			}
			this.layersChanged = true;
		}
		if (this.dragged)
		{
			Audio.self.playLoopSound("8e6db0d9-dda2-44da-a139-5caa44efc439");
			Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().startSpray();
		}
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x0005F444 File Offset: 0x0005D844
	public override void OnMouseUp()
	{
		if (this.dragged)
		{
			Audio.self.stopLoopSound("8e6db0d9-dda2-44da-a139-5caa44efc439", true);
			Global.self.currPuzzle.GetComponent<AudioVoice_Deodorant>().stopSpray();
		}
		base.OnMouseUp();
		this.particles.EnableEmmision(false);
		if (this.layersChanged && !base.WasMoved())
		{
			foreach (SpriteRenderer spriteRenderer in base.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.sortingOrder -= 15;
			}
			this.layersChanged = false;
		}
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x0005F4E0 File Offset: 0x0005D8E0
	private bool Raycast()
	{
		int layerMask = 1 << LayerMask.NameToLayer("Individual");
		RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position + new Vector3(0f, 0.3f), base.transform.right * -Mathf.Sign(base.transform.localScale.x), this.distance, layerMask);
		return raycastHit2D.collider != null && raycastHit2D.transform.tag == "SuccessCollider";
	}

	// Token: 0x04001774 RID: 6004
	[Header("Human stuff")]
	public float distance;

	// Token: 0x04001775 RID: 6005
	public float secondsForNormal;

	// Token: 0x04001776 RID: 6006
	public float secondsForStench;

	// Token: 0x04001777 RID: 6007
	public float secondsForBad;

	// Token: 0x04001778 RID: 6008
	public ParticleSystem stenchParticles;

	// Token: 0x04001779 RID: 6009
	public float minStench;

	// Token: 0x0400177A RID: 6010
	public float maxStench;

	// Token: 0x0400177B RID: 6011
	private float seconds;

	// Token: 0x0400177C RID: 6012
	private bool isGood;

	// Token: 0x0400177D RID: 6013
	private bool isBad;

	// Token: 0x0400177E RID: 6014
	public Transform human;

	// Token: 0x0400177F RID: 6015
	public ParticleSystem particles;

	// Token: 0x04001780 RID: 6016
	[HideInInspector]
	public bool enableTracking;

	// Token: 0x04001781 RID: 6017
	[Header("Movement stuff")]
	public float lerpSpeed;

	// Token: 0x04001782 RID: 6018
	public float moveSpeed;

	// Token: 0x04001783 RID: 6019
	private bool layersChanged;
}
