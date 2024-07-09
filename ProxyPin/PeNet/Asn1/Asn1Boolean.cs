using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B73 RID: 2931
	[ComVisible(true)]
	public class Asn1Boolean : Asn1Node
	{
		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x060075FF RID: 30207 RVA: 0x00236598 File Offset: 0x00236598
		// (set) Token: 0x06007600 RID: 30208 RVA: 0x002365A0 File Offset: 0x002365A0
		public bool Value { get; set; }

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06007601 RID: 30209 RVA: 0x002365AC File Offset: 0x002365AC
		// (set) Token: 0x06007602 RID: 30210 RVA: 0x002365B4 File Offset: 0x002365B4
		public static byte TrueValue { get; set; } = 1;

		// Token: 0x06007603 RID: 30211 RVA: 0x002365BC File Offset: 0x002365BC
		public Asn1Boolean(bool value)
		{
			this.Value = value;
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x002365CC File Offset: 0x002365CC
		public Asn1Boolean()
		{
		}

		// Token: 0x06007605 RID: 30213 RVA: 0x002365D4 File Offset: 0x002365D4
		public static Asn1Boolean ReadFrom(Stream stream)
		{
			return new Asn1Boolean(stream.ReadByte() != 0);
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06007606 RID: 30214 RVA: 0x002365E4 File Offset: 0x002365E4
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.Boolean;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06007607 RID: 30215 RVA: 0x002365E8 File Offset: 0x002365E8
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x06007608 RID: 30216 RVA: 0x002365EC File Offset: 0x002365EC
		protected override XElement ToXElementCore()
		{
			return new XElement("Boolean", this.Value.ToString().ToLowerInvariant());
		}

		// Token: 0x06007609 RID: 30217 RVA: 0x00236620 File Offset: 0x00236620
		protected override byte[] GetBytesCore()
		{
			return new byte[]
			{
				this.Value ? Asn1Boolean.TrueValue : 0
			};
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x00236654 File Offset: 0x00236654
		public new static Asn1Boolean Parse(XElement xNode)
		{
			return new Asn1Boolean(xNode.Value.ToLowerInvariant().Trim() == "trim");
		}

		// Token: 0x0400395D RID: 14685
		public const string NODE_NAME = "Boolean";
	}
}
