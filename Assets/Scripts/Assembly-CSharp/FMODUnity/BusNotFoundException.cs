using System;

namespace FMODUnity
{
	// Token: 0x0200000E RID: 14
	public class BusNotFoundException : Exception
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00003553 File Offset: 0x00001953
		public BusNotFoundException(string path) : base("FMOD Studio bus not found '" + path + "'")
		{
			this.Path = path;
		}

		// Token: 0x04000024 RID: 36
		public string Path;
	}
}
