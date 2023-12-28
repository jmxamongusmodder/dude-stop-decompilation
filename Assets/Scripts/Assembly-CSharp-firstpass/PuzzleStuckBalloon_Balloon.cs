using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000456 RID: 1110
public class PuzzleStuckBalloon_Balloon : PuzzleStuckBalloon_StringPart
{
	// Token: 0x06001C7D RID: 7293 RVA: 0x00079974 File Offset: 0x00077D74
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		this.renderers.Add(base.transform.GetChild(0).GetComponent<SpriteRenderer>());
		IEnumerator enumerator = this.lineParent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				this.renderers.Add(transform.GetComponent<SpriteRenderer>());
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

	// Token: 0x06001C7E RID: 7294 RVA: 0x00079A10 File Offset: 0x00077E10
	private void Update()
	{
		this.CheckVictoryConditions();
	}

	// Token: 0x06001C7F RID: 7295 RVA: 0x00079A18 File Offset: 0x00077E18
	private void CheckVictoryConditions()
	{
		if (this.dragged)
		{
			return;
		}
		bool flag = false;
		foreach (SpriteRenderer spriteRenderer in this.renderers)
		{
			flag |= GeometryUtility.TestPlanesAABB(this.planes, spriteRenderer.bounds);
		}
		if (!flag)
		{
			Global.LevelCompleted(0f, true);
			this.GetComponentInPuzzleStats<PuzzleStuckBalloon_Lines>().enabled = true;
			UnityEngine.Object.Destroy(this.GetComponentInPuzzleStats<PuzzleStuckBalloon_StringLines>().gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
			IEnumerator enumerator2 = this.lineParent.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj = enumerator2.Current;
					Transform transform = (Transform)obj;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator2 as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.GetComponentInPuzzleStats<AudioVoice_StuckBalloon>().cancelCatchLine();
		}
	}

	// Token: 0x04001AE8 RID: 6888
	public Transform lineParent;

	// Token: 0x04001AE9 RID: 6889
	private Plane[] planes;

	// Token: 0x04001AEA RID: 6890
	private List<SpriteRenderer> renderers = new List<SpriteRenderer>();
}
