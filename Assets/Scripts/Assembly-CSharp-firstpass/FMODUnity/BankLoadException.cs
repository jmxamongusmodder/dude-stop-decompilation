using System;
using FMOD;

namespace FMODUnity
{
	// Token: 0x02000010 RID: 16
	public class BankLoadException : Exception
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003591 File Offset: 0x00001991
		public BankLoadException(string path, RESULT result) : base(string.Format("FMOD Studio could not load bank '{0}' : {1} : {2}", path, result.ToString(), Error.String(result)))
		{
			this.Path = path;
			this.Result = result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000035C5 File Offset: 0x000019C5
		public BankLoadException(string path, string error) : base(string.Format("FMOD Studio could not load bank '{0}' : {1}", path, error))
		{
			this.Path = path;
			this.Result = RESULT.ERR_INTERNAL;
		}

		// Token: 0x04000026 RID: 38
		public string Path;

		// Token: 0x04000027 RID: 39
		public RESULT Result;
	}
}
