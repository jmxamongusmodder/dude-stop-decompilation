using System;
using UnityEngine;

namespace Steamworks
{
	// Token: 0x02000227 RID: 551
	public static class CallbackDispatcher
	{
		// Token: 0x06000D10 RID: 3344 RVA: 0x0000F2E6 File Offset: 0x0000D6E6
		public static void ExceptionHandler(Exception e)
		{
			Debug.LogException(e);
		}
	}
}
