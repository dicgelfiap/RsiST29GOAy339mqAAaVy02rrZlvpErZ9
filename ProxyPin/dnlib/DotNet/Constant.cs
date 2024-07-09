using System;
using System.Runtime.InteropServices;
using dnlib.DotNet.MD;

namespace dnlib.DotNet
{
	// Token: 0x0200078D RID: 1933
	[ComVisible(true)]
	public abstract class Constant : IMDTokenProvider
	{
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x0016C9CC File Offset: 0x0016C9CC
		public MDToken MDToken
		{
			get
			{
				return new MDToken(Table.Constant, this.rid);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x0016C9DC File Offset: 0x0016C9DC
		// (set) Token: 0x06004511 RID: 17681 RVA: 0x0016C9E4 File Offset: 0x0016C9E4
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

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x0016C9F0 File Offset: 0x0016C9F0
		// (set) Token: 0x06004513 RID: 17683 RVA: 0x0016C9F8 File Offset: 0x0016C9F8
		public ElementType Type
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

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x0016CA04 File Offset: 0x0016CA04
		// (set) Token: 0x06004515 RID: 17685 RVA: 0x0016CA0C File Offset: 0x0016CA0C
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002424 RID: 9252
		protected uint rid;

		// Token: 0x04002425 RID: 9253
		protected ElementType type;

		// Token: 0x04002426 RID: 9254
		protected object value;
	}
}
