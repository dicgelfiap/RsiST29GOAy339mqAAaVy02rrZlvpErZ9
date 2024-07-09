using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000227 RID: 551
	public class DHDomainParameters : Asn1Encodable
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00065AA8 File Offset: 0x00065AA8
		public static DHDomainParameters GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return DHDomainParameters.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00065AB8 File Offset: 0x00065AB8
		public static DHDomainParameters GetInstance(object obj)
		{
			if (obj == null || obj is DHDomainParameters)
			{
				return (DHDomainParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DHDomainParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DHDomainParameters: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00065B14 File Offset: 0x00065B14
		public DHDomainParameters(DerInteger p, DerInteger g, DerInteger q, DerInteger j, DHValidationParms validationParms)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.p = p;
			this.g = g;
			this.q = q;
			this.j = j;
			this.validationParms = validationParms;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00065B84 File Offset: 0x00065B84
		private DHDomainParameters(Asn1Sequence seq)
		{
			if (seq.Count < 3 || seq.Count > 5)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			IEnumerator enumerator = seq.GetEnumerator();
			this.p = DerInteger.GetInstance(DHDomainParameters.GetNext(enumerator));
			this.g = DerInteger.GetInstance(DHDomainParameters.GetNext(enumerator));
			this.q = DerInteger.GetInstance(DHDomainParameters.GetNext(enumerator));
			Asn1Encodable next = DHDomainParameters.GetNext(enumerator);
			if (next != null && next is DerInteger)
			{
				this.j = DerInteger.GetInstance(next);
				next = DHDomainParameters.GetNext(enumerator);
			}
			if (next != null)
			{
				this.validationParms = DHValidationParms.GetInstance(next.ToAsn1Object());
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00065C50 File Offset: 0x00065C50
		private static Asn1Encodable GetNext(IEnumerator e)
		{
			if (!e.MoveNext())
			{
				return null;
			}
			return (Asn1Encodable)e.Current;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00065C6C File Offset: 0x00065C6C
		public DerInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00065C74 File Offset: 0x00065C74
		public DerInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00065C7C File Offset: 0x00065C7C
		public DerInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00065C84 File Offset: 0x00065C84
		public DerInteger J
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00065C8C File Offset: 0x00065C8C
		public DHValidationParms ValidationParms
		{
			get
			{
				return this.validationParms;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00065C94 File Offset: 0x00065C94
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.p,
				this.g,
				this.q
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.j,
				this.validationParms
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000CF4 RID: 3316
		private readonly DerInteger p;

		// Token: 0x04000CF5 RID: 3317
		private readonly DerInteger g;

		// Token: 0x04000CF6 RID: 3318
		private readonly DerInteger q;

		// Token: 0x04000CF7 RID: 3319
		private readonly DerInteger j;

		// Token: 0x04000CF8 RID: 3320
		private readonly DHValidationParms validationParms;
	}
}
