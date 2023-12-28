using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class PuzzleCupSmallest_animation : MonoBehaviour
{
	// Token: 0x0600196A RID: 6506 RVA: 0x0005E5FC File Offset: 0x0005C9FC
	private void Awake()
	{
		base.Invoke("placeCups", 0.5f);
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x0005E60E File Offset: 0x0005CA0E
	private void Start()
	{
		this.voice = this.GetPuzzleStats().GetComponent<AudioVoice_CupSmallest>();
		base.StartCoroutine(this.coroutineControl());
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x0005E630 File Offset: 0x0005CA30
	private void placeCups()
	{
		for (int i = 0; i < 3; i++)
		{
			this.cupList[i].position = this.setActive[i].transform.position;
			this.cupList[i].rotation = this.setActive[i].transform.rotation;
			this.setActive[i].SetActive(false);
		}
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x0005E69C File Offset: 0x0005CA9C
	private IEnumerator coroutineControl()
	{
		Audio.self.playOneShot("c1874c83-cc9d-4326-9f06-ec22773f6219", 1f);
		yield return new WaitForSeconds(1f);
		this.voice.start();
		yield return base.StartCoroutine(this.pickUp(this.cupList[0]));
		while (!this.voice.canShaffle)
		{
			yield return null;
		}
		yield return base.StartCoroutine(this.swapTwoCups(this.cupList[0], this.cupList[1]));
		yield return new WaitForSeconds(this.betweenSwaps);
		yield return base.StartCoroutine(this.swapTwoCups(this.cupList[1], this.cupList[2]));
		yield return new WaitForSeconds(this.betweenSwaps);
		yield return base.StartCoroutine(this.swapTwoCups(this.cupList[0], this.cupList[2]));
		yield return new WaitForSeconds(this.betweenSwaps);
		yield return base.StartCoroutine(this.swapTwoCups(this.cupList[0], this.cupList[1]));
		yield return new WaitForSeconds(this.betweenSwaps);
		yield return base.StartCoroutine(this.swapTwoCups(this.cupList[1], this.cupList[2]));
		foreach (GameObject gameObject in this.setActive)
		{
			gameObject.SetActive(true);
			if (gameObject.GetComponent<RewardCupRed>() != null)
			{
				gameObject.GetComponent<RewardCupRed>().enabled = true;
				gameObject.GetComponent<PhysicsSound>().enable = true;
			}
		}
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x0005E6B8 File Offset: 0x0005CAB8
	private IEnumerator pickUp(Transform cup)
	{
		float time = 0f;
		while (time < this.pickUpCurve.keys[this.pickUpCurve.keys.Length - 1].time)
		{
			time += Time.deltaTime / this.pickUpTime;
			float newY = this.pickUpCurve.Evaluate(time);
			Vector2 pos = cup.localPosition;
			pos.y = newY * this.pickUpMaxY;
			cup.localPosition = pos;
			yield return null;
		}
		this.diamond.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x0005E6DC File Offset: 0x0005CADC
	private IEnumerator swapTwoCups(Transform cup1, Transform cup2)
	{
		cup1.GetComponent<SpriteRenderer>().sortingLayerName = "Top";
		cup2.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
		float dist = Mathf.Abs(cup1.localPosition.x - cup2.localPosition.x);
		Vector2 orig = cup1.localPosition;
		float time = 0f;
		float timeMax = this.swapCurve.keys[this.swapCurve.keys.Length - 1].time;
		while (time < timeMax)
		{
			time += Time.deltaTime / this.swapTime;
			time = Mathf.Min(time, timeMax);
			float delta = this.swapCurve.Evaluate(time);
			Vector2 pos = cup1.localPosition;
			pos.x = orig.x + delta * dist;
			pos.y = orig.y - Mathf.Sin(time / timeMax * 3.1415927f) * this.swapMaxY;
			cup1.localPosition = pos;
			pos = cup2.localPosition;
			pos.x = orig.x + dist - delta * dist;
			pos.y = orig.y + Mathf.Sin(time / timeMax * 3.1415927f) * this.swapMaxY;
			cup2.localPosition = pos;
			yield return null;
		}
		cup1.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		cup2.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
		cup1.localPosition = orig;
		cup2.localPosition = orig + Vector2.right * dist;
		yield break;
	}

	// Token: 0x04001769 RID: 5993
	public AnimationCurve pickUpCurve;

	// Token: 0x0400176A RID: 5994
	public float pickUpTime = 3f;

	// Token: 0x0400176B RID: 5995
	public float pickUpMaxY = 1.5f;

	// Token: 0x0400176C RID: 5996
	[Space(10f)]
	public AnimationCurve swapCurve;

	// Token: 0x0400176D RID: 5997
	public float swapTime = 0.5f;

	// Token: 0x0400176E RID: 5998
	public float swapMaxY = 0.3f;

	// Token: 0x0400176F RID: 5999
	public float betweenSwaps = 0.3f;

	// Token: 0x04001770 RID: 6000
	[Space(10f)]
	public Transform[] cupList;

	// Token: 0x04001771 RID: 6001
	public Transform diamond;

	// Token: 0x04001772 RID: 6002
	[Space(20f)]
	public GameObject[] setActive;

	// Token: 0x04001773 RID: 6003
	private AudioVoice_CupSmallest voice;
}
