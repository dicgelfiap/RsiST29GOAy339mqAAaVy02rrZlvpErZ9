using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B1D RID: 2845
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		// Token: 0x06007292 RID: 29330 RVA: 0x00227FDC File Offset: 0x00227FDC
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(this._name, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return this.WriteValueAsync(writer, cancellationToken, converters);
			}
			return this.WriteToAsync(task, writer, cancellationToken, converters);
		}

		// Token: 0x06007293 RID: 29331 RVA: 0x0022801C File Offset: 0x0022801C
		private async Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			await task.ConfigureAwait(false);
			await this.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false);
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x00228088 File Offset: 0x00228088
		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = this.Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x002280B8 File Offset: 0x002280B8
		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JProperty.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x002280C4 File Offset: 0x002280C4
		public new static async Task<JProperty> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
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
					throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
				}
			}
			await reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false);
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty p = new JProperty((string)reader.Value);
			p.SetLineInfo(reader as IJsonLineInfo, settings);
			await p.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false);
			return p;
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06007297 RID: 29335 RVA: 0x00228120 File Offset: 0x00228120
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06007298 RID: 29336 RVA: 0x00228128 File Offset: 0x00228128
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06007299 RID: 29337 RVA: 0x00228130 File Offset: 0x00228130
		// (set) Token: 0x0600729A RID: 29338 RVA: 0x00228140 File Offset: 0x00228140
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken item = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, item, false);
					return;
				}
				this.SetItem(0, item);
			}
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x00228188 File Offset: 0x00228188
		public JProperty(JProperty other) : base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x002281A8 File Offset: 0x002281A8
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x002281BC File Offset: 0x002281BC
		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x0022822C File Offset: 0x0022822C
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x0022824C File Offset: 0x0022824C
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060072A0 RID: 29344 RVA: 0x0022826C File Offset: 0x0022826C
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x00228284 File Offset: 0x00228284
		[NullableContext(2)]
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			base.InsertItem(0, item, false);
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x002282DC File Offset: 0x002282DC
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x002282E8 File Offset: 0x002282E8
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = (jproperty != null) ? jproperty.Value : null;
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x0022832C File Offset: 0x0022832C
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060072A5 RID: 29349 RVA: 0x0022834C File Offset: 0x0022834C
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x060072A6 RID: 29350 RVA: 0x0022838C File Offset: 0x0022838C
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060072A7 RID: 29351 RVA: 0x00228394 File Offset: 0x00228394
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x00228398 File Offset: 0x00228398
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x002283C0 File Offset: 0x002283C0
		public JProperty(string name, params object[] content) : this(name, content)
		{
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x002283CC File Offset: 0x002283CC
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x00228424 File Offset: 0x00228424
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x00228460 File Offset: 0x00228460
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x00228498 File Offset: 0x00228498
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x002284A4 File Offset: 0x002284A4
		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x0400386A RID: 14442
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x0400386B RID: 14443
		private readonly string _name;

		// Token: 0x02001122 RID: 4386
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06009239 RID: 37433 RVA: 0x002BEE80 File Offset: 0x002BEE80
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x0600923A RID: 37434 RVA: 0x002BEE90 File Offset: 0x002BEE90
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600923B RID: 37435 RVA: 0x002BEE98 File Offset: 0x002BEE98
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x0600923C RID: 37436 RVA: 0x002BEEA4 File Offset: 0x002BEEA4
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x0600923D RID: 37437 RVA: 0x002BEEB0 File Offset: 0x002BEEB0
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x0600923E RID: 37438 RVA: 0x002BEEBC File Offset: 0x002BEEBC
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x0600923F RID: 37439 RVA: 0x002BEED8 File Offset: 0x002BEED8
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x17001E4D RID: 7757
			// (get) Token: 0x06009240 RID: 37440 RVA: 0x002BEEF0 File Offset: 0x002BEEF0
			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x17001E4E RID: 7758
			// (get) Token: 0x06009241 RID: 37441 RVA: 0x002BEF00 File Offset: 0x002BEF00
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06009242 RID: 37442 RVA: 0x002BEF04 File Offset: 0x002BEF04
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x06009243 RID: 37443 RVA: 0x002BEF18 File Offset: 0x002BEF18
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x06009244 RID: 37444 RVA: 0x002BEF28 File Offset: 0x002BEF28
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x17001E4F RID: 7759
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this._token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					this._token = value;
				}
			}

			// Token: 0x0400495A RID: 18778
			[Nullable(2)]
			internal JToken _token;
		}
	}
}
