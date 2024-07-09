using System;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020006C1 RID: 1729
	public class TimeStampTokenInfo
	{
		// Token: 0x06003C8D RID: 15501 RVA: 0x0014E960 File Offset: 0x0014E960
		public TimeStampTokenInfo(TstInfo tstInfo)
		{
			this.tstInfo = tstInfo;
			try
			{
				this.genTime = tstInfo.GenTime.ToDateTime();
			}
			catch (Exception ex)
			{
				throw new TspException("unable to parse genTime field: " + ex.Message);
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06003C8E RID: 15502 RVA: 0x0014E9B8 File Offset: 0x0014E9B8
		public bool IsOrdered
		{
			get
			{
				return this.tstInfo.Ordering.IsTrue;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x0014E9CC File Offset: 0x0014E9CC
		public Accuracy Accuracy
		{
			get
			{
				return this.tstInfo.Accuracy;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x0014E9DC File Offset: 0x0014E9DC
		public DateTime GenTime
		{
			get
			{
				return this.genTime;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x0014E9E4 File Offset: 0x0014E9E4
		public GenTimeAccuracy GenTimeAccuracy
		{
			get
			{
				if (this.Accuracy != null)
				{
					return new GenTimeAccuracy(this.Accuracy);
				}
				return null;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x0014EA00 File Offset: 0x0014EA00
		public string Policy
		{
			get
			{
				return this.tstInfo.Policy.Id;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003C93 RID: 15507 RVA: 0x0014EA14 File Offset: 0x0014EA14
		public BigInteger SerialNumber
		{
			get
			{
				return this.tstInfo.SerialNumber.Value;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x0014EA28 File Offset: 0x0014EA28
		public GeneralName Tsa
		{
			get
			{
				return this.tstInfo.Tsa;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x0014EA38 File Offset: 0x0014EA38
		public BigInteger Nonce
		{
			get
			{
				if (this.tstInfo.Nonce != null)
				{
					return this.tstInfo.Nonce.Value;
				}
				return null;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003C96 RID: 15510 RVA: 0x0014EA5C File Offset: 0x0014EA5C
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.tstInfo.MessageImprint.HashAlgorithm;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x0014EA70 File Offset: 0x0014EA70
		public string MessageImprintAlgOid
		{
			get
			{
				return this.tstInfo.MessageImprint.HashAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x0014EA9C File Offset: 0x0014EA9C
		public byte[] GetMessageImprintDigest()
		{
			return this.tstInfo.MessageImprint.GetHashedMessage();
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x0014EAB0 File Offset: 0x0014EAB0
		public byte[] GetEncoded()
		{
			return this.tstInfo.GetEncoded();
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x0014EAC0 File Offset: 0x0014EAC0
		public TstInfo TstInfo
		{
			get
			{
				return this.tstInfo;
			}
		}

		// Token: 0x04001ECA RID: 7882
		private TstInfo tstInfo;

		// Token: 0x04001ECB RID: 7883
		private DateTime genTime;
	}
}
