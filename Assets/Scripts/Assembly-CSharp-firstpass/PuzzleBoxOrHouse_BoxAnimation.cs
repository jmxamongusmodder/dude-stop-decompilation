using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003CB RID: 971
public class PuzzleBoxOrHouse_BoxAnimation : PuzzleBoxOrHouse_AnimationFinish, TransitionProcessor
{
	// Token: 0x0600184E RID: 6222 RVA: 0x00054D80 File Offset: 0x00053180
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x00054D92 File Offset: 0x00053192
	public void StartBoxAnimation()
	{
		Audio.self.playOneShot("713b2282-9951-4350-b0fd-120572052290", 1f);
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x00054DA9 File Offset: 0x000531A9
	public void EndBoxAnimation()
	{
		base.StartCoroutine(this.MovingCoroutine());
		base.EndAnimation();
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x00054DC0 File Offset: 0x000531C0
	private IEnumerator MovingCoroutine()
	{
		base.GetComponent<Animator>().enabled = false;
		int position = 0;
		this.moving = true;
		float time = this.movement.keys[this.movement.length - 1].time;
		for (;;)
		{
			yield return new WaitForSeconds(this.waitBetweenMovements);
			Audio.self.playOneShot("51023245-cc2b-49d0-8f46-7c89c3ee2893", 1f);
			float timer = 0f;
			int direction = (position != 0) ? (((double)UnityEngine.Random.value >= 0.5) ? -1 : 1) : 1;
			Vector2 startPosition = base.transform.localPosition;
			float multiplier = UnityEngine.Random.Range(this.minMultiplier, this.maxMultiplier);
			while (timer != time)
			{
				timer = Mathf.MoveTowards(timer, time, Time.deltaTime);
				base.transform.localPosition = startPosition + multiplier * Vector2.right * (float)direction * this.movement.Evaluate(timer);
				yield return null;
			}
			position += direction;
		}
		yield break;
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x00054DDC File Offset: 0x000531DC
	public void TransitionUpdate()
	{
		if (!this.moving)
		{
			return;
		}
		if (!GeometryUtility.TestPlanesAABB(this.planes, this.spriteToCheck.bounds))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			this.moving = false;
		}
	}

	// Token: 0x04001633 RID: 5683
	public SpriteRenderer spriteToCheck;

	// Token: 0x04001634 RID: 5684
	public AnimationCurve movement;

	// Token: 0x04001635 RID: 5685
	public float waitBetweenMovements;

	// Token: 0x04001636 RID: 5686
	public float minMultiplier;

	// Token: 0x04001637 RID: 5687
	public float maxMultiplier;

	// Token: 0x04001638 RID: 5688
	private bool moving;

	// Token: 0x04001639 RID: 5689
	private Plane[] planes;
}
