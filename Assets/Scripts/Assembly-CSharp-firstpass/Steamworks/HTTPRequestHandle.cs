using System;

namespace Steamworks
{
	// Token: 0x0200026D RID: 621
	[Serializable]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x000117F3 File Offset: 0x0000FBF3
		public HTTPRequestHandle(uint value)
		{
			this.m_HTTPRequestHandle = value;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000117FC File Offset: 0x0000FBFC
		public override string ToString()
		{
			return this.m_HTTPRequestHandle.ToString();
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0001180F File Offset: 0x0000FC0F
		public override bool Equals(object other)
		{
			return other is HTTPRequestHandle && this == (HTTPRequestHandle)other;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00011830 File Offset: 0x0000FC30
		public override int GetHashCode()
		{
			return this.m_HTTPRequestHandle.GetHashCode();
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00011843 File Offset: 0x0000FC43
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00011855 File Offset: 0x0000FC55
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00011861 File Offset: 0x0000FC61
		public static explicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle(value);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00011869 File Offset: 0x0000FC69
		public static explicit operator uint(HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00011872 File Offset: 0x0000FC72
		public bool Equals(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x00011883 File Offset: 0x0000FC83
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}

		// Token: 0x04000CA9 RID: 3241
		public static readonly HTTPRequestHandle Invalid = new HTTPRequestHandle(0U);

		// Token: 0x04000CAA RID: 3242
		public uint m_HTTPRequestHandle;
	}
}
