using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000AAF RID: 2735
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DynamicProxyMetaObject<[Nullable(2)] T> : DynamicMetaObject
	{
		// Token: 0x06006CFC RID: 27900 RVA: 0x0020F318 File Offset: 0x0020F318
		internal DynamicProxyMetaObject(Expression expression, T value, DynamicProxy<T> proxy) : base(expression, BindingRestrictions.Empty, value)
		{
			this._proxy = proxy;
		}

		// Token: 0x06006CFD RID: 27901 RVA: 0x0020F334 File Offset: 0x0020F334
		private bool IsOverridden(string method)
		{
			return ReflectionUtils.IsMethodOverridden(this._proxy.GetType(), typeof(DynamicProxy<T>), method);
		}

		// Token: 0x06006CFE RID: 27902 RVA: 0x0020F354 File Offset: 0x0020F354
		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			if (!this.IsOverridden("TryGetMember"))
			{
				return base.BindGetMember(binder);
			}
			return this.CallMethodWithResult("TryGetMember", binder, DynamicProxyMetaObject<T>.NoArgs, ([Nullable(2)] DynamicMetaObject e) => binder.FallbackGetMember(this, e), null);
		}

		// Token: 0x06006CFF RID: 27903 RVA: 0x0020F3BC File Offset: 0x0020F3BC
		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			if (!this.IsOverridden("TrySetMember"))
			{
				return base.BindSetMember(binder, value);
			}
			return this.CallMethodReturnLast("TrySetMember", binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[]
			{
				value
			}), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackSetMember(this, value, e));
		}

		// Token: 0x06006D00 RID: 27904 RVA: 0x0020F43C File Offset: 0x0020F43C
		public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			if (!this.IsOverridden("TryDeleteMember"))
			{
				return base.BindDeleteMember(binder);
			}
			return this.CallMethodNoResult("TryDeleteMember", binder, DynamicProxyMetaObject<T>.NoArgs, ([Nullable(2)] DynamicMetaObject e) => binder.FallbackDeleteMember(this, e));
		}

		// Token: 0x06006D01 RID: 27905 RVA: 0x0020F4A0 File Offset: 0x0020F4A0
		public override DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			if (!this.IsOverridden("TryConvert"))
			{
				return base.BindConvert(binder);
			}
			return this.CallMethodWithResult("TryConvert", binder, DynamicProxyMetaObject<T>.NoArgs, ([Nullable(2)] DynamicMetaObject e) => binder.FallbackConvert(this, e), null);
		}

		// Token: 0x06006D02 RID: 27906 RVA: 0x0020F508 File Offset: 0x0020F508
		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryInvokeMember"))
			{
				return base.BindInvokeMember(binder, args);
			}
			DynamicProxyMetaObject<T>.Fallback fallback = ([Nullable(2)] DynamicMetaObject e) => binder.FallbackInvokeMember(this, args, e);
			return this.BuildCallMethodWithResult("TryInvokeMember", binder, DynamicProxyMetaObject<T>.GetArgArray(args), this.BuildCallMethodWithResult("TryGetMember", new DynamicProxyMetaObject<T>.GetBinderAdapter(binder), DynamicProxyMetaObject<T>.NoArgs, fallback(null), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x0020F5B0 File Offset: 0x0020F5B0
		public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryCreateInstance"))
			{
				return base.BindCreateInstance(binder, args);
			}
			return this.CallMethodWithResult("TryCreateInstance", binder, DynamicProxyMetaObject<T>.GetArgArray(args), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackCreateInstance(this, args, e), null);
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x0020F628 File Offset: 0x0020F628
		public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryInvoke"))
			{
				return base.BindInvoke(binder, args);
			}
			return this.CallMethodWithResult("TryInvoke", binder, DynamicProxyMetaObject<T>.GetArgArray(args), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackInvoke(this, args, e), null);
		}

		// Token: 0x06006D05 RID: 27909 RVA: 0x0020F6A0 File Offset: 0x0020F6A0
		public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			if (!this.IsOverridden("TryBinaryOperation"))
			{
				return base.BindBinaryOperation(binder, arg);
			}
			return this.CallMethodWithResult("TryBinaryOperation", binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[]
			{
				arg
			}), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackBinaryOperation(this, arg, e), null);
		}

		// Token: 0x06006D06 RID: 27910 RVA: 0x0020F724 File Offset: 0x0020F724
		public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			if (!this.IsOverridden("TryUnaryOperation"))
			{
				return base.BindUnaryOperation(binder);
			}
			return this.CallMethodWithResult("TryUnaryOperation", binder, DynamicProxyMetaObject<T>.NoArgs, ([Nullable(2)] DynamicMetaObject e) => binder.FallbackUnaryOperation(this, e), null);
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x0020F78C File Offset: 0x0020F78C
		public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden("TryGetIndex"))
			{
				return base.BindGetIndex(binder, indexes);
			}
			return this.CallMethodWithResult("TryGetIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackGetIndex(this, indexes, e), null);
		}

		// Token: 0x06006D08 RID: 27912 RVA: 0x0020F804 File Offset: 0x0020F804
		public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			if (!this.IsOverridden("TrySetIndex"))
			{
				return base.BindSetIndex(binder, indexes, value);
			}
			return this.CallMethodReturnLast("TrySetIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes, value), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackSetIndex(this, indexes, value, e));
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x0020F890 File Offset: 0x0020F890
		public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden("TryDeleteIndex"))
			{
				return base.BindDeleteIndex(binder, indexes);
			}
			return this.CallMethodNoResult("TryDeleteIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), ([Nullable(2)] DynamicMetaObject e) => binder.FallbackDeleteIndex(this, indexes, e));
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06006D0A RID: 27914 RVA: 0x0020F908 File Offset: 0x0020F908
		private static Expression[] NoArgs
		{
			get
			{
				return CollectionUtils.ArrayEmpty<Expression>();
			}
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x0020F910 File Offset: 0x0020F910
		private static IEnumerable<Expression> GetArgs(params DynamicMetaObject[] args)
		{
			return args.Select(delegate(DynamicMetaObject arg)
			{
				Expression expression = arg.Expression;
				if (!expression.Type.IsValueType())
				{
					return expression;
				}
				return Expression.Convert(expression, typeof(object));
			});
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x0020F93C File Offset: 0x0020F93C
		private static Expression[] GetArgArray(DynamicMetaObject[] args)
		{
			return new NewArrayExpression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args))
			};
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x0020F970 File Offset: 0x0020F970
		private static Expression[] GetArgArray(DynamicMetaObject[] args, DynamicMetaObject value)
		{
			Expression expression = value.Expression;
			return new Expression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args)),
				expression.Type.IsValueType() ? Expression.Convert(expression, typeof(object)) : expression
			};
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x0020F9D0 File Offset: 0x0020F9D0
		private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
		{
			Type type = binder.GetType();
			while (!type.IsVisible())
			{
				type = type.BaseType();
			}
			return Expression.Constant(binder, type);
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x0020FA04 File Offset: 0x0020FA04
		private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, [Nullable(new byte[]
		{
			1,
			0
		})] DynamicProxyMetaObject<T>.Fallback fallback, [Nullable(new byte[]
		{
			2,
			0
		})] DynamicProxyMetaObject<T>.Fallback fallbackInvoke = null)
		{
			DynamicMetaObject fallbackResult = fallback(null);
			return this.BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x0020FA2C File Offset: 0x0020FA2C
		private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicMetaObject fallbackResult, [Nullable(new byte[]
		{
			2,
			0
		})] DynamicProxyMetaObject<T>.Fallback fallbackInvoke)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list.Add(parameterExpression);
			DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
			if (binder.ReturnType != typeof(object))
			{
				dynamicMetaObject = new DynamicMetaObject(Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType), dynamicMetaObject.Restrictions);
			}
			if (fallbackInvoke != null)
			{
				dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
			}
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), dynamicMetaObject.Expression, fallbackResult.Expression, binder.ReturnType)
			}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x0020FB50 File Offset: 0x0020FB50
		private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, [Nullable(new byte[]
		{
			1,
			0
		})] DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list[list.Count - 1] = Expression.Assign(parameterExpression, list[list.Count - 1]);
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[]
			{
				parameterExpression
			}, new Expression[]
			{
				Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), parameterExpression, dynamicMetaObject.Expression, typeof(object))
			}), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x0020FC38 File Offset: 0x0020FC38
		private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, [Nullable(new byte[]
		{
			1,
			0
		})] DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			return new DynamicMetaObject(Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), Expression.Empty(), dynamicMetaObject.Expression, typeof(void)), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		// Token: 0x06006D13 RID: 27923 RVA: 0x0020FCD8 File Offset: 0x0020FCD8
		private BindingRestrictions GetRestrictions()
		{
			if (base.Value != null || !base.HasValue)
			{
				return BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
			}
			return BindingRestrictions.GetInstanceRestriction(base.Expression, null);
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x0020FD20 File Offset: 0x0020FD20
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this._proxy.GetDynamicMemberNames((T)((object)base.Value));
		}

		// Token: 0x040036BC RID: 14012
		private readonly DynamicProxy<T> _proxy;

		// Token: 0x020010C3 RID: 4291
		// (Invoke) Token: 0x06009130 RID: 37168
		[NullableContext(0)]
		private delegate DynamicMetaObject Fallback([Nullable(2)] DynamicMetaObject errorSuggestion);

		// Token: 0x020010C4 RID: 4292
		[Nullable(0)]
		private sealed class GetBinderAdapter : GetMemberBinder
		{
			// Token: 0x06009133 RID: 37171 RVA: 0x002BB4C0 File Offset: 0x002BB4C0
			internal GetBinderAdapter(InvokeMemberBinder binder) : base(binder.Name, binder.IgnoreCase)
			{
			}

			// Token: 0x06009134 RID: 37172 RVA: 0x002BB4D4 File Offset: 0x002BB4D4
			public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
			{
				throw new NotSupportedException();
			}
		}
	}
}
