using System;
using System.Collections.Generic;
using System.Reflection;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000C5B RID: 3163
	internal class MapDecorator<TDictionary, TKey, TValue> : ProtoDecoratorBase where TDictionary : class, object, IDictionary<TKey, TValue>
	{
		// Token: 0x06007DA2 RID: 32162 RVA: 0x0024F318 File Offset: 0x0024F318
		internal MapDecorator(TypeModel model, Type concreteType, IProtoSerializer keyTail, IProtoSerializer valueTail, int fieldNumber, WireType wireType, WireType keyWireType, WireType valueWireType, bool overwriteList)
		{
			IProtoSerializer tail;
			if (MapDecorator<TDictionary, TKey, TValue>.DefaultValue != null)
			{
				IProtoSerializer protoSerializer = new DefaultValueDecorator(model, MapDecorator<TDictionary, TKey, TValue>.DefaultValue, new TagDecorator(2, valueWireType, false, valueTail));
				tail = protoSerializer;
			}
			else
			{
				IProtoSerializer protoSerializer = new TagDecorator(2, valueWireType, false, valueTail);
				tail = protoSerializer;
			}
			base..ctor(tail);
			this.wireType = wireType;
			this.keyTail = new DefaultValueDecorator(model, MapDecorator<TDictionary, TKey, TValue>.DefaultKey, new TagDecorator(1, keyWireType, false, keyTail));
			this.fieldNumber = fieldNumber;
			this.concreteType = (concreteType ?? typeof(TDictionary));
			if (keyTail.RequiresOldValue)
			{
				throw new InvalidOperationException("Key tail should not require the old value");
			}
			if (!keyTail.ReturnsValue)
			{
				throw new InvalidOperationException("Key tail should return a value");
			}
			if (!valueTail.ReturnsValue)
			{
				throw new InvalidOperationException("Value tail should return a value");
			}
			this.AppendToCollection = !overwriteList;
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x0024F404 File Offset: 0x0024F404
		private static MethodInfo GetIndexerSetter()
		{
			foreach (PropertyInfo propertyInfo in typeof(TDictionary).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!(propertyInfo.Name != "Item") && !(propertyInfo.PropertyType != typeof(TValue)))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters != null && indexParameters.Length == 1 && !(indexParameters[0].ParameterType != typeof(TKey)))
					{
						MethodInfo setMethod = propertyInfo.GetSetMethod(true);
						if (setMethod != null)
						{
							return setMethod;
						}
					}
				}
			}
			throw new InvalidOperationException("Unable to resolve indexer for map");
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06007DA4 RID: 32164 RVA: 0x0024F4CC File Offset: 0x0024F4CC
		public override Type ExpectedType
		{
			get
			{
				return typeof(TDictionary);
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06007DA5 RID: 32165 RVA: 0x0024F4D8 File Offset: 0x0024F4D8
		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06007DA6 RID: 32166 RVA: 0x0024F4DC File Offset: 0x0024F4DC
		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06007DA7 RID: 32167 RVA: 0x0024F4E4 File Offset: 0x0024F4E4
		private bool AppendToCollection { get; }

		// Token: 0x06007DA8 RID: 32168 RVA: 0x0024F4EC File Offset: 0x0024F4EC
		public override object Read(object untyped, ProtoReader source)
		{
			TDictionary tdictionary = this.AppendToCollection ? ((TDictionary)((object)untyped)) : default(TDictionary);
			if (tdictionary == null)
			{
				tdictionary = (TDictionary)((object)Activator.CreateInstance(this.concreteType));
			}
			do
			{
				TKey key = MapDecorator<TDictionary, TKey, TValue>.DefaultKey;
				TValue tvalue = MapDecorator<TDictionary, TKey, TValue>.DefaultValue;
				SubItemToken token = ProtoReader.StartSubItem(source);
				int num;
				while ((num = source.ReadFieldHeader()) > 0)
				{
					if (num != 1)
					{
						if (num != 2)
						{
							source.SkipField();
						}
						else
						{
							tvalue = (TValue)((object)this.Tail.Read(this.Tail.RequiresOldValue ? tvalue : null, source));
						}
					}
					else
					{
						key = (TKey)((object)this.keyTail.Read(null, source));
					}
				}
				ProtoReader.EndSubItem(token, source);
				tdictionary[key] = tvalue;
			}
			while (source.TryReadFieldHeader(this.fieldNumber));
			return tdictionary;
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x0024F5F0 File Offset: 0x0024F5F0
		public override void Write(object untyped, ProtoWriter dest)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in ((TDictionary)((object)untyped)))
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, dest);
				SubItemToken token = ProtoWriter.StartSubItem(null, dest);
				if (keyValuePair.Key != null)
				{
					this.keyTail.Write(keyValuePair.Key, dest);
				}
				if (keyValuePair.Value != null)
				{
					this.Tail.Write(keyValuePair.Value, dest);
				}
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x0024F6BC File Offset: 0x0024F6BC
		protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
		{
			Type typeFromHandle = typeof(KeyValuePair<TKey, TValue>);
			MethodInfo method;
			MethodInfo methodInfo;
			MethodInfo enumeratorInfo = ListDecorator.GetEnumeratorInfo(ctx.Model, this.ExpectedType, typeFromHandle, out method, out methodInfo);
			Type returnType = enumeratorInfo.ReturnType;
			MethodInfo getMethod = typeFromHandle.GetProperty("Key").GetGetMethod();
			MethodInfo getMethod2 = typeFromHandle.GetProperty("Value").GetGetMethod();
			using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
			{
				using (Local local = new Local(ctx, returnType))
				{
					using (Local local2 = new Local(ctx, typeof(SubItemToken)))
					{
						using (Local local3 = new Local(ctx, typeFromHandle))
						{
							ctx.LoadAddress(localWithValue, this.ExpectedType, false);
							ctx.EmitCall(enumeratorInfo, this.ExpectedType);
							ctx.StoreValue(local);
							using (ctx.Using(local))
							{
								CodeLabel label = ctx.DefineLabel();
								CodeLabel label2 = ctx.DefineLabel();
								ctx.Branch(label2, false);
								ctx.MarkLabel(label);
								ctx.LoadAddress(local, returnType, false);
								ctx.EmitCall(methodInfo, returnType);
								if (typeFromHandle != ctx.MapType(typeof(object)) && methodInfo.ReturnType == ctx.MapType(typeof(object)))
								{
									ctx.CastFromObject(typeFromHandle);
								}
								ctx.StoreValue(local3);
								ctx.LoadValue(this.fieldNumber);
								ctx.LoadValue((int)this.wireType);
								ctx.LoadReaderWriter();
								ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("WriteFieldHeader"));
								ctx.LoadNullRef();
								ctx.LoadReaderWriter();
								ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("StartSubItem"));
								ctx.StoreValue(local2);
								ctx.LoadAddress(local3, typeFromHandle, false);
								ctx.EmitCall(getMethod, typeFromHandle);
								ctx.WriteNullCheckedTail(typeof(TKey), this.keyTail, null);
								ctx.LoadAddress(local3, typeFromHandle, false);
								ctx.EmitCall(getMethod2, typeFromHandle);
								ctx.WriteNullCheckedTail(typeof(TValue), this.Tail, null);
								ctx.LoadValue(local2);
								ctx.LoadReaderWriter();
								ctx.EmitCall(ctx.MapType(typeof(ProtoWriter)).GetMethod("EndSubItem"));
								ctx.MarkLabel(label2);
								ctx.LoadAddress(local, returnType, false);
								ctx.EmitCall(method, returnType);
								ctx.BranchIfTrue(label, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x06007DAB RID: 32171 RVA: 0x0024F9E0 File Offset: 0x0024F9E0
		protected override void EmitRead(CompilerContext ctx, Local valueFrom)
		{
			using (Local local = this.AppendToCollection ? ctx.GetLocalWithValue(this.ExpectedType, valueFrom) : new Local(ctx, typeof(TDictionary)))
			{
				using (Local local2 = new Local(ctx, typeof(SubItemToken)))
				{
					using (Local local3 = new Local(ctx, typeof(TKey)))
					{
						using (Local local4 = new Local(ctx, typeof(TValue)))
						{
							using (Local local5 = new Local(ctx, ctx.MapType(typeof(int))))
							{
								if (!this.AppendToCollection)
								{
									ctx.LoadNullRef();
									ctx.StoreValue(local);
								}
								if (this.concreteType != null)
								{
									ctx.LoadValue(local);
									CodeLabel label = ctx.DefineLabel();
									ctx.BranchIfTrue(label, true);
									ctx.EmitCtor(this.concreteType);
									ctx.StoreValue(local);
									ctx.MarkLabel(label);
								}
								CodeLabel label2 = ctx.DefineLabel();
								ctx.MarkLabel(label2);
								if (typeof(TKey) == typeof(string))
								{
									ctx.LoadValue("");
									ctx.StoreValue(local3);
								}
								else
								{
									ctx.InitLocal(typeof(TKey), local3);
								}
								if (typeof(TValue) == typeof(string))
								{
									ctx.LoadValue("");
									ctx.StoreValue(local4);
								}
								else
								{
									ctx.InitLocal(typeof(TValue), local4);
								}
								ctx.LoadReaderWriter();
								ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("StartSubItem"));
								ctx.StoreValue(local2);
								CodeLabel label3 = ctx.DefineLabel();
								CodeLabel label4 = ctx.DefineLabel();
								ctx.Branch(label3, false);
								ctx.MarkLabel(label4);
								ctx.LoadValue(local5);
								CodeLabel codeLabel = ctx.DefineLabel();
								CodeLabel codeLabel2 = ctx.DefineLabel();
								CodeLabel codeLabel3 = ctx.DefineLabel();
								ctx.Switch(new CodeLabel[]
								{
									codeLabel,
									codeLabel2,
									codeLabel3
								});
								ctx.MarkLabel(codeLabel);
								ctx.LoadReaderWriter();
								ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("SkipField"));
								ctx.Branch(label3, false);
								ctx.MarkLabel(codeLabel2);
								this.keyTail.EmitRead(ctx, null);
								ctx.StoreValue(local3);
								ctx.Branch(label3, false);
								ctx.MarkLabel(codeLabel3);
								this.Tail.EmitRead(ctx, this.Tail.RequiresOldValue ? local4 : null);
								ctx.StoreValue(local4);
								ctx.MarkLabel(label3);
								ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof(int)));
								ctx.CopyValue();
								ctx.StoreValue(local5);
								ctx.LoadValue(0);
								ctx.BranchIfGreater(label4, false);
								ctx.LoadValue(local2);
								ctx.LoadReaderWriter();
								ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("EndSubItem"));
								ctx.LoadAddress(local, this.ExpectedType, false);
								ctx.LoadValue(local3);
								ctx.LoadValue(local4);
								ctx.EmitCall(MapDecorator<TDictionary, TKey, TValue>.indexerSet);
								ctx.LoadReaderWriter();
								ctx.LoadValue(this.fieldNumber);
								ctx.EmitCall(ctx.MapType(typeof(ProtoReader)).GetMethod("TryReadFieldHeader"));
								ctx.BranchIfTrue(label2, false);
								if (this.ReturnsValue)
								{
									ctx.LoadValue(local);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04003C65 RID: 15461
		private readonly Type concreteType;

		// Token: 0x04003C66 RID: 15462
		private readonly IProtoSerializer keyTail;

		// Token: 0x04003C67 RID: 15463
		private readonly int fieldNumber;

		// Token: 0x04003C68 RID: 15464
		private readonly WireType wireType;

		// Token: 0x04003C69 RID: 15465
		private static readonly MethodInfo indexerSet = MapDecorator<TDictionary, TKey, TValue>.GetIndexerSetter();

		// Token: 0x04003C6A RID: 15466
		private static readonly TKey DefaultKey = (typeof(TKey) == typeof(string)) ? ((TKey)((object)"")) : default(TKey);

		// Token: 0x04003C6B RID: 15467
		private static readonly TValue DefaultValue = (typeof(TValue) == typeof(string)) ? ((TValue)((object)"")) : default(TValue);
	}
}
