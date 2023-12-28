using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class CupCandyCup_CandyController : MonoBehaviour
{
	// Token: 0x06001473 RID: 5235 RVA: 0x00035F75 File Offset: 0x00034375
	private void Start()
	{
		base.StartCoroutine(this.SpawningCoroutine());
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x00035F94 File Offset: 0x00034394
	private IEnumerator SpawningCoroutine()
	{
		List<Transform> toRemove = new List<Transform>();
		while (Global.self.NoCurrentTransition)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.position.y <= 0f)
					{
						if (!GeometryUtility.TestPlanesAABB(this.planes, transform.GetComponent<Renderer>().bounds))
						{
							toRemove.Add(transform);
						}
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			foreach (Transform transform2 in toRemove)
			{
				transform2.GetComponent<Rigidbody2D>().isKinematic = true;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
			toRemove.Clear();
			float offset = UnityEngine.Random.value;
			float hue = (offset + 0.618034f * (float)(++this.colors)) % 1f;
			Color c = ColorExtension.HSVToRGB(hue, this.saturation, this.value);
			float randX = UnityEngine.Random.Range(this.minX, this.maxX);
			int rand = UnityEngine.Random.Range(0, this.candies.Length);
			Vector2 line = Camera.main.ViewportToWorldPoint(new Vector2(randX, 1.1f));
			Transform newCandy = UnityEngine.Object.Instantiate<Transform>(this.candies[rand]);
			newCandy.SetParent(base.transform);
			newCandy.position = line;
			newCandy.gameObject.SetActive(true);
			newCandy.GetComponent<SpriteRenderer>().color = c;
			this.timeBetweenCandies = Mathf.MoveTowards(this.timeBetweenCandies, this.minTimeBetweenCandies, this.timeDecrease);
			CupCandyCup_Candy script = newCandy.GetComponent<CupCandyCup_Candy>();
			script.maxMagnitude = this.maxMagnitude * UnityEngine.Random.Range(1f - this.randomRange, 1f + this.randomRange);
			script.maxAngularVelocity = this.maxAngularVelocity * UnityEngine.Random.Range(1f - this.randomRange, 1f + this.randomRange);
			yield return new WaitForSeconds(this.timeBetweenCandies);
		}
		yield break;
	}

	// Token: 0x040011C9 RID: 4553
	public Transform[] candies;

	// Token: 0x040011CA RID: 4554
	[Range(0f, 1f)]
	public float saturation;

	// Token: 0x040011CB RID: 4555
	[Range(0f, 1f)]
	public float value;

	// Token: 0x040011CC RID: 4556
	[Range(0.1f, 0.9f)]
	public float minX = 0.1f;

	// Token: 0x040011CD RID: 4557
	[Range(0.1f, 0.9f)]
	public float maxX = 0.9f;

	// Token: 0x040011CE RID: 4558
	public float timeBetweenCandies;

	// Token: 0x040011CF RID: 4559
	public float timeDecrease;

	// Token: 0x040011D0 RID: 4560
	public float minTimeBetweenCandies;

	// Token: 0x040011D1 RID: 4561
	[Header("Candy physics stuff")]
	public float maxMagnitude = 3f;

	// Token: 0x040011D2 RID: 4562
	public float maxAngularVelocity = 15f;

	// Token: 0x040011D3 RID: 4563
	public float randomRange = 0.1f;

	// Token: 0x040011D4 RID: 4564
	private Plane[] planes;

	// Token: 0x040011D5 RID: 4565
	private int colors;

	// Token: 0x040011D6 RID: 4566
	private const float GOLDEN_RATIO = 0.618034f;
}
