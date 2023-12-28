using System;

namespace FMOD.Studio
{
	// Token: 0x020000E5 RID: 229
	public struct SOUND_INFO
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00007560 File Offset: 0x00005960
		public string name
		{
			get
			{
				string result;
				using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
				{
					result = (((this.mode & (MODE.OPENMEMORY | MODE.OPENMEMORY_POINT)) != MODE.DEFAULT) ? string.Empty : freeHelper.stringFromNative(this.name_or_data));
				}
				return result;
			}
		}

		// Token: 0x04000495 RID: 1173
		public IntPtr name_or_data;

		// Token: 0x04000496 RID: 1174
		public MODE mode;

		// Token: 0x04000497 RID: 1175
		public CREATESOUNDEXINFO exinfo;

		// Token: 0x04000498 RID: 1176
		public int subsoundindex;
	}
}
