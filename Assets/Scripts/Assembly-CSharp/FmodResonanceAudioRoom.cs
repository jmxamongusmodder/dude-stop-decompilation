using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[AddComponentMenu("ResonanceAudio/FmodResonanceAudioRoom")]
public class FmodResonanceAudioRoom : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x00002CCC File Offset: 0x00000ECC
	private void OnEnable()
	{
		FmodResonanceAudio.UpdateAudioRoom(this, FmodResonanceAudio.IsListenerInsideRoom(this));
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002CDA File Offset: 0x00000EDA
	private void OnDisable()
	{
		FmodResonanceAudio.UpdateAudioRoom(this, false);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002CE3 File Offset: 0x00000EE3
	private void Update()
	{
		FmodResonanceAudio.UpdateAudioRoom(this, FmodResonanceAudio.IsListenerInsideRoom(this));
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002CF1 File Offset: 0x00000EF1
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.zero, this.size);
	}

	// Token: 0x04000068 RID: 104
	public FmodResonanceAudioRoom.SurfaceMaterial leftWall = FmodResonanceAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x04000069 RID: 105
	public FmodResonanceAudioRoom.SurfaceMaterial rightWall = FmodResonanceAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x0400006A RID: 106
	public FmodResonanceAudioRoom.SurfaceMaterial floor = FmodResonanceAudioRoom.SurfaceMaterial.ParquetOnConcrete;

	// Token: 0x0400006B RID: 107
	public FmodResonanceAudioRoom.SurfaceMaterial ceiling = FmodResonanceAudioRoom.SurfaceMaterial.PlasterRough;

	// Token: 0x0400006C RID: 108
	public FmodResonanceAudioRoom.SurfaceMaterial backWall = FmodResonanceAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x0400006D RID: 109
	public FmodResonanceAudioRoom.SurfaceMaterial frontWall = FmodResonanceAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x0400006E RID: 110
	public float reflectivity = 1f;

	// Token: 0x0400006F RID: 111
	public float reverbGainDb;

	// Token: 0x04000070 RID: 112
	public float reverbBrightness;

	// Token: 0x04000071 RID: 113
	public float reverbTime = 1f;

	// Token: 0x04000072 RID: 114
	public Vector3 size = Vector3.one;

	// Token: 0x02000009 RID: 9
	public enum SurfaceMaterial
	{
		// Token: 0x04000074 RID: 116
		Transparent,
		// Token: 0x04000075 RID: 117
		AcousticCeilingTiles,
		// Token: 0x04000076 RID: 118
		BrickBare,
		// Token: 0x04000077 RID: 119
		BrickPainted,
		// Token: 0x04000078 RID: 120
		ConcreteBlockCoarse,
		// Token: 0x04000079 RID: 121
		ConcreteBlockPainted,
		// Token: 0x0400007A RID: 122
		CurtainHeavy,
		// Token: 0x0400007B RID: 123
		FiberglassInsulation,
		// Token: 0x0400007C RID: 124
		GlassThin,
		// Token: 0x0400007D RID: 125
		GlassThick,
		// Token: 0x0400007E RID: 126
		Grass,
		// Token: 0x0400007F RID: 127
		LinoleumOnConcrete,
		// Token: 0x04000080 RID: 128
		Marble,
		// Token: 0x04000081 RID: 129
		Metal,
		// Token: 0x04000082 RID: 130
		ParquetOnConcrete,
		// Token: 0x04000083 RID: 131
		PlasterRough,
		// Token: 0x04000084 RID: 132
		PlasterSmooth,
		// Token: 0x04000085 RID: 133
		PlywoodPanel,
		// Token: 0x04000086 RID: 134
		PolishedConcreteOrTile,
		// Token: 0x04000087 RID: 135
		Sheetrock,
		// Token: 0x04000088 RID: 136
		WaterOrIceSurface,
		// Token: 0x04000089 RID: 137
		WoodCeiling,
		// Token: 0x0400008A RID: 138
		WoodPanel
	}
}
