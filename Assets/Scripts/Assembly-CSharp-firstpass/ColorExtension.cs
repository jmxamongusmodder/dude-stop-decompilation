using System;
using System.Globalization;
using UnityEngine;

// Token: 0x02000344 RID: 836
public static class ColorExtension
{
	// Token: 0x06001460 RID: 5216 RVA: 0x000353E8 File Offset: 0x000337E8
	public static Color FromHexString(this string color)
	{
		Color result = new Color
		{
			r = Mathf.Clamp01((float)int.Parse(color.Substring(0, 2), NumberStyles.HexNumber) / 255f),
			g = Mathf.Clamp01((float)int.Parse(color.Substring(2, 2), NumberStyles.HexNumber) / 255f),
			b = Mathf.Clamp01((float)int.Parse(color.Substring(4, 2), NumberStyles.HexNumber) / 255f)
		};
		result.a = ((color.Length < 8) ? 1f : Mathf.Clamp01((float)int.Parse(color.Substring(6, 2), NumberStyles.HexNumber) / 255f));
		return result;
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x000354AC File Offset: 0x000338AC
	public static ColorExtension.ColorHSV RGBToHSV(Color32 color)
	{
		float num = (float)color.r / 255f;
		float num2 = (float)color.g / 255f;
		float num3 = (float)color.b / 255f;
		float num4 = Mathf.Min(new float[]
		{
			num,
			num2,
			num3
		});
		float num5 = Mathf.Max(new float[]
		{
			num,
			num2,
			num3
		});
		float num6 = num5 - num4;
		float num7 = 0f;
		float saturation = 0f;
		float value = num5;
		if (num6.Equals(0f))
		{
			return new ColorExtension.ColorHSV(num7, saturation, value);
		}
		saturation = num6 / num5;
		float num8 = ((num5 - num) / 6f + num6 / 2f) / num6;
		float num9 = ((num5 - num2) / 6f + num6 / 2f) / num6;
		float num10 = ((num5 - num3) / 6f + num6 / 2f) / num6;
		if (num.Equals(num5))
		{
			num7 = num10 - num9;
		}
		else if (num2.Equals(num5))
		{
			num7 = 0.33333334f + num8 - num10;
		}
		else if (num3.Equals(num5))
		{
			num7 = 0.6666667f + num9 - num8;
		}
		if (num7 < 0f)
		{
			num7 += 1f;
		}
		else if (num7 > 1f)
		{
			num7 -= 1f;
		}
		return new ColorExtension.ColorHSV(num7, saturation, value);
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0003562C File Offset: 0x00033A2C
	public static Color HSVToRGB(ColorExtension.ColorHSV color)
	{
		return ColorExtension.HSVToRGB(color.hue, color.saturation, color.value);
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x00035648 File Offset: 0x00033A48
	public static Color HSVToRGB(float H, float S, float V)
	{
		if (S == 0f)
		{
			return new Color(V, V, V);
		}
		if (V == 0f)
		{
			return Color.black;
		}
		Color black = Color.black;
		float num = H * 6f;
		int num2 = Mathf.FloorToInt(num);
		float num3 = num - (float)num2;
		float num4 = V * (1f - S);
		float num5 = V * (1f - S * num3);
		float num6 = V * (1f - S * (1f - num3));
		switch (num2 + 1)
		{
		case 0:
			black.r = V;
			black.g = num4;
			black.b = num5;
			break;
		case 1:
			black.r = V;
			black.g = num6;
			black.b = num4;
			break;
		case 2:
			black.r = num5;
			black.g = V;
			black.b = num4;
			break;
		case 3:
			black.r = num4;
			black.g = V;
			black.b = num6;
			break;
		case 4:
			black.r = num4;
			black.g = num5;
			black.b = V;
			break;
		case 5:
			black.r = num6;
			black.g = num4;
			black.b = V;
			break;
		case 6:
			black.r = V;
			black.g = num4;
			black.b = num5;
			break;
		case 7:
			black.r = V;
			black.g = num6;
			black.b = num4;
			break;
		}
		black.r = Mathf.Clamp(black.r, 0f, 1f);
		black.g = Mathf.Clamp(black.g, 0f, 1f);
		black.b = Mathf.Clamp(black.b, 0f, 1f);
		return black;
	}

	// Token: 0x02000345 RID: 837
	public class ColorHSV
	{
		// Token: 0x06001464 RID: 5220 RVA: 0x00035840 File Offset: 0x00033C40
		public ColorHSV(float hue, float saturation, float value)
		{
			this.hue = hue;
			this.saturation = saturation;
			this.value = value;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003585D File Offset: 0x00033C5D
		public Color ToRGB()
		{
			return ColorExtension.HSVToRGB(this);
		}

		// Token: 0x040011AB RID: 4523
		public float hue;

		// Token: 0x040011AC RID: 4524
		public float saturation;

		// Token: 0x040011AD RID: 4525
		public float value;
	}
}
