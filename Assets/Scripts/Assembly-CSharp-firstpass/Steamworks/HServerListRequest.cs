using System;

namespace Steamworks
{
	// Token: 0x02000271 RID: 625
	[Serializable]
	public struct HServerListRequest : IEquatable<HServerListRequest>
	{
		// Token: 0x06000EE0 RID: 3808 RVA: 0x00011AAB File Offset: 0x0000FEAB
		public HServerListRequest(IntPtr value)
		{
			this.m_HServerListRequest = value;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00011AB4 File Offset: 0x0000FEB4
		public override string ToString()
		{
			return this.m_HServerListRequest.ToString();
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00011AC7 File Offset: 0x0000FEC7
		public override bool Equals(object other)
		{
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00011AE8 File Offset: 0x0000FEE8
		public override int GetHashCode()
		{
			return this.m_HServerListRequest.GetHashCode();
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00011AFB File Offset: 0x0000FEFB
		public static bool operator ==(HServerListRequest x, HServerListRequest y)
		{
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00011B10 File Offset: 0x0000FF10
		public static bool operator !=(HServerListRequest x, HServerListRequest y)
		{
			return !(x == y);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00011B1C File Offset: 0x0000FF1C
		public static explicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest(value);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00011B24 File Offset: 0x0000FF24
		public static explicit operator IntPtr(HServerListRequest that)
		{
			return that.m_HServerListRequest;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00011B2D File Offset: 0x0000FF2D
		public bool Equals(HServerListRequest other)
		{
			return this.m_HServerListRequest == other.m_HServerListRequest;
		}

		// Token: 0x04000CB0 RID: 3248
		public static readonly HServerListRequest Invalid = new HServerListRequest(IntPtr.Zero);

		// Token: 0x04000CB1 RID: 3249
		public IntPtr m_HServerListRequest;
	}
}
