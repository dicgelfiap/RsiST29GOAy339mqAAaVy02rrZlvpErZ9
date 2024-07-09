using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet.MD;
using dnlib.DotNet.Pdb;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x020007A4 RID: 1956
	[ComVisible(true)]
	public abstract class EventDef : IHasCustomAttribute, ICodedToken, IMDTokenProvider, IHasSemantic, IFullName, IMemberRef, IOwnerModule, IIsTypeOrMethod, IHasCustomDebugInformation, IMemberDef, IDnlibDef
	{
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x0016F41C File Offset: 0x0016F41C
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Event, this.rid);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x0016F42C File Offset: 0x0016F42C
		// (set) Token: 0x060045EC RID: 17900 RVA: 0x0016F434 File Offset: 0x0016F434
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

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x0016F440 File Offset: 0x0016F440
		public int HasCustomAttributeTag
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x0016F444 File Offset: 0x0016F444
		public int HasSemanticTag
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x0016F448 File Offset: 0x0016F448
		// (set) Token: 0x060045F0 RID: 17904 RVA: 0x0016F454 File Offset: 0x0016F454
		public EventAttributes Attributes
		{
			get
			{
				return (EventAttributes)this.attributes;
			}
			set
			{
				this.attributes = (int)value;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x0016F460 File Offset: 0x0016F460
		// (set) Token: 0x060045F2 RID: 17906 RVA: 0x0016F468 File Offset: 0x0016F468
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

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x0016F474 File Offset: 0x0016F474
		// (set) Token: 0x060045F4 RID: 17908 RVA: 0x0016F47C File Offset: 0x0016F47C
		public ITypeDefOrRef EventType
		{
			get
			{
				return this.eventType;
			}
			set
			{
				this.eventType = value;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x0016F488 File Offset: 0x0016F488
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

		// Token: 0x060045F6 RID: 17910 RVA: 0x0016F4A4 File Offset: 0x0016F4A4
		protected virtual void InitializeCustomAttributes()
		{
			Interlocked.CompareExchange<CustomAttributeCollection>(ref this.customAttributes, new CustomAttributeCollection(), null);
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x0016F4B8 File Offset: 0x0016F4B8
		public int HasCustomDebugInformationTag
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x0016F4BC File Offset: 0x0016F4BC
		public bool HasCustomDebugInfos
		{
			get
			{
				return this.CustomDebugInfos.Count > 0;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x0016F4CC File Offset: 0x0016F4CC
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

		// Token: 0x060045FA RID: 17914 RVA: 0x0016F4E8 File Offset: 0x0016F4E8
		protected virtual void InitializeCustomDebugInfos()
		{
			Interlocked.CompareExchange<IList<PdbCustomDebugInfo>>(ref this.customDebugInfos, new List<PdbCustomDebugInfo>(), null);
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x0016F4FC File Offset: 0x0016F4FC
		// (set) Token: 0x060045FC RID: 17916 RVA: 0x0016F518 File Offset: 0x0016F518
		public MethodDef AddMethod
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				return this.addMethod;
			}
			set
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				this.addMethod = value;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x0016F534 File Offset: 0x0016F534
		// (set) Token: 0x060045FE RID: 17918 RVA: 0x0016F550 File Offset: 0x0016F550
		public MethodDef InvokeMethod
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				return this.invokeMethod;
			}
			set
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				this.invokeMethod = value;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x0016F56C File Offset: 0x0016F56C
		// (set) Token: 0x06004600 RID: 17920 RVA: 0x0016F588 File Offset: 0x0016F588
		public MethodDef RemoveMethod
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				return this.removeMethod;
			}
			set
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				this.removeMethod = value;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x0016F5A4 File Offset: 0x0016F5A4
		public IList<MethodDef> OtherMethods
		{
			get
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods();
				}
				return this.otherMethods;
			}
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x0016F5C0 File Offset: 0x0016F5C0
		private void InitializeEventMethods()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (this.otherMethods == null)
				{
					this.InitializeEventMethods_NoLock();
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x0016F60C File Offset: 0x0016F60C
		protected virtual void InitializeEventMethods_NoLock()
		{
			this.otherMethods = new List<MethodDef>();
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x0016F61C File Offset: 0x0016F61C
		protected void ResetMethods()
		{
			this.otherMethods = null;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x0016F628 File Offset: 0x0016F628
		public bool IsEmpty
		{
			get
			{
				return this.AddMethod == null && this.removeMethod == null && this.invokeMethod == null && this.otherMethods.Count == 0;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x0016F65C File Offset: 0x0016F65C
		public bool HasCustomAttributes
		{
			get
			{
				return this.CustomAttributes.Count > 0;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x0016F66C File Offset: 0x0016F66C
		public bool HasOtherMethods
		{
			get
			{
				return this.OtherMethods.Count > 0;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x0016F67C File Offset: 0x0016F67C
		// (set) Token: 0x06004609 RID: 17929 RVA: 0x0016F684 File Offset: 0x0016F684
		public TypeDef DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
			set
			{
				TypeDef typeDef = this.DeclaringType2;
				if (typeDef == value)
				{
					return;
				}
				if (typeDef != null)
				{
					typeDef.Events.Remove(this);
				}
				if (value != null)
				{
					value.Events.Add(this);
				}
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x0016F6CC File Offset: 0x0016F6CC
		ITypeDefOrRef IMemberRef.DeclaringType
		{
			get
			{
				return this.declaringType2;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x0016F6D4 File Offset: 0x0016F6D4
		// (set) Token: 0x0600460C RID: 17932 RVA: 0x0016F6DC File Offset: 0x0016F6DC
		public TypeDef DeclaringType2
		{
			get
			{
				return this.declaringType2;
			}
			set
			{
				this.declaringType2 = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x0016F6E8 File Offset: 0x0016F6E8
		public ModuleDef Module
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				if (typeDef == null)
				{
					return null;
				}
				return typeDef.Module;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x0016F700 File Offset: 0x0016F700
		public string FullName
		{
			get
			{
				TypeDef typeDef = this.declaringType2;
				return FullNameFactory.EventFullName((typeDef != null) ? typeDef.FullName : null, this.name, this.eventType, null, null);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x0016F730 File Offset: 0x0016F730
		bool IIsTypeOrMethod.IsType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x0016F734 File Offset: 0x0016F734
		bool IIsTypeOrMethod.IsMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x0016F738 File Offset: 0x0016F738
		bool IMemberRef.IsField
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x0016F73C File Offset: 0x0016F73C
		bool IMemberRef.IsTypeSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x0016F740 File Offset: 0x0016F740
		bool IMemberRef.IsTypeRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x0016F744 File Offset: 0x0016F744
		bool IMemberRef.IsTypeDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x0016F748 File Offset: 0x0016F748
		bool IMemberRef.IsMethodSpec
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x0016F74C File Offset: 0x0016F74C
		bool IMemberRef.IsMethodDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x0016F750 File Offset: 0x0016F750
		bool IMemberRef.IsMemberRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x0016F754 File Offset: 0x0016F754
		bool IMemberRef.IsFieldDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x0016F758 File Offset: 0x0016F758
		bool IMemberRef.IsPropertyDef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x0016F75C File Offset: 0x0016F75C
		bool IMemberRef.IsEventDef
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x0016F760 File Offset: 0x0016F760
		bool IMemberRef.IsGenericParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x0016F764 File Offset: 0x0016F764
		private void ModifyAttributes(bool set, EventAttributes flags)
		{
			if (set)
			{
				this.attributes |= (int)flags;
				return;
			}
			this.attributes &= (int)(~(int)flags);
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x0016F78C File Offset: 0x0016F78C
		// (set) Token: 0x0600461E RID: 17950 RVA: 0x0016F7A0 File Offset: 0x0016F7A0
		public bool IsSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 512) > 0;
			}
			set
			{
				this.ModifyAttributes(value, EventAttributes.SpecialName);
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x0016F7B0 File Offset: 0x0016F7B0
		// (set) Token: 0x06004620 RID: 17952 RVA: 0x0016F7C4 File Offset: 0x0016F7C4
		public bool IsRuntimeSpecialName
		{
			get
			{
				return ((ushort)this.attributes & 1024) > 0;
			}
			set
			{
				this.ModifyAttributes(value, EventAttributes.RTSpecialName);
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x0016F7D4 File Offset: 0x0016F7D4
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x0400248A RID: 9354
		protected uint rid;

		// Token: 0x0400248B RID: 9355
		private readonly Lock theLock = Lock.Create();

		// Token: 0x0400248C RID: 9356
		protected int attributes;

		// Token: 0x0400248D RID: 9357
		protected UTF8String name;

		// Token: 0x0400248E RID: 9358
		protected ITypeDefOrRef eventType;

		// Token: 0x0400248F RID: 9359
		protected CustomAttributeCollection customAttributes;

		// Token: 0x04002490 RID: 9360
		protected IList<PdbCustomDebugInfo> customDebugInfos;

		// Token: 0x04002491 RID: 9361
		protected MethodDef addMethod;

		// Token: 0x04002492 RID: 9362
		protected MethodDef invokeMethod;

		// Token: 0x04002493 RID: 9363
		protected MethodDef removeMethod;

		// Token: 0x04002494 RID: 9364
		protected IList<MethodDef> otherMethods;

		// Token: 0x04002495 RID: 9365
		protected TypeDef declaringType2;
	}
}
