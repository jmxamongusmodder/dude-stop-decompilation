using System;

namespace FMOD.Studio
{
	// Token: 0x020000F3 RID: 243
	// (Invoke) Token: 0x060004FF RID: 1279
	public delegate RESULT COMMANDREPLAY_LOAD_BANK_CALLBACK(CommandReplay replay, Guid guid, StringWrapper bankFilename, LOAD_BANK_FLAGS flags, out Bank bank, IntPtr userdata);
}
