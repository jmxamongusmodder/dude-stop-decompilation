using System;

namespace Steamworks
{
	// Token: 0x02000279 RID: 633
	[Serializable]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x00012013 File Offset: 0x00010413
		public ScreenshotHandle(uint value)
		{
			this.m_ScreenshotHandle = value;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0001201C File Offset: 0x0001041C
		public override string ToString()
		{
			return this.m_ScreenshotHandle.ToString();
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0001202F File Offset: 0x0001042F
		public override bool Equals(object other)
		{
			return other is ScreenshotHandle && this == (ScreenshotHandle)other;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00012050 File Offset: 0x00010450
		public override int GetHashCode()
		{
			return this.m_ScreenshotHandle.GetHashCode();
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00012063 File Offset: 0x00010463
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00012075 File Offset: 0x00010475
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00012081 File Offset: 0x00010481
		public static explicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle(value);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00012089 File Offset: 0x00010489
		public static explicit operator uint(ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00012092 File Offset: 0x00010492
		public bool Equals(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000120A3 File Offset: 0x000104A3
		public int CompareTo(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}

		// Token: 0x04000CBE RID: 3262
		public static readonly ScreenshotHandle Invalid = new ScreenshotHandle(0U);

		// Token: 0x04000CBF RID: 3263
		public uint m_ScreenshotHandle;
	}
}
