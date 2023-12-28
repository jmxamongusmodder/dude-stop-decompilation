using System;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x02000020 RID: 32
	[AddComponentMenu("FMOD Studio/FMOD Studio Listener")]
	public class StudioListener : MonoBehaviour
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000044E4 File Offset: 0x000028E4
		private void OnEnable()
		{
			RuntimeUtils.EnforceLibraryOrder();
			this.rigidBody = base.gameObject.GetComponent<Rigidbody>();
			this.rigidBody2D = base.gameObject.GetComponent<Rigidbody2D>();
			RuntimeManager.HasListener[this.ListenerNumber] = true;
			this.SetListenerLocation();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004520 File Offset: 0x00002920
		private void OnDisable()
		{
			RuntimeManager.HasListener[this.ListenerNumber] = false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000452F File Offset: 0x0000292F
		private void Update()
		{
			this.SetListenerLocation();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004538 File Offset: 0x00002938
		private void SetListenerLocation()
		{
			if (this.rigidBody)
			{
				RuntimeManager.SetListenerLocation(this.ListenerNumber, base.gameObject, this.rigidBody);
			}
			else
			{
				RuntimeManager.SetListenerLocation(this.ListenerNumber, base.gameObject, this.rigidBody2D);
			}
		}

		// Token: 0x0400008E RID: 142
		private Rigidbody rigidBody;

		// Token: 0x0400008F RID: 143
		private Rigidbody2D rigidBody2D;

		// Token: 0x04000090 RID: 144
		public int ListenerNumber;
	}
}
