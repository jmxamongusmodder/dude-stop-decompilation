using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200045F RID: 1119
public class PuzzleTetris : MonoBehaviour
{
	// Token: 0x06001CC5 RID: 7365 RVA: 0x0007C208 File Offset: 0x0007A608
	private void Start()
	{
		this.timer = this.period;
		this.botRender = this.bottom.GetComponent<SpriteRenderer>();
		this.render = base.GetComponent<SpriteRenderer>();
		if (Global.self.CountPackPlayedTimes(0) > 0)
		{
			this.period = this.periodOnSecondRun;
		}
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x0007C25C File Offset: 0x0007A65C
	private void OnDrawGizmos()
	{
		List<Vector3> list = new List<Vector3>();
		Vector3 position = base.transform.position;
		Gizmos.color = Color.red;
		list.Add(new Vector3(position.x, position.y - 0.184f));
		list.Add(new Vector3(position.x, position.y - 0.552f));
		list.Add(new Vector3(position.x, position.y + 0.184f));
		list.Add(new Vector3(position.x, position.y + 0.552f));
		foreach (Vector3 vector in list)
		{
			Gizmos.DrawLine(vector, vector + new Vector3(0.375f, 0f));
			Gizmos.DrawLine(vector, vector - new Vector3(0.375f, 0f));
		}
		Gizmos.DrawLine(list[1], list[1] - new Vector3(0f, 0.368f));
	}

	// Token: 0x06001CC7 RID: 7367 RVA: 0x0007C3A4 File Offset: 0x0007A7A4
	private void Update()
	{
		this.CheckInput();
		this.Raycast();
		if (this.moving)
		{
			this.timer -= Time.deltaTime;
			if (this.timer < 0f)
			{
				if (!this.canMoveDown)
				{
					this.moving = false;
				}
				else
				{
					this.timer = this.period;
					base.transform.position -= Vector3.up * 0.375f;
					Audio.self.playOneShot("0f07f41f-1b99-403f-b072-6717381b90ad", 1f);
					this.Raycast();
					if (--this.ticks == 0)
					{
						this.moving = false;
					}
				}
			}
			if (this.moving)
			{
				if (this.moveTimer > 0f)
				{
					this.moveTimer = Mathf.MoveTowards(this.moveTimer, 0f, Time.deltaTime);
				}
				else if (this.movingLeft && this.canMoveLeft)
				{
					base.transform.position -= Vector3.right * 0.375f;
					this.moveTimer = this.waitBetweenMoves;
				}
				else if (this.movingRight && this.canMoveRight)
				{
					base.transform.position += Vector3.right * 0.375f;
					this.moveTimer = this.waitBetweenMoves;
				}
			}
		}
		else if (!this.canMoveDown)
		{
			Audio.self.playOneShot("4271235a-80d7-4621-a890-087f0eda6362", 1f);
			Global.LevelCompleted(0f, true);
		}
		else if (!this.flashing)
		{
			Audio.self.playOneShot("1893dd13-af91-4231-89dc-7e9e1ba17a9d", 1f);
			this.flashing = true;
			this.flashTicksLeft = this.flashTicks;
			this.flashTimer = this.flashPeriod;
		}
		if (this.flashing)
		{
			if (this.flashTicksLeft > 0)
			{
				this.flashTimer -= Time.deltaTime;
				if (this.flashTimer < 0f)
				{
					this.flashTimer = this.flashPeriod;
					this.botRender.material.SetFloat("_Alpha", (float)(this.flashTicksLeft % 2));
					this.render.material.SetFloat("_Alpha", (float)(this.flashTicksLeft-- % 2));
				}
			}
			else if (this.flashTicksLeft == 0)
			{
				this.flashTimer -= Time.deltaTime;
				if (this.flashTimer < 0f)
				{
					this.botRender.gameObject.SetActive(false);
					Global.LevelFailed(0f, true);
					base.StartCoroutine(this.BottomFallingCoroutine());
				}
			}
		}
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x0007C693 File Offset: 0x0007AA93
	private void CheckInput()
	{
		this.CheckMouseInput();
		this.CheckButtonInput();
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x0007C6A4 File Offset: 0x0007AAA4
	private void CheckMouseInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.dragged = true;
			this.mouseClickOffset = Camera.main.GetMousePosition().x - base.transform.position.x;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.dragged = false;
			this.movingLeft = false;
			this.movingRight = false;
		}
		if (this.dragged)
		{
			float num = Camera.main.GetMousePosition().x - this.mouseClickOffset - base.transform.position.x;
			this.movingLeft = false;
			this.movingRight = false;
			if (Mathf.Abs(num) > 0.375f)
			{
				if (num < 0f)
				{
					this.movingLeft = true;
				}
				else
				{
					this.movingRight = true;
				}
			}
		}
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x0007C788 File Offset: 0x0007AB88
	private void CheckButtonInput()
	{
		if (this.dragged)
		{
			return;
		}
		KeyCode[] array = new KeyCode[]
		{
			KeyCode.LeftArrow,
			KeyCode.A
		};
		KeyCode[] array2 = new KeyCode[]
		{
			KeyCode.RightArrow,
			KeyCode.D
		};
		foreach (KeyCode key in array)
		{
			if (Input.GetKeyDown(key))
			{
				this.movingLeft = true;
				this.movingRight = false;
			}
			else if (Input.GetKeyUp(key))
			{
				this.movingLeft = false;
			}
		}
		foreach (KeyCode key2 in array2)
		{
			if (Input.GetKeyDown(key2))
			{
				this.movingLeft = false;
				this.movingRight = true;
			}
			else if (Input.GetKeyUp(key2))
			{
				this.movingRight = false;
			}
		}
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x0007C870 File Offset: 0x0007AC70
	private void Raycast()
	{
		List<Vector3> list = new List<Vector3>();
		Vector3 position = base.transform.position;
		list.Add(new Vector3(position.x, position.y - 0.184f));
		list.Add(new Vector3(position.x, position.y - 0.552f));
		list.Add(new Vector3(position.x, position.y + 0.184f));
		list.Add(new Vector3(position.x, position.y + 0.552f));
		LayerMask mask = 1 << LayerMask.NameToLayer("UI");
		bool flag = true;
		bool flag2 = true;
		foreach (Vector3 v in list)
		{
			flag &= (Physics2D.Raycast(v, new Vector3(0.375f, 0f), 0.375f, mask).collider == null);
			flag2 &= (Physics2D.Raycast(v, new Vector3(-0.375f, 0f), 0.375f, mask).collider == null);
		}
		this.canMoveRight = flag;
		this.canMoveLeft = flag2;
		this.canMoveDown = (Physics2D.Raycast(list[1], new Vector3(0f, -0.368f), 0.368f, mask).collider == null);
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x0007CA3C File Offset: 0x0007AE3C
	private IEnumerator BottomFallingCoroutine()
	{
		base.enabled = false;
		this.render.enabled = false;
		base.transform.GetChild(0).gameObject.SetActive(false);
		while (this.top.position.y > this.topEndPosition)
		{
			float y = Mathf.MoveTowards(this.top.position.y, this.topEndPosition, this.topFallingSpeed * Time.deltaTime);
			this.top.position = new Vector2(this.top.position.x, y);
			yield return null;
		}
		yield break;
	}

	// Token: 0x04001B47 RID: 6983
	public float period = 1f;

	// Token: 0x04001B48 RID: 6984
	public float periodOnSecondRun = 0.2f;

	// Token: 0x04001B49 RID: 6985
	public Transform bottom;

	// Token: 0x04001B4A RID: 6986
	public Transform top;

	// Token: 0x04001B4B RID: 6987
	public float topFallingSpeed;

	// Token: 0x04001B4C RID: 6988
	public float topEndPosition;

	// Token: 0x04001B4D RID: 6989
	public float waitBetweenMoves = 0.1f;

	// Token: 0x04001B4E RID: 6990
	private SpriteRenderer botRender;

	// Token: 0x04001B4F RID: 6991
	private SpriteRenderer render;

	// Token: 0x04001B50 RID: 6992
	private int ticks = 14;

	// Token: 0x04001B51 RID: 6993
	private float timer;

	// Token: 0x04001B52 RID: 6994
	private bool dragged;

	// Token: 0x04001B53 RID: 6995
	private bool moving = true;

	// Token: 0x04001B54 RID: 6996
	private bool movingLeft;

	// Token: 0x04001B55 RID: 6997
	private bool movingRight;

	// Token: 0x04001B56 RID: 6998
	private float mouseClickOffset;

	// Token: 0x04001B57 RID: 6999
	private float moveTimer;

	// Token: 0x04001B58 RID: 7000
	private bool canMoveDown = true;

	// Token: 0x04001B59 RID: 7001
	private bool canMoveRight = true;

	// Token: 0x04001B5A RID: 7002
	private bool canMoveLeft = true;

	// Token: 0x04001B5B RID: 7003
	public int flashTicks = 6;

	// Token: 0x04001B5C RID: 7004
	public float flashPeriod = 0.5f;

	// Token: 0x04001B5D RID: 7005
	private bool flashing;

	// Token: 0x04001B5E RID: 7006
	private int flashTicksLeft;

	// Token: 0x04001B5F RID: 7007
	private float flashTimer;

	// Token: 0x04001B60 RID: 7008
	private const float SQUARE_SIZE = 0.375f;
}
