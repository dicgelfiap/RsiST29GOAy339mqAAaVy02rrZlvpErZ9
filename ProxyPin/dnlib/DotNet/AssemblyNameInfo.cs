using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x0200077D RID: 1917
	[ComVisible(true)]
	public sealed class AssemblyNameInfo : IAssembly, IFullName
	{
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x0016A098 File Offset: 0x0016A098
		// (set) Token: 0x060043E6 RID: 17382 RVA: 0x0016A0A0 File Offset: 0x0016A0A0
		public AssemblyHashAlgorithm HashAlgId
		{
			get
			{
				return this.hashAlgId;
			}
			set
			{
				this.hashAlgId = value;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x0016A0AC File Offset: 0x0016A0AC
		// (set) Token: 0x060043E8 RID: 17384 RVA: 0x0016A0B4 File Offset: 0x0016A0B4
		public Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x0016A0C0 File Offset: 0x0016A0C0
		// (set) Token: 0x060043EA RID: 17386 RVA: 0x0016A0C8 File Offset: 0x0016A0C8
		public AssemblyAttributes Attributes
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x0016A0D4 File Offset: 0x0016A0D4
		// (set) Token: 0x060043EC RID: 17388 RVA: 0x0016A0DC File Offset: 0x0016A0DC
		public PublicKeyBase PublicKeyOrToken
		{
			get
			{
				return this.publicKeyOrToken;
			}
			set
			{
				this.publicKeyOrToken = value;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x0016A0E8 File Offset: 0x0016A0E8
		// (set) Token: 0x060043EE RID: 17390 RVA: 0x0016A0F0 File Offset: 0x0016A0F0
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

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x0016A0FC File Offset: 0x0016A0FC
		// (set) Token: 0x060043F0 RID: 17392 RVA: 0x0016A104 File Offset: 0x0016A104
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

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x0016A110 File Offset: 0x0016A110
		public string FullName
		{
			get
			{
				return this.FullNameToken;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x0016A118 File Offset: 0x0016A118
		public string FullNameToken
		{
			get
			{
				PublicKeyBase token = this.publicKeyOrToken;
				if (token is PublicKey)
				{
					token = (token as PublicKey).Token;
				}
				return Utils.GetAssemblyNameString(this.name, this.version, this.culture, token, this.flags);
			}
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x0016A168 File Offset: 0x0016A168
		private void ModifyAttributes(AssemblyAttributes andMask, AssemblyAttributes orMask)
		{
			this.Attributes = ((this.Attributes & andMask) | orMask);
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x0016A17C File Offset: 0x0016A17C
		private void ModifyAttributes(bool set, AssemblyAttributes flags)
		{
			if (set)
			{
				this.Attributes |= flags;
				return;
			}
			this.Attributes &= ~flags;
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x0016A1A4 File Offset: 0x0016A1A4
		// (set) Token: 0x060043F6 RID: 17398 RVA: 0x0016A1B4 File Offset: 0x0016A1B4
		public bool HasPublicKey
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PublicKey) > AssemblyAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.PublicKey);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x0016A1C0 File Offset: 0x0016A1C0
		// (set) Token: 0x060043F8 RID: 17400 RVA: 0x0016A1CC File Offset: 0x0016A1CC
		public AssemblyAttributes ProcessorArchitecture
		{
			get
			{
				return this.Attributes & AssemblyAttributes.PA_NoPlatform;
			}
			set
			{
				this.ModifyAttributes(~(AssemblyAttributes.PA_MSIL | AssemblyAttributes.PA_x86 | AssemblyAttributes.PA_AMD64), value & AssemblyAttributes.PA_NoPlatform);
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x0016A1DC File Offset: 0x0016A1DC
		// (set) Token: 0x060043FA RID: 17402 RVA: 0x0016A1EC File Offset: 0x0016A1EC
		public AssemblyAttributes ProcessorArchitectureFull
		{
			get
			{
				return this.Attributes & AssemblyAttributes.PA_FullMask;
			}
			set
			{
				this.ModifyAttributes(~(AssemblyAttributes.PA_MSIL | AssemblyAttributes.PA_x86 | AssemblyAttributes.PA_AMD64 | AssemblyAttributes.PA_Specified), value & AssemblyAttributes.PA_FullMask);
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x0016A200 File Offset: 0x0016A200
		public bool IsProcessorArchitectureNone
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.None;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060043FC RID: 17404 RVA: 0x0016A210 File Offset: 0x0016A210
		public bool IsProcessorArchitectureMSIL
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.PA_MSIL;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x0016A220 File Offset: 0x0016A220
		public bool IsProcessorArchitectureX86
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.PA_x86;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x0016A230 File Offset: 0x0016A230
		public bool IsProcessorArchitectureIA64
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.PA_IA64;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x0016A240 File Offset: 0x0016A240
		public bool IsProcessorArchitectureX64
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.PA_AMD64;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x0016A250 File Offset: 0x0016A250
		public bool IsProcessorArchitectureARM
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.PA_ARM;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x0016A260 File Offset: 0x0016A260
		public bool IsProcessorArchitectureNoPlatform
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_NoPlatform) == AssemblyAttributes.PA_NoPlatform;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x0016A270 File Offset: 0x0016A270
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x0016A284 File Offset: 0x0016A284
		public bool IsProcessorArchitectureSpecified
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.PA_Specified) > AssemblyAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.PA_Specified);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x0016A294 File Offset: 0x0016A294
		// (set) Token: 0x06004405 RID: 17413 RVA: 0x0016A2A8 File Offset: 0x0016A2A8
		public bool EnableJITcompileTracking
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.EnableJITcompileTracking) > AssemblyAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.EnableJITcompileTracking);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x0016A2B8 File Offset: 0x0016A2B8
		// (set) Token: 0x06004407 RID: 17415 RVA: 0x0016A2CC File Offset: 0x0016A2CC
		public bool DisableJITcompileOptimizer
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.DisableJITcompileOptimizer) > AssemblyAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.DisableJITcompileOptimizer);
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004408 RID: 17416 RVA: 0x0016A2DC File Offset: 0x0016A2DC
		// (set) Token: 0x06004409 RID: 17417 RVA: 0x0016A2F0 File Offset: 0x0016A2F0
		public bool IsRetargetable
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.Retargetable) > AssemblyAttributes.None;
			}
			set
			{
				this.ModifyAttributes(value, AssemblyAttributes.Retargetable);
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0016A300 File Offset: 0x0016A300
		// (set) Token: 0x0600440B RID: 17419 RVA: 0x0016A310 File Offset: 0x0016A310
		public AssemblyAttributes ContentType
		{
			get
			{
				return this.Attributes & AssemblyAttributes.ContentType_Mask;
			}
			set
			{
				this.ModifyAttributes((AssemblyAttributes)4294963711U, value & AssemblyAttributes.ContentType_Mask);
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0016A324 File Offset: 0x0016A324
		public bool IsContentTypeDefault
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.ContentType_Mask) == AssemblyAttributes.None;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x0016A338 File Offset: 0x0016A338
		public bool IsContentTypeWindowsRuntime
		{
			get
			{
				return (this.Attributes & AssemblyAttributes.ContentType_Mask) == AssemblyAttributes.ContentType_WindowsRuntime;
			}
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x0016A350 File Offset: 0x0016A350
		public AssemblyNameInfo()
		{
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x0016A358 File Offset: 0x0016A358
		public AssemblyNameInfo(string asmFullName) : this(ReflectionTypeNameParser.ParseAssemblyRef(asmFullName))
		{
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x0016A368 File Offset: 0x0016A368
		public AssemblyNameInfo(IAssembly asm)
		{
			if (asm == null)
			{
				return;
			}
			AssemblyDef assemblyDef = asm as AssemblyDef;
			this.hashAlgId = ((assemblyDef == null) ? AssemblyHashAlgorithm.None : assemblyDef.HashAlgorithm);
			this.version = (asm.Version ?? new Version(0, 0, 0, 0));
			this.flags = asm.Attributes;
			this.publicKeyOrToken = asm.PublicKeyOrToken;
			this.name = (UTF8String.IsNullOrEmpty(asm.Name) ? UTF8String.Empty : asm.Name);
			this.culture = (UTF8String.IsNullOrEmpty(asm.Culture) ? UTF8String.Empty : asm.Culture);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x0016A428 File Offset: 0x0016A428
		public AssemblyNameInfo(AssemblyName asmName)
		{
			if (asmName == null)
			{
				return;
			}
			this.hashAlgId = (AssemblyHashAlgorithm)asmName.HashAlgorithm;
			this.version = (asmName.Version ?? new Version(0, 0, 0, 0));
			this.flags = (AssemblyAttributes)asmName.Flags;
			this.publicKeyOrToken = (PublicKeyBase.CreatePublicKey(asmName.GetPublicKey()) ?? PublicKeyBase.CreatePublicKeyToken(asmName.GetPublicKeyToken()));
			this.name = (asmName.Name ?? string.Empty);
			this.culture = ((asmName.CultureInfo != null && asmName.CultureInfo.Name != null) ? asmName.CultureInfo.Name : string.Empty);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x0016A4F8 File Offset: 0x0016A4F8
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040023E2 RID: 9186
		private AssemblyHashAlgorithm hashAlgId;

		// Token: 0x040023E3 RID: 9187
		private Version version;

		// Token: 0x040023E4 RID: 9188
		private AssemblyAttributes flags;

		// Token: 0x040023E5 RID: 9189
		private PublicKeyBase publicKeyOrToken;

		// Token: 0x040023E6 RID: 9190
		private UTF8String name;

		// Token: 0x040023E7 RID: 9191
		private UTF8String culture;
	}
}
