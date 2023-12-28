using System;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public class PuzzlePuddle_Car : MonoBehaviour
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00073A2D File Offset: 0x00071E2D
	private SpringJoint2D spring
	{
		get
		{
			if (this._spring == null)
			{
				this._spring = base.GetComponent<SpringJoint2D>();
			}
			return this._spring;
		}
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x00073A54 File Offset: 0x00071E54
	private void Awake()
	{
		foreach (Transform transform in this.goodFaces)
		{
			transform.gameObject.SetActive(true);
		}
		foreach (Transform transform2 in this.sadFaces)
		{
			transform2.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x00073ABF File Offset: 0x00071EBF
	private void OnDrawGizmos()
	{
		GizmosExtension.DrawVerticalLine(this.failX);
		GizmosExtension.DrawVerticalLine(this.turnOffX);
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00073AD8 File Offset: 0x00071ED8
	private void Update()
	{
		float paramValue = Extensions.Between(0f, this.thresholdSpeed, Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x), true);
		float paramValue2 = (float)((!this.inPuddle) ? 0 : 1);
		Audio.self.playLoopSound("590eddd8-348f-4471-b10b-bc3c65145a25", base.transform, "Velocity", paramValue);
		Audio.self.playLoopSound("590eddd8-348f-4471-b10b-bc3c65145a25", base.transform, "Puddle", paramValue2);
		this.UpdatePuddleParticles();
		if (this.dragged)
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			this.spring.connectedAnchor = new Vector2(vector.x - this.delta.x, base.transform.position.y);
			if (this.spring.connectedAnchor.x > this.turnOffX)
			{
				this.dragged = false;
				this.canBeDragged = false;
			}
		}
		else if (base.transform.position.x > this.failX)
		{
			this.levelEnded = true;
			Global.LevelFailed(0f, true);
		}
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x00073C14 File Offset: 0x00072014
	private void OnEnable()
	{
		this.spring.enabled = true;
		if (Audio.self != null)
		{
			Audio.self.playLoopSound("590eddd8-348f-4471-b10b-bc3c65145a25", base.transform);
		}
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00073C48 File Offset: 0x00072048
	private void OnDisable()
	{
		this.spring.enabled = false;
		if (Audio.self != null)
		{
			if (!this.levelEnded)
			{
				Audio.self.stopLoopSound("590eddd8-348f-4471-b10b-bc3c65145a25", base.transform, true);
			}
			else
			{
				Audio.self.playLoopSound("590eddd8-348f-4471-b10b-bc3c65145a25", base.transform, "Velocity", 0f);
				Audio.self.playLoopSound("590eddd8-348f-4471-b10b-bc3c65145a25", base.transform, "Puddle", 0f);
			}
		}
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x00073CD5 File Offset: 0x000720D5
	private void OnTriggerEnter2D(Collider2D other)
	{
		this.inPuddle = true;
		this.particles.EnableEmmision(true);
		this.particles.SetEmissionRate(0f);
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x00073CFC File Offset: 0x000720FC
	private void UpdatePuddleParticles()
	{
		if (!this.inPuddle)
		{
			return;
		}
		float num = Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x);
		this.particles.SetEmissionRate((float)((int)num) * this.particleEmission);
		ParticleSystem.MainModule main = this.particles.main;
		main.startSpeedMultiplier = Mathf.Clamp(num * this.particleSpeed, this.particleSpeedMin, this.particleSpeedMax);
		main.startSizeMultiplier = Mathf.Clamp(num * this.particleSize, this.particleSizeMin, this.particleSizeMax);
		if (base.enabled && Mathf.Abs(base.GetComponent<Rigidbody2D>().velocity.x) > this.thresholdSpeed)
		{
			if (base.GetComponent<Rigidbody2D>().velocity.x < 0f)
			{
				this.GetPuzzleStats().GetComponent<AudioVoiceEndAchievement>().getTrophy();
			}
			this.levelEnded = true;
			Global.LevelCompleted(1f, true);
			this.particles.main.loop = false;
			foreach (Transform transform in this.goodFaces)
			{
				transform.gameObject.SetActive(false);
			}
			foreach (Transform transform2 in this.sadFaces)
			{
				transform2.gameObject.SetActive(true);
			}
			Audio.self.playOneShot("80d21363-087a-4e16-b2f1-16875f81add4", 1f);
		}
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x00073E91 File Offset: 0x00072291
	private void OnTriggerExit2D()
	{
		this.particles.EnableEmmision(false);
		this.inPuddle = false;
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x00073EA8 File Offset: 0x000722A8
	private void OnMouseDown()
	{
		if (!this.canBeDragged)
		{
			return;
		}
		this.dragged = true;
		Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.delta = a - base.transform.position;
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x00073EEF File Offset: 0x000722EF
	private void OnMouseUp()
	{
		this.dragged = false;
	}

	// Token: 0x04001A15 RID: 6677
	public float thresholdSpeed;

	// Token: 0x04001A16 RID: 6678
	public float failX;

	// Token: 0x04001A17 RID: 6679
	public float turnOffX;

	// Token: 0x04001A18 RID: 6680
	private Vector3 delta;

	// Token: 0x04001A19 RID: 6681
	private bool dragged;

	// Token: 0x04001A1A RID: 6682
	private bool canBeDragged = true;

	// Token: 0x04001A1B RID: 6683
	private SpringJoint2D _spring;

	// Token: 0x04001A1C RID: 6684
	private bool inPuddle;

	// Token: 0x04001A1D RID: 6685
	public ParticleSystem particles;

	// Token: 0x04001A1E RID: 6686
	[Space(20f)]
	public Transform[] goodFaces;

	// Token: 0x04001A1F RID: 6687
	public Transform[] sadFaces;

	// Token: 0x04001A20 RID: 6688
	[Header("Particles")]
	public float particleEmission = 50f;

	// Token: 0x04001A21 RID: 6689
	public float particleSpeed = 3f;

	// Token: 0x04001A22 RID: 6690
	public float particleSpeedMin;

	// Token: 0x04001A23 RID: 6691
	public float particleSpeedMax = 75f;

	// Token: 0x04001A24 RID: 6692
	public float particleSize = 0.02f;

	// Token: 0x04001A25 RID: 6693
	public float particleSizeMin;

	// Token: 0x04001A26 RID: 6694
	public float particleSizeMax = 0.3f;

	// Token: 0x04001A27 RID: 6695
	public bool levelEnded;
}
