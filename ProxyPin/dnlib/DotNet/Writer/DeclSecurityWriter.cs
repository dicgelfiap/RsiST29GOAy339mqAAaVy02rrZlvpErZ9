using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace dnlib.DotNet.Writer
{
	// Token: 0x0200089A RID: 2202
	[ComVisible(true)]
	public readonly struct DeclSecurityWriter : ICustomAttributeWriterHelper, IWriterError, IFullNameFactoryHelper
	{
		// Token: 0x06005450 RID: 21584 RVA: 0x0019B5A0 File Offset: 0x0019B5A0
		public static byte[] Write(ModuleDef module, IList<SecurityAttribute> secAttrs, IWriterError helper)
		{
			return DeclSecurityWriter.Write(module, secAttrs, helper, false);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0019B5AC File Offset: 0x0019B5AC
		public static byte[] Write(ModuleDef module, IList<SecurityAttribute> secAttrs, IWriterError helper, bool optimizeCustomAttributeSerializedTypeNames)
		{
			return new DeclSecurityWriter(module, helper, optimizeCustomAttributeSerializedTypeNames, null).Write(secAttrs);
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0019B5D0 File Offset: 0x0019B5D0
		internal static byte[] Write(ModuleDef module, IList<SecurityAttribute> secAttrs, IWriterError helper, bool optimizeCustomAttributeSerializedTypeNames, DataWriterContext context)
		{
			return new DeclSecurityWriter(module, helper, optimizeCustomAttributeSerializedTypeNames, context).Write(secAttrs);
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0019B5F4 File Offset: 0x0019B5F4
		private DeclSecurityWriter(ModuleDef module, IWriterError helper, bool optimizeCustomAttributeSerializedTypeNames, DataWriterContext context)
		{
			this.module = module;
			this.helper = helper;
			this.context = context;
			this.optimizeCustomAttributeSerializedTypeNames = optimizeCustomAttributeSerializedTypeNames;
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0019B614 File Offset: 0x0019B614
		private byte[] Write(IList<SecurityAttribute> secAttrs)
		{
			if (secAttrs == null)
			{
				secAttrs = Array2.Empty<SecurityAttribute>();
			}
			string net1xXmlStringInternal = DeclSecurity.GetNet1xXmlStringInternal(secAttrs);
			if (net1xXmlStringInternal != null)
			{
				return this.WriteFormat1(net1xXmlStringInternal);
			}
			return this.WriteFormat2(secAttrs);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0019B650 File Offset: 0x0019B650
		private byte[] WriteFormat1(string xml)
		{
			return Encoding.Unicode.GetBytes(xml);
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0019B660 File Offset: 0x0019B660
		private byte[] WriteFormat2(IList<SecurityAttribute> secAttrs)
		{
			MemoryStream memoryStream = new MemoryStream();
			DataWriter dataWriter = new DataWriter(memoryStream);
			dataWriter.WriteByte(46);
			this.WriteCompressedUInt32(dataWriter, (uint)secAttrs.Count);
			int count = secAttrs.Count;
			for (int i = 0; i < count; i++)
			{
				SecurityAttribute securityAttribute = secAttrs[i];
				if (securityAttribute == null)
				{
					this.helper.Error("SecurityAttribute is null");
					this.Write(dataWriter, UTF8String.Empty);
					this.WriteCompressedUInt32(dataWriter, 1U);
					this.WriteCompressedUInt32(dataWriter, 0U);
				}
				else
				{
					ITypeDefOrRef attributeType = securityAttribute.AttributeType;
					string s;
					if (attributeType == null)
					{
						this.helper.Error("SecurityAttribute attribute type is null");
						s = string.Empty;
					}
					else
					{
						s = attributeType.AssemblyQualifiedName;
					}
					this.Write(dataWriter, s);
					byte[] array = (this.context == null) ? CustomAttributeWriter.Write(this, securityAttribute.NamedArguments) : CustomAttributeWriter.Write(this, securityAttribute.NamedArguments, this.context);
					if (array.Length > 536870911)
					{
						this.helper.Error("Named arguments blob size doesn't fit in 29 bits");
						array = Array2.Empty<byte>();
					}
					this.WriteCompressedUInt32(dataWriter, (uint)array.Length);
					dataWriter.WriteBytes(array);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0019B7B8 File Offset: 0x0019B7B8
		private uint WriteCompressedUInt32(DataWriter writer, uint value)
		{
			return writer.WriteCompressedUInt32(this.helper, value);
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0019B7C8 File Offset: 0x0019B7C8
		private void Write(DataWriter writer, UTF8String s)
		{
			writer.Write(this.helper, s);
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0019B7D8 File Offset: 0x0019B7D8
		void IWriterError.Error(string message)
		{
			this.helper.Error(message);
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0019B7E8 File Offset: 0x0019B7E8
		bool IFullNameFactoryHelper.MustUseAssemblyName(IType type)
		{
			return FullNameFactory.MustUseAssemblyName(this.module, type, this.optimizeCustomAttributeSerializedTypeNames);
		}

		// Token: 0x04002878 RID: 10360
		private readonly ModuleDef module;

		// Token: 0x04002879 RID: 10361
		private readonly IWriterError helper;

		// Token: 0x0400287A RID: 10362
		private readonly DataWriterContext context;

		// Token: 0x0400287B RID: 10363
		private readonly bool optimizeCustomAttributeSerializedTypeNames;
	}
}
