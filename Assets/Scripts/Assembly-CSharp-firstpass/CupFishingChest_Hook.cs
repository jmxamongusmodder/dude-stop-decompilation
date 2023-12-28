using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class CupFishingChest_Hook : MonoBehaviour
{
	// Token: 0x060014A0 RID: 5280 RVA: 0x000385CE File Offset: 0x000369CE
	private void Start()
	{
		this.rod = this.GetComponentInPuzzleStats<CupFishingChest_Rod>();
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x000385DC File Offset: 0x000369DC
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			Audio.self.playOneShot("029310a3-ff16-422f-a6d9-dbbe092cd931", 1f);
			this.rod.StuffCaught(other.transform);
			Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().pullOutCup();
		}
		else if (other.tag == "FailCollider" && !this.goingForChest && !this.gotStuff)
		{
			this.rod.StuffCaught(other.transform);
			Global.self.currPuzzle.GetComponent<AudioVoice_CupFishingChest>().hitObstacle();
		}
		else if (other.tag == "GlobalCollider" && !this.gotStuff)
		{
			this.staticBubbles.gameObject.SetActive(true);
			Audio.self.playOneShot("a6e1a698-b590-45f5-9944-22cff03ddb5d", 1f);
			this.waterSplash.transform.position = new Vector2(base.transform.position.x, this.waterSplash.transform.position.y);
			this.waterSplash.Emit();
		}
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x00038728 File Offset: 0x00036B28
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "GlobalCollider" && this.gotStuff)
		{
			Audio.self.playOneShot("eb9347df-5138-4501-9f15-00d66c981f83", 1f);
			this.waterSplash.transform.position = new Vector2(base.transform.position.x, this.waterSplash.transform.position.y);
			this.waterSplash.Emit();
		}
	}

	// Token: 0x0400120C RID: 4620
	public ParticleSystem waterSplash;

	// Token: 0x0400120D RID: 4621
	public ParticleSystem staticBubbles;

	// Token: 0x0400120E RID: 4622
	[HideInInspector]
	public bool goingForChest;

	// Token: 0x0400120F RID: 4623
	[HideInInspector]
	public bool gotStuff;

	// Token: 0x04001210 RID: 4624
	private CupFishingChest_Rod rod;
}
