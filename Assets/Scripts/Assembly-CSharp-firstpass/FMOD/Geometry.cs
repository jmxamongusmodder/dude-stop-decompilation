using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	// Token: 0x02000067 RID: 103
	public struct Geometry
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00006CAD File Offset: 0x000050AD
		public RESULT release()
		{
			return Geometry.FMOD5_Geometry_Release(this.handle);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00006CBA File Offset: 0x000050BA
		public RESULT addPolygon(float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex)
		{
			return Geometry.FMOD5_Geometry_AddPolygon(this.handle, directocclusion, reverbocclusion, doublesided, numvertices, vertices, out polygonindex);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00006CD0 File Offset: 0x000050D0
		public RESULT getNumPolygons(out int numpolygons)
		{
			return Geometry.FMOD5_Geometry_GetNumPolygons(this.handle, out numpolygons);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00006CDE File Offset: 0x000050DE
		public RESULT getMaxPolygons(out int maxpolygons, out int maxvertices)
		{
			return Geometry.FMOD5_Geometry_GetMaxPolygons(this.handle, out maxpolygons, out maxvertices);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00006CED File Offset: 0x000050ED
		public RESULT getPolygonNumVertices(int index, out int numvertices)
		{
			return Geometry.FMOD5_Geometry_GetPolygonNumVertices(this.handle, index, out numvertices);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00006CFC File Offset: 0x000050FC
		public RESULT setPolygonVertex(int index, int vertexindex, ref VECTOR vertex)
		{
			return Geometry.FMOD5_Geometry_SetPolygonVertex(this.handle, index, vertexindex, ref vertex);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00006D0C File Offset: 0x0000510C
		public RESULT getPolygonVertex(int index, int vertexindex, out VECTOR vertex)
		{
			return Geometry.FMOD5_Geometry_GetPolygonVertex(this.handle, index, vertexindex, out vertex);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00006D1C File Offset: 0x0000511C
		public RESULT setPolygonAttributes(int index, float directocclusion, float reverbocclusion, bool doublesided)
		{
			return Geometry.FMOD5_Geometry_SetPolygonAttributes(this.handle, index, directocclusion, reverbocclusion, doublesided);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00006D2E File Offset: 0x0000512E
		public RESULT getPolygonAttributes(int index, out float directocclusion, out float reverbocclusion, out bool doublesided)
		{
			return Geometry.FMOD5_Geometry_GetPolygonAttributes(this.handle, index, out directocclusion, out reverbocclusion, out doublesided);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00006D40 File Offset: 0x00005140
		public RESULT setActive(bool active)
		{
			return Geometry.FMOD5_Geometry_SetActive(this.handle, active);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00006D4E File Offset: 0x0000514E
		public RESULT getActive(out bool active)
		{
			return Geometry.FMOD5_Geometry_GetActive(this.handle, out active);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00006D5C File Offset: 0x0000515C
		public RESULT setRotation(ref VECTOR forward, ref VECTOR up)
		{
			return Geometry.FMOD5_Geometry_SetRotation(this.handle, ref forward, ref up);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00006D6B File Offset: 0x0000516B
		public RESULT getRotation(out VECTOR forward, out VECTOR up)
		{
			return Geometry.FMOD5_Geometry_GetRotation(this.handle, out forward, out up);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00006D7A File Offset: 0x0000517A
		public RESULT setPosition(ref VECTOR position)
		{
			return Geometry.FMOD5_Geometry_SetPosition(this.handle, ref position);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00006D88 File Offset: 0x00005188
		public RESULT getPosition(out VECTOR position)
		{
			return Geometry.FMOD5_Geometry_GetPosition(this.handle, out position);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00006D96 File Offset: 0x00005196
		public RESULT setScale(ref VECTOR scale)
		{
			return Geometry.FMOD5_Geometry_SetScale(this.handle, ref scale);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00006DA4 File Offset: 0x000051A4
		public RESULT getScale(out VECTOR scale)
		{
			return Geometry.FMOD5_Geometry_GetScale(this.handle, out scale);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00006DB2 File Offset: 0x000051B2
		public RESULT save(IntPtr data, out int datasize)
		{
			return Geometry.FMOD5_Geometry_Save(this.handle, data, out datasize);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00006DC1 File Offset: 0x000051C1
		public RESULT setUserData(IntPtr userdata)
		{
			return Geometry.FMOD5_Geometry_SetUserData(this.handle, userdata);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00006DCF File Offset: 0x000051CF
		public RESULT getUserData(out IntPtr userdata)
		{
			return Geometry.FMOD5_Geometry_GetUserData(this.handle, out userdata);
		}

		// Token: 0x06000425 RID: 1061
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_Release(IntPtr geometry);

		// Token: 0x06000426 RID: 1062
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_AddPolygon(IntPtr geometry, float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex);

		// Token: 0x06000427 RID: 1063
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetNumPolygons(IntPtr geometry, out int numpolygons);

		// Token: 0x06000428 RID: 1064
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetMaxPolygons(IntPtr geometry, out int maxpolygons, out int maxvertices);

		// Token: 0x06000429 RID: 1065
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetPolygonNumVertices(IntPtr geometry, int index, out int numvertices);

		// Token: 0x0600042A RID: 1066
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetPolygonVertex(IntPtr geometry, int index, int vertexindex, ref VECTOR vertex);

		// Token: 0x0600042B RID: 1067
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetPolygonVertex(IntPtr geometry, int index, int vertexindex, out VECTOR vertex);

		// Token: 0x0600042C RID: 1068
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetPolygonAttributes(IntPtr geometry, int index, float directocclusion, float reverbocclusion, bool doublesided);

		// Token: 0x0600042D RID: 1069
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetPolygonAttributes(IntPtr geometry, int index, out float directocclusion, out float reverbocclusion, out bool doublesided);

		// Token: 0x0600042E RID: 1070
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetActive(IntPtr geometry, bool active);

		// Token: 0x0600042F RID: 1071
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetActive(IntPtr geometry, out bool active);

		// Token: 0x06000430 RID: 1072
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetRotation(IntPtr geometry, ref VECTOR forward, ref VECTOR up);

		// Token: 0x06000431 RID: 1073
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetRotation(IntPtr geometry, out VECTOR forward, out VECTOR up);

		// Token: 0x06000432 RID: 1074
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetPosition(IntPtr geometry, ref VECTOR position);

		// Token: 0x06000433 RID: 1075
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetPosition(IntPtr geometry, out VECTOR position);

		// Token: 0x06000434 RID: 1076
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetScale(IntPtr geometry, ref VECTOR scale);

		// Token: 0x06000435 RID: 1077
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetScale(IntPtr geometry, out VECTOR scale);

		// Token: 0x06000436 RID: 1078
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_Save(IntPtr geometry, IntPtr data, out int datasize);

		// Token: 0x06000437 RID: 1079
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_SetUserData(IntPtr geometry, IntPtr userdata);

		// Token: 0x06000438 RID: 1080
		[DllImport("fmodstudio")]
		private static extern RESULT FMOD5_Geometry_GetUserData(IntPtr geometry, out IntPtr userdata);

		// Token: 0x06000439 RID: 1081 RVA: 0x00006DDD File Offset: 0x000051DD
		public bool hasHandle()
		{
			return this.handle != IntPtr.Zero;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00006DEF File Offset: 0x000051EF
		public void clearHandle()
		{
			this.handle = IntPtr.Zero;
		}

		// Token: 0x0400028B RID: 651
		public IntPtr handle;
	}
}
