using System;
using UnityEngine;

// Token: 0x020003E2 RID: 994
public class PuzzleChristmasTornPage : MonoBehaviour
{
	// Token: 0x06001918 RID: 6424 RVA: 0x0005BC58 File Offset: 0x0005A058
	private void Update()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vector.z = 0f;
		if (this.startPosition == Vector3.zero)
		{
			this.startPosition = vector;
		}
		Vector2 vector2 = vector - base.transform.position;
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		if (this.delta == 370f)
		{
			this.delta = num;
		}
		else
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, num - this.delta);
		}
		if (this.startCrumpling)
		{
			if (base.transform.localScale.x > 0.5f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 15f);
			}
			else
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.paper.gameObject);
				gameObject.transform.SetParent(base.transform.parent);
				gameObject.transform.position = vector;
				gameObject.SetActive(true);
				if (Input.GetMouseButton(0))
				{
					gameObject.GetComponent<Draggable>().OnMouseDown();
					gameObject.GetComponent<Draggable>().EmulateMouseUp();
					gameObject.GetComponent<Draggable>().SetDelta(Vector3.zero);
				}
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (!Input.GetMouseButton(0) || Vector3.Distance(vector, this.startPosition) > this.crumpleDistance)
		{
			this.startCrumpling = true;
		}
	}

	// Token: 0x04001715 RID: 5909
	public Transform paper;

	// Token: 0x04001716 RID: 5910
	public float crumpleDistance;

	// Token: 0x04001717 RID: 5911
	private bool startCrumpling;

	// Token: 0x04001718 RID: 5912
	private float delta = 370f;

	// Token: 0x04001719 RID: 5913
	private Vector3 startPosition = Vector3.zero;
}
