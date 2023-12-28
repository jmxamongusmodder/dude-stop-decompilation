using System;
using FMOD;

namespace FMODUnity
{
	// Token: 0x02000011 RID: 17
	public class SystemNotInitializedException : Exception
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000035E8 File Offset: 0x000019E8
		public SystemNotInitializedException(RESULT result, string location) : base(string.Format("FMOD Studio initialization failed : {2} : {0} : {1}", result.ToString(), Error.String(result), location))
		{
			this.Result = result;
			this.Location = location;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000361C File Offset: 0x00001A1C
		public SystemNotInitializedException(Exception inner) : base("FMOD Studio initialization failed", inner)
		{
		}

		// Token: 0x04000028 RID: 40
		public RESULT Result;

		// Token: 0x04000029 RID: 41
		public string Location;
	}
}
