using System;
using System.Runtime.InteropServices;
using dnlib.PE;
using dnlib.Utils;

namespace dnlib.DotNet
{
	// Token: 0x0200081D RID: 2077
	[ComVisible(true)]
	public class ModuleDefUser : ModuleDef
	{
		// Token: 0x06004C79 RID: 19577 RVA: 0x0017FEDC File Offset: 0x0017FEDC
		public ModuleDefUser() : this(null, null)
		{
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0017FF00 File Offset: 0x0017FF00
		public ModuleDefUser(UTF8String name) : this(name, new Guid?(Guid.NewGuid()))
		{
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0017FF14 File Offset: 0x0017FF14
		public ModuleDefUser(UTF8String name, Guid? mvid) : this(name, mvid, null)
		{
		}

		// Token: 0x06004C7C RID: 19580 RVA: 0x0017FF20 File Offset: 0x0017FF20
		public ModuleDefUser(UTF8String name, Guid? mvid, AssemblyRef corLibAssemblyRef)
		{
			base.Kind = ModuleKind.Windows;
			base.Characteristics = (Characteristics.ExecutableImage | Characteristics.Bit32Machine);
			base.DllCharacteristics = (DllCharacteristics.DynamicBase | DllCharacteristics.NxCompat | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware);
			base.RuntimeVersion = "v2.0.50727";
			base.Machine = Machine.I386;
			this.cor20HeaderFlags = 1;
			base.Cor20HeaderRuntimeVersion = new uint?(131077U);
			base.TablesHeaderVersion = new ushort?((ushort)512);
			this.types = new LazyList<TypeDef>(this);
			this.exportedTypes = new LazyList<ExportedType>();
			this.resources = new ResourceCollection();
			this.corLibTypes = new CorLibTypes(this, corLibAssemblyRef);
			this.types = new LazyList<TypeDef>(this);
			this.name = name;
			this.mvid = mvid;
			this.types.Add(this.CreateModuleType());
			base.UpdateRowId<ModuleDefUser>(this);
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0017FFF4 File Offset: 0x0017FFF4
		private TypeDef CreateModuleType()
		{
			TypeDefUser typeDefUser = base.UpdateRowId<TypeDefUser>(new TypeDefUser(UTF8String.Empty, "<Module>", null));
			typeDefUser.Attributes = TypeAttributes.NotPublic;
			return typeDefUser;
		}
	}
}
