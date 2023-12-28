using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000452 RID: 1106
public class PuzzleSlowLeftLine_StationaryCar : MonoBehaviour
{
	// Token: 0x06001C5C RID: 7260 RVA: 0x00078410 File Offset: 0x00076810
	private void Start()
	{
		base.StartCoroutine(this.MovingCoroutine());
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x00078420 File Offset: 0x00076820
	private IEnumerator MovingCoroutine()
	{
		this.startingPosition = base.transform.localPosition;
		this.endPosition = this.startingPosition;
		float moveTimer = this.moveTime;
		for (;;)
		{
			if (moveTimer == this.moveTime)
			{
				float x = UnityEngine.Random.Range(-1f, 1f);
				float y = UnityEngine.Random.Range(-1f, 1f);
				this.currentPosition = this.endPosition;
				this.endPosition = this.startingPosition + new Vector2(x, y) * this.moveDistance;
				moveTimer = 0f;
			}
			else
			{
				moveTimer = Mathf.MoveTowards(moveTimer, this.moveTime, Time.deltaTime);
				base.transform.localPosition = Vector2.Lerp(this.currentPosition, this.endPosition, moveTimer / this.moveTime);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001ABE RID: 6846
	public float moveDistance;

	// Token: 0x04001ABF RID: 6847
	public float moveTime;

	// Token: 0x04001AC0 RID: 6848
	private Vector2 startingPosition;

	// Token: 0x04001AC1 RID: 6849
	private Vector2 currentPosition;

	// Token: 0x04001AC2 RID: 6850
	private Vector2 endPosition;
}
