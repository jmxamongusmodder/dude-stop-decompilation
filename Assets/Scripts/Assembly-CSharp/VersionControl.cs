using System;
using UnityEngine;

// Token: 0x02000591 RID: 1425
[ExecuteInEditMode]
public class VersionControl : MonoBehaviour
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000A2B60 File Offset: 0x000A0F60
	// (set) Token: 0x060020F9 RID: 8441 RVA: 0x000A2B8B File Offset: 0x000A0F8B
	public static VersionControl self
	{
		get
		{
			if (VersionControl._self == null)
			{
				VersionControl._self = GameObject.FindGameObjectWithTag("Global").GetComponent<VersionControl>();
			}
			return VersionControl._self;
		}
		set
		{
			VersionControl._self = value;
		}
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000A2B93 File Offset: 0x000A0F93
	private void Update()
	{
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000A2B95 File Offset: 0x000A0F95
	public static string GetVersion()
	{
		return (!(VersionControl.self == null)) ? VersionControl.self.version : string.Empty;
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x000A2BBB File Offset: 0x000A0FBB
	public static string GetBuildDate()
	{
		return (!(VersionControl.self == null)) ? VersionControl.self.buildDate : string.Empty;
	}

	// Token: 0x0400244E RID: 9294
	public string version;

	// Token: 0x0400244F RID: 9295
	public bool manualDate;

	// Token: 0x04002450 RID: 9296
	public string buildDate;

	// Token: 0x04002451 RID: 9297
	private static VersionControl _self;
}
