using System;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x0200001E RID: 30
	[AddComponentMenu("FMOD Studio/FMOD Studio Bank Loader")]
	public class StudioBankLoader : MonoBehaviour
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003E0C File Offset: 0x0000220C
		private void HandleGameEvent(LoaderGameEvent gameEvent)
		{
			if (this.LoadEvent == gameEvent)
			{
				this.Load();
			}
			if (this.UnloadEvent == gameEvent)
			{
				this.Unload();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E32 File Offset: 0x00002232
		private void Start()
		{
			RuntimeUtils.EnforceLibraryOrder();
			this.HandleGameEvent(LoaderGameEvent.ObjectStart);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E40 File Offset: 0x00002240
		private void OnApplicationQuit()
		{
			this.isQuitting = true;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E49 File Offset: 0x00002249
		private void OnDestroy()
		{
			if (!this.isQuitting)
			{
				this.HandleGameEvent(LoaderGameEvent.ObjectDestroy);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E5D File Offset: 0x0000225D
		private void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(LoaderGameEvent.TriggerEnter);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003E87 File Offset: 0x00002287
		private void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(LoaderGameEvent.TriggerExit);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003EB1 File Offset: 0x000022B1
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(LoaderGameEvent.TriggerEnter2D);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003EDB File Offset: 0x000022DB
		private void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(LoaderGameEvent.TriggerExit2D);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F08 File Offset: 0x00002308
		public void Load()
		{
			foreach (string bankName in this.Banks)
			{
				try
				{
					RuntimeManager.LoadBank(bankName, this.PreloadSamples);
				}
				catch (BankLoadException exception)
				{
					Debug.LogException(exception);
				}
			}
			RuntimeManager.WaitForAllLoads();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003F8C File Offset: 0x0000238C
		public void Unload()
		{
			foreach (string bankName in this.Banks)
			{
				RuntimeManager.UnloadBank(bankName);
			}
		}

		// Token: 0x04000079 RID: 121
		public LoaderGameEvent LoadEvent;

		// Token: 0x0400007A RID: 122
		public LoaderGameEvent UnloadEvent;

		// Token: 0x0400007B RID: 123
		[BankRef]
		public List<string> Banks;

		// Token: 0x0400007C RID: 124
		public string CollisionTag;

		// Token: 0x0400007D RID: 125
		public bool PreloadSamples;

		// Token: 0x0400007E RID: 126
		private bool isQuitting;
	}
}
