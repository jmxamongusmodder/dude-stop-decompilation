using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class CupStone_Draggable : MonoBehaviour
{
	// Token: 0x1700002A RID: 42
	// (get) Token: 0x0600157D RID: 5501 RVA: 0x000415F7 File Offset: 0x0003F9F7
	// (set) Token: 0x0600157E RID: 5502 RVA: 0x00041600 File Offset: 0x0003FA00
	private Vector2 endPos
	{
		get
		{
			return this._endPos;
		}
		set
		{
			if (this._endPos != Vector2.zero && value != this._endPos)
			{
				Audio.self.playOneShot("f67a1140-23bc-4308-b66a-7dfcabe864f5", 1f);
			}
			this._endPos = value;
		}
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x0004164F File Offset: 0x0003FA4F
	private void Update()
	{
		this.CheckMousePosition();
		this.CheckStonePosition();
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x00041660 File Offset: 0x0003FA60
	private void Start()
	{
		BoxCollider2D component = base.GetComponent<BoxCollider2D>();
		if (this.goingLeft)
		{
			this.squareSize = component.size.y * base.transform.lossyScale.y;
		}
		else if (this.goingUp)
		{
			this.squareSize = component.size.x * base.transform.lossyScale.x;
		}
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000416E0 File Offset: 0x0003FAE0
	private void OnMouseDown()
	{
		this.dragged = true;
		this.mouseStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.startPos = base.transform.position;
		this.startOffset = ((!this.longPiece) ? (this.squareSize * 0.5f) : this.squareSize);
		if (this.goingUp)
		{
			this.startOffset *= Mathf.Sign(this.mouseStart.y - base.transform.position.y);
		}
		else
		{
			this.startOffset *= Mathf.Sign(this.mouseStart.x - base.transform.position.x);
		}
		if (!this.activeGlow)
		{
			base.StartCoroutine(this.GlowingCoroutine());
		}
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000417D6 File Offset: 0x0003FBD6
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		this.CheckPiecePosition();
		this.CheckVictoryCondition();
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000417F8 File Offset: 0x0003FBF8
	private void CheckPiecePosition()
	{
		if (this.finalPosition == Vector2.zero)
		{
			this.set = true;
		}
		else if (Vector2.Distance(base.transform.localPosition, this.finalPosition) < 0.1f)
		{
			this.set = true;
			base.enabled = false;
			Audio.self.playOneShot("94fe07b3-59d3-429e-a0b5-e858c634ad2d", 1f);
			Global.self.currPuzzle.GetComponent<AudioVoice_CupStone>().insertStone();
		}
		else
		{
			this.set = false;
		}
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x00041890 File Offset: 0x0003FC90
	private void CheckVictoryCondition()
	{
		bool flag = true;
		foreach (CupStone_Draggable cupStone_Draggable in this.GetComponentsInPuzzleStats(false))
		{
			flag &= cupStone_Draggable.set;
		}
		if (flag)
		{
			Global.self.currPuzzle.GetComponent<AudioVoice_CupStone>().insertLast();
			Audio.self.playOneShot("cf97b3da-9d52-426c-8cbb-97b5c7b66b5d", 1f);
			this.GetComponentInPuzzleStats<CupStone_Stone>().GetComponent<Animator>().SetBool("play", true);
			foreach (CupStone_Draggable cupStone_Draggable2 in this.GetComponentsInPuzzleStats(false))
			{
				cupStone_Draggable2.enabled = false;
			}
		}
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x00041940 File Offset: 0x0003FD40
	private void CheckMousePosition()
	{
		if (!this.dragged)
		{
			return;
		}
		Vector2 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 vector = a - this.mouseStart;
		vector /= this.squareSize;
		vector.x = (float)Mathf.RoundToInt(vector.x);
		vector.y = (float)Mathf.RoundToInt(vector.y);
		if (!this.goingUp)
		{
			vector.y = 0f;
		}
		if (!this.goingLeft)
		{
			vector.x = 0f;
		}
		while (!this.CanMove(vector))
		{
			vector = Vector2.MoveTowards(vector, Vector2.zero, 1f);
		}
		this.endPos = this.startPos + this.squareSize * vector;
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x00041A20 File Offset: 0x0003FE20
	private void CheckStonePosition()
	{
		if (this.endPos != Vector2.zero && base.transform.position != this.endPos)
		{
			base.transform.position = Vector2.Lerp(base.transform.position, this.endPos, this.lerpSpeed * Time.deltaTime);
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.endPos, this.moveSpeed * Time.deltaTime);
			if (base.transform.position == this.endPos)
			{
				this.CheckPiecePosition();
				this.CheckVictoryCondition();
			}
		}
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x00041AFC File Offset: 0x0003FEFC
	private bool CanMove(Vector2 direction)
	{
		int layer = base.gameObject.layer;
		base.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
		Vector2 vector = (!this.goingUp) ? (Vector2.right * this.startOffset) : (Vector2.up * this.startOffset);
		vector += this.startPos;
		int num = (!this.longPiece) ? 2 : 3;
		if (Mathf.Sign(direction.x + direction.y) != Mathf.Sign(this.startOffset))
		{
			direction += direction.normalized * (float)(num - 1);
		}
		RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, direction, this.squareSize * direction.magnitude);
		Debug.DrawLine(vector, vector + direction * this.squareSize, Color.red);
		base.gameObject.layer = layer;
		return raycastHit2D.collider == null;
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x00041C10 File Offset: 0x00040010
	private IEnumerator GlowingCoroutine()
	{
		if (this.glow == null || !base.enabled)
		{
			yield break;
		}
		this.activeGlow = true;
		float timer = 0f;
		float target = this.glowTime;
		Color newColor = this.glow.color;
		while (base.enabled && this.dragged)
		{
			while (timer != target)
			{
				timer = Mathf.MoveTowards(timer, target, Time.deltaTime);
				newColor.a = Mathf.Lerp(0f, this.glowAlpha, timer / this.glowTime);
				this.glow.color = newColor;
				if (timer == 0f)
				{
					break;
				}
				if (timer == target)
				{
					target = 0f;
				}
				yield return null;
			}
			target = this.glowTime;
		}
		this.activeGlow = false;
		yield break;
	}

	// Token: 0x04001335 RID: 4917
	public float lerpSpeed;

	// Token: 0x04001336 RID: 4918
	public float moveSpeed;

	// Token: 0x04001337 RID: 4919
	[HideInInspector]
	public float squareSize;

	// Token: 0x04001338 RID: 4920
	public bool goingUp;

	// Token: 0x04001339 RID: 4921
	public bool goingLeft;

	// Token: 0x0400133A RID: 4922
	public Vector2 finalPosition = Vector2.zero;

	// Token: 0x0400133B RID: 4923
	public bool longPiece;

	// Token: 0x0400133C RID: 4924
	public SpriteRenderer glow;

	// Token: 0x0400133D RID: 4925
	public float glowTime;

	// Token: 0x0400133E RID: 4926
	public float glowAlpha;

	// Token: 0x0400133F RID: 4927
	private bool dragged;

	// Token: 0x04001340 RID: 4928
	private bool activeGlow;

	// Token: 0x04001341 RID: 4929
	private Vector2 mouseStart;

	// Token: 0x04001342 RID: 4930
	private Vector2 startPos;

	// Token: 0x04001343 RID: 4931
	private float startOffset;

	// Token: 0x04001344 RID: 4932
	private Vector2 _endPos = Vector2.zero;

	// Token: 0x04001345 RID: 4933
	private bool set;
}
