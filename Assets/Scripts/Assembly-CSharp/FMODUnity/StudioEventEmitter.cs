using System;
using System.Threading;
using FMOD.Studio;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x0200001F RID: 31
	[AddComponentMenu("FMOD Studio/FMOD Studio Event Emitter")]
	public class StudioEventEmitter : MonoBehaviour
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000403A File Offset: 0x0000243A
		public EventDescription EventDescription
		{
			get
			{
				return this.eventDescription;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004042 File Offset: 0x00002442
		public EventInstance EventInstance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000404C File Offset: 0x0000244C
		private void Start()
		{
			RuntimeUtils.EnforceLibraryOrder();
			if (this.Preload)
			{
				this.Lookup();
				this.eventDescription.loadSampleData();
				RuntimeManager.StudioSystem.update();
				LOADING_STATE loading_STATE;
				this.eventDescription.getSampleLoadingState(out loading_STATE);
				while (loading_STATE == LOADING_STATE.LOADING)
				{
					Thread.Sleep(1);
					this.eventDescription.getSampleLoadingState(out loading_STATE);
				}
			}
			this.HandleGameEvent(EmitterGameEvent.ObjectStart);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000040BE File Offset: 0x000024BE
		private void OnApplicationQuit()
		{
			this.isQuitting = true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000040C8 File Offset: 0x000024C8
		private void OnDestroy()
		{
			if (!this.isQuitting)
			{
				this.HandleGameEvent(EmitterGameEvent.ObjectDestroy);
				if (this.instance.isValid())
				{
					RuntimeManager.DetachInstanceFromGameObject(this.instance);
				}
				if (this.Preload)
				{
					this.eventDescription.unloadSampleData();
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004119 File Offset: 0x00002519
		private void OnEnable()
		{
			this.HandleGameEvent(EmitterGameEvent.ObjectEnable);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004123 File Offset: 0x00002523
		private void OnDisable()
		{
			this.HandleGameEvent(EmitterGameEvent.ObjectDisable);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000412D File Offset: 0x0000252D
		private void OnTriggerEnter(Collider other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerEnter);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004157 File Offset: 0x00002557
		private void OnTriggerExit(Collider other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerExit);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004181 File Offset: 0x00002581
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerEnter2D);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000041AB File Offset: 0x000025AB
		private void OnTriggerExit2D(Collider2D other)
		{
			if (string.IsNullOrEmpty(this.CollisionTag) || other.CompareTag(this.CollisionTag))
			{
				this.HandleGameEvent(EmitterGameEvent.TriggerExit2D);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000041D5 File Offset: 0x000025D5
		private void OnCollisionEnter()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionEnter);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000041DE File Offset: 0x000025DE
		private void OnCollisionExit()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionExit);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000041E7 File Offset: 0x000025E7
		private void OnCollisionEnter2D()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionEnter2D);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000041F1 File Offset: 0x000025F1
		private void OnCollisionExit2D()
		{
			this.HandleGameEvent(EmitterGameEvent.CollisionExit2D);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000041FB File Offset: 0x000025FB
		private void HandleGameEvent(EmitterGameEvent gameEvent)
		{
			if (this.PlayEvent == gameEvent)
			{
				this.Play();
			}
			if (this.StopEvent == gameEvent)
			{
				this.Stop();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004221 File Offset: 0x00002621
		private void Lookup()
		{
			this.eventDescription = RuntimeManager.GetEventDescription(this.Event);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004234 File Offset: 0x00002634
		public void Play()
		{
			if (this.TriggerOnce && this.hasTriggered)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.Event))
			{
				return;
			}
			if (!this.eventDescription.isValid())
			{
				this.Lookup();
			}
			bool flag = false;
			if (!this.Event.StartsWith("snapshot", StringComparison.CurrentCultureIgnoreCase))
			{
				this.eventDescription.isOneshot(out flag);
			}
			bool flag2;
			this.eventDescription.is3D(out flag2);
			if (!this.instance.isValid())
			{
				this.instance.clearHandle();
			}
			if (flag && this.instance.isValid())
			{
				this.instance.release();
				this.instance.clearHandle();
			}
			if (!this.instance.isValid())
			{
				this.eventDescription.createInstance(out this.instance);
				if (flag2)
				{
					Rigidbody component = base.GetComponent<Rigidbody>();
					Rigidbody2D component2 = base.GetComponent<Rigidbody2D>();
					Transform component3 = base.GetComponent<Transform>();
					if (component)
					{
						this.instance.set3DAttributes(RuntimeUtils.To3DAttributes(base.gameObject, component));
						RuntimeManager.AttachInstanceToGameObject(this.instance, component3, component);
					}
					else
					{
						this.instance.set3DAttributes(RuntimeUtils.To3DAttributes(base.gameObject, component2));
						RuntimeManager.AttachInstanceToGameObject(this.instance, component3, component2);
					}
				}
			}
			foreach (ParamRef paramRef in this.Params)
			{
				this.instance.setParameterValue(paramRef.Name, paramRef.Value);
			}
			if (flag2 && this.OverrideAttenuation)
			{
				this.instance.setProperty(EVENT_PROPERTY.MINIMUM_DISTANCE, this.OverrideMinDistance);
				this.instance.setProperty(EVENT_PROPERTY.MAXIMUM_DISTANCE, this.OverrideMaxDistance);
			}
			this.instance.start();
			this.hasTriggered = true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004424 File Offset: 0x00002824
		public void Stop()
		{
			if (this.instance.isValid())
			{
				this.instance.stop((!this.AllowFadeout) ? STOP_MODE.IMMEDIATE : STOP_MODE.ALLOWFADEOUT);
				this.instance.release();
				this.instance.clearHandle();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004476 File Offset: 0x00002876
		public void SetParameter(string name, float value)
		{
			if (this.instance.isValid())
			{
				this.instance.setParameterValue(name, value);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004498 File Offset: 0x00002898
		public bool IsPlaying()
		{
			if (this.instance.isValid() && this.instance.isValid())
			{
				PLAYBACK_STATE playback_STATE;
				this.instance.getPlaybackState(out playback_STATE);
				return playback_STATE != PLAYBACK_STATE.STOPPED;
			}
			return false;
		}

		// Token: 0x0400007F RID: 127
		[EventRef]
		public string Event = string.Empty;

		// Token: 0x04000080 RID: 128
		public EmitterGameEvent PlayEvent;

		// Token: 0x04000081 RID: 129
		public EmitterGameEvent StopEvent;

		// Token: 0x04000082 RID: 130
		public string CollisionTag = string.Empty;

		// Token: 0x04000083 RID: 131
		public bool AllowFadeout = true;

		// Token: 0x04000084 RID: 132
		public bool TriggerOnce;

		// Token: 0x04000085 RID: 133
		public bool Preload;

		// Token: 0x04000086 RID: 134
		public ParamRef[] Params = new ParamRef[0];

		// Token: 0x04000087 RID: 135
		public bool OverrideAttenuation;

		// Token: 0x04000088 RID: 136
		public float OverrideMinDistance = -1f;

		// Token: 0x04000089 RID: 137
		public float OverrideMaxDistance = -1f;

		// Token: 0x0400008A RID: 138
		private EventDescription eventDescription;

		// Token: 0x0400008B RID: 139
		private EventInstance instance;

		// Token: 0x0400008C RID: 140
		private bool hasTriggered;

		// Token: 0x0400008D RID: 141
		private bool isQuitting;
	}
}
