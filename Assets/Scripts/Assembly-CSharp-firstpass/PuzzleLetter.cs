using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class PuzzleLetter : EnhancedDraggable
{
	// Token: 0x06001B29 RID: 6953 RVA: 0x0006EB8A File Offset: 0x0006CF8A
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		this.MoveToStartingPosition();
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x0006EB98 File Offset: 0x0006CF98
	protected override void MouseUpped()
	{
		base.body.velocity = Vector2.zero;
		this.CheckVictoryConditions();
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x0006EBB0 File Offset: 0x0006CFB0
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!this.dragged)
		{
			return;
		}
		if (other.tag == "SuccessCollider")
		{
			this.success = true;
		}
		else if (other.tag == "FailCollider")
		{
			this.fail = true;
			this.colliderPos = other.gameObject.transform.position;
		}
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x0006EC1C File Offset: 0x0006D01C
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!this.dragged)
		{
			return;
		}
		if (other.tag == "SuccessCollider")
		{
			this.success = false;
		}
		else if (other.tag == "FailCollider")
		{
			this.fail = false;
		}
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x0006EC74 File Offset: 0x0006D074
	private void MoveToStartingPosition()
	{
		if (this.dragged || !base.WasMoved())
		{
			return;
		}
		base.transform.position = Vector2.Lerp(base.transform.position, this.startingPosition, Time.fixedDeltaTime * this.returnSpeed);
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x0006ECD4 File Offset: 0x0006D0D4
	private void CheckVictoryConditions()
	{
		if (this.fail)
		{
			float percentage = Mathf.Clamp(Vector2.Distance(base.transform.position, this.colliderPos), 0f, 1f);
			this.GetPuzzleStats().GetComponent<AudioVoice_Letter>().setPercentage(percentage);
			Global.LevelFailed(0f, true);
			this.shadow.SetActive(false);
			Audio.self.playOneShot("13b4f248-644a-4a95-b158-e13cd9a262e4", 1f);
		}
		else if (this.success)
		{
			Global.LevelCompleted(0f, true);
			this.shadow.SetActive(false);
			Audio.self.playOneShot("13b4f248-644a-4a95-b158-e13cd9a262e4", 1f);
		}
	}

	// Token: 0x04001965 RID: 6501
	public float returnSpeed = 2f;

	// Token: 0x04001966 RID: 6502
	private bool fail;

	// Token: 0x04001967 RID: 6503
	private bool success;

	// Token: 0x04001968 RID: 6504
	public GameObject shadow;

	// Token: 0x04001969 RID: 6505
	public Vector3 colliderPos;
}
