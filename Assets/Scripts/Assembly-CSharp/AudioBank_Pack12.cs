using System;
using FMODUnity;

// Token: 0x02000296 RID: 662
public class AudioBank_Pack12 : AudioBank
{
	// Token: 0x06001035 RID: 4149 RVA: 0x00014CEB File Offset: 0x000130EB
	public override void startPack()
	{
		if (SerializableGameStats.self.isGameFinished)
		{
			Audio.self.loadBank(this.bankList, true);
		}
		else
		{
			Audio.self.loadBank(this.bankListDuck, true);
		}
	}

	// Token: 0x04000D48 RID: 3400
	[BankRef]
	public string[] bankList;

	// Token: 0x04000D49 RID: 3401
	[BankRef]
	public string[] bankListDuck;
}
