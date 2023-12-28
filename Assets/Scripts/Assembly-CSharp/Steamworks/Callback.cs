using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000228 RID: 552
	public sealed class Callback<T> : IDisposable
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x0000F2EE File Offset: 0x0000D6EE
		public Callback(Callback<T>.DispatchDelegate func, bool bGameServer = false)
		{
			this.m_bGameServer = bGameServer;
			this.BuildCCallbackBase();
			this.Register(func);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000D12 RID: 3346 RVA: 0x0000F32C File Offset: 0x0000D72C
		// (remove) Token: 0x06000D13 RID: 3347 RVA: 0x0000F364 File Offset: 0x0000D764
		private event Callback<T>.DispatchDelegate m_Func;

		// Token: 0x06000D14 RID: 3348 RVA: 0x0000F39A File Offset: 0x0000D79A
		public static Callback<T> Create(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, false);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0000F3A3 File Offset: 0x0000D7A3
		public static Callback<T> CreateGameServer(Callback<T>.DispatchDelegate func)
		{
			return new Callback<T>(func, true);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0000F3AC File Offset: 0x0000D7AC
		~Callback()
		{
			this.Dispose();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0000F3DC File Offset: 0x0000D7DC
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Unregister();
			if (this.m_pVTable != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_pVTable);
			}
			if (this.m_pCCallbackBase.IsAllocated)
			{
				this.m_pCCallbackBase.Free();
			}
			this.m_bDisposed = true;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0000F444 File Offset: 0x0000D844
		public void Register(Callback<T>.DispatchDelegate func)
		{
			if (func == null)
			{
				throw new Exception("Callback function must not be null.");
			}
			if ((this.m_CCallbackBase.m_nCallbackFlags & 1) == 1)
			{
				this.Unregister();
			}
			if (this.m_bGameServer)
			{
				this.SetGameserverFlag();
			}
			this.m_Func = func;
			NativeMethods.SteamAPI_RegisterCallback(this.m_pCCallbackBase.AddrOfPinnedObject(), CallbackIdentities.GetCallbackIdentity(typeof(T)));
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0000F4B2 File Offset: 0x0000D8B2
		public void Unregister()
		{
			NativeMethods.SteamAPI_UnregisterCallback(this.m_pCCallbackBase.AddrOfPinnedObject());
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0000F4C4 File Offset: 0x0000D8C4
		public void SetGameserverFlag()
		{
			CCallbackBase ccallbackBase = this.m_CCallbackBase;
			ccallbackBase.m_nCallbackFlags |= 2;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0000F4DC File Offset: 0x0000D8DC
		private void OnRunCallback(IntPtr pvParam)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0000F52C File Offset: 0x0000D92C
		private void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall)
		{
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))));
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0000F57C File Offset: 0x0000D97C
		private int OnGetCallbackSizeBytes()
		{
			return this.m_size;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0000F584 File Offset: 0x0000D984
		private void BuildCCallbackBase()
		{
			this.VTable = new CCallbackBaseVTable
			{
				m_RunCallResult = new CCallbackBaseVTable.RunCRDel(this.OnRunCallResult),
				m_RunCallback = new CCallbackBaseVTable.RunCBDel(this.OnRunCallback),
				m_GetCallbackSizeBytes = new CCallbackBaseVTable.GetCallbackSizeBytesDel(this.OnGetCallbackSizeBytes)
			};
			this.m_pVTable = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CCallbackBaseVTable)));
			Marshal.StructureToPtr(this.VTable, this.m_pVTable, false);
			this.m_CCallbackBase = new CCallbackBase
			{
				m_vfptr = this.m_pVTable,
				m_nCallbackFlags = 0,
				m_iCallback = CallbackIdentities.GetCallbackIdentity(typeof(T))
			};
			this.m_pCCallbackBase = GCHandle.Alloc(this.m_CCallbackBase, GCHandleType.Pinned);
		}

		// Token: 0x04000C2F RID: 3119
		private CCallbackBaseVTable VTable;

		// Token: 0x04000C30 RID: 3120
		private IntPtr m_pVTable = IntPtr.Zero;

		// Token: 0x04000C31 RID: 3121
		private CCallbackBase m_CCallbackBase;

		// Token: 0x04000C32 RID: 3122
		private GCHandle m_pCCallbackBase;

		// Token: 0x04000C34 RID: 3124
		private bool m_bGameServer;

		// Token: 0x04000C35 RID: 3125
		private readonly int m_size = Marshal.SizeOf(typeof(T));

		// Token: 0x04000C36 RID: 3126
		private bool m_bDisposed;

		// Token: 0x02000229 RID: 553
		// (Invoke) Token: 0x06000D20 RID: 3360
		public delegate void DispatchDelegate(T param);
	}
}
