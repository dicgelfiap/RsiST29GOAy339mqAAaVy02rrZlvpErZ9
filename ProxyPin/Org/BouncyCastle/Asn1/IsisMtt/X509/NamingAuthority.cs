using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.IsisMtt.X509
{
	// Token: 0x0200017B RID: 379
	public class NamingAuthority : Asn1Encodable
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x00051390 File Offset: 0x00051390
		public static NamingAuthority GetInstance(object obj)
		{
			if (obj == null || obj is NamingAuthority)
			{
				return (NamingAuthority)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new NamingAuthority((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000513EC File Offset: 0x000513EC
		public static NamingAuthority GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return NamingAuthority.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000513FC File Offset: 0x000513FC
		private NamingAuthority(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			IEnumerator enumerator = seq.GetEnumerator();
			if (enumerator.MoveNext())
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable is DerObjectIdentifier)
				{
					this.namingAuthorityID = (DerObjectIdentifier)asn1Encodable;
				}
				else if (asn1Encodable is DerIA5String)
				{
					this.namingAuthorityUrl = DerIA5String.GetInstance(asn1Encodable).GetString();
				}
				else
				{
					if (!(asn1Encodable is IAsn1String))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable));
					}
					this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable);
				}
			}
			if (enumerator.MoveNext())
			{
				Asn1Encodable asn1Encodable2 = (Asn1Encodable)enumerator.Current;
				if (asn1Encodable2 is DerIA5String)
				{
					this.namingAuthorityUrl = DerIA5String.GetInstance(asn1Encodable2).GetString();
				}
				else
				{
					if (!(asn1Encodable2 is IAsn1String))
					{
						throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable2));
					}
					this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable2);
				}
			}
			if (!enumerator.MoveNext())
			{
				return;
			}
			Asn1Encodable asn1Encodable3 = (Asn1Encodable)enumerator.Current;
			if (asn1Encodable3 is IAsn1String)
			{
				this.namingAuthorityText = DirectoryString.GetInstance(asn1Encodable3);
				return;
			}
			throw new ArgumentException("Bad object encountered: " + Platform.GetTypeName(asn1Encodable3));
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00051578 File Offset: 0x00051578
		public virtual DerObjectIdentifier NamingAuthorityID
		{
			get
			{
				return this.namingAuthorityID;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00051580 File Offset: 0x00051580
		public virtual DirectoryString NamingAuthorityText
		{
			get
			{
				return this.namingAuthorityText;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00051588 File Offset: 0x00051588
		public virtual string NamingAuthorityUrl
		{
			get
			{
				return this.namingAuthorityUrl;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00051590 File Offset: 0x00051590
		public NamingAuthority(DerObjectIdentifier namingAuthorityID, string namingAuthorityUrl, DirectoryString namingAuthorityText)
		{
			this.namingAuthorityID = namingAuthorityID;
			this.namingAuthorityUrl = namingAuthorityUrl;
			this.namingAuthorityText = namingAuthorityText;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000515B0 File Offset: 0x000515B0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.namingAuthorityID
			});
			if (this.namingAuthorityUrl != null)
			{
				asn1EncodableVector.Add(new DerIA5String(this.namingAuthorityUrl, true));
			}
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.namingAuthorityText
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040008C3 RID: 2243
		public static readonly DerObjectIdentifier IdIsisMttATNamingAuthoritiesRechtWirtschaftSteuern = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttATNamingAuthorities + ".1");

		// Token: 0x040008C4 RID: 2244
		private readonly DerObjectIdentifier namingAuthorityID;

		// Token: 0x040008C5 RID: 2245
		private readonly string namingAuthorityUrl;

		// Token: 0x040008C6 RID: 2246
		private readonly DirectoryString namingAuthorityText;
	}
}
