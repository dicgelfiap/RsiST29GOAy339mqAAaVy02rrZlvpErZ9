using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006BB RID: 1723
	public class TimeStampRequest : X509ExtensionBase
	{
		// Token: 0x06003C47 RID: 15431 RVA: 0x0014D804 File Offset: 0x0014D804
		public TimeStampRequest(TimeStampReq req)
		{
			this.req = req;
			this.extensions = req.Extensions;
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x0014D820 File Offset: 0x0014D820
		public TimeStampRequest(byte[] req) : this(new Asn1InputStream(req))
		{
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x0014D830 File Offset: 0x0014D830
		public TimeStampRequest(Stream input) : this(new Asn1InputStream(input))
		{
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x0014D840 File Offset: 0x0014D840
		private TimeStampRequest(Asn1InputStream str)
		{
			try
			{
				this.req = TimeStampReq.GetInstance(str.ReadObject());
			}
			catch (InvalidCastException arg)
			{
				throw new IOException("malformed request: " + arg);
			}
			catch (ArgumentException arg2)
			{
				throw new IOException("malformed request: " + arg2);
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x0014D8AC File Offset: 0x0014D8AC
		public int Version
		{
			get
			{
				return this.req.Version.IntValueExact;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06003C4C RID: 15436 RVA: 0x0014D8C0 File Offset: 0x0014D8C0
		public string MessageImprintAlgOid
		{
			get
			{
				return this.req.MessageImprint.HashAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x0014D8EC File Offset: 0x0014D8EC
		public byte[] GetMessageImprintDigest()
		{
			return this.req.MessageImprint.GetHashedMessage();
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06003C4E RID: 15438 RVA: 0x0014D900 File Offset: 0x0014D900
		public string ReqPolicy
		{
			get
			{
				if (this.req.ReqPolicy != null)
				{
					return this.req.ReqPolicy.Id;
				}
				return null;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x0014D924 File Offset: 0x0014D924
		public BigInteger Nonce
		{
			get
			{
				if (this.req.Nonce != null)
				{
					return this.req.Nonce.Value;
				}
				return null;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x0014D948 File Offset: 0x0014D948
		public bool CertReq
		{
			get
			{
				return this.req.CertReq != null && this.req.CertReq.IsTrue;
			}
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x0014D96C File Offset: 0x0014D96C
		public void Validate(IList algorithms, IList policies, IList extensions)
		{
			if (!algorithms.Contains(this.MessageImprintAlgOid))
			{
				throw new TspValidationException("request contains unknown algorithm", 128);
			}
			if (policies != null && this.ReqPolicy != null && !policies.Contains(this.ReqPolicy))
			{
				throw new TspValidationException("request contains unknown policy", 256);
			}
			if (this.Extensions != null && extensions != null)
			{
				foreach (object obj in this.Extensions.ExtensionOids)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					if (!extensions.Contains(derObjectIdentifier.Id))
					{
						throw new TspValidationException("request contains unknown extension", 8388608);
					}
				}
			}
			int digestLength = TspUtil.GetDigestLength(this.MessageImprintAlgOid);
			if (digestLength != this.GetMessageImprintDigest().Length)
			{
				throw new TspValidationException("imprint digest the wrong length", 4);
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x0014DA78 File Offset: 0x0014DA78
		public byte[] GetEncoded()
		{
			return this.req.GetEncoded();
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x0014DA88 File Offset: 0x0014DA88
		internal X509Extensions Extensions
		{
			get
			{
				return this.req.Extensions;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x0014DA98 File Offset: 0x0014DA98
		public virtual bool HasExtensions
		{
			get
			{
				return this.extensions != null;
			}
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x0014DAA8 File Offset: 0x0014DAA8
		public virtual X509Extension GetExtension(DerObjectIdentifier oid)
		{
			if (this.extensions != null)
			{
				return this.extensions.GetExtension(oid);
			}
			return null;
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x0014DAC4 File Offset: 0x0014DAC4
		public virtual IList GetExtensionOids()
		{
			return TspUtil.GetExtensionOids(this.extensions);
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x0014DAD4 File Offset: 0x0014DAD4
		protected override X509Extensions GetX509Extensions()
		{
			return this.Extensions;
		}

		// Token: 0x04001EAA RID: 7850
		private TimeStampReq req;

		// Token: 0x04001EAB RID: 7851
		private X509Extensions extensions;
	}
}
