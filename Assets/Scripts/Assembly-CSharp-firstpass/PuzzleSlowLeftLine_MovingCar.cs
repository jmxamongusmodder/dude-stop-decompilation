using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200044F RID: 1103
public class PuzzleSlowLeftLine_MovingCar : MonoBehaviour
{
	// Token: 0x06001C43 RID: 7235 RVA: 0x00077028 File Offset: 0x00075428
	private void Start()
	{
		this.planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		this.screen = Camera.main.ViewportToWorldPoint(Vector3.zero);
		this.rend = base.GetComponent<Renderer>();
		this.secondCarRend = this.secondCar.GetComponent<Renderer>();
		base.StartCoroutine(this.MovingCoroutine());
	}

	// Token: 0x06001C44 RID: 7236 RVA: 0x0007708C File Offset: 0x0007548C
	private IEnumerator MovingCoroutine()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.waitTime);
			yield return base.StartCoroutine(this.DrivingCoroutine());
		}
		yield break;
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x000770A8 File Offset: 0x000754A8
	private IEnumerator DrivingCoroutine()
	{
		float secondCarQuot = 0.5f;
		float quot = 1f;
		float secondCarTimer = 0f;
		float victoryTimer = 0f;
		base.transform.GetChild(0).GetComponent<Renderer>().material.color = Extensions.Color.GetRandom();
		Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", base.transform);
		yield return new WaitForSeconds(0.1f);
		bool seenOnLane = true;
		bool secondCarOnLane = false;
		while (seenOnLane || secondCarOnLane)
		{
			if (this.player.onLeftLane && this.player.transform.position.y > base.transform.position.y)
			{
				float num = Mathf.Abs(base.transform.position.y - this.player.transform.position.y);
				float target = 1f;
				if (num < this.deccelerationDistance && num > this.minDistanceToPlayer)
				{
					target = (num - this.minDistanceToPlayer) / (this.deccelerationDistance - this.minDistanceToPlayer);
				}
				else if (num < this.minDistanceToPlayer)
				{
					target = 0f;
					Audio.self.playOneShot("7755e823-058f-498e-9e99-a4f90a37e22f", 1f);
				}
				quot = Mathf.MoveTowards(quot, target, this.deccelerationRate * Time.deltaTime);
			}
			else
			{
				quot = Mathf.MoveTowards(quot, 1f, this.deccelerationRate * Time.deltaTime);
			}
			if (quot < secondCarQuot)
			{
				secondCarTimer = Mathf.MoveTowards(secondCarTimer, this.waitBeforeSecondCar, Time.deltaTime);
			}
			else
			{
				secondCarTimer = Mathf.MoveTowards(secondCarTimer, 0f, Time.deltaTime);
			}
			if (!this.secondCar.gameObject.activeSelf && secondCarTimer >= this.waitBeforeSecondCar)
			{
				this.secondCar.gameObject.SetActive(true);
				this.secondCar.GetChild(0).GetComponent<Renderer>().material.color = Extensions.Color.GetRandom();
				victoryTimer = 0f;
			}
			base.transform.position += Vector3.up * this.movementSpeed * quot * Time.deltaTime;
			float vert = Mathf.Clamp((base.transform.position.y - this.screen.y) / (this.screen.y * -2f), 0f, 1f);
			Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", base.transform, "Index", 0f);
			Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", base.transform, "Velocity", quot);
			Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", base.transform, "Position", vert);
			if (this.secondCar.gameObject.activeSelf)
			{
				float value = 0f;
				if (Mathf.Abs(this.secondCar.position.y - base.transform.position.y) > this.distanceToSecondCar + 0.01f)
				{
					Vector2 target2 = base.transform.position - this.distanceToSecondCar * Vector2.up;
					float y = this.secondCar.position.y;
					this.secondCar.position = Vector2.MoveTowards(this.secondCar.position, target2, this.movementSpeed / 2f * Time.deltaTime);
					value = Mathf.Abs(this.secondCar.position.y - y);
					victoryTimer = 0f;
				}
				else
				{
					victoryTimer = Mathf.MoveTowards(victoryTimer, this.waitBeforeEnding, Time.deltaTime);
				}
				float paramValue = Mathf.Clamp((this.secondCar.position.y - this.screen.y) / (this.screen.y * -2f), 0f, 1f);
				float paramValue2 = Extensions.Between(0f, this.movementSpeed / 2f * Time.deltaTime, value, true);
				Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", this.secondCar, "Index", 1f);
				Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", this.secondCar, "Velocity", paramValue2);
				Audio.self.playLoopSound("dfd8a2c2-fc1d-4de9-b2a7-ec62ce808d5a", this.secondCar, "Position", paramValue);
			}
			if (this.player != null)
			{
				if (!this.player.onLeftLane && secondCarOnLane && this.player.transform.position.y > this.secondCar.position.y && this.player.transform.position.y < base.transform.position.y)
				{
					this.player.limitLeft = true;
				}
				else
				{
					this.player.limitLeft = false;
				}
			}
			if (victoryTimer == this.waitBeforeEnding && base.enabled)
			{
				Global.LevelCompleted(0f, true);
				UnityEngine.Object.Destroy(this);
			}
			if (victoryTimer > this.waitBeforeEnding * 0.5f && base.enabled)
			{
				Global.self.currPuzzle.GetComponent<AudioVoice_SlowLeftLane>().carBehind();
			}
			seenOnLane = (GeometryUtility.TestPlanesAABB(this.planes, this.rend.bounds) || base.transform.position.y < 0f);
			secondCarOnLane = (this.secondCar.gameObject.activeSelf && (GeometryUtility.TestPlanesAABB(this.planes, this.secondCarRend.bounds) || base.transform.position.y < 0f));
			yield return null;
		}
		base.transform.position = new Vector3(base.transform.position.x, this.screen.y - this.rend.bounds.size.y, 0f);
		this.secondCar.position = new Vector3(this.secondCar.position.x, this.screen.y - this.secondCarRend.bounds.size.y, 0f);
		this.secondCar.gameObject.SetActive(false);
		Audio.self.resetLoopSounds();
		yield break;
	}

	// Token: 0x04001A96 RID: 6806
	public Transform secondCar;

	// Token: 0x04001A97 RID: 6807
	public PuzzleSlowLeftLine_Player player;

	// Token: 0x04001A98 RID: 6808
	public float movementSpeed;

	// Token: 0x04001A99 RID: 6809
	public float minDistanceToPlayer;

	// Token: 0x04001A9A RID: 6810
	public float distanceToSecondCar;

	// Token: 0x04001A9B RID: 6811
	public float deccelerationDistance;

	// Token: 0x04001A9C RID: 6812
	public float deccelerationRate;

	// Token: 0x04001A9D RID: 6813
	public float waitBeforeSecondCar;

	// Token: 0x04001A9E RID: 6814
	public float waitTime;

	// Token: 0x04001A9F RID: 6815
	public float waitBeforeEnding;

	// Token: 0x04001AA0 RID: 6816
	private Plane[] planes;

	// Token: 0x04001AA1 RID: 6817
	private Vector2 screen;

	// Token: 0x04001AA2 RID: 6818
	private Renderer rend;

	// Token: 0x04001AA3 RID: 6819
	private Renderer secondCarRend;
}
