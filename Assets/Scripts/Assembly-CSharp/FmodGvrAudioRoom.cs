using UnityEngine;

public class FmodGvrAudioRoom : MonoBehaviour
{
	public enum SurfaceMaterial
	{
		Transparent = 0,
		AcousticCeilingTiles = 1,
		BrickBare = 2,
		BrickPainted = 3,
		ConcreteBlockCoarse = 4,
		ConcreteBlockPainted = 5,
		CurtainHeavy = 6,
		FiberglassInsulation = 7,
		GlassThin = 8,
		GlassThick = 9,
		Grass = 10,
		LinoleumOnConcrete = 11,
		Marble = 12,
		Metal = 13,
		ParquetOnConcrete = 14,
		PlasterRough = 15,
		PlasterSmooth = 16,
		PlywoodPanel = 17,
		PolishedConcreteOrTile = 18,
		Sheetrock = 19,
		WaterOrIceSurface = 20,
		WoodCeiling = 21,
		WoodPanel = 22,
	}

	public SurfaceMaterial leftWall;
	public SurfaceMaterial rightWall;
	public SurfaceMaterial floor;
	public SurfaceMaterial ceiling;
	public SurfaceMaterial backWall;
	public SurfaceMaterial frontWall;
	public float reflectivity;
	public float reverbGainDb;
	public float reverbBrightness;
	public float reverbTime;
	public Vector3 size;
}
