using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;

namespace FMODUnity
{
	// Token: 0x02000007 RID: 7
	public class OneshotList
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020A9 File Offset: 0x000004A9
		public void Add(EventInstance instance)
		{
			this.instances.Add(instance);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B8 File Offset: 0x000004B8
		public void Update(ATTRIBUTES_3D attributes)
		{
			PLAYBACK_STATE state;
			List<EventInstance> list = this.instances.FindAll(delegate(EventInstance x)
			{
				x.getPlaybackState(out state);
				return state == PLAYBACK_STATE.STOPPED;
			});
			foreach (EventInstance eventInstance in list)
			{
				eventInstance.release();
			}
			this.instances.RemoveAll((EventInstance x) => !x.isValid());
			foreach (EventInstance eventInstance2 in this.instances)
			{
				eventInstance2.set3DAttributes(attributes);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A8 File Offset: 0x000005A8
		public void SetParameterValue(string name, float value)
		{
			foreach (EventInstance eventInstance in this.instances)
			{
				eventInstance.setParameterValue(name, value);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002208 File Offset: 0x00000608
		public void StopAll(STOP_MODE stopMode)
		{
			foreach (EventInstance eventInstance in this.instances)
			{
				eventInstance.stop(stopMode);
				eventInstance.release();
			}
			this.instances.Clear();
		}

		// Token: 0x04000007 RID: 7
		private List<EventInstance> instances = new List<EventInstance>();
	}
}
