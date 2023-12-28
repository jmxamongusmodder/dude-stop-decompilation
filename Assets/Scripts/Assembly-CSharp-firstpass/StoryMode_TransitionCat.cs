using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000471 RID: 1137
public class StoryMode_TransitionCat : MonoBehaviour
{
	// Token: 0x06001D38 RID: 7480 RVA: 0x00080294 File Offset: 0x0007E694
	private void Awake()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x000802A4 File Offset: 0x0007E6A4
	public void showCat(Transform oldCat)
	{
		base.transform.SetParent(null);
		base.transform.localScale = oldCat.lossyScale;
		base.transform.position = oldCat.position;
		base.transform.rotation = oldCat.rotation;
		this.originalX = base.transform.position.x;
		base.gameObject.SetActive(true);
		base.StartCoroutine(this.moveCat());
		this.rotationCoroutine = base.StartCoroutine(this.RotationCoroutine());
		StoryMode_TransitionCat.self = this;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x0008033A File Offset: 0x0007E73A
	public void SetRotationPosition(Vector3 position)
	{
		this.lastPosition = position;
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x00080344 File Offset: 0x0007E744
	private IEnumerator moveCat()
	{
		float time = Global.self.transitionTimeSide;
		while (time != 0f)
		{
			time = Mathf.MoveTowards(time, 0f, Time.deltaTime);
			float prog = time / Global.self.transitionTimeSide;
			float curveValue = Global.self.transitionStoryMode.Evaluate(prog);
			Vector2 pos = base.transform.position;
			pos.x = this.originalX + (this.destinationX - this.originalX) * curveValue;
			base.transform.position = pos;
			yield return null;
		}
		while (Global.self.currPuzzle == null)
		{
			yield return null;
		}
		time = 0f;
		float startingAngle = base.transform.eulerAngles.z;
		base.StopCoroutine(this.rotationCoroutine);
		while (time != this.rotationTime)
		{
			time = Mathf.MoveTowards(time, this.rotationTime, Time.deltaTime);
			float angle = Mathf.LerpAngle(startingAngle, 0f, time / this.rotationTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, angle);
			yield return null;
		}
		PuzzleCatOnBlackDress[] list = Global.self.currPuzzle.GetComponentsInChildren<PuzzleCatOnBlackDress>(true);
		if (list.Length != 0)
		{
			list[0].gameObject.SetActive(true);
		}
		UnityEngine.Object.Destroy(base.gameObject);
		StoryMode_TransitionCat.self = null;
		yield break;
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x00080360 File Offset: 0x0007E760
	private void RotateAnimal()
	{
		float num = (base.transform.localPosition.x - this.lastPosition.x) / this.length;
		float z = Mathf.Sin(num * 3.1415927f * 0.5f) * this.amplitude;
		base.transform.localRotation = Quaternion.Euler(0f, 0f, z);
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x000803CC File Offset: 0x0007E7CC
	private IEnumerator RotationCoroutine()
	{
		yield return new WaitForEndOfFrame();
		for (; ; )
		{
			this.RotateAnimal();
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001BDE RID: 7134
	public static StoryMode_TransitionCat self;

	// Token: 0x04001BDF RID: 7135
	public float destinationX;

	// Token: 0x04001BE0 RID: 7136
	private float originalX;

	// Token: 0x04001BE1 RID: 7137
	[Header("Rotation stuff")]
	[Tooltip("Distance to walk for one of four cycles")]
	public float length = 1f;

	// Token: 0x04001BE2 RID: 7138
	[Tooltip("Max angle for moving cat")]
	public float amplitude = 10f;

	// Token: 0x04001BE3 RID: 7139
	[Tooltip("The time in which the cat rotates back to zero while not dragged")]
	public float rotationTime = 0.5f;

	// Token: 0x04001BE4 RID: 7140
	public float scaleTime = 0.3f;

	// Token: 0x04001BE5 RID: 7141
	private Vector3 lastPosition;

	// Token: 0x04001BE6 RID: 7142
	private Coroutine rotationCoroutine;
}
