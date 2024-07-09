using System;
using System.Runtime.InteropServices;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C7B RID: 3195
	[ComVisible(true)]
	public class TypeFormatEventArgs : EventArgs
	{
		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x06007F7B RID: 32635 RVA: 0x0025ACF4 File Offset: 0x0025ACF4
		// (set) Token: 0x06007F7C RID: 32636 RVA: 0x0025ACFC File Offset: 0x0025ACFC
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (this.type != value)
				{
					if (this.typeFixed)
					{
						throw new InvalidOperationException("The type is fixed and cannot be changed");
					}
					this.type = value;
				}
			}
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x06007F7D RID: 32637 RVA: 0x0025AD2C File Offset: 0x0025AD2C
		// (set) Token: 0x06007F7E RID: 32638 RVA: 0x0025AD34 File Offset: 0x0025AD34
		public string FormattedName
		{
			get
			{
				return this.formattedName;
			}
			set
			{
				if (this.formattedName != value)
				{
					if (!this.typeFixed)
					{
						throw new InvalidOperationException("The formatted-name is fixed and cannot be changed");
					}
					this.formattedName = value;
				}
			}
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x0025AD64 File Offset: 0x0025AD64
		internal TypeFormatEventArgs(string formattedName)
		{
			if (string.IsNullOrEmpty(formattedName))
			{
				throw new ArgumentNullException("formattedName");
			}
			this.formattedName = formattedName;
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x0025AD8C File Offset: 0x0025AD8C
		internal TypeFormatEventArgs(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
			this.typeFixed = true;
		}

		// Token: 0x04003CDF RID: 15583
		private Type type;

		// Token: 0x04003CE0 RID: 15584
		private string formattedName;

		// Token: 0x04003CE1 RID: 15585
		private readonly bool typeFixed;
	}
}
