using System;
using UnityEngine;

// Token: 0x0200043A RID: 1082
public class PuzzlePainting_Painting : MonoBehaviour, TransitionProcessor
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000722FF File Offset: 0x000706FF
	// (set) Token: 0x06001B94 RID: 7060 RVA: 0x00072307 File Offset: 0x00070707
	private bool snapped
	{
		get
		{
			return this._snapped;
		}
		set
		{
			if (value && this._snapped != value)
			{
				Audio.self.playOneShot("0ac9b8a2-6228-4529-8c22-d59a4f6a7e92", 1f);
			}
			this._snapped = value;
		}
	}

	// Token: 0x06001B95 RID: 7061 RVA: 0x00072338 File Offset: 0x00070738
	private void OnDrawGizmos()
	{
		if (!base.enabled)
		{
			return;
		}
		Vector3 a = Quaternion.Euler(0f, 0f, -this.snapAngle) * Vector3.down;
		Vector3 a2 = Quaternion.Euler(0f, 0f, this.snapAngle) * Vector3.down;
		Gizmos.DrawLine(base.transform.position, base.transform.position + a * 5f);
		Gizmos.DrawLine(base.transform.position, base.transform.position + a2 * 5f);
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x000723E8 File Offset: 0x000707E8
	private void Start()
	{
		this.voice = this.GetPuzzleStats().GetComponent<AudioVoice_Painting>();
		PuzzlePainting_Painting.levelFinished = false;
		this.body = base.GetComponent<Rigidbody2D>();
		this.joint = base.GetComponent<HingeJoint2D>();
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x0007241C File Offset: 0x0007081C
	private void Update()
	{
		this.CheckPaintingPosition();
		if (!this.dragged && this.snapped && this.voice.canFinishLevel && !PuzzlePainting_Painting.levelFinished)
		{
			this.voice.finishLevel();
			Global.LevelCompleted(0f, true);
			base.enabled = false;
			PuzzlePainting_Painting.levelFinished = true;
		}
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x00072484 File Offset: 0x00070884
	public void TransitionUpdate()
	{
		if (this.body == null || this.joint == null)
		{
			return;
		}
		if (this.body.bodyType == RigidbodyType2D.Dynamic)
		{
			this.joint.connectedAnchor = base.transform.position;
		}
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x000724E0 File Offset: 0x000708E0
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.body.bodyType == RigidbodyType2D.Kinematic)
		{
			this.body.bodyType = RigidbodyType2D.Dynamic;
		}
		Audio.self.playOneShot("78d9ba73-c314-4920-bcb4-b6c4603d82fa", 1f);
		this.dragged = true;
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x00072534 File Offset: 0x00070934
	private void OnMouseUp()
	{
		if (!base.enabled)
		{
			return;
		}
		this.dragged = false;
		this.delta = -1f;
		if (this.snapped)
		{
			this.voice.failedLevel(base.transform);
		}
		else
		{
			this.voice.fixedLevel(base.transform);
		}
		this.body.velocity = Vector2.zero;
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x000725A4 File Offset: 0x000709A4
	private void CheckPaintingPosition()
	{
		if (!this.dragged)
		{
			return;
		}
		this.body.angularVelocity = 0f;
		Vector3 mousePosition = Input.mousePosition;
		Vector3 vector = Camera.main.WorldToScreenPoint(base.transform.localPosition);
		Vector2 vector2 = new Vector2(mousePosition.x - vector.x, mousePosition.y - vector.y);
		float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		num += 90f;
		if (this.delta == -1f)
		{
			this.delta = num - base.transform.eulerAngles.z;
		}
		else
		{
			num -= this.delta;
			if (Mathf.Abs(Mathf.DeltaAngle(num, 180f)) < this.snapDist)
			{
				num = 180f;
				this.body.bodyType = RigidbodyType2D.Kinematic;
				this.body.velocity = Vector2.zero;
				this.snapped = true;
			}
			else if (Mathf.Abs(Mathf.DeltaAngle(num, -this.snapAngle)) < this.snapDist)
			{
				num = -this.snapAngle;
				this.body.bodyType = RigidbodyType2D.Kinematic;
				this.body.velocity = Vector2.zero;
				this.snapped = true;
			}
			else if (Mathf.Abs(Mathf.DeltaAngle(num, this.snapAngle)) < this.snapDist)
			{
				num = this.snapAngle;
				this.body.bodyType = RigidbodyType2D.Kinematic;
				this.body.velocity = Vector2.zero;
				this.snapped = true;
			}
			else
			{
				this.body.bodyType = RigidbodyType2D.Dynamic;
				this.snapped = false;
			}
			this.body.MoveRotation(num);
		}
	}

	// Token: 0x040019D8 RID: 6616
	public float snapAngle;

	// Token: 0x040019D9 RID: 6617
	public float snapDist;

	// Token: 0x040019DA RID: 6618
	private bool _snapped;

	// Token: 0x040019DB RID: 6619
	private bool dragged;

	// Token: 0x040019DC RID: 6620
	private float delta = -1f;

	// Token: 0x040019DD RID: 6621
	private Rigidbody2D body;

	// Token: 0x040019DE RID: 6622
	private HingeJoint2D joint;

	// Token: 0x040019DF RID: 6623
	private AudioVoice_Painting voice;

	// Token: 0x040019E0 RID: 6624
	private static bool levelFinished;
}
