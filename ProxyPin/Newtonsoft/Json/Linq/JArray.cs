using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B18 RID: 2840
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x060071A8 RID: 29096 RVA: 0x00225708 File Offset: 0x00225708
		public override async Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await writer.WriteStartArrayAsync(cancellationToken).ConfigureAwait(false);
			for (int i = 0; i < this._values.Count; i++)
			{
				await this._values[i].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(false);
			}
			await writer.WriteEndArrayAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x0022576C File Offset: 0x0022576C
		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JArray.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x00225778 File Offset: 0x00225778
		public new static async Task<JArray> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (reader.TokenType == JsonToken.None)
			{
				ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
				if (!configuredTaskAwaiter.GetResult())
				{
					throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray a = new JArray();
			a.SetLineInfo(reader as IJsonLineInfo, settings);
			await a.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return a;
		}

		// Token: 0x170017B3 RID: 6067
		// (get) Token: 0x060071AB RID: 29099 RVA: 0x002257D4 File Offset: 0x002257D4
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170017B4 RID: 6068
		// (get) Token: 0x060071AC RID: 29100 RVA: 0x002257DC File Offset: 0x002257DC
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x002257E0 File Offset: 0x002257E0
		public JArray()
		{
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x002257F4 File Offset: 0x002257F4
		public JArray(JArray other) : base(other)
		{
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x00225808 File Offset: 0x00225808
		public JArray(params object[] content) : this(content)
		{
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x00225814 File Offset: 0x00225814
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x00225830 File Offset: 0x00225830
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x00225858 File Offset: 0x00225858
		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x00225860 File Offset: 0x00225860
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x0022586C File Offset: 0x0022586C
		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x002258F0 File Offset: 0x002258F0
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x002258FC File Offset: 0x002258FC
		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JArray result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jarray;
			}
			return result;
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x0022594C File Offset: 0x0022594C
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x0022595C File Offset: 0x0022595C
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x002259A8 File Offset: 0x002259A8
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170017B5 RID: 6069
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170017B6 RID: 6070
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x060071BE RID: 29118 RVA: 0x00225A8C File Offset: 0x00225A8C
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x060071BF RID: 29119 RVA: 0x00225AA4 File Offset: 0x00225AA4
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = (base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null;
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x00225AE8 File Offset: 0x00225AE8
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x00225AF4 File Offset: 0x00225AF4
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x00225B00 File Offset: 0x00225B00
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x00225B0C File Offset: 0x00225B0C
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x00225B2C File Offset: 0x00225B2C
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x00225B38 File Offset: 0x00225B38
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x00225B40 File Offset: 0x00225B40
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x00225B4C File Offset: 0x00225B4C
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x060071C8 RID: 29128 RVA: 0x00225B58 File Offset: 0x00225B58
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x00225B5C File Offset: 0x00225B5C
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x00225B68 File Offset: 0x00225B68
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x0400385D RID: 14429
		private readonly List<JToken> _values = new List<JToken>();
	}
}
