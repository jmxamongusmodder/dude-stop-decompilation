using System;

namespace Steamworks
{
	// Token: 0x0200026C RID: 620
	[Serializable]
	public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x06000EAA RID: 3754 RVA: 0x00011742 File Offset: 0x0000FB42
		public HTTPCookieContainerHandle(uint value)
		{
			this.m_HTTPCookieContainerHandle = value;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0001174B File Offset: 0x0000FB4B
		public override string ToString()
		{
			return this.m_HTTPCookieContainerHandle.ToString();
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0001175E File Offset: 0x0000FB5E
		public override bool Equals(object other)
		{
			return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0001177F File Offset: 0x0000FB7F
		public override int GetHashCode()
		{
			return this.m_HTTPCookieContainerHandle.GetHashCode();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00011792 File Offset: 0x0000FB92
		public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000117A4 File Offset: 0x0000FBA4
		public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000117B0 File Offset: 0x0000FBB0
		public static explicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle(value);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000117B8 File Offset: 0x0000FBB8
		public static explicit operator uint(HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000117C1 File Offset: 0x0000FBC1
		public bool Equals(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000117D2 File Offset: 0x0000FBD2
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}

		// Token: 0x04000CA7 RID: 3239
		public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0U);

		// Token: 0x04000CA8 RID: 3240
		public uint m_HTTPCookieContainerHandle;
	}
}
