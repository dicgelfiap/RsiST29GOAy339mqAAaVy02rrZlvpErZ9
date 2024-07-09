using System;
using System.Runtime.InteropServices;

namespace dnlib.DotNet
{
	// Token: 0x020007BE RID: 1982
	[ComVisible(true)]
	public readonly struct GenericParamContext
	{
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x00177120 File Offset: 0x00177120
		public bool IsEmpty
		{
			get
			{
				return this.Type == null && this.Method == null;
			}
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x00177138 File Offset: 0x00177138
		public static GenericParamContext Create(MethodDef method)
		{
			if (method == null)
			{
				return default(GenericParamContext);
			}
			return new GenericParamContext(method.DeclaringType, method);
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x00177168 File Offset: 0x00177168
		public static GenericParamContext Create(TypeDef type)
		{
			return new GenericParamContext(type);
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00177170 File Offset: 0x00177170
		public GenericParamContext(TypeDef type)
		{
			this.Type = type;
			this.Method = null;
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x00177180 File Offset: 0x00177180
		public GenericParamContext(MethodDef method)
		{
			this.Type = null;
			this.Method = method;
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x00177190 File Offset: 0x00177190
		public GenericParamContext(TypeDef type, MethodDef method)
		{
			this.Type = type;
			this.Method = method;
		}

		// Token: 0x0400250B RID: 9483
		public readonly TypeDef Type;

		// Token: 0x0400250C RID: 9484
		public readonly MethodDef Method;
	}
}
