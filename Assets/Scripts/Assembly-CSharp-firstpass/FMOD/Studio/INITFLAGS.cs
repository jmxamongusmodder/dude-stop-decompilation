﻿using System;

namespace FMOD.Studio
{
	// Token: 0x020000E9 RID: 233
	[Flags]
	public enum INITFLAGS : uint
	{
		// Token: 0x040004A6 RID: 1190
		NORMAL = 0U,
		// Token: 0x040004A7 RID: 1191
		LIVEUPDATE = 1U,
		// Token: 0x040004A8 RID: 1192
		ALLOW_MISSING_PLUGINS = 2U,
		// Token: 0x040004A9 RID: 1193
		SYNCHRONOUS_UPDATE = 4U,
		// Token: 0x040004AA RID: 1194
		DEFERRED_CALLBACKS = 8U,
		// Token: 0x040004AB RID: 1195
		LOAD_FROM_UPDATE = 16U
	}
}
