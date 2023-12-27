using System;

namespace DudeStop.Analytics
{
	[Serializable]
	public struct Entry
	{
		public int id;
		public string level;
		public string key;
		public string value;
		public int time;
	}
}
