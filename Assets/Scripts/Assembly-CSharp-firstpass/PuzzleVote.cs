using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class PuzzleVote : MonoBehaviour
{
	// Token: 0x06001CFE RID: 7422 RVA: 0x0007E260 File Offset: 0x0007C660
	private void OnMouseDown()
	{
		if (this.finished)
		{
			return;
		}
		this.check.transform.position = base.transform.position;
		this.check.gameObject.SetActive(true);
		if (base.tag == "SuccessCollider")
		{
			Global.LevelCompleted(0f, true);
			this.EndElections();
		}
		else
		{
			Global.LevelFailed(0f, true);
			this.EndElections();
		}
	}

	// Token: 0x06001CFF RID: 7423 RVA: 0x0007E2E1 File Offset: 0x0007C6E1
	private void OnMouseEnter()
	{
		if (this.finished)
		{
			return;
		}
		this.hover.transform.position = base.transform.position;
		this.hover.gameObject.SetActive(true);
	}

	// Token: 0x06001D00 RID: 7424 RVA: 0x0007E31B File Offset: 0x0007C71B
	private void OnMouseExit()
	{
		if (this.finished)
		{
			return;
		}
		this.hover.gameObject.SetActive(false);
	}

	// Token: 0x06001D01 RID: 7425 RVA: 0x0007E33C File Offset: 0x0007C73C
	private void EndElections()
	{
		IEnumerator enumerator = base.transform.parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				transform.GetComponent<PuzzleVote>().finished = true;
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
	}

	// Token: 0x04001B9F RID: 7071
	public bool finished;

	// Token: 0x04001BA0 RID: 7072
	public Transform hover;

	// Token: 0x04001BA1 RID: 7073
	public Transform check;
}
