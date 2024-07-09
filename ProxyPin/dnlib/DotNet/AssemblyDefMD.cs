using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x02000777 RID: 1911
	internal sealed class AssemblyDefMD : AssemblyDef, IMDTokenProviderMD, IMDTokenProvider
	{
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x00167498 File Offset: 0x00167498
		public uint OrigRid
		{
			get
			{
				return this.origRid;
			}
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x001674A0 File Offset: 0x001674A0
		protected override void InitializeDeclSecurities()
		{
			RidList declSecurityRidList = this.readerModule.Metadata.GetDeclSecurityRidList(Table.Assembly, this.origRid);
			LazyList<DeclSecurity, RidList> value = new LazyList<DeclSecurity, RidList>(declSecurityRidList.Count, declSecurityRidList, (RidList list2, int index) => this.readerModule.ResolveDeclSecurity(list2[index]));
			Interlocked.CompareExchange<IList<DeclSecurity>>(ref this.declSecurities, value, null);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x001674F4 File Offset: 0x001674F4
		protected override void InitializeModules()
		{
			RidList moduleRidList = this.readerModule.GetModuleRidList();
			LazyList<ModuleDef, RidList> value = new LazyList<ModuleDef, RidList>(moduleRidList.Count + 1, this, moduleRidList, delegate(RidList list2, int index)
			{
				ModuleDef moduleDef;
				if (index == 0)
				{
					moduleDef = this.readerModule;
				}
				else
				{
					moduleDef = this.readerModule.ReadModule(list2[index - 1], this);
				}
				if (moduleDef == null)
				{
					moduleDef = new ModuleDefUser("INVALID", new Guid?(Guid.NewGuid()));
				}
				moduleDef.Assembly = this;
				return moduleDef;
			});
			Interlocked.CompareExchange<LazyList<ModuleDef>>(ref this.modules, value, null);
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x0016753C File Offset: 0x0016753C
		protected override void InitializeCustomAttributes()
		{
			RidList list = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Assembly, this.origRid);
			CustomAttributeCollection value = new CustomAttributeCollection(list.Count, list, (object list2, int index) => this.readerModule.ReadCustomAttribute(list[index]));
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, value, null);
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x001675B0 File Offset: 0x001675B0
		protected override void InitializeCustomDebugInfos()
		{
			List<PdbCustomDebugInfo> list = new List<PdbCustomDebugInfo>();
			this.readerModule.InitializeCustomDebugInfos(new MDToken(base.MDToken.Table, this.origRid), default(GenericParamContext), list);
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, list, null);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x00167604 File Offset: 0x00167604
		public override bool TryGetOriginalTargetFrameworkAttribute(out string framework, out Version version, out string profile)
		{
			if (!this.hasInitdTFA)
			{
				this.InitializeTargetFrameworkAttribute();
			}
			framework = this.tfaFramework;
			version = this.tfaVersion;
			profile = this.tfaProfile;
			return this.tfaReturnValue;
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x00167638 File Offset: 0x00167638
		private void InitializeTargetFrameworkAttribute()
		{
			if (this.hasInitdTFA)
			{
				return;
			}
			RidList customAttributeRidList = this.readerModule.Metadata.GetCustomAttributeRidList(Table.Assembly, this.origRid);
			GenericParamContext gpContext = default(GenericParamContext);
			for (int i = 0; i < customAttributeRidList.Count; i++)
			{
				uint rid = customAttributeRidList[i];
				RawCustomAttributeRow rawCustomAttributeRow;
				if (this.readerModule.TablesStream.TryReadCustomAttributeRow(rid, out rawCustomAttributeRow))
				{
					ICustomAttributeType customAttributeType = this.readerModule.ResolveCustomAttributeType(rawCustomAttributeRow.Type, gpContext);
					UTF8String left;
					UTF8String left2;
					if (AssemblyDefMD.TryGetName(customAttributeType, out left, out left2) && !(left != AssemblyDefMD.nameSystemRuntimeVersioning) && !(left2 != AssemblyDefMD.nameTargetFrameworkAttribute))
					{
						CustomAttribute customAttribute = CustomAttributeReader.Read(this.readerModule, customAttributeType, rawCustomAttributeRow.Value, gpContext);
						if (customAttribute != null && customAttribute.ConstructorArguments.Count == 1)
						{
							UTF8String utf8String = customAttribute.ConstructorArguments[0].Value as UTF8String;
							string text;
							Version version;
							string text2;
							if (utf8String != null && AssemblyDefMD.TryCreateTargetFrameworkInfo(utf8String, out text, out version, out text2))
							{
								this.tfaFramework = text;
								this.tfaVersion = version;
								this.tfaProfile = text2;
								this.tfaReturnValue = true;
								break;
							}
						}
					}
				}
			}
			this.hasInitdTFA = true;
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x00167790 File Offset: 0x00167790
		private static bool TryGetName(ICustomAttributeType caType, out UTF8String ns, out UTF8String name)
		{
			MemberRef memberRef = caType as MemberRef;
			ITypeDefOrRef typeDefOrRef;
			if (memberRef != null)
			{
				typeDefOrRef = memberRef.DeclaringType;
			}
			else
			{
				MethodDef methodDef = caType as MethodDef;
				typeDefOrRef = ((methodDef != null) ? methodDef.DeclaringType : null);
			}
			TypeRef typeRef = typeDefOrRef as TypeRef;
			if (typeRef != null)
			{
				ns = typeRef.Namespace;
				name = typeRef.Name;
				return true;
			}
			TypeDef typeDef = typeDefOrRef as TypeDef;
			if (typeDef != null)
			{
				ns = typeDef.Namespace;
				name = typeDef.Name;
				return true;
			}
			ns = null;
			name = null;
			return false;
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x00167818 File Offset: 0x00167818
		private static bool TryCreateTargetFrameworkInfo(string attrString, out string framework, out Version version, out string profile)
		{
			framework = null;
			version = null;
			profile = null;
			string[] array = attrString.Split(new char[]
			{
				','
			});
			if (array.Length < 2 || array.Length > 3)
			{
				return false;
			}
			string text = array[0].Trim();
			if (text.Length == 0)
			{
				return false;
			}
			Version version2 = null;
			string text2 = null;
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length != 2)
				{
					return false;
				}
				string text3 = array2[0].Trim();
				string text4 = array2[1].Trim();
				if (text3.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					if (text4.StartsWith("v", StringComparison.OrdinalIgnoreCase))
					{
						text4 = text4.Substring(1);
					}
					if (!AssemblyDefMD.TryParse(text4, out version2))
					{
						return false;
					}
					version2 = new Version(version2.Major, version2.Minor, (version2.Build == -1) ? 0 : version2.Build, 0);
				}
				else if (text3.Equals("Profile", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(text4))
				{
					text2 = text4;
				}
			}
			if (version2 == null)
			{
				return false;
			}
			framework = text;
			version = version2;
			profile = text2;
			return true;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x00167974 File Offset: 0x00167974
		private static int ParseInt32(string s)
		{
			int result;
			if (!int.TryParse(s, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x00167998 File Offset: 0x00167998
		private static bool TryParse(string s, out Version version)
		{
			Match match = Regex.Match(s, "^(\\d+)\\.(\\d+)$");
			if (match.Groups.Count == 3)
			{
				version = new Version(AssemblyDefMD.ParseInt32(match.Groups[1].Value), AssemblyDefMD.ParseInt32(match.Groups[2].Value));
				return true;
			}
			match = Regex.Match(s, "^(\\d+)\\.(\\d+)\\.(\\d+)$");
			if (match.Groups.Count == 4)
			{
				version = new Version(AssemblyDefMD.ParseInt32(match.Groups[1].Value), AssemblyDefMD.ParseInt32(match.Groups[2].Value), AssemblyDefMD.ParseInt32(match.Groups[3].Value));
				return true;
			}
			match = Regex.Match(s, "^(\\d+)\\.(\\d+)\\.(\\d+)\\.(\\d+)$");
			if (match.Groups.Count == 5)
			{
				version = new Version(AssemblyDefMD.ParseInt32(match.Groups[1].Value), AssemblyDefMD.ParseInt32(match.Groups[2].Value), AssemblyDefMD.ParseInt32(match.Groups[3].Value), AssemblyDefMD.ParseInt32(match.Groups[4].Value));
				return true;
			}
			version = null;
			return false;
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x00167AE8 File Offset: 0x00167AE8
		public AssemblyDefMD(ModuleDefMD readerModule, uint rid)
		{
			this.origRid = rid;
			this.rid = rid;
			this.readerModule = readerModule;
			if (rid != 1U)
			{
				this.modules = new LazyList<ModuleDef>(this);
			}
			RawAssemblyRow rawAssemblyRow;
			readerModule.TablesStream.TryReadAssemblyRow(this.origRid, out rawAssemblyRow);
			this.hashAlgorithm = (AssemblyHashAlgorithm)rawAssemblyRow.HashAlgId;
			this.version = new Version((int)rawAssemblyRow.MajorVersion, (int)rawAssemblyRow.MinorVersion, (int)rawAssemblyRow.BuildNumber, (int)rawAssemblyRow.RevisionNumber);
			this.attributes = (int)rawAssemblyRow.Flags;
			this.name = readerModule.StringsStream.ReadNoNull(rawAssemblyRow.Name);
			this.culture = readerModule.StringsStream.ReadNoNull(rawAssemblyRow.Locale);
			this.publicKey = new PublicKey(readerModule.BlobStream.Read(rawAssemblyRow.PublicKey));
		}

		// Token: 0x040023BF RID: 9151
		private readonly ModuleDefMD readerModule;

		// Token: 0x040023C0 RID: 9152
		private readonly uint origRid;

		// Token: 0x040023C1 RID: 9153
		private volatile bool hasInitdTFA;

		// Token: 0x040023C2 RID: 9154
		private string tfaFramework;

		// Token: 0x040023C3 RID: 9155
		private Version tfaVersion;

		// Token: 0x040023C4 RID: 9156
		private string tfaProfile;

		// Token: 0x040023C5 RID: 9157
		private bool tfaReturnValue;

		// Token: 0x040023C6 RID: 9158
		private static readonly UTF8String nameSystemRuntimeVersioning = new UTF8String("System.Runtime.Versioning");

		// Token: 0x040023C7 RID: 9159
		private static readonly UTF8String nameTargetFrameworkAttribute = new UTF8String("TargetFrameworkAttribute");
	}
}
