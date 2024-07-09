using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x02000179 RID: 377
	public class DeclarationOfMajority : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000CAA RID: 3242 RVA: 0x00051020 File Offset: 0x00051020
		public DeclarationOfMajority(int notYoungerThan)
		{
			this.declaration = new DerTaggedObject(false, 0, new DerInteger(notYoungerThan));
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0005103C File Offset: 0x0005103C
		public DeclarationOfMajority(bool fullAge, string country)
		{
			if (country.Length > 2)
			{
				throw new ArgumentException("country can only be 2 characters");
			}
			DerPrintableString derPrintableString = new DerPrintableString(country, true);
			DerSequence obj;
			if (fullAge)
			{
				obj = new DerSequence(derPrintableString);
			}
			else
			{
				obj = new DerSequence(new Asn1Encodable[]
				{
					DerBoolean.False,
					derPrintableString
				});
			}
			this.declaration = new DerTaggedObject(false, 1, obj);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000510B4 File Offset: 0x000510B4
		public DeclarationOfMajority(DerGeneralizedTime dateOfBirth)
		{
			this.declaration = new DerTaggedObject(false, 2, dateOfBirth);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000510CC File Offset: 0x000510CC
		public static DeclarationOfMajority GetInstance(object obj)
		{
			if (obj == null || obj is DeclarationOfMajority)
			{
				return (DeclarationOfMajority)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new DeclarationOfMajority((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00051128 File Offset: 0x00051128
		private DeclarationOfMajority(Asn1TaggedObject o)
		{
			if (o.TagNo > 2)
			{
				throw new ArgumentException("Bad tag number: " + o.TagNo);
			}
			this.declaration = o;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00051160 File Offset: 0x00051160
		public override Asn1Object ToAsn1Object()
		{
			return this.declaration;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00051168 File Offset: 0x00051168
		public DeclarationOfMajority.Choice Type
		{
			get
			{
				return (DeclarationOfMajority.Choice)this.declaration.TagNo;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00051178 File Offset: 0x00051178
		public virtual int NotYoungerThan
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.NotYoungerThan)
				{
					return DerInteger.GetInstance(this.declaration, false).IntValueExact;
				}
				return -1;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x000511B0 File Offset: 0x000511B0
		public virtual Asn1Sequence FullAgeAtCountry
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.FullAgeAtCountry)
				{
					return Asn1Sequence.GetInstance(this.declaration, false);
				}
				return null;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x000511E4 File Offset: 0x000511E4
		public virtual DerGeneralizedTime DateOfBirth
		{
			get
			{
				DeclarationOfMajority.Choice tagNo = (DeclarationOfMajority.Choice)this.declaration.TagNo;
				if (tagNo == DeclarationOfMajority.Choice.DateOfBirth)
				{
					return DerGeneralizedTime.GetInstance(this.declaration, false);
				}
				return null;
			}
		}

		// Token: 0x040008BF RID: 2239
		private readonly Asn1TaggedObject declaration;

		// Token: 0x02000D8A RID: 3466
		public enum Choice
		{
			// Token: 0x04003FD9 RID: 16345
			NotYoungerThan,
			// Token: 0x04003FDA RID: 16346
			FullAgeAtCountry,
			// Token: 0x04003FDB RID: 16347
			DateOfBirth
		}
	}
}
