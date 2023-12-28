using System;

namespace FMOD
{
	// Token: 0x02000069 RID: 105
	public struct StringWrapper
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00006E9C File Offset: 0x0000529C
		public static implicit operator string(StringWrapper fstring)
		{
			string result;
			using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
			{
				result = freeHelper.stringFromNative(fstring.nativeUtf8Ptr);
			}
			return result;
		}

		// Token: 0x0400028D RID: 653
		private IntPtr nativeUtf8Ptr;
	}
}
