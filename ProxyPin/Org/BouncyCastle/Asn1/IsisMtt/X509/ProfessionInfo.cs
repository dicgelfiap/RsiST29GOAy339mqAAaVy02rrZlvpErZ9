using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200017D RID: 381
	public class ProfessionInfo : Asn1Encodable
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x00051890 File Offset: 0x00051890
		public static ProfessionInfo GetInstance(object obj)
		{
			if (obj == null || obj is ProfessionInfo)
			{
				return (ProfessionInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ProfessionInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000518EC File Offset: 0x000518EC
		private ProfessionInfo(Asn1Sequence seq)
		{
			if (seq.Count > 5)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Bad tag number: " + asn1TaggedObject.TagNo);
				}
				this.namingAuthority = NamingAuthority.GetInstance(asn1TaggedObject, true);
				enumerator.MoveNext();
				asn1Encodable = (Asn1Encodable)enumerator.Current;
			}
			this.professionItems = Asn1Sequence.GetInstance(asn1Encodable);
			if (enumerator.MoveNext())
			{
				asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is Asn1Sequence)
				{
					this.professionOids = Asn1Sequence.GetInstance(asn1Encodable);
				}
				else if (asn1Encodable is DerPrintableString)
				{
					this.registrationNumber = DerPrintableString.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is Asn1OctetString))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
					}
					this.addProfessionInfo = Asn1OctetString.GetInstance(asn1Encodable);
				}
			}
			if (enumerator.MoveNext())
			{
				asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is DerPrintableString)
				{
					this.registrationNumber = DerPrintableString.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is DerOctetString))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
					}
					this.addProfessionInfo = (DerOctetString)asn1Encodable;
				}
			}
			if (!enumerator.MoveNext())
			{
				return;
			}
			asn1Encodable = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable is DerOctetString)
			{
				this.addProfessionInfo = (DerOctetString)asn1Encodable;
				return;
			}
			throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00051AE0 File Offset: 0x00051AE0
		public ProfessionInfo(NamingAuthority namingAuthority, DirectoryString[] professionItems, DerObjectIdentifier[] professionOids, string registrationNumber, Asn1OctetString addProfessionInfo)
		{
			this.namingAuthority = namingAuthority;
			this.professionItems = new DerSequence(professionItems);
			if (professionOids != null)
			{
				this.professionOids = new DerSequence(professionOids);
			}
			this.registrationNumber = registrationNumber;
			this.addProfessionInfo = addProfessionInfo;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00051B20 File Offset: 0x00051B20
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptionalTagged(true, 0, this.namingAuthority);
			asn1EncodableVector.Add(this.professionItems);
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.professionOids
			});
			if (this.registrationNumber != null)
			{
				asn1EncodableVector.Add(new DerPrintableString(this.registrationNumber, true));
			}
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.addProfessionInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00051BAC File Offset: 0x00051BAC
		public virtual Asn1OctetString AddProfessionInfo
		{
			get
			{
				return this.addProfessionInfo;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00051BB4 File Offset: 0x00051BB4
		public virtual NamingAuthority NamingAuthority
		{
			get
			{
				return this.namingAuthority;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00051BBC File Offset: 0x00051BBC
		public virtual DirectoryString[] GetProfessionItems()
		{
			DirectoryString[] array = new DirectoryString[this.professionItems.Count];
			for (int i = 0; i < this.professionItems.Count; i++)
			{
				array[i] = DirectoryString.GetInstance(this.professionItems[i]);
			}
			return array;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00051C10 File Offset: 0x00051C10
		public virtual DerObjectIdentifier[] GetProfessionOids()
		{
			if (this.professionOids == null)
			{
				return new DerObjectIdentifier[0];
			}
			DerObjectIdentifier[] array = new DerObjectIdentifier[this.professionOids.Count];
			for (int i = 0; i < this.professionOids.Count; i++)
			{
				array[i] = DerObjectIdentifier.GetInstance(this.professionOids[i]);
			}
			return array;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00051C78 File Offset: 0x00051C78
		public virtual string RegistrationNumber
		{
			get
			{
				return this.registrationNumber;
			}
		}

		// Token: 0x040008CB RID: 2251
		public static readonly DerObjectIdentifier Rechtsanwltin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".1");

		// Token: 0x040008CC RID: 2252
		public static readonly DerObjectIdentifier Rechtsanwalt = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".2");

		// Token: 0x040008CD RID: 2253
		public static readonly DerObjectIdentifier Rechtsbeistand = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".3");

		// Token: 0x040008CE RID: 2254
		public static readonly DerObjectIdentifier Steuerberaterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".4");

		// Token: 0x040008CF RID: 2255
		public static readonly DerObjectIdentifier Steuerberater = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".5");

		// Token: 0x040008D0 RID: 2256
		public static readonly DerObjectIdentifier Steuerbevollmchtigte = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".6");

		// Token: 0x040008D1 RID: 2257
		public static readonly DerObjectIdentifier Steuerbevollmchtigter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".7");

		// Token: 0x040008D2 RID: 2258
		public static readonly DerObjectIdentifier Notarin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".8");

		// Token: 0x040008D3 RID: 2259
		public static readonly DerObjectIdentifier Notar = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".9");

		// Token: 0x040008D4 RID: 2260
		public static readonly DerObjectIdentifier Notarvertreterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".10");

		// Token: 0x040008D5 RID: 2261
		public static readonly DerObjectIdentifier Notarvertreter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".11");

		// Token: 0x040008D6 RID: 2262
		public static readonly DerObjectIdentifier Notariatsverwalterin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".12");

		// Token: 0x040008D7 RID: 2263
		public static readonly DerObjectIdentifier Notariatsverwalter = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".13");

		// Token: 0x040008D8 RID: 2264
		public static readonly DerObjectIdentifier Wirtschaftsprferin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".14");

		// Token: 0x040008D9 RID: 2265
		public static readonly DerObjectIdentifier Wirtschaftsprfer = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".15");

		// Token: 0x040008DA RID: 2266
		public static readonly DerObjectIdentifier VereidigteBuchprferin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".16");

		// Token: 0x040008DB RID: 2267
		public static readonly DerObjectIdentifier VereidigterBuchprfer = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".17");

		// Token: 0x040008DC RID: 2268
		public static readonly DerObjectIdentifier Patentanwltin = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".18");

		// Token: 0x040008DD RID: 2269
		public static readonly DerObjectIdentifier Patentanwalt = new DerObjectIdentifier(NamingAuthority.IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern + ".19");

		// Token: 0x040008DE RID: 2270
		private readonly NamingAuthority namingAuthority;

		// Token: 0x040008DF RID: 2271
		private readonly Asn1Sequence professionItems;

		// Token: 0x040008E0 RID: 2272
		private readonly Asn1Sequence professionOids;

		// Token: 0x040008E1 RID: 2273
		private readonly string registrationNumber;

		// Token: 0x040008E2 RID: 2274
		private readonly Asn1OctetString addProfessionInfo;
	}
}
