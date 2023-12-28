using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	// Token: 0x0200038E RID: 910
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("")]
	public class ImageEffectBase : MonoBehaviour
	{
		// Token: 0x0600169D RID: 5789 RVA: 0x0004595E File Offset: 0x00043D5E
		protected virtual void Start()
		{
			if (!SystemInfo.supportsImageEffects)
			{
				base.enabled = false;
				return;
			}
			if (!this.shader || !this.shader.isSupported)
			{
				base.enabled = false;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00045999 File Offset: 0x00043D99
		protected Material material
		{
			get
			{
				if (this.m_Material == null)
				{
					this.m_Material = new Material(this.shader);
					this.m_Material.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_Material;
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000459D0 File Offset: 0x00043DD0
		protected virtual void OnDisable()
		{
			if (this.m_Material)
			{
				UnityEngine.Object.DestroyImmediate(this.m_Material);
			}
		}

		// Token: 0x0400146D RID: 5229
		public Shader shader;

		// Token: 0x0400146E RID: 5230
		private Material m_Material;
	}
}
