using System;

namespace Steamworks
{
	// Token: 0x0200027B RID: 635
	[Serializable]
	public struct AppId_t : IEquatable<AppId_t>, IComparable<AppId_t>
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x00012168 File Offset: 0x00010568
		public AppId_t(uint value)
		{
			this.m_AppId = value;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00012171 File Offset: 0x00010571
		public override string ToString()
		{
			return this.m_AppId.ToString();
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00012184 File Offset: 0x00010584
		public override bool Equals(object other)
		{
			return other is AppId_t && this == (AppId_t)other;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000121A5 File Offset: 0x000105A5
		public override int GetHashCode()
		{
			return this.m_AppId.GetHashCode();
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000121B8 File Offset: 0x000105B8
		public static bool operator ==(AppId_t x, AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000121CA File Offset: 0x000105CA
		public static bool operator !=(AppId_t x, AppId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000121D6 File Offset: 0x000105D6
		public static explicit operator AppId_t(uint value)
		{
			return new AppId_t(value);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000121DE File Offset: 0x000105DE
		public static explicit operator uint(AppId_t that)
		{
			return that.m_AppId;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000121E7 File Offset: 0x000105E7
		public bool Equals(AppId_t other)
		{
			return this.m_AppId == other.m_AppId;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000121F8 File Offset: 0x000105F8
		public int CompareTo(AppId_t other)
		{
			return this.m_AppId.CompareTo(other.m_AppId);
		}

		// Token: 0x04000CC1 RID: 3265
		public static readonly AppId_t Invalid = new AppId_t(0U);

		// Token: 0x04000CC2 RID: 3266
		public uint m_AppId;
	}
}
