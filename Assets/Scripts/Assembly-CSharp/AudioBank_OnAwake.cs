using System;
using FMODUnity;

// Token: 0x02000293 RID: 659
public class AudioBank_OnAwake : AudioBank
{
	// Token: 0x0600102E RID: 4142 RVA: 0x00014BF4 File Offset: 0x00012FF4
	private void Awake()
	{
		Audio.self.loadBank(this.bankList, true);
	}

	// Token: 0x04000D43 RID: 3395
	[BankRef]
	public string[] bankList;
}
