using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000A86 RID: 2694
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonReader : IDisposable
	{
		// Token: 0x060068FA RID: 26874 RVA: 0x001FC82C File Offset: 0x001FC82C
		[NullableContext(1)]
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool>() ?? this.Read().ToAsync();
		}

		// Token: 0x060068FB RID: 26875 RVA: 0x001FC848 File Offset: 0x001FC848
		[NullableContext(1)]
		public async Task SkipAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				await this.ReadAsync(cancellationToken).ConfigureAwait(false);
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
				do
				{
					configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					}
				}
				while (configuredTaskAwaiter.GetResult() && depth < this.Depth);
			}
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x001FC89C File Offset: 0x001FC89C
		[NullableContext(1)]
		internal async Task ReaderReadAndAssertAsync(CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x001FC8F0 File Offset: 0x001FC8F0
		[NullableContext(1)]
		public virtual Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool?>() ?? Task.FromResult<bool?>(this.ReadAsBoolean());
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x001FC90C File Offset: 0x001FC90C
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<byte[]>() ?? Task.FromResult<byte[]>(this.ReadAsBytes());
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x001FC928 File Offset: 0x001FC928
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		internal async Task<byte[]> ReadArrayIntoByteArrayAsync(CancellationToken cancellationToken)
		{
			List<byte> buffer = new List<byte>();
			do
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(buffer));
			byte[] array = buffer.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		// Token: 0x06006900 RID: 26880 RVA: 0x001FC97C File Offset: 0x001FC97C
		[NullableContext(1)]
		public virtual Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTime?>() ?? Task.FromResult<DateTime?>(this.ReadAsDateTime());
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x001FC998 File Offset: 0x001FC998
		[NullableContext(1)]
		public virtual Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTimeOffset?>() ?? Task.FromResult<DateTimeOffset?>(this.ReadAsDateTimeOffset());
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x001FC9B4 File Offset: 0x001FC9B4
		[NullableContext(1)]
		public virtual Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<decimal?>() ?? Task.FromResult<decimal?>(this.ReadAsDecimal());
		}

		// Token: 0x06006903 RID: 26883 RVA: 0x001FC9D0 File Offset: 0x001FC9D0
		[NullableContext(1)]
		public virtual Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult<double?>(this.ReadAsDouble());
		}

		// Token: 0x06006904 RID: 26884 RVA: 0x001FC9E0 File Offset: 0x001FC9E0
		[NullableContext(1)]
		public virtual Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<int?>() ?? Task.FromResult<int?>(this.ReadAsInt32());
		}

		// Token: 0x06006905 RID: 26885 RVA: 0x001FC9FC File Offset: 0x001FC9FC
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<string>() ?? Task.FromResult<string>(this.ReadAsString());
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x001FCA18 File Offset: 0x001FCA18
		[NullableContext(1)]
		internal async Task<bool> ReadAndMoveToContentAsync(CancellationToken cancellationToken)
		{
			bool flag = await this.ReadAsync(cancellationToken).ConfigureAwait(false);
			if (flag)
			{
				flag = await this.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			}
			return flag;
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x001FCA6C File Offset: 0x001FCA6C
		[NullableContext(1)]
		internal Task<bool> MoveToContentAsync(CancellationToken cancellationToken)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				return this.MoveToContentFromNonContentAsync(cancellationToken);
			}
			return AsyncUtils.True;
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x001FCAA0 File Offset: 0x001FCAA0
		[NullableContext(1)]
		private async Task<bool> MoveToContentFromNonContentAsync(CancellationToken cancellationToken)
		{
			for (;;)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					break;
				}
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.None && tokenType != JsonToken.Comment)
				{
					goto Block_3;
				}
			}
			return false;
			Block_3:
			return true;
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06006909 RID: 26889 RVA: 0x001FCAF4 File Offset: 0x001FCAF4
		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x0600690A RID: 26890 RVA: 0x001FCAFC File Offset: 0x001FCAFC
		// (set) Token: 0x0600690B RID: 26891 RVA: 0x001FCB04 File Offset: 0x001FCB04
		public bool CloseInput { get; set; }

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x0600690C RID: 26892 RVA: 0x001FCB10 File Offset: 0x001FCB10
		// (set) Token: 0x0600690D RID: 26893 RVA: 0x001FCB18 File Offset: 0x001FCB18
		public bool SupportMultipleContent { get; set; }

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x0600690E RID: 26894 RVA: 0x001FCB24 File Offset: 0x001FCB24
		// (set) Token: 0x0600690F RID: 26895 RVA: 0x001FCB2C File Offset: 0x001FCB2C
		public virtual char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			protected internal set
			{
				this._quoteChar = value;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06006910 RID: 26896 RVA: 0x001FCB38 File Offset: 0x001FCB38
		// (set) Token: 0x06006911 RID: 26897 RVA: 0x001FCB40 File Offset: 0x001FCB40
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06006912 RID: 26898 RVA: 0x001FCB64 File Offset: 0x001FCB64
		// (set) Token: 0x06006913 RID: 26899 RVA: 0x001FCB6C File Offset: 0x001FCB6C
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling;
			}
			set
			{
				if (value < DateParseHandling.None || value > DateParseHandling.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateParseHandling = value;
			}
		}

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06006914 RID: 26900 RVA: 0x001FCB90 File Offset: 0x001FCB90
		// (set) Token: 0x06006915 RID: 26901 RVA: 0x001FCB98 File Offset: 0x001FCB98
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling;
			}
			set
			{
				if (value < FloatParseHandling.Double || value > FloatParseHandling.Decimal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatParseHandling = value;
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06006916 RID: 26902 RVA: 0x001FCBBC File Offset: 0x001FCBBC
		// (set) Token: 0x06006917 RID: 26903 RVA: 0x001FCBC4 File Offset: 0x001FCBC4
		public string DateFormatString
		{
			get
			{
				return this._dateFormatString;
			}
			set
			{
				this._dateFormatString = value;
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06006918 RID: 26904 RVA: 0x001FCBD0 File Offset: 0x001FCBD0
		// (set) Token: 0x06006919 RID: 26905 RVA: 0x001FCBD8 File Offset: 0x001FCBD8
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if (num.GetValueOrDefault() <= num2 & num != null)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x0600691A RID: 26906 RVA: 0x001FCC20 File Offset: 0x001FCC20
		public virtual JsonToken TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x0600691B RID: 26907 RVA: 0x001FCC28 File Offset: 0x001FCC28
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x0600691C RID: 26908 RVA: 0x001FCC30 File Offset: 0x001FCC30
		public virtual Type ValueType
		{
			get
			{
				object value = this._value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x0600691D RID: 26909 RVA: 0x001FCC48 File Offset: 0x001FCC48
		public virtual int Depth
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (JsonTokenUtils.IsStartToken(this.TokenType) || this._currentPosition.Type == JsonContainerType.None)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x0600691E RID: 26910 RVA: 0x001FCC98 File Offset: 0x001FCC98
		[Nullable(1)]
		public virtual string Path
		{
			[NullableContext(1)]
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonReader.State.ArrayStart && this._currentState != JsonReader.State.ConstructorStart && this._currentState != JsonReader.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : null;
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x0600691F RID: 26911 RVA: 0x001FCD18 File Offset: 0x001FCD18
		// (set) Token: 0x06006920 RID: 26912 RVA: 0x001FCD2C File Offset: 0x001FCD2C
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06006921 RID: 26913 RVA: 0x001FCD38 File Offset: 0x001FCD38
		internal JsonPosition GetPosition(int depth)
		{
			if (this._stack != null && depth < this._stack.Count)
			{
				return this._stack[depth];
			}
			return this._currentPosition;
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x001FCD6C File Offset: 0x001FCD6C
		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this._dateParseHandling = DateParseHandling.DateTime;
			this._floatParseHandling = FloatParseHandling.Double;
			this.CloseInput = true;
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x001FCD98 File Offset: 0x001FCD98
		private void Push(JsonContainerType value)
		{
			this.UpdateScopeWithFinishedValue();
			if (this._currentPosition.Type == JsonContainerType.None)
			{
				this._currentPosition = new JsonPosition(value);
				return;
			}
			if (this._stack == null)
			{
				this._stack = new List<JsonPosition>();
			}
			this._stack.Add(this._currentPosition);
			this._currentPosition = new JsonPosition(value);
			if (this._maxDepth != null)
			{
				int num = this.Depth + 1;
				int? maxDepth = this._maxDepth;
				if ((num > maxDepth.GetValueOrDefault() & maxDepth != null) && !this._hasExceededMaxDepth)
				{
					this._hasExceededMaxDepth = true;
					throw JsonReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, this._maxDepth));
				}
			}
		}

		// Token: 0x06006924 RID: 26916 RVA: 0x001FCE68 File Offset: 0x001FCE68
		private JsonContainerType Pop()
		{
			JsonPosition currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				currentPosition = this._currentPosition;
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				currentPosition = this._currentPosition;
				this._currentPosition = default(JsonPosition);
			}
			if (this._maxDepth != null)
			{
				int depth = this.Depth;
				int? maxDepth = this._maxDepth;
				if (depth <= maxDepth.GetValueOrDefault() & maxDepth != null)
				{
					this._hasExceededMaxDepth = false;
				}
			}
			return currentPosition.Type;
		}

		// Token: 0x06006925 RID: 26917 RVA: 0x001FCF30 File Offset: 0x001FCF30
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x06006926 RID: 26918
		public abstract bool Read();

		// Token: 0x06006927 RID: 26919 RVA: 0x001FCF40 File Offset: 0x001FCF40
		public virtual int? ReadAsInt32()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					int num;
					if (value is int)
					{
						num = (int)value;
						return new int?(num);
					}
					if (value is BigInteger)
					{
						BigInteger value2 = (BigInteger)value;
						num = (int)value2;
					}
					else
					{
						try
						{
							num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							throw JsonReaderException.Create(this, "Could not convert to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
						}
					}
					this.SetToken(JsonToken.Integer, num, false);
					return new int?(num);
				}
				case JsonToken.String:
				{
					string s = (string)this.Value;
					return this.ReadInt32String(s);
				}
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x001FD054 File Offset: 0x001FD054
		internal int? ReadInt32String(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			int num;
			if (int.TryParse(s, NumberStyles.Integer, this.Culture, out num))
			{
				this.SetToken(JsonToken.Integer, num, false);
				return new int?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x001FD0D0 File Offset: 0x001FD0D0
		public virtual string ReadAsString()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_40;
					}
					return (string)this.Value;
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				goto IL_40;
			}
			return null;
			IL_40:
			if (JsonTokenUtils.IsPrimitiveToken(contentToken))
			{
				object value = this.Value;
				if (value != null)
				{
					IFormattable formattable = value as IFormattable;
					string text;
					if (formattable != null)
					{
						text = formattable.ToString(null, this.Culture);
					}
					else
					{
						Uri uri = value as Uri;
						text = ((uri != null) ? uri.OriginalString : value.ToString());
					}
					this.SetToken(JsonToken.String, text, false);
					return text;
				}
			}
			throw JsonReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x001FD1A4 File Offset: 0x001FD1A4
		public virtual byte[] ReadAsBytes()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				switch (contentToken)
				{
				case JsonToken.None:
					break;
				case JsonToken.StartObject:
				{
					this.ReadIntoWrappedTypeObject();
					byte[] array = this.ReadAsBytes();
					this.ReaderReadAndAssert();
					if (this.TokenType != JsonToken.EndObject)
					{
						throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
					this.SetToken(JsonToken.Bytes, array, false);
					return array;
				}
				case JsonToken.StartArray:
					return this.ReadArrayIntoByteArray();
				default:
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_130;
					}
					string text = (string)this.Value;
					byte[] array2;
					Guid guid;
					if (text.Length == 0)
					{
						array2 = CollectionUtils.ArrayEmpty<byte>();
					}
					else if (ConvertUtils.TryConvertGuid(text, out guid))
					{
						array2 = guid.ToByteArray();
					}
					else
					{
						array2 = Convert.FromBase64String(text);
					}
					this.SetToken(JsonToken.Bytes, array2, false);
					return array2;
				}
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Bytes)
				{
					goto IL_130;
				}
				object value = this.Value;
				if (value is Guid)
				{
					byte[] array3 = ((Guid)value).ToByteArray();
					this.SetToken(JsonToken.Bytes, array3, false);
					return array3;
				}
				return (byte[])this.Value;
			}
			return null;
			IL_130:
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x001FD300 File Offset: 0x001FD300
		[NullableContext(1)]
		internal byte[] ReadArrayIntoByteArray()
		{
			List<byte> list = new List<byte>();
			do
			{
				if (!this.Read())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x001FD344 File Offset: 0x001FD344
		[NullableContext(1)]
		private bool ReadArrayElementIntoByteArrayReportDone(List<byte> buffer)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType <= JsonToken.Comment)
			{
				if (tokenType == JsonToken.None)
				{
					throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
				}
				if (tokenType == JsonToken.Comment)
				{
					return false;
				}
			}
			else
			{
				if (tokenType == JsonToken.Integer)
				{
					buffer.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
					return false;
				}
				if (tokenType == JsonToken.EndArray)
				{
					return true;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x001FD3D4 File Offset: 0x001FD3D4
		public virtual double? ReadAsDouble()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					double num;
					if (value is double)
					{
						num = (double)value;
						return new double?(num);
					}
					if (value is BigInteger)
					{
						BigInteger value2 = (BigInteger)value;
						num = (double)value2;
					}
					else
					{
						num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
					}
					this.SetToken(JsonToken.Float, num, false);
					return new double?(num);
				}
				case JsonToken.String:
					return this.ReadDoubleString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading double. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x001FD4BC File Offset: 0x001FD4BC
		internal double? ReadDoubleString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			double num;
			if (double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, this.Culture, out num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new double?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to double: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x001FD53C File Offset: 0x001FD53C
		public virtual bool? ReadAsBoolean()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					bool flag;
					if (value is BigInteger)
					{
						BigInteger left = (BigInteger)value;
						flag = (left != 0L);
					}
					else
					{
						flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
					}
					this.SetToken(JsonToken.Boolean, flag, false);
					return new bool?(flag);
				}
				case JsonToken.String:
					return this.ReadBooleanString((string)this.Value);
				case JsonToken.Boolean:
					return new bool?((bool)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading boolean. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x001FD624 File Offset: 0x001FD624
		internal bool? ReadBooleanString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			bool flag;
			if (bool.TryParse(s, out flag))
			{
				this.SetToken(JsonToken.Boolean, flag, false);
				return new bool?(flag);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to boolean: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x001FD698 File Offset: 0x001FD698
		public virtual decimal? ReadAsDecimal()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					decimal num;
					if (value is decimal)
					{
						num = (decimal)value;
						return new decimal?(num);
					}
					if (value is BigInteger)
					{
						BigInteger value2 = (BigInteger)value;
						num = (decimal)value2;
					}
					else
					{
						try
						{
							num = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							throw JsonReaderException.Create(this, "Could not convert to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
						}
					}
					this.SetToken(JsonToken.Float, num, false);
					return new decimal?(num);
				}
				case JsonToken.String:
					return this.ReadDecimalString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x001FD7A8 File Offset: 0x001FD7A8
		internal decimal? ReadDecimalString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			decimal num;
			if (decimal.TryParse(s, NumberStyles.Number, this.Culture, out num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			if (ConvertUtils.DecimalTryParse(s.ToCharArray(), 0, s.Length, out num) == ParseResult.Success)
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x001FD854 File Offset: 0x001FD854
		public virtual DateTime? ReadAsDateTime()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_9A;
					}
					return this.ReadDateTimeString((string)this.Value);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_9A;
				}
				object value = this.Value;
				if (value is DateTimeOffset)
				{
					this.SetToken(JsonToken.Date, ((DateTimeOffset)value).DateTime, false);
				}
				return new DateTime?((DateTime)this.Value);
			}
			return null;
			IL_9A:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x001FD920 File Offset: 0x001FD920
		internal DateTime? ReadDateTimeString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTime dateTime;
			if (DateTimeUtils.TryParseDateTime(s, this.DateTimeZoneHandling, this._dateFormatString, this.Culture, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			if (DateTime.TryParse(s, this.Culture, DateTimeStyles.RoundtripKind, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			throw JsonReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x001FD9E4 File Offset: 0x001FD9E4
		public virtual DateTimeOffset? ReadAsDateTimeOffset()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_9D;
					}
					string s = (string)this.Value;
					return this.ReadDateTimeOffsetString(s);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_9D;
				}
				object value = this.Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					this.SetToken(JsonToken.Date, new DateTimeOffset(dateTime), false);
				}
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			return null;
			IL_9D:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x001FDAB0 File Offset: 0x001FDAB0
		internal DateTimeOffset? ReadDateTimeOffsetString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeUtils.TryParseDateTimeOffset(s, this._dateFormatString, this.Culture, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			if (DateTimeOffset.TryParse(s, this.Culture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to DateTimeOffset: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x001FDB60 File Offset: 0x001FDB60
		internal void ReaderReadAndAssert()
		{
			if (!this.Read())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		// Token: 0x06006938 RID: 26936 RVA: 0x001FDB74 File Offset: 0x001FDB74
		[NullableContext(1)]
		internal JsonReaderException CreateUnexpectedEndException()
		{
			return JsonReaderException.Create(this, "Unexpected end when reading JSON.");
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x001FDB84 File Offset: 0x001FDB84
		internal void ReadIntoWrappedTypeObject()
		{
			this.ReaderReadAndAssert();
			if (this.Value != null && this.Value.ToString() == "$type")
			{
				this.ReaderReadAndAssert();
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]", StringComparison.Ordinal))
				{
					this.ReaderReadAndAssert();
					if (this.Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		// Token: 0x0600693A RID: 26938 RVA: 0x001FDC28 File Offset: 0x001FDC28
		public void Skip()
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				this.Read();
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x001FDC78 File Offset: 0x001FDC78
		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null, true);
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x001FDC84 File Offset: 0x001FDC84
		protected void SetToken(JsonToken newToken, object value)
		{
			this.SetToken(newToken, value, true);
		}

		// Token: 0x0600693D RID: 26941 RVA: 0x001FDC90 File Offset: 0x001FDC90
		protected void SetToken(JsonToken newToken, object value, bool updateIndex)
		{
			this._tokenType = newToken;
			this._value = value;
			switch (newToken)
			{
			case JsonToken.StartObject:
				this._currentState = JsonReader.State.ObjectStart;
				this.Push(JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this._currentState = JsonReader.State.ArrayStart;
				this.Push(JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this._currentState = JsonReader.State.ConstructorStart;
				this.Push(JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				this._currentState = JsonReader.State.Property;
				this._currentPosition.PropertyName = (string)value;
				return;
			case JsonToken.Comment:
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.SetPostValueState(updateIndex);
				break;
			case JsonToken.EndObject:
				this.ValidateEnd(JsonToken.EndObject);
				return;
			case JsonToken.EndArray:
				this.ValidateEnd(JsonToken.EndArray);
				return;
			case JsonToken.EndConstructor:
				this.ValidateEnd(JsonToken.EndConstructor);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600693E RID: 26942 RVA: 0x001FDD68 File Offset: 0x001FDD68
		internal void SetPostValueState(bool updateIndex)
		{
			if (this.Peek() != JsonContainerType.None || this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.PostValue;
			}
			else
			{
				this.SetFinished();
			}
			if (updateIndex)
			{
				this.UpdateScopeWithFinishedValue();
			}
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x001FDDA0 File Offset: 0x001FDDA0
		private void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x001FDDC4 File Offset: 0x001FDDC4
		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, "JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (this.Peek() != JsonContainerType.None || this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.PostValue;
				return;
			}
			this.SetFinished();
		}

		// Token: 0x06006941 RID: 26945 RVA: 0x001FDE30 File Offset: 0x001FDE30
		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this.SetFinished();
				return;
			case JsonContainerType.Object:
				this._currentState = JsonReader.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonReader.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonReader.State.Constructor;
				return;
			default:
				throw JsonReaderException.Create(this, "While setting the reader state back to current object an unexpected JsonType was encountered: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x001FDEA0 File Offset: 0x001FDEA0
		private void SetFinished()
		{
			this._currentState = (this.SupportMultipleContent ? JsonReader.State.Start : JsonReader.State.Finished);
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x001FDEBC File Offset: 0x001FDEBC
		private JsonContainerType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JsonContainerType.Object;
			case JsonToken.EndArray:
				return JsonContainerType.Array;
			case JsonToken.EndConstructor:
				return JsonContainerType.Constructor;
			default:
				throw JsonReaderException.Create(this, "Not a valid close JsonToken: {0}".FormatWith(CultureInfo.InvariantCulture, token));
			}
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x001FDEFC File Offset: 0x001FDEFC
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x001FDF0C File Offset: 0x001FDF0C
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x001FDF28 File Offset: 0x001FDF28
		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._tokenType = JsonToken.None;
			this._value = null;
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x001FDF40 File Offset: 0x001FDF40
		internal void ReadAndAssert()
		{
			if (!this.Read())
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x001FDF5C File Offset: 0x001FDF5C
		internal void ReadForTypeAndAssert(JsonContract contract, bool hasConverter)
		{
			if (!this.ReadForType(contract, hasConverter))
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x06006949 RID: 26953 RVA: 0x001FDF78 File Offset: 0x001FDF78
		internal bool ReadForType(JsonContract contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return this.Read();
			}
			switch ((contract != null) ? contract.InternalReadType : ReadType.Read)
			{
			case ReadType.Read:
				return this.ReadAndMoveToContent();
			case ReadType.ReadAsInt32:
				this.ReadAsInt32();
				break;
			case ReadType.ReadAsInt64:
			{
				bool result = this.ReadAndMoveToContent();
				if (this.TokenType == JsonToken.Undefined)
				{
					throw JsonReaderException.Create(this, "An undefined token is not a valid {0}.".FormatWith(CultureInfo.InvariantCulture, ((contract != null) ? contract.UnderlyingType : null) ?? typeof(long)));
				}
				return result;
			}
			case ReadType.ReadAsBytes:
				this.ReadAsBytes();
				break;
			case ReadType.ReadAsString:
				this.ReadAsString();
				break;
			case ReadType.ReadAsDecimal:
				this.ReadAsDecimal();
				break;
			case ReadType.ReadAsDateTime:
				this.ReadAsDateTime();
				break;
			case ReadType.ReadAsDateTimeOffset:
				this.ReadAsDateTimeOffset();
				break;
			case ReadType.ReadAsDouble:
				this.ReadAsDouble();
				break;
			case ReadType.ReadAsBoolean:
				this.ReadAsBoolean();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return this.TokenType > JsonToken.None;
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x001FE0A0 File Offset: 0x001FE0A0
		internal bool ReadAndMoveToContent()
		{
			return this.Read() && this.MoveToContent();
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x001FE0B8 File Offset: 0x001FE0B8
		internal bool MoveToContent()
		{
			JsonToken tokenType = this.TokenType;
			while (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				if (!this.Read())
				{
					return false;
				}
				tokenType = this.TokenType;
			}
			return true;
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x001FE0F4 File Offset: 0x001FE0F4
		private JsonToken GetContentToken()
		{
			while (this.Read())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					return tokenType;
				}
			}
			this.SetToken(JsonToken.None);
			return JsonToken.None;
		}

		// Token: 0x0400355E RID: 13662
		private JsonToken _tokenType;

		// Token: 0x0400355F RID: 13663
		private object _value;

		// Token: 0x04003560 RID: 13664
		internal char _quoteChar;

		// Token: 0x04003561 RID: 13665
		internal JsonReader.State _currentState;

		// Token: 0x04003562 RID: 13666
		private JsonPosition _currentPosition;

		// Token: 0x04003563 RID: 13667
		private CultureInfo _culture;

		// Token: 0x04003564 RID: 13668
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x04003565 RID: 13669
		private int? _maxDepth;

		// Token: 0x04003566 RID: 13670
		private bool _hasExceededMaxDepth;

		// Token: 0x04003567 RID: 13671
		internal DateParseHandling _dateParseHandling;

		// Token: 0x04003568 RID: 13672
		internal FloatParseHandling _floatParseHandling;

		// Token: 0x04003569 RID: 13673
		private string _dateFormatString;

		// Token: 0x0400356A RID: 13674
		private List<JsonPosition> _stack;

		// Token: 0x0200106F RID: 4207
		[NullableContext(0)]
		protected internal enum State
		{
			// Token: 0x04004613 RID: 17939
			Start,
			// Token: 0x04004614 RID: 17940
			Complete,
			// Token: 0x04004615 RID: 17941
			Property,
			// Token: 0x04004616 RID: 17942
			ObjectStart,
			// Token: 0x04004617 RID: 17943
			Object,
			// Token: 0x04004618 RID: 17944
			ArrayStart,
			// Token: 0x04004619 RID: 17945
			Array,
			// Token: 0x0400461A RID: 17946
			Closed,
			// Token: 0x0400461B RID: 17947
			PostValue,
			// Token: 0x0400461C RID: 17948
			ConstructorStart,
			// Token: 0x0400461D RID: 17949
			Constructor,
			// Token: 0x0400461E RID: 17950
			Error,
			// Token: 0x0400461F RID: 17951
			Finished
		}
	}
}
