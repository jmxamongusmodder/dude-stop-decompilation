using System;

namespace FMOD
{
	// Token: 0x02000059 RID: 89
	public class PRESET
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000047B8 File Offset: 0x00002BB8
		public static REVERB_PROPERTIES OFF()
		{
			return new REVERB_PROPERTIES(1000f, 7f, 11f, 5000f, 100f, 100f, 100f, 250f, 0f, 20f, 96f, -80f);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004808 File Offset: 0x00002C08
		public static REVERB_PROPERTIES GENERIC()
		{
			return new REVERB_PROPERTIES(1500f, 7f, 11f, 5000f, 83f, 100f, 100f, 250f, 0f, 14500f, 96f, -8f);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004858 File Offset: 0x00002C58
		public static REVERB_PROPERTIES PADDEDCELL()
		{
			return new REVERB_PROPERTIES(170f, 1f, 2f, 5000f, 10f, 100f, 100f, 250f, 0f, 160f, 84f, -7.8f);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000048A8 File Offset: 0x00002CA8
		public static REVERB_PROPERTIES ROOM()
		{
			return new REVERB_PROPERTIES(400f, 2f, 3f, 5000f, 83f, 100f, 100f, 250f, 0f, 6050f, 88f, -9.4f);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000048F8 File Offset: 0x00002CF8
		public static REVERB_PROPERTIES BATHROOM()
		{
			return new REVERB_PROPERTIES(1500f, 7f, 11f, 5000f, 54f, 100f, 60f, 250f, 0f, 2900f, 83f, 0.5f);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004948 File Offset: 0x00002D48
		public static REVERB_PROPERTIES LIVINGROOM()
		{
			return new REVERB_PROPERTIES(500f, 3f, 4f, 5000f, 10f, 100f, 100f, 250f, 0f, 160f, 58f, -19f);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004998 File Offset: 0x00002D98
		public static REVERB_PROPERTIES STONEROOM()
		{
			return new REVERB_PROPERTIES(2300f, 12f, 17f, 5000f, 64f, 100f, 100f, 250f, 0f, 7800f, 71f, -8.5f);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000049E8 File Offset: 0x00002DE8
		public static REVERB_PROPERTIES AUDITORIUM()
		{
			return new REVERB_PROPERTIES(4300f, 20f, 30f, 5000f, 59f, 100f, 100f, 250f, 0f, 5850f, 64f, -11.7f);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004A38 File Offset: 0x00002E38
		public static REVERB_PROPERTIES CONCERTHALL()
		{
			return new REVERB_PROPERTIES(3900f, 20f, 29f, 5000f, 70f, 100f, 100f, 250f, 0f, 5650f, 80f, -9.8f);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004A88 File Offset: 0x00002E88
		public static REVERB_PROPERTIES CAVE()
		{
			return new REVERB_PROPERTIES(2900f, 15f, 22f, 5000f, 100f, 100f, 100f, 250f, 0f, 20000f, 59f, -11.3f);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004AD8 File Offset: 0x00002ED8
		public static REVERB_PROPERTIES ARENA()
		{
			return new REVERB_PROPERTIES(7200f, 20f, 30f, 5000f, 33f, 100f, 100f, 250f, 0f, 4500f, 80f, -9.6f);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004B28 File Offset: 0x00002F28
		public static REVERB_PROPERTIES HANGAR()
		{
			return new REVERB_PROPERTIES(10000f, 20f, 30f, 5000f, 23f, 100f, 100f, 250f, 0f, 3400f, 72f, -7.4f);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004B78 File Offset: 0x00002F78
		public static REVERB_PROPERTIES CARPETTEDHALLWAY()
		{
			return new REVERB_PROPERTIES(300f, 2f, 30f, 5000f, 10f, 100f, 100f, 250f, 0f, 500f, 56f, -24f);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004BC8 File Offset: 0x00002FC8
		public static REVERB_PROPERTIES HALLWAY()
		{
			return new REVERB_PROPERTIES(1500f, 7f, 11f, 5000f, 59f, 100f, 100f, 250f, 0f, 7800f, 87f, -5.5f);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004C18 File Offset: 0x00003018
		public static REVERB_PROPERTIES STONECORRIDOR()
		{
			return new REVERB_PROPERTIES(270f, 13f, 20f, 5000f, 79f, 100f, 100f, 250f, 0f, 9000f, 86f, -6f);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004C68 File Offset: 0x00003068
		public static REVERB_PROPERTIES ALLEY()
		{
			return new REVERB_PROPERTIES(1500f, 7f, 11f, 5000f, 86f, 100f, 100f, 250f, 0f, 8300f, 80f, -9.8f);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004CB8 File Offset: 0x000030B8
		public static REVERB_PROPERTIES FOREST()
		{
			return new REVERB_PROPERTIES(1500f, 162f, 88f, 5000f, 54f, 79f, 100f, 250f, 0f, 760f, 94f, -12.3f);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004D08 File Offset: 0x00003108
		public static REVERB_PROPERTIES CITY()
		{
			return new REVERB_PROPERTIES(1500f, 7f, 11f, 5000f, 67f, 50f, 100f, 250f, 0f, 4050f, 66f, -26f);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004D58 File Offset: 0x00003158
		public static REVERB_PROPERTIES MOUNTAINS()
		{
			return new REVERB_PROPERTIES(1500f, 300f, 100f, 5000f, 21f, 27f, 100f, 250f, 0f, 1220f, 82f, -24f);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004DA8 File Offset: 0x000031A8
		public static REVERB_PROPERTIES QUARRY()
		{
			return new REVERB_PROPERTIES(1500f, 61f, 25f, 5000f, 83f, 100f, 100f, 250f, 0f, 3400f, 100f, -5f);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004DF8 File Offset: 0x000031F8
		public static REVERB_PROPERTIES PLAIN()
		{
			return new REVERB_PROPERTIES(1500f, 179f, 100f, 5000f, 50f, 21f, 100f, 250f, 0f, 1670f, 65f, -28f);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004E48 File Offset: 0x00003248
		public static REVERB_PROPERTIES PARKINGLOT()
		{
			return new REVERB_PROPERTIES(1700f, 8f, 12f, 5000f, 100f, 100f, 100f, 250f, 0f, 20000f, 56f, -19.5f);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004E98 File Offset: 0x00003298
		public static REVERB_PROPERTIES SEWERPIPE()
		{
			return new REVERB_PROPERTIES(2800f, 14f, 21f, 5000f, 14f, 80f, 60f, 250f, 0f, 3400f, 66f, 1.2f);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004EE8 File Offset: 0x000032E8
		public static REVERB_PROPERTIES UNDERWATER()
		{
			return new REVERB_PROPERTIES(1500f, 7f, 11f, 5000f, 10f, 100f, 100f, 250f, 0f, 500f, 92f, 7f);
		}
	}
}
