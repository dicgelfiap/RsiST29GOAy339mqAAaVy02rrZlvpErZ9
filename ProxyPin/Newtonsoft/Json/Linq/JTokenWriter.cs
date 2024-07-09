using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B27 RID: 2855
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06007390 RID: 29584 RVA: 0x0022BB90 File Offset: 0x0022BB90
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06007391 RID: 29585 RVA: 0x0022BBB8 File Offset: 0x0022BBB8
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06007392 RID: 29586 RVA: 0x0022BBC0 File Offset: 0x0022BBC0
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x0022BBDC File Offset: 0x0022BBDC
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06007394 RID: 29588 RVA: 0x0022BC00 File Offset: 0x0022BC00
		public JTokenWriter()
		{
		}

		// Token: 0x06007395 RID: 29589 RVA: 0x0022BC08 File Offset: 0x0022BC08
		public override void Flush()
		{
		}

		// Token: 0x06007396 RID: 29590 RVA: 0x0022BC0C File Offset: 0x0022BC0C
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06007397 RID: 29591 RVA: 0x0022BC14 File Offset: 0x0022BC14
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x0022BC28 File Offset: 0x0022BC28
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x0022BC5C File Offset: 0x0022BC5C
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x0022BCB8 File Offset: 0x0022BCB8
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x0600739B RID: 29595 RVA: 0x0022BCCC File Offset: 0x0022BCCC
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x0600739C RID: 29596 RVA: 0x0022BCE4 File Offset: 0x0022BCE4
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x0022BCEC File Offset: 0x0022BCEC
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x0022BD20 File Offset: 0x0022BD20
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x0022BD30 File Offset: 0x0022BD30
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				this._parent.Add(value);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					return;
				}
			}
			else
			{
				this._value = (value ?? JValue.CreateNull());
				this._current = this._value;
			}
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x0022BDAC File Offset: 0x0022BDAC
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x0022BDD0 File Offset: 0x0022BDD0
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x060073A2 RID: 29602 RVA: 0x0022BDE4 File Offset: 0x0022BDE4
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x060073A3 RID: 29603 RVA: 0x0022BDF8 File Offset: 0x0022BDF8
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x060073A4 RID: 29604 RVA: 0x0022BE10 File Offset: 0x0022BE10
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x0022BE28 File Offset: 0x0022BE28
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060073A6 RID: 29606 RVA: 0x0022BE3C File Offset: 0x0022BE3C
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073A7 RID: 29607 RVA: 0x0022BE54 File Offset: 0x0022BE54
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x0022BE6C File Offset: 0x0022BE6C
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x0022BE84 File Offset: 0x0022BE84
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073AA RID: 29610 RVA: 0x0022BE9C File Offset: 0x0022BE9C
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x060073AB RID: 29611 RVA: 0x0022BEB4 File Offset: 0x0022BEB4
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x0022BECC File Offset: 0x0022BECC
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x060073AD RID: 29613 RVA: 0x0022BEE4 File Offset: 0x0022BEE4
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x0022BEFC File Offset: 0x0022BEFC
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073AF RID: 29615 RVA: 0x0022BF14 File Offset: 0x0022BF14
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString(CultureInfo.InvariantCulture);
			this.AddValue(value2, JsonToken.String);
		}

		// Token: 0x060073B0 RID: 29616 RVA: 0x0022BF44 File Offset: 0x0022BF44
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073B1 RID: 29617 RVA: 0x0022BF5C File Offset: 0x0022BF5C
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x060073B2 RID: 29618 RVA: 0x0022BF74 File Offset: 0x0022BF74
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x060073B3 RID: 29619 RVA: 0x0022BF8C File Offset: 0x0022BF8C
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x0022BFB4 File Offset: 0x0022BFB4
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x060073B5 RID: 29621 RVA: 0x0022BFCC File Offset: 0x0022BFCC
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x060073B6 RID: 29622 RVA: 0x0022BFE0 File Offset: 0x0022BFE0
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x0022BFF8 File Offset: 0x0022BFF8
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x0022C010 File Offset: 0x0022C010
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x060073B9 RID: 29625 RVA: 0x0022C024 File Offset: 0x0022C024
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken();
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = (jtoken as JContainer);
					this._value = (jtoken as JValue);
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x0400389A RID: 14490
		private JContainer _token;

		// Token: 0x0400389B RID: 14491
		private JContainer _parent;

		// Token: 0x0400389C RID: 14492
		private JValue _value;

		// Token: 0x0400389D RID: 14493
		private JToken _current;
	}
}
