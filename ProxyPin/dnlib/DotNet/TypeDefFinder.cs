using System;
using System.Collections.Generic;
using System.Text;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x0200085D RID: 2141
	internal sealed class TypeDefFinder : ITypeDefFinder, IDisposable
	{
		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x001939E4 File Offset: 0x001939E4
		// (set) Token: 0x060051BF RID: 20927 RVA: 0x00193A28 File Offset: 0x00193A28
		public bool IsCacheEnabled
		{
			get
			{
				this.theLock.EnterReadLock();
				bool isCacheEnabled_NoLock;
				try
				{
					isCacheEnabled_NoLock = this.IsCacheEnabled_NoLock;
				}
				finally
				{
					this.theLock.ExitReadLock();
				}
				return isCacheEnabled_NoLock;
			}
			set
			{
				this.theLock.EnterWriteLock();
				try
				{
					this.IsCacheEnabled_NoLock = value;
				}
				finally
				{
					this.theLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060051C0 RID: 20928 RVA: 0x00193A68 File Offset: 0x00193A68
		// (set) Token: 0x060051C1 RID: 20929 RVA: 0x00193A70 File Offset: 0x00193A70
		private bool IsCacheEnabled_NoLock
		{
			get
			{
				return this.isCacheEnabled;
			}
			set
			{
				if (this.isCacheEnabled == value)
				{
					return;
				}
				if (this.typeEnumerator != null)
				{
					this.typeEnumerator.Dispose();
					this.typeEnumerator = null;
				}
				this.typeRefCache.Clear();
				this.normalNameCache.Clear();
				this.reflectionNameCache.Clear();
				if (value)
				{
					this.InitializeTypeEnumerator();
				}
				this.isCacheEnabled = value;
			}
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00193AE0 File Offset: 0x00193AE0
		public TypeDefFinder(IEnumerable<TypeDef> rootTypes) : this(rootTypes, true)
		{
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x00193AEC File Offset: 0x00193AEC
		public TypeDefFinder(IEnumerable<TypeDef> rootTypes, bool includeNestedTypes)
		{
			if (rootTypes == null)
			{
				throw new ArgumentNullException("rootTypes");
			}
			this.rootTypes = rootTypes;
			this.includeNestedTypes = includeNestedTypes;
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00193B70 File Offset: 0x00193B70
		private void InitializeTypeEnumerator()
		{
			if (this.typeEnumerator != null)
			{
				this.typeEnumerator.Dispose();
				this.typeEnumerator = null;
			}
			this.typeEnumerator = (this.includeNestedTypes ? AllTypesHelper.Types(this.rootTypes) : this.rootTypes).GetEnumerator();
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00193BCC File Offset: 0x00193BCC
		public void ResetCache()
		{
			this.theLock.EnterWriteLock();
			try
			{
				bool isCacheEnabled_NoLock = this.IsCacheEnabled_NoLock;
				this.IsCacheEnabled_NoLock = false;
				this.IsCacheEnabled_NoLock = isCacheEnabled_NoLock;
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00193C1C File Offset: 0x00193C1C
		public TypeDef Find(string fullName, bool isReflectionName)
		{
			if (fullName == null)
			{
				return null;
			}
			this.theLock.EnterWriteLock();
			TypeDef result;
			try
			{
				if (this.isCacheEnabled)
				{
					result = (isReflectionName ? this.FindCacheReflection(fullName) : this.FindCacheNormal(fullName));
				}
				else
				{
					result = (isReflectionName ? this.FindSlowReflection(fullName) : this.FindSlowNormal(fullName));
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x00193CA4 File Offset: 0x00193CA4
		public TypeDef Find(TypeRef typeRef)
		{
			if (typeRef == null)
			{
				return null;
			}
			this.theLock.EnterWriteLock();
			TypeDef result;
			try
			{
				result = (this.isCacheEnabled ? this.FindCache(typeRef) : this.FindSlow(typeRef));
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00193D08 File Offset: 0x00193D08
		private TypeDef FindCache(TypeRef typeRef)
		{
			TypeDef nextTypeDefCache;
			if (this.typeRefCache.TryGetValue(typeRef, out nextTypeDefCache))
			{
				return nextTypeDefCache;
			}
			SigComparer sigComparer = new SigComparer(SigComparerOptions.DontCompareTypeScope | SigComparerOptions.TypeRefCanReferenceGlobalType);
			do
			{
				nextTypeDefCache = this.GetNextTypeDefCache();
			}
			while (nextTypeDefCache != null && !sigComparer.Equals(nextTypeDefCache, typeRef));
			return nextTypeDefCache;
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x00193D54 File Offset: 0x00193D54
		private TypeDef FindCacheReflection(string fullName)
		{
			TypeDef nextTypeDefCache;
			if (this.reflectionNameCache.TryGetValue(fullName, out nextTypeDefCache))
			{
				return nextTypeDefCache;
			}
			for (;;)
			{
				nextTypeDefCache = this.GetNextTypeDefCache();
				if (nextTypeDefCache == null)
				{
					break;
				}
				this.sb.Length = 0;
				if (FullNameFactory.FullName(nextTypeDefCache, true, null, this.sb) == fullName)
				{
					return nextTypeDefCache;
				}
			}
			return nextTypeDefCache;
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x00193DAC File Offset: 0x00193DAC
		private TypeDef FindCacheNormal(string fullName)
		{
			TypeDef nextTypeDefCache;
			if (this.normalNameCache.TryGetValue(fullName, out nextTypeDefCache))
			{
				return nextTypeDefCache;
			}
			for (;;)
			{
				nextTypeDefCache = this.GetNextTypeDefCache();
				if (nextTypeDefCache == null)
				{
					break;
				}
				this.sb.Length = 0;
				if (FullNameFactory.FullName(nextTypeDefCache, false, null, this.sb) == fullName)
				{
					return nextTypeDefCache;
				}
			}
			return nextTypeDefCache;
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00193E04 File Offset: 0x00193E04
		private TypeDef FindSlow(TypeRef typeRef)
		{
			this.InitializeTypeEnumerator();
			SigComparer sigComparer = new SigComparer(SigComparerOptions.DontCompareTypeScope | SigComparerOptions.TypeRefCanReferenceGlobalType);
			TypeDef nextTypeDef;
			do
			{
				nextTypeDef = this.GetNextTypeDef();
			}
			while (nextTypeDef != null && !sigComparer.Equals(nextTypeDef, typeRef));
			return nextTypeDef;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00193E40 File Offset: 0x00193E40
		private TypeDef FindSlowReflection(string fullName)
		{
			this.InitializeTypeEnumerator();
			TypeDef nextTypeDef;
			for (;;)
			{
				nextTypeDef = this.GetNextTypeDef();
				if (nextTypeDef == null)
				{
					break;
				}
				this.sb.Length = 0;
				if (FullNameFactory.FullName(nextTypeDef, true, null, this.sb) == fullName)
				{
					return nextTypeDef;
				}
			}
			return nextTypeDef;
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x00193E8C File Offset: 0x00193E8C
		private TypeDef FindSlowNormal(string fullName)
		{
			this.InitializeTypeEnumerator();
			TypeDef nextTypeDef;
			for (;;)
			{
				nextTypeDef = this.GetNextTypeDef();
				if (nextTypeDef == null)
				{
					break;
				}
				this.sb.Length = 0;
				if (FullNameFactory.FullName(nextTypeDef, false, null, this.sb) == fullName)
				{
					return nextTypeDef;
				}
			}
			return nextTypeDef;
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00193ED8 File Offset: 0x00193ED8
		private TypeDef GetNextTypeDef()
		{
			while (this.typeEnumerator.MoveNext())
			{
				TypeDef typeDef = this.typeEnumerator.Current;
				if (typeDef != null)
				{
					return typeDef;
				}
			}
			return null;
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00193F10 File Offset: 0x00193F10
		private TypeDef GetNextTypeDefCache()
		{
			TypeDef nextTypeDef = this.GetNextTypeDef();
			if (nextTypeDef == null)
			{
				return null;
			}
			if (!this.typeRefCache.ContainsKey(nextTypeDef))
			{
				this.typeRefCache[nextTypeDef] = nextTypeDef;
			}
			this.sb.Length = 0;
			string key;
			if (!this.normalNameCache.ContainsKey(key = FullNameFactory.FullName(nextTypeDef, false, null, this.sb)))
			{
				this.normalNameCache[key] = nextTypeDef;
			}
			this.sb.Length = 0;
			if (!this.reflectionNameCache.ContainsKey(key = FullNameFactory.FullName(nextTypeDef, true, null, this.sb)))
			{
				this.reflectionNameCache[key] = nextTypeDef;
			}
			return nextTypeDef;
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00193FC4 File Offset: 0x00193FC4
		public void Dispose()
		{
			this.theLock.EnterWriteLock();
			try
			{
				if (this.typeEnumerator != null)
				{
					this.typeEnumerator.Dispose();
				}
				this.typeEnumerator = null;
				this.typeRefCache = null;
				this.normalNameCache = null;
				this.reflectionNameCache = null;
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
		}

		// Token: 0x040027AE RID: 10158
		private const SigComparerOptions TypeComparerOptions = SigComparerOptions.DontCompareTypeScope | SigComparerOptions.TypeRefCanReferenceGlobalType;

		// Token: 0x040027AF RID: 10159
		private bool isCacheEnabled;

		// Token: 0x040027B0 RID: 10160
		private readonly bool includeNestedTypes;

		// Token: 0x040027B1 RID: 10161
		private Dictionary<ITypeDefOrRef, TypeDef> typeRefCache = new Dictionary<ITypeDefOrRef, TypeDef>(new TypeEqualityComparer(SigComparerOptions.DontCompareTypeScope | SigComparerOptions.TypeRefCanReferenceGlobalType));

		// Token: 0x040027B2 RID: 10162
		private Dictionary<string, TypeDef> normalNameCache = new Dictionary<string, TypeDef>(StringComparer.Ordinal);

		// Token: 0x040027B3 RID: 10163
		private Dictionary<string, TypeDef> reflectionNameCache = new Dictionary<string, TypeDef>(StringComparer.Ordinal);

		// Token: 0x040027B4 RID: 10164
		private readonly StringBuilder sb = new StringBuilder();

		// Token: 0x040027B5 RID: 10165
		private IEnumerator<TypeDef> typeEnumerator;

		// Token: 0x040027B6 RID: 10166
		private readonly IEnumerable<TypeDef> rootTypes;

		// Token: 0x040027B7 RID: 10167
		private readonly Lock theLock = Lock.Create();
	}
}
