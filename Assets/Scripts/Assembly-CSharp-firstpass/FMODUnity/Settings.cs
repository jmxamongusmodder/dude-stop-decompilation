using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FMODUnity
{
	// Token: 0x0200001D RID: 29
	public class Settings : ScriptableObject
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000038EC File Offset: 0x00001CEC
		private Settings()
		{
			this.Banks = new List<string>();
			this.RealChannelSettings = new List<PlatformIntSetting>();
			this.VirtualChannelSettings = new List<PlatformIntSetting>();
			this.LoggingSettings = new List<PlatformBoolSetting>();
			this.LiveUpdateSettings = new List<PlatformBoolSetting>();
			this.OverlaySettings = new List<PlatformBoolSetting>();
			this.SampleRateSettings = new List<PlatformIntSetting>();
			this.SpeakerModeSettings = new List<PlatformIntSetting>();
			this.BankDirectorySettings = new List<PlatformStringSetting>();
			Settings.SetSetting<PlatformBoolSetting, TriStateBool>(this.LoggingSettings, FMODPlatform.PlayInEditor, TriStateBool.Enabled);
			Settings.SetSetting<PlatformBoolSetting, TriStateBool>(this.LiveUpdateSettings, FMODPlatform.PlayInEditor, TriStateBool.Enabled);
			Settings.SetSetting<PlatformBoolSetting, TriStateBool>(this.OverlaySettings, FMODPlatform.PlayInEditor, TriStateBool.Enabled);
			Settings.SetSetting<PlatformIntSetting, int>(this.RealChannelSettings, FMODPlatform.PlayInEditor, 256);
			Settings.SetSetting<PlatformIntSetting, int>(this.VirtualChannelSettings, FMODPlatform.PlayInEditor, 1024);
			Settings.SetSetting<PlatformBoolSetting, TriStateBool>(this.LoggingSettings, FMODPlatform.Default, TriStateBool.Disabled);
			Settings.SetSetting<PlatformBoolSetting, TriStateBool>(this.LiveUpdateSettings, FMODPlatform.Default, TriStateBool.Disabled);
			Settings.SetSetting<PlatformBoolSetting, TriStateBool>(this.OverlaySettings, FMODPlatform.Default, TriStateBool.Disabled);
			Settings.SetSetting<PlatformIntSetting, int>(this.RealChannelSettings, FMODPlatform.Default, 32);
			Settings.SetSetting<PlatformIntSetting, int>(this.VirtualChannelSettings, FMODPlatform.Default, 128);
			Settings.SetSetting<PlatformIntSetting, int>(this.SampleRateSettings, FMODPlatform.Default, 0);
			Settings.SetSetting<PlatformIntSetting, int>(this.SpeakerModeSettings, FMODPlatform.Default, 3);
			this.ImportType = ImportType.StreamingAssets;
			this.AutomaticEventLoading = true;
			this.AutomaticSampleLoading = false;
			this.TargetAssetPath = string.Empty;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003A44 File Offset: 0x00001E44
		public static Settings Instance
		{
			get
			{
				if (Settings.instance == null)
				{
					Settings.instance = (Resources.Load("FMODStudioSettings") as Settings);
					if (Settings.instance == null)
					{
						Debug.Log("FMOD Studio: cannot find integration settings, creating default settings");
						Settings.instance = ScriptableObject.CreateInstance<Settings>();
						Settings.instance.name = "FMOD Studio Integration Settings";
					}
				}
				return Settings.instance;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003AAD File Offset: 0x00001EAD
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003AE7 File Offset: 0x00001EE7
		public string SourceProjectPath
		{
			get
			{
				if (string.IsNullOrEmpty(this.sourceProjectPath) && !string.IsNullOrEmpty(this.SourceProjectPathUnformatted))
				{
					this.sourceProjectPath = this.GetPlatformSpecificPath(this.SourceProjectPathUnformatted);
				}
				return this.sourceProjectPath;
			}
			set
			{
				this.sourceProjectPath = this.GetPlatformSpecificPath(value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003AF6 File Offset: 0x00001EF6
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003B30 File Offset: 0x00001F30
		public string SourceBankPath
		{
			get
			{
				if (string.IsNullOrEmpty(this.sourceBankPath) && !string.IsNullOrEmpty(this.SourceBankPathUnformatted))
				{
					this.sourceBankPath = this.GetPlatformSpecificPath(this.SourceBankPathUnformatted);
				}
				return this.sourceBankPath;
			}
			set
			{
				this.sourceBankPath = this.GetPlatformSpecificPath(value);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003B40 File Offset: 0x00001F40
		public static FMODPlatform GetParent(FMODPlatform platform)
		{
			switch (platform)
			{
			case FMODPlatform.Desktop:
			case FMODPlatform.Mobile:
			case FMODPlatform.Console:
				return FMODPlatform.Default;
			case FMODPlatform.MobileHigh:
			case FMODPlatform.MobileLow:
			case FMODPlatform.iOS:
			case FMODPlatform.Android:
			case FMODPlatform.WindowsPhone:
			case FMODPlatform.PSVita:
			case FMODPlatform.AppleTV:
			case FMODPlatform.Switch:
				return FMODPlatform.Mobile;
			case FMODPlatform.Windows:
			case FMODPlatform.Mac:
			case FMODPlatform.Linux:
			case FMODPlatform.UWP:
				return FMODPlatform.Desktop;
			case FMODPlatform.XboxOne:
			case FMODPlatform.PS4:
			case FMODPlatform.WiiU:
				return FMODPlatform.Console;
			}
			return FMODPlatform.None;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003BB4 File Offset: 0x00001FB4
		public static bool HasSetting<T>(List<T> list, FMODPlatform platform) where T : PlatformSettingBase
		{
			return list.Exists((T x) => x.Platform == platform);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003BE0 File Offset: 0x00001FE0
		public static U GetSetting<T, U>(List<T> list, FMODPlatform platform, U def) where T : PlatformSetting<U>
		{
			T t = list.Find((T x) => x.Platform == platform);
			if (t != null)
			{
				return t.Value;
			}
			FMODPlatform parent = Settings.GetParent(platform);
			if (parent != FMODPlatform.None)
			{
				return Settings.GetSetting<T, U>(list, parent, def);
			}
			return def;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003C40 File Offset: 0x00002040
		public static void SetSetting<T, U>(List<T> list, FMODPlatform platform, U value) where T : PlatformSetting<U>, new()
		{
			T t = list.Find((T x) => x.Platform == platform);
			if (t == null)
			{
				t = Activator.CreateInstance<T>();
				t.Platform = platform;
				list.Add(t);
			}
			t.Value = value;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003CA4 File Offset: 0x000020A4
		public static void RemoveSetting<T>(List<T> list, FMODPlatform platform) where T : PlatformSettingBase
		{
			list.RemoveAll((T x) => x.Platform == platform);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003CD1 File Offset: 0x000020D1
		public bool IsLiveUpdateEnabled(FMODPlatform platform)
		{
			return Settings.GetSetting<PlatformBoolSetting, TriStateBool>(this.LiveUpdateSettings, platform, TriStateBool.Disabled) == TriStateBool.Enabled;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003CE3 File Offset: 0x000020E3
		public bool IsOverlayEnabled(FMODPlatform platform)
		{
			return Settings.GetSetting<PlatformBoolSetting, TriStateBool>(this.OverlaySettings, platform, TriStateBool.Disabled) == TriStateBool.Enabled;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003CF5 File Offset: 0x000020F5
		public int GetRealChannels(FMODPlatform platform)
		{
			return Settings.GetSetting<PlatformIntSetting, int>(this.RealChannelSettings, platform, 64);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003D05 File Offset: 0x00002105
		public int GetVirtualChannels(FMODPlatform platform)
		{
			return Settings.GetSetting<PlatformIntSetting, int>(this.VirtualChannelSettings, platform, 128);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003D18 File Offset: 0x00002118
		public int GetSpeakerMode(FMODPlatform platform)
		{
			return Settings.GetSetting<PlatformIntSetting, int>(this.SpeakerModeSettings, platform, 3);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D27 File Offset: 0x00002127
		public int GetSampleRate(FMODPlatform platform)
		{
			return Settings.GetSetting<PlatformIntSetting, int>(this.SampleRateSettings, platform, 48000);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D3A File Offset: 0x0000213A
		public string GetBankPlatform(FMODPlatform platform)
		{
			if (!this.HasPlatforms)
			{
				return string.Empty;
			}
			return Settings.GetSetting<PlatformStringSetting, string>(this.BankDirectorySettings, platform, "Desktop");
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003D5E File Offset: 0x0000215E
		private string GetPlatformSpecificPath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			if (Path.DirectorySeparatorChar == '/')
			{
				return path.Replace('\\', '/');
			}
			return path.Replace('/', '\\');
		}

		// Token: 0x04000062 RID: 98
		private const string SettingsAssetName = "FMODStudioSettings";

		// Token: 0x04000063 RID: 99
		private static Settings instance;

		// Token: 0x04000064 RID: 100
		[SerializeField]
		public bool HasSourceProject = true;

		// Token: 0x04000065 RID: 101
		[SerializeField]
		public bool HasPlatforms = true;

		// Token: 0x04000066 RID: 102
		[SerializeField]
		private string sourceProjectPath;

		// Token: 0x04000067 RID: 103
		[SerializeField]
		public string SourceProjectPathUnformatted;

		// Token: 0x04000068 RID: 104
		private string sourceBankPath;

		// Token: 0x04000069 RID: 105
		[SerializeField]
		public string SourceBankPathUnformatted;

		// Token: 0x0400006A RID: 106
		[SerializeField]
		public bool AutomaticEventLoading;

		// Token: 0x0400006B RID: 107
		[SerializeField]
		public bool AutomaticSampleLoading;

		// Token: 0x0400006C RID: 108
		[SerializeField]
		public ImportType ImportType;

		// Token: 0x0400006D RID: 109
		[SerializeField]
		public string TargetAssetPath;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		public List<PlatformIntSetting> SpeakerModeSettings;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		public List<PlatformIntSetting> SampleRateSettings;

		// Token: 0x04000070 RID: 112
		[SerializeField]
		public List<PlatformBoolSetting> LiveUpdateSettings;

		// Token: 0x04000071 RID: 113
		[SerializeField]
		public List<PlatformBoolSetting> OverlaySettings;

		// Token: 0x04000072 RID: 114
		[SerializeField]
		public List<PlatformBoolSetting> LoggingSettings;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		public List<PlatformStringSetting> BankDirectorySettings;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		public List<PlatformIntSetting> VirtualChannelSettings;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		public List<PlatformIntSetting> RealChannelSettings;

		// Token: 0x04000076 RID: 118
		[SerializeField]
		public List<string> Plugins = new List<string>();

		// Token: 0x04000077 RID: 119
		[SerializeField]
		public string MasterBank;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		public List<string> Banks;
	}
}
