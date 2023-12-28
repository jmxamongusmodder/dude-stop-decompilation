using System;
using System.Linq;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class PuzzleFriendsPen_Answer : MonoBehaviour
{
	// Token: 0x06001A3E RID: 6718 RVA: 0x00066078 File Offset: 0x00064478
	private void OnTriggerEnter2D(Collider2D other)
	{
		DrawingPenPoint component = other.GetComponent<DrawingPenPoint>();
		if (component == null || component.Depleted())
		{
			return;
		}
		PuzzleFriendsPen_Answer puzzleFriendsPen_Answer = (from x in base.transform.parent.GetComponentsInChildren<PuzzleFriendsPen_Answer>()
		where x != this
		select x).First<PuzzleFriendsPen_Answer>();
		UnityEngine.Object.Destroy(puzzleFriendsPen_Answer.gameObject);
		SpriteRenderer component2 = base.GetComponent<SpriteRenderer>();
		component2.color = component.color;
		component2.enabled = true;
		base.GetComponent<Collider2D>().enabled = false;
		base.enabled = false;
	}
}
