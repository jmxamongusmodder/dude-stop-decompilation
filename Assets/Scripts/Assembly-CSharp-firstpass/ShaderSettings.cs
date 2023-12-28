using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047B RID: 1147
[ExecuteInEditMode]
public class ShaderSettings : MonoBehaviour
{
	// Token: 0x06001D87 RID: 7559 RVA: 0x00081950 File Offset: 0x0007FD50
	private void Awake()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		Material material = base.GetComponent<Renderer>().material;
		foreach (ShaderSettings.Setting setting in this.settings)
		{
			material.SetFloat(setting.name, setting.value);
		}
	}

	// Token: 0x04001C41 RID: 7233
	public List<ShaderSettings.Setting> settings = new List<ShaderSettings.Setting>();

	// Token: 0x04001C42 RID: 7234
	private bool import;

	// Token: 0x0200047C RID: 1148
	[Serializable]
	public class Setting
	{
		// Token: 0x06001D88 RID: 7560 RVA: 0x000819D0 File Offset: 0x0007FDD0
		public Setting(Shader s, int i)
		{
			this.order = i;
		}

		// Token: 0x04001C43 RID: 7235
		public string name;

		// Token: 0x04001C44 RID: 7236
		public string description;

		// Token: 0x04001C45 RID: 7237
		public float value;

		// Token: 0x04001C46 RID: 7238
		public int order;

		// Token: 0x04001C47 RID: 7239
		public float min;

		// Token: 0x04001C48 RID: 7240
		public float max;

		// Token: 0x04001C49 RID: 7241
		public float def;

		// Token: 0x04001C4A RID: 7242
		public Color color;
	}
}
