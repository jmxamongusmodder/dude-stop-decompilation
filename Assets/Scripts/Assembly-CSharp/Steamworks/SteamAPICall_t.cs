using System;

namespace Steamworks
{
	// Token: 0x0200027E RID: 638
	[Serializable]
	public struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0001237C File Offset: 0x0001077C
		public SteamAPICall_t(ulong value)
		{
			this.m_SteamAPICall = value;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00012385 File Offset: 0x00010785
		public override string ToString()
		{
			return this.m_SteamAPICall.ToString();
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00012398 File Offset: 0x00010798
		public override bool Equals(object other)
		{
			return other is SteamAPICall_t && this == (SteamAPICall_t)other;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000123B9 File Offset: 0x000107B9
		public override int GetHashCode()
		{
			return this.m_SteamAPICall.GetHashCode();
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x000123CC File Offset: 0x000107CC
		public static bool operator ==(SteamAPICall_t x, SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000123DE File Offset: 0x000107DE
		public static bool operator !=(SteamAPICall_t x, SteamAPICall_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x000123EA File Offset: 0x000107EA
		public static explicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t(value);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x000123F2 File Offset: 0x000107F2
		public static explicit operator ulong(SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000123FB File Offset: 0x000107FB
		public bool Equals(SteamAPICall_t other)
		{
			return this.m_SteamAPICall == other.m_SteamAPICall;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0001240C File Offset: 0x0001080C
		public int CompareTo(SteamAPICall_t other)
		{
			return this.m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}

		// Token: 0x04000CC7 RID: 3271
		public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0UL);

		// Token: 0x04000CC8 RID: 3272
		public ulong m_SteamAPICall;
	}
}
