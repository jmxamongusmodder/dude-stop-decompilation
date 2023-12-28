using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000553 RID: 1363
public class JigSaw_piece_spawnpoint : MonoBehaviour
{
	// Token: 0x06001F3A RID: 7994 RVA: 0x00094F29 File Offset: 0x00093329
	private void Awake()
	{
		base.StartCoroutine(this.LateAwake());
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x00094F38 File Offset: 0x00093338
	public IEnumerator LateAwake()
	{
		yield return null;
		UnityEngine.Object.Destroy(base.gameObject);
		if (Global.self.unlockedJigsawPieces >= 20)
		{
		}
		if (this.lookForChosen)
		{
			JigSaw_piece_spawnpoint[] componentsInChildren = this.GetPuzzleStats().GetComponentsInChildren<JigSaw_piece_spawnpoint>();
			JigSaw_piece_spawnpoint jigSaw_piece_spawnpoint = componentsInChildren[UnityEngine.Random.Range(0, componentsInChildren.Length)];
			foreach (JigSaw_piece_spawnpoint jigSaw_piece_spawnpoint2 in componentsInChildren)
			{
				if (jigSaw_piece_spawnpoint2.GetInstanceID() != jigSaw_piece_spawnpoint.GetInstanceID())
				{
					jigSaw_piece_spawnpoint2.active = false;
					UnityEngine.Object.Destroy(jigSaw_piece_spawnpoint2.gameObject);
				}
				jigSaw_piece_spawnpoint2.lookForChosen = false;
			}
		}
		if (!this.active)
		{
			yield break;
		}
		if (SerializablePuzzleStats.Get(this.GetPuzzleStats().name).jigSawPiecesFound > 0 && AwardController.self != null)
		{
			yield break;
		}
		if (AwardController.self == null)
		{
			this.createPiece();
		}
		else if (UnityEngine.Random.Range(0f, 0.999f) < AwardController.self.getJigSawSpawnChance())
		{
			this.createPiece();
		}
		yield break;
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x00094F54 File Offset: 0x00093354
	private void createPiece()
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.piecePrefab);
		transform.SetParent(base.transform.parent);
		transform.position = base.transform.position;
		transform.localScale = base.transform.localScale;
		transform.localRotation = base.transform.localRotation;
		transform.gameObject.layer = base.gameObject.layer;
		JigSaw_piece component = transform.GetComponent<JigSaw_piece>();
		component.spawnPiece(base.transform);
		this.GetPuzzleStats().jigSawPieceOnPuzzle = component;
		if (this.rndRotation)
		{
			transform.Rotate(Vector3.forward * (float)UnityEngine.Random.Range(0, 360));
		}
	}

	// Token: 0x04002277 RID: 8823
	public Transform piecePrefab;

	// Token: 0x04002278 RID: 8824
	public bool rndRotation = true;

	// Token: 0x04002279 RID: 8825
	[HideInInspector]
	public bool active = true;

	// Token: 0x0400227A RID: 8826
	[HideInInspector]
	public bool lookForChosen = true;
}
