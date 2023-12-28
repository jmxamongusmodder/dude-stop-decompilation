using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
[AddComponentMenu("GoogleVR/Audio/FmodGvrAudioRoom")]
public class FmodGvrAudioRoom : MonoBehaviour
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002664 File Offset: 0x00000864
	private void OnEnable()
	{
		FmodGvrAudio.UpdateAudioRoom(this, FmodGvrAudio.IsListenerInsideRoom(this));
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002672 File Offset: 0x00000872
	private void OnDisable()
	{
		FmodGvrAudio.UpdateAudioRoom(this, false);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000267B File Offset: 0x0000087B
	private void Update()
	{
		FmodGvrAudio.UpdateAudioRoom(this, FmodGvrAudio.IsListenerInsideRoom(this));
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002689 File Offset: 0x00000889
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireCube(Vector3.zero, this.size);
	}

	// Token: 0x04000023 RID: 35
	public FmodGvrAudioRoom.SurfaceMaterial leftWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x04000024 RID: 36
	public FmodGvrAudioRoom.SurfaceMaterial rightWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x04000025 RID: 37
	public FmodGvrAudioRoom.SurfaceMaterial floor = FmodGvrAudioRoom.SurfaceMaterial.ParquetOnConcrete;

	// Token: 0x04000026 RID: 38
	public FmodGvrAudioRoom.SurfaceMaterial ceiling = FmodGvrAudioRoom.SurfaceMaterial.PlasterRough;

	// Token: 0x04000027 RID: 39
	public FmodGvrAudioRoom.SurfaceMaterial backWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x04000028 RID: 40
	public FmodGvrAudioRoom.SurfaceMaterial frontWall = FmodGvrAudioRoom.SurfaceMaterial.ConcreteBlockCoarse;

	// Token: 0x04000029 RID: 41
	public float reflectivity = 1f;

	// Token: 0x0400002A RID: 42
	public float reverbGainDb;

	// Token: 0x0400002B RID: 43
	public float reverbBrightness;

	// Token: 0x0400002C RID: 44
	public float reverbTime = 1f;

	// Token: 0x0400002D RID: 45
	public Vector3 size = Vector3.one;

	// Token: 0x02000005 RID: 5
	public enum SurfaceMaterial
	{
		// Token: 0x0400002F RID: 47
		Transparent,
		// Token: 0x04000030 RID: 48
		AcousticCeilingTiles,
		// Token: 0x04000031 RID: 49
		BrickBare,
		// Token: 0x04000032 RID: 50
		BrickPainted,
		// Token: 0x04000033 RID: 51
		ConcreteBlockCoarse,
		// Token: 0x04000034 RID: 52
		ConcreteBlockPainted,
		// Token: 0x04000035 RID: 53
		CurtainHeavy,
		// Token: 0x04000036 RID: 54
		FiberglassInsulation,
		// Token: 0x04000037 RID: 55
		GlassThin,
		// Token: 0x04000038 RID: 56
		GlassThick,
		// Token: 0x04000039 RID: 57
		Grass,
		// Token: 0x0400003A RID: 58
		LinoleumOnConcrete,
		// Token: 0x0400003B RID: 59
		Marble,
		// Token: 0x0400003C RID: 60
		Metal,
		// Token: 0x0400003D RID: 61
		ParquetOnConcrete,
		// Token: 0x0400003E RID: 62
		PlasterRough,
		// Token: 0x0400003F RID: 63
		PlasterSmooth,
		// Token: 0x04000040 RID: 64
		PlywoodPanel,
		// Token: 0x04000041 RID: 65
		PolishedConcreteOrTile,
		// Token: 0x04000042 RID: 66
		Sheetrock,
		// Token: 0x04000043 RID: 67
		WaterOrIceSurface,
		// Token: 0x04000044 RID: 68
		WoodCeiling,
		// Token: 0x04000045 RID: 69
		WoodPanel
	}
}
