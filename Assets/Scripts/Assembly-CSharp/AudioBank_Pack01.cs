using System;
using FMODUnity;

// Token: 0x02000294 RID: 660
public class AudioBank_Pack01 : AudioBank
{
	// Token: 0x06001030 RID: 4144 RVA: 0x00014C0F File Offset: 0x0001300F
	private void Awake()
	{
		if (Global.self.isGameIntroActive && !Global.self.isGameIntroJustFinished)
		{
			Audio.self.loadBank(this.gameIntroList, true);
		}
		else
		{
			Audio.self.UnloadBank();
		}
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x00014C4F File Offset: 0x0001304F
	public override void startPack()
	{
		if (Global.self.isGameIntroActive)
		{
			Audio.self.loadBank(this.gameIntroList, true);
		}
		else
		{
			Audio.self.loadBank(this.banksForPack, true);
		}
	}

	// Token: 0x04000D44 RID: 3396
	[BankRef]
	public string[] gameIntroList;

	// Token: 0x04000D45 RID: 3397
	[BankRef]
	public string[] banksForPack;
}
