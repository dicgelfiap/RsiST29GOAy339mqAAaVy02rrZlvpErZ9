using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000198 RID: 408
	public class ResponseBytes : Asn1Encodable
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x00053F64 File Offset: 0x00053F64
		public static ResponseBytes GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ResponseBytes.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00053F74 File Offset: 0x00053F74
		public static ResponseBytes GetInstance(object obj)
		{
			if (obj == null || obj is ResponseBytes)
			{
				return (ResponseBytes)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ResponseBytes((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00053FD0 File Offset: 0x00053FD0
		public ResponseBytes(DerObjectIdentifier responseType, Asn1OctetString response)
		{
			if (responseType == null)
			{
				throw new ArgumentNullException("responseType");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.responseType = responseType;
			this.response = response;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00054008 File Offset: 0x00054008
		private ResponseBytes(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.responseType = DerObjectIdentifier.GetInstance(seq[0]);
			this.response = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00054060 File Offset: 0x00054060
		public DerObjectIdentifier ResponseType
		{
			get
			{
				return this.responseType;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00054068 File Offset: 0x00054068
		public Asn1OctetString Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00054070 File Offset: 0x00054070
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.responseType,
				this.response
			});
		}

		// Token: 0x040009A1 RID: 2465
		private readonly DerObjectIdentifier responseType;

		// Token: 0x040009A2 RID: 2466
		private readonly Asn1OctetString response;
	}
}
