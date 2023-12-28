using System;

namespace FMODUnity
{
	// Token: 0x0200000F RID: 15
	public class VCANotFoundException : Exception
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003572 File Offset: 0x00001972
		public VCANotFoundException(string path) : base("FMOD Studio VCA not found '" + path + "'")
		{
			this.Path = path;
		}

		// Token: 0x04000025 RID: 37
		public string Path;
	}
}
