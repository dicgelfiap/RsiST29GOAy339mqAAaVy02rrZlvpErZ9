using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using dnlib.IO;
using dnlib.PE;

namespace dnlib.DotNet
{
	// Token: 0x02000887 RID: 2183
	[DebuggerDisplay("RVA = {RVA}, Count = {VTables.Count}")]
	[ComVisible(true)]
	public sealed class VTableFixups : IEnumerable<VTable>, IEnumerable
	{
		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x001977FC File Offset: 0x001977FC
		// (set) Token: 0x060053A2 RID: 21410 RVA: 0x00197804 File Offset: 0x00197804
		public RVA RVA
		{
			get
			{
				return this.rva;
			}
			set
			{
				this.rva = value;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x00197810 File Offset: 0x00197810
		public IList<VTable> VTables
		{
			get
			{
				return this.vtables;
			}
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x00197818 File Offset: 0x00197818
		public VTableFixups()
		{
			this.vtables = new List<VTable>();
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0019782C File Offset: 0x0019782C
		public VTableFixups(ModuleDefMD module)
		{
			this.Initialize(module);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x0019783C File Offset: 0x0019783C
		private void Initialize(ModuleDefMD module)
		{
			ImageDataDirectory vtableFixups = module.Metadata.ImageCor20Header.VTableFixups;
			if (vtableFixups.VirtualAddress == (RVA)0U || vtableFixups.Size == 0U)
			{
				this.vtables = new List<VTable>();
				return;
			}
			this.rva = vtableFixups.VirtualAddress;
			this.vtables = new List<VTable>((int)(vtableFixups.Size / 8U));
			IPEImage peimage = module.Metadata.PEImage;
			DataReader dataReader = peimage.CreateReader();
			dataReader.Position = (uint)peimage.ToFileOffset(vtableFixups.VirtualAddress);
			ulong num = (ulong)dataReader.Position + (ulong)vtableFixups.Size;
			while ((ulong)dataReader.Position + 8UL <= num && dataReader.CanRead(8U))
			{
				RVA rva = (RVA)dataReader.ReadUInt32();
				int numSlots = (int)dataReader.ReadUInt16();
				VTableFlags flags = (VTableFlags)dataReader.ReadUInt16();
				VTable vtable = new VTable(rva, flags, numSlots);
				this.vtables.Add(vtable);
				uint position = dataReader.Position;
				dataReader.Position = (uint)peimage.ToFileOffset(rva);
				uint num2 = vtable.Is64Bit ? 8U : 4U;
				while (numSlots-- > 0 && dataReader.CanRead(num2))
				{
					vtable.Methods.Add(module.ResolveToken(dataReader.ReadUInt32()) as IMethod);
					if (num2 == 8U)
					{
						dataReader.ReadUInt32();
					}
				}
				dataReader.Position = position;
			}
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x001979B0 File Offset: 0x001979B0
		public IEnumerator<VTable> GetEnumerator()
		{
			return this.vtables.GetEnumerator();
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x001979C0 File Offset: 0x001979C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002821 RID: 10273
		private RVA rva;

		// Token: 0x04002822 RID: 10274
		private IList<VTable> vtables;
	}
}
