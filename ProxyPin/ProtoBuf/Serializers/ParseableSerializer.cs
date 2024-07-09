using System;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C5F RID: 3167
	internal sealed class ParseableSerializer : IProtoSerializer
	{
		// Token: 0x06007DC5 RID: 32197 RVA: 0x002507C0 File Offset: 0x002507C0
		public static ParseableSerializer TryCreate(Type type, TypeModel model)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MethodInfo method = type.GetMethod("Parse", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				model.MapType(typeof(string))
			}, null);
			if (method != null && method.ReturnType == type)
			{
				if (Helpers.IsValueType(type))
				{
					MethodInfo customToString = ParseableSerializer.GetCustomToString(type);
					if (customToString == null || customToString.ReturnType != model.MapType(typeof(string)))
					{
						return null;
					}
				}
				return new ParseableSerializer(method);
			}
			return null;
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x00250878 File Offset: 0x00250878
		private static MethodInfo GetCustomToString(Type type)
		{
			return type.GetMethod("ToString", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public, null, Helpers.EmptyTypes, null);
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x00250890 File Offset: 0x00250890
		private ParseableSerializer(MethodInfo parse)
		{
			this.parse = parse;
		}

		// Token: 0x17001B4B RID: 6987
		// (get) Token: 0x06007DC8 RID: 32200 RVA: 0x002508A0 File Offset: 0x002508A0
		public Type ExpectedType
		{
			get
			{
				return this.parse.DeclaringType;
			}
		}

		// Token: 0x17001B4C RID: 6988
		// (get) Token: 0x06007DC9 RID: 32201 RVA: 0x002508B0 File Offset: 0x002508B0
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B4D RID: 6989
		// (get) Token: 0x06007DCA RID: 32202 RVA: 0x002508B4 File Offset: 0x002508B4
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007DCB RID: 32203 RVA: 0x002508B8 File Offset: 0x002508B8
		public object Read(object value, ProtoReader source)
		{
			return this.parse.Invoke(null, new object[]
			{
				source.ReadString()
			});
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x002508E4 File Offset: 0x002508E4
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString(value.ToString(), dest);
		}

		// Token: 0x06007DCD RID: 32205 RVA: 0x002508F4 File Offset: 0x002508F4
		void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			Type expectedType = this.ExpectedType;
			if (Helpers.IsValueType(expectedType))
			{
				using (Local localWithValue = ctx.GetLocalWithValue(expectedType, valueFrom))
				{
					ctx.LoadAddress(localWithValue, expectedType, false);
					ctx.EmitCall(ParseableSerializer.GetCustomToString(expectedType));
					goto IL_62;
				}
			}
			ctx.EmitCall(ctx.MapType(typeof(object)).GetMethod("ToString"));
			IL_62:
			ctx.EmitBasicWrite("WriteString", valueFrom);
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x00250980 File Offset: 0x00250980
		void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
		{
			ctx.EmitBasicRead("ReadString", ctx.MapType(typeof(string)));
			ctx.EmitCall(this.parse);
		}

		// Token: 0x04003C74 RID: 15476
		private readonly MethodInfo parse;
	}
}
