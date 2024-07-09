using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x02000700 RID: 1792
	internal abstract class Platform
	{
		// Token: 0x06003EB9 RID: 16057 RVA: 0x00159B0C File Offset: 0x00159B0C
		private static string GetNewLine()
		{
			return Environment.NewLine;
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x00159B14 File Offset: 0x00159B14
		internal static bool EqualsIgnoreCase(string a, string b)
		{
			return Platform.ToUpperInvariant(a) == Platform.ToUpperInvariant(b);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x00159B28 File Offset: 0x00159B28
		internal static string GetEnvironmentVariable(string variable)
		{
			string result;
			try
			{
				result = Environment.GetEnvironmentVariable(variable);
			}
			catch (SecurityException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x00159B5C File Offset: 0x00159B5C
		internal static Exception CreateNotImplementedException(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x00159B64 File Offset: 0x00159B64
		internal static IList CreateArrayList()
		{
			return new ArrayList();
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x00159B6C File Offset: 0x00159B6C
		internal static IList CreateArrayList(int capacity)
		{
			return new ArrayList(capacity);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x00159B74 File Offset: 0x00159B74
		internal static IList CreateArrayList(ICollection collection)
		{
			return new ArrayList(collection);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x00159B7C File Offset: 0x00159B7C
		internal static IList CreateArrayList(IEnumerable collection)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object value in collection)
			{
				arrayList.Add(value);
			}
			return arrayList;
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x00159BDC File Offset: 0x00159BDC
		internal static IDictionary CreateHashtable()
		{
			return new Hashtable();
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x00159BE4 File Offset: 0x00159BE4
		internal static IDictionary CreateHashtable(int capacity)
		{
			return new Hashtable(capacity);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00159BEC File Offset: 0x00159BEC
		internal static IDictionary CreateHashtable(IDictionary dictionary)
		{
			return new Hashtable(dictionary);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x00159BF4 File Offset: 0x00159BF4
		internal static string ToLowerInvariant(string s)
		{
			return s.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x00159C04 File Offset: 0x00159C04
		internal static string ToUpperInvariant(string s)
		{
			return s.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x00159C14 File Offset: 0x00159C14
		internal static void Dispose(Stream s)
		{
			s.Close();
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00159C1C File Offset: 0x00159C1C
		internal static void Dispose(TextWriter t)
		{
			t.Close();
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x00159C24 File Offset: 0x00159C24
		internal static int IndexOf(string source, string value)
		{
			return Platform.InvariantCompareInfo.IndexOf(source, value, CompareOptions.Ordinal);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00159C38 File Offset: 0x00159C38
		internal static int LastIndexOf(string source, string value)
		{
			return Platform.InvariantCompareInfo.LastIndexOf(source, value, CompareOptions.Ordinal);
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x00159C4C File Offset: 0x00159C4C
		internal static bool StartsWith(string source, string prefix)
		{
			return Platform.InvariantCompareInfo.IsPrefix(source, prefix, CompareOptions.Ordinal);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x00159C60 File Offset: 0x00159C60
		internal static bool EndsWith(string source, string suffix)
		{
			return Platform.InvariantCompareInfo.IsSuffix(source, suffix, CompareOptions.Ordinal);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x00159C74 File Offset: 0x00159C74
		internal static string GetTypeName(object obj)
		{
			return obj.GetType().FullName;
		}

		// Token: 0x0400207B RID: 8315
		private static readonly CompareInfo InvariantCompareInfo = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x0400207C RID: 8316
		internal static readonly string NewLine = Platform.GetNewLine();
	}
}
