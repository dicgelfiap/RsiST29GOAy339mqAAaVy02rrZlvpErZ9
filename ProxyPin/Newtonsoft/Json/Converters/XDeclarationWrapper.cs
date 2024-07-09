using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B56 RID: 2902
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060074F2 RID: 29938 RVA: 0x00231F24 File Offset: 0x00231F24
		internal XDeclaration Declaration { get; }

		// Token: 0x060074F3 RID: 29939 RVA: 0x00231F2C File Offset: 0x00231F2C
		public XDeclarationWrapper(XDeclaration declaration) : base(null)
		{
			this.Declaration = declaration;
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x060074F4 RID: 29940 RVA: 0x00231F3C File Offset: 0x00231F3C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060074F5 RID: 29941 RVA: 0x00231F40 File Offset: 0x00231F40
		public string Version
		{
			get
			{
				return this.Declaration.Version;
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060074F6 RID: 29942 RVA: 0x00231F50 File Offset: 0x00231F50
		// (set) Token: 0x060074F7 RID: 29943 RVA: 0x00231F60 File Offset: 0x00231F60
		public string Encoding
		{
			get
			{
				return this.Declaration.Encoding;
			}
			set
			{
				this.Declaration.Encoding = value;
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060074F8 RID: 29944 RVA: 0x00231F70 File Offset: 0x00231F70
		// (set) Token: 0x060074F9 RID: 29945 RVA: 0x00231F80 File Offset: 0x00231F80
		public string Standalone
		{
			get
			{
				return this.Declaration.Standalone;
			}
			set
			{
				this.Declaration.Standalone = value;
			}
		}
	}
}
