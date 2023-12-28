using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DudeStop.Analytics
{
	// Token: 0x02000284 RID: 644
	public class Analytics
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x0001278C File Offset: 0x00010B8C
		public Analytics(IContainer container)
		{
			this.container = container;
			this.sha = new SHA1Managed();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000127FB File Offset: 0x00010BFB
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00012803 File Offset: 0x00010C03
		public string address { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0001280C File Offset: 0x00010C0C
		public bool Initialized
		{
			get
			{
				return this.analyticsId != 0U;
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0001281A File Offset: 0x00010C1A
		public void SetAddress(string address)
		{
			this.address = address;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00012823 File Offset: 0x00010C23
		public void SetSaveID(string saveId)
		{
			if (string.IsNullOrEmpty(saveId))
			{
				saveId = "(empty)";
			}
			this.saveId = saveId;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00012840 File Offset: 0x00010C40
		public void Init(string userId, string sessionId, string version)
		{
			if (this.analyticsId > 0U)
			{
				return;
			}
			this.userId = userId;
			this.sessionId = sessionId;
			this.version = version;
			this.hash = null;
			if ("...".Equals(userId) && "...".Equals(sessionId))
			{
				this.analyticsId = 1U;
				return;
			}
			this.SendInit();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000128A4 File Offset: 0x00010CA4
		public void Report(string level, string key, string value)
		{
			if (this.logMessages.Contains(key) && this.logMessageEntries.Contains(value.GetHashCode()))
			{
				return;
			}
			this.entries.Add(new Entry
			{
				id = this.lastEventId++,
				level = level,
				key = key,
				value = value,
				time = this.GetTimestamp()
			});
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0001292A File Offset: 0x00010D2A
		public bool Flush(int entries, bool immediate)
		{
			if (this.analyticsId == 0U || this.entries.Count == 0)
			{
				return false;
			}
			if (this.entries.Count < entries && !immediate)
			{
				return false;
			}
			this.SendReports(entries);
			return true;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0001296C File Offset: 0x00010D6C
		protected void SendReports(int entryCount)
		{
			IEnumerable<Entry> enumerable = (from x in this.entries
			orderby x.time
			select x).Take(entryCount);
			string[] value = (from x in enumerable
			select JsonUtility.ToJson(x)).ToArray<string>();
			string value2 = "[" + string.Join(",", value) + "]";
			if (string.IsNullOrEmpty(this.saveId))
			{
				this.saveId = "(empty)";
			}
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("version", this.version);
			wwwform.AddField("data", value2);
			WWW www = new WWW(this.address, wwwform);
			this.container.Coroutine(this.ProcessReport(www, enumerable));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00012A4C File Offset: 0x00010E4C
		protected string GetHash()
		{
			if (string.IsNullOrEmpty(this.hash))
			{
				string s = this.userId + this.sessionId;
				byte[] source = this.sha.ComputeHash(Encoding.UTF8.GetBytes(s));
				string text = string.Join(string.Empty, (from c in source
				select c.ToString("x2")).ToArray<string>());
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < 10; i++)
				{
					stringBuilder.Append(text[i * 4 + 1]);
				}
				this.hash = stringBuilder.ToString();
			}
			return this.hash;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00012B08 File Offset: 0x00010F08
		protected void SendInit()
		{
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("userId", this.userId);
			wwwform.AddField("sessionId", this.sessionId);
			WWW www = new WWW(this.address, wwwform);
			this.container.Coroutine(this.ProcessInit(www));
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00012B5C File Offset: 0x00010F5C
		private IEnumerator ProcessInit(WWW www)
		{
			yield return null;
			yield return www;
			if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
			{
				InitResponse initResponse = JsonUtility.FromJson<InitResponse>(www.text);
				if (initResponse.code == ResponseCode.OK)
				{
					this.analyticsId = initResponse.analyticsId;
				}
				else
				{
					this.ProcessCode(initResponse.code);
				}
			}
			else
			{
				this.container.Fail();
			}
			yield break;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00012B80 File Offset: 0x00010F80
		private IEnumerator ProcessReport(WWW www, IEnumerable<Entry> sentEntries)
		{
			yield return null;
			yield return www;
			if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
			{
				FlushResponse flushResponse = JsonUtility.FromJson<FlushResponse>(www.text);
				if (flushResponse.code == ResponseCode.OK)
				{
					this.entries.RemoveAll((Entry x) => sentEntries.Contains(x));
				}
				else
				{
					this.ProcessCode(flushResponse.code);
				}
			}
			else
			{
				this.container.Fail();
			}
			yield break;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00012BA9 File Offset: 0x00010FA9
		private void ProcessCode(ResponseCode code)
		{
			if (code != ResponseCode.Fail)
			{
				if (code == ResponseCode.AnalyticsDisabled)
				{
					this.container.DisableInternetAccess();
				}
			}
			else
			{
				this.container.Fail();
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00012BE0 File Offset: 0x00010FE0
		private int GetTimestamp()
		{
			return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x04000CD1 RID: 3281
		private List<Entry> entries = new List<Entry>();

		// Token: 0x04000CD2 RID: 3282
		private IContainer container;

		// Token: 0x04000CD3 RID: 3283
		private SHA1 sha;

		// Token: 0x04000CD4 RID: 3284
		private string hash;

		// Token: 0x04000CD5 RID: 3285
		private int lastEventId;

		// Token: 0x04000CD7 RID: 3287
		private string version;

		// Token: 0x04000CD8 RID: 3288
		private string userId;

		// Token: 0x04000CD9 RID: 3289
		private string sessionId;

		// Token: 0x04000CDA RID: 3290
		private string saveId;

		// Token: 0x04000CDB RID: 3291
		private uint analyticsId;

		// Token: 0x04000CDC RID: 3292
		private string[] logMessages = new string[]
		{
			"LOG_ASSERT",
			"LOG_ERROR",
			"LOG_EXCEPTION",
			"LOG_MESSAGE",
			"LOG_WARNING"
		};

		// Token: 0x04000CDD RID: 3293
		private List<int> logMessageEntries = new List<int>();
	}
}
