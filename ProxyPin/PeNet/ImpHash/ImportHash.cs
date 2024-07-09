using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace PeNet.ImpHash
{
	// Token: 0x02000C08 RID: 3080
	[ComVisible(true)]
	public class ImportHash
	{
		// Token: 0x06007ACE RID: 31438 RVA: 0x00242004 File Offset: 0x00242004
		public ImportHash(ICollection<ImportFunction> importedFunctions)
		{
			this.ImpHash = this.ComputeImpHash(importedFunctions);
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06007ACF RID: 31439 RVA: 0x0024201C File Offset: 0x0024201C
		// (set) Token: 0x06007AD0 RID: 31440 RVA: 0x00242024 File Offset: 0x00242024
		public string ImpHash { get; private set; }

		// Token: 0x06007AD1 RID: 31441 RVA: 0x00242030 File Offset: 0x00242030
		private string ComputeImpHash(ICollection<ImportFunction> importedFunctions)
		{
			if (importedFunctions == null || importedFunctions.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (ImportFunction importFunction in importedFunctions)
			{
				string text = this.FormatLibraryName(importFunction.DLL);
				text += this.FormatFunctionName(importFunction);
				list.Add(text);
			}
			string s = string.Join(",", list);
			HashAlgorithm hashAlgorithm = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			byte[] array = hashAlgorithm.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x00242120 File Offset: 0x00242120
		private string FormatLibraryName(string libraryName)
		{
			List<string> list = new List<string>
			{
				"ocx",
				"sys",
				"dll"
			};
			string[] array = libraryName.ToLower().Split(new char[]
			{
				'.'
			});
			string text = "";
			if (array.Length > 1 && list.Contains(array[array.Length - 1]))
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					text += array[i];
					text += ".";
				}
			}
			else
			{
				foreach (string str in array)
				{
					text += str;
					text += ".";
				}
			}
			return text;
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x00242200 File Offset: 0x00242200
		private string FormatFunctionName(ImportFunction impFunc)
		{
			string text = "";
			if (impFunc.Name == null)
			{
				if (impFunc.DLL.ToLower() == "oleaut32.dll")
				{
					text += OrdinalSymbolMapping.Lookup(OrdinalSymbolMapping.Modul.oleaut32, (uint)impFunc.Hint);
				}
				else if (impFunc.DLL.ToLower() == "ws2_32.dll")
				{
					text += OrdinalSymbolMapping.Lookup(OrdinalSymbolMapping.Modul.ws2_32, (uint)impFunc.Hint);
				}
				else if (impFunc.DLL.ToLower() == "wsock32.dll")
				{
					text += OrdinalSymbolMapping.Lookup(OrdinalSymbolMapping.Modul.wsock32, (uint)impFunc.Hint);
				}
				else
				{
					text += "ord";
					text += impFunc.Hint.ToString();
				}
			}
			else
			{
				text += impFunc.Name;
			}
			return text.ToLower();
		}
	}
}
