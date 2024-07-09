using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000B70 RID: 2928
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonWriter : JsonWriter
	{
		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x060075C9 RID: 30153 RVA: 0x00235E78 File Offset: 0x00235E78
		// (set) Token: 0x060075CA RID: 30154 RVA: 0x00235E88 File Offset: 0x00235E88
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x00235E98 File Offset: 0x00235E98
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x00235EBC File Offset: 0x00235EBC
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x00235EDC File Offset: 0x00235EDC
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x00235EEC File Offset: 0x00235EEC
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x060075CF RID: 30159 RVA: 0x00235F18 File Offset: 0x00235F18
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x00235F28 File Offset: 0x00235F28
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x00235F38 File Offset: 0x00235F38
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x060075D2 RID: 30162 RVA: 0x00235F48 File Offset: 0x00235F48
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x00235F58 File Offset: 0x00235F58
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x00235F6C File Offset: 0x00235F6C
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x00235F80 File Offset: 0x00235F80
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x00235F90 File Offset: 0x00235F90
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				BsonBinaryWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x00235FB8 File Offset: 0x00235FB8
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x00235FC8 File Offset: 0x00235FC8
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x00235FDC File Offset: 0x00235FDC
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x060075DA RID: 30170 RVA: 0x00235FEC File Offset: 0x00235FEC
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				BsonObject bsonObject = this._parent as BsonObject;
				if (bsonObject != null)
				{
					bsonObject.Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw JsonWriterException.Create(this, "Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, token.Type), null);
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x060075DB RID: 30171 RVA: 0x0023608C File Offset: 0x0023608C
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				base.SetWriteState(JsonToken.Integer, null);
				this.AddToken(new BsonBinary(bigInteger.ToByteArray(), BsonBinaryType.Binary));
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x002360D4 File Offset: 0x002360D4
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddToken(BsonEmpty.Null);
		}

		// Token: 0x060075DD RID: 30173 RVA: 0x002360E8 File Offset: 0x002360E8
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddToken(BsonEmpty.Undefined);
		}

		// Token: 0x060075DE RID: 30174 RVA: 0x002360FC File Offset: 0x002360FC
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddToken((value == null) ? BsonEmpty.Null : new BsonString(value, true));
		}

		// Token: 0x060075DF RID: 30175 RVA: 0x00236124 File Offset: 0x00236124
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x0023613C File Offset: 0x0023613C
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > 2147483647U)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x0023616C File Offset: 0x0023616C
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x060075E2 RID: 30178 RVA: 0x00236184 File Offset: 0x00236184
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x060075E3 RID: 30179 RVA: 0x002361B8 File Offset: 0x002361B8
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x002361D0 File Offset: 0x002361D0
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x002361E8 File Offset: 0x002361E8
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddToken(value ? BsonBoolean.True : BsonBoolean.False);
		}

		// Token: 0x060075E6 RID: 30182 RVA: 0x0023620C File Offset: 0x0023620C
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060075E7 RID: 30183 RVA: 0x00236224 File Offset: 0x00236224
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060075E8 RID: 30184 RVA: 0x0023623C File Offset: 0x0023623C
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString(CultureInfo.InvariantCulture);
			this.AddToken(new BsonString(value2, true));
		}

		// Token: 0x060075E9 RID: 30185 RVA: 0x00236270 File Offset: 0x00236270
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x00236288 File Offset: 0x00236288
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x002362A0 File Offset: 0x002362A0
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x002362B8 File Offset: 0x002362B8
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x060075ED RID: 30189 RVA: 0x002362E0 File Offset: 0x002362E0
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x060075EE RID: 30190 RVA: 0x002362F8 File Offset: 0x002362F8
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		// Token: 0x060075EF RID: 30191 RVA: 0x0023631C File Offset: 0x0023631C
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		// Token: 0x060075F0 RID: 30192 RVA: 0x00236338 File Offset: 0x00236338
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x0023635C File Offset: 0x0023635C
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x0023639C File Offset: 0x0023639C
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x060075F3 RID: 30195 RVA: 0x002363D4 File Offset: 0x002363D4
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x04003940 RID: 14656
		private readonly BsonBinaryWriter _writer;

		// Token: 0x04003941 RID: 14657
		private BsonToken _root;

		// Token: 0x04003942 RID: 14658
		private BsonToken _parent;

		// Token: 0x04003943 RID: 14659
		private string _propertyName;
	}
}
