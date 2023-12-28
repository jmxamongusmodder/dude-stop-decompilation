using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200043C RID: 1084
public class PuzzlePizza : MonoBehaviour
{
	// Token: 0x06001BA8 RID: 7080 RVA: 0x00072E6F File Offset: 0x0007126F
	private void OnDisable()
	{
		if (this.currentlyPlaying == PuzzlePizza.KnifeSounds.Movement)
		{
			Audio.self.stopLoopSound("b89acb3e-5ba7-453b-b404-3a1f1e45cdc2", true);
		}
		else if (this.currentlyPlaying == PuzzlePizza.KnifeSounds.Slicing)
		{
			Audio.self.stopLoopSound("b89acb3e-5ba7-453b-b404-3a1f1e45cdc2", true);
		}
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x00072EB0 File Offset: 0x000712B0
	private void Start()
	{
		this.currentLine = this.line;
		this.cutsSlices.Push(1);
		this.distanceFromPizza = Vector3.Distance(this.knife.position, base.transform.position);
		this.radius = base.GetComponent<CircleCollider2D>().radius;
		this.particles = this.knife.GetComponentInChildren<ParticleSystem>();
		this.particles.EnableEmmision(false);
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x00072F24 File Offset: 0x00071324
	private void Update()
	{
		if (this.cutsMade.Count == this.cuts && this.transition == PuzzlePizza.Transition.None)
		{
			if (this.allSmallSlices)
			{
				this.GetPuzzleStats().GetComponent<AudioVoice_Pizza>().playNoCrust();
				Global.self.GetCup(AwardName.PIZZA_PINEAPPLE);
				Global.self.GetCup(AwardName.PIZZA_FIRST);
			}
			else if (this.kolbasaUntouched)
			{
				this.GetPuzzleStats().GetComponent<AudioVoice_Pizza>().playNoSosage();
				Global.self.GetCup(AwardName.PIZZA_NECKLACE);
				Global.self.GetCup(AwardName.PIZZA_FIRST);
			}
			if (this.allThroughCenter)
			{
				Global.LevelFailed(0f, true);
			}
			else
			{
				int num = this.cuts * (this.cuts + 1) / 2 + 1;
				if (this.TotalSlices() == num)
				{
					this.GetPuzzleStats().GetComponent<AudioVoice_Pizza>().playMaxPiece();
					Global.self.GetCup(AwardName.PIZZA_CUTTER);
					Global.self.GetCup(AwardName.PIZZA_FIRST);
				}
				Global.LevelCompleted(0f, true);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.Click();
		}
		this.AnimateTransition();
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x00073049 File Offset: 0x00071449
	private void Click()
	{
		if (!this.showLine || this.transition != PuzzlePizza.Transition.None)
		{
			return;
		}
		if (!this.snapped)
		{
			this.allThroughCenter = false;
		}
		this.CutSlice();
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x0007307C File Offset: 0x0007147C
	private void AnimateTransition()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vector.z = 0f;
		PuzzlePizza.Transition transition = this.transition;
		if (transition != PuzzlePizza.Transition.None)
		{
			if (transition != PuzzlePizza.Transition.Forward)
			{
				if (transition == PuzzlePizza.Transition.Rotate)
				{
					float num = Mathf.Atan2(this.knife.position.x - vector.x, this.knife.position.y - vector.y) * 57.29578f;
					num += 90f;
					this.knifeRotationTimer = Mathf.MoveTowards(this.knifeRotationTimer, this.knifeRotation, Time.deltaTime);
					this.tableRotationTimer = Mathf.MoveTowards(this.tableRotationTimer, this.tableRotation, Time.deltaTime);
					float num2 = this.knifeRotationTimer / this.knifeRotation;
					num2 = Mathf.Sin(num2 * 3.1415927f * 0.5f);
					this.knife.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(this.knife.eulerAngles.z, -num, num2));
					num2 = this.tableRotationTimer / this.tableRotation;
					num2 = Mathf.Sin(num2 * 3.1415927f * 0.5f);
					num = (float)(180 / this.cuts * this.cutsMade.Count);
					if (num - this.rotationCircle.rotation.eulerAngles.z > 1f)
					{
						this.rotationCircle.rotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, this.startingTableAngle), Quaternion.Euler(0f, 0f, num), num2);
					}
					else
					{
						this.transition = PuzzlePizza.Transition.None;
						this.SwitchSound(PuzzlePizza.KnifeSounds.None);
					}
				}
			}
			else
			{
				this.knife.position = Vector3.Lerp(this.knife.position, this.knife.position + this.knife.right, Time.deltaTime * this.knifeThroughPizzaMovement);
				Material material = this.cutsMade.Peek().GetComponent<Renderer>().material;
				material.SetFloat("_Left", Camera.main.WorldToViewportPoint(this.knife.position).x);
				material.SetFloat("_Top", Camera.main.WorldToViewportPoint(this.knife.position).y);
				this.crossed |= (Vector3.Distance(this.knife.position, base.transform.position) < this.distanceFromPizza);
				if (Vector3.Distance(this.knife.position, base.transform.position) > this.distanceFromPizza && this.crossed)
				{
					this.transition = PuzzlePizza.Transition.Rotate;
					this.knifeRotationTimer = 0f;
					this.startingTableAngle = this.rotationCircle.eulerAngles.z;
					this.tableRotationTimer = 0f;
					this.crossed = false;
				}
				if (Vector3.Distance(this.knife.position, base.transform.position) < this.radius)
				{
					this.SwitchSound(PuzzlePizza.KnifeSounds.Slicing);
					this.particles.EnableEmmision(true);
				}
				else
				{
					this.SwitchSound(PuzzlePizza.KnifeSounds.Movement);
					this.particles.EnableEmmision(false);
				}
			}
		}
		else
		{
			float num = Mathf.Atan2(this.knife.position.x - vector.x, this.knife.position.y - vector.y) * 57.29578f;
			num += 90f;
			this.knife.rotation = Quaternion.Euler(0f, 0f, -num);
			this.RaycastLine();
			if (this.snapped)
			{
				vector = base.transform.position;
				num = Mathf.Atan2(this.knife.position.x - vector.x, this.knife.position.y - vector.y) * 57.29578f;
				num += 90f;
				this.knife.rotation = Quaternion.Euler(0f, 0f, -num);
			}
			if (this.showLine != this.currentLine.gameObject.activeSelf && this.cutsMade.Count != this.cuts)
			{
				this.currentLine.gameObject.SetActive(this.showLine);
			}
		}
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x00073534 File Offset: 0x00071934
	private void CutSlice()
	{
		if (Vector3.Distance(this.currentLine.position, base.transform.position) < this.distanceForSmallSlices)
		{
			this.allSmallSlices = false;
		}
		this.cutsMade.Push(this.currentLine);
		this.cutsSlices.Push(this.CountSlices());
		Material material = this.currentLine.GetComponent<Renderer>().material;
		material.SetFloat("_Angle", 0.017453292f * (this.knife.rotation.eulerAngles.z + 90f));
		material.SetFloat("_Left", Camera.main.WorldToViewportPoint(this.knife.position).x);
		material.SetFloat("_Top", Camera.main.WorldToViewportPoint(this.knife.position).y);
		this.transition = PuzzlePizza.Transition.Forward;
		this.DetachLine();
		if (this.cutsMade.Count < this.cuts)
		{
			this.CreateLine();
			this.RaycastKolbasa();
		}
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x00073654 File Offset: 0x00071A54
	private void DetachLine()
	{
		this.currentLine.gameObject.layer = LayerMask.NameToLayer("Front");
		this.currentLine.GetComponent<EdgeCollider2D>().enabled = true;
		this.currentLine.SetParent(base.transform.parent);
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x000736A2 File Offset: 0x00071AA2
	private void CreateLine()
	{
		this.currentLine = UnityEngine.Object.Instantiate<Transform>(this.currentLine);
		this.currentLine.SetParent(this.knife);
		this.currentLine.gameObject.SetActive(false);
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x000736D8 File Offset: 0x00071AD8
	private void SwitchSound(PuzzlePizza.KnifeSounds newSound)
	{
		if (newSound == this.currentlyPlaying)
		{
			return;
		}
		if (this.currentlyPlaying == PuzzlePizza.KnifeSounds.Movement)
		{
			Audio.self.stopLoopSound("b89acb3e-5ba7-453b-b404-3a1f1e45cdc2", false);
		}
		else if (this.currentlyPlaying == PuzzlePizza.KnifeSounds.Slicing)
		{
			Audio.self.stopLoopSound("b89acb3e-5ba7-453b-b404-3a1f1e45cdc2", false);
		}
		if (newSound == PuzzlePizza.KnifeSounds.None)
		{
			return;
		}
		if (newSound == PuzzlePizza.KnifeSounds.Movement)
		{
			Audio.self.playLoopSound("b89acb3e-5ba7-453b-b404-3a1f1e45cdc2", this.knife);
		}
		else if (newSound == PuzzlePizza.KnifeSounds.Slicing)
		{
			Audio.self.playLoopSound("b89acb3e-5ba7-453b-b404-3a1f1e45cdc2", this.knife);
		}
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00073774 File Offset: 0x00071B74
	private int TotalSlices()
	{
		int num = 0;
		foreach (int num2 in this.cutsSlices)
		{
			num += num2;
		}
		return num;
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000737D0 File Offset: 0x00071BD0
	private void RaycastKolbasa()
	{
		if (!this.kolbasaUntouched)
		{
			return;
		}
		LayerMask mask = 1 << LayerMask.NameToLayer("Back");
		Debug.DrawRay(this.knife.position, this.knife.right * 16f, Color.red, 1f);
		if (Physics2D.Raycast(this.knife.position, this.knife.right, 16f, mask).collider != null)
		{
			this.kolbasaUntouched = false;
		}
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x00073878 File Offset: 0x00071C78
	private void RaycastLine()
	{
		this.snapped = false;
		this.showLine = false;
		LayerMask mask = 1 << LayerMask.NameToLayer("Individual");
		RaycastHit2D[] array = Physics2D.RaycastAll(this.knife.position, this.knife.right, 16f, mask);
		foreach (RaycastHit2D raycastHit2D in array)
		{
			if (!raycastHit2D.collider.isTrigger)
			{
				this.snapped = true;
			}
			else
			{
				this.showLine = true;
			}
		}
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x00073928 File Offset: 0x00071D28
	private int CountSlices()
	{
		LayerMask mask = 1 << LayerMask.NameToLayer("Front");
		RaycastHit2D[] array = Physics2D.RaycastAll(this.knife.position, this.knife.right, 6f, mask);
		float num = base.GetComponent<CircleCollider2D>().radius;
		int num2 = 1;
		foreach (RaycastHit2D raycastHit2D in array)
		{
			if (Vector3.Distance(base.transform.position, raycastHit2D.point) < num)
			{
				num2++;
			}
		}
		return num2;
	}

	// Token: 0x040019F3 RID: 6643
	public Transform knife;

	// Token: 0x040019F4 RID: 6644
	public Transform rotationCircle;

	// Token: 0x040019F5 RID: 6645
	public Transform line;

	// Token: 0x040019F6 RID: 6646
	public float tableRotation = 2f;

	// Token: 0x040019F7 RID: 6647
	public float knifeRotation = 2f;

	// Token: 0x040019F8 RID: 6648
	public float knifeThroughPizzaMovement = 2f;

	// Token: 0x040019F9 RID: 6649
	public float snapDistance = 0.3f;

	// Token: 0x040019FA RID: 6650
	public int cuts = 4;

	// Token: 0x040019FB RID: 6651
	public float distanceForSmallSlices = 1.35f;

	// Token: 0x040019FC RID: 6652
	private PuzzlePizza.Transition transition = PuzzlePizza.Transition.None;

	// Token: 0x040019FD RID: 6653
	private Transform currentLine;

	// Token: 0x040019FE RID: 6654
	private Stack<Transform> cutsMade = new Stack<Transform>();

	// Token: 0x040019FF RID: 6655
	private Stack<int> cutsSlices = new Stack<int>();

	// Token: 0x04001A00 RID: 6656
	private bool snapped;

	// Token: 0x04001A01 RID: 6657
	private bool showLine;

	// Token: 0x04001A02 RID: 6658
	private bool allThroughCenter = true;

	// Token: 0x04001A03 RID: 6659
	private bool allSmallSlices = true;

	// Token: 0x04001A04 RID: 6660
	private bool kolbasaUntouched = true;

	// Token: 0x04001A05 RID: 6661
	private ParticleSystem particles;

	// Token: 0x04001A06 RID: 6662
	private float radius;

	// Token: 0x04001A07 RID: 6663
	private float distanceFromPizza;

	// Token: 0x04001A08 RID: 6664
	private bool crossed;

	// Token: 0x04001A09 RID: 6665
	private float knifeRotationTimer;

	// Token: 0x04001A0A RID: 6666
	private float startingTableAngle;

	// Token: 0x04001A0B RID: 6667
	private float tableRotationTimer;

	// Token: 0x04001A0C RID: 6668
	private PuzzlePizza.KnifeSounds currentlyPlaying;

	// Token: 0x0200043D RID: 1085
	private enum Transition
	{
		// Token: 0x04001A0E RID: 6670
		Forward,
		// Token: 0x04001A0F RID: 6671
		Rotate,
		// Token: 0x04001A10 RID: 6672
		None
	}

	// Token: 0x0200043E RID: 1086
	private enum KnifeSounds
	{
		// Token: 0x04001A12 RID: 6674
		None,
		// Token: 0x04001A13 RID: 6675
		Movement,
		// Token: 0x04001A14 RID: 6676
		Slicing
	}
}
