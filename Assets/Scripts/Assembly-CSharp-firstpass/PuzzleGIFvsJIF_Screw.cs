using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class PuzzleGIFvsJIF_Screw : MonoBehaviour
{
	// Token: 0x06001A6E RID: 6766 RVA: 0x00067BF6 File Offset: 0x00065FF6
	private void Start()
	{
		this.otherScrew = (from x in base.transform.parent.GetComponentsInChildren<PuzzleGIFvsJIF_Screw>()
		where x != this
		select x).First<PuzzleGIFvsJIF_Screw>();
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x00067C34 File Offset: 0x00066034
	private void Update()
	{
		if (this.fallenOut && !GeometryUtility.TestPlanesAABB(this.planes, base.transform.GetComponent<Renderer>().bounds))
		{
			base.gameObject.SetActive(false);
		}
		if (this.screwingOut && !this.fallenOut)
		{
			this.timer = Mathf.MoveTowards(this.timer, this.screwingTime, Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, this.screwingDegrees, this.timer / this.screwingTime));
			if (this.timer == this.screwingTime)
			{
				this.fallenOut = true;
				Audio.self.stopLoopSound("0dc1782a-596b-4dac-8254-e6b2a8e6bc85", true);
				Audio.self.playOneShot("cdcceea9-3e81-4177-997f-efbd7ee797e5", 1f);
				base.GetComponent<Rigidbody2D>().isKinematic = false;
				if (this.otherScrew.fallenOut)
				{
					this.cover.GetComponent<Rigidbody2D>().isKinematic = false;
					Audio.self.playOneShot("7c06eb2f-8b0a-417f-a71f-baf80583f298", 1f);
					foreach (MonoBehaviour monoBehaviour in base.transform.parent.GetComponentsInChildren<MonoBehaviour>())
					{
						monoBehaviour.enabled = true;
					}
					Global.self.currPuzzle.GetComponent<AudioVoice_GifJif>().openCover();
				}
			}
		}
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x00067DA4 File Offset: 0x000661A4
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.isTrigger)
		{
			return;
		}
		this.screwingOut = true;
		Audio.self.playLoopSound("0dc1782a-596b-4dac-8254-e6b2a8e6bc85");
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x00067DC8 File Offset: 0x000661C8
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.isTrigger)
		{
			return;
		}
		this.screwingOut = false;
		Audio.self.stopLoopSound("0dc1782a-596b-4dac-8254-e6b2a8e6bc85", true);
	}

	// Token: 0x0400188B RID: 6283
	public Transform cover;

	// Token: 0x0400188C RID: 6284
	public float screwingDegrees = 360f;

	// Token: 0x0400188D RID: 6285
	public float screwingTime = 1f;

	// Token: 0x0400188E RID: 6286
	private PuzzleGIFvsJIF_Screw otherScrew;

	// Token: 0x0400188F RID: 6287
	private bool screwingOut;

	// Token: 0x04001890 RID: 6288
	private bool fallenOut;

	// Token: 0x04001891 RID: 6289
	private float timer;

	// Token: 0x04001892 RID: 6290
	private Plane[] planes;
}
