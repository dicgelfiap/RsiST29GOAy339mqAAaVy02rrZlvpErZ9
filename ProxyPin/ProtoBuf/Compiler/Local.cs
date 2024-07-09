using System;
using System.Reflection.Emit;

namespace ProtoBuf.Compiler
{
	// Token: 0x02000C83 RID: 3203
	internal sealed class Local : IDisposable
	{
		// Token: 0x06008059 RID: 32857 RVA: 0x00260188 File Offset: 0x00260188
		private Local(LocalBuilder value, Type type)
		{
			this.value = value;
			this.type = type;
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x002601A0 File Offset: 0x002601A0
		internal Local(CompilerContext ctx, Type type)
		{
			this.ctx = ctx;
			if (ctx != null)
			{
				this.value = ctx.GetFromPool(type);
			}
			this.type = type;
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x0600805B RID: 32859 RVA: 0x002601CC File Offset: 0x002601CC
		internal LocalBuilder Value
		{
			get
			{
				LocalBuilder localBuilder = this.value;
				if (localBuilder == null)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return localBuilder;
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x0600805C RID: 32860 RVA: 0x002601EC File Offset: 0x002601EC
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x002601F4 File Offset: 0x002601F4
		public Local AsCopy()
		{
			if (this.ctx == null)
			{
				return this;
			}
			return new Local(this.value, this.type);
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x00260214 File Offset: 0x00260214
		public void Dispose()
		{
			if (this.ctx != null)
			{
				this.ctx.ReleaseToPool(this.value);
				this.value = null;
				this.ctx = null;
			}
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x00260240 File Offset: 0x00260240
		internal bool IsSame(Local other)
		{
			if (this == other)
			{
				return true;
			}
			object obj = this.value;
			return other != null && obj == other.value;
		}

		// Token: 0x04003D0F RID: 15631
		private LocalBuilder value;

		// Token: 0x04003D10 RID: 15632
		private readonly Type type;

		// Token: 0x04003D11 RID: 15633
		private CompilerContext ctx;
	}
}
