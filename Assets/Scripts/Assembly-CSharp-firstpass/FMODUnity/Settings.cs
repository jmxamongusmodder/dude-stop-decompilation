using UnityEngine;
using System.Collections.Generic;

namespace FMODUnity
{
	public class Settings : ScriptableObject
	{
		private Settings()
		{
		}

		[SerializeField]
		public bool HasSourceProject;
		[SerializeField]
		public bool HasPlatforms;
		[SerializeField]
		private string sourceProjectPath;
		[SerializeField]
		public string SourceProjectPathUnformatted;
		[SerializeField]
		public string SourceBankPathUnformatted;
		[SerializeField]
		public bool AutomaticEventLoading;
		[SerializeField]
		public bool AutomaticSampleLoading;
		[SerializeField]
		public ImportType ImportType;
		[SerializeField]
		public string TargetAssetPath;
		[SerializeField]
		public List<PlatformIntSetting> SpeakerModeSettings;
		[SerializeField]
		public List<PlatformIntSetting> SampleRateSettings;
		[SerializeField]
		public List<PlatformBoolSetting> LiveUpdateSettings;
		[SerializeField]
		public List<PlatformBoolSetting> OverlaySettings;
		[SerializeField]
		public List<PlatformBoolSetting> LoggingSettings;
		[SerializeField]
		public List<PlatformStringSetting> BankDirectorySettings;
		[SerializeField]
		public List<PlatformIntSetting> VirtualChannelSettings;
		[SerializeField]
		public List<PlatformIntSetting> RealChannelSettings;
		[SerializeField]
		public List<string> Plugins;
		[SerializeField]
		public string MasterBank;
		[SerializeField]
		public List<string> Banks;
	}
}
