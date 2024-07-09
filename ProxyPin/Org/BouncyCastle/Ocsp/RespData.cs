using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x0200063F RID: 1599
	public class RespData : X509ExtensionBase
	{
		// Token: 0x060037A9 RID: 14249 RVA: 0x0012A950 File Offset: 0x0012A950
		public RespData(ResponseData data)
		{
			this.data = data;
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x0012A960 File Offset: 0x0012A960
		public int Version
		{
			get
			{
				return this.data.Version.IntValueExact + 1;
			}
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x0012A974 File Offset: 0x0012A974
		public RespID GetResponderId()
		{
			return new RespID(this.data.ResponderID);
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x0012A988 File Offset: 0x0012A988
		public DateTime ProducedAt
		{
			get
			{
				return this.data.ProducedAt.ToDateTime();
			}
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x0012A99C File Offset: 0x0012A99C
		public SingleResp[] GetResponses()
		{
			Asn1Sequence responses = this.data.Responses;
			SingleResp[] array = new SingleResp[responses.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = new SingleResp(SingleResponse.GetInstance(responses[num]));
			}
			return array;
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x0012A9F0 File Offset: 0x0012A9F0
		public X509Extensions ResponseExtensions
		{
			get
			{
				return this.data.ResponseExtensions;
			}
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x0012AA00 File Offset: 0x0012AA00
		protected override X509Extensions GetX509Extensions()
		{
			return this.ResponseExtensions;
		}

		// Token: 0x04001D6C RID: 7532
		internal readonly ResponseData data;
	}
}
