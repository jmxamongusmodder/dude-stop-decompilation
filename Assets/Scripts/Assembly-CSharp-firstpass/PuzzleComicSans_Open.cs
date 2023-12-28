using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public class PuzzleComicSans_Open : MonoBehaviour
{
	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005E099 File Offset: 0x0005C499
	// (set) Token: 0x0600195D RID: 6493 RVA: 0x0005E0A0 File Offset: 0x0005C4A0
	public static bool open { get; private set; }

	// Token: 0x0600195E RID: 6494 RVA: 0x0005E0A8 File Offset: 0x0005C4A8
	private void Update()
	{
		if (PuzzleComicSans_Open.open && base.transform.eulerAngles.z != 180f)
		{
			float z = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 180f, this.rotationSpeed * Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
		}
		else if (!PuzzleComicSans_Open.open && base.transform.eulerAngles.z != 0f)
		{
			float z2 = Mathf.MoveTowardsAngle(base.transform.eulerAngles.z, 0f, this.rotationSpeed * Time.deltaTime);
			base.transform.rotation = Quaternion.Euler(0f, 0f, z2);
		}
		if (PuzzleComicSans_Open.open && this.list.position.y != this.openPosition)
		{
			float y = Mathf.MoveTowards(this.list.position.y, this.openPosition, this.openSpeed * Time.deltaTime);
			this.list.position = new Vector3(this.list.position.x, y, 0f);
		}
		else if (!PuzzleComicSans_Open.open && this.list.position.y != this.closedPosition)
		{
			float y2 = Mathf.MoveTowards(this.list.position.y, this.closedPosition, this.openSpeed * Time.deltaTime);
			this.list.position = new Vector3(this.list.position.x, y2, 0f);
		}
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x0005E29C File Offset: 0x0005C69C
	public void Click()
	{
		this.OnMouseDown();
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x0005E2A4 File Offset: 0x0005C6A4
	private void OnMouseDown()
	{
		if (this.respond)
		{
			PuzzleComicSans_Open.open = !PuzzleComicSans_Open.open;
			Audio.self.playOneShot("712f7735-9abf-4d30-9273-810a522ab57c", 1f);
			Audio.self.playOneShot("6904ab56-fee7-4598-b53e-c786243267d7", 1f);
		}
	}

	// Token: 0x0400175D RID: 5981
	public Transform list;

	// Token: 0x0400175E RID: 5982
	public Transform background;

	// Token: 0x0400175F RID: 5983
	public List<Transform> fonts = new List<Transform>();

	// Token: 0x04001760 RID: 5984
	public float openSpeed = 10f;

	// Token: 0x04001761 RID: 5985
	public bool respond = true;

	// Token: 0x04001762 RID: 5986
	public float rotationSpeed = 60f;

	// Token: 0x04001763 RID: 5987
	public float openPosition = -0.22f;

	// Token: 0x04001764 RID: 5988
	public float closedPosition = 2.6f;
}
