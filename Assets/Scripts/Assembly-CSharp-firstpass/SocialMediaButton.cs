using System;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class SocialMediaButton : MonoBehaviour
{
	// Token: 0x06002095 RID: 8341 RVA: 0x000A0188 File Offset: 0x0009E588
	public void bPress()
	{
		if (!Global.self.NoCurrentTransition)
		{
			return;
		}
		int num = this.URL.IndexOf(".");
		int num2 = this.URL.IndexOf(".", num + 1);
		string website = this.URL.Substring(num + 1, num2 - num - 1);
		AnalyticsComponent.SocialMedia(website);
		Application.OpenURL(this.URL);
	}

	// Token: 0x040023EE RID: 9198
	public string URL;
}
