using System;

namespace Steamworks
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	public struct ControllerDigitalActionHandle_t : IEquatable<ControllerDigitalActionHandle_t>, IComparable<ControllerDigitalActionHandle_t>
	{
		// Token: 0x06000E80 RID: 3712 RVA: 0x00011498 File Offset: 0x0000F898
		public ControllerDigitalActionHandle_t(ulong value)
		{
			this.m_ControllerDigitalActionHandle = value;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000114A1 File Offset: 0x0000F8A1
		public override string ToString()
		{
			return this.m_ControllerDigitalActionHandle.ToString();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000114B4 File Offset: 0x0000F8B4
		public override bool Equals(object other)
		{
			return other is ControllerDigitalActionHandle_t && this == (ControllerDigitalActionHandle_t)other;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000114D5 File Offset: 0x0000F8D5
		public override int GetHashCode()
		{
			return this.m_ControllerDigitalActionHandle.GetHashCode();
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000114E8 File Offset: 0x0000F8E8
		public static bool operator ==(ControllerDigitalActionHandle_t x, ControllerDigitalActionHandle_t y)
		{
			return x.m_ControllerDigitalActionHandle == y.m_ControllerDigitalActionHandle;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000114FA File Offset: 0x0000F8FA
		public static bool operator !=(ControllerDigitalActionHandle_t x, ControllerDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00011506 File Offset: 0x0000F906
		public static explicit operator ControllerDigitalActionHandle_t(ulong value)
		{
			return new ControllerDigitalActionHandle_t(value);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0001150E File Offset: 0x0000F90E
		public static explicit operator ulong(ControllerDigitalActionHandle_t that)
		{
			return that.m_ControllerDigitalActionHandle;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00011517 File Offset: 0x0000F917
		public bool Equals(ControllerDigitalActionHandle_t other)
		{
			return this.m_ControllerDigitalActionHandle == other.m_ControllerDigitalActionHandle;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00011528 File Offset: 0x0000F928
		public int CompareTo(ControllerDigitalActionHandle_t other)
		{
			return this.m_ControllerDigitalActionHandle.CompareTo(other.m_ControllerDigitalActionHandle);
		}

		// Token: 0x04000CA1 RID: 3233
		public ulong m_ControllerDigitalActionHandle;
	}
}
