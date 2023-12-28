using System;

namespace Steamworks
{
	// Token: 0x02000231 RID: 561
	internal class CallbackIdentities
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x0000FA48 File Offset: 0x0000DE48
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), false);
			int num = 0;
			if (num >= customAttributes.Length)
			{
				throw new Exception("Callback number not found for struct " + callbackStruct);
			}
			CallbackIdentityAttribute callbackIdentityAttribute = (CallbackIdentityAttribute)customAttributes[num];
			return callbackIdentityAttribute.Identity;
		}
	}
}
