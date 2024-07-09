using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet
{
	// Token: 0x020007A1 RID: 1953
	[ComVisible(true)]
	public struct DeclSecurityReader
	{
		// Token: 0x060045DF RID: 17887 RVA: 0x0016F178 File Offset: 0x0016F178
		public static IList<SecurityAttribute> Read(ModuleDefMD module, uint sig)
		{
			return DeclSecurityReader.Read(module, module.BlobStream.CreateReader(sig), default(GenericParamContext));
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x0016F1A4 File Offset: 0x0016F1A4
		public static IList<SecurityAttribute> Read(ModuleDefMD module, uint sig, GenericParamContext gpContext)
		{
			return DeclSecurityReader.Read(module, module.BlobStream.CreateReader(sig), gpContext);
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x0016F1BC File Offset: 0x0016F1BC
		public static IList<SecurityAttribute> Read(ModuleDef module, byte[] blob)
		{
			return DeclSecurityReader.Read(module, ByteArrayDataReaderFactory.CreateReader(blob), default(GenericParamContext));
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x0016F1E4 File Offset: 0x0016F1E4
		public static IList<SecurityAttribute> Read(ModuleDef module, byte[] blob, GenericParamContext gpContext)
		{
			return DeclSecurityReader.Read(module, ByteArrayDataReaderFactory.CreateReader(blob), gpContext);
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x0016F1F4 File Offset: 0x0016F1F4
		public static IList<SecurityAttribute> Read(ModuleDef module, DataReader signature)
		{
			return DeclSecurityReader.Read(module, signature, default(GenericParamContext));
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x0016F218 File Offset: 0x0016F218
		public static IList<SecurityAttribute> Read(ModuleDef module, DataReader signature, GenericParamContext gpContext)
		{
			DeclSecurityReader declSecurityReader = new DeclSecurityReader(module, signature, gpContext);
			return declSecurityReader.Read();
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x0016F23C File Offset: 0x0016F23C
		private DeclSecurityReader(ModuleDef module, DataReader reader, GenericParamContext gpContext)
		{
			this.reader = reader;
			this.module = module;
			this.gpContext = gpContext;
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x0016F254 File Offset: 0x0016F254
		private IList<SecurityAttribute> Read()
		{
			IList<SecurityAttribute> result;
			try
			{
				if (this.reader.Position >= this.reader.Length)
				{
					result = new List<SecurityAttribute>();
				}
				else if (this.reader.ReadByte() == 46)
				{
					result = this.ReadBinaryFormat();
				}
				else
				{
					uint position = this.reader.Position;
					this.reader.Position = position - 1U;
					result = this.ReadXmlFormat();
				}
			}
			catch
			{
				result = new List<SecurityAttribute>();
			}
			return result;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x0016F2E4 File Offset: 0x0016F2E4
		private IList<SecurityAttribute> ReadBinaryFormat()
		{
			int num = (int)this.reader.ReadCompressedUInt32();
			List<SecurityAttribute> list = new List<SecurityAttribute>(num);
			for (int i = 0; i < num; i++)
			{
				UTF8String utf = this.ReadUTF8String();
				ITypeDefOrRef attrType = TypeNameParser.ParseReflection(this.module, UTF8String.ToSystemStringOrEmpty(utf), new CAAssemblyRefFinder(this.module), this.gpContext);
				this.reader.ReadCompressedUInt32();
				int numNamedArgs = (int)this.reader.ReadCompressedUInt32();
				List<CANamedArgument> list2 = CustomAttributeReader.ReadNamedArguments(this.module, ref this.reader, numNamedArgs, this.gpContext);
				if (list2 == null)
				{
					throw new ApplicationException("Could not read named arguments");
				}
				list.Add(new SecurityAttribute(attrType, list2));
			}
			return list;
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x0016F39C File Offset: 0x0016F39C
		private IList<SecurityAttribute> ReadXmlFormat()
		{
			string xml = this.reader.ReadUtf16String((int)(this.reader.Length / 2U));
			SecurityAttribute item = SecurityAttribute.CreateFromXml(this.module, xml);
			return new List<SecurityAttribute>
			{
				item
			};
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0016F3E0 File Offset: 0x0016F3E0
		private UTF8String ReadUTF8String()
		{
			uint num = this.reader.ReadCompressedUInt32();
			if (num != 0U)
			{
				return new UTF8String(this.reader.ReadBytes((int)num));
			}
			return UTF8String.Empty;
		}

		// Token: 0x0400245E RID: 9310
		private DataReader reader;

		// Token: 0x0400245F RID: 9311
		private readonly ModuleDef module;

		// Token: 0x04002460 RID: 9312
		private readonly GenericParamContext gpContext;
	}
}
