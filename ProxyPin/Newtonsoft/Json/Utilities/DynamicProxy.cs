using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AAE RID: 2734
	[NullableContext(1)]
	[Nullable(0)]
	internal class DynamicProxy<[Nullable(2)] T>
	{
		// Token: 0x06006CEE RID: 27886 RVA: 0x0020F2B8 File Offset: 0x0020F2B8
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return CollectionUtils.ArrayEmpty<string>();
		}

		// Token: 0x06006CEF RID: 27887 RVA: 0x0020F2C0 File Offset: 0x0020F2C0
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x0020F2C8 File Offset: 0x0020F2C8
		public virtual bool TryConvert(T instance, ConvertBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x0020F2D0 File Offset: 0x0020F2D0
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x0020F2D8 File Offset: 0x0020F2D8
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x0020F2DC File Offset: 0x0020F2DC
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x06006CF4 RID: 27892 RVA: 0x0020F2E0 File Offset: 0x0020F2E0
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x0020F2E8 File Offset: 0x0020F2E8
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x0020F2F0 File Offset: 0x0020F2F0
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x0020F2F8 File Offset: 0x0020F2F8
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x0020F300 File Offset: 0x0020F300
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x06006CF9 RID: 27897 RVA: 0x0020F304 File Offset: 0x0020F304
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x06006CFA RID: 27898 RVA: 0x0020F308 File Offset: 0x0020F308
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}
	}
}
