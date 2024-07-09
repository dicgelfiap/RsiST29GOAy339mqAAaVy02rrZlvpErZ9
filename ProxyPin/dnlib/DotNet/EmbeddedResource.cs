using System;
using System.Runtime.InteropServices;
using dnlib.IO;

namespace dnlib.DotNet
{
	// Token: 0x0200083E RID: 2110
	[ComVisible(true)]
	public sealed class EmbeddedResource : Resource
	{
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x001872A0 File Offset: 0x001872A0
		public uint Length
		{
			get
			{
				return this.resourceLength;
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x001872A8 File Offset: 0x001872A8
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Embedded;
			}
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x001872AC File Offset: 0x001872AC
		public EmbeddedResource(UTF8String name, byte[] data, ManifestResourceAttributes flags = ManifestResourceAttributes.Private) : this(name, ByteArrayDataReaderFactory.Create(data, null), 0U, (uint)data.Length, flags)
		{
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x001872C4 File Offset: 0x001872C4
		public EmbeddedResource(UTF8String name, DataReaderFactory dataReaderFactory, uint offset, uint length, ManifestResourceAttributes flags = ManifestResourceAttributes.Private) : base(name, flags)
		{
			if (dataReaderFactory == null)
			{
				throw new ArgumentNullException("dataReaderFactory");
			}
			this.dataReaderFactory = dataReaderFactory;
			this.resourceStartOffset = offset;
			this.resourceLength = length;
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x001872F8 File Offset: 0x001872F8
		public DataReader CreateReader()
		{
			return this.dataReaderFactory.CreateReader(this.resourceStartOffset, this.resourceLength);
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x00187314 File Offset: 0x00187314
		public override string ToString()
		{
			return string.Format("{0} - size: {1}", UTF8String.ToSystemStringOrEmpty(base.Name), this.resourceLength);
		}

		// Token: 0x040026D0 RID: 9936
		private readonly DataReaderFactory dataReaderFactory;

		// Token: 0x040026D1 RID: 9937
		private readonly uint resourceStartOffset;

		// Token: 0x040026D2 RID: 9938
		private readonly uint resourceLength;
	}
}
