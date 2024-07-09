using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x02000783 RID: 1923
	[ComVisible(true)]
	public abstract class CallingConventionSig : IContainsGenericParameter
	{
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x0016BD94 File Offset: 0x0016BD94
		// (set) Token: 0x0600448E RID: 17550 RVA: 0x0016BD9C File Offset: 0x0016BD9C
		public byte[] ExtraData
		{
			get
			{
				return this.extraData;
			}
			set
			{
				this.extraData = value;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x0016BDA8 File Offset: 0x0016BDA8
		public bool IsDefault
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.Default;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x0016BDB8 File Offset: 0x0016BDB8
		public bool IsC
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.C;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x0016BDC8 File Offset: 0x0016BDC8
		public bool IsStdCall
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.StdCall;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x0016BDD8 File Offset: 0x0016BDD8
		public bool IsThisCall
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.ThisCall;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x0016BDE8 File Offset: 0x0016BDE8
		public bool IsFastCall
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.FastCall;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x0016BDF8 File Offset: 0x0016BDF8
		public bool IsVarArg
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.VarArg;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x0016BE08 File Offset: 0x0016BE08
		public bool IsField
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.Field;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0016BE18 File Offset: 0x0016BE18
		public bool IsLocalSig
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.LocalSig;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x0016BE28 File Offset: 0x0016BE28
		public bool IsProperty
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.Property;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x0016BE38 File Offset: 0x0016BE38
		public bool IsUnmanaged
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.Unmanaged;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004499 RID: 17561 RVA: 0x0016BE48 File Offset: 0x0016BE48
		public bool IsGenericInst
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.GenericInst;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x0016BE58 File Offset: 0x0016BE58
		public bool IsNativeVarArg
		{
			get
			{
				return (this.callingConvention & CallingConvention.Mask) == CallingConvention.NativeVarArg;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x0016BE68 File Offset: 0x0016BE68
		// (set) Token: 0x0600449C RID: 17564 RVA: 0x0016BE78 File Offset: 0x0016BE78
		public bool Generic
		{
			get
			{
				return (this.callingConvention & CallingConvention.Generic) > CallingConvention.Default;
			}
			set
			{
				if (value)
				{
					this.callingConvention |= CallingConvention.Generic;
					return;
				}
				this.callingConvention &= ~CallingConvention.Generic;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x0016BEA4 File Offset: 0x0016BEA4
		// (set) Token: 0x0600449E RID: 17566 RVA: 0x0016BEB4 File Offset: 0x0016BEB4
		public bool HasThis
		{
			get
			{
				return (this.callingConvention & CallingConvention.HasThis) > CallingConvention.Default;
			}
			set
			{
				if (value)
				{
					this.callingConvention |= CallingConvention.HasThis;
					return;
				}
				this.callingConvention &= ~CallingConvention.HasThis;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x0016BEE0 File Offset: 0x0016BEE0
		// (set) Token: 0x060044A0 RID: 17568 RVA: 0x0016BEF0 File Offset: 0x0016BEF0
		public bool ExplicitThis
		{
			get
			{
				return (this.callingConvention & CallingConvention.ExplicitThis) > CallingConvention.Default;
			}
			set
			{
				if (value)
				{
					this.callingConvention |= CallingConvention.ExplicitThis;
					return;
				}
				this.callingConvention &= ~CallingConvention.ExplicitThis;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x0016BF1C File Offset: 0x0016BF1C
		// (set) Token: 0x060044A2 RID: 17570 RVA: 0x0016BF30 File Offset: 0x0016BF30
		public bool ReservedByCLR
		{
			get
			{
				return (this.callingConvention & CallingConvention.ReservedByCLR) > CallingConvention.Default;
			}
			set
			{
				if (value)
				{
					this.callingConvention |= CallingConvention.ReservedByCLR;
					return;
				}
				this.callingConvention &= ~CallingConvention.ReservedByCLR;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x0016BF5C File Offset: 0x0016BF5C
		public bool ImplicitThis
		{
			get
			{
				return this.HasThis && !this.ExplicitThis;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x0016BF74 File Offset: 0x0016BF74
		public bool ContainsGenericParameter
		{
			get
			{
				return TypeHelper.ContainsGenericParameter(this);
			}
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x0016BF7C File Offset: 0x0016BF7C
		protected CallingConventionSig()
		{
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x0016BF84 File Offset: 0x0016BF84
		protected CallingConventionSig(CallingConvention callingConvention)
		{
			this.callingConvention = callingConvention;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x0016BF94 File Offset: 0x0016BF94
		public CallingConvention GetCallingConvention()
		{
			return this.callingConvention;
		}

		// Token: 0x04002416 RID: 9238
		protected CallingConvention callingConvention;

		// Token: 0x04002417 RID: 9239
		private byte[] extraData;
	}
}
