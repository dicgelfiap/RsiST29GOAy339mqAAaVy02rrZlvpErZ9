using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Org.BouncyCastle.Ocsp
{
	// Token: 0x02000639 RID: 1593
	public class OcspResp
	{
		// Token: 0x06003792 RID: 14226 RVA: 0x0012A174 File Offset: 0x0012A174
		public OcspResp(OcspResponse resp)
		{
			this.resp = resp;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0012A184 File Offset: 0x0012A184
		public OcspResp(byte[] resp) : this(new Asn1InputStream(resp))
		{
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0012A194 File Offset: 0x0012A194
		public OcspResp(Stream inStr) : this(new Asn1InputStream(inStr))
		{
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0012A1A4 File Offset: 0x0012A1A4
		private OcspResp(Asn1InputStream aIn)
		{
			try
			{
				this.resp = OcspResponse.GetInstance(aIn.ReadObject());
			}
			catch (Exception ex)
			{
				throw new IOException("malformed response: " + ex.Message, ex);
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x0012A1F8 File Offset: 0x0012A1F8
		public int Status
		{
			get
			{
				return this.resp.ResponseStatus.IntValueExact;
			}
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x0012A20C File Offset: 0x0012A20C
		public object GetResponseObject()
		{
			ResponseBytes responseBytes = this.resp.ResponseBytes;
			if (responseBytes == null)
			{
				return null;
			}
			if (responseBytes.ResponseType.Equals(OcspObjectIdentifiers.PkixOcspBasic))
			{
				try
				{
					return new BasicOcspResp(BasicOcspResponse.GetInstance(Asn1Object.FromByteArray(responseBytes.Response.GetOctets())));
				}
				catch (Exception ex)
				{
					throw new OcspException("problem decoding object: " + ex, ex);
				}
			}
			return responseBytes.Response;
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x0012A290 File Offset: 0x0012A290
		public byte[] GetEncoded()
		{
			return this.resp.GetEncoded();
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x0012A2A0 File Offset: 0x0012A2A0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			OcspResp ocspResp = obj as OcspResp;
			return ocspResp != null && this.resp.Equals(ocspResp.resp);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x0012A2DC File Offset: 0x0012A2DC
		public override int GetHashCode()
		{
			return this.resp.GetHashCode();
		}

		// Token: 0x04001D5B RID: 7515
		private OcspResponse resp;
	}
}
