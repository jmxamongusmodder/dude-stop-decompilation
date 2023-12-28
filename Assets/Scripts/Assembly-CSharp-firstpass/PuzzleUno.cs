using System;
using UnityEngine;

// Token: 0x02000460 RID: 1120
public class PuzzleUno : Draggable
{
	// Token: 0x06001CCE RID: 7374 RVA: 0x0007CBD8 File Offset: 0x0007AFD8
	private void Start()
	{
		this.startingAngle = base.transform.rotation.eulerAngles.z;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x0007CC08 File Offset: 0x0007B008
	private void Update()
	{
		if (this.onTable && base.transform.rotation.eulerAngles.z != 0f)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(base.transform.rotation.eulerAngles.z, 0f, Time.deltaTime * this.rotationSpeed));
		}
		else if (!this.onTable && base.transform.rotation.eulerAngles.z != this.startingAngle)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(base.transform.rotation.eulerAngles.z, this.startingAngle, Time.deltaTime * this.rotationSpeed));
		}
		if (!this.dragged)
		{
			if (this.onTable)
			{
				this.DisableOtherCards();
				if (Vector2.Distance(base.transform.position, this.target.position) > 0.1f)
				{
					base.transform.position = Vector3.Lerp(base.transform.position, this.target.position, Time.deltaTime * this.returnSpeed);
				}
				else if (this.fail)
				{
					Global.LevelFailed(0f, true);
				}
				else
				{
					Global.LevelCompleted(0f, true);
				}
			}
			else if (base.WasMoved())
			{
				base.transform.position = Vector3.Lerp(base.transform.position, this.startingPosition, Time.deltaTime * this.returnSpeed);
			}
		}
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x0007CDFB File Offset: 0x0007B1FB
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onTable = true;
		}
	}

	// Token: 0x06001CD1 RID: 7377 RVA: 0x0007CE19 File Offset: 0x0007B219
	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "SuccessCollider")
		{
			this.onTable = false;
		}
	}

	// Token: 0x06001CD2 RID: 7378 RVA: 0x0007CE38 File Offset: 0x0007B238
	private void DisableOtherCards()
	{
		foreach (PuzzleUno puzzleUno in base.transform.parent.GetComponentsInChildren<PuzzleUno>())
		{
			if (puzzleUno != this)
			{
				puzzleUno.dragEnabled = false;
			}
		}
	}

	// Token: 0x04001B61 RID: 7009
	public Transform target;

	// Token: 0x04001B62 RID: 7010
	public float returnSpeed = 2f;

	// Token: 0x04001B63 RID: 7011
	public float rotationSpeed = 10f;

	// Token: 0x04001B64 RID: 7012
	public bool fail;

	// Token: 0x04001B65 RID: 7013
	private bool onTable;

	// Token: 0x04001B66 RID: 7014
	private float startingAngle;
}
