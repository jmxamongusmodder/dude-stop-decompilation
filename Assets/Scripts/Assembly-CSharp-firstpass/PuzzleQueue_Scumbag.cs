using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public class PuzzleQueue_Scumbag : EnhancedDraggable
{
	// Token: 0x06001BCE RID: 7118 RVA: 0x000744C3 File Offset: 0x000728C3
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();
		GizmosExtension.DrawHorizontalLine(this.queueDivider, -10f, 10f);
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x000744E0 File Offset: 0x000728E0
	private void Start()
	{
		if (!this.loopStarted)
		{
			Audio.self.playLoopSound("1bc5e2aa-8937-4de3-916c-0bc464379528");
			this.loopStarted = true;
		}
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x00074504 File Offset: 0x00072904
	private void Update()
	{
		this.UpdateSpriteSortingOrder();
		if (base.transform.position.x < 1f && base.transform.position.y > -2f && !this.crossedCenter)
		{
			this.crossedCenter = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_Queue>().playDrag();
		}
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x00074578 File Offset: 0x00072978
	private void CheckVictoryConditions()
	{
		if (this.success)
		{
			this.endCoroutine = base.StartCoroutine(this.MonsterEndingCoroutine());
		}
		else if (this.fail)
		{
			this.endCoroutine = base.StartCoroutine(this.NormalEndingCoroutine());
		}
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000745C4 File Offset: 0x000729C4
	public override void OnMouseDown()
	{
		base.OnMouseDown();
		if (this.dragged && this.dragEnabled)
		{
			Audio.self.playLoopSound("249afb48-0b4d-496b-8ece-a1acb078ec22");
			this.StopEndingCoroutine();
			Global.self.currPuzzle.GetComponent<AudioVoice_Queue>().onUnitClick();
		}
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x00074618 File Offset: 0x00072A18
	public override void OnMouseUp()
	{
		if (base.enabled && this.dragged && this.dragEnabled)
		{
			Audio.self.stopLoopSound("249afb48-0b4d-496b-8ece-a1acb078ec22", true);
			this.CheckVictoryConditions();
		}
		base.OnMouseUp();
		base.body.velocity = Vector2.zero;
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x00074672 File Offset: 0x00072A72
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.success = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.fail = true;
		}
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000746B1 File Offset: 0x00072AB1
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.success = false;
		}
		else if (other.tag == "FailCollider")
		{
			this.fail = false;
		}
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000746F0 File Offset: 0x00072AF0
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.success = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.fail = true;
		}
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x0007472F File Offset: 0x00072B2F
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag != "Respawn")
		{
			return;
		}
		Audio.self.playOneShot("8217a6d1-a605-4526-a71f-f1e40b524c2a", 1f);
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x00074761 File Offset: 0x00072B61
	private void StopEndingCoroutine()
	{
		if (this.endCoroutine != null)
		{
			base.StopCoroutine(this.endCoroutine);
			this.endCoroutine = null;
		}
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x00074784 File Offset: 0x00072B84
	private void UpdateSpriteSortingOrder()
	{
		if (base.transform.position.y > this.queueDivider && !this.overDivider)
		{
			foreach (SpriteRenderer spriteRenderer in base.transform.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer.sortingOrder -= 10;
			}
			this.overDivider = true;
		}
		else if (base.transform.position.y < this.queueDivider && this.overDivider)
		{
			foreach (SpriteRenderer spriteRenderer2 in base.transform.GetComponentsInChildren<SpriteRenderer>())
			{
				spriteRenderer2.sortingOrder += 10;
			}
			this.overDivider = false;
		}
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x00074868 File Offset: 0x00072C68
	private IEnumerator MonsterEndingCoroutine()
	{
		yield return new WaitForSeconds(this.waitAfterStop);
		if (this.success && Global.self.currPuzzle.GetComponent<AudioVoice_Queue>().trySolveBad())
		{
			Global.LevelCompleted(0f, true);
		}
		yield break;
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x00074884 File Offset: 0x00072C84
	private IEnumerator NormalEndingCoroutine()
	{
		while (base.transform.position != this.failPoint.position)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.failPoint.position, Time.deltaTime * this.movementSpeed);
			yield return null;
		}
		float timer = 0f;
		while (timer < this.waitAfterStop)
		{
			timer = Mathf.MoveTowards(timer, this.waitAfterStop, Time.deltaTime);
			yield return null;
		}
		if (this.fail && Global.self.currPuzzle.GetComponent<AudioVoice_Queue>().trySolveGood())
		{
			Global.LevelFailed(0f, true);
		}
		yield break;
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x0007489F File Offset: 0x00072C9F
	public void forceMoveToBad()
	{
		base.StartCoroutine(this.moveUnitToBad());
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x000748B0 File Offset: 0x00072CB0
	private IEnumerator moveUnitToBad()
	{
		base.GetComponent<CircleCollider2D>().enabled = false;
		GlitchEffectController.self.startGlitch();
		this.duckSprite.gameObject.SetActive(true);
		Audio.self.playOneShot("cdf2bd47-b914-4f88-8070-456a027d1c22", 1f);
		int step_count = 6;
		float max_dist = 8.3f;
		float step_delta = 1f;
		float step_dist = max_dist / (float)step_count;
		for (int i = 0; i < step_count; i++)
		{
			float x = 5f - (UnityEngine.Random.Range(step_dist - step_delta, step_dist + step_delta) + step_dist * (float)i);
			float y = UnityEngine.Random.Range(-0.6f, 1f);
			base.transform.localPosition = new Vector2(x, y);
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.12f, 0.18f));
			this.duckSprite.gameObject.SetActive(!this.duckSprite.gameObject.activeInHierarchy);
			this.duckSprite.position = Extensions.GetRandomPointOnScreen(Vector2.one * 0.8f);
			this.duckSprite.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-60f, 60f));
		}
		base.transform.localPosition = new Vector2(-3.4f, -0.8f);
		base.GetComponent<CircleCollider2D>().enabled = true;
		GlitchEffectController.self.stopGlitch();
		this.duckSprite.gameObject.SetActive(false);
		AudioVoice_Queue voice = Global.self.currPuzzle.GetComponent<AudioVoice_Queue>();
		while (voice.isPlaying())
		{
			yield return null;
		}
		Global.LevelCompleted(0f, true);
		yield break;
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000748CB File Offset: 0x00072CCB
	public void disableUnit()
	{
		this.dragEnabled = false;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000748D4 File Offset: 0x00072CD4
	public void enableUnit()
	{
		this.dragEnabled = true;
	}

	// Token: 0x04001A39 RID: 6713
	public Transform failPoint;

	// Token: 0x04001A3A RID: 6714
	public float movementSpeed;

	// Token: 0x04001A3B RID: 6715
	public float waitAfterStop = 1f;

	// Token: 0x04001A3C RID: 6716
	public float queueDivider;

	// Token: 0x04001A3D RID: 6717
	private bool overDivider;

	// Token: 0x04001A3E RID: 6718
	private bool success;

	// Token: 0x04001A3F RID: 6719
	private bool fail;

	// Token: 0x04001A40 RID: 6720
	private Coroutine endCoroutine;

	// Token: 0x04001A41 RID: 6721
	private bool crossedCenter;

	// Token: 0x04001A42 RID: 6722
	public Transform duckSprite;

	// Token: 0x04001A43 RID: 6723
	private bool loopStarted;
}
