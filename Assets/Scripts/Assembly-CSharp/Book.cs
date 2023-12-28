using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class Book : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600001E RID: 30 RVA: 0x00002D3E File Offset: 0x00000F3E
	public int TotalPageCount
	{
		get
		{
			return this.bookPages.Length;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600001F RID: 31 RVA: 0x00002D48 File Offset: 0x00000F48
	public Vector3 EndBottomLeft
	{
		get
		{
			return this.edgeBottomLeft;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000020 RID: 32 RVA: 0x00002D50 File Offset: 0x00000F50
	public Vector3 EndBottomRight
	{
		get
		{
			return this.edgeBottomRight;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000021 RID: 33 RVA: 0x00002D58 File Offset: 0x00000F58
	public float Height
	{
		get
		{
			return this.BookPanel.rect.height;
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002D78 File Offset: 0x00000F78
	private void Awake()
	{
		this.canvas = UIControl.self.GetComponent<Canvas>();
		this.endButton = base.GetComponent<examPackUI>();
		if (this.canvas)
		{
			float scaleFactor = this.canvas.scaleFactor;
		}
		float num = this.BookPanel.rect.width / 2f;
		float height = this.BookPanel.rect.height;
		this.Left.gameObject.SetActive(false);
		this.Right.gameObject.SetActive(false);
		this.UpdateSprites();
		Vector3 global = this.BookPanel.transform.position + new Vector3(0f, -height / 2f);
		Vector3 global2 = this.BookPanel.transform.position + new Vector3(0f, height / 2f);
		Vector3 global3 = this.BookPanel.transform.position + new Vector3(num, -height / 2f);
		Vector3 global4 = this.BookPanel.transform.position + new Vector3(-num, -height / 2f);
		this.spineBottom = this.transformPoint(global);
		this.spineTop = this.transformPoint(global2);
		this.edgeBottomRight = this.transformPoint(global3);
		this.edgeBottomLeft = this.transformPoint(global4);
		this.radius1 = Vector2.Distance(this.spineBottom, this.edgeBottomRight);
		float num2 = num;
		float num3 = height;
		this.radius2 = Mathf.Sqrt(num2 * num2 + num3 * num3);
		this.ClippingPlane.rectTransform.sizeDelta = new Vector2(num2 * 2f, num3 + num2 * 2f);
		this.Shadow.sizeDelta = new Vector2(num2, num3 + num2 * 0.6f);
		this.ShadowLTR.sizeDelta = new Vector2(num2, num3 + num2 * 0.6f);
		this.NextPageClip.rectTransform.sizeDelta = new Vector2(num2, num3 + num2 * 0.6f);
		if (Global.self.CountPackPlayedTimes(0) >= 1)
		{
			List<Transform> list = new List<Transform>(this.bookPages);
			list.RemoveAt(list.Count - 1);
			list.RemoveAt(list.Count - 1);
			this.bookPages = list.ToArray();
		}
		this.LeftNextShadow = this.LeftNext.GetChild(1).GetComponent<RectTransform>();
		this.RightNextShadow = this.RightNext.GetChild(1).GetComponent<RectTransform>();
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00003028 File Offset: 0x00001228
	public Vector3 transformPoint(Vector3 global)
	{
		return this.BookPanel.InverseTransformPoint(global);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00003036 File Offset: 0x00001236
	private void Update()
	{
		if (this.pageDragging && this.interactable)
		{
			this.UpdateBook();
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003054 File Offset: 0x00001254
	public void UpdateBook()
	{
		this.follow = Vector3.Lerp(this.follow, this.transformPoint(Camera.main.GetMousePosition()), Time.deltaTime * 10f);
		if (this.mode == FlipMode.RightToLeft)
		{
			this.UpdateBookRTLToPoint(this.follow);
		}
		else
		{
			this.UpdateBookLTRToPoint(this.follow);
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000030B5 File Offset: 0x000012B5
	public void OnMouseDragRightPage()
	{
		if (!this.endButton.enabled)
		{
			return;
		}
		if (this.interactable && !this.activeCoroutine)
		{
			this.DragRightPageToPoint(this.transformPoint(Camera.main.GetMousePosition()));
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000030F4 File Offset: 0x000012F4
	public void OnMouseDragLeftPage()
	{
		if (!this.endButton.enabled)
		{
			return;
		}
		if (this.interactable && !this.activeCoroutine)
		{
			this.DragLeftPageToPoint(this.transformPoint(Camera.main.GetMousePosition()));
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003133 File Offset: 0x00001333
	public void OnRightHotspotPress()
	{
		if (!this.endButton.enabled)
		{
			return;
		}
		if (this.currentPage >= this.bookPages.Length - 1)
		{
			return;
		}
		Audio.self.playOneShot("4cfa8223-302f-42a3-b25f-29cd59260eb4", 1f);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003171 File Offset: 0x00001371
	public void OnLeftHotspotPress()
	{
		if (!this.endButton.enabled)
		{
			return;
		}
		if (this.currentPage <= 0)
		{
			return;
		}
		Audio.self.playOneShot("4cfa8223-302f-42a3-b25f-29cd59260eb4", 1f);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000031A6 File Offset: 0x000013A6
	public void OnMouseRelease()
	{
		if (this.interactable && !this.activeCoroutine)
		{
			this.ReleasePage();
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000031C4 File Offset: 0x000013C4
	public void UpdateBookLTRToPoint(Vector3 followLocation)
	{
		this.mode = FlipMode.LeftToRight;
		this.follow = followLocation;
		this.ShadowLTR.SetParent(this.ClippingPlane.transform, true);
		this.ShadowLTR.localPosition = Vector3.zero;
		this.ShadowLTR.localEulerAngles = Vector3.zero;
		this.Left.SetParent(this.ClippingPlane.transform, true);
		this.Right.SetParent(this.BookPanel, true);
		this.LeftNext.SetParent(this.BookPanel, true);
		this.corner = this.CalculateCornerPosition(followLocation);
		Vector3 vector;
		float num = this.CalculatePivotAngle(this.corner, this.edgeBottomLeft, out vector);
		if (num < 0f)
		{
			num += 180f;
		}
		this.UpdateOverlayShadows(vector, num, false);
		this.ClippingPlane.transform.eulerAngles = new Vector3(0f, 0f, num - 90f);
		this.ClippingPlane.transform.position = this.BookPanel.TransformPoint(vector);
		this.Left.transform.position = this.BookPanel.TransformPoint(this.corner);
		Vector2 vector2 = vector - this.corner;
		float num2 = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		this.Left.transform.eulerAngles = new Vector3(0f, 0f, num2 - 180f);
		this.NextPageClip.transform.eulerAngles = new Vector3(0f, 0f, num - 90f);
		this.NextPageClip.transform.position = this.BookPanel.TransformPoint(vector);
		this.LeftNext.SetParent(this.NextPageClip.transform, true);
		this.LeftNext.SetAsLastSibling();
		this.Right.SetParent(this.ClippingPlane.transform, true);
		this.Right.SetAsFirstSibling();
		this.ShadowLTR.SetParent(this.Left, true);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000033DC File Offset: 0x000015DC
	public void UpdateBookRTLToPoint(Vector3 followLocation)
	{
		this.mode = FlipMode.RightToLeft;
		this.follow = followLocation;
		this.Shadow.SetParent(this.ClippingPlane.transform, true);
		this.Shadow.localPosition = Vector3.zero;
		this.Shadow.localEulerAngles = Vector3.zero;
		this.Right.SetParent(this.ClippingPlane.transform, true);
		this.Left.SetParent(this.BookPanel, true);
		this.RightNext.SetParent(this.BookPanel, true);
		this.corner = this.CalculateCornerPosition(followLocation);
		Vector3 vector;
		float num = this.CalculatePivotAngle(this.corner, this.edgeBottomRight, out vector);
		if (num > -90f)
		{
			num -= 180f;
		}
		this.UpdateOverlayShadows(vector, num, true);
		this.ClippingPlane.rectTransform.pivot = new Vector2(1f, 0.35f);
		this.ClippingPlane.transform.eulerAngles = new Vector3(0f, 0f, num + 90f);
		this.ClippingPlane.transform.position = this.BookPanel.TransformPoint(vector);
		this.Right.position = this.BookPanel.TransformPoint(this.corner);
		Vector2 vector2 = vector - this.corner;
		float z = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		this.Right.eulerAngles = new Vector3(0f, 0f, z);
		this.NextPageClip.transform.eulerAngles = new Vector3(0f, 0f, num + 90f);
		this.NextPageClip.transform.position = this.BookPanel.TransformPoint(vector);
		this.RightNext.SetParent(this.NextPageClip.transform, true);
		this.RightNext.SetAsLastSibling();
		this.Left.SetParent(this.ClippingPlane.transform, true);
		this.Left.SetAsFirstSibling();
		this.Shadow.SetParent(this.Right, true);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00003604 File Offset: 0x00001804
	private float CalculatePivotAngle(Vector3 corner, Vector3 bookCorner, out Vector3 pivotPoint)
	{
		Vector3 b = (corner + bookCorner) / 2f;
		Vector2 vector = bookCorner - b;
		float num = Mathf.Atan2(vector.y, vector.x);
		float num2 = 1.5707964f - num;
		float num3 = b.x - vector.y * Mathf.Tan(num);
		num3 = this.normalizeT1X(num3, bookCorner, this.spineBottom);
		pivotPoint = new Vector3(num3, this.spineBottom.y);
		Vector2 vector2 = pivotPoint - b;
		return Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000036B8 File Offset: 0x000018B8
	private float normalizeT1X(float t1, Vector3 corner, Vector3 sb)
	{
		if (t1 > sb.x && sb.x > corner.x)
		{
			return sb.x;
		}
		if (t1 < sb.x && sb.x < corner.x)
		{
			return sb.x;
		}
		return t1;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003718 File Offset: 0x00001918
	private Vector3 CalculateCornerPosition(Vector3 followLocation)
	{
		this.follow = followLocation;
		Vector2 vector = new Vector2(this.follow.x - this.spineBottom.x, this.follow.y - this.spineBottom.y);
		float f = Mathf.Atan2(vector.y, vector.x);
		Vector3 vector2 = new Vector3(this.radius1 * Mathf.Cos(f), this.radius1 * Mathf.Sin(f)) + this.spineBottom;
		float num = Vector2.Distance(this.follow, this.spineBottom);
		Vector3 vector3;
		if (num < this.radius1)
		{
			vector3 = this.follow;
		}
		else
		{
			vector3 = vector2;
		}
		Vector2 vector4 = new Vector2(vector3.x - this.spineTop.x, vector3.y - this.spineTop.y);
		float f2 = Mathf.Atan2(vector4.y, vector4.x);
		Vector3 vector5 = new Vector3(this.radius2 * Mathf.Cos(f2), this.radius2 * Mathf.Sin(f2)) + this.spineTop;
		float num2 = Vector2.Distance(vector3, this.spineTop);
		if (num2 > this.radius2)
		{
			vector3 = vector5;
		}
		vector3.z = 0f;
		return vector3;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003880 File Offset: 0x00001A80
	public void DragRightPageToPoint(Vector3 point)
	{
		if (this.currentPage >= this.bookPages.Length - 1)
		{
			return;
		}
		this.pageDragging = true;
		this.mode = FlipMode.RightToLeft;
		this.follow = point;
		this.NextPageClip.rectTransform.pivot = new Vector2(0f, 0.12f);
		this.ClippingPlane.rectTransform.pivot = new Vector2(1f, 0.35f);
		this.Left.gameObject.SetActive(true);
		this.Left.pivot = new Vector2(0.5f, 0.5f);
		this.Left.transform.position = this.RightNext.transform.position;
		this.Left.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		if (this.currentPage < this.bookPages.Length)
		{
			this.CopyPage(this.Left, this.bookPages[this.currentPage]);
		}
		else
		{
			this.CopyPage(this.Left, this.background);
		}
		this.Left.transform.SetAsFirstSibling();
		this.Right.gameObject.SetActive(true);
		this.Right.transform.position = this.RightNext.transform.position;
		this.Right.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		if (this.currentPage < this.bookPages.Length - 1)
		{
			this.CopyPage(this.Right, this.bookPages[this.currentPage + 1]);
		}
		else
		{
			this.CopyPage(this.Right, this.background);
		}
		if (this.currentPage < this.bookPages.Length - 2)
		{
			this.CopyPage(this.RightNext, this.bookPages[this.currentPage + 2]);
		}
		else
		{
			this.CopyPage(this.RightNext, this.background);
		}
		this.LeftNext.transform.SetAsFirstSibling();
		if (this.enableShadowEffect)
		{
			this.Shadow.gameObject.SetActive(true);
		}
		this.UpdateBookRTLToPoint(this.follow);
		this.SetOverlayShadowsTo(true);
		this.LeftNextShadow.gameObject.SetActive(this.currentPage > 0);
		this.RightNextShadow.gameObject.SetActive(this.currentPage < this.bookPages.Length - 2);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003B18 File Offset: 0x00001D18
	public void DragLeftPageToPoint(Vector3 point)
	{
		if (this.currentPage <= 0)
		{
			return;
		}
		this.pageDragging = true;
		this.mode = FlipMode.LeftToRight;
		this.follow = point;
		this.NextPageClip.rectTransform.pivot = new Vector2(1f, 0.12f);
		this.ClippingPlane.rectTransform.pivot = new Vector2(0f, 0.35f);
		this.Right.gameObject.SetActive(true);
		this.Right.transform.position = this.LeftNext.transform.position;
		this.CopyPage(this.Right, this.bookPages[this.currentPage - 1]);
		this.Right.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		this.Right.transform.SetAsFirstSibling();
		this.Left.gameObject.SetActive(true);
		this.Left.pivot = new Vector2(1f, 0f);
		this.Left.transform.position = this.LeftNext.transform.position;
		this.Left.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		if (this.currentPage >= 2)
		{
			this.CopyPage(this.Left, this.bookPages[this.currentPage - 2]);
		}
		else
		{
			this.CopyPage(this.Left, this.background);
		}
		if (this.currentPage >= 3)
		{
			this.CopyPage(this.LeftNext, this.bookPages[this.currentPage - 3]);
		}
		else
		{
			this.CopyPage(this.LeftNext, this.background);
		}
		this.RightNext.transform.SetAsFirstSibling();
		if (this.enableShadowEffect)
		{
			this.ShadowLTR.gameObject.SetActive(true);
		}
		this.UpdateBookLTRToPoint(this.follow);
		this.SetOverlayShadowsTo(true);
		this.LeftNextShadow.gameObject.SetActive(this.currentPage > 3);
		this.RightNextShadow.gameObject.SetActive(this.currentPage < this.bookPages.Length - 1);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00003D6C File Offset: 0x00001F6C
	public void ReleasePage()
	{
		if (this.pageDragging)
		{
			Audio.self.playOneShot("169a8b5d-0f2a-4de4-b76a-143ff76774cd", 1f);
			this.pageDragging = false;
			float num = Vector2.Distance(this.corner, this.edgeBottomLeft);
			float num2 = Vector2.Distance(this.corner, this.edgeBottomRight);
			if (num2 < num && this.mode == FlipMode.RightToLeft)
			{
				this.TweenBack();
			}
			else if (num2 > num && this.mode == FlipMode.LeftToRight)
			{
				this.TweenBack();
			}
			else
			{
				this.TweenForward();
			}
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003E1C File Offset: 0x0000201C
	private void CopyPage(Transform to, Transform from)
	{
		for (int i = 0; i < to.childCount; i++)
		{
			Transform child = to.GetChild(i);
			if (!(child.tag == "Respawn"))
			{
				child.SetParent(this.pageList, false);
			}
		}
		from.SetParent(to, false);
		from.SetAsFirstSibling();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003E80 File Offset: 0x00002080
	private void UpdateSprites()
	{
		if (this.currentPage > 0 && this.currentPage <= this.bookPages.Length)
		{
			this.CopyPage(this.LeftNext, this.bookPages[this.currentPage - 1]);
		}
		else
		{
			this.CopyPage(this.LeftNext, this.background);
		}
		if (this.currentPage >= 0 && this.currentPage < this.bookPages.Length)
		{
			this.CopyPage(this.RightNext, this.bookPages[this.currentPage]);
		}
		else
		{
			this.CopyPage(this.RightNext, this.background);
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003F30 File Offset: 0x00002130
	public void TweenForward()
	{
		if (this.mode == FlipMode.RightToLeft)
		{
			base.StartCoroutine(this.TweenTo(this.edgeBottomLeft, this.returnFlipSpeed, new Action(this.Flip)));
		}
		else
		{
			base.StartCoroutine(this.TweenTo(this.edgeBottomRight, this.returnFlipSpeed, new Action(this.Flip)));
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003F98 File Offset: 0x00002198
	private void Flip()
	{
		if (this.mode == FlipMode.RightToLeft)
		{
			this.currentPage += 2;
		}
		else
		{
			this.currentPage -= 2;
		}
		this.SetOverlayShadowsTo(false);
		this.LeftNext.transform.SetParent(this.BookPanel.transform, true);
		this.Left.transform.SetParent(this.BookPanel.transform, true);
		this.LeftNext.transform.SetParent(this.BookPanel.transform, true);
		this.Left.gameObject.SetActive(false);
		this.Right.gameObject.SetActive(false);
		this.Right.transform.SetParent(this.BookPanel.transform, true);
		this.RightNext.transform.SetParent(this.BookPanel.transform, true);
		this.UpdateSprites();
		this.Shadow.gameObject.SetActive(false);
		this.ShadowLTR.gameObject.SetActive(false);
		if (this.OnFlip != null)
		{
			this.OnFlip.Invoke();
		}
		this.LeftHotspot.SetAsLastSibling();
		this.RightHotstop.SetAsLastSibling();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000040DC File Offset: 0x000022DC
	public void TweenBack()
	{
		if (this.mode == FlipMode.RightToLeft)
		{
			base.StartCoroutine(this.TweenTo(this.edgeBottomRight, this.returnFlipSpeed, delegate
			{
				this.Left.gameObject.SetActive(false);
				this.Right.gameObject.SetActive(false);
				this.UpdateSprites();
				this.RightNext.transform.SetParent(this.BookPanel.transform);
				this.Right.transform.SetParent(this.BookPanel.transform);
				this.SetOverlayShadowsTo(false);
				this.pageDragging = false;
				this.LeftHotspot.SetAsLastSibling();
				this.RightHotstop.SetAsLastSibling();
			}));
		}
		else
		{
			base.StartCoroutine(this.TweenTo(this.edgeBottomLeft, this.returnFlipSpeed, delegate
			{
				this.Left.gameObject.SetActive(false);
				this.Right.gameObject.SetActive(false);
				this.UpdateSprites();
				this.LeftNext.transform.SetParent(this.BookPanel.transform);
				this.Left.transform.SetParent(this.BookPanel.transform);
				this.SetOverlayShadowsTo(false);
				this.pageDragging = false;
				this.LeftHotspot.SetAsLastSibling();
				this.RightHotstop.SetAsLastSibling();
			}));
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00004144 File Offset: 0x00002344
	public IEnumerator TweenTo(Vector3 to, float speed, Action onFinish)
	{
		this.activeCoroutine = true;
		List<Vector3> points = new List<Vector3>();
		if (this.follow.y < to.y)
		{
			points.Add(new Vector3(this.follow.x, to.y + 1f));
		}
		points.Add(to);
		foreach (Vector3 point in points)
		{
			while (Vector3.Distance(this.follow, point) > Mathf.Epsilon)
			{
				yield return null;
				Vector3 pos = Vector3.MoveTowards(this.follow, point, speed * Time.deltaTime);
				if (this.mode == FlipMode.RightToLeft)
				{
					this.UpdateBookRTLToPoint(pos);
				}
				else
				{
					this.UpdateBookLTRToPoint(pos);
				}
			}
		}
		if (onFinish != null)
		{
			onFinish();
		}
		this.activeCoroutine = false;
		yield break;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004174 File Offset: 0x00002374
	private void UpdateOverlayShadows(Vector3 pivotPoint, float pivotAngle, bool rightToLeft = true)
	{
		RectTransform shadow = (!rightToLeft) ? this.LeftNextShadow : this.RightNextShadow;
		RectTransform shadow2 = (!rightToLeft) ? this.RightNextShadow : this.LeftNextShadow;
		RectTransform shadow3 = (!rightToLeft) ? this.ShadowLTR.GetChild(0).GetComponent<RectTransform>() : this.Shadow.GetChild(0).GetComponent<RectTransform>();
		RectTransform shadow4 = (!rightToLeft) ? this.Shadow.GetChild(0).GetComponent<RectTransform>() : this.ShadowLTR.GetChild(0).GetComponent<RectTransform>();
		float num = (pivotPoint.x <= this.spineBottom.x) ? this.edgeBottomLeft.x : this.edgeBottomRight.x;
		float num2 = Mathf.Abs(pivotPoint.x - num);
		bool flag = false;
		pivotAngle = Mathf.Abs(pivotAngle);
		if (pivotAngle < 90f)
		{
			pivotAngle = 180f - pivotAngle;
			flag = true;
		}
		float num3 = Mathf.Tan(pivotAngle * 0.017453292f);
		if (flag)
		{
			num2 = Mathf.Abs(this.Height / num3) + num2;
		}
		float num4 = Mathf.Abs(num3 * num2);
		float num7;
		if (num4 > this.Height)
		{
			float num5 = num4 - this.Height;
			float num6 = Mathf.Abs(num5 / num3);
			num7 = (num2 + num6) * this.Height / 2f;
		}
		else
		{
			num7 = num4 * num2 / 2f;
		}
		float num8 = num7 / (this.radius1 * this.Height);
		this.ChangeShadowAlpha(shadow, this.bigPageShadowAlpha, num8);
		Vector2 vector = pivotPoint - this.corner;
		Vector3 vector3;
		if (rightToLeft)
		{
			Vector3 vector2 = new Vector3(-vector.y, vector.x);
			vector3 = vector2.normalized * this.Height + this.corner;
		}
		else
		{
			Vector3 vector4 = new Vector3(vector.y, -vector.x);
			vector3 = vector4.normalized * this.Height + this.corner;
		}
		Vector3 a = vector3;
		Vector2 vector5 = (a - this.corner) / 2f + this.corner;
		int num9 = (!rightToLeft) ? -1 : 1;
		if (vector5.x * (float)num9 < 0f)
		{
			num8 = Mathf.Abs(vector5.x / this.radius1);
		}
		else
		{
			num8 = 0f;
		}
		this.ChangeShadowAlpha(shadow2, this.bigPageShadowAlpha, 1f - num8);
		this.ChangeShadowAlpha(shadow3, this.innerPageShadowAlpha, num8);
		this.ChangeShadowAlpha(shadow4, this.innerPageShadowAlpha, 1f);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004440 File Offset: 0x00002640
	private void ChangeShadowAlpha(RectTransform shadow, AnimationCurve curve, float t)
	{
		Color color = shadow.GetComponent<Image>().color;
		color.a = curve.Evaluate(t);
		shadow.GetComponent<Image>().color = color;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00004473 File Offset: 0x00002673
	private void SetOverlayShadowsTo(bool status)
	{
		this.LeftNextShadow.gameObject.SetActive(status);
		this.RightNextShadow.gameObject.SetActive(status);
	}

	// Token: 0x0400008E RID: 142
	public Canvas canvas;

	// Token: 0x0400008F RID: 143
	[SerializeField]
	private RectTransform BookPanel;

	// Token: 0x04000090 RID: 144
	public Transform background;

	// Token: 0x04000091 RID: 145
	public Transform pageList;

	// Token: 0x04000092 RID: 146
	public Transform[] bookPages;

	// Token: 0x04000093 RID: 147
	public bool interactable = true;

	// Token: 0x04000094 RID: 148
	public bool enableShadowEffect = true;

	// Token: 0x04000095 RID: 149
	public float returnFlipSpeed = 0.25f;

	// Token: 0x04000096 RID: 150
	public int currentPage;

	// Token: 0x04000097 RID: 151
	public AnimationCurve bigPageShadowAlpha;

	// Token: 0x04000098 RID: 152
	public AnimationCurve innerPageShadowAlpha;

	// Token: 0x04000099 RID: 153
	public Image ClippingPlane;

	// Token: 0x0400009A RID: 154
	public Image NextPageClip;

	// Token: 0x0400009B RID: 155
	public RectTransform Shadow;

	// Token: 0x0400009C RID: 156
	public RectTransform ShadowLTR;

	// Token: 0x0400009D RID: 157
	public RectTransform Left;

	// Token: 0x0400009E RID: 158
	public RectTransform LeftNext;

	// Token: 0x0400009F RID: 159
	public RectTransform LeftHotspot;

	// Token: 0x040000A0 RID: 160
	public RectTransform Right;

	// Token: 0x040000A1 RID: 161
	public RectTransform RightNext;

	// Token: 0x040000A2 RID: 162
	public RectTransform RightHotstop;

	// Token: 0x040000A3 RID: 163
	public UnityEvent OnFlip;

	// Token: 0x040000A4 RID: 164
	private RectTransform LeftNextShadow;

	// Token: 0x040000A5 RID: 165
	private RectTransform RightNextShadow;

	// Token: 0x040000A6 RID: 166
	private float radius1;

	// Token: 0x040000A7 RID: 167
	private float radius2;

	// Token: 0x040000A8 RID: 168
	private Vector3 spineBottom;

	// Token: 0x040000A9 RID: 169
	private Vector3 spineTop;

	// Token: 0x040000AA RID: 170
	private Vector3 corner;

	// Token: 0x040000AB RID: 171
	private Vector3 edgeBottomRight;

	// Token: 0x040000AC RID: 172
	private Vector3 edgeBottomLeft;

	// Token: 0x040000AD RID: 173
	private Vector3 follow;

	// Token: 0x040000AE RID: 174
	private bool pageDragging;

	// Token: 0x040000AF RID: 175
	private FlipMode mode;

	// Token: 0x040000B0 RID: 176
	private bool activeCoroutine;

	// Token: 0x040000B1 RID: 177
	private examPackUI endButton;
}
