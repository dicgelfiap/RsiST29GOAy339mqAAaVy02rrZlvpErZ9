using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000799 RID: 1945
	[ComVisible(true)]
	public sealed class CANamedArgument : ICloneable
	{
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x0016DAC4 File Offset: 0x0016DAC4
		// (set) Token: 0x06004582 RID: 17794 RVA: 0x0016DACC File Offset: 0x0016DACC
		public bool IsField
		{
			get
			{
				return this.isField;
			}
			set
			{
				this.isField = value;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x0016DAD8 File Offset: 0x0016DAD8
		// (set) Token: 0x06004584 RID: 17796 RVA: 0x0016DAE4 File Offset: 0x0016DAE4
		public bool IsProperty
		{
			get
			{
				return !this.isField;
			}
			set
			{
				this.isField = !value;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x0016DAF0 File Offset: 0x0016DAF0
		// (set) Token: 0x06004586 RID: 17798 RVA: 0x0016DAF8 File Offset: 0x0016DAF8
		public TypeSig Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x0016DB04 File Offset: 0x0016DB04
		// (set) Token: 0x06004588 RID: 17800 RVA: 0x0016DB0C File Offset: 0x0016DB0C
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

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x0016DB18 File Offset: 0x0016DB18
		// (set) Token: 0x0600458A RID: 17802 RVA: 0x0016DB20 File Offset: 0x0016DB20
		public CAArgument Argument
		{
			get
			{
				return this.argument;
			}
			set
			{
				this.argument = value;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x0016DB2C File Offset: 0x0016DB2C
		// (set) Token: 0x0600458C RID: 17804 RVA: 0x0016DB3C File Offset: 0x0016DB3C
		public TypeSig ArgumentType
		{
			get
			{
				return this.argument.Type;
			}
			set
			{
				this.argument.Type = value;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x0016DB4C File Offset: 0x0016DB4C
		// (set) Token: 0x0600458E RID: 17806 RVA: 0x0016DB5C File Offset: 0x0016DB5C
		public object Value
		{
			get
			{
				return this.argument.Value;
			}
			set
			{
				this.argument.Value = value;
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x0016DB6C File Offset: 0x0016DB6C
		public CANamedArgument()
		{
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x0016DB74 File Offset: 0x0016DB74
		public CANamedArgument(bool isField)
		{
			this.isField = isField;
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x0016DB84 File Offset: 0x0016DB84
		public CANamedArgument(bool isField, TypeSig type)
		{
			this.isField = isField;
			this.type = type;
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x0016DB9C File Offset: 0x0016DB9C
		public CANamedArgument(bool isField, TypeSig type, UTF8String name)
		{
			this.isField = isField;
			this.type = type;
			this.name = name;
		}

		// Token: 0x06004593 RID: 17811 RVA: 0x0016DBBC File Offset: 0x0016DBBC
		public CANamedArgument(bool isField, TypeSig type, UTF8String name, CAArgument argument)
		{
			this.isField = isField;
			this.type = type;
			this.name = name;
			this.argument = argument;
		}

		// Token: 0x06004594 RID: 17812 RVA: 0x0016DBE4 File Offset: 0x0016DBE4
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x0016DBEC File Offset: 0x0016DBEC
		public CANamedArgument Clone()
		{
			return new CANamedArgument(this.isField, this.type, this.name, this.argument.Clone());
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x0016DC10 File Offset: 0x0016DC10
		public override string ToString()
		{
			return string.Format("({0}) {1} {2} = {3} ({4})", new object[]
			{
				this.isField ? "field" : "property",
				this.type,
				this.name,
				this.Value ?? "null",
				this.ArgumentType
			});
		}

		// Token: 0x0400244A RID: 9290
		private bool isField;

		// Token: 0x0400244B RID: 9291
		private TypeSig type;

		// Token: 0x0400244C RID: 9292
		private UTF8String name;

		// Token: 0x0400244D RID: 9293
		private CAArgument argument;
	}
}
