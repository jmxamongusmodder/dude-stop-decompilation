using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000006 RID: 6
public static class FmodResonanceAudio
{
	// Token: 0x0600000F RID: 15 RVA: 0x000026B8 File Offset: 0x000008B8
	public static void UpdateAudioRoom(FmodResonanceAudioRoom room, bool roomEnabled)
	{
		if (roomEnabled)
		{
			if (!FmodResonanceAudio.enabledRooms.Contains(room))
			{
				FmodResonanceAudio.enabledRooms.Add(room);
			}
		}
		else
		{
			FmodResonanceAudio.enabledRooms.Remove(room);
		}
		if (FmodResonanceAudio.enabledRooms.Count > 0)
		{
			FmodResonanceAudioRoom room2 = FmodResonanceAudio.enabledRooms[FmodResonanceAudio.enabledRooms.Count - 1];
			FmodResonanceAudio.RoomProperties roomProperties = FmodResonanceAudio.GetRoomProperties(room2);
			IntPtr intPtr = Marshal.AllocHGlobal(FmodResonanceAudio.roomPropertiesSize);
			Marshal.StructureToPtr(roomProperties, intPtr, false);
			FmodResonanceAudio.ListenerPlugin.setParameterData(FmodResonanceAudio.roomPropertiesIndex, FmodResonanceAudio.GetBytes(intPtr, FmodResonanceAudio.roomPropertiesSize));
			Marshal.FreeHGlobal(intPtr);
		}
		else
		{
			FmodResonanceAudio.ListenerPlugin.setParameterData(FmodResonanceAudio.roomPropertiesIndex, FmodResonanceAudio.GetBytes(IntPtr.Zero, 0));
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002788 File Offset: 0x00000988
	public static bool IsListenerInsideRoom(FmodResonanceAudioRoom room)
	{
		VECTOR vector;
		RuntimeManager.LowlevelSystem.get3DListenerAttributes(0, out FmodResonanceAudio.listenerPositionFmod, out vector, out vector, out vector);
		Vector3 a = new Vector3(FmodResonanceAudio.listenerPositionFmod.x, FmodResonanceAudio.listenerPositionFmod.y, FmodResonanceAudio.listenerPositionFmod.z);
		Vector3 point = a - room.transform.position;
		Quaternion rotation = Quaternion.Inverse(room.transform.rotation);
		FmodResonanceAudio.bounds.size = Vector3.Scale(room.transform.lossyScale, room.size);
		return FmodResonanceAudio.bounds.Contains(rotation * point);
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000011 RID: 17 RVA: 0x0000282A File Offset: 0x00000A2A
	private static DSP ListenerPlugin
	{
		get
		{
			if (!FmodResonanceAudio.listenerPlugin.hasHandle())
			{
				FmodResonanceAudio.listenerPlugin = FmodResonanceAudio.Initialize();
			}
			return FmodResonanceAudio.listenerPlugin;
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000284A File Offset: 0x00000A4A
	private static float ConvertAmplitudeFromDb(float db)
	{
		return Mathf.Pow(10f, 0.05f * db);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002860 File Offset: 0x00000A60
	private static void ConvertAudioTransformFromUnity(ref Vector3 position, ref Quaternion rotation)
	{
		Matrix4x4 rhs = Matrix4x4.TRS(position, rotation, Vector3.one);
		rhs = FmodResonanceAudio.flipZ * rhs * FmodResonanceAudio.flipZ;
		position = rhs.GetColumn(3);
		rotation = Quaternion.LookRotation(rhs.GetColumn(2), rhs.GetColumn(1));
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000028D4 File Offset: 0x00000AD4
	private static byte[] GetBytes(IntPtr ptr, int length)
	{
		if (ptr != IntPtr.Zero)
		{
			byte[] array = new byte[length];
			Marshal.Copy(ptr, array, 0, length);
			return array;
		}
		return new byte[1];
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000290C File Offset: 0x00000B0C
	private static FmodResonanceAudio.RoomProperties GetRoomProperties(FmodResonanceAudioRoom room)
	{
		Vector3 position = room.transform.position;
		Quaternion rotation = room.transform.rotation;
		Vector3 vector = Vector3.Scale(room.transform.lossyScale, room.size);
		FmodResonanceAudio.ConvertAudioTransformFromUnity(ref position, ref rotation);
		FmodResonanceAudio.RoomProperties result;
		result.positionX = position.x;
		result.positionY = position.y;
		result.positionZ = position.z;
		result.rotationX = rotation.x;
		result.rotationY = rotation.y;
		result.rotationZ = rotation.z;
		result.rotationW = rotation.w;
		result.dimensionsX = vector.x;
		result.dimensionsY = vector.y;
		result.dimensionsZ = vector.z;
		result.materialLeft = room.leftWall;
		result.materialRight = room.rightWall;
		result.materialBottom = room.floor;
		result.materialTop = room.ceiling;
		result.materialFront = room.frontWall;
		result.materialBack = room.backWall;
		result.reverbGain = FmodResonanceAudio.ConvertAmplitudeFromDb(room.reverbGainDb);
		result.reverbTime = room.reverbTime;
		result.reverbBrightness = room.reverbBrightness;
		result.reflectionScalar = room.reflectivity;
		return result;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002A68 File Offset: 0x00000C68
	private static DSP Initialize()
	{
		int num = 0;
		DSP result = default(DSP);
		Bank[] array = null;
		RuntimeManager.StudioSystem.getBankCount(out num);
		RuntimeManager.StudioSystem.getBankList(out array);
		for (int i = 0; i < num; i++)
		{
			int num2 = 0;
			Bus[] array2 = null;
			array[i].getBusCount(out num2);
			array[i].getBusList(out array2);
			RuntimeManager.StudioSystem.flushCommands();
			for (int j = 0; j < num2; j++)
			{
				string path = null;
				array2[j].getPath(out path);
				RuntimeManager.StudioSystem.getBus(path, out array2[j]);
				RuntimeManager.StudioSystem.flushCommands();
				ChannelGroup channelGroup;
				array2[j].getChannelGroup(out channelGroup);
				RuntimeManager.StudioSystem.flushCommands();
				if (channelGroup.hasHandle())
				{
					int num3 = 0;
					channelGroup.getNumDSPs(out num3);
					for (int k = 0; k < num3; k++)
					{
						channelGroup.getDSP(k, out result);
						int num4 = 0;
						uint num5 = 0U;
						string text;
						result.getInfo(out text, out num5, out num4, out num4, out num4);
						if (text.ToString().Equals(FmodResonanceAudio.listenerPluginName) && result.hasHandle())
						{
							return result;
						}
					}
				}
			}
		}
		UnityEngine.Debug.LogError(FmodResonanceAudio.listenerPluginName + " not found in the FMOD project.");
		return result;
	}

	// Token: 0x04000046 RID: 70
	public const float maxGainDb = 24f;

	// Token: 0x04000047 RID: 71
	public const float minGainDb = -24f;

	// Token: 0x04000048 RID: 72
	public const float maxReverbBrightness = 1f;

	// Token: 0x04000049 RID: 73
	public const float minReverbBrightness = -1f;

	// Token: 0x0400004A RID: 74
	public const float maxReverbTime = 3f;

	// Token: 0x0400004B RID: 75
	public const float maxReflectivity = 2f;

	// Token: 0x0400004C RID: 76
	private static readonly Matrix4x4 flipZ = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));

	// Token: 0x0400004D RID: 77
	private static readonly string listenerPluginName = "Resonance Audio Listener";

	// Token: 0x0400004E RID: 78
	private static readonly int roomPropertiesSize = Marshal.SizeOf(typeof(FmodResonanceAudio.RoomProperties));

	// Token: 0x0400004F RID: 79
	private static readonly int roomPropertiesIndex = 1;

	// Token: 0x04000050 RID: 80
	private static Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

	// Token: 0x04000051 RID: 81
	private static List<FmodResonanceAudioRoom> enabledRooms = new List<FmodResonanceAudioRoom>();

	// Token: 0x04000052 RID: 82
	private static VECTOR listenerPositionFmod = default(VECTOR);

	// Token: 0x04000053 RID: 83
	private static DSP listenerPlugin;

	// Token: 0x02000007 RID: 7
	private struct RoomProperties
	{
		// Token: 0x04000054 RID: 84
		public float positionX;

		// Token: 0x04000055 RID: 85
		public float positionY;

		// Token: 0x04000056 RID: 86
		public float positionZ;

		// Token: 0x04000057 RID: 87
		public float rotationX;

		// Token: 0x04000058 RID: 88
		public float rotationY;

		// Token: 0x04000059 RID: 89
		public float rotationZ;

		// Token: 0x0400005A RID: 90
		public float rotationW;

		// Token: 0x0400005B RID: 91
		public float dimensionsX;

		// Token: 0x0400005C RID: 92
		public float dimensionsY;

		// Token: 0x0400005D RID: 93
		public float dimensionsZ;

		// Token: 0x0400005E RID: 94
		public FmodResonanceAudioRoom.SurfaceMaterial materialLeft;

		// Token: 0x0400005F RID: 95
		public FmodResonanceAudioRoom.SurfaceMaterial materialRight;

		// Token: 0x04000060 RID: 96
		public FmodResonanceAudioRoom.SurfaceMaterial materialBottom;

		// Token: 0x04000061 RID: 97
		public FmodResonanceAudioRoom.SurfaceMaterial materialTop;

		// Token: 0x04000062 RID: 98
		public FmodResonanceAudioRoom.SurfaceMaterial materialFront;

		// Token: 0x04000063 RID: 99
		public FmodResonanceAudioRoom.SurfaceMaterial materialBack;

		// Token: 0x04000064 RID: 100
		public float reflectionScalar;

		// Token: 0x04000065 RID: 101
		public float reverbGain;

		// Token: 0x04000066 RID: 102
		public float reverbTime;

		// Token: 0x04000067 RID: 103
		public float reverbBrightness;
	}
}
