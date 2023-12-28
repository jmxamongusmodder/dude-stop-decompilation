using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x0200005D RID: 93
	public struct Memory
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004F43 File Offset: 0x00003343
		public static RESULT Initialize(IntPtr poolmem, int poollen, MEMORY_ALLOC_CALLBACK useralloc, MEMORY_REALLOC_CALLBACK userrealloc, MEMORY_FREE_CALLBACK userfree, MEMORY_TYPE memtypeflags)
		{
			return Memory.FMOD5_Memory_Initialize(poolmem, poollen, useralloc, userrealloc, userfree, memtypeflags);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004F52 File Offset: 0x00003352
		public static RESULT GetStats(out int currentalloced, out int maxalloced)
		{
			return Memory.GetStats(out currentalloced, out maxalloced, false);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004F5C File Offset: 0x0000335C
		public static RESULT GetStats(out int currentalloced, out int maxalloced, bool blocking)
		{
			return Memory.FMOD5_Memory_GetStats(out currentalloced, out maxalloced, blocking);
		}

		// Token: 0x06000104 RID: 260
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Memory_Initialize(IntPtr poolmem, int poollen, MEMORY_ALLOC_CALLBACK useralloc, MEMORY_REALLOC_CALLBACK userrealloc, MEMORY_FREE_CALLBACK userfree, MEMORY_TYPE memtypeflags);

		// Token: 0x06000105 RID: 261
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Memory_GetStats(out int currentalloced, out int maxalloced, bool blocking);
	}
}
