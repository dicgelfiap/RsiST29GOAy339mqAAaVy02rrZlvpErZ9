using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.DotNet.Writer;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x02000775 RID: 1909
	[ComVisible(true)]
	public abstract class AssemblyDef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasDeclSecurity, IFullName, IHasCustomDebugInformation, IAssembly, IListListener<ModuleDef>, ITypeDefFinder, IDnlibDef
	{
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x00166718 File Offset: 0x00166718
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Assembly, this.rid);
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x00166728 File Offset: 0x00166728
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x00166730 File Offset: 0x00166730
		public uint Rid
		{
			get
			{
				return this.rid;
			}
			set
			{
				this.rid = value;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x0016673C File Offset: 0x0016673C
		public int HasCustomAttributeTag
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x00166740 File Offset: 0x00166740
		public int HasDeclSecurityTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x00166744 File Offset: 0x00166744
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x0016674C File Offset: 0x0016674C
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
			set
			{
				this.hashAlgorithm = value;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x00166758 File Offset: 0x00166758
		// (set) Token: 0x060042EA RID: 17130 RVA: 0x00166760 File Offset: 0x00166760
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.version = value;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x0016677C File Offset: 0x0016677C
		// (set) Token: 0x060042EC RID: 17132 RVA: 0x00166784 File Offset: 0x00166784
		public AssemblyAttributes Attributes
		{
			get
			{
				return (AssemblyAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x00166790 File Offset: 0x00166790
		// (set) Token: 0x060042EE RID: 17134 RVA: 0x00166798 File Offset: 0x00166798
		public PublicKey PublicKey
		{
			get
			{
				return this.publicKey;
			}
			set
			{
				this.publicKey = (value ?? new PublicKey());
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x001667B0 File Offset: 0x001667B0
		public PublicKeyToken PublicKeyToken
		{
			get
			{
				return this.publicKey.Token;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x001667C0 File Offset: 0x001667C0
		// (set) Token: 0x060042F1 RID: 17137 RVA: 0x001667C8 File Offset: 0x001667C8
		public UTF8String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x001667D4 File Offset: 0x001667D4
		// (set) Token: 0x060042F3 RID: 17139 RVA: 0x001667DC File Offset: 0x001667DC
		public UTF8String Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x001667E8 File Offset: 0x001667E8
		public IList<DeclSecurity> DeclSecurities
		{
			get
			{
				if (this.declSecurities == null)
				{
					this.InitializeDeclSecurities();
				}
				return this.declSecurities;
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x00166804 File Offset: 0x00166804
		protected virtual void InitializeDeclSecurities()
		{
			Interlocked.CompareExchange<IList<DeclSecurity>>(ref this.declSecurities, new List<DeclSecurity>(), null);
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x00166818 File Offset: 0x00166818
		public PublicKeyBase PublicKeyOrToken
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x00166820 File Offset: 0x00166820
		public string FullName
		{
			get
			{
				return this.GetFullNameWithPublicKeyToken();
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060042F8 RID: 17144 RVA: 0x00166828 File Offset: 0x00166828
		public string FullNameToken
		{
			get
			{
				return this.GetFullNameWithPublicKeyToken();
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x00166830 File Offset: 0x00166830
		public IList<ModuleDef> Modules
		{
			get
			{
				if (this.modules == null)
				{
					this.InitializeModules();
				}
				return this.modules;
			}
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x0016684C File Offset: 0x0016684C
		protected virtual void InitializeModules()
		{
			Interlocked.CompareExchange<LazyList<ModuleDef>>(ref this.modules, new LazyList<ModuleDef>(this), null);
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x00166864 File Offset: 0x00166864
		public CustomAttributeCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.InitializeCustomAttributes();
				}
				return this.customAttributes;
			}
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x00166880 File Offset: 0x00166880
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x00166894 File Offset: 0x00166894
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060042FE RID: 17150 RVA: 0x001668A4 File Offset: 0x001668A4
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x001668A8 File Offset: 0x001668A8
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x001668B8 File Offset: 0x001668B8
		public IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				if (this.customDebugInfos == null)
				{
					this.InitializeCustomDebugInfos();
				}
				return this.customDebugInfos;
			}
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x001668D4 File Offset: 0x001668D4
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x001668E8 File Offset: 0x001668E8
		public bool HasDeclSecurities
		{
			get
			{
				return this.DeclSecurities.Count > 0;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x001668F8 File Offset: 0x001668F8
		public bool HasModules
		{
			get
			{
				return this.Modules.Count > 0;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x00166908 File Offset: 0x00166908
		public ModuleDef ManifestModule
		{
			get
			{
				if (this.Modules.Count != 0)
				{
					return this.Modules[0];
				}
				return null;
			}
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x00166928 File Offset: 0x00166928
		private void ModifyAttributes(AssemblyAttributes andMask, AssemblyAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x0016693C File Offset: 0x0016693C
		private void ModifyAttributes(bool set, AssemblyAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x00166964 File Offset: 0x00166964
		// (set) Token: 0x06004308 RID: 17160 RVA: 0x00166974 File Offset: 0x00166974
		public bool HasPublicKey
		{
			get
			{
				return (this.attributes & 1) != 0;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.PublicKey);
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x00166980 File Offset: 0x00166980
		// (set) Token: 0x0600430A RID: 17162 RVA: 0x0016698C File Offset: 0x0016698C
		public AssemblyAttributes ProcessorArchitecture
		{
			get
			{
				return (AssemblyAttributes)(this.attributes & 112);
			}
			set
			{
				this.ModifyAttributes(~(AssemblyAttributes.PA_MSIL | AssemblyAttributes.PA_x86 | AssemblyAttributes.PA_AMD64), value & AssemblyAttributes.PA_NoPlatform);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x0016699C File Offset: 0x0016699C
		// (set) Token: 0x0600430C RID: 17164 RVA: 0x001669AC File Offset: 0x001669AC
		public AssemblyAttributes ProcessorArchitectureFull
		{
			get
			{
				return (AssemblyAttributes)(this.attributes & 240);
			}
			set
			{
				this.ModifyAttributes(~(AssemblyAttributes.PA_MSIL | AssemblyAttributes.PA_x86 | AssemblyAttributes.PA_AMD64 | AssemblyAttributes.PA_Specified), value & AssemblyAttributes.PA_FullMask);
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x001669C0 File Offset: 0x001669C0
		public bool IsProcessorArchitectureNone
		{
			get
			{
				return (this.attributes & 112) == 0;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x001669D0 File Offset: 0x001669D0
		public bool IsProcessorArchitectureMSIL
		{
			get
			{
				return (this.attributes & 112) == 16;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x001669E0 File Offset: 0x001669E0
		public bool IsProcessorArchitectureX86
		{
			get
			{
				return (this.attributes & 112) == 32;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x001669F0 File Offset: 0x001669F0
		public bool IsProcessorArchitectureIA64
		{
			get
			{
				return (this.attributes & 112) == 48;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x00166A00 File Offset: 0x00166A00
		public bool IsProcessorArchitectureX64
		{
			get
			{
				return (this.attributes & 112) == 64;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x00166A10 File Offset: 0x00166A10
		public bool IsProcessorArchitectureARM
		{
			get
			{
				return (this.attributes & 112) == 80;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x00166A20 File Offset: 0x00166A20
		public bool IsProcessorArchitectureNoPlatform
		{
			get
			{
				return (this.attributes & 112) == 112;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x00166A30 File Offset: 0x00166A30
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x00166A44 File Offset: 0x00166A44
		public bool IsProcessorArchitectureSpecified
		{
			get
			{
				return (this.attributes & 128) != 0;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.PA_Specified);
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x00166A54 File Offset: 0x00166A54
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x00166A68 File Offset: 0x00166A68
		public bool EnableJITcompileTracking
		{
			get
			{
				return (this.attributes & 32768) != 0;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.EnableJITcompileTracking);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x00166A78 File Offset: 0x00166A78
		// (set) Token: 0x06004319 RID: 17177 RVA: 0x00166A8C File Offset: 0x00166A8C
		public bool DisableJITcompileOptimizer
		{
			get
			{
				return (this.attributes & 16384) != 0;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.DisableJITcompileOptimizer);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x00166A9C File Offset: 0x00166A9C
		// (set) Token: 0x0600431B RID: 17179 RVA: 0x00166AB0 File Offset: 0x00166AB0
		public bool IsRetargetable
		{
			get
			{
				return (this.attributes & 256) != 0;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.Retargetable);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x00166AC0 File Offset: 0x00166AC0
		// (set) Token: 0x0600431D RID: 17181 RVA: 0x00166AD0 File Offset: 0x00166AD0
		public AssemblyAttributes ContentType
		{
			get
			{
				return (AssemblyAttributes)(this.attributes & 3584);
			}
			set
			{
				this.ModifyAttributes((AssemblyAttributes)4294963711U, value & AssemblyAttributes.ContentType_Mask);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x00166AE4 File Offset: 0x00166AE4
		public bool IsContentTypeDefault
		{
			get
			{
				return (this.attributes & 3584) == 0;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x00166AF8 File Offset: 0x00166AF8
		public bool IsContentTypeWindowsRuntime
		{
			get
			{
				return (this.attributes & 3584) == 512;
			}
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x00166B10 File Offset: 0x00166B10
		public ModuleDef FindModule(UTF8String name)
		{
			IList<ModuleDef> list = this.Modules;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleDef moduleDef = list[i];
				if (moduleDef != null && UTF8String.CaseInsensitiveEquals(moduleDef.Name, name))
				{
					return moduleDef;
				}
			}
			return null;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x00166B60 File Offset: 0x00166B60
		public static AssemblyDef Load(string fileName, ModuleContext context)
		{
			return AssemblyDef.Load(fileName, new ModuleCreationOptions(context));
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x00166B70 File Offset: 0x00166B70
		public static AssemblyDef Load(string fileName, ModuleCreationOptions options = null)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			ModuleDef moduleDef = null;
			AssemblyDef result;
			try
			{
				moduleDef = ModuleDefMD.Load(fileName, options);
				AssemblyDef assembly = moduleDef.Assembly;
				if (assembly == null)
				{
					throw new BadImageFormatException(fileName + " is only a .NET module, not a .NET assembly. Use ModuleDef.Load().");
				}
				result = assembly;
			}
			catch
			{
				if (moduleDef != null)
				{
					moduleDef.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x00166BDC File Offset: 0x00166BDC
		public static AssemblyDef Load(byte[] data, ModuleContext context)
		{
			return AssemblyDef.Load(data, new ModuleCreationOptions(context));
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x00166BEC File Offset: 0x00166BEC
		public static AssemblyDef Load(byte[] data, ModuleCreationOptions options = null)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			ModuleDef moduleDef = null;
			AssemblyDef result;
			try
			{
				moduleDef = ModuleDefMD.Load(data, options);
				AssemblyDef assembly = moduleDef.Assembly;
				if (assembly == null)
				{
					throw new BadImageFormatException(moduleDef.ToString() + " is only a .NET module, not a .NET assembly. Use ModuleDef.Load().");
				}
				result = assembly;
			}
			catch
			{
				if (moduleDef != null)
				{
					moduleDef.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x00166C5C File Offset: 0x00166C5C
		public static AssemblyDef Load(IntPtr addr, ModuleContext context)
		{
			return AssemblyDef.Load(addr, new ModuleCreationOptions(context));
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x00166C6C File Offset: 0x00166C6C
		public static AssemblyDef Load(IntPtr addr, ModuleCreationOptions options = null)
		{
			if (addr == IntPtr.Zero)
			{
				throw new ArgumentNullException("addr");
			}
			ModuleDef moduleDef = null;
			AssemblyDef result;
			try
			{
				moduleDef = ModuleDefMD.Load(addr, options);
				AssemblyDef assembly = moduleDef.Assembly;
				if (assembly == null)
				{
					throw new BadImageFormatException(string.Format("{0} (addr: {1:X8}) is only a .NET module, not a .NET assembly. Use ModuleDef.Load().", moduleDef.ToString(), addr.ToInt64()));
				}
				result = assembly;
			}
			catch
			{
				if (moduleDef != null)
				{
					moduleDef.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x00166CF4 File Offset: 0x00166CF4
		public static AssemblyDef Load(Stream stream, ModuleContext context)
		{
			return AssemblyDef.Load(stream, new ModuleCreationOptions(context));
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x00166D04 File Offset: 0x00166D04
		public static AssemblyDef Load(Stream stream, ModuleCreationOptions options = null)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			ModuleDef moduleDef = null;
			AssemblyDef result;
			try
			{
				moduleDef = ModuleDefMD.Load(stream, options);
				AssemblyDef assembly = moduleDef.Assembly;
				if (assembly == null)
				{
					throw new BadImageFormatException(moduleDef.ToString() + " is only a .NET module, not a .NET assembly. Use ModuleDef.Load().");
				}
				result = assembly;
			}
			catch
			{
				if (moduleDef != null)
				{
					moduleDef.Dispose();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x00166D74 File Offset: 0x00166D74
		public string GetFullNameWithPublicKey()
		{
			return this.GetFullName(this.publicKey);
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x00166D84 File Offset: 0x00166D84
		public string GetFullNameWithPublicKeyToken()
		{
			return this.GetFullName(this.publicKey.Token);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x00166D98 File Offset: 0x00166D98
		private string GetFullName(PublicKeyBase pkBase)
		{
			return Utils.GetAssemblyNameString(this.name, this.version, this.culture, pkBase, this.Attributes);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x00166DB8 File Offset: 0x00166DB8
		public TypeDef Find(string fullName, bool isReflectionName)
		{
			IList<ModuleDef> list = this.Modules;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleDef moduleDef = list[i];
				if (moduleDef != null)
				{
					TypeDef typeDef = moduleDef.Find(fullName, isReflectionName);
					if (typeDef != null)
					{
						return typeDef;
					}
				}
			}
			return null;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x00166E0C File Offset: 0x00166E0C
		public TypeDef Find(TypeRef typeRef)
		{
			IList<ModuleDef> list = this.Modules;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				ModuleDef moduleDef = list[i];
				if (moduleDef != null)
				{
					TypeDef typeDef = moduleDef.Find(typeRef);
					if (typeDef != null)
					{
						return typeDef;
					}
				}
			}
			return null;
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x00166E5C File Offset: 0x00166E5C
		public void Write(string filename, ModuleWriterOptions options = null)
		{
			this.ManifestModule.Write(filename, options);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x00166E6C File Offset: 0x00166E6C
		public void Write(Stream dest, ModuleWriterOptions options = null)
		{
			this.ManifestModule.Write(dest, options);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x00166E7C File Offset: 0x00166E7C
		public bool IsFriendAssemblyOf(AssemblyDef targetAsm)
		{
			if (targetAsm == null)
			{
				return false;
			}
			if (this == targetAsm)
			{
				return true;
			}
			if (PublicKeyBase.IsNullOrEmpty2(this.publicKey) != PublicKeyBase.IsNullOrEmpty2(targetAsm.PublicKey))
			{
				return false;
			}
			foreach (CustomAttribute customAttribute in targetAsm.CustomAttributes.FindAll("System.Runtime.CompilerServices.InternalsVisibleToAttribute"))
			{
				if (customAttribute.ConstructorArguments.Count == 1)
				{
					CAArgument caargument = (customAttribute.ConstructorArguments.Count == 0) ? default(CAArgument) : customAttribute.ConstructorArguments[0];
					if (caargument.Type.GetElementType() == ElementType.String)
					{
						UTF8String utf8String = caargument.Value as UTF8String;
						if (!UTF8String.IsNull(utf8String))
						{
							AssemblyNameInfo assemblyNameInfo = new AssemblyNameInfo(utf8String);
							if (!(assemblyNameInfo.Name != this.name))
							{
								if (!PublicKeyBase.IsNullOrEmpty2(this.publicKey))
								{
									if (!this.PublicKey.Equals(assemblyNameInfo.PublicKeyOrToken as PublicKey))
									{
										continue;
									}
								}
								else if (!PublicKeyBase.IsNullOrEmpty2(assemblyNameInfo.PublicKeyOrToken))
								{
									continue;
								}
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x00166FE4 File Offset: 0x00166FE4
		public void UpdateOrCreateAssemblySignatureKeyAttribute(StrongNamePublicKey identityPubKey, StrongNameKey identityKey, StrongNamePublicKey signaturePubKey)
		{
			ModuleDef manifestModule = this.ManifestModule;
			if (manifestModule == null)
			{
				return;
			}
			CustomAttribute customAttribute = null;
			for (int i = 0; i < this.CustomAttributes.Count; i++)
			{
				CustomAttribute customAttribute2 = this.CustomAttributes[i];
				if (!(customAttribute2.TypeFullName != "System.Reflection.AssemblySignatureKeyAttribute"))
				{
					this.CustomAttributes.RemoveAt(i);
					i--;
					if (customAttribute == null)
					{
						customAttribute = customAttribute2;
					}
				}
			}
			if (this.IsValidAssemblySignatureKeyAttribute(customAttribute))
			{
				customAttribute.NamedArguments.Clear();
			}
			else
			{
				customAttribute = this.CreateAssemblySignatureKeyAttribute();
			}
			string s = StrongNameKey.CreateCounterSignatureAsString(identityPubKey, identityKey, signaturePubKey);
			customAttribute.ConstructorArguments[0] = new CAArgument(manifestModule.CorLibTypes.String, new UTF8String(signaturePubKey.ToString()));
			customAttribute.ConstructorArguments[1] = new CAArgument(manifestModule.CorLibTypes.String, new UTF8String(s));
			this.CustomAttributes.Add(customAttribute);
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x001670DC File Offset: 0x001670DC
		private bool IsValidAssemblySignatureKeyAttribute(CustomAttribute ca)
		{
			if (Settings.IsThreadSafe)
			{
				return false;
			}
			if (ca == null)
			{
				return false;
			}
			ICustomAttributeType constructor = ca.Constructor;
			if (constructor == null)
			{
				return false;
			}
			MethodSig methodSig = constructor.MethodSig;
			return methodSig != null && methodSig.Params.Count == 2 && methodSig.Params[0].GetElementType() == ElementType.String && methodSig.Params[1].GetElementType() == ElementType.String && ca.ConstructorArguments.Count == 2;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x00167178 File Offset: 0x00167178
		private CustomAttribute CreateAssemblySignatureKeyAttribute()
		{
			ModuleDef manifestModule = this.ManifestModule;
			TypeRefUser @class = manifestModule.UpdateRowId<TypeRefUser>(new TypeRefUser(manifestModule, "System.Reflection", "AssemblySignatureKeyAttribute", manifestModule.CorLibTypes.AssemblyRef));
			MethodSig sig = MethodSig.CreateInstance(manifestModule.CorLibTypes.Void, manifestModule.CorLibTypes.String, manifestModule.CorLibTypes.String);
			return new CustomAttribute(manifestModule.UpdateRowId<MemberRefUser>(new MemberRefUser(manifestModule, MethodDef.InstanceConstructorName, sig, @class)))
			{
				ConstructorArguments = 
				{
					new CAArgument(manifestModule.CorLibTypes.String, UTF8String.Empty),
					new CAArgument(manifestModule.CorLibTypes.String, UTF8String.Empty)
				}
			};
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x0016723C File Offset: 0x0016723C
		public virtual bool TryGetOriginalTargetFrameworkAttribute(out string framework, out Version version, out string profile)
		{
			framework = null;
			version = null;
			profile = null;
			return false;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x00167248 File Offset: 0x00167248
		void IListListener<ModuleDef>.OnLazyAdd(int index, ref ModuleDef module)
		{
			ModuleDef moduleDef = module;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x00167250 File Offset: 0x00167250
		void IListListener<ModuleDef>.OnAdd(int index, ModuleDef module)
		{
			if (module == null)
			{
				return;
			}
			if (module.Assembly != null)
			{
				throw new InvalidOperationException("Module already has an assembly. Remove it from that assembly before adding it to this assembly.");
			}
			module.Assembly = this;
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x00167278 File Offset: 0x00167278
		void IListListener<ModuleDef>.OnRemove(int index, ModuleDef module)
		{
			if (module != null)
			{
				module.Assembly = null;
			}
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x00167288 File Offset: 0x00167288
		void IListListener<ModuleDef>.OnResize(int index)
		{
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x0016728C File Offset: 0x0016728C
		void IListListener<ModuleDef>.OnClear()
		{
			foreach (ModuleDef moduleDef in this.modules.GetEnumerable_NoLock())
			{
				if (moduleDef != null)
				{
					moduleDef.Assembly = null;
				}
			}
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x001672F0 File Offset: 0x001672F0
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040023B4 RID: 9140
		protected uint rid;

		// Token: 0x040023B5 RID: 9141
		protected AssemblyHashAlgorithm hashAlgorithm;

		// Token: 0x040023B6 RID: 9142
		protected Version version;

		// Token: 0x040023B7 RID: 9143
		protected int attributes;

		// Token: 0x040023B8 RID: 9144
		protected PublicKey publicKey;

		// Token: 0x040023B9 RID: 9145
		protected UTF8String name;

		// Token: 0x040023BA RID: 9146
		protected UTF8String culture;

		// Token: 0x040023BB RID: 9147
		protected IList<DeclSecurity> declSecurities;

		// Token: 0x040023BC RID: 9148
		protected LazyList<ModuleDef> modules;

		// Token: 0x040023BD RID: 9149
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040023BE RID: 9150
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
