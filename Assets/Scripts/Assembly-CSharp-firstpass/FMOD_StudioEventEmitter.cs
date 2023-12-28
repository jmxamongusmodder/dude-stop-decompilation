using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[AddComponentMenu("")]
public class FMOD_StudioEventEmitter : MonoBehaviour
{
	// Token: 0x04000002 RID: 2
	[Header("This component is obsolete. Use FMODUnity.StudioEventEmitter instead")]
	public FMODAsset asset;

	// Token: 0x04000003 RID: 3
	public string path = string.Empty;

	// Token: 0x04000004 RID: 4
	public bool startEventOnAwake = true;
}
