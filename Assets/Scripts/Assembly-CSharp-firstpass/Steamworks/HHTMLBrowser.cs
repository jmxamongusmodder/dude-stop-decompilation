using System;

namespace Steamworks
{
	// Token: 0x0200026B RID: 619
	[Serializable]
	public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x00011691 File Offset: 0x0000FA91
		public HHTMLBrowser(uint value)
		{
			this.m_HHTMLBrowser = value;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0001169A File Offset: 0x0000FA9A
		public override string ToString()
		{
			return this.m_HHTMLBrowser.ToString();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000116AD File Offset: 0x0000FAAD
		public override bool Equals(object other)
		{
			return other is HHTMLBrowser && this == (HHTMLBrowser)other;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000116CE File Offset: 0x0000FACE
		public override int GetHashCode()
		{
			return this.m_HHTMLBrowser.GetHashCode();
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000116E1 File Offset: 0x0000FAE1
		public static bool operator ==(HHTMLBrowser x, HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000116F3 File Offset: 0x0000FAF3
		public static bool operator !=(HHTMLBrowser x, HHTMLBrowser y)
		{
			return !(x == y);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000116FF File Offset: 0x0000FAFF
		public static explicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser(value);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00011707 File Offset: 0x0000FB07
		public static explicit operator uint(HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00011710 File Offset: 0x0000FB10
		public bool Equals(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00011721 File Offset: 0x0000FB21
		public int CompareTo(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}

		// Token: 0x04000CA5 RID: 3237
		public static readonly HHTMLBrowser Invalid = new HHTMLBrowser(0U);

		// Token: 0x04000CA6 RID: 3238
		public uint m_HHTMLBrowser;
	}
}
