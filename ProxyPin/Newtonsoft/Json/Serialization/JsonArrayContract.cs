using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000AE2 RID: 2786
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonArrayContract : JsonContainerContract
	{
		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06006EAA RID: 28330 RVA: 0x00217FD4 File Offset: 0x00217FD4
		public Type CollectionItemType { get; }

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06006EAB RID: 28331 RVA: 0x00217FDC File Offset: 0x00217FDC
		public bool IsMultidimensionalArray { get; }

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06006EAC RID: 28332 RVA: 0x00217FE4 File Offset: 0x00217FE4
		internal bool IsArray { get; }

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06006EAD RID: 28333 RVA: 0x00217FEC File Offset: 0x00217FEC
		internal bool ShouldCreateWrapper { get; }

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06006EAE RID: 28334 RVA: 0x00217FF4 File Offset: 0x00217FF4
		// (set) Token: 0x06006EAF RID: 28335 RVA: 0x00217FFC File Offset: 0x00217FFC
		internal bool CanDeserialize { get; private set; }

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06006EB0 RID: 28336 RVA: 0x00218008 File Offset: 0x00218008
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				if (this._parameterizedCreator == null && this._parameterizedConstructor != null)
				{
					this._parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(this._parameterizedConstructor);
				}
				return this._parameterizedCreator;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06006EB1 RID: 28337 RVA: 0x00218044 File Offset: 0x00218044
		// (set) Token: 0x06006EB2 RID: 28338 RVA: 0x0021804C File Offset: 0x0021804C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._overrideCreator = value;
				this.CanDeserialize = true;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06006EB3 RID: 28339 RVA: 0x0021805C File Offset: 0x0021805C
		// (set) Token: 0x06006EB4 RID: 28340 RVA: 0x00218064 File Offset: 0x00218064
		public bool HasParameterizedCreator { get; set; }

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06006EB5 RID: 28341 RVA: 0x00218070 File Offset: 0x00218070
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x06006EB6 RID: 28342 RVA: 0x00218098 File Offset: 0x00218098
		[NullableContext(1)]
		public JsonArrayContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Array;
			this.IsArray = (base.CreatedType.IsArray || (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Linq.EmptyPartition`1"));
			bool canDeserialize;
			Type type;
			if (this.IsArray)
			{
				this.CollectionItemType = ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				canDeserialize = true;
				this.IsMultidimensionalArray = (base.CreatedType.IsArray && base.UnderlyingType.GetArrayRank() > 1);
			}
			else if (typeof(IList).IsAssignableFrom(this.NonNullableUnderlyingType))
			{
				if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
				{
					this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				}
				else
				{
					this.CollectionItemType = ReflectionUtils.GetCollectionItemType(this.NonNullableUnderlyingType);
				}
				if (this.NonNullableUnderlyingType == typeof(IList))
				{
					base.CreatedType = typeof(List<object>);
				}
				if (this.CollectionItemType != null)
				{
					this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(this.NonNullableUnderlyingType, typeof(ReadOnlyCollection<>));
				canDeserialize = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
			{
				this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>)) || ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IList<>)))
				{
					base.CreatedType = typeof(List<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(ISet<>)))
				{
					base.CreatedType = typeof(HashSet<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				canDeserialize = true;
				this.ShouldCreateWrapper = 1;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyCollection<>), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyCollection<>)) || ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IReadOnlyList<>)))
				{
					base.CreatedType = typeof(ReadOnlyCollection<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, this.CollectionItemType);
				this.StoreFSharpListCreatorIfNecessary(this.NonNullableUnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				canDeserialize = this.HasParameterizedCreatorInternal;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IEnumerable<>), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IEnumerable<>)))
				{
					base.CreatedType = typeof(List<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				this.StoreFSharpListCreatorIfNecessary(this.NonNullableUnderlyingType);
				if (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					this._genericCollectionDefinitionType = type;
					this.IsReadOnlyOrFixedSize = false;
					this.ShouldCreateWrapper = 0;
					canDeserialize = true;
				}
				else
				{
					this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
					this.IsReadOnlyOrFixedSize = true;
					this.ShouldCreateWrapper = 1;
					canDeserialize = this.HasParameterizedCreatorInternal;
				}
			}
			else
			{
				canDeserialize = false;
				this.ShouldCreateWrapper = 1;
			}
			this.CanDeserialize = canDeserialize;
			Type createdType;
			ObjectConstructor<object> parameterizedCreator;
			if (this.CollectionItemType != null && ImmutableCollectionsUtils.TryBuildImmutableForArrayContract(this.NonNullableUnderlyingType, this.CollectionItemType, out createdType, out parameterizedCreator))
			{
				base.CreatedType = createdType;
				this._parameterizedCreator = parameterizedCreator;
				this.IsReadOnlyOrFixedSize = true;
				this.CanDeserialize = true;
			}
		}

		// Token: 0x06006EB7 RID: 28343 RVA: 0x00218598 File Offset: 0x00218598
		[NullableContext(1)]
		internal IWrappedCollection CreateWrapper(object list)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(CollectionWrapper<>).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				Type type;
				if (ReflectionUtils.InheritsGenericDefinition(this._genericCollectionDefinitionType, typeof(List<>)) || this._genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					type = typeof(ICollection<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				else
				{
					type = this._genericCollectionDefinitionType;
				}
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[]
				{
					type
				});
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedCollection)this._genericWrapperCreator(new object[]
			{
				list
			});
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x00218680 File Offset: 0x00218680
		[NullableContext(1)]
		internal IList CreateTemporaryCollection()
		{
			if (this._genericTemporaryCollectionCreator == null)
			{
				Type type = (this.IsMultidimensionalArray || this.CollectionItemType == null) ? typeof(object) : this.CollectionItemType;
				Type type2 = typeof(List<>).MakeGenericType(new Type[]
				{
					type
				});
				this._genericTemporaryCollectionCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type2);
			}
			return (IList)this._genericTemporaryCollectionCreator();
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x0021870C File Offset: 0x0021870C
		[NullableContext(1)]
		private void StoreFSharpListCreatorIfNecessary(Type underlyingType)
		{
			if (!this.HasParameterizedCreatorInternal && underlyingType.Name == "FSharpList`1")
			{
				FSharpUtils.EnsureInitialized(underlyingType.Assembly());
				this._parameterizedCreator = FSharpUtils.Instance.CreateSeq(this.CollectionItemType);
			}
		}

		// Token: 0x04003732 RID: 14130
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x04003733 RID: 14131
		private Type _genericWrapperType;

		// Token: 0x04003734 RID: 14132
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x04003735 RID: 14133
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Func<object> _genericTemporaryCollectionCreator;

		// Token: 0x04003739 RID: 14137
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x0400373A RID: 14138
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x0400373B RID: 14139
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _overrideCreator;
	}
}
