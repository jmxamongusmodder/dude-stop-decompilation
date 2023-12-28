using System;
using UnityEngine;

// Token: 0x020003C6 RID: 966
public class PuzzleBookmark : MonoBehaviour
{
	// Token: 0x17000049 RID: 73
	// (set) Token: 0x06001830 RID: 6192 RVA: 0x00054439 File Offset: 0x00052839
	private bool @out
	{
		set
		{
			if (this._out != value)
			{
				this._out = value;
			}
		}
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x00054450 File Offset: 0x00052850
	private void Update()
	{
		if (base.transform.position.y < this.winPosition)
		{
			this.shadows.gameObject.SetActive(false);
		}
		else
		{
			this.shadows.gameObject.SetActive(true);
		}
		if (this.dragged)
		{
			Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			a.z = 0f;
			if (this.delta.z == 1f)
			{
				this.delta = a - base.transform.position;
			}
			else
			{
				a -= this.delta;
				if (a.x > this.startingPosition.x && a.y < this.startingPosition.y)
				{
					Vector2 vector = a - this.startingPosition;
					float num = Mathf.Max(vector.x * 0.66f, -vector.y);
					base.transform.position = this.startingPosition + new Vector3(num, -num, 0f);
				}
				else if (this.startingPosition.x - a.x > 0.1f || a.y - this.startingPosition.y > 0.1f)
				{
					this.ignoreWin = true;
					this.OnMouseUp();
				}
			}
		}
		else if (base.transform.position.y < this.winPosition && !this.ignoreWin)
		{
			this.GetPuzzleStats().GetComponent<AudioVoice_Bookmark>().finishLevel();
			Global.LevelCompleted(0f, true);
		}
		else if (base.transform.position != this.startingPosition && this.startingPosition.z != 1f)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.startingPosition, Time.deltaTime * this.returnSpeed);
			if (!(base.transform.position != this.startingPosition) || this.startingPosition.z == 1f)
			{
			}
		}
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000546AD File Offset: 0x00052AAD
	private void OnMouseEnter()
	{
		if (!base.enabled || this.dragged)
		{
			return;
		}
		Audio.self.playOneShot("972c7d07-2020-4056-8ed7-5f3e4e64dc31", 1f);
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000546DC File Offset: 0x00052ADC
	private void OnMouseDown()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.startingPosition.z == 1f)
		{
			this.startingPosition = base.transform.position;
		}
		this.dragged = true;
		this.ignoreWin = false;
		Audio.self.playOneShot("c8b83fa7-a572-4ffc-be0a-727164eb2323", 1f);
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x0005473E File Offset: 0x00052B3E
	private void OnMouseUp()
	{
		if (!this.dragged)
		{
			return;
		}
		this.dragged = false;
		this.delta.z = 1f;
		Audio.self.playOneShot("cc4c04a0-d2f7-4a5c-b278-2b06a74288f6", 1f);
	}

	// Token: 0x0400161C RID: 5660
	public Transform shadows;

	// Token: 0x0400161D RID: 5661
	public float winPosition;

	// Token: 0x0400161E RID: 5662
	public float returnSpeed = 2f;

	// Token: 0x0400161F RID: 5663
	public float dragQuotient = 0.66f;

	// Token: 0x04001620 RID: 5664
	private bool dragged;

	// Token: 0x04001621 RID: 5665
	private bool ignoreWin;

	// Token: 0x04001622 RID: 5666
	private Vector3 startingPosition = new Vector3(0f, 0f, 1f);

	// Token: 0x04001623 RID: 5667
	private Vector3 delta = new Vector3(0f, 0f, 1f);

	// Token: 0x04001624 RID: 5668
	private bool _out;
}
