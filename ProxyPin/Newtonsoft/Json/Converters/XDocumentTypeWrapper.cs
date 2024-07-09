using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000B57 RID: 2903
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x060074FA RID: 29946 RVA: 0x00231F90 File Offset: 0x00231F90
		public XDocumentTypeWrapper(XDocumentType documentType) : base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060074FB RID: 29947 RVA: 0x00231FA0 File Offset: 0x00231FA0
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060074FC RID: 29948 RVA: 0x00231FB0 File Offset: 0x00231FB0
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060074FD RID: 29949 RVA: 0x00231FC0 File Offset: 0x00231FC0
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060074FE RID: 29950 RVA: 0x00231FD0 File Offset: 0x00231FD0
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060074FF RID: 29951 RVA: 0x00231FE0 File Offset: 0x00231FE0
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x040038ED RID: 14573
		private readonly XDocumentType _documentType;
	}
}
