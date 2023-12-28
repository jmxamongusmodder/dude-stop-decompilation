using System;

namespace Steamworks
{
	// Token: 0x02000232 RID: 562
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : Attribute
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x0000FA9B File Offset: 0x0000DE9B
		public CallbackIdentityAttribute(int callbackNum)
		{
			this.Identity = callbackNum;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0000FAAA File Offset: 0x0000DEAA
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x0000FAB2 File Offset: 0x0000DEB2
		public int Identity { get; set; }
	}
}
