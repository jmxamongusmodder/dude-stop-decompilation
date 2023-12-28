using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000462 RID: 1122
public class PuzzleUSB_Drive : Draggable, TransitionProcessor
{
	// Token: 0x06001CD8 RID: 7384 RVA: 0x0007CF30 File Offset: 0x0007B330
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(this.warningX + this.outsideOffset, Color.red);
		GizmosExtension.DrawVerticalLine(this.warningX - this.outsideOffset, Color.red);
		GizmosExtension.DrawVerticalLine(base.transform.position.x, Color.black);
	}

	// Token: 0x06001CD9 RID: 7385 RVA: 0x0007CF88 File Offset: 0x0007B388
	private void Update()
	{
		if (this.dragged && base.transform.position.x > this.warningX && !this.removedCorrectly)
		{
			if (this.popup.Next())
			{
				base.StartCoroutine(this.PopupShowingCoroutine());
			}
			else
			{
				this.lastWarning = true;
				this.RemoveLimit();
			}
		}
		if (base.transform.position.x > this.warningX + this.outsideOffset && !this.outside)
		{
			this.outside = true;
			if (!this.removedCorrectly)
			{
				this.particles.Emit(this.particleCount);
				Audio.self.playOneShot("ac87db2c-4a5b-4589-9e55-4533d154eccc", 1f);
			}
			else
			{
				this.limit.leftVal = this.warningX + this.outsideOffset;
				Audio.self.playOneShot("3dbe35c0-a4f2-4870-a37e-42633acf6b18", 1f);
			}
		}
		else if (base.transform.position.x < this.warningX - this.outsideOffset && this.outside)
		{
			Audio.self.playOneShot("1df126f6-50e5-4c33-8e57-30eee1471155", 1f);
			this.outside = false;
		}
		if (!this.dragged && base.WasMoved())
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.startingPosition, Time.deltaTime * this.returnSpeed);
		}
	}

	// Token: 0x06001CDA RID: 7386 RVA: 0x0007D138 File Offset: 0x0007B538
	public override void OnMouseUp()
	{
		base.OnMouseUp();
		if (base.transform.position.x < this.warningX || !base.enabled)
		{
			return;
		}
		if (!this.removedCorrectly && this.lastWarning)
		{
			Global.LevelCompleted(0f, true);
		}
		else if (this.removedCorrectly)
		{
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x06001CDB RID: 7387 RVA: 0x0007D1B4 File Offset: 0x0007B5B4
	public void TransitionUpdate()
	{
		if (this.renderer == null)
		{
			this.renderer = base.GetComponent<SpriteRenderer>();
		}
		Vector3 vector = Camera.main.WorldToViewportPoint(new Vector3(this.shaderLine.position.x, 0f, 0f));
		this.renderer.material.SetFloat("_VerticalLine", vector.x);
	}

	// Token: 0x06001CDC RID: 7388 RVA: 0x0007D227 File Offset: 0x0007B627
	public void RemovedCorrectly()
	{
		this.removedCorrectly = true;
		this.RemoveLimit();
	}

	// Token: 0x06001CDD RID: 7389 RVA: 0x0007D236 File Offset: 0x0007B636
	private void RemoveLimit()
	{
		this.limit.rightScreen = true;
		this.limit.rightVal = 0.9f;
	}

	// Token: 0x06001CDE RID: 7390 RVA: 0x0007D254 File Offset: 0x0007B654
	private IEnumerator PopupShowingCoroutine()
	{
		this.OnMouseUp();
		this.dragEnabled = false;
		yield return new WaitForSeconds(this.waitOnPopup);
		this.dragEnabled = true;
		yield break;
	}

	// Token: 0x04001B69 RID: 7017
	public PuzzleUSB_CrossIcon cross;

	// Token: 0x04001B6A RID: 7018
	public PuzzleUSB_Popup popup;

	// Token: 0x04001B6B RID: 7019
	public Transform shaderLine;

	// Token: 0x04001B6C RID: 7020
	public ParticleSystem particles;

	// Token: 0x04001B6D RID: 7021
	public float returnSpeed = 2f;

	// Token: 0x04001B6E RID: 7022
	public float warningX;

	// Token: 0x04001B6F RID: 7023
	public float outsideOffset = 0.1f;

	// Token: 0x04001B70 RID: 7024
	public float waitOnPopup;

	// Token: 0x04001B71 RID: 7025
	public int particleCount = 50;

	// Token: 0x04001B72 RID: 7026
	private SpriteRenderer renderer;

	// Token: 0x04001B73 RID: 7027
	private bool removedCorrectly;

	// Token: 0x04001B74 RID: 7028
	private bool lastWarning;

	// Token: 0x04001B75 RID: 7029
	private bool outside;
}
