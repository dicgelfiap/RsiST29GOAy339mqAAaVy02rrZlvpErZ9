using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200077C RID: 1916
	[ComVisible(true)]
	public readonly struct AssemblyNameComparer : IEqualityComparer<IAssembly>
	{
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x00169BA8 File Offset: 0x00169BA8
		public bool CompareName
		{
			get
			{
				return (this.flags & AssemblyNameComparerFlags.Name) > (AssemblyNameComparerFlags)0;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x00169BB8 File Offset: 0x00169BB8
		public bool CompareVersion
		{
			get
			{
				return (this.flags & AssemblyNameComparerFlags.Version) > (AssemblyNameComparerFlags)0;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x00169BC8 File Offset: 0x00169BC8
		public bool ComparePublicKeyToken
		{
			get
			{
				return (this.flags & AssemblyNameComparerFlags.PublicKeyToken) > (AssemblyNameComparerFlags)0;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x00169BD8 File Offset: 0x00169BD8
		public bool CompareCulture
		{
			get
			{
				return (this.flags & AssemblyNameComparerFlags.Culture) > (AssemblyNameComparerFlags)0;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x00169BE8 File Offset: 0x00169BE8
		public bool CompareContentType
		{
			get
			{
				return (this.flags & AssemblyNameComparerFlags.ContentType) > (AssemblyNameComparerFlags)0;
			}
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x00169BF8 File Offset: 0x00169BF8
		public AssemblyNameComparer(AssemblyNameComparerFlags flags)
		{
			this.flags = flags;
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x00169C04 File Offset: 0x00169C04
		public int CompareTo(IAssembly a, IAssembly b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			int result;
			if (this.CompareName && (result = UTF8String.CaseInsensitiveCompareTo(a.Name, b.Name)) != 0)
			{
				return result;
			}
			if (this.CompareVersion && (result = Utils.CompareTo(a.Version, b.Version)) != 0)
			{
				return result;
			}
			if (this.ComparePublicKeyToken && (result = PublicKeyBase.TokenCompareTo(a.PublicKeyOrToken, b.PublicKeyOrToken)) != 0)
			{
				return result;
			}
			if (this.CompareCulture && (result = Utils.LocaleCompareTo(a.Culture, b.Culture)) != 0)
			{
				return result;
			}
			if (this.CompareContentType && (result = a.ContentType.CompareTo(b.ContentType)) != 0)
			{
				return result;
			}
			return 0;
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x00169CF8 File Offset: 0x00169CF8
		public bool Equals(IAssembly a, IAssembly b)
		{
			return this.CompareTo(a, b) == 0;
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x00169D08 File Offset: 0x00169D08
		public int CompareClosest(IAssembly requested, IAssembly a, IAssembly b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				if (!this.CompareName)
				{
					return 1;
				}
				if (!UTF8String.CaseInsensitiveEquals(requested.Name, b.Name))
				{
					return 0;
				}
				return 1;
			}
			else if (b == null)
			{
				if (!this.CompareName)
				{
					return 0;
				}
				if (!UTF8String.CaseInsensitiveEquals(requested.Name, a.Name))
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (this.CompareName)
				{
					bool flag = UTF8String.CaseInsensitiveEquals(requested.Name, a.Name);
					bool flag2 = UTF8String.CaseInsensitiveEquals(requested.Name, b.Name);
					if (flag && !flag2)
					{
						return 0;
					}
					if (!flag && flag2)
					{
						return 1;
					}
					if (!flag && !flag2)
					{
						return -1;
					}
				}
				if (this.ComparePublicKeyToken)
				{
					bool flag3;
					bool flag4;
					if (PublicKeyBase.IsNullOrEmpty2(requested.PublicKeyOrToken))
					{
						flag3 = PublicKeyBase.IsNullOrEmpty2(a.PublicKeyOrToken);
						flag4 = PublicKeyBase.IsNullOrEmpty2(b.PublicKeyOrToken);
					}
					else
					{
						flag3 = PublicKeyBase.TokenEquals(requested.PublicKeyOrToken, a.PublicKeyOrToken);
						flag4 = PublicKeyBase.TokenEquals(requested.PublicKeyOrToken, b.PublicKeyOrToken);
					}
					if (flag3 && !flag4)
					{
						return 0;
					}
					if (!flag3 && flag4)
					{
						return 1;
					}
				}
				if (!this.CompareVersion || Utils.Equals(a.Version, b.Version))
				{
					if (this.CompareCulture)
					{
						bool flag5 = Utils.LocaleEquals(requested.Culture, a.Culture);
						bool flag6 = Utils.LocaleEquals(requested.Culture, b.Culture);
						if (flag5 && !flag6)
						{
							return 0;
						}
						if (!flag5 && flag6)
						{
							return 1;
						}
					}
					if (this.CompareContentType)
					{
						bool flag7 = requested.ContentType == a.ContentType;
						bool flag8 = requested.ContentType == b.ContentType;
						if (flag7 && !flag8)
						{
							return 0;
						}
						if (!flag7 && flag8)
						{
							return 1;
						}
					}
					return -1;
				}
				Version version = Utils.CreateVersionWithNoUndefinedValues(requested.Version);
				if (version == new Version(0, 0, 0, 0))
				{
					version = new Version(65535, 65535, 65535, 65535);
				}
				int num = Utils.CompareTo(a.Version, version);
				int num2 = Utils.CompareTo(b.Version, version);
				if (num == 0)
				{
					return 0;
				}
				if (num2 == 0)
				{
					return 1;
				}
				if (num > 0 && num2 < 0)
				{
					return 0;
				}
				if (num < 0 && num2 > 0)
				{
					return 1;
				}
				if (num > 0)
				{
					if (Utils.CompareTo(a.Version, b.Version) >= 0)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (Utils.CompareTo(a.Version, b.Version) <= 0)
					{
						return 1;
					}
					return 0;
				}
			}
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x00169FD8 File Offset: 0x00169FD8
		public int GetHashCode(IAssembly a)
		{
			if (a == null)
			{
				return 0;
			}
			int num = 0;
			if (this.CompareName)
			{
				num += UTF8String.GetHashCode(a.Name);
			}
			if (this.CompareVersion)
			{
				num += Utils.CreateVersionWithNoUndefinedValues(a.Version).GetHashCode();
			}
			if (this.ComparePublicKeyToken)
			{
				num += PublicKeyBase.GetHashCodeToken(a.PublicKeyOrToken);
			}
			if (this.CompareCulture)
			{
				num += Utils.GetHashCodeLocale(a.Culture);
			}
			if (this.CompareContentType)
			{
				num = (int)((uint)num + a.ContentType);
			}
			return num;
		}

		// Token: 0x040023DE RID: 9182
		public static readonly AssemblyNameComparer CompareAll = new AssemblyNameComparer(AssemblyNameComparerFlags.All);

		// Token: 0x040023DF RID: 9183
		public static readonly AssemblyNameComparer NameAndPublicKeyTokenOnly = new AssemblyNameComparer(AssemblyNameComparerFlags.Name | AssemblyNameComparerFlags.PublicKeyToken);

		// Token: 0x040023E0 RID: 9184
		public static readonly AssemblyNameComparer NameOnly = new AssemblyNameComparer(AssemblyNameComparerFlags.Name);

		// Token: 0x040023E1 RID: 9185
		private readonly AssemblyNameComparerFlags flags;
	}
}
