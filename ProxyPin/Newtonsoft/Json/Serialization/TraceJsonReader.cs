using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000B02 RID: 2818
	[NullableContext(1)]
	[Nullable(0)]
	internal class TraceJsonReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x06007064 RID: 28772 RVA: 0x00220F54 File Offset: 0x00220F54
		public TraceJsonReader(JsonReader innerReader)
		{
			this._innerReader = innerReader;
			this._sw = new StringWriter(CultureInfo.InvariantCulture);
			this._sw.Write("Deserialized JSON: " + Environment.NewLine);
			this._textWriter = new JsonTextWriter(this._sw);
			this._textWriter.Formatting = Formatting.Indented;
		}

		// Token: 0x06007065 RID: 28773 RVA: 0x00220FBC File Offset: 0x00220FBC
		public string GetDeserializedJsonMessage()
		{
			return this._sw.ToString();
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x00220FCC File Offset: 0x00220FCC
		public override bool Read()
		{
			bool result = this._innerReader.Read();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06007067 RID: 28775 RVA: 0x00220FE0 File Offset: 0x00220FE0
		public override int? ReadAsInt32()
		{
			int? result = this._innerReader.ReadAsInt32();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x00220FF4 File Offset: 0x00220FF4
		[NullableContext(2)]
		public override string ReadAsString()
		{
			string result = this._innerReader.ReadAsString();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x00221008 File Offset: 0x00221008
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			byte[] result = this._innerReader.ReadAsBytes();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x0022101C File Offset: 0x0022101C
		public override decimal? ReadAsDecimal()
		{
			decimal? result = this._innerReader.ReadAsDecimal();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x0600706B RID: 28779 RVA: 0x00221030 File Offset: 0x00221030
		public override double? ReadAsDouble()
		{
			double? result = this._innerReader.ReadAsDouble();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x0600706C RID: 28780 RVA: 0x00221044 File Offset: 0x00221044
		public override bool? ReadAsBoolean()
		{
			bool? result = this._innerReader.ReadAsBoolean();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x0600706D RID: 28781 RVA: 0x00221058 File Offset: 0x00221058
		public override DateTime? ReadAsDateTime()
		{
			DateTime? result = this._innerReader.ReadAsDateTime();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x0600706E RID: 28782 RVA: 0x0022106C File Offset: 0x0022106C
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			DateTimeOffset? result = this._innerReader.ReadAsDateTimeOffset();
			this.WriteCurrentToken();
			return result;
		}

		// Token: 0x0600706F RID: 28783 RVA: 0x00221080 File Offset: 0x00221080
		public void WriteCurrentToken()
		{
			this._textWriter.WriteToken(this._innerReader, false, false, true);
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06007070 RID: 28784 RVA: 0x00221098 File Offset: 0x00221098
		public override int Depth
		{
			get
			{
				return this._innerReader.Depth;
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06007071 RID: 28785 RVA: 0x002210A8 File Offset: 0x002210A8
		public override string Path
		{
			get
			{
				return this._innerReader.Path;
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06007072 RID: 28786 RVA: 0x002210B8 File Offset: 0x002210B8
		// (set) Token: 0x06007073 RID: 28787 RVA: 0x002210C8 File Offset: 0x002210C8
		public override char QuoteChar
		{
			get
			{
				return this._innerReader.QuoteChar;
			}
			protected internal set
			{
				this._innerReader.QuoteChar = value;
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06007074 RID: 28788 RVA: 0x002210D8 File Offset: 0x002210D8
		public override JsonToken TokenType
		{
			get
			{
				return this._innerReader.TokenType;
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06007075 RID: 28789 RVA: 0x002210E8 File Offset: 0x002210E8
		[Nullable(2)]
		public override object Value
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.Value;
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06007076 RID: 28790 RVA: 0x002210F8 File Offset: 0x002210F8
		[Nullable(2)]
		public override Type ValueType
		{
			[NullableContext(2)]
			get
			{
				return this._innerReader.ValueType;
			}
		}

		// Token: 0x06007077 RID: 28791 RVA: 0x00221108 File Offset: 0x00221108
		public override void Close()
		{
			this._innerReader.Close();
		}

		// Token: 0x06007078 RID: 28792 RVA: 0x00221118 File Offset: 0x00221118
		bool IJsonLineInfo.HasLineInfo()
		{
			IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06007079 RID: 28793 RVA: 0x00221144 File Offset: 0x00221144
		int IJsonLineInfo.LineNumber
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LineNumber;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x0600707A RID: 28794 RVA: 0x00221170 File Offset: 0x00221170
		int IJsonLineInfo.LinePosition
		{
			get
			{
				IJsonLineInfo jsonLineInfo = this._innerReader as IJsonLineInfo;
				if (jsonLineInfo == null)
				{
					return 0;
				}
				return jsonLineInfo.LinePosition;
			}
		}

		// Token: 0x040037CA RID: 14282
		private readonly JsonReader _innerReader;

		// Token: 0x040037CB RID: 14283
		private readonly JsonTextWriter _textWriter;

		// Token: 0x040037CC RID: 14284
		private readonly StringWriter _sw;
	}
}
