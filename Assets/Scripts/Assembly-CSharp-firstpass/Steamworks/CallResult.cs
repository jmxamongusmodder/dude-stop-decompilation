using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200022A RID: 554
	public sealed class CallResult<T> : IDisposable
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x0000F647 File Offset: 0x0000DA47
		public CallResult(CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
			this.BuildCCallbackBase();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000D24 RID: 3364 RVA: 0x0000F688 File Offset: 0x0000DA88
		// (remove) Token: 0x06000D25 RID: 3365 RVA: 0x0000F6C0 File Offset: 0x0000DAC0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event CallResult<T>.APIDispatchDelegate m_Func;

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0000F6F6 File Offset: 0x0000DAF6
		public SteamAPICall_t Handle
		{
			get
			{
				return this.m_hAPICall;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0000F6FE File Offset: 0x0000DAFE
		public static CallResult<T> Create(CallResult<T>.APIDispatchDelegate func = null)
		{
			return new CallResult<T>(func);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0000F708 File Offset: 0x0000DB08
		~CallResult()
		{
			this.Dispose();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0000F738 File Offset: 0x0000DB38
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Cancel();
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

		// Token: 0x06000D2A RID: 3370 RVA: 0x0000F7A0 File Offset: 0x0000DBA0
		public void Set(SteamAPICall_t hAPICall, CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new Exception("CallResult function was null, you must either set it in the CallResult Constructor or in Set()");
			}
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				NativeMethods.SteamAPI_UnregisterCallResult(this.m_pCCallbackBase.AddrOfPinnedObject(), (ulong)this.m_hAPICall);
			}
			this.m_hAPICall = hAPICall;
			if (hAPICall != SteamAPICall_t.Invalid)
			{
				NativeMethods.SteamAPI_RegisterCallResult(this.m_pCCallbackBase.AddrOfPinnedObject(), (ulong)hAPICall);
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0000F82D File Offset: 0x0000DC2D
		public bool IsActive()
		{
			return this.m_hAPICall != SteamAPICall_t.Invalid;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0000F83F File Offset: 0x0000DC3F
		public void Cancel()
		{
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				NativeMethods.SteamAPI_UnregisterCallResult(this.m_pCCallbackBase.AddrOfPinnedObject(), (ulong)this.m_hAPICall);
				this.m_hAPICall = SteamAPICall_t.Invalid;
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0000F87C File Offset: 0x0000DC7C
		public void SetGameserverFlag()
		{
			CCallbackBase ccallbackBase = this.m_CCallbackBase;
			ccallbackBase.m_nCallbackFlags |= 2;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0000F894 File Offset: 0x0000DC94
		private void OnRunCallback(IntPtr pvParam)
		{
			this.m_hAPICall = SteamAPICall_t.Invalid;
			try
			{
				this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), false);
			}
			catch (Exception e)
			{
				CallbackDispatcher.ExceptionHandler(e);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0000F8F0 File Offset: 0x0000DCF0
		private void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			SteamAPICall_t x = (SteamAPICall_t)hSteamAPICall_;
			if (x == this.m_hAPICall)
			{
				this.m_hAPICall = SteamAPICall_t.Invalid;
				try
				{
					this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), bFailed);
				}
				catch (Exception e)
				{
					CallbackDispatcher.ExceptionHandler(e);
				}
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0000F964 File Offset: 0x0000DD64
		private int OnGetCallbackSizeBytes()
		{
			return this.m_size;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0000F96C File Offset: 0x0000DD6C
		private void BuildCCallbackBase()
		{
			this.VTable = new CCallbackBaseVTable
			{
				m_RunCallback = new CCallbackBaseVTable.RunCBDel(this.OnRunCallback),
				m_RunCallResult = new CCallbackBaseVTable.RunCRDel(this.OnRunCallResult),
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

		// Token: 0x04000C37 RID: 3127
		private CCallbackBaseVTable VTable;

		// Token: 0x04000C38 RID: 3128
		private IntPtr m_pVTable = IntPtr.Zero;

		// Token: 0x04000C39 RID: 3129
		private CCallbackBase m_CCallbackBase;

		// Token: 0x04000C3A RID: 3130
		private GCHandle m_pCCallbackBase;

		// Token: 0x04000C3C RID: 3132
		private SteamAPICall_t m_hAPICall = SteamAPICall_t.Invalid;

		// Token: 0x04000C3D RID: 3133
		private readonly int m_size = Marshal.SizeOf(typeof(T));

		// Token: 0x04000C3E RID: 3134
		private bool m_bDisposed;

		// Token: 0x0200022B RID: 555
		// (Invoke) Token: 0x06000D33 RID: 3379
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);
	}
}
