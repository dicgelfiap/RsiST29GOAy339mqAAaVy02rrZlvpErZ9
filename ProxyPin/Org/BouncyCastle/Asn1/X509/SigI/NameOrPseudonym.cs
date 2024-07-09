using System;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x020001DF RID: 479
	public class NameOrPseudonym : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000F63 RID: 3939 RVA: 0x0005C2D4 File Offset: 0x0005C2D4
		public static NameOrPseudonym GetInstance(object obj)
		{
			if (obj == null || obj is NameOrPseudonym)
			{
				return (NameOrPseudonym)obj;
			}
			if (obj is IAsn1String)
			{
				return new NameOrPseudonym(DirectoryString.GetInstance(obj));
			}
			if (obj is Asn1Sequence)
			{
				return new NameOrPseudonym((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0005C348 File Offset: 0x0005C348
		public NameOrPseudonym(DirectoryString pseudonym)
		{
			this.pseudonym = pseudonym;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0005C358 File Offset: 0x0005C358
		private NameOrPseudonym(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (!(seq[0] is IAsn1String))
			{
				throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(seq[0]));
			}
			this.surname = DirectoryString.GetInstance(seq[0]);
			this.givenName = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0005C3E8 File Offset: 0x0005C3E8
		public NameOrPseudonym(string pseudonym) : this(new DirectoryString(pseudonym))
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0005C3F8 File Offset: 0x0005C3F8
		public NameOrPseudonym(DirectoryString surname, Asn1Sequence givenName)
		{
			this.surname = surname;
			this.givenName = givenName;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0005C410 File Offset: 0x0005C410
		public DirectoryString Pseudonym
		{
			get
			{
				return this.pseudonym;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0005C418 File Offset: 0x0005C418
		public DirectoryString Surname
		{
			get
			{
				return this.surname;
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0005C420 File Offset: 0x0005C420
		public DirectoryString[] GetGivenName()
		{
			DirectoryString[] array = new DirectoryString[this.givenName.Count];
			int num = 0;
			foreach (object obj in this.givenName)
			{
				array[num++] = DirectoryString.GetInstance(obj);
			}
			return array;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0005C49C File Offset: 0x0005C49C
		public override Asn1Object ToAsn1Object()
		{
			if (this.pseudonym != null)
			{
				return this.pseudonym.ToAsn1Object();
			}
			return new DerSequence(new Asn1Encodable[]
			{
				this.surname,
				this.givenName
			});
		}

		// Token: 0x04000B88 RID: 2952
		private readonly DirectoryString pseudonym;

		// Token: 0x04000B89 RID: 2953
		private readonly DirectoryString surname;

		// Token: 0x04000B8A RID: 2954
		private readonly Asn1Sequence givenName;
	}
}
