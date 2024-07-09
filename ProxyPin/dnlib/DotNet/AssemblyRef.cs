using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;

namespace dnlib.DotNet
{
	// Token: 0x0200077E RID: 1918
	[ComVisible(true)]
	public abstract class AssemblyRef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IImplementation, IFullName, IResolutionScope, IHasCustomDebugInformation, IAssembly, IScope
	{
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x0016A500 File Offset: 0x0016A500
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.AssemblyRef, this.rid);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x0016A510 File Offset: 0x0016A510
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x0016A518 File Offset: 0x0016A518
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

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x0016A524 File Offset: 0x0016A524
		public int HasCustomAttributeTag
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x0016A528 File Offset: 0x0016A528
		public int ImplementationTag
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x0016A52C File Offset: 0x0016A52C
		public int ResolutionScopeTag
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x0016A530 File Offset: 0x0016A530
		public ScopeType ScopeType
		{
			get
			{
				return ScopeType.AssemblyRef;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x0016A534 File Offset: 0x0016A534
		public string ScopeName
		{
			get
			{
				return this.FullName;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x0016A53C File Offset: 0x0016A53C
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x0016A544 File Offset: 0x0016A544
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

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x0016A560 File Offset: 0x0016A560
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x0016A568 File Offset: 0x0016A568
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

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x0016A574 File Offset: 0x0016A574
		// (set) Token: 0x06004420 RID: 17440 RVA: 0x0016A57C File Offset: 0x0016A57C
		public PublicKeyBase PublicKeyOrToken
		{
			get
			{
				return this.publicKeyOrToken;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.publicKeyOrToken = value;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x0016A598 File Offset: 0x0016A598
		// (set) Token: 0x06004422 RID: 17442 RVA: 0x0016A5A0 File Offset: 0x0016A5A0
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

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x0016A5AC File Offset: 0x0016A5AC
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x0016A5B4 File Offset: 0x0016A5B4
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

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x0016A5C0 File Offset: 0x0016A5C0
		// (set) Token: 0x06004426 RID: 17446 RVA: 0x0016A5C8 File Offset: 0x0016A5C8
		public byte[] Hash
		{
			get
			{
				return this.hashValue;
			}
			set
			{
				this.hashValue = value;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x0016A5D4 File Offset: 0x0016A5D4
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

		// Token: 0x06004428 RID: 17448 RVA: 0x0016A5F0 File Offset: 0x0016A5F0
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004429 RID: 17449 RVA: 0x0016A604 File Offset: 0x0016A604
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x0016A614 File Offset: 0x0016A614
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x0016A618 File Offset: 0x0016A618
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x0016A628 File Offset: 0x0016A628
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

		// Token: 0x0600442D RID: 17453 RVA: 0x0016A644 File Offset: 0x0016A644
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x0016A658 File Offset: 0x0016A658
		public string FullName
		{
			get
			{
				return this.FullNameToken;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x0016A660 File Offset: 0x0016A660
		public string RealFullName
		{
			get
			{
				return Utils.GetAssemblyNameString(this.name, this.version, this.culture, this.publicKeyOrToken, this.Attributes);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x0016A688 File Offset: 0x0016A688
		public string FullNameToken
		{
			get
			{
				return Utils.GetAssemblyNameString(this.name, this.version, this.culture, PublicKeyBase.ToPublicKeyToken(this.publicKeyOrToken), this.Attributes);
			}
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x0016A6B4 File Offset: 0x0016A6B4
		private void ModifyAttributes(AssemblyAttributes andMask, AssemblyAttributes orMask)
		{
			this.attributes = ((this.attributes & (int)andMask) | (int)orMask);
		}

		// Token: 0x06004432 RID: 17458 RVA: 0x0016A6C8 File Offset: 0x0016A6C8
		private void ModifyAttributes(bool set, AssemblyAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x0016A6F0 File Offset: 0x0016A6F0
		// (set) Token: 0x06004434 RID: 17460 RVA: 0x0016A700 File Offset: 0x0016A700
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

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004435 RID: 17461 RVA: 0x0016A70C File Offset: 0x0016A70C
		// (set) Token: 0x06004436 RID: 17462 RVA: 0x0016A718 File Offset: 0x0016A718
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

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x0016A728 File Offset: 0x0016A728
		// (set) Token: 0x06004438 RID: 17464 RVA: 0x0016A738 File Offset: 0x0016A738
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

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x0016A74C File Offset: 0x0016A74C
		public bool IsProcessorArchitectureNone
		{
			get
			{
				return (this.attributes & 112) == 0;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x0016A75C File Offset: 0x0016A75C
		public bool IsProcessorArchitectureMSIL
		{
			get
			{
				return (this.attributes & 112) == 16;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x0016A76C File Offset: 0x0016A76C
		public bool IsProcessorArchitectureX86
		{
			get
			{
				return (this.attributes & 112) == 32;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x0016A77C File Offset: 0x0016A77C
		public bool IsProcessorArchitectureIA64
		{
			get
			{
				return (this.attributes & 112) == 48;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x0016A78C File Offset: 0x0016A78C
		public bool IsProcessorArchitectureX64
		{
			get
			{
				return (this.attributes & 112) == 64;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x0016A79C File Offset: 0x0016A79C
		public bool IsProcessorArchitectureARM
		{
			get
			{
				return (this.attributes & 112) == 80;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x0016A7AC File Offset: 0x0016A7AC
		public bool IsProcessorArchitectureNoPlatform
		{
			get
			{
				return (this.attributes & 112) == 112;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x0016A7BC File Offset: 0x0016A7BC
		// (set) Token: 0x06004441 RID: 17473 RVA: 0x0016A7D0 File Offset: 0x0016A7D0
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

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x0016A7E0 File Offset: 0x0016A7E0
		// (set) Token: 0x06004443 RID: 17475 RVA: 0x0016A7F4 File Offset: 0x0016A7F4
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

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x0016A804 File Offset: 0x0016A804
		// (set) Token: 0x06004445 RID: 17477 RVA: 0x0016A818 File Offset: 0x0016A818
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

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x0016A828 File Offset: 0x0016A828
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x0016A83C File Offset: 0x0016A83C
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

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x0016A84C File Offset: 0x0016A84C
		// (set) Token: 0x06004449 RID: 17481 RVA: 0x0016A85C File Offset: 0x0016A85C
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

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x0016A870 File Offset: 0x0016A870
		public bool IsContentTypeDefault
		{
			get
			{
				return (this.attributes & 3584) == 0;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x0016A884 File Offset: 0x0016A884
		public bool IsContentTypeWindowsRuntime
		{
			get
			{
				return (this.attributes & 3584) == 512;
			}
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x0016A89C File Offset: 0x0016A89C
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040023E8 RID: 9192
		public static readonly AssemblyRef CurrentAssembly = new AssemblyRefUser("<<<CURRENT_ASSEMBLY>>>");

		// Token: 0x040023E9 RID: 9193
		protected uint rid;

		// Token: 0x040023EA RID: 9194
		protected Version version;

		// Token: 0x040023EB RID: 9195
		protected int attributes;

		// Token: 0x040023EC RID: 9196
		protected PublicKeyBase publicKeyOrToken;

		// Token: 0x040023ED RID: 9197
		protected UTF8String name;

		// Token: 0x040023EE RID: 9198
		protected UTF8String culture;

		// Token: 0x040023EF RID: 9199
		protected byte[] hashValue;

		// Token: 0x040023F0 RID: 9200
		protected CustomAttributeCollection customAttributes;

		// Token: 0x040023F1 RID: 9201
		protected IList<PdbCustomDebugInfo> customDebugInfos;
	}
}
