using System;
using System.Collections;

namespace DudeStop.Analytics
{
	// Token: 0x02000286 RID: 646
	public interface IContainer
	{
		// Token: 0x06000FEB RID: 4075
		void Fail();

		// Token: 0x06000FEC RID: 4076
		void DisableInternetAccess();

		// Token: 0x06000FED RID: 4077
		void Coroutine(IEnumerator func);
	}
}
