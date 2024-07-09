using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AE3 RID: 2787
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06006EBA RID: 28346 RVA: 0x00218760 File Offset: 0x00218760
		// (set) Token: 0x06006EBB RID: 28347 RVA: 0x00218768 File Offset: 0x00218768
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06006EBC RID: 28348 RVA: 0x002187BC File Offset: 0x002187BC
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06006EBD RID: 28349 RVA: 0x002187C4 File Offset: 0x002187C4
		// (set) Token: 0x06006EBE RID: 28350 RVA: 0x002187CC File Offset: 0x002187CC
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06006EBF RID: 28351 RVA: 0x002187D8 File Offset: 0x002187D8
		// (set) Token: 0x06006EC0 RID: 28352 RVA: 0x002187E0 File Offset: 0x002187E0
		public bool? ItemIsReference { get; set; }

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06006EC1 RID: 28353 RVA: 0x002187EC File Offset: 0x002187EC
		// (set) Token: 0x06006EC2 RID: 28354 RVA: 0x002187F4 File Offset: 0x002187F4
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06006EC3 RID: 28355 RVA: 0x00218800 File Offset: 0x00218800
		// (set) Token: 0x06006EC4 RID: 28356 RVA: 0x00218808 File Offset: 0x00218808
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x06006EC5 RID: 28357 RVA: 0x00218814 File Offset: 0x00218814
		[NullableContext(1)]
		internal JsonContainerContract(Type underlyingType) : base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x0400373D RID: 14141
		private JsonContract _itemContract;

		// Token: 0x0400373E RID: 14142
		private JsonContract _finalItemContract;
	}
}
