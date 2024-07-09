using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;

namespace PeNet.Structures
{
	// Token: 0x02000B98 RID: 2968
	[ComVisible(true)]
	public abstract class AbstractStructure
	{
		// Token: 0x0600779B RID: 30619 RVA: 0x0023AF64 File Offset: 0x0023AF64
		protected AbstractStructure(byte[] buff, uint offset)
		{
			this.Buff = buff;
			this.Offset = offset;
		}

		// Token: 0x0600779C RID: 30620 RVA: 0x0023AF7C File Offset: 0x0023AF7C
		public override string ToString()
		{
			PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GetType().Name + "\n");
			PropertyInfo[] array = properties;
			int i = 0;
			while (i < array.Length)
			{
				PropertyInfo propertyInfo = array[i];
				if (!propertyInfo.PropertyType.IsArray)
				{
					goto IL_D3;
				}
				if (propertyInfo.GetValue(this, null) != null)
				{
					using (IEnumerator enumerator = ((IEnumerable)propertyInfo.GetValue(this, null)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							if (obj.GetType().IsSubclassOf(typeof(AbstractStructure)))
							{
								stringBuilder.Append(obj.ToString());
							}
						}
						goto IL_EF;
					}
					goto IL_D3;
				}
				IL_EF:
				i++;
				continue;
				IL_D3:
				stringBuilder.AppendFormat("{0}: {1}\n", propertyInfo.Name, propertyInfo.GetValue(this, null));
				goto IL_EF;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600779D RID: 30621 RVA: 0x0023B09C File Offset: 0x0023B09C
		public string ToJson(bool formatted = false)
		{
			return JsonConvert.SerializeObject(this, formatted ? Formatting.Indented : Formatting.None);
		}

		// Token: 0x040039F3 RID: 14835
		internal readonly byte[] Buff;

		// Token: 0x040039F4 RID: 14836
		internal readonly uint Offset;
	}
}
