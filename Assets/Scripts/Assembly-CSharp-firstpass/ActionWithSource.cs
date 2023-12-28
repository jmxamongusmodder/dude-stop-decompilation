using System;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class ActionWithSource
{
	// Token: 0x06001391 RID: 5009 RVA: 0x00030544 File Offset: 0x0002E944
	public ActionWithSource(UnityEngine.Object source, Action<VoiceLine, string> actionString)
	{
		this.actionString = actionString;
		this.source = source;
		this.action = null;
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x00030561 File Offset: 0x0002E961
	public ActionWithSource(UnityEngine.Object source, Action<VoiceLine> action)
	{
		this.action = action;
		this.source = source;
		this.actionString = null;
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x00030580 File Offset: 0x0002E980
	public void runAction(VoiceLine voice, string name = null)
	{
		if (this.source == null)
		{
			return;
		}
		if (name == null)
		{
			if (this.action != null)
			{
				this.action(voice);
			}
		}
		else if (this.actionString != null)
		{
			this.actionString(voice, name);
		}
	}

	// Token: 0x0400105E RID: 4190
	public readonly Action<VoiceLine, string> actionString;

	// Token: 0x0400105F RID: 4191
	public readonly Action<VoiceLine> action;

	// Token: 0x04001060 RID: 4192
	public readonly UnityEngine.Object source;
}
