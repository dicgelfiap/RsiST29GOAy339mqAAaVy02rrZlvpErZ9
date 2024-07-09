using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B4F RID: 2895
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x060074BB RID: 29883 RVA: 0x00231BB0 File Offset: 0x00231BB0
		public XmlDocumentTypeWrapper(XmlDocumentType documentType) : base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x060074BC RID: 29884 RVA: 0x00231BC0 File Offset: 0x00231BC0
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060074BD RID: 29885 RVA: 0x00231BD0 File Offset: 0x00231BD0
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x060074BE RID: 29886 RVA: 0x00231BE0 File Offset: 0x00231BE0
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x060074BF RID: 29887 RVA: 0x00231BF0 File Offset: 0x00231BF0
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060074C0 RID: 29888 RVA: 0x00231C00 File Offset: 0x00231C00
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040038E8 RID: 14568
		private readonly XmlDocumentType _documentType;
	}
}
