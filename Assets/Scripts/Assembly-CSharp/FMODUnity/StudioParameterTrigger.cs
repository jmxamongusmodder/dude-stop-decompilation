using System;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x02000022 RID: 34
	[AddComponentMenu("FMOD Studio/FMOD Studio Parameter Trigger")]
	public class StudioParameterTrigger : MonoBehaviour
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00004598 File Offset: 0x00002998
		private void Start()
		{
			this.HandleGameEvent(EmitterGameEvent.ObjectStart);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000045A1 File Offset: 0x000029A1
		private void OnDestroy()
		{
			this.HandleGameEvent(EmitterGameEvent.ObjectDestroy);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000045AA File Offset: 0x000029AA
		private void OnEnable()
		{
			this.HandleGameEvent(EmitterGameEvent.ObjectEnable);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000045B4 File Offset: 0x000029B4
		private void OnDisable()
		{
			this.HandleGameEvent(EmitterGameEvent.ObjectDisable);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000045BE File Offset: 0x000029BE
		private void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerEnter);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000045E8 File Offset: 0x000029E8
		private void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerExit);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004612 File Offset: 0x00002A12
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerEnter2D);
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000463C File Offset: 0x00002A3C
		private void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerExit2D);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004666 File Offset: 0x00002A66
		private void OnCollisionEnter()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionEnter);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000466F File Offset: 0x00002A6F
		private void OnCollisionExit()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionExit);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004678 File Offset: 0x00002A78
		private void OnCollisionEnter2D()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionEnter2D);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004682 File Offset: 0x00002A82
		private void OnCollisionExit2D()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionExit2D);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000468C File Offset: 0x00002A8C
		private void HandleGameEvent(EmitterGameEvent gameEvent)
		{
			if (this.TriggerEvent == gameEvent)
			{
				this.TriggerParameters();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000046A0 File Offset: 0x00002AA0
		public void TriggerParameters()
		{
			for (int i = 0; i < this.Emitters.Length; i++)
			{
				EmitterRef emitterRef = this.Emitters[i];
				if (emitterRef.Target != null)
				{
					for (int j = 0; j < this.Emitters[i].Params.Length; j++)
					{
						emitterRef.Target.SetParameter(this.Emitters[i].Params[j].Name, this.Emitters[i].Params[j].Value);
					}
				}
			}
		}

		// Token: 0x04000093 RID: 147
		public EmitterRef[] Emitters;

		// Token: 0x04000094 RID: 148
		public EmitterGameEvent TriggerEvent;

		// Token: 0x04000095 RID: 149
		public string CollisionTag;
	}
}
