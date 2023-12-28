using System;

namespace FMOD.Studio
{
	// Token: 0x020000E7 RID: 231
	public struct USER_PROPERTY
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x000075C0 File Offset: 0x000059C0
		public int intValue()
		{
			return (this.type != USER_PROPERTY_TYPE.INTEGER) ? -1 : this.value.intvalue;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000075DE File Offset: 0x000059DE
		public bool boolValue()
		{
			return this.type == USER_PROPERTY_TYPE.BOOLEAN && this.value.boolvalue;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000075FD File Offset: 0x000059FD
		public float floatValue()
		{
			return (this.type != USER_PROPERTY_TYPE.FLOAT) ? -1f : this.value.floatvalue;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00007620 File Offset: 0x00005A20
		public string stringValue()
		{
			return (this.type != USER_PROPERTY_TYPE.STRING) ? string.Empty : this.value.stringvalue;
		}

		// Token: 0x0400049E RID: 1182
		public StringWrapper name;

		// Token: 0x0400049F RID: 1183
		public USER_PROPERTY_TYPE type;

		// Token: 0x040004A0 RID: 1184
		private Union_IntBoolFloatString value;
	}
}
