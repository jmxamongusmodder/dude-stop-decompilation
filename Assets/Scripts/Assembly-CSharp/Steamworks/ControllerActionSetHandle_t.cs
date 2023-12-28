using System;

namespace Steamworks
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	public struct ControllerActionSetHandle_t : IEquatable<ControllerActionSetHandle_t>, IComparable<ControllerActionSetHandle_t>
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x00011350 File Offset: 0x0000F750
		public ControllerActionSetHandle_t(ulong value)
		{
			this.m_ControllerActionSetHandle = value;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00011359 File Offset: 0x0000F759
		public override string ToString()
		{
			return this.m_ControllerActionSetHandle.ToString();
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0001136C File Offset: 0x0000F76C
		public override bool Equals(object other)
		{
			return other is ControllerActionSetHandle_t && this == (ControllerActionSetHandle_t)other;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0001138D File Offset: 0x0000F78D
		public override int GetHashCode()
		{
			return this.m_ControllerActionSetHandle.GetHashCode();
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000113A0 File Offset: 0x0000F7A0
		public static bool operator ==(ControllerActionSetHandle_t x, ControllerActionSetHandle_t y)
		{
			return x.m_ControllerActionSetHandle == y.m_ControllerActionSetHandle;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000113B2 File Offset: 0x0000F7B2
		public static bool operator !=(ControllerActionSetHandle_t x, ControllerActionSetHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000113BE File Offset: 0x0000F7BE
		public static explicit operator ControllerActionSetHandle_t(ulong value)
		{
			return new ControllerActionSetHandle_t(value);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000113C6 File Offset: 0x0000F7C6
		public static explicit operator ulong(ControllerActionSetHandle_t that)
		{
			return that.m_ControllerActionSetHandle;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000113CF File Offset: 0x0000F7CF
		public bool Equals(ControllerActionSetHandle_t other)
		{
			return this.m_ControllerActionSetHandle == other.m_ControllerActionSetHandle;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000113E0 File Offset: 0x0000F7E0
		public int CompareTo(ControllerActionSetHandle_t other)
		{
			return this.m_ControllerActionSetHandle.CompareTo(other.m_ControllerActionSetHandle);
		}

		// Token: 0x04000C9F RID: 3231
		public ulong m_ControllerActionSetHandle;
	}
}
