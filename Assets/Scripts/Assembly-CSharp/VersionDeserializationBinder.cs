using System;
using System.Reflection;
using System.Runtime.Serialization;

// Token: 0x02000477 RID: 1143
public sealed class VersionDeserializationBinder : SerializationBinder
{
	// Token: 0x06001D71 RID: 7537 RVA: 0x00081460 File Offset: 0x0007F860
	public override Type BindToType(string assemblyName, string typeName)
	{
		if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
		{
			assemblyName = Assembly.GetExecutingAssembly().FullName;
			return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
		}
		return null;
	}
}
