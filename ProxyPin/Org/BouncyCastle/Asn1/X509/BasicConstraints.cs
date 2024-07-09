using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001EA RID: 490
	public class BasicConstraints : Asn1Encodable
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x0005D50C File Offset: 0x0005D50C
		public static BasicConstraints GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return BasicConstraints.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0005D51C File Offset: 0x0005D51C
		public static BasicConstraints GetInstance(object obj)
		{
			if (obj is BasicConstraints)
			{
				return (BasicConstraints)obj;
			}
			if (obj is X509Extension)
			{
				return BasicConstraints.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			if (obj == null)
			{
				return null;
			}
			return new BasicConstraints(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0005D570 File Offset: 0x0005D570
		public static BasicConstraints FromExtensions(X509Extensions extensions)
		{
			return BasicConstraints.GetInstance(X509Extensions.GetExtensionParsedValue(extensions, X509Extensions.BasicConstraints));
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0005D584 File Offset: 0x0005D584
		private BasicConstraints(Asn1Sequence seq)
		{
			if (seq.Count > 0)
			{
				if (seq[0] is DerBoolean)
				{
					this.cA = DerBoolean.GetInstance(seq[0]);
				}
				else
				{
					this.pathLenConstraint = DerInteger.GetInstance(seq[0]);
				}
				if (seq.Count > 1)
				{
					if (this.cA == null)
					{
						throw new ArgumentException("wrong sequence in constructor", "seq");
					}
					this.pathLenConstraint = DerInteger.GetInstance(seq[1]);
				}
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0005D61C File Offset: 0x0005D61C
		public BasicConstraints(bool cA)
		{
			if (cA)
			{
				this.cA = DerBoolean.True;
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0005D638 File Offset: 0x0005D638
		public BasicConstraints(int pathLenConstraint)
		{
			this.cA = DerBoolean.True;
			this.pathLenConstraint = new DerInteger(pathLenConstraint);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0005D658 File Offset: 0x0005D658
		public bool IsCA()
		{
			return this.cA != null && this.cA.IsTrue;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0005D674 File Offset: 0x0005D674
		public BigInteger PathLenConstraint
		{
			get
			{
				if (this.pathLenConstraint != null)
				{
					return this.pathLenConstraint.Value;
				}
				return null;
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0005D690 File Offset: 0x0005D690
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(2);
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.cA,
				this.pathLenConstraint
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0005D6D8 File Offset: 0x0005D6D8
		public override string ToString()
		{
			if (this.pathLenConstraint == null)
			{
				return "BasicConstraints: isCa(" + this.IsCA() + ")";
			}
			return string.Concat(new object[]
			{
				"BasicConstraints: isCa(",
				this.IsCA(),
				"), pathLenConstraint = ",
				this.pathLenConstraint.Value
			});
		}

		// Token: 0x04000BB1 RID: 2993
		private readonly DerBoolean cA;

		// Token: 0x04000BB2 RID: 2994
		private readonly DerInteger pathLenConstraint;
	}
}
