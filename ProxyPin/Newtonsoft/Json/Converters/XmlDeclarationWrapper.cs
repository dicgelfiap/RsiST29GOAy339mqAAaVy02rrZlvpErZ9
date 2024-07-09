using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B4E RID: 2894
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDeclarationWrapper : XmlNodeWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x060074B5 RID: 29877 RVA: 0x00231B50 File Offset: 0x00231B50
		public XmlDeclarationWrapper(XmlDeclaration declaration) : base(declaration)
		{
			this._declaration = declaration;
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x060074B6 RID: 29878 RVA: 0x00231B60 File Offset: 0x00231B60
		public string Version
		{
			get
			{
				return this._declaration.Version;
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x00231B70 File Offset: 0x00231B70
		// (set) Token: 0x060074B8 RID: 29880 RVA: 0x00231B80 File Offset: 0x00231B80
		public string Encoding
		{
			get
			{
				return this._declaration.Encoding;
			}
			set
			{
				this._declaration.Encoding = value;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x00231B90 File Offset: 0x00231B90
		// (set) Token: 0x060074BA RID: 29882 RVA: 0x00231BA0 File Offset: 0x00231BA0
		public string Standalone
		{
			get
			{
				return this._declaration.Standalone;
			}
			set
			{
				this._declaration.Standalone = value;
			}
		}

		// Token: 0x040038E7 RID: 14567
		private readonly XmlDeclaration _declaration;
	}
}
