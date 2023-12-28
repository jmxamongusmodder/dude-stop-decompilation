using System;
using FMODUnity;

// Token: 0x02000295 RID: 661
public class AudioBank_Pack10 : AudioBank
{
	// Token: 0x06001033 RID: 4147 RVA: 0x00014C90 File Offset: 0x00013090
	public override void startPack()
	{
		if (Global.self.pack10CutsceneActive || base.GetComponent<AudioVoice_Pack10>().debugForceOn)
		{
			Audio.self.loadBank(this.bankListDuck, true);
		}
		else
		{
			Audio.self.loadBank(this.bankList, true);
		}
	}

	// Token: 0x04000D46 RID: 3398
	[BankRef]
	public string[] bankList;

	// Token: 0x04000D47 RID: 3399
	[BankRef]
	public string[] bankListDuck;
}
