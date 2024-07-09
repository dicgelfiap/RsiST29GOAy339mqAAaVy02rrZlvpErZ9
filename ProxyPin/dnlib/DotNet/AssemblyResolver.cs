using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using dnlib.Threading;

namespace dnlib.DotNet
{
	// Token: 0x02000781 RID: 1921
	[ComVisible(true)]
	public class AssemblyResolver : IAssemblyResolver
	{
		// Token: 0x0600445E RID: 17502 RVA: 0x0016ACAC File Offset: 0x0016ACAC
		static AssemblyResolver()
		{
			AssemblyResolver.gacInfos = new List<AssemblyResolver.GacInfo>();
			if (Type.GetType("Mono.Runtime") != null)
			{
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
				List<string> list = new List<string>();
				foreach (string path in AssemblyResolver.FindMonoPrefixes())
				{
					string text = Path.Combine(Path.Combine(Path.Combine(path, "lib"), "mono"), "gac");
					if (!dictionary.ContainsKey(text))
					{
						dictionary[text] = true;
						if (Directory.Exists(text))
						{
							AssemblyResolver.gacInfos.Add(new AssemblyResolver.GacInfo(-1, "", Path.GetDirectoryName(text), new string[]
							{
								Path.GetFileName(text)
							}));
						}
						text = Path.GetDirectoryName(text);
						foreach (string text2 in AssemblyResolver.monoVerDirs)
						{
							string text3 = text;
							foreach (string path2 in text2.Split(new char[]
							{
								'\\'
							}))
							{
								text3 = Path.Combine(text3, path2);
							}
							if (Directory.Exists(text3))
							{
								list.Add(text3);
							}
						}
					}
				}
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_PATH");
				if (environmentVariable != null)
				{
					string[] array = environmentVariable.Split(new char[]
					{
						Path.PathSeparator
					});
					for (int i = 0; i < array.Length; i++)
					{
						string text4 = array[i].Trim();
						if (text4 != string.Empty && Directory.Exists(text4))
						{
							list.Add(text4);
						}
					}
				}
				AssemblyResolver.extraMonoPaths = list.ToArray();
				return;
			}
			string environmentVariable2 = Environment.GetEnvironmentVariable("WINDIR");
			if (!string.IsNullOrEmpty(environmentVariable2))
			{
				string path3 = Path.Combine(environmentVariable2, "assembly");
				if (Directory.Exists(path3))
				{
					AssemblyResolver.gacInfos.Add(new AssemblyResolver.GacInfo(2, "", path3, new string[]
					{
						"GAC_32",
						"GAC_64",
						"GAC_MSIL",
						"GAC"
					}));
				}
				path3 = Path.Combine(Path.Combine(environmentVariable2, "Microsoft.NET"), "assembly");
				if (Directory.Exists(path3))
				{
					AssemblyResolver.gacInfos.Add(new AssemblyResolver.GacInfo(4, "v4.0_", path3, new string[]
					{
						"GAC_32",
						"GAC_64",
						"GAC_MSIL"
					}));
				}
			}
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x0016B020 File Offset: 0x0016B020
		private static string GetCurrentMonoPrefix()
		{
			string text = typeof(object).Module.FullyQualifiedName;
			for (int i = 0; i < 4; i++)
			{
				text = Path.GetDirectoryName(text);
			}
			return text;
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x0016B060 File Offset: 0x0016B060
		private static IEnumerable<string> FindMonoPrefixes()
		{
			yield return AssemblyResolver.GetCurrentMonoPrefix();
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_GAC_PREFIX");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				string[] array = environmentVariable.Split(new char[]
				{
					Path.PathSeparator
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					if (text != string.Empty)
					{
						yield return text;
					}
				}
				array = null;
			}
			yield break;
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x0016B06C File Offset: 0x0016B06C
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x0016B074 File Offset: 0x0016B074
		public ModuleContext DefaultModuleContext
		{
			get
			{
				return this.defaultModuleContext;
			}
			set
			{
				this.defaultModuleContext = value;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0016B080 File Offset: 0x0016B080
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x0016B088 File Offset: 0x0016B088
		public bool FindExactMatch
		{
			get
			{
				return this.findExactMatch;
			}
			set
			{
				this.findExactMatch = value;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x0016B094 File Offset: 0x0016B094
		// (set) Token: 0x06004466 RID: 17510 RVA: 0x0016B09C File Offset: 0x0016B09C
		public bool EnableFrameworkRedirect
		{
			get
			{
				return this.enableFrameworkRedirect;
			}
			set
			{
				this.enableFrameworkRedirect = value;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x0016B0A8 File Offset: 0x0016B0A8
		// (set) Token: 0x06004468 RID: 17512 RVA: 0x0016B0B0 File Offset: 0x0016B0B0
		public bool EnableTypeDefCache
		{
			get
			{
				return this.enableTypeDefCache;
			}
			set
			{
				this.enableTypeDefCache = value;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x0016B0BC File Offset: 0x0016B0BC
		// (set) Token: 0x0600446A RID: 17514 RVA: 0x0016B0C4 File Offset: 0x0016B0C4
		public bool UseGAC
		{
			get
			{
				return this.useGac;
			}
			set
			{
				this.useGac = value;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x0016B0D0 File Offset: 0x0016B0D0
		public IList<string> PreSearchPaths
		{
			get
			{
				return this.preSearchPaths;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x0016B0D8 File Offset: 0x0016B0D8
		public IList<string> PostSearchPaths
		{
			get
			{
				return this.postSearchPaths;
			}
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x0016B0E0 File Offset: 0x0016B0E0
		public AssemblyResolver() : this(null)
		{
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x0016B0EC File Offset: 0x0016B0EC
		public AssemblyResolver(ModuleContext defaultModuleContext)
		{
			this.defaultModuleContext = defaultModuleContext;
			this.enableFrameworkRedirect = true;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x0016B15C File Offset: 0x0016B15C
		public AssemblyDef Resolve(IAssembly assembly, ModuleDef sourceModule)
		{
			if (assembly == null)
			{
				return null;
			}
			if (this.EnableFrameworkRedirect && !this.FindExactMatch)
			{
				FrameworkRedirect.ApplyFrameworkRedirect(ref assembly, sourceModule);
			}
			this.theLock.EnterWriteLock();
			AssemblyDef result;
			try
			{
				AssemblyDef assemblyDef = this.Resolve2(assembly, sourceModule);
				if (assemblyDef == null)
				{
					string text = UTF8String.ToSystemStringOrEmpty(assembly.Name);
					string text2 = text.Trim();
					if (text != text2)
					{
						assembly = new AssemblyNameInfo
						{
							Name = text2,
							Version = assembly.Version,
							PublicKeyOrToken = assembly.PublicKeyOrToken,
							Culture = assembly.Culture
						};
						assemblyDef = this.Resolve2(assembly, sourceModule);
					}
				}
				if (assemblyDef == null)
				{
					this.cachedAssemblies[AssemblyResolver.GetAssemblyNameKey(assembly)] = null;
					result = null;
				}
				else
				{
					string assemblyNameKey = AssemblyResolver.GetAssemblyNameKey(assemblyDef);
					string assemblyNameKey2 = AssemblyResolver.GetAssemblyNameKey(assembly);
					AssemblyDef assemblyDef2;
					this.cachedAssemblies.TryGetValue(assemblyNameKey, out assemblyDef2);
					AssemblyDef assemblyDef3;
					this.cachedAssemblies.TryGetValue(assemblyNameKey2, out assemblyDef3);
					if (assemblyDef2 != assemblyDef && assemblyDef3 != assemblyDef && this.enableTypeDefCache)
					{
						IList<ModuleDef> modules = assemblyDef.Modules;
						int count = modules.Count;
						for (int i = 0; i < count; i++)
						{
							ModuleDef moduleDef = modules[i];
							if (moduleDef != null)
							{
								moduleDef.EnableTypeDefFindCache = true;
							}
						}
					}
					bool flag = false;
					if (!this.cachedAssemblies.ContainsKey(assemblyNameKey))
					{
						this.cachedAssemblies.Add(assemblyNameKey, assemblyDef);
						flag = true;
					}
					if (!this.cachedAssemblies.ContainsKey(assemblyNameKey2))
					{
						this.cachedAssemblies.Add(assemblyNameKey2, assemblyDef);
						flag = true;
					}
					if (flag || assemblyDef2 == assemblyDef || assemblyDef3 == assemblyDef)
					{
						result = assemblyDef;
					}
					else
					{
						ModuleDef manifestModule = assemblyDef.ManifestModule;
						if (manifestModule != null)
						{
							manifestModule.Dispose();
						}
						result = (assemblyDef2 ?? assemblyDef3);
					}
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x0016B364 File Offset: 0x0016B364
		public bool AddToCache(ModuleDef module)
		{
			return module != null && this.AddToCache(module.Assembly);
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x0016B37C File Offset: 0x0016B37C
		public bool AddToCache(AssemblyDef asm)
		{
			if (asm == null)
			{
				return false;
			}
			string assemblyNameKey = AssemblyResolver.GetAssemblyNameKey(asm);
			this.theLock.EnterWriteLock();
			bool result;
			try
			{
				AssemblyDef assemblyDef;
				if (this.cachedAssemblies.TryGetValue(assemblyNameKey, out assemblyDef) && assemblyDef != null)
				{
					result = (asm == assemblyDef);
				}
				else
				{
					this.cachedAssemblies[assemblyNameKey] = asm;
					result = true;
				}
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x0016B3F8 File Offset: 0x0016B3F8
		public bool Remove(ModuleDef module)
		{
			return module != null && this.Remove(module.Assembly);
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x0016B410 File Offset: 0x0016B410
		public bool Remove(AssemblyDef asm)
		{
			if (asm == null)
			{
				return false;
			}
			string assemblyNameKey = AssemblyResolver.GetAssemblyNameKey(asm);
			this.theLock.EnterWriteLock();
			bool result;
			try
			{
				result = this.cachedAssemblies.Remove(assemblyNameKey);
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x0016B468 File Offset: 0x0016B468
		public void Clear()
		{
			this.theLock.EnterWriteLock();
			List<AssemblyDef> list;
			try
			{
				list = new List<AssemblyDef>(this.cachedAssemblies.Values);
				this.cachedAssemblies.Clear();
			}
			finally
			{
				this.theLock.ExitWriteLock();
			}
			foreach (AssemblyDef assemblyDef in list)
			{
				if (assemblyDef != null)
				{
					foreach (ModuleDef moduleDef in assemblyDef.Modules)
					{
						moduleDef.Dispose();
					}
				}
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x0016B544 File Offset: 0x0016B544
		public IEnumerable<AssemblyDef> GetCachedAssemblies()
		{
			this.theLock.EnterReadLock();
			AssemblyDef[] result;
			try
			{
				result = this.cachedAssemblies.Values.ToArray<AssemblyDef>();
			}
			finally
			{
				this.theLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x0016B590 File Offset: 0x0016B590
		private static string GetAssemblyNameKey(IAssembly asmName)
		{
			return asmName.FullNameToken;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x0016B598 File Offset: 0x0016B598
		private AssemblyDef Resolve2(IAssembly assembly, ModuleDef sourceModule)
		{
			AssemblyDef assemblyDef;
			if (this.cachedAssemblies.TryGetValue(AssemblyResolver.GetAssemblyNameKey(assembly), out assemblyDef))
			{
				return assemblyDef;
			}
			ModuleContext context = this.defaultModuleContext;
			if (context == null && sourceModule != null)
			{
				context = sourceModule.Context;
			}
			AssemblyDef assemblyDef2;
			if ((assemblyDef2 = this.FindExactAssembly(assembly, this.PreFindAssemblies(assembly, sourceModule, true), context)) == null)
			{
				assemblyDef2 = (this.FindExactAssembly(assembly, this.FindAssemblies(assembly, sourceModule, true), context) ?? this.FindExactAssembly(assembly, this.PostFindAssemblies(assembly, sourceModule, true), context));
			}
			assemblyDef = assemblyDef2;
			if (assemblyDef != null)
			{
				return assemblyDef;
			}
			if (!this.findExactMatch)
			{
				assemblyDef = this.FindClosestAssembly(assembly);
				assemblyDef = this.FindClosestAssembly(assembly, assemblyDef, this.PreFindAssemblies(assembly, sourceModule, false), context);
				assemblyDef = this.FindClosestAssembly(assembly, assemblyDef, this.FindAssemblies(assembly, sourceModule, false), context);
				assemblyDef = this.FindClosestAssembly(assembly, assemblyDef, this.PostFindAssemblies(assembly, sourceModule, false), context);
			}
			return assemblyDef;
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x0016B674 File Offset: 0x0016B674
		private AssemblyDef FindExactAssembly(IAssembly assembly, IEnumerable<string> paths, ModuleContext moduleContext)
		{
			if (paths == null)
			{
				return null;
			}
			AssemblyNameComparer compareAll = AssemblyNameComparer.CompareAll;
			foreach (string fileName in paths)
			{
				ModuleDefMD moduleDefMD = null;
				try
				{
					moduleDefMD = ModuleDefMD.Load(fileName, moduleContext);
					AssemblyDef assembly2 = moduleDefMD.Assembly;
					if (assembly2 != null && compareAll.Equals(assembly, assembly2))
					{
						moduleDefMD = null;
						return assembly2;
					}
				}
				catch
				{
				}
				finally
				{
					if (moduleDefMD != null)
					{
						moduleDefMD.Dispose();
					}
				}
			}
			return null;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x0016B734 File Offset: 0x0016B734
		private AssemblyDef FindClosestAssembly(IAssembly assembly)
		{
			AssemblyDef assemblyDef = null;
			AssemblyNameComparer compareAll = AssemblyNameComparer.CompareAll;
			foreach (KeyValuePair<string, AssemblyDef> keyValuePair in this.cachedAssemblies)
			{
				AssemblyDef value = keyValuePair.Value;
				if (value != null && compareAll.CompareClosest(assembly, assemblyDef, value) == 1)
				{
					assemblyDef = value;
				}
			}
			return assemblyDef;
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x0016B7B4 File Offset: 0x0016B7B4
		private AssemblyDef FindClosestAssembly(IAssembly assembly, AssemblyDef closest, IEnumerable<string> paths, ModuleContext moduleContext)
		{
			if (paths == null)
			{
				return closest;
			}
			AssemblyNameComparer compareAll = AssemblyNameComparer.CompareAll;
			foreach (string fileName in paths)
			{
				ModuleDefMD moduleDefMD = null;
				try
				{
					moduleDefMD = ModuleDefMD.Load(fileName, moduleContext);
					AssemblyDef assembly2 = moduleDefMD.Assembly;
					if (assembly2 != null && compareAll.CompareClosest(assembly, closest, assembly2) == 1)
					{
						if (!this.IsCached(closest) && closest != null)
						{
							ModuleDef manifestModule = closest.ManifestModule;
							if (manifestModule != null)
							{
								manifestModule.Dispose();
							}
						}
						closest = assembly2;
						moduleDefMD = null;
					}
				}
				catch
				{
				}
				finally
				{
					if (moduleDefMD != null)
					{
						moduleDefMD.Dispose();
					}
				}
			}
			return closest;
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x0016B898 File Offset: 0x0016B898
		private bool IsCached(AssemblyDef asm)
		{
			AssemblyDef assemblyDef;
			return asm != null && this.cachedAssemblies.TryGetValue(AssemblyResolver.GetAssemblyNameKey(asm), out assemblyDef) && assemblyDef == asm;
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x0016B8D0 File Offset: 0x0016B8D0
		private IEnumerable<string> FindAssemblies2(IAssembly assembly, IEnumerable<string> paths)
		{
			if (paths != null)
			{
				string asmSimpleName = UTF8String.ToSystemStringOrEmpty(assembly.Name);
				string[] array = assembly.IsContentTypeWindowsRuntime ? AssemblyResolver.winMDAssemblyExtensions : AssemblyResolver.assemblyExtensions;
				foreach (string ext in array)
				{
					foreach (string path in paths)
					{
						string text;
						try
						{
							text = Path.Combine(path, asmSimpleName + ext);
						}
						catch (ArgumentException)
						{
							yield break;
						}
						if (File.Exists(text))
						{
							yield return text;
						}
					}
					IEnumerator<string> enumerator = null;
					ext = null;
				}
				string[] array2 = null;
				asmSimpleName = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x0016B8E8 File Offset: 0x0016B8E8
		protected virtual IEnumerable<string> PreFindAssemblies(IAssembly assembly, ModuleDef sourceModule, bool matchExactly)
		{
			foreach (string text in this.FindAssemblies2(assembly, this.preSearchPaths))
			{
				yield return text;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x0016B900 File Offset: 0x0016B900
		protected virtual IEnumerable<string> PostFindAssemblies(IAssembly assembly, ModuleDef sourceModule, bool matchExactly)
		{
			foreach (string text in this.FindAssemblies2(assembly, this.postSearchPaths))
			{
				yield return text;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x0016B918 File Offset: 0x0016B918
		protected virtual IEnumerable<string> FindAssemblies(IAssembly assembly, ModuleDef sourceModule, bool matchExactly)
		{
			IEnumerator<string> enumerator;
			if (assembly.IsContentTypeWindowsRuntime)
			{
				string text;
				try
				{
					text = Path.Combine(Path.Combine(Environment.SystemDirectory, "WinMetadata"), assembly.Name + ".winmd");
				}
				catch (ArgumentException)
				{
					text = null;
				}
				if (File.Exists(text))
				{
					yield return text;
				}
			}
			else if (this.UseGAC)
			{
				foreach (string text2 in this.FindAssembliesGac(assembly, sourceModule, matchExactly))
				{
					yield return text2;
				}
				enumerator = null;
			}
			foreach (string text3 in this.FindAssembliesModuleSearchPaths(assembly, sourceModule, matchExactly))
			{
				yield return text3;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x0016B940 File Offset: 0x0016B940
		private IEnumerable<string> FindAssembliesGac(IAssembly assembly, ModuleDef sourceModule, bool matchExactly)
		{
			if (matchExactly)
			{
				return this.FindAssembliesGacExactly(assembly, sourceModule);
			}
			return this.FindAssembliesGacAny(assembly, sourceModule);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x0016B95C File Offset: 0x0016B95C
		private IEnumerable<AssemblyResolver.GacInfo> GetGacInfos(ModuleDef sourceModule)
		{
			int version = (sourceModule == null) ? int.MinValue : (sourceModule.IsClr40 ? 4 : 2);
			foreach (AssemblyResolver.GacInfo gacInfo in AssemblyResolver.gacInfos)
			{
				if (gacInfo.Version == version)
				{
					yield return gacInfo;
				}
			}
			List<AssemblyResolver.GacInfo>.Enumerator enumerator = default(List<AssemblyResolver.GacInfo>.Enumerator);
			foreach (AssemblyResolver.GacInfo gacInfo2 in AssemblyResolver.gacInfos)
			{
				if (gacInfo2.Version != version)
				{
					yield return gacInfo2;
				}
			}
			enumerator = default(List<AssemblyResolver.GacInfo>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x0016B96C File Offset: 0x0016B96C
		private IEnumerable<string> FindAssembliesGacExactly(IAssembly assembly, ModuleDef sourceModule)
		{
			foreach (AssemblyResolver.GacInfo gacInfo in this.GetGacInfos(sourceModule))
			{
				foreach (string text in this.FindAssembliesGacExactly(gacInfo, assembly, sourceModule))
				{
					yield return text;
				}
				IEnumerator<string> enumerator2 = null;
			}
			IEnumerator<AssemblyResolver.GacInfo> enumerator = null;
			if (AssemblyResolver.extraMonoPaths != null)
			{
				foreach (string text2 in AssemblyResolver.GetExtraMonoPaths(assembly, sourceModule))
				{
					yield return text2;
				}
				IEnumerator<string> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x0016B98C File Offset: 0x0016B98C
		private static IEnumerable<string> GetExtraMonoPaths(IAssembly assembly, ModuleDef sourceModule)
		{
			if (AssemblyResolver.extraMonoPaths != null)
			{
				foreach (string path in AssemblyResolver.extraMonoPaths)
				{
					string text;
					try
					{
						text = Path.Combine(path, assembly.Name + ".dll");
					}
					catch (ArgumentException)
					{
						break;
					}
					if (File.Exists(text))
					{
						yield return text;
					}
				}
				string[] array = null;
			}
			yield break;
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x0016B99C File Offset: 0x0016B99C
		private IEnumerable<string> FindAssembliesGacExactly(AssemblyResolver.GacInfo gacInfo, IAssembly assembly, ModuleDef sourceModule)
		{
			PublicKeyToken publicKeyToken = PublicKeyBase.ToPublicKeyToken(assembly.PublicKeyOrToken);
			if (gacInfo != null && publicKeyToken != null)
			{
				string pktString = publicKeyToken.ToString();
				string verString = Utils.CreateVersionWithNoUndefinedValues(assembly.Version).ToString();
				string cultureString = UTF8String.ToSystemStringOrEmpty(assembly.Culture);
				if (cultureString.Equals("neutral", StringComparison.OrdinalIgnoreCase))
				{
					cultureString = string.Empty;
				}
				string asmSimpleName = UTF8String.ToSystemStringOrEmpty(assembly.Name);
				foreach (string path in gacInfo.SubDirs)
				{
					string path2 = Path.Combine(gacInfo.Path, path);
					try
					{
						path2 = Path.Combine(path2, asmSimpleName);
					}
					catch (ArgumentException)
					{
						break;
					}
					path2 = Path.Combine(path2, string.Concat(new string[]
					{
						gacInfo.Prefix,
						verString,
						"_",
						cultureString,
						"_",
						pktString
					}));
					string text = Path.Combine(path2, asmSimpleName + ".dll");
					if (File.Exists(text))
					{
						yield return text;
					}
				}
				string[] array = null;
				pktString = null;
				verString = null;
				cultureString = null;
				asmSimpleName = null;
			}
			yield break;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x0016B9B4 File Offset: 0x0016B9B4
		private IEnumerable<string> FindAssembliesGacAny(IAssembly assembly, ModuleDef sourceModule)
		{
			foreach (AssemblyResolver.GacInfo gacInfo in this.GetGacInfos(sourceModule))
			{
				foreach (string text in this.FindAssembliesGacAny(gacInfo, assembly, sourceModule))
				{
					yield return text;
				}
				IEnumerator<string> enumerator2 = null;
			}
			IEnumerator<AssemblyResolver.GacInfo> enumerator = null;
			if (AssemblyResolver.extraMonoPaths != null)
			{
				foreach (string text2 in AssemblyResolver.GetExtraMonoPaths(assembly, sourceModule))
				{
					yield return text2;
				}
				IEnumerator<string> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x0016B9D4 File Offset: 0x0016B9D4
		private IEnumerable<string> FindAssembliesGacAny(AssemblyResolver.GacInfo gacInfo, IAssembly assembly, ModuleDef sourceModule)
		{
			if (gacInfo != null)
			{
				string asmSimpleName = UTF8String.ToSystemStringOrEmpty(assembly.Name);
				foreach (string path in gacInfo.SubDirs)
				{
					string text = Path.Combine(gacInfo.Path, path);
					try
					{
						text = Path.Combine(text, asmSimpleName);
					}
					catch (ArgumentException)
					{
						break;
					}
					foreach (string path2 in this.GetDirs(text))
					{
						string text2 = Path.Combine(path2, asmSimpleName + ".dll");
						if (File.Exists(text2))
						{
							yield return text2;
						}
					}
					IEnumerator<string> enumerator = null;
				}
				string[] array = null;
				asmSimpleName = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0016B9F4 File Offset: 0x0016B9F4
		private IEnumerable<string> GetDirs(string baseDir)
		{
			if (!Directory.Exists(baseDir))
			{
				return Array2.Empty<string>();
			}
			List<string> list = new List<string>();
			try
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(baseDir).GetDirectories())
				{
					list.Add(directoryInfo.FullName);
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0016BA68 File Offset: 0x0016BA68
		private IEnumerable<string> FindAssembliesModuleSearchPaths(IAssembly assembly, ModuleDef sourceModule, bool matchExactly)
		{
			string asmSimpleName = UTF8String.ToSystemStringOrEmpty(assembly.Name);
			IEnumerable<string> searchPaths = this.GetSearchPaths(sourceModule);
			string[] array = assembly.IsContentTypeWindowsRuntime ? AssemblyResolver.winMDAssemblyExtensions : AssemblyResolver.assemblyExtensions;
			foreach (string ext in array)
			{
				foreach (string path in searchPaths)
				{
					int num;
					for (int i = 0; i < 2; i = num + 1)
					{
						string text;
						try
						{
							if (i == 0)
							{
								text = Path.Combine(path, asmSimpleName + ext);
							}
							else
							{
								text = Path.Combine(Path.Combine(path, asmSimpleName), asmSimpleName + ext);
							}
						}
						catch (ArgumentException)
						{
							yield break;
						}
						if (File.Exists(text))
						{
							yield return text;
						}
						num = i;
					}
					path = null;
				}
				IEnumerator<string> enumerator = null;
				ext = null;
			}
			string[] array2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x0016BA88 File Offset: 0x0016BA88
		private IEnumerable<string> GetSearchPaths(ModuleDef module)
		{
			ModuleDef moduleDef = module;
			if (moduleDef == null)
			{
				moduleDef = AssemblyResolver.nullModule;
			}
			List<string> result;
			if (this.moduleSearchPaths.TryGetValue(moduleDef, out result))
			{
				return result;
			}
			result = (this.moduleSearchPaths[moduleDef] = new List<string>(this.GetModuleSearchPaths(module)));
			return result;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x0016BAD8 File Offset: 0x0016BAD8
		protected virtual IEnumerable<string> GetModuleSearchPaths(ModuleDef module)
		{
			return this.GetModulePrivateSearchPaths(module);
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x0016BAE4 File Offset: 0x0016BAE4
		protected IEnumerable<string> GetModulePrivateSearchPaths(ModuleDef module)
		{
			if (module == null)
			{
				return Array2.Empty<string>();
			}
			AssemblyDef assembly = module.Assembly;
			if (assembly == null)
			{
				return Array2.Empty<string>();
			}
			module = assembly.ManifestModule;
			if (module == null)
			{
				return Array2.Empty<string>();
			}
			string text = null;
			try
			{
				string location = module.Location;
				if (location != string.Empty)
				{
					text = Directory.GetParent(location).FullName;
					string text2 = location + ".config";
					if (File.Exists(text2))
					{
						return this.GetPrivatePaths(text, text2);
					}
				}
			}
			catch
			{
			}
			if (text != null)
			{
				return new List<string>
				{
					text
				};
			}
			return Array2.Empty<string>();
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x0016BBA8 File Offset: 0x0016BBA8
		private IEnumerable<string> GetPrivatePaths(string baseDir, string configFileName)
		{
			List<string> list = new List<string>();
			try
			{
				string directoryName = Path.GetDirectoryName(Path.GetFullPath(configFileName));
				list.Add(directoryName);
				using (FileStream fileStream = new FileStream(configFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(XmlReader.Create(fileStream));
					foreach (object obj in xmlDocument.GetElementsByTagName("probing"))
					{
						XmlElement xmlElement = obj as XmlElement;
						if (xmlElement != null)
						{
							string attribute = xmlElement.GetAttribute("privatePath");
							if (!string.IsNullOrEmpty(attribute))
							{
								string[] array = attribute.Split(new char[]
								{
									';'
								});
								for (int i = 0; i < array.Length; i++)
								{
									string text = array[i].Trim();
									if (!(text == ""))
									{
										string fullPath = Path.GetFullPath(Path.Combine(directoryName, text.Replace('\\', Path.DirectorySeparatorChar)));
										if (Directory.Exists(fullPath) && fullPath.StartsWith(baseDir + Path.DirectorySeparatorChar.ToString()))
										{
											list.Add(fullPath);
										}
									}
								}
							}
						}
					}
				}
			}
			catch (ArgumentException)
			{
			}
			catch (IOException)
			{
			}
			catch (XmlException)
			{
			}
			return list;
		}

		// Token: 0x040023F4 RID: 9204
		private static readonly ModuleDef nullModule = new ModuleDefUser();

		// Token: 0x040023F5 RID: 9205
		private static readonly string[] assemblyExtensions = new string[]
		{
			".dll",
			".exe"
		};

		// Token: 0x040023F6 RID: 9206
		private static readonly string[] winMDAssemblyExtensions = new string[]
		{
			".winmd"
		};

		// Token: 0x040023F7 RID: 9207
		private static readonly List<AssemblyResolver.GacInfo> gacInfos;

		// Token: 0x040023F8 RID: 9208
		private static readonly string[] extraMonoPaths;

		// Token: 0x040023F9 RID: 9209
		private static readonly string[] monoVerDirs = new string[]
		{
			"4.5",
			"4.5\\Facades",
			"4.5-api",
			"4.5-api\\Facades",
			"4.0",
			"4.0-api",
			"3.5",
			"3.5-api",
			"3.0",
			"3.0-api",
			"2.0",
			"2.0-api",
			"1.1",
			"1.0"
		};

		// Token: 0x040023FA RID: 9210
		private ModuleContext defaultModuleContext;

		// Token: 0x040023FB RID: 9211
		private readonly Dictionary<ModuleDef, List<string>> moduleSearchPaths = new Dictionary<ModuleDef, List<string>>();

		// Token: 0x040023FC RID: 9212
		private readonly Dictionary<string, AssemblyDef> cachedAssemblies = new Dictionary<string, AssemblyDef>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040023FD RID: 9213
		private readonly List<string> preSearchPaths = new List<string>();

		// Token: 0x040023FE RID: 9214
		private readonly List<string> postSearchPaths = new List<string>();

		// Token: 0x040023FF RID: 9215
		private bool findExactMatch;

		// Token: 0x04002400 RID: 9216
		private bool enableFrameworkRedirect;

		// Token: 0x04002401 RID: 9217
		private bool enableTypeDefCache = true;

		// Token: 0x04002402 RID: 9218
		private bool useGac = true;

		// Token: 0x04002403 RID: 9219
		private readonly Lock theLock = Lock.Create();

		// Token: 0x02000FCD RID: 4045
		private sealed class GacInfo
		{
			// Token: 0x06008DC3 RID: 36291 RVA: 0x002A76F8 File Offset: 0x002A76F8
			public GacInfo(int version, string prefix, string path, string[] subDirs)
			{
				this.Version = version;
				this.Prefix = prefix;
				this.Path = path;
				this.SubDirs = subDirs;
			}

			// Token: 0x0400431E RID: 17182
			public readonly int Version;

			// Token: 0x0400431F RID: 17183
			public readonly string Path;

			// Token: 0x04004320 RID: 17184
			public readonly string Prefix;

			// Token: 0x04004321 RID: 17185
			public readonly string[] SubDirs;
		}
	}
}
