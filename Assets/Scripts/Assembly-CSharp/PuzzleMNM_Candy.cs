using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class PuzzleMNM_Candy : PivotDraggable
{
	// Token: 0x06000126 RID: 294 RVA: 0x0000B0DF File Offset: 0x000092DF
	private void Start()
	{
		this.allCandies = this.GetComponentsInPuzzleStats(false);
		this.broCandies = (from x in this.allCandies
		where x.red == this.red
		select x).ToArray<PuzzleMNM_Candy>();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000B110 File Offset: 0x00009310
	private void Update()
	{
		this.timer = Mathf.MoveTowards(this.timer, 0f, Time.deltaTime);
		this.CheckVictoryConditions();
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000B134 File Offset: 0x00009334
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider" && !this.inGreenBowl)
		{
			this.inRedBowl = true;
			this.timer = this.waitAfterTrigger;
		}
		else if (other.tag == "FailCollider" && !this.inRedBowl)
		{
			this.inGreenBowl = true;
			this.timer = this.waitAfterTrigger;
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000B1AC File Offset: 0x000093AC
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.inRedBowl = false;
		}
		else if (other.tag == "FailCollider")
		{
			this.inGreenBowl = false;
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000B1EC File Offset: 0x000093EC
	private void CheckVictoryConditions()
	{
		if (!this.inRedBowl && !this.inGreenBowl)
		{
			return;
		}
		if ((from x in this.allCandies
		where x.timer == 0f && !x.dragged && (x.inRedBowl || x.inGreenBowl)
		select x).Count<PuzzleMNM_Candy>() != this.allCandies.Count<PuzzleMNM_Candy>())
		{
			return;
		}
		if ((from x in this.broCandies
		where x.inRedBowl == this.inRedBowl && x.inGreenBowl == this.inGreenBowl
		select x).Count<PuzzleMNM_Candy>() != this.broCandies.Count<PuzzleMNM_Candy>())
		{
			this.CheckBowls();
			Global.LevelCompleted(0f, true);
			return;
		}
		if ((from x in this.allCandies
		where x.inRedBowl == this.inRedBowl && x.inGreenBowl == this.inGreenBowl
		select x).Count<PuzzleMNM_Candy>() != this.broCandies.Count<PuzzleMNM_Candy>())
		{
			this.CheckBowls();
			Global.LevelCompleted(0f, true);
			return;
		}
		if ((this.red && this.inGreenBowl) || (!this.red && this.inRedBowl))
		{
			this.CheckBowls();
			Global.LevelCompleted(0f, true);
			return;
		}
		this.CheckBowls();
		Global.LevelFailed(0f, true);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000B328 File Offset: 0x00009528
	private void CheckBowls()
	{
		if (Mathf.Abs(Mathf.DeltaAngle(this.redBowl.eulerAngles.z, 180f)) < this.deltaAngle && Mathf.Abs(Mathf.DeltaAngle(this.greenBowl.eulerAngles.z, 180f)) < this.deltaAngle)
		{
			this.GetPuzzleStats().GetComponent<AudioVoiceEndAchievement>().getTrophy();
		}
	}

	// Token: 0x040001B9 RID: 441
	public bool red;

	// Token: 0x040001BA RID: 442
	public float waitAfterTrigger = 0.2f;

	// Token: 0x040001BB RID: 443
	public Transform redBowl;

	// Token: 0x040001BC RID: 444
	public Transform greenBowl;

	// Token: 0x040001BD RID: 445
	public float deltaAngle = 2.5f;

	// Token: 0x040001BE RID: 446
	private PuzzleMNM_Candy[] allCandies;

	// Token: 0x040001BF RID: 447
	private PuzzleMNM_Candy[] broCandies;

	// Token: 0x040001C0 RID: 448
	private bool inRedBowl;

	// Token: 0x040001C1 RID: 449
	private bool inGreenBowl;

	// Token: 0x040001C2 RID: 450
	private float timer;
}
