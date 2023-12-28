using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200045C RID: 1116
public class PuzzleSwipePhoto_Phone : MonoBehaviour, TransitionProcessor
{
	// Token: 0x06001CA7 RID: 7335 RVA: 0x0007AEA1 File Offset: 0x000792A1
	private void Start()
	{
		this.InitPictures();
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x0007AEA9 File Offset: 0x000792A9
	private void Update()
	{
		this.CheckMouse();
		this.CheckPosition();
		this.UpdateShaderPosition();
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x0007AEC0 File Offset: 0x000792C0
	private void SetThumbnailActivityTo(bool active)
	{
		foreach (PuzzleSwipePhoto_Thumbnail puzzleSwipePhoto_Thumbnail in this.albumScreen.GetComponentsInChildren<PuzzleSwipePhoto_Thumbnail>())
		{
			if (puzzleSwipePhoto_Thumbnail.onScreen)
			{
				puzzleSwipePhoto_Thumbnail.enabled = active;
				puzzleSwipePhoto_Thumbnail.clickable = active;
			}
		}
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x0007AF0C File Offset: 0x0007930C
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = true;
		if (this.startingPosition == -Vector2.one)
		{
			this.startingPosition = base.transform.position;
			this.startingRotation = base.transform.localEulerAngles.z;
			this.startingScale = base.transform.localScale.x;
		}
		this.delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - base.transform.position;
		Audio.self.playOneShot("2ab2073f-e844-4cf5-930d-d14ac700373e", 1f);
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x0007AFD0 File Offset: 0x000793D0
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		float num = Mathf.Clamp(base.transform.position.x, this.startingPosition.x, 0f);
		float num2 = (num - this.startingPosition.x) / -this.startingPosition.x;
		float target = (num2 <= this.thresholdDistance) ? this.startingPosition.x : 0f;
		bool active = num2 > this.thresholdDistance;
		base.StartCoroutine(this.FinishingCoroutine(target, active));
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x0007B074 File Offset: 0x00079474
	private void CheckMouse()
	{
		if (!this.dragged)
		{
			return;
		}
		base.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - this.delta.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - this.delta.y);
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x0007B0E4 File Offset: 0x000794E4
	private void CheckPosition()
	{
		if (this.startingPosition == -Vector2.one)
		{
			return;
		}
		float num = Mathf.Clamp(base.transform.position.x, this.startingPosition.x, 0f);
		float num2 = Mathf.Clamp(base.transform.position.y, 0f, this.startingPosition.y);
		float a = (num - this.startingPosition.x) / -this.startingPosition.x;
		float b = (num2 - this.startingPosition.y) / -this.startingPosition.y;
		float t = Mathf.Max(a, b);
		base.transform.position = Vector3.Lerp(this.startingPosition, Vector2.zero, t);
		base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingRotation, 0f, t));
		base.transform.localScale = Vector3.Lerp(new Vector3(this.startingScale, this.startingScale), Vector3.one, t);
		if (!this.isMoved && Vector2.Distance(base.transform.position, this.startingPosition) > 1f)
		{
			this.isMoved = true;
		}
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x0007B254 File Offset: 0x00079654
	private void InitPictures()
	{
		if (this.pictures == null)
		{
			this.pictures = (from x in base.GetComponentsInChildren<SpriteRenderer>(true)
			where x.material.name.Contains("BoxMask")
			select x).ToList<SpriteRenderer>();
		}
		this.text = this.GetComponentInPuzzleStats(true);
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x0007B2B0 File Offset: 0x000796B0
	private void UpdateShaderPosition()
	{
		Vector2 vector = Camera.main.WorldToViewportPoint(this.pictureScreen.bounds.center);
		Vector2 v = this.originalSize;
		v.x *= base.transform.lossyScale.x;
		v.y *= base.transform.lossyScale.y;
		v = Camera.main.WorldToViewportPoint(v) - new Vector3(0.5f, 0.5f);
		foreach (SpriteRenderer spriteRenderer in this.pictures)
		{
			float num = base.transform.eulerAngles.z * 0.017453292f;
			num = Mathf.Round(num * 100f) / 100f;
			spriteRenderer.material.SetFloat("_Bottom", vector.y);
			spriteRenderer.material.SetFloat("_Left", vector.x);
			spriteRenderer.material.SetFloat("_Width", v.x);
			spriteRenderer.material.SetFloat("_Height", v.y);
			spriteRenderer.material.SetFloat("_Angle", num);
			spriteRenderer.material.SetFloat("_AlphaOnly", 0f);
		}
		this.text.material.SetFloat("_Bottom", vector.y);
		this.text.material.SetFloat("_Left", vector.x);
		this.text.material.SetFloat("_Width", v.x);
		this.text.material.SetFloat("_Height", v.y);
		this.text.material.SetFloat("_Angle", base.transform.eulerAngles.z * 0.017453292f);
		this.text.material.SetColor("_Color", this.text.color);
		this.text.material.SetFloat("_AlphaOnly", 1f);
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x0007B534 File Offset: 0x00079934
	private IEnumerator FinishingCoroutine(float target, bool active)
	{
		foreach (Collider2D collider2D in base.GetComponents<Collider2D>())
		{
			collider2D.enabled = false;
		}
		float x = base.transform.position.x;
		while (x != target)
		{
			x = Mathf.MoveTowards(x, target, this.moveSpeed * Time.deltaTime);
			float t = (x - this.startingPosition.x) / -this.startingPosition.x;
			base.transform.position = Vector2.Lerp(this.startingPosition, Vector2.zero, t);
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.startingRotation, 0f, t));
			base.transform.localScale = Vector3.Lerp(new Vector3(this.startingScale, this.startingScale), Vector3.one, t);
			yield return null;
		}
		foreach (PuzzleSwipePhoto_Photo puzzleSwipePhoto_Photo in this.GetComponentsInPuzzleStats(false))
		{
			if (puzzleSwipePhoto_Photo.onScreen)
			{
				puzzleSwipePhoto_Photo.clickable = active;
			}
		}
		this.SetThumbnailActivityTo(active);
		if (active)
		{
			base.GetComponent<PolygonCollider2D>().enabled = true;
		}
		else
		{
			base.GetComponent<BoxCollider2D>().enabled = true;
			if (this.albumSwiped)
			{
				Global.LevelFailed(0f, true);
			}
			else if (!this.returnCommented && this.isMoved)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_SwipePhoto>().ReturnBeforeLastGood();
				this.returnCommented = true;
			}
		}
		yield break;
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x0007B55D File Offset: 0x0007995D
	public void TransitionUpdate()
	{
		this.InitPictures();
		this.UpdateShaderPosition();
	}

	// Token: 0x04001B1E RID: 6942
	public Transform albumScreen;

	// Token: 0x04001B1F RID: 6943
	[HideInInspector]
	public bool albumSwiped;

	// Token: 0x04001B20 RID: 6944
	public SpriteRenderer pictureScreen;

	// Token: 0x04001B21 RID: 6945
	public Vector2 originalSize;

	// Token: 0x04001B22 RID: 6946
	public float moveSpeed;

	// Token: 0x04001B23 RID: 6947
	[Range(0.01f, 1f)]
	public float thresholdDistance = 0.5f;

	// Token: 0x04001B24 RID: 6948
	private bool dragged;

	// Token: 0x04001B25 RID: 6949
	private Vector2 delta;

	// Token: 0x04001B26 RID: 6950
	private Vector2 startingPosition = -Vector3.one;

	// Token: 0x04001B27 RID: 6951
	private bool isMoved;

	// Token: 0x04001B28 RID: 6952
	private List<SpriteRenderer> pictures;

	// Token: 0x04001B29 RID: 6953
	private Text text;

	// Token: 0x04001B2A RID: 6954
	private float startingRotation;

	// Token: 0x04001B2B RID: 6955
	private float startingScale;

	// Token: 0x04001B2C RID: 6956
	private bool returnCommented;
}
