using UnityEngine;
using System;
using System.Collections.Generic;

public class ShaderSettings : MonoBehaviour
{
	[Serializable]
	public class Setting
	{
		public Setting(Shader s, int i)
		{
		}

		public string name;
		public string description;
		public float value;
		public int order;
		public float min;
		public float max;
		public float def;
		public Color color;
	}

	public List<ShaderSettings.Setting> settings;
}
