using System;

namespace FMODUnity
{
	// Token: 0x0200000D RID: 13
	public class EventNotFoundException : Exception
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000350A File Offset: 0x0000190A
		public EventNotFoundException(string path) : base("FMOD Studio event not found '" + path + "'")
		{
			this.Path = path;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003529 File Offset: 0x00001929
		public EventNotFoundException(Guid guid) : base("FMOD Studio event not found " + guid.ToString("b") + string.Empty)
		{
			this.Guid = guid;
		}

		// Token: 0x04000022 RID: 34
		public Guid Guid;

		// Token: 0x04000023 RID: 35
		public string Path;
	}
}
