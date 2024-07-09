using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace PeNet.Asn1
{
	// Token: 0x02000B7B RID: 2939
	[ComVisible(true)]
	public class Asn1ObjectIdentifier : Asn1Node
	{
		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06007657 RID: 30295 RVA: 0x00237428 File Offset: 0x00237428
		private ulong[] Sids { get; }

		// Token: 0x06007658 RID: 30296 RVA: 0x00237430 File Offset: 0x00237430
		public Asn1ObjectIdentifier(ulong[] sids)
		{
			this.Sids = sids;
			if (this.Sids[0] > 2UL)
			{
				throw new FormatException("first sid must be 0, 1 or 2");
			}
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x0023745C File Offset: 0x0023745C
		public Asn1ObjectIdentifier(string value)
		{
			this.Sids = value.Split(new char[]
			{
				'.'
			}).Select(new Func<string, ulong>(ulong.Parse)).ToArray<ulong>();
			if (this.Sids[0] > 2UL)
			{
				throw new FormatException("first sid must be 0, 1 or 2");
			}
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x002374BC File Offset: 0x002374BC
		public static bool operator ==(Asn1ObjectIdentifier first, Asn1ObjectIdentifier second)
		{
			if (first == null && second == null)
			{
				return true;
			}
			if (first == null ^ second == null)
			{
				return false;
			}
			if (first.Sids.Length != second.Sids.Length)
			{
				return false;
			}
			for (int i = 0; i < first.Sids.Length; i++)
			{
				ulong num = first.Sids[i];
				ulong num2 = second.Sids[i];
				if (num != num2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x00237534 File Offset: 0x00237534
		public static bool operator !=(Asn1ObjectIdentifier first, Asn1ObjectIdentifier second)
		{
			return !(first == second);
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x00237540 File Offset: 0x00237540
		protected bool Equals(Asn1ObjectIdentifier other)
		{
			return this.ToString().Equals(other.ToString());
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x00237554 File Offset: 0x00237554
		public override bool Equals(object obj)
		{
			return this.ToString().Equals(obj.ToString());
		}

		// Token: 0x0600765E RID: 30302 RVA: 0x00237568 File Offset: 0x00237568
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600765F RID: 30303 RVA: 0x00237578 File Offset: 0x00237578
		public static Asn1ObjectIdentifier ReadFrom(Stream stream)
		{
			List<ulong> list = new List<ulong>();
			while (stream.Position < stream.Length)
			{
				ulong num = Asn1ObjectIdentifier.ReadUInt64(stream);
				if (list.Count == 0)
				{
					if (num < 40UL)
					{
						list.Add(0UL);
					}
					else if (num < 80UL)
					{
						list.Add(1UL);
						num -= 40UL;
					}
					else
					{
						list.Add(2UL);
						num -= 80UL;
					}
				}
				list.Add(num);
			}
			return new Asn1ObjectIdentifier(list.ToArray());
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06007660 RID: 30304 RVA: 0x00237608 File Offset: 0x00237608
		public override Asn1UniversalNodeType NodeType
		{
			get
			{
				return Asn1UniversalNodeType.ObjectId;
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06007661 RID: 30305 RVA: 0x0023760C File Offset: 0x0023760C
		public override Asn1TagForm TagForm
		{
			get
			{
				return Asn1TagForm.Primitive;
			}
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x00237610 File Offset: 0x00237610
		protected override XElement ToXElementCore()
		{
			return new XElement("ObjectId", string.Join(".", from s in this.Sids
			select s.ToString()));
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x00237668 File Offset: 0x00237668
		protected override byte[] GetBytesCore()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				ulong val = this.Sids[0] * 40UL + this.Sids[1];
				Asn1ObjectIdentifier.Write(memoryStream, val);
				for (int i = 2; i < this.Sids.Length; i++)
				{
					ulong val2 = this.Sids[i];
					Asn1ObjectIdentifier.Write(memoryStream, val2);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x002376EC File Offset: 0x002376EC
		private static ulong ReadUInt64(Stream stream)
		{
			ulong num = 0UL;
			int num2;
			do
			{
				num2 = stream.ReadByte();
				num = (num << 7) + (ulong)((long)(num2 & 127));
			}
			while (num2 >= 128);
			return num;
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x0023771C File Offset: 0x0023771C
		private static void Write(Stream mem, ulong val)
		{
			bool flag = true;
			for (int i = 9; i >= 0; i--)
			{
				ulong num = val >> i * 7 & 127UL;
				if (!flag || num != 0UL)
				{
					flag = false;
					if (i != 0)
					{
						num |= 128UL;
					}
					mem.WriteByte((byte)num);
				}
			}
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x00237774 File Offset: 0x00237774
		public new static Asn1ObjectIdentifier Parse(XElement xNode)
		{
			return new Asn1ObjectIdentifier(xNode.Value.Trim());
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06007667 RID: 30311 RVA: 0x00237788 File Offset: 0x00237788
		public string Value
		{
			get
			{
				return string.Join(".", from s in this.Sids
				select s.ToString());
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06007668 RID: 30312 RVA: 0x002377D0 File Offset: 0x002377D0
		public string FriendlyName
		{
			get
			{
				string value = this.Value;
				if (value != null)
				{
					uint num = PeNet.Asn1.<PrivateImplementationDetails>.ComputeStringHash(value);
					if (num <= 2477476687U)
					{
						if (num <= 601591448U)
						{
							if (num != 134011153U)
							{
								if (num != 184344010U)
								{
									if (num == 601591448U)
									{
										if (value == "1.2.840.113549.1.1.5")
										{
											return "sha1WithRSAEncryption";
										}
									}
								}
								else if (value == "2.5.4.6")
								{
									return "countryName";
								}
							}
							else if (value == "2.5.4.3")
							{
								return "commonName";
							}
						}
						else if (num <= 1191487022U)
						{
							if (num != 668701924U)
							{
								if (num == 1191487022U)
								{
									if (value == "2.5.4.11")
									{
										return "organizationalUnitName";
									}
								}
							}
							else if (value == "1.2.840.113549.1.1.1")
							{
								return "rsaEncryption";
							}
						}
						else if (num != 1208264641U)
						{
							if (num == 2477476687U)
							{
								if (value == "1.2.840.113549.1.1.11")
								{
									return "sha256WithRSAEncryption";
								}
							}
						}
						else if (value == "2.5.4.10")
						{
							return "organizationName";
						}
					}
					else if (num <= 3506813397U)
					{
						if (num <= 3422925302U)
						{
							if (num != 3406147683U)
							{
								if (num == 3422925302U)
								{
									if (value == "2.5.29.32")
									{
										return "certificatePolicies";
									}
								}
							}
							else if (value == "2.5.29.31")
							{
								return "cRLDistributionPoints";
							}
						}
						else if (num != 3473258159U)
						{
							if (num == 3506813397U)
							{
								if (value == "2.5.29.37")
								{
									return "extKeyUsage";
								}
							}
						}
						else if (value == "2.5.29.35")
						{
							return "authorityKeyIdentifier";
						}
					}
					else if (num <= 3540662825U)
					{
						if (num != 3507107587U)
						{
							if (num == 3540662825U)
							{
								if (value == "2.5.29.15")
								{
									return "keyUsage";
								}
							}
						}
						else if (value == "2.5.29.17")
						{
							return "subjectAltName";
						}
					}
					else if (num != 3741994253U)
					{
						if (num == 3841236317U)
						{
							if (value == "1.3.6.1.5.5.7.1.1")
							{
								return "authorityInfoAccess";
							}
						}
					}
					else if (value == "2.5.29.19")
					{
						return "basicConstraint";
					}
				}
				return value;
			}
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x00237A98 File Offset: 0x00237A98
		public override string ToString()
		{
			return this.FriendlyName;
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x00237AA0 File Offset: 0x00237AA0
		public static Asn1ObjectIdentifier CommonName { get; } = new Asn1ObjectIdentifier("2.5.4.3");

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x0600766B RID: 30315 RVA: 0x00237AA8 File Offset: 0x00237AA8
		public static Asn1ObjectIdentifier CountryName { get; } = new Asn1ObjectIdentifier("2.5.4.6");

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x0600766C RID: 30316 RVA: 0x00237AB0 File Offset: 0x00237AB0
		public static Asn1ObjectIdentifier LocalityName { get; } = new Asn1ObjectIdentifier("2.5.4.7");

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x0600766D RID: 30317 RVA: 0x00237AB8 File Offset: 0x00237AB8
		public static Asn1ObjectIdentifier StreetAddress { get; } = new Asn1ObjectIdentifier("2.5.4.9");

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x0600766E RID: 30318 RVA: 0x00237AC0 File Offset: 0x00237AC0
		public static Asn1ObjectIdentifier OrganizationName { get; } = new Asn1ObjectIdentifier("2.5.4.10");

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x0600766F RID: 30319 RVA: 0x00237AC8 File Offset: 0x00237AC8
		public static Asn1ObjectIdentifier OrganizationalUnitName { get; } = new Asn1ObjectIdentifier("2.5.4.11");

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06007670 RID: 30320 RVA: 0x00237AD0 File Offset: 0x00237AD0
		public static Asn1ObjectIdentifier SubjectKeyIdentifier { get; } = new Asn1ObjectIdentifier("2.5.29.14");

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06007671 RID: 30321 RVA: 0x00237AD8 File Offset: 0x00237AD8
		public static Asn1ObjectIdentifier KeyUsage { get; } = new Asn1ObjectIdentifier("2.5.29.15");

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06007672 RID: 30322 RVA: 0x00237AE0 File Offset: 0x00237AE0
		public static Asn1ObjectIdentifier SubjectAltName { get; } = new Asn1ObjectIdentifier("2.5.29.17");

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06007673 RID: 30323 RVA: 0x00237AE8 File Offset: 0x00237AE8
		public static Asn1ObjectIdentifier IssuerAltName { get; } = new Asn1ObjectIdentifier("2.5.29.18");

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06007674 RID: 30324 RVA: 0x00237AF0 File Offset: 0x00237AF0
		public static Asn1ObjectIdentifier BasicConstraints { get; } = new Asn1ObjectIdentifier("2.5.29.19");

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06007675 RID: 30325 RVA: 0x00237AF8 File Offset: 0x00237AF8
		public static Asn1ObjectIdentifier CrlDistributionPoints { get; } = new Asn1ObjectIdentifier("2.5.29.31");

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06007676 RID: 30326 RVA: 0x00237B00 File Offset: 0x00237B00
		public static Asn1ObjectIdentifier CertificatePolicies { get; } = new Asn1ObjectIdentifier("2.5.29.32");

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06007677 RID: 30327 RVA: 0x00237B08 File Offset: 0x00237B08
		public static Asn1ObjectIdentifier AuthorityKeyIdentifier { get; } = new Asn1ObjectIdentifier("2.5.29.35");

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06007678 RID: 30328 RVA: 0x00237B10 File Offset: 0x00237B10
		public static Asn1ObjectIdentifier ExtKeyUsage { get; } = new Asn1ObjectIdentifier("2.5.29.37");

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x00237B18 File Offset: 0x00237B18
		public static Asn1ObjectIdentifier RsaEncryption { get; } = new Asn1ObjectIdentifier("1.2.840.113549.1.1.1");

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x0600767A RID: 30330 RVA: 0x00237B20 File Offset: 0x00237B20
		public static Asn1ObjectIdentifier Sha1WithRsaEncryption { get; } = new Asn1ObjectIdentifier("1.2.840.113549.1.1.5");

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x0600767B RID: 30331 RVA: 0x00237B28 File Offset: 0x00237B28
		public static Asn1ObjectIdentifier Sha256WithRsaEncryption { get; } = new Asn1ObjectIdentifier("1.2.840.113549.1.1.11");

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x0600767C RID: 30332 RVA: 0x00237B30 File Offset: 0x00237B30
		public static Asn1ObjectIdentifier AuthorityInfoAccess { get; } = new Asn1ObjectIdentifier("1.3.6.1.5.5.7.1.1");

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x0600767D RID: 30333 RVA: 0x00237B38 File Offset: 0x00237B38
		public static Asn1ObjectIdentifier Sha1 { get; } = new Asn1ObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x0400396D RID: 14701
		public const string NODE_NAME = "ObjectId";
	}
}
