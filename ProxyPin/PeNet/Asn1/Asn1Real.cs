using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B7F RID: 2943
	[ComVisible(true)]
	public class Asn1Real : Asn1Node
	{
		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06007693 RID: 30355 RVA: 0x00237E0C File Offset: 0x00237E0C
		public override Asn1UniversalNodeType NodeType { get; }

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06007694 RID: 30356 RVA: 0x00237E14 File Offset: 0x00237E14
		public override Asn1TagForm TagForm { get; }

		// Token: 0x06007695 RID: 30357 RVA: 0x00237E1C File Offset: 0x00237E1C
		protected override XElement ToXElementCore()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x00237E24 File Offset: 0x00237E24
		protected override byte[] GetBytesCore()
		{
			throw new NotImplementedException();
		}
	}
}
