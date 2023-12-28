using System;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class PuzzleNonWaitingBus_Passenger : MonoBehaviour
{
	// Token: 0x06001B8D RID: 7053 RVA: 0x000721F9 File Offset: 0x000705F9
	private void TwoAreIn()
	{
		this.door.twoAreIn = true;
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x00072208 File Offset: 0x00070608
	private void CheckForDoor()
	{
		if (!this.door.isOpen)
		{
			base.GetComponent<Animator>().enabled = false;
			this.normalFace.SetActive(false);
			this.angryFace.SetActive(true);
			this.UnblockDoor();
			this.door.waitingOutside = true;
			Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>().OnStuckOutside();
		}
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x0007226F File Offset: 0x0007066F
	private void UnblockDoor()
	{
		this.door.blocked = false;
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x0007227D File Offset: 0x0007067D
	private void BlockDoor()
	{
		this.door.blocked = true;
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x0007228C File Offset: 0x0007068C
	private void EverybodyIsIn()
	{
		this.normalFace.SetActive(true);
		this.angryFace.SetActive(false);
		this.door.blocked = false;
		this.door.everybodyIsIn = true;
		if (Global.self.NoCurrentTransition)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_NonWaitingBus>().OnEveryoneIsInside();
		}
	}

	// Token: 0x040019D2 RID: 6610
	public PuzzleNonWaitingBus_Door door;

	// Token: 0x040019D3 RID: 6611
	public GameObject normalFace;

	// Token: 0x040019D4 RID: 6612
	public GameObject angryFace;

	// Token: 0x040019D5 RID: 6613
	public Transform firstP;

	// Token: 0x040019D6 RID: 6614
	public Transform secondP;

	// Token: 0x040019D7 RID: 6615
	public Transform bus;
}
