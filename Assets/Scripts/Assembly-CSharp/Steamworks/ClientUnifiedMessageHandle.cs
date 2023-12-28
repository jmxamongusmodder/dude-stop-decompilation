using System;

namespace Steamworks
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	public struct ClientUnifiedMessageHandle : IEquatable<ClientUnifiedMessageHandle>, IComparable<ClientUnifiedMessageHandle>
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x00012592 File Offset: 0x00010992
		public ClientUnifiedMessageHandle(ulong value)
		{
			this.m_ClientUnifiedMessageHandle = value;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0001259B File Offset: 0x0001099B
		public override string ToString()
		{
			return this.m_ClientUnifiedMessageHandle.ToString();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000125AE File Offset: 0x000109AE
		public override bool Equals(object other)
		{
			return other is ClientUnifiedMessageHandle && this == (ClientUnifiedMessageHandle)other;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000125CF File Offset: 0x000109CF
		public override int GetHashCode()
		{
			return this.m_ClientUnifiedMessageHandle.GetHashCode();
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000125E2 File Offset: 0x000109E2
		public static bool operator ==(ClientUnifiedMessageHandle x, ClientUnifiedMessageHandle y)
		{
			return x.m_ClientUnifiedMessageHandle == y.m_ClientUnifiedMessageHandle;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000125F4 File Offset: 0x000109F4
		public static bool operator !=(ClientUnifiedMessageHandle x, ClientUnifiedMessageHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00012600 File Offset: 0x00010A00
		public static explicit operator ClientUnifiedMessageHandle(ulong value)
		{
			return new ClientUnifiedMessageHandle(value);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00012608 File Offset: 0x00010A08
		public static explicit operator ulong(ClientUnifiedMessageHandle that)
		{
			return that.m_ClientUnifiedMessageHandle;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00012611 File Offset: 0x00010A11
		public bool Equals(ClientUnifiedMessageHandle other)
		{
			return this.m_ClientUnifiedMessageHandle == other.m_ClientUnifiedMessageHandle;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00012622 File Offset: 0x00010A22
		public int CompareTo(ClientUnifiedMessageHandle other)
		{
			return this.m_ClientUnifiedMessageHandle.CompareTo(other.m_ClientUnifiedMessageHandle);
		}

		// Token: 0x04000CCD RID: 3277
		public static readonly ClientUnifiedMessageHandle Invalid = new ClientUnifiedMessageHandle(0UL);

		// Token: 0x04000CCE RID: 3278
		public ulong m_ClientUnifiedMessageHandle;
	}
}
