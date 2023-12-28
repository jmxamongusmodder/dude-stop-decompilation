using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200044E RID: 1102
public class PuzzleSitInEmptyBus_Scumbag : Draggable
{
	// Token: 0x06001C39 RID: 7225 RVA: 0x00076C6D File Offset: 0x0007506D
	private void Awake()
	{
		this.particles.SetEmissionRate(0f);
		base.StartCoroutine(this.recordSpeed());
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x00076C8C File Offset: 0x0007508C
	private void Update()
	{
		if (this.dragged)
		{
			return;
		}
		if (this.freeSpot || this.creepSpot)
		{
			bool monster = false;
			if (this.freeSpot)
			{
				monster = false;
			}
			else if (this.creepSpot)
			{
				monster = true;
			}
			Audio.self.playOneShot("e345c0af-7d6c-4a37-89aa-5c4f59b4d983", 1f);
			this.bus.GetComponent<PuzzleSitInEmptyBus_Bus>().DriveAway(monster);
			base.transform.SetParent(this.bus);
			base.enabled = false;
		}
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x00076D1C File Offset: 0x0007511C
	private IEnumerator recordSpeed()
	{
		Audio.self.playLoopSound("a20c93c1-d796-428c-884e-61dc7240d1b3");
		Vector2 lastPos = base.transform.position;
		for (;;)
		{
			if (!base.enabled)
			{
				yield return null;
			}
			this.list.Enqueue(Vector2.SqrMagnitude(lastPos - base.transform.position));
			lastPos = base.transform.position;
			if ((float)this.list.Count > 10f)
			{
				this.list.Dequeue();
				if (this.list.Average() > 10f)
				{
					this.particles.SetEmissionRate(Mathf.Clamp(this.particles.GetEmissionRate(), 0f, 10f) + 0.1f);
				}
				else
				{
					this.particles.SetEmissionRate(0f);
				}
			}
			yield return new WaitForSeconds(this.recordTime / 10f);
		}
		yield break;
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x00076D37 File Offset: 0x00075137
	private void OnEnable()
	{
		base.GetComponent<Collider2D>().enabled = true;
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x00076D45 File Offset: 0x00075145
	private void OnDisable()
	{
		base.GetComponent<Collider2D>().enabled = false;
		if (this.dragged)
		{
			Audio.self.stopLoopSound("a20c93c1-d796-428c-884e-61dc7240d1b3", true);
		}
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x00076D6E File Offset: 0x0007516E
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.dragged)
		{
			Audio.self.playLoopSound("a20c93c1-d796-428c-884e-61dc7240d1b3");
		}
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x00076D90 File Offset: 0x00075190
	public override void OnMouseUp()
	{
		if (this.dragged)
		{
			Audio.self.stopLoopSound("a20c93c1-d796-428c-884e-61dc7240d1b3", true);
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x00076DC3 File Offset: 0x000751C3
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.creepSpot = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.freeSpot = true;
		}
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x00076E02 File Offset: 0x00075202
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.creepSpot = false;
		}
		else if (other.tag == "FailCollider")
		{
			this.freeSpot = false;
		}
	}

	// Token: 0x04001A90 RID: 6800
	public Transform bus;

	// Token: 0x04001A91 RID: 6801
	public bool freeSpot;

	// Token: 0x04001A92 RID: 6802
	public bool creepSpot;

	// Token: 0x04001A93 RID: 6803
	public ParticleSystem particles;

	// Token: 0x04001A94 RID: 6804
	private Queue<float> list = new Queue<float>();

	// Token: 0x04001A95 RID: 6805
	public float recordTime = 1f;
}
