using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000002 RID: 2
public static class FmodGvrAudio
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static void UpdateAudioRoom(FmodGvrAudioRoom room, bool roomEnabled)
	{
		if (roomEnabled)
		{
			if (!FmodGvrAudio.enabledRooms.Contains(room))
			{
				FmodGvrAudio.enabledRooms.Add(room);
			}
		}
		else
		{
			FmodGvrAudio.enabledRooms.Remove(room);
		}
		if (FmodGvrAudio.enabledRooms.Count > 0)
		{
			FmodGvrAudioRoom room2 = FmodGvrAudio.enabledRooms[FmodGvrAudio.enabledRooms.Count - 1];
			FmodGvrAudio.RoomProperties roomProperties = FmodGvrAudio.GetRoomProperties(room2);
			IntPtr intPtr = Marshal.AllocHGlobal(FmodGvrAudio.roomPropertiesSize);
			Marshal.StructureToPtr(roomProperties, intPtr, false);
			FmodGvrAudio.ListenerPlugin.setParameterData(FmodGvrAudio.roomPropertiesIndex, FmodGvrAudio.GetBytes(intPtr, FmodGvrAudio.roomPropertiesSize));
			Marshal.FreeHGlobal(intPtr);
		}
		else
		{
			FmodGvrAudio.ListenerPlugin.setParameterData(FmodGvrAudio.roomPropertiesIndex, FmodGvrAudio.GetBytes(IntPtr.Zero, 0));
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002120 File Offset: 0x00000320
	public static bool IsListenerInsideRoom(FmodGvrAudioRoom room)
	{
		VECTOR vector;
		RuntimeManager.LowlevelSystem.get3DListenerAttributes(0, out FmodGvrAudio.listenerPositionFmod, out vector, out vector, out vector);
		Vector3 a = new Vector3(FmodGvrAudio.listenerPositionFmod.x, FmodGvrAudio.listenerPositionFmod.y, FmodGvrAudio.listenerPositionFmod.z);
		Vector3 point = a - room.transform.position;
		Quaternion rotation = Quaternion.Inverse(room.transform.rotation);
		FmodGvrAudio.bounds.size = Vector3.Scale(room.transform.lossyScale, room.size);
		return FmodGvrAudio.bounds.Contains(rotation * point);
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000003 RID: 3 RVA: 0x000021C2 File Offset: 0x000003C2
	private static DSP ListenerPlugin
	{
		get
		{
			if (!FmodGvrAudio.listenerPlugin.hasHandle())
			{
				FmodGvrAudio.listenerPlugin = FmodGvrAudio.Initialize();
			}
			return FmodGvrAudio.listenerPlugin;
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000021E2 File Offset: 0x000003E2
	private static float ConvertAmplitudeFromDb(float db)
	{
		return Mathf.Pow(10f, 0.05f * db);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000021F8 File Offset: 0x000003F8
	private static void ConvertAudioTransformFromUnity(ref Vector3 position, ref Quaternion rotation)
	{
		Matrix4x4 rhs = Matrix4x4.TRS(position, rotation, Vector3.one);
		rhs = FmodGvrAudio.flipZ * rhs * FmodGvrAudio.flipZ;
		position = rhs.GetColumn(3);
		rotation = Quaternion.LookRotation(rhs.GetColumn(2), rhs.GetColumn(1));
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000226C File Offset: 0x0000046C
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

	// Token: 0x06000007 RID: 7 RVA: 0x000022A4 File Offset: 0x000004A4
	private static FmodGvrAudio.RoomProperties GetRoomProperties(FmodGvrAudioRoom room)
	{
		Vector3 position = room.transform.position;
		Quaternion rotation = room.transform.rotation;
		Vector3 vector = Vector3.Scale(room.transform.lossyScale, room.size);
		FmodGvrAudio.ConvertAudioTransformFromUnity(ref position, ref rotation);
		FmodGvrAudio.RoomProperties result;
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
		result.reverbGain = FmodGvrAudio.ConvertAmplitudeFromDb(room.reverbGainDb);
		result.reverbTime = room.reverbTime;
		result.reverbBrightness = room.reverbBrightness;
		result.reflectionScalar = room.reflectivity;
		return result;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002400 File Offset: 0x00000600
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
						if (text.ToString().Equals(FmodGvrAudio.listenerPluginName) && result.hasHandle())
						{
							return result;
						}
					}
				}
			}
		}
		UnityEngine.Debug.LogError(FmodGvrAudio.listenerPluginName + " not found in the FMOD project.");
		return result;
	}

	// Token: 0x04000001 RID: 1
	public const float maxGainDb = 24f;

	// Token: 0x04000002 RID: 2
	public const float minGainDb = -24f;

	// Token: 0x04000003 RID: 3
	public const float maxReverbBrightness = 1f;

	// Token: 0x04000004 RID: 4
	public const float minReverbBrightness = -1f;

	// Token: 0x04000005 RID: 5
	public const float maxReverbTime = 3f;

	// Token: 0x04000006 RID: 6
	public const float maxReflectivity = 2f;

	// Token: 0x04000007 RID: 7
	private static readonly Matrix4x4 flipZ = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));

	// Token: 0x04000008 RID: 8
	private static readonly string listenerPluginName = "Google GVR Listener";

	// Token: 0x04000009 RID: 9
	private static readonly int roomPropertiesSize = Marshal.SizeOf(typeof(FmodGvrAudio.RoomProperties));

	// Token: 0x0400000A RID: 10
	private static readonly int roomPropertiesIndex = 1;

	// Token: 0x0400000B RID: 11
	private static Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

	// Token: 0x0400000C RID: 12
	private static List<FmodGvrAudioRoom> enabledRooms = new List<FmodGvrAudioRoom>();

	// Token: 0x0400000D RID: 13
	private static VECTOR listenerPositionFmod = default(VECTOR);

	// Token: 0x0400000E RID: 14
	private static DSP listenerPlugin;

	// Token: 0x02000003 RID: 3
	private struct RoomProperties
	{
		// Token: 0x0400000F RID: 15
		public float positionX;

		// Token: 0x04000010 RID: 16
		public float positionY;

		// Token: 0x04000011 RID: 17
		public float positionZ;

		// Token: 0x04000012 RID: 18
		public float rotationX;

		// Token: 0x04000013 RID: 19
		public float rotationY;

		// Token: 0x04000014 RID: 20
		public float rotationZ;

		// Token: 0x04000015 RID: 21
		public float rotationW;

		// Token: 0x04000016 RID: 22
		public float dimensionsX;

		// Token: 0x04000017 RID: 23
		public float dimensionsY;

		// Token: 0x04000018 RID: 24
		public float dimensionsZ;

		// Token: 0x04000019 RID: 25
		public FmodGvrAudioRoom.SurfaceMaterial materialLeft;

		// Token: 0x0400001A RID: 26
		public FmodGvrAudioRoom.SurfaceMaterial materialRight;

		// Token: 0x0400001B RID: 27
		public FmodGvrAudioRoom.SurfaceMaterial materialBottom;

		// Token: 0x0400001C RID: 28
		public FmodGvrAudioRoom.SurfaceMaterial materialTop;

		// Token: 0x0400001D RID: 29
		public FmodGvrAudioRoom.SurfaceMaterial materialFront;

		// Token: 0x0400001E RID: 30
		public FmodGvrAudioRoom.SurfaceMaterial materialBack;

		// Token: 0x0400001F RID: 31
		public float reflectionScalar;

		// Token: 0x04000020 RID: 32
		public float reverbGain;

		// Token: 0x04000021 RID: 33
		public float reverbTime;

		// Token: 0x04000022 RID: 34
		public float reverbBrightness;
	}
}
