using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using dnlib.DotNet.Pdb.Symbols;
using dnlib.IO;

namespace dnlib.DotNet.Pdb.Managed
{
	// Token: 0x0200094C RID: 2380
	internal sealed class DbiScope : SymbolScope
	{
		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06005B7D RID: 23421 RVA: 0x001BE6A0 File Offset: 0x001BE6A0
		public override SymbolMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x001BE6A8 File Offset: 0x001BE6A8
		public override SymbolScope Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06005B7F RID: 23423 RVA: 0x001BE6B0 File Offset: 0x001BE6B0
		public override int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06005B80 RID: 23424 RVA: 0x001BE6B8 File Offset: 0x001BE6B8
		public override int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06005B81 RID: 23425 RVA: 0x001BE6C0 File Offset: 0x001BE6C0
		public override IList<SymbolScope> Children
		{
			get
			{
				return this.childrenList;
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06005B82 RID: 23426 RVA: 0x001BE6C8 File Offset: 0x001BE6C8
		public override IList<SymbolVariable> Locals
		{
			get
			{
				return this.localsList;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06005B83 RID: 23427 RVA: 0x001BE6D0 File Offset: 0x001BE6D0
		public override IList<SymbolNamespace> Namespaces
		{
			get
			{
				return this.namespacesList;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06005B84 RID: 23428 RVA: 0x001BE6D8 File Offset: 0x001BE6D8
		public override IList<PdbCustomDebugInfo> CustomDebugInfos
		{
			get
			{
				return Array2.Empty<PdbCustomDebugInfo>();
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06005B85 RID: 23429 RVA: 0x001BE6E0 File Offset: 0x001BE6E0
		public override PdbImportScope ImportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x001BE6E4 File Offset: 0x001BE6E4
		public DbiScope(SymbolMethod method, SymbolScope parent, string name, uint offset, uint length)
		{
			this.method = method;
			this.parent = parent;
			this.Name = name;
			this.startOffset = (int)offset;
			this.endOffset = (int)(offset + length);
			this.childrenList = new List<SymbolScope>();
			this.localsList = new List<SymbolVariable>();
			this.namespacesList = new List<SymbolNamespace>();
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06005B87 RID: 23431 RVA: 0x001BE744 File Offset: 0x001BE744
		// (set) Token: 0x06005B88 RID: 23432 RVA: 0x001BE74C File Offset: 0x001BE74C
		public string Name { get; private set; }

		// Token: 0x06005B89 RID: 23433 RVA: 0x001BE758 File Offset: 0x001BE758
		public void Read(RecursionCounter counter, ref DataReader reader, uint scopeEnd)
		{
			if (!counter.Increment())
			{
				throw new PdbException("Scopes too deep");
			}
			while (reader.Position < scopeEnd)
			{
				ushort num = reader.ReadUInt16();
				uint num2 = reader.Position + (uint)num;
				SymbolType symbolType = (SymbolType)reader.ReadUInt16();
				DbiScope dbiScope = null;
				uint? num3 = null;
				if (symbolType <= SymbolType.S_BLOCK32)
				{
					if (symbolType != SymbolType.S_END)
					{
						if (symbolType != SymbolType.S_OEM)
						{
							if (symbolType == SymbolType.S_BLOCK32)
							{
								reader.Position += 4U;
								num3 = new uint?(reader.ReadUInt32());
								uint length = reader.ReadUInt32();
								PdbAddress pdbAddress = PdbAddress.ReadAddress(ref reader);
								string text = PdbReader.ReadCString(ref reader);
								dbiScope = new DbiScope(this.method, this, text, pdbAddress.Offset, length);
							}
						}
						else if ((ulong)reader.Position + 20UL <= (ulong)num2 && DbiScope.ReadAndCompareBytes(ref reader, num2, DbiScope.dotNetOemGuid))
						{
							reader.Position += 4U;
							string text = DbiScope.ReadUnicodeString(ref reader, num2);
							if (text != null)
							{
								byte[] data = reader.ReadBytes((int)(num2 - reader.Position));
								if (this.oemInfos == null)
								{
									this.oemInfos = new List<DbiScope.OemInfo>(1);
								}
								this.oemInfos.Add(new DbiScope.OemInfo(text, data));
							}
						}
					}
				}
				else if (symbolType != SymbolType.S_MANSLOT)
				{
					if (symbolType != SymbolType.S_UNAMESPACE)
					{
						if (symbolType == SymbolType.S_MANCONSTANT)
						{
							uint signatureToken = reader.ReadUInt32();
							object value;
							if (NumericReader.TryReadNumeric(ref reader, (ulong)num2, out value))
							{
								string text = PdbReader.ReadCString(ref reader);
								if (this.constants == null)
								{
									this.constants = new List<DbiScope.ConstantInfo>();
								}
								this.constants.Add(new DbiScope.ConstantInfo(text, signatureToken, value));
							}
						}
					}
					else
					{
						this.namespacesList.Add(new DbiNamespace(PdbReader.ReadCString(ref reader)));
					}
				}
				else
				{
					DbiVariable dbiVariable = new DbiVariable();
					if (dbiVariable.Read(ref reader))
					{
						this.localsList.Add(dbiVariable);
					}
				}
				reader.Position = num2;
				if (dbiScope != null)
				{
					dbiScope.Read(counter, ref reader, num3.Value);
					this.childrenList.Add(dbiScope);
				}
			}
			counter.Decrement();
			if (reader.Position != scopeEnd)
			{
				Debugger.Break();
			}
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x001BE9A0 File Offset: 0x001BE9A0
		private static string ReadUnicodeString(ref DataReader reader, uint end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while ((ulong)reader.Position + 2UL <= (ulong)end)
			{
				char c = reader.ReadChar();
				if (c == '\0')
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append(c);
			}
			return null;
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x001BE9E8 File Offset: 0x001BE9E8
		private static bool ReadAndCompareBytes(ref DataReader reader, uint end, byte[] bytes)
		{
			if ((ulong)reader.Position + (ulong)bytes.Length > (ulong)end)
			{
				return false;
			}
			for (int i = 0; i < bytes.Length; i++)
			{
				if (reader.ReadByte() != bytes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x001BEA30 File Offset: 0x001BEA30
		public override IList<PdbConstant> GetConstants(ModuleDef module, GenericParamContext gpContext)
		{
			if (this.constants == null)
			{
				return Array2.Empty<PdbConstant>();
			}
			PdbConstant[] array = new PdbConstant[this.constants.Count];
			for (int i = 0; i < array.Length; i++)
			{
				DbiScope.ConstantInfo constantInfo = this.constants[i];
				StandAloneSig standAloneSig = module.ResolveToken(constantInfo.SignatureToken, gpContext) as StandAloneSig;
				FieldSig fieldSig = (standAloneSig == null) ? null : (standAloneSig.Signature as FieldSig);
				TypeSig type;
				if (fieldSig == null)
				{
					type = null;
				}
				else
				{
					type = fieldSig.Type;
				}
				array[i] = new PdbConstant(constantInfo.Name, type, constantInfo.Value);
			}
			return array;
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x001BEAE0 File Offset: 0x001BEAE0
		internal byte[] GetSymAttribute(string name)
		{
			if (this.oemInfos == null)
			{
				return null;
			}
			foreach (DbiScope.OemInfo oemInfo in this.oemInfos)
			{
				if (oemInfo.Name == name)
				{
					return oemInfo.Data;
				}
			}
			return null;
		}

		// Token: 0x04002C3F RID: 11327
		private readonly SymbolMethod method;

		// Token: 0x04002C40 RID: 11328
		private readonly SymbolScope parent;

		// Token: 0x04002C41 RID: 11329
		internal int startOffset;

		// Token: 0x04002C42 RID: 11330
		internal int endOffset;

		// Token: 0x04002C43 RID: 11331
		private readonly List<SymbolScope> childrenList;

		// Token: 0x04002C44 RID: 11332
		private readonly List<SymbolVariable> localsList;

		// Token: 0x04002C45 RID: 11333
		private readonly List<SymbolNamespace> namespacesList;

		// Token: 0x04002C47 RID: 11335
		private List<DbiScope.OemInfo> oemInfos;

		// Token: 0x04002C48 RID: 11336
		private List<DbiScope.ConstantInfo> constants;

		// Token: 0x04002C49 RID: 11337
		private static readonly byte[] dotNetOemGuid = new byte[]
		{
			201,
			63,
			234,
			198,
			179,
			89,
			214,
			73,
			188,
			37,
			9,
			2,
			187,
			171,
			180,
			96
		};

		// Token: 0x02001036 RID: 4150
		private readonly struct ConstantInfo
		{
			// Token: 0x06008FC0 RID: 36800 RVA: 0x002AD400 File Offset: 0x002AD400
			public ConstantInfo(string name, uint signatureToken, object value)
			{
				this.Name = name;
				this.SignatureToken = signatureToken;
				this.Value = value;
			}

			// Token: 0x0400450C RID: 17676
			public readonly string Name;

			// Token: 0x0400450D RID: 17677
			public readonly uint SignatureToken;

			// Token: 0x0400450E RID: 17678
			public readonly object Value;
		}

		// Token: 0x02001037 RID: 4151
		internal readonly struct OemInfo
		{
			// Token: 0x06008FC1 RID: 36801 RVA: 0x002AD418 File Offset: 0x002AD418
			public OemInfo(string name, byte[] data)
			{
				this.Name = name;
				this.Data = data;
			}

			// Token: 0x06008FC2 RID: 36802 RVA: 0x002AD428 File Offset: 0x002AD428
			public override string ToString()
			{
				return this.Name + " = (" + this.Data.Length.ToString() + " bytes)";
			}

			// Token: 0x0400450F RID: 17679
			public readonly string Name;

			// Token: 0x04004510 RID: 17680
			public readonly byte[] Data;
		}
	}
}
