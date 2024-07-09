using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X500;
using Org.BouncyCastle.Asn1.X500.Style;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200069B RID: 1691
	public class PkixNameConstraintValidator
	{
		// Token: 0x06003AF6 RID: 15094 RVA: 0x0013E51C File Offset: 0x0013E51C
		private static bool WithinDNSubtree(Asn1Sequence dns, Asn1Sequence subtree)
		{
			if (subtree.Count < 1 || subtree.Count > dns.Count)
			{
				return false;
			}
			int num = 0;
			Rdn instance = Rdn.GetInstance(subtree[0]);
			for (int i = 0; i < dns.Count; i++)
			{
				num = i;
				Rdn instance2 = Rdn.GetInstance(dns[i]);
				if (IetfUtilities.RdnAreEqual(instance, instance2))
				{
					break;
				}
			}
			if (subtree.Count > dns.Count - num)
			{
				return false;
			}
			for (int j = 0; j < subtree.Count; j++)
			{
				Rdn instance3 = Rdn.GetInstance(subtree[j]);
				Rdn instance4 = Rdn.GetInstance(dns[num + j]);
				if (instance3.Count == 1 && instance4.Count == 1 && instance3.GetFirst().GetType().Equals(PkixNameConstraintValidator.SerialNumberOid) && instance4.GetFirst().GetType().Equals(PkixNameConstraintValidator.SerialNumberOid))
				{
					if (!Platform.StartsWith(instance4.GetFirst().Value.ToString(), instance3.GetFirst().Value.ToString()))
					{
						return false;
					}
				}
				else if (!IetfUtilities.RdnAreEqual(instance3, instance4))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x0013E66C File Offset: 0x0013E66C
		public void CheckPermittedDN(Asn1Sequence dns)
		{
			this.CheckPermittedDN(this.permittedSubtreesDN, dns);
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x0013E67C File Offset: 0x0013E67C
		public void CheckExcludedDN(Asn1Sequence dns)
		{
			this.CheckExcludedDN(this.excludedSubtreesDN, dns);
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x0013E68C File Offset: 0x0013E68C
		private void CheckPermittedDN(ISet permitted, Asn1Sequence dns)
		{
			if (permitted == null)
			{
				return;
			}
			if (permitted.Count == 0 && dns.Count == 0)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				Asn1Sequence subtree = (Asn1Sequence)obj;
				if (PkixNameConstraintValidator.WithinDNSubtree(dns, subtree))
				{
					return;
				}
			}
			throw new PkixNameConstraintValidatorException("Subject distinguished name is not from a permitted subtree");
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x0013E6F4 File Offset: 0x0013E6F4
		private void CheckExcludedDN(ISet excluded, Asn1Sequence dns)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				Asn1Sequence subtree = (Asn1Sequence)obj;
				if (PkixNameConstraintValidator.WithinDNSubtree(dns, subtree))
				{
					throw new PkixNameConstraintValidatorException("Subject distinguished name is from an excluded subtree");
				}
			}
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x0013E748 File Offset: 0x0013E748
		private ISet IntersectDN(ISet permitted, ISet dns)
		{
			ISet set = new HashSet();
			foreach (object obj in dns)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(((GeneralSubtree)obj).Base.Name.ToAsn1Object());
				if (permitted == null)
				{
					if (instance != null)
					{
						set.Add(instance);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						Asn1Sequence asn1Sequence = (Asn1Sequence)obj2;
						if (PkixNameConstraintValidator.WithinDNSubtree(instance, asn1Sequence))
						{
							set.Add(instance);
						}
						else if (PkixNameConstraintValidator.WithinDNSubtree(asn1Sequence, instance))
						{
							set.Add(asn1Sequence);
						}
					}
				}
			}
			return set;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x0013E800 File Offset: 0x0013E800
		private ISet UnionDN(ISet excluded, Asn1Sequence dn)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)obj;
					if (PkixNameConstraintValidator.WithinDNSubtree(dn, asn1Sequence))
					{
						set.Add(asn1Sequence);
					}
					else if (PkixNameConstraintValidator.WithinDNSubtree(asn1Sequence, dn))
					{
						set.Add(dn);
					}
					else
					{
						set.Add(asn1Sequence);
						set.Add(dn);
					}
				}
				return set;
			}
			if (dn == null)
			{
				return excluded;
			}
			excluded.Add(dn);
			return excluded;
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x0013E894 File Offset: 0x0013E894
		private ISet IntersectEmail(ISet permitted, ISet emails)
		{
			ISet set = new HashSet();
			foreach (object obj in emails)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string email = (string)obj2;
						this.intersectEmail(text, email, set);
					}
				}
			}
			return set;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x0013E91C File Offset: 0x0013E91C
		private ISet UnionEmail(ISet excluded, string email)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string email2 = (string)obj;
					this.UnionEmail(email2, email, set);
				}
				return set;
			}
			if (email == null)
			{
				return excluded;
			}
			excluded.Add(email);
			return excluded;
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x0013E97C File Offset: 0x0013E97C
		private ISet IntersectIP(ISet permitted, ISet ips)
		{
			ISet set = new HashSet();
			foreach (object obj in ips)
			{
				byte[] octets = Asn1OctetString.GetInstance(((GeneralSubtree)obj).Base.Name).GetOctets();
				if (permitted == null)
				{
					if (octets != null)
					{
						set.Add(octets);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						byte[] ipWithSubmask = (byte[])obj2;
						set.AddAll(this.IntersectIPRange(ipWithSubmask, octets));
					}
				}
			}
			return set;
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x0013EA10 File Offset: 0x0013EA10
		private ISet UnionIP(ISet excluded, byte[] ip)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					byte[] ipWithSubmask = (byte[])obj;
					set.AddAll(this.UnionIPRange(ipWithSubmask, ip));
				}
				return set;
			}
			if (ip == null)
			{
				return excluded;
			}
			excluded.Add(ip);
			return excluded;
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x0013EA74 File Offset: 0x0013EA74
		private ISet UnionIPRange(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			ISet set = new HashSet();
			if (Arrays.AreEqual(ipWithSubmask1, ipWithSubmask2))
			{
				set.Add(ipWithSubmask1);
			}
			else
			{
				set.Add(ipWithSubmask1);
				set.Add(ipWithSubmask2);
			}
			return set;
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x0013EAB4 File Offset: 0x0013EAB4
		private ISet IntersectIPRange(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			if (ipWithSubmask1.Length != ipWithSubmask2.Length)
			{
				return new HashSet();
			}
			byte[][] array = this.ExtractIPsAndSubnetMasks(ipWithSubmask1, ipWithSubmask2);
			byte[] ip = array[0];
			byte[] array2 = array[1];
			byte[] ip2 = array[2];
			byte[] array3 = array[3];
			byte[][] array4 = this.MinMaxIPs(ip, array2, ip2, array3);
			byte[] ip3 = PkixNameConstraintValidator.Min(array4[1], array4[3]);
			byte[] ip4 = PkixNameConstraintValidator.Max(array4[0], array4[2]);
			if (PkixNameConstraintValidator.CompareTo(ip4, ip3) == 1)
			{
				return new HashSet();
			}
			byte[] ip5 = PkixNameConstraintValidator.Or(array4[0], array4[2]);
			byte[] subnetMask = PkixNameConstraintValidator.Or(array2, array3);
			return new HashSet
			{
				this.IpWithSubnetMask(ip5, subnetMask)
			};
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x0013EB8C File Offset: 0x0013EB8C
		private byte[] IpWithSubnetMask(byte[] ip, byte[] subnetMask)
		{
			int num = ip.Length;
			byte[] array = new byte[num * 2];
			Array.Copy(ip, 0, array, 0, num);
			Array.Copy(subnetMask, 0, array, num, num);
			return array;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x0013EBC0 File Offset: 0x0013EBC0
		private byte[][] ExtractIPsAndSubnetMasks(byte[] ipWithSubmask1, byte[] ipWithSubmask2)
		{
			int num = ipWithSubmask1.Length / 2;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(ipWithSubmask1, 0, array, 0, num);
			Array.Copy(ipWithSubmask1, num, array2, 0, num);
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			Array.Copy(ipWithSubmask2, 0, array3, 0, num);
			Array.Copy(ipWithSubmask2, num, array4, 0, num);
			return new byte[][]
			{
				array,
				array2,
				array3,
				array4
			};
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x0013EC4C File Offset: 0x0013EC4C
		private byte[][] MinMaxIPs(byte[] ip1, byte[] subnetmask1, byte[] ip2, byte[] subnetmask2)
		{
			int num = ip1.Length;
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			byte[] array3 = new byte[num];
			byte[] array4 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (ip1[i] & subnetmask1[i]);
				array2[i] = ((ip1[i] & subnetmask1[i]) | ~subnetmask1[i]);
				array3[i] = (ip2[i] & subnetmask2[i]);
				array4[i] = ((ip2[i] & subnetmask2[i]) | ~subnetmask2[i]);
			}
			return new byte[][]
			{
				array,
				array2,
				array3,
				array4
			};
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x0013ED08 File Offset: 0x0013ED08
		private void CheckPermittedEmail(ISet permitted, string email)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string constraint = (string)obj;
				if (this.EmailIsConstrained(email, constraint))
				{
					return;
				}
			}
			if (email.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("Subject email address is not from a permitted subtree.");
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x0013ED70 File Offset: 0x0013ED70
		private void CheckExcludedEmail(ISet excluded, string email)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string constraint = (string)obj;
				if (this.EmailIsConstrained(email, constraint))
				{
					throw new PkixNameConstraintValidatorException("Email address is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x0013EDC8 File Offset: 0x0013EDC8
		private void CheckPermittedIP(ISet permitted, byte[] ip)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				byte[] constraint = (byte[])obj;
				if (this.IsIPConstrained(ip, constraint))
				{
					return;
				}
			}
			if (ip.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("IP is not from a permitted subtree.");
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x0013EE2C File Offset: 0x0013EE2C
		private void CheckExcludedIP(ISet excluded, byte[] ip)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				byte[] constraint = (byte[])obj;
				if (this.IsIPConstrained(ip, constraint))
				{
					throw new PkixNameConstraintValidatorException("IP is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x0013EE84 File Offset: 0x0013EE84
		private bool IsIPConstrained(byte[] ip, byte[] constraint)
		{
			int num = ip.Length;
			if (num != constraint.Length / 2)
			{
				return false;
			}
			byte[] array = new byte[num];
			Array.Copy(constraint, num, array, 0, num);
			byte[] array2 = new byte[num];
			byte[] array3 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (constraint[i] & array[i]);
				array3[i] = (ip[i] & array[i]);
			}
			return Arrays.AreEqual(array2, array3);
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x0013EEFC File Offset: 0x0013EEFC
		private bool EmailIsConstrained(string email, string constraint)
		{
			string text = email.Substring(email.IndexOf('@') + 1);
			if (constraint.IndexOf('@') != -1)
			{
				if (Platform.ToUpperInvariant(email).Equals(Platform.ToUpperInvariant(constraint)))
				{
					return true;
				}
			}
			else if (!constraint[0].Equals('.'))
			{
				if (Platform.ToUpperInvariant(text).Equals(Platform.ToUpperInvariant(constraint)))
				{
					return true;
				}
			}
			else if (this.WithinDomain(text, constraint))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x0013EF88 File Offset: 0x0013EF88
		private bool WithinDomain(string testDomain, string domain)
		{
			string text = domain;
			if (Platform.StartsWith(text, "."))
			{
				text = text.Substring(1);
			}
			string[] array = text.Split(new char[]
			{
				'.'
			});
			string[] array2 = testDomain.Split(new char[]
			{
				'.'
			});
			if (array2.Length <= array.Length)
			{
				return false;
			}
			int num = array2.Length - array.Length;
			for (int i = -1; i < array.Length; i++)
			{
				if (i == -1)
				{
					if (array2[i + num].Length < 1)
					{
						return false;
					}
				}
				else if (!Platform.EqualsIgnoreCase(array2[i + num], array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x0013F048 File Offset: 0x0013F048
		private void CheckPermittedDns(ISet permitted, string dns)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string text = (string)obj;
				if (this.WithinDomain(dns, text) || Platform.EqualsIgnoreCase(dns, text))
				{
					return;
				}
			}
			if (dns.Length != 0 || permitted.Count != 0)
			{
				throw new PkixNameConstraintValidatorException("DNS is not from a permitted subtree.");
			}
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0013F0E4 File Offset: 0x0013F0E4
		private void CheckExcludedDns(ISet excluded, string dns)
		{
			foreach (object obj in excluded)
			{
				string text = (string)obj;
				if (this.WithinDomain(dns, text) || Platform.EqualsIgnoreCase(dns, text))
				{
					throw new PkixNameConstraintValidatorException("DNS is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0013F160 File Offset: 0x0013F160
		private void UnionEmail(string email1, string email2, ISet union)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (Platform.EqualsIgnoreCase(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email2);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string a = email2.Substring(email1.IndexOf('@') + 1);
				if (Platform.EqualsIgnoreCase(a, email1))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					union.Add(email2);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else
			{
				if (Platform.EqualsIgnoreCase(email1, email2))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x0013F37C File Offset: 0x0013F37C
		private void unionURI(string email1, string email2, ISet union)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (Platform.EqualsIgnoreCase(text, email2))
					{
						union.Add(email2);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						union.Add(email2);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
				else
				{
					if (this.WithinDomain(email2, email1))
					{
						union.Add(email1);
						return;
					}
					union.Add(email1);
					union.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string a = email2.Substring(email1.IndexOf('@') + 1);
				if (Platform.EqualsIgnoreCase(a, email1))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					union.Add(email2);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
			else
			{
				if (Platform.EqualsIgnoreCase(email1, email2))
				{
					union.Add(email1);
					return;
				}
				union.Add(email1);
				union.Add(email2);
				return;
			}
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x0013F598 File Offset: 0x0013F598
		private ISet intersectDNS(ISet permitted, ISet dnss)
		{
			ISet set = new HashSet();
			foreach (object obj in dnss)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string text2 = (string)obj2;
						if (this.WithinDomain(text2, text))
						{
							set.Add(text2);
						}
						else if (this.WithinDomain(text, text2))
						{
							set.Add(text);
						}
					}
				}
			}
			return set;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x0013F648 File Offset: 0x0013F648
		protected ISet unionDNS(ISet excluded, string dns)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string text = (string)obj;
					if (this.WithinDomain(text, dns))
					{
						set.Add(dns);
					}
					else if (this.WithinDomain(dns, text))
					{
						set.Add(text);
					}
					else
					{
						set.Add(text);
						set.Add(dns);
					}
				}
				return set;
			}
			if (dns == null)
			{
				return excluded;
			}
			excluded.Add(dns);
			return excluded;
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x0013F6DC File Offset: 0x0013F6DC
		private void intersectEmail(string email1, string email2, ISet intersect)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.EqualsIgnoreCase(text, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (this.WithinDomain(email2, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string a = email2.Substring(email2.IndexOf('@') + 1);
				if (Platform.EqualsIgnoreCase(a, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.EqualsIgnoreCase(email1, email2))
			{
				intersect.Add(email1);
			}
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x0013F874 File Offset: 0x0013F874
		private void checkExcludedURI(ISet excluded, string uri)
		{
			if (excluded.IsEmpty)
			{
				return;
			}
			foreach (object obj in excluded)
			{
				string constraint = (string)obj;
				if (this.IsUriConstrained(uri, constraint))
				{
					throw new PkixNameConstraintValidatorException("URI is from an excluded subtree.");
				}
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0013F8CC File Offset: 0x0013F8CC
		private ISet intersectURI(ISet permitted, ISet uris)
		{
			ISet set = new HashSet();
			foreach (object obj in uris)
			{
				string text = this.ExtractNameAsString(((GeneralSubtree)obj).Base);
				if (permitted == null)
				{
					if (text != null)
					{
						set.Add(text);
					}
				}
				else
				{
					foreach (object obj2 in permitted)
					{
						string email = (string)obj2;
						this.intersectURI(email, text, set);
					}
				}
			}
			return set;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x0013F954 File Offset: 0x0013F954
		private ISet unionURI(ISet excluded, string uri)
		{
			if (!excluded.IsEmpty)
			{
				ISet set = new HashSet();
				foreach (object obj in excluded)
				{
					string email = (string)obj;
					this.unionURI(email, uri, set);
				}
				return set;
			}
			if (uri == null)
			{
				return excluded;
			}
			excluded.Add(uri);
			return excluded;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0013F9B4 File Offset: 0x0013F9B4
		private void intersectURI(string email1, string email2, ISet intersect)
		{
			if (email1.IndexOf('@') != -1)
			{
				string text = email1.Substring(email1.IndexOf('@') + 1);
				if (email2.IndexOf('@') != -1)
				{
					if (Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(text, email2))
					{
						intersect.Add(email1);
						return;
					}
				}
				else if (Platform.EqualsIgnoreCase(text, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.StartsWith(email1, "."))
			{
				if (email2.IndexOf('@') != -1)
				{
					string testDomain = email2.Substring(email1.IndexOf('@') + 1);
					if (this.WithinDomain(testDomain, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (Platform.StartsWith(email2, "."))
				{
					if (this.WithinDomain(email1, email2) || Platform.EqualsIgnoreCase(email1, email2))
					{
						intersect.Add(email1);
						return;
					}
					if (this.WithinDomain(email2, email1))
					{
						intersect.Add(email2);
						return;
					}
				}
				else if (this.WithinDomain(email2, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (email2.IndexOf('@') != -1)
			{
				string a = email2.Substring(email2.IndexOf('@') + 1);
				if (Platform.EqualsIgnoreCase(a, email1))
				{
					intersect.Add(email2);
					return;
				}
			}
			else if (Platform.StartsWith(email2, "."))
			{
				if (this.WithinDomain(email1, email2))
				{
					intersect.Add(email1);
					return;
				}
			}
			else if (Platform.EqualsIgnoreCase(email1, email2))
			{
				intersect.Add(email1);
			}
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x0013FB4C File Offset: 0x0013FB4C
		private void CheckPermittedURI(ISet permitted, string uri)
		{
			if (permitted == null)
			{
				return;
			}
			foreach (object obj in permitted)
			{
				string constraint = (string)obj;
				if (this.IsUriConstrained(uri, constraint))
				{
					return;
				}
			}
			if (uri.Length == 0 && permitted.Count == 0)
			{
				return;
			}
			throw new PkixNameConstraintValidatorException("URI is not from a permitted subtree.");
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x0013FBB4 File Offset: 0x0013FBB4
		private bool IsUriConstrained(string uri, string constraint)
		{
			string text = PkixNameConstraintValidator.ExtractHostFromURL(uri);
			if (!Platform.StartsWith(constraint, "."))
			{
				if (Platform.EqualsIgnoreCase(text, constraint))
				{
					return true;
				}
			}
			else if (this.WithinDomain(text, constraint))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x0013FBFC File Offset: 0x0013FBFC
		private static string ExtractHostFromURL(string url)
		{
			string text = url.Substring(url.IndexOf(':') + 1);
			int num = Platform.IndexOf(text, "//");
			if (num != -1)
			{
				text = text.Substring(num + 2);
			}
			if (text.LastIndexOf(':') != -1)
			{
				text = text.Substring(0, text.LastIndexOf(':'));
			}
			text = text.Substring(text.IndexOf(':') + 1);
			text = text.Substring(text.IndexOf('@') + 1);
			if (text.IndexOf('/') != -1)
			{
				text = text.Substring(0, text.IndexOf('/'));
			}
			return text;
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x0013FC9C File Offset: 0x0013FC9C
		public void checkPermitted(GeneralName name)
		{
			switch (name.TagNo)
			{
			case 1:
				this.CheckPermittedEmail(this.permittedSubtreesEmail, this.ExtractNameAsString(name));
				return;
			case 2:
				this.CheckPermittedDns(this.permittedSubtreesDNS, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.CheckPermittedDN(Asn1Sequence.GetInstance(name.Name.ToAsn1Object()));
				return;
			case 6:
				this.CheckPermittedURI(this.permittedSubtreesURI, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 7:
			{
				byte[] octets = Asn1OctetString.GetInstance(name.Name).GetOctets();
				this.CheckPermittedIP(this.permittedSubtreesIP, octets);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x0013FD5C File Offset: 0x0013FD5C
		public void checkExcluded(GeneralName name)
		{
			switch (name.TagNo)
			{
			case 1:
				this.CheckExcludedEmail(this.excludedSubtreesEmail, this.ExtractNameAsString(name));
				return;
			case 2:
				this.CheckExcludedDns(this.excludedSubtreesDNS, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.CheckExcludedDN(Asn1Sequence.GetInstance(name.Name.ToAsn1Object()));
				return;
			case 6:
				this.checkExcludedURI(this.excludedSubtreesURI, DerIA5String.GetInstance(name.Name).GetString());
				return;
			case 7:
			{
				byte[] octets = Asn1OctetString.GetInstance(name.Name).GetOctets();
				this.CheckExcludedIP(this.excludedSubtreesIP, octets);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x0013FE1C File Offset: 0x0013FE1C
		public void IntersectPermittedSubtree(Asn1Sequence permitted)
		{
			IDictionary dictionary = Platform.CreateHashtable();
			foreach (object obj in permitted)
			{
				GeneralSubtree instance = GeneralSubtree.GetInstance(obj);
				int tagNo = instance.Base.TagNo;
				if (dictionary[tagNo] == null)
				{
					dictionary[tagNo] = new HashSet();
				}
				((ISet)dictionary[tagNo]).Add(instance);
			}
			foreach (object obj2 in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				switch ((int)dictionaryEntry.Key)
				{
				case 1:
					this.permittedSubtreesEmail = this.IntersectEmail(this.permittedSubtreesEmail, (ISet)dictionaryEntry.Value);
					break;
				case 2:
					this.permittedSubtreesDNS = this.intersectDNS(this.permittedSubtreesDNS, (ISet)dictionaryEntry.Value);
					break;
				case 4:
					this.permittedSubtreesDN = this.IntersectDN(this.permittedSubtreesDN, (ISet)dictionaryEntry.Value);
					break;
				case 6:
					this.permittedSubtreesURI = this.intersectURI(this.permittedSubtreesURI, (ISet)dictionaryEntry.Value);
					break;
				case 7:
					this.permittedSubtreesIP = this.IntersectIP(this.permittedSubtreesIP, (ISet)dictionaryEntry.Value);
					break;
				}
			}
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x0013FFA8 File Offset: 0x0013FFA8
		private string ExtractNameAsString(GeneralName name)
		{
			return DerIA5String.GetInstance(name.Name).GetString();
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x0013FFBC File Offset: 0x0013FFBC
		public void IntersectEmptyPermittedSubtree(int nameType)
		{
			switch (nameType)
			{
			case 1:
				this.permittedSubtreesEmail = new HashSet();
				return;
			case 2:
				this.permittedSubtreesDNS = new HashSet();
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.permittedSubtreesDN = new HashSet();
				return;
			case 6:
				this.permittedSubtreesURI = new HashSet();
				return;
			case 7:
				this.permittedSubtreesIP = new HashSet();
				break;
			default:
				return;
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x00140030 File Offset: 0x00140030
		public void AddExcludedSubtree(GeneralSubtree subtree)
		{
			GeneralName @base = subtree.Base;
			switch (@base.TagNo)
			{
			case 1:
				this.excludedSubtreesEmail = this.UnionEmail(this.excludedSubtreesEmail, this.ExtractNameAsString(@base));
				return;
			case 2:
				this.excludedSubtreesDNS = this.unionDNS(this.excludedSubtreesDNS, this.ExtractNameAsString(@base));
				return;
			case 3:
			case 5:
				break;
			case 4:
				this.excludedSubtreesDN = this.UnionDN(this.excludedSubtreesDN, (Asn1Sequence)@base.Name.ToAsn1Object());
				return;
			case 6:
				this.excludedSubtreesURI = this.unionURI(this.excludedSubtreesURI, this.ExtractNameAsString(@base));
				return;
			case 7:
				this.excludedSubtreesIP = this.UnionIP(this.excludedSubtreesIP, Asn1OctetString.GetInstance(@base.Name).GetOctets());
				break;
			default:
				return;
			}
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x00140108 File Offset: 0x00140108
		private static byte[] Max(byte[] ip1, byte[] ip2)
		{
			for (int i = 0; i < ip1.Length; i++)
			{
				if (((int)ip1[i] & 65535) > ((int)ip2[i] & 65535))
				{
					return ip1;
				}
			}
			return ip2;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x00140144 File Offset: 0x00140144
		private static byte[] Min(byte[] ip1, byte[] ip2)
		{
			for (int i = 0; i < ip1.Length; i++)
			{
				if (((int)ip1[i] & 65535) < ((int)ip2[i] & 65535))
				{
					return ip1;
				}
			}
			return ip2;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x00140180 File Offset: 0x00140180
		private static int CompareTo(byte[] ip1, byte[] ip2)
		{
			if (Arrays.AreEqual(ip1, ip2))
			{
				return 0;
			}
			if (Arrays.AreEqual(PkixNameConstraintValidator.Max(ip1, ip2), ip1))
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x001401A8 File Offset: 0x001401A8
		private static byte[] Or(byte[] ip1, byte[] ip2)
		{
			byte[] array = new byte[ip1.Length];
			for (int i = 0; i < ip1.Length; i++)
			{
				array[i] = (ip1[i] | ip2[i]);
			}
			return array;
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x001401E0 File Offset: 0x001401E0
		[Obsolete("Use GetHashCode instead")]
		public int HashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x001401E8 File Offset: 0x001401E8
		public override int GetHashCode()
		{
			return this.HashCollection(this.excludedSubtreesDN) + this.HashCollection(this.excludedSubtreesDNS) + this.HashCollection(this.excludedSubtreesEmail) + this.HashCollection(this.excludedSubtreesIP) + this.HashCollection(this.excludedSubtreesURI) + this.HashCollection(this.permittedSubtreesDN) + this.HashCollection(this.permittedSubtreesDNS) + this.HashCollection(this.permittedSubtreesEmail) + this.HashCollection(this.permittedSubtreesIP) + this.HashCollection(this.permittedSubtreesURI);
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x0014027C File Offset: 0x0014027C
		private int HashCollection(ICollection coll)
		{
			if (coll == null)
			{
				return 0;
			}
			int num = 0;
			foreach (object obj in coll)
			{
				if (obj is byte[])
				{
					num += Arrays.GetHashCode((byte[])obj);
				}
				else
				{
					num += obj.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x001402DC File Offset: 0x001402DC
		public override bool Equals(object o)
		{
			if (!(o is PkixNameConstraintValidator))
			{
				return false;
			}
			PkixNameConstraintValidator pkixNameConstraintValidator = (PkixNameConstraintValidator)o;
			return this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesDN, this.excludedSubtreesDN) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesDNS, this.excludedSubtreesDNS) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesEmail, this.excludedSubtreesEmail) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesIP, this.excludedSubtreesIP) && this.CollectionsAreEqual(pkixNameConstraintValidator.excludedSubtreesURI, this.excludedSubtreesURI) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesDN, this.permittedSubtreesDN) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesDNS, this.permittedSubtreesDNS) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesEmail, this.permittedSubtreesEmail) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesIP, this.permittedSubtreesIP) && this.CollectionsAreEqual(pkixNameConstraintValidator.permittedSubtreesURI, this.permittedSubtreesURI);
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x001403E4 File Offset: 0x001403E4
		private bool CollectionsAreEqual(ICollection coll1, ICollection coll2)
		{
			if (coll1 == coll2)
			{
				return true;
			}
			if (coll1 == null || coll2 == null)
			{
				return false;
			}
			if (coll1.Count != coll2.Count)
			{
				return false;
			}
			foreach (object o in coll1)
			{
				IEnumerator enumerator2 = coll2.GetEnumerator();
				bool flag = false;
				while (enumerator2.MoveNext())
				{
					object o2 = enumerator2.Current;
					if (this.SpecialEquals(o, o2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x00140478 File Offset: 0x00140478
		private bool SpecialEquals(object o1, object o2)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			if (o1 is byte[] && o2 is byte[])
			{
				return Arrays.AreEqual((byte[])o1, (byte[])o2);
			}
			return o1.Equals(o2);
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x001404D0 File Offset: 0x001404D0
		private string StringifyIP(byte[] ip)
		{
			string text = "";
			for (int i = 0; i < ip.Length / 2; i++)
			{
				text = text + (int)(ip[i] & byte.MaxValue) + ".";
			}
			text = text.Substring(0, text.Length - 1);
			text += "/";
			for (int j = ip.Length / 2; j < ip.Length; j++)
			{
				text = text + (int)(ip[j] & byte.MaxValue) + ".";
			}
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x00140570 File Offset: 0x00140570
		private string StringifyIPCollection(ISet ips)
		{
			string text = "";
			text += "[";
			foreach (object obj in ips)
			{
				text = text + this.StringifyIP((byte[])obj) + ",";
			}
			if (text.Length > 1)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text + "]";
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x001405F0 File Offset: 0x001405F0
		public override string ToString()
		{
			string text = "";
			text += "permitted:\n";
			if (this.permittedSubtreesDN != null)
			{
				text += "DN:\n";
				text = text + this.permittedSubtreesDN.ToString() + "\n";
			}
			if (this.permittedSubtreesDNS != null)
			{
				text += "DNS:\n";
				text = text + this.permittedSubtreesDNS.ToString() + "\n";
			}
			if (this.permittedSubtreesEmail != null)
			{
				text += "Email:\n";
				text = text + this.permittedSubtreesEmail.ToString() + "\n";
			}
			if (this.permittedSubtreesURI != null)
			{
				text += "URI:\n";
				text = text + this.permittedSubtreesURI.ToString() + "\n";
			}
			if (this.permittedSubtreesIP != null)
			{
				text += "IP:\n";
				text = text + this.StringifyIPCollection(this.permittedSubtreesIP) + "\n";
			}
			text += "excluded:\n";
			if (!this.excludedSubtreesDN.IsEmpty)
			{
				text += "DN:\n";
				text = text + this.excludedSubtreesDN.ToString() + "\n";
			}
			if (!this.excludedSubtreesDNS.IsEmpty)
			{
				text += "DNS:\n";
				text = text + this.excludedSubtreesDNS.ToString() + "\n";
			}
			if (!this.excludedSubtreesEmail.IsEmpty)
			{
				text += "Email:\n";
				text = text + this.excludedSubtreesEmail.ToString() + "\n";
			}
			if (!this.excludedSubtreesURI.IsEmpty)
			{
				text += "URI:\n";
				text = text + this.excludedSubtreesURI.ToString() + "\n";
			}
			if (!this.excludedSubtreesIP.IsEmpty)
			{
				text += "IP:\n";
				text = text + this.StringifyIPCollection(this.excludedSubtreesIP) + "\n";
			}
			return text;
		}

		// Token: 0x04001E75 RID: 7797
		private static readonly DerObjectIdentifier SerialNumberOid = new DerObjectIdentifier("2.5.4.5");

		// Token: 0x04001E76 RID: 7798
		private ISet excludedSubtreesDN = new HashSet();

		// Token: 0x04001E77 RID: 7799
		private ISet excludedSubtreesDNS = new HashSet();

		// Token: 0x04001E78 RID: 7800
		private ISet excludedSubtreesEmail = new HashSet();

		// Token: 0x04001E79 RID: 7801
		private ISet excludedSubtreesURI = new HashSet();

		// Token: 0x04001E7A RID: 7802
		private ISet excludedSubtreesIP = new HashSet();

		// Token: 0x04001E7B RID: 7803
		private ISet permittedSubtreesDN;

		// Token: 0x04001E7C RID: 7804
		private ISet permittedSubtreesDNS;

		// Token: 0x04001E7D RID: 7805
		private ISet permittedSubtreesEmail;

		// Token: 0x04001E7E RID: 7806
		private ISet permittedSubtreesURI;

		// Token: 0x04001E7F RID: 7807
		private ISet permittedSubtreesIP;
	}
}
