using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ProtoBuf.Serializers;

namespace ProtoBuf.Meta
{
	// Token: 0x02000C79 RID: 3193
	[ComVisible(true)]
	public sealed class SubType
	{
		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x06007F6E RID: 32622 RVA: 0x0025AB8C File Offset: 0x0025AB8C
		// (set) Token: 0x06007F6F RID: 32623 RVA: 0x0025AB94 File Offset: 0x0025AB94
		public int FieldNumber
		{
			get
			{
				return this._fieldNumber;
			}
			internal set
			{
				if (this._fieldNumber != value)
				{
					MetaType.AssertValidFieldNumber(value);
					this.ThrowIfFrozen();
					this._fieldNumber = value;
				}
			}
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x0025ABB8 File Offset: 0x0025ABB8
		private void ThrowIfFrozen()
		{
			if (this.serializer != null)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated");
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x06007F71 RID: 32625 RVA: 0x0025ABD0 File Offset: 0x0025ABD0
		public MetaType DerivedType
		{
			get
			{
				return this.derivedType;
			}
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x0025ABD8 File Offset: 0x0025ABD8
		public SubType(int fieldNumber, MetaType derivedType, DataFormat format)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			this._fieldNumber = fieldNumber;
			this.derivedType = derivedType;
			this.dataFormat = format;
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x0025AC18 File Offset: 0x0025AC18
		internal IProtoSerializer Serializer
		{
			get
			{
				IProtoSerializer result;
				if ((result = this.serializer) == null)
				{
					result = (this.serializer = this.BuildSerializer());
				}
				return result;
			}
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x0025AC48 File Offset: 0x0025AC48
		private IProtoSerializer BuildSerializer()
		{
			WireType wireType = WireType.String;
			if (this.dataFormat == DataFormat.Group)
			{
				wireType = WireType.StartGroup;
			}
			IProtoSerializer tail = new SubItemSerializer(this.derivedType.Type, this.derivedType.GetKey(false, false), this.derivedType, false);
			return new TagDecorator(this._fieldNumber, wireType, false, tail);
		}

		// Token: 0x04003CD9 RID: 15577
		private int _fieldNumber;

		// Token: 0x04003CDA RID: 15578
		private readonly MetaType derivedType;

		// Token: 0x04003CDB RID: 15579
		private readonly DataFormat dataFormat;

		// Token: 0x04003CDC RID: 15580
		private IProtoSerializer serializer;

		// Token: 0x02001182 RID: 4482
		internal sealed class Comparer : IComparer, IComparer<SubType>
		{
			// Token: 0x06009394 RID: 37780 RVA: 0x002C35AC File Offset: 0x002C35AC
			public int Compare(object x, object y)
			{
				return this.Compare(x as SubType, y as SubType);
			}

			// Token: 0x06009395 RID: 37781 RVA: 0x002C35C0 File Offset: 0x002C35C0
			public int Compare(SubType x, SubType y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}

			// Token: 0x04004B7B RID: 19323
			public static readonly SubType.Comparer Default = new SubType.Comparer();
		}
	}
}
