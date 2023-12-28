using System;

namespace FMOD.Studio
{
	// Token: 0x020000DF RID: 223
	[Flags]
	public enum SYSTEM_CALLBACK_TYPE : uint
	{
		// Token: 0x0400047E RID: 1150
		PREUPDATE = 1U,
		// Token: 0x0400047F RID: 1151
		POSTUPDATE = 2U,
		// Token: 0x04000480 RID: 1152
		BANK_UNLOAD = 4U,
		// Token: 0x04000481 RID: 1153
		ALL = 4294967295U
	}
}
