using System;
using FMODUnity;

// Token: 0x02000292 RID: 658
public class AudioBank_Default : AudioBank
{
	// Token: 0x0600102B RID: 4139 RVA: 0x00014BCD File Offset: 0x00012FCD
	private void Awake()
	{
		Audio.self.UnloadBank();
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x00014BD9 File Offset: 0x00012FD9
	public override void startPack()
	{
		Audio.self.loadBank(this.bankList, true);
	}

	// Token: 0x04000D42 RID: 3394
	[BankRef]
	public string[] bankList;
}
