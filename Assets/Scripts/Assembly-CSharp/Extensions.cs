using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

// Token: 0x02000384 RID: 900
public static class Extensions
{
	// Token: 0x0600160F RID: 5647 RVA: 0x000452D0 File Offset: 0x000436D0
	public static Vector2 GetRandomPointOnScreen(Vector2 border)
	{
		Vector2 a = Camera.main.ViewportToWorldPoint(Vector2.zero);
		Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.one);
		return a + new Vector2(UnityEngine.Random.Range(border.x, vector.x - a.x - border.x), UnityEngine.Random.Range(border.y, vector.y - a.y - border.y));
	}

	// Token: 0x06001610 RID: 5648 RVA: 0x00045364 File Offset: 0x00043764
	public static Vector2 GetRandomPointOnScreen()
	{
		Vector2 vector = Camera.main.ViewportToWorldPoint(Vector2.zero);
		Vector2 vector2 = Camera.main.ViewportToWorldPoint(Vector2.one);
		return new Vector2(UnityEngine.Random.Range(vector.x, vector2.x - vector.x), UnityEngine.Random.Range(vector.y, vector2.y - vector.y));
	}

	// Token: 0x06001611 RID: 5649 RVA: 0x000453E0 File Offset: 0x000437E0
	public static Vector3 GetMousePosition(this Camera camera)
	{
		return camera.ScreenToWorldPoint(Input.mousePosition);
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000453ED File Offset: 0x000437ED
	public static T GetRandom<T>(this IList<T> list) where T : class
	{
		if (list.Count == 0)
		{
			return (T)((object)null);
		}
		return list[Extensions.random.Next(0, list.Count)];
	}

	// Token: 0x06001613 RID: 5651 RVA: 0x00045418 File Offset: 0x00043818
	public static T GetRandomEnum<T>(this IList<T> list) where T : struct, IConvertible, IComparable, IFormattable
	{
		if (!typeof(T).IsEnum)
		{
			throw new ArgumentException("Must be an Enum type");
		}
		if (list.Count == 0)
		{
			return default(T);
		}
		return list[Extensions.random.Next(0, list.Count)];
	}

	// Token: 0x06001614 RID: 5652 RVA: 0x00045470 File Offset: 0x00043870
	public static void Shuffle<T>(this IList<T> list)
	{
		int i = list.Count;
		while (i > 1)
		{
			i--;
			int index = Extensions.random.Next(i + 1);
			T value = list[index];
			list[index] = list[i];
			list[i] = value;
		}
	}

	// Token: 0x06001615 RID: 5653 RVA: 0x000454C0 File Offset: 0x000438C0
	public static T GetValue<T>(this SerializationInfo info, string key)
	{
		T result;
		try
		{
			result = (T)((object)info.GetValue(key, typeof(T)));
		}
		catch
		{
			result = default(T);
		}
		return result;
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x0004550C File Offset: 0x0004390C
	public static void SetX(this Transform transform, float newX)
	{
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x00045544 File Offset: 0x00043944
	public static void SetLocalX(this Transform transform, float newX)
	{
		transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x0004557C File Offset: 0x0004397C
	public static void SetLocalZ(this Transform transform, float newZ)
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newZ);
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x000455B4 File Offset: 0x000439B4
	public static void SetY(this Transform transform, float newY, bool local = false)
	{
		if (local)
		{
			transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
		}
		else
		{
			transform.position = new Vector3(transform.position.x, newY, transform.position.z);
		}
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x0004561C File Offset: 0x00043A1C
	public static Vector2 setY(this Vector2 current, float newY)
	{
		return new Vector2(current.x, newY);
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x0004562B File Offset: 0x00043A2B
	public static Vector2 setX(this Vector2 current, float newX)
	{
		return new Vector2(newX, current.y);
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x0004563A File Offset: 0x00043A3A
	public static void SetAngle(this Transform transform, float angle)
	{
		transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00045652 File Offset: 0x00043A52
	public static Vector3 XY(this Vector3 pos)
	{
		return new Vector3(pos.x, pos.y, 0f);
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x0004566C File Offset: 0x00043A6C
	public static PuzzleStats GetPuzzleStats(this MonoBehaviour script)
	{
		Transform transform = script.transform;
		PuzzleStats component;
		do
		{
			component = transform.GetComponent<PuzzleStats>();
			transform = transform.parent;
		}
		while (component == null && transform != null);
		return component;
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x000456A7 File Offset: 0x00043AA7
	public static T[] GetComponentsInPuzzleStats<T>(this MonoBehaviour script, bool includeInactive = false)
	{
		return script.GetPuzzleStats().GetComponentsInChildren<T>(includeInactive);
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x000456B5 File Offset: 0x00043AB5
	public static T GetComponentInPuzzleStats<T>(this MonoBehaviour script)
	{
		return script.GetPuzzleStats().GetComponentInChildren<T>();
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x000456C2 File Offset: 0x00043AC2
	public static T GetComponentInPuzzleStats<T>(this MonoBehaviour script, bool includeInactive)
	{
		return script.GetPuzzleStats().GetComponentsInChildren<T>(includeInactive).FirstOrDefault<T>();
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x000456D5 File Offset: 0x00043AD5
	public static float GetAnimationLength(this AnimationCurve curve)
	{
		return curve.keys[curve.length - 1].time;
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x000456F0 File Offset: 0x00043AF0
	public static Keyframe NextKey(this AnimationCurve curve, float time)
	{
		return (from x in curve.keys
		where x.time > time
		select x).FirstOrDefault<Keyframe>();
	}

	// Token: 0x06001624 RID: 5668 RVA: 0x00045728 File Offset: 0x00043B28
	public static Keyframe NextKey(this AnimationCurve curve, Keyframe key)
	{
		return (from x in curve.keys
		where x.time > key.time
		select x).FirstOrDefault<Keyframe>();
	}

	// Token: 0x06001625 RID: 5669 RVA: 0x00045760 File Offset: 0x00043B60
	public static void EnableEmmision(this ParticleSystem particles, bool on)
	{
		particles.emission.enabled = on;
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x0004577C File Offset: 0x00043B7C
	public static void Emit(this ParticleSystem particles)
	{
		particles.Emit((int)particles.GetEmissionRate());
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x0004578C File Offset: 0x00043B8C
	public static float GetEmissionRate(this ParticleSystem particles)
	{
		return particles.emission.rateOverTimeMultiplier;
	}

	// Token: 0x06001628 RID: 5672 RVA: 0x000457A8 File Offset: 0x00043BA8
	public static void SetEmissionRate(this ParticleSystem particles, float value)
	{
		particles.emission.rateOverTime = new ParticleSystem.MinMaxCurve(value);
	}

	// Token: 0x06001629 RID: 5673 RVA: 0x000457CC File Offset: 0x00043BCC
	public static void IncrementEmissionRate(this ParticleSystem particles, float value)
	{
		ParticleSystem.EmissionModule emission = particles.emission;
		emission.rateOverTime = new ParticleSystem.MinMaxCurve(emission.rateOverTimeMultiplier + value);
	}

	// Token: 0x0600162A RID: 5674 RVA: 0x000457F5 File Offset: 0x00043BF5
	public static void SetDynamic(this Rigidbody2D body)
	{
		body.bodyType = RigidbodyType2D.Dynamic;
		body.velocity = Vector2.zero;
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x00045809 File Offset: 0x00043C09
	public static void SetKinematic(this Rigidbody2D body)
	{
		body.bodyType = RigidbodyType2D.Kinematic;
		body.velocity = Vector2.zero;
		body.angularVelocity = 0f;
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x0600162C RID: 5676 RVA: 0x00045828 File Offset: 0x00043C28
	private static System.Random random
	{
		get
		{
			if (Extensions._random == null)
			{
				Extensions._random = new System.Random();
			}
			return Extensions._random;
		}
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x00045843 File Offset: 0x00043C43
	public static float Random(Vector2 vec)
	{
		return UnityEngine.Random.Range(vec.x, vec.y);
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x00045858 File Offset: 0x00043C58
	public static bool RandomBool()
	{
		return UnityEngine.Random.value > 0.5f;
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x00045866 File Offset: 0x00043C66
	public static int RandomInt(Vector2 vec)
	{
		return UnityEngine.Random.Range((int)vec.x, (int)vec.y);
	}

	// Token: 0x06001630 RID: 5680 RVA: 0x0004587D File Offset: 0x00043C7D
	public static int GetRandomSign()
	{
		return ((double)UnityEngine.Random.value >= 0.5) ? -1 : 1;
	}

	// Token: 0x06001631 RID: 5681 RVA: 0x0004589A File Offset: 0x00043C9A
	public static string AddTextSize(this string txt, int size)
	{
		return string.Format("<size={0}>{1}</size>", size, txt);
	}

	// Token: 0x06001632 RID: 5682 RVA: 0x000458AD File Offset: 0x00043CAD
	public static string getUniqueGUIDForThisPCOnly()
	{
		return SystemInfo.deviceUniqueIdentifier;
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x000458B4 File Offset: 0x00043CB4
	public static float Between(float min, float max, float value, bool clamp = false)
	{
		max -= min;
		value -= min;
		value /= max;
		if (clamp)
		{
			value = Mathf.Clamp(value, 0f, 1f);
		}
		return value;
	}

	// Token: 0x040013CE RID: 5070
	private static System.Random _random;

	// Token: 0x02000385 RID: 901
	public static class Color
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x000458E0 File Offset: 0x00043CE0
		public static UnityEngine.Color GetRandom()
		{
			return new UnityEngine.Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00045914 File Offset: 0x00043D14
		public static UnityEngine.Color SetAlpha(UnityEngine.Color color, float alpha)
		{
			color.a = alpha;
			return color;
		}
	}
}
