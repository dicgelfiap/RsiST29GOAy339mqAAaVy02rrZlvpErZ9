using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000B1A RID: 2842
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable, ITypedList, IBindingList, IList, ICollection, INotifyCollectionChanged
	{
		// Token: 0x060071E1 RID: 29153 RVA: 0x00225F64 File Offset: 0x00225F64
		internal async Task ReadTokenFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings options, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			int startDepth = reader.Depth;
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
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			await this.ReadContentFromAsync(reader, options, cancellationToken).ConfigureAwait(false);
			if (reader.Depth > startDepth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x00225FC8 File Offset: 0x00225FC8
		private async Task ReadContentFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			IJsonLineInfo lineInfo = reader as IJsonLineInfo;
			JContainer parent = this;
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
			do
			{
				JProperty jproperty = parent as JProperty;
				if (jproperty != null && jproperty.Value != null)
				{
					if (parent == this)
					{
						break;
					}
					parent = parent.Parent;
				}
				switch (reader.TokenType)
				{
				case JsonToken.None:
					goto IL_3B0;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo, settings);
					parent.Add(jobject);
					parent = jobject;
					goto IL_3B0;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo, settings);
					parent.Add(jarray);
					parent = jarray;
					goto IL_3B0;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(reader.Value.ToString());
					jconstructor.SetLineInfo(lineInfo, settings);
					parent.Add(jconstructor);
					parent = jconstructor;
					goto IL_3B0;
				}
				case JsonToken.PropertyName:
				{
					JProperty jproperty2 = JContainer.ReadProperty(reader, settings, lineInfo, parent);
					if (jproperty2 != null)
					{
						parent = jproperty2;
						goto IL_3B0;
					}
					await reader.SkipAsync(default(CancellationToken)).ConfigureAwait(false);
					goto IL_3B0;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(reader.Value.ToString());
						jvalue.SetLineInfo(lineInfo, settings);
						parent.Add(jvalue);
						goto IL_3B0;
					}
					goto IL_3B0;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(reader.Value);
					jvalue.SetLineInfo(lineInfo, settings);
					parent.Add(jvalue);
					goto IL_3B0;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(lineInfo, settings);
					parent.Add(jvalue);
					goto IL_3B0;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(lineInfo, settings);
					parent.Add(jvalue);
					goto IL_3B0;
				}
				case JsonToken.EndObject:
					if (parent == this)
					{
						goto Block_6;
					}
					parent = parent.Parent;
					goto IL_3B0;
				case JsonToken.EndArray:
					if (parent == this)
					{
						goto Block_5;
					}
					parent = parent.Parent;
					goto IL_3B0;
				case JsonToken.EndConstructor:
					if (parent == this)
					{
						goto Block_7;
					}
					parent = parent.Parent;
					goto IL_3B0;
				}
				goto Block_4;
				IL_3B0:
				configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
				}
			}
			while (configuredTaskAwaiter.GetResult());
			return;
			Block_4:
			goto IL_38B;
			Block_5:
			Block_6:
			Block_7:
			return;
			IL_38B:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060071E3 RID: 29155 RVA: 0x0022602C File Offset: 0x0022602C
		// (remove) Token: 0x060071E4 RID: 29156 RVA: 0x00226048 File Offset: 0x00226048
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Combine(this._listChanged, value);
			}
			remove
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Remove(this._listChanged, value);
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060071E5 RID: 29157 RVA: 0x00226064 File Offset: 0x00226064
		// (remove) Token: 0x060071E6 RID: 29158 RVA: 0x00226080 File Offset: 0x00226080
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Combine(this._addingNew, value);
			}
			remove
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Remove(this._addingNew, value);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060071E7 RID: 29159 RVA: 0x0022609C File Offset: 0x0022609C
		// (remove) Token: 0x060071E8 RID: 29160 RVA: 0x002260B8 File Offset: 0x002260B8
		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x060071E9 RID: 29161
		protected abstract IList<JToken> ChildrenTokens { get; }

		// Token: 0x060071EA RID: 29162 RVA: 0x002260D4 File Offset: 0x002260D4
		internal JContainer()
		{
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x002260DC File Offset: 0x002260DC
		internal JContainer(JContainer other) : this()
		{
			ValidationUtils.ArgumentNotNull(other, "other");
			int num = 0;
			foreach (JToken content in ((IEnumerable<JToken>)other))
			{
				this.AddInternal(num, content, false);
				num++;
			}
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x00226148 File Offset: 0x00226148
		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x00226170 File Offset: 0x00226170
		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x00226178 File Offset: 0x00226178
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNew = this._addingNew;
			if (addingNew == null)
			{
				return;
			}
			addingNew(this, e);
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x00226190 File Offset: 0x00226190
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChanged = this._listChanged;
			if (listChanged != null)
			{
				this._busy = true;
				try
				{
					listChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x002261D8 File Offset: 0x002261D8
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this._collectionChanged;
			if (collectionChanged != null)
			{
				this._busy = true;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x060071F1 RID: 29169 RVA: 0x00226220 File Offset: 0x00226220
		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x00226230 File Offset: 0x00226230
		internal bool ContentsEqual(JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			IList<JToken> childrenTokens = this.ChildrenTokens;
			IList<JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x060071F3 RID: 29171 RVA: 0x0022629C File Offset: 0x0022629C
		[Nullable(2)]
		public override JToken First
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x060071F4 RID: 29172 RVA: 0x002262CC File Offset: 0x002262CC
		[Nullable(2)]
		public override JToken Last
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x00226300 File Offset: 0x00226300
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x00226310 File Offset: 0x00226310
		public override IEnumerable<T> Values<[Nullable(2)] T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		// Token: 0x060071F7 RID: 29175 RVA: 0x00226320 File Offset: 0x00226320
		public IEnumerable<JToken> Descendants()
		{
			return this.GetDescendants(false);
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x0022632C File Offset: 0x0022632C
		public IEnumerable<JToken> DescendantsAndSelf()
		{
			return this.GetDescendants(true);
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x00226338 File Offset: 0x00226338
		internal IEnumerable<JToken> GetDescendants(bool self)
		{
			if (self)
			{
				yield return this;
			}
			foreach (JToken o in this.ChildrenTokens)
			{
				yield return o;
				JContainer jcontainer = o as JContainer;
				if (jcontainer != null)
				{
					foreach (JToken jtoken in jcontainer.Descendants())
					{
						yield return jtoken;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				o = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x00226350 File Offset: 0x00226350
		[NullableContext(2)]
		internal bool IsMultiContent([NotNull] object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		// Token: 0x060071FB RID: 29179 RVA: 0x00226384 File Offset: 0x00226384
		internal JToken EnsureParentToken([Nullable(2)] JToken item, bool skipParentCheck)
		{
			if (item == null)
			{
				return JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				item = item.CloneToken();
			}
			return item;
		}

		// Token: 0x060071FC RID: 29180
		[NullableContext(2)]
		internal abstract int IndexOfItem(JToken item);

		// Token: 0x060071FD RID: 29181 RVA: 0x002263DC File Offset: 0x002263DC
		[NullableContext(2)]
		internal virtual void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, skipParentCheck);
			JToken jtoken = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken2 = (index == childrenTokens.Count) ? null : childrenTokens[index];
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			childrenTokens.Insert(index, item);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
			}
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x002264C0 File Offset: 0x002264C0
		internal virtual void RemoveItemAt(int index)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = childrenTokens[index];
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			childrenTokens.RemoveAt(index);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, jtoken, index));
			}
		}

		// Token: 0x060071FF RID: 29183 RVA: 0x002265B4 File Offset: 0x002265B4
		[NullableContext(2)]
		internal virtual bool RemoveItem(JToken item)
		{
			if (item != null)
			{
				int num = this.IndexOfItem(item);
				if (num >= 0)
				{
					this.RemoveItemAt(num);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x002265E4 File Offset: 0x002265E4
		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x002265F4 File Offset: 0x002265F4
		[NullableContext(2)]
		internal virtual void SetItem(int index, JToken item)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = childrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, false);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			childrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, jtoken, index));
			}
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x00226720 File Offset: 0x00226720
		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			IList<JToken> childrenTokens = this.ChildrenTokens;
			foreach (JToken jtoken in childrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			childrenTokens.Clear();
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x002267C0 File Offset: 0x002267C0
		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int index = this.IndexOfItem(existing);
			this.SetItem(index, replacement);
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x002267F4 File Offset: 0x002267F4
		[NullableContext(2)]
		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		// Token: 0x06007205 RID: 29189 RVA: 0x00226804 File Offset: 0x00226804
		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken value in this.ChildrenTokens)
			{
				array.SetValue(value, arrayIndex + num);
				num++;
			}
		}

		// Token: 0x06007206 RID: 29190 RVA: 0x002268C8 File Offset: 0x002268C8
		internal static bool IsTokenUnchanged(JToken currentValue, [Nullable(2)] JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			if (jvalue == null)
			{
				return false;
			}
			if (newValue == null)
			{
				return jvalue.Type == JTokenType.Null;
			}
			return jvalue.Equals(newValue);
		}

		// Token: 0x06007207 RID: 29191 RVA: 0x00226900 File Offset: 0x00226900
		internal virtual void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x0022694C File Offset: 0x0022694C
		[NullableContext(2)]
		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content, false);
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x00226970 File Offset: 0x00226970
		internal void AddAndSkipParentCheck(JToken token)
		{
			this.AddInternal(this.ChildrenTokens.Count, token, true);
		}

		// Token: 0x0600720A RID: 29194 RVA: 0x00226994 File Offset: 0x00226994
		[NullableContext(2)]
		public void AddFirst(object content)
		{
			this.AddInternal(0, content, false);
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x002269A0 File Offset: 0x002269A0
		[NullableContext(2)]
		internal void AddInternal(int index, object content, bool skipParentCheck)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object content2 = enumerator.Current;
						this.AddInternal(num, content2, skipParentCheck);
						num++;
					}
					return;
				}
			}
			JToken item = JContainer.CreateFromContent(content);
			this.InsertItem(index, item, skipParentCheck);
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x00226A24 File Offset: 0x00226A24
		internal static JToken CreateFromContent([Nullable(2)] object content)
		{
			JToken jtoken = content as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			return new JValue(content);
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x00226A4C File Offset: 0x00226A4C
		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x00226A54 File Offset: 0x00226A54
		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x00226A64 File Offset: 0x00226A64
		public void RemoveAll()
		{
			this.ClearItems();
		}

		// Token: 0x06007210 RID: 29200
		internal abstract void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings);

		// Token: 0x06007211 RID: 29201 RVA: 0x00226A6C File Offset: 0x00226A6C
		public void Merge(object content)
		{
			this.MergeItem(content, null);
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x00226A78 File Offset: 0x00226A78
		public void Merge(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			this.MergeItem(content, settings);
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x00226A84 File Offset: 0x00226A84
		internal void ReadTokenFrom(JsonReader reader, [Nullable(2)] JsonLoadSettings options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			this.ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x00226B00 File Offset: 0x00226B00
		internal void ReadContentFrom(JsonReader r, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				JProperty jproperty = jcontainer as JProperty;
				if (jproperty != null && jproperty.Value != null)
				{
					if (jcontainer == this)
					{
						break;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_218;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_218;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_218;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_218;
				}
				case JsonToken.PropertyName:
				{
					JProperty jproperty2 = JContainer.ReadProperty(r, settings, lineInfo, jcontainer);
					if (jproperty2 != null)
					{
						jcontainer = jproperty2;
						goto IL_218;
					}
					r.Skip();
					goto IL_218;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(r.Value.ToString());
						jvalue.SetLineInfo(lineInfo, settings);
						jcontainer.Add(jvalue);
						goto IL_218;
					}
					goto IL_218;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_218;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_218;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_218;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_218;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_218;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_218;
				}
				goto Block_4;
				IL_218:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, r.TokenType));
		}

		// Token: 0x06007215 RID: 29205 RVA: 0x00226D34 File Offset: 0x00226D34
		[NullableContext(2)]
		private static JProperty ReadProperty([Nullable(1)] JsonReader r, JsonLoadSettings settings, IJsonLineInfo lineInfo, [Nullable(1)] JContainer parent)
		{
			DuplicatePropertyNameHandling duplicatePropertyNameHandling = (settings != null) ? settings.DuplicatePropertyNameHandling : DuplicatePropertyNameHandling.Replace;
			JObject jobject = (JObject)parent;
			string text = r.Value.ToString();
			JProperty jproperty = jobject.Property(text, StringComparison.Ordinal);
			if (jproperty != null)
			{
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Ignore)
				{
					return null;
				}
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Error)
				{
					throw JsonReaderException.Create(r, "Property with the name '{0}' already exists in the current JSON object.".FormatWith(CultureInfo.InvariantCulture, text));
				}
			}
			JProperty jproperty2 = new JProperty(text);
			jproperty2.SetLineInfo(lineInfo, settings);
			if (jproperty == null)
			{
				parent.Add(jproperty2);
			}
			else
			{
				jproperty.Replace(jproperty2);
			}
			return jproperty2;
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x00226DC8 File Offset: 0x00226DC8
		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		// Token: 0x06007217 RID: 29207 RVA: 0x00226E24 File Offset: 0x00226E24
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x00226E2C File Offset: 0x00226E2C
		[return: Nullable(2)]
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICustomTypeDescriptor customTypeDescriptor = this.First as ICustomTypeDescriptor;
			if (customTypeDescriptor == null)
			{
				return null;
			}
			return customTypeDescriptor.GetProperties();
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x00226E48 File Offset: 0x00226E48
		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x00226E54 File Offset: 0x00226E54
		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x00226E60 File Offset: 0x00226E60
		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170017C0 RID: 6080
		JToken IList<JToken>.this[int index]
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

		// Token: 0x0600721E RID: 29214 RVA: 0x00226E84 File Offset: 0x00226E84
		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x00226E90 File Offset: 0x00226E90
		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x00226E98 File Offset: 0x00226E98
		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x00226EA4 File Offset: 0x00226EA4
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06007222 RID: 29218 RVA: 0x00226EB0 File Offset: 0x00226EB0
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x00226EB4 File Offset: 0x00226EB4
		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x00226EC0 File Offset: 0x00226EC0
		[return: Nullable(2)]
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			JToken jtoken = value as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x00226EF4 File Offset: 0x00226EF4
		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		// Token: 0x06007226 RID: 29222 RVA: 0x00226F0C File Offset: 0x00226F0C
		void IList.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x00226F14 File Offset: 0x00226F14
		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		// Token: 0x06007228 RID: 29224 RVA: 0x00226F24 File Offset: 0x00226F24
		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x00226F34 File Offset: 0x00226F34
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value), false);
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x0600722A RID: 29226 RVA: 0x00226F48 File Offset: 0x00226F48
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x0600722B RID: 29227 RVA: 0x00226F4C File Offset: 0x00226F4C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x00226F50 File Offset: 0x00226F50
		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x00226F60 File Offset: 0x00226F60
		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170017C4 RID: 6084
		object IList.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		// Token: 0x06007230 RID: 29232 RVA: 0x00226F88 File Offset: 0x00226F88
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06007231 RID: 29233 RVA: 0x00226F94 File Offset: 0x00226F94
		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06007232 RID: 29234 RVA: 0x00226FA4 File Offset: 0x00226FA4
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06007233 RID: 29235 RVA: 0x00226FA8 File Offset: 0x00226FA8
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06007234 RID: 29236 RVA: 0x00226FD0 File Offset: 0x00226FD0
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x00226FD4 File Offset: 0x00226FD4
		object IBindingList.AddNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			this.OnAddingNew(addingNewEventArgs);
			if (addingNewEventArgs.NewObject == null)
			{
				throw new JsonException("Could not determine new value to add to '{0}'.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			JToken jtoken = addingNewEventArgs.NewObject as JToken;
			if (jtoken == null)
			{
				throw new JsonException("New item to be added to collection must be compatible with {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JToken)));
			}
			this.Add(jtoken);
			return jtoken;
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06007236 RID: 29238 RVA: 0x00227054 File Offset: 0x00227054
		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x06007237 RID: 29239 RVA: 0x00227058 File Offset: 0x00227058
		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x06007238 RID: 29240 RVA: 0x0022705C File Offset: 0x0022705C
		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x00227060 File Offset: 0x00227060
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x00227068 File Offset: 0x00227068
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x0600723B RID: 29243 RVA: 0x00227070 File Offset: 0x00227070
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x00227074 File Offset: 0x00227074
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x00227078 File Offset: 0x00227078
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x0600723E RID: 29246 RVA: 0x00227080 File Offset: 0x00227080
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x0600723F RID: 29247 RVA: 0x00227084 File Offset: 0x00227084
		[Nullable(2)]
		PropertyDescriptor IBindingList.SortProperty
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06007240 RID: 29248 RVA: 0x00227088 File Offset: 0x00227088
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06007241 RID: 29249 RVA: 0x0022708C File Offset: 0x0022708C
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06007242 RID: 29250 RVA: 0x00227090 File Offset: 0x00227090
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x00227094 File Offset: 0x00227094
		internal static void MergeEnumerableContent(JContainer target, IEnumerable content, [Nullable(2)] JsonMergeSettings settings)
		{
			switch ((settings != null) ? settings.MergeArrayHandling : MergeArrayHandling.Concat)
			{
			case MergeArrayHandling.Concat:
				using (IEnumerator enumerator = content.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						JToken content2 = (JToken)obj;
						target.Add(content2);
					}
					return;
				}
				break;
			case MergeArrayHandling.Union:
				break;
			case MergeArrayHandling.Replace:
				goto IL_CB;
			case MergeArrayHandling.Merge:
				goto IL_11A;
			default:
				goto IL_1CC;
			}
			HashSet<JToken> hashSet = new HashSet<JToken>(target, JToken.EqualityComparer);
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					JToken jtoken = (JToken)obj2;
					if (hashSet.Add(jtoken))
					{
						target.Add(jtoken);
					}
				}
				return;
			}
			IL_CB:
			if (target == content)
			{
				return;
			}
			target.ClearItems();
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					JToken content3 = (JToken)obj3;
					target.Add(content3);
				}
				return;
			}
			IL_11A:
			int num = 0;
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj4 = enumerator.Current;
					if (num < target.Count)
					{
						JContainer jcontainer = target[num] as JContainer;
						if (jcontainer != null)
						{
							jcontainer.Merge(obj4, settings);
						}
						else if (obj4 != null)
						{
							JToken jtoken2 = JContainer.CreateFromContent(obj4);
							if (jtoken2.Type != JTokenType.Null)
							{
								target[num] = jtoken2;
							}
						}
					}
					else
					{
						target.Add(obj4);
					}
					num++;
				}
				return;
			}
			IL_1CC:
			throw new ArgumentOutOfRangeException("settings", "Unexpected merge array handling when merging JSON.");
		}

		// Token: 0x04003860 RID: 14432
		[Nullable(2)]
		internal ListChangedEventHandler _listChanged;

		// Token: 0x04003861 RID: 14433
		[Nullable(2)]
		internal AddingNewEventHandler _addingNew;

		// Token: 0x04003862 RID: 14434
		[Nullable(2)]
		internal NotifyCollectionChangedEventHandler _collectionChanged;

		// Token: 0x04003863 RID: 14435
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x04003864 RID: 14436
		private bool _busy;
	}
}
