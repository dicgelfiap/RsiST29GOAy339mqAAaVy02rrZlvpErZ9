using System;
using System.Collections;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000224 RID: 548
	public class X509Name : Asn1Encodable
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00064128 File Offset: 0x00064128
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x00064134 File Offset: 0x00064134
		public static bool DefaultReverse
		{
			get
			{
				return X509Name.defaultReverse[0];
			}
			set
			{
				X509Name.defaultReverse[0] = value;
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00064140 File Offset: 0x00064140
		static X509Name()
		{
			bool[] array = new bool[1];
			X509Name.defaultReverse = array;
			X509Name.DefaultSymbols = new Hashtable();
			X509Name.RFC2253Symbols = new Hashtable();
			X509Name.RFC1779Symbols = new Hashtable();
			X509Name.DefaultLookup = new Hashtable();
			X509Name.DefaultSymbols.Add(X509Name.C, "C");
			X509Name.DefaultSymbols.Add(X509Name.O, "O");
			X509Name.DefaultSymbols.Add(X509Name.T, "T");
			X509Name.DefaultSymbols.Add(X509Name.OU, "OU");
			X509Name.DefaultSymbols.Add(X509Name.CN, "CN");
			X509Name.DefaultSymbols.Add(X509Name.L, "L");
			X509Name.DefaultSymbols.Add(X509Name.ST, "ST");
			X509Name.DefaultSymbols.Add(X509Name.SerialNumber, "SERIALNUMBER");
			X509Name.DefaultSymbols.Add(X509Name.EmailAddress, "E");
			X509Name.DefaultSymbols.Add(X509Name.DC, "DC");
			X509Name.DefaultSymbols.Add(X509Name.UID, "UID");
			X509Name.DefaultSymbols.Add(X509Name.Street, "STREET");
			X509Name.DefaultSymbols.Add(X509Name.Surname, "SURNAME");
			X509Name.DefaultSymbols.Add(X509Name.GivenName, "GIVENNAME");
			X509Name.DefaultSymbols.Add(X509Name.Initials, "INITIALS");
			X509Name.DefaultSymbols.Add(X509Name.Generation, "GENERATION");
			X509Name.DefaultSymbols.Add(X509Name.UnstructuredAddress, "unstructuredAddress");
			X509Name.DefaultSymbols.Add(X509Name.UnstructuredName, "unstructuredName");
			X509Name.DefaultSymbols.Add(X509Name.UniqueIdentifier, "UniqueIdentifier");
			X509Name.DefaultSymbols.Add(X509Name.DnQualifier, "DN");
			X509Name.DefaultSymbols.Add(X509Name.Pseudonym, "Pseudonym");
			X509Name.DefaultSymbols.Add(X509Name.PostalAddress, "PostalAddress");
			X509Name.DefaultSymbols.Add(X509Name.NameAtBirth, "NameAtBirth");
			X509Name.DefaultSymbols.Add(X509Name.CountryOfCitizenship, "CountryOfCitizenship");
			X509Name.DefaultSymbols.Add(X509Name.CountryOfResidence, "CountryOfResidence");
			X509Name.DefaultSymbols.Add(X509Name.Gender, "Gender");
			X509Name.DefaultSymbols.Add(X509Name.PlaceOfBirth, "PlaceOfBirth");
			X509Name.DefaultSymbols.Add(X509Name.DateOfBirth, "DateOfBirth");
			X509Name.DefaultSymbols.Add(X509Name.PostalCode, "PostalCode");
			X509Name.DefaultSymbols.Add(X509Name.BusinessCategory, "BusinessCategory");
			X509Name.DefaultSymbols.Add(X509Name.TelephoneNumber, "TelephoneNumber");
			X509Name.RFC2253Symbols.Add(X509Name.C, "C");
			X509Name.RFC2253Symbols.Add(X509Name.O, "O");
			X509Name.RFC2253Symbols.Add(X509Name.OU, "OU");
			X509Name.RFC2253Symbols.Add(X509Name.CN, "CN");
			X509Name.RFC2253Symbols.Add(X509Name.L, "L");
			X509Name.RFC2253Symbols.Add(X509Name.ST, "ST");
			X509Name.RFC2253Symbols.Add(X509Name.Street, "STREET");
			X509Name.RFC2253Symbols.Add(X509Name.DC, "DC");
			X509Name.RFC2253Symbols.Add(X509Name.UID, "UID");
			X509Name.RFC1779Symbols.Add(X509Name.C, "C");
			X509Name.RFC1779Symbols.Add(X509Name.O, "O");
			X509Name.RFC1779Symbols.Add(X509Name.OU, "OU");
			X509Name.RFC1779Symbols.Add(X509Name.CN, "CN");
			X509Name.RFC1779Symbols.Add(X509Name.L, "L");
			X509Name.RFC1779Symbols.Add(X509Name.ST, "ST");
			X509Name.RFC1779Symbols.Add(X509Name.Street, "STREET");
			X509Name.DefaultLookup.Add("c", X509Name.C);
			X509Name.DefaultLookup.Add("o", X509Name.O);
			X509Name.DefaultLookup.Add("t", X509Name.T);
			X509Name.DefaultLookup.Add("ou", X509Name.OU);
			X509Name.DefaultLookup.Add("cn", X509Name.CN);
			X509Name.DefaultLookup.Add("l", X509Name.L);
			X509Name.DefaultLookup.Add("st", X509Name.ST);
			X509Name.DefaultLookup.Add("serialnumber", X509Name.SerialNumber);
			X509Name.DefaultLookup.Add("street", X509Name.Street);
			X509Name.DefaultLookup.Add("emailaddress", X509Name.E);
			X509Name.DefaultLookup.Add("dc", X509Name.DC);
			X509Name.DefaultLookup.Add("e", X509Name.E);
			X509Name.DefaultLookup.Add("uid", X509Name.UID);
			X509Name.DefaultLookup.Add("surname", X509Name.Surname);
			X509Name.DefaultLookup.Add("givenname", X509Name.GivenName);
			X509Name.DefaultLookup.Add("initials", X509Name.Initials);
			X509Name.DefaultLookup.Add("generation", X509Name.Generation);
			X509Name.DefaultLookup.Add("unstructuredaddress", X509Name.UnstructuredAddress);
			X509Name.DefaultLookup.Add("unstructuredname", X509Name.UnstructuredName);
			X509Name.DefaultLookup.Add("uniqueidentifier", X509Name.UniqueIdentifier);
			X509Name.DefaultLookup.Add("dn", X509Name.DnQualifier);
			X509Name.DefaultLookup.Add("pseudonym", X509Name.Pseudonym);
			X509Name.DefaultLookup.Add("postaladdress", X509Name.PostalAddress);
			X509Name.DefaultLookup.Add("nameofbirth", X509Name.NameAtBirth);
			X509Name.DefaultLookup.Add("countryofcitizenship", X509Name.CountryOfCitizenship);
			X509Name.DefaultLookup.Add("countryofresidence", X509Name.CountryOfResidence);
			X509Name.DefaultLookup.Add("gender", X509Name.Gender);
			X509Name.DefaultLookup.Add("placeofbirth", X509Name.PlaceOfBirth);
			X509Name.DefaultLookup.Add("dateofbirth", X509Name.DateOfBirth);
			X509Name.DefaultLookup.Add("postalcode", X509Name.PostalCode);
			X509Name.DefaultLookup.Add("businesscategory", X509Name.BusinessCategory);
			X509Name.DefaultLookup.Add("telephonenumber", X509Name.TelephoneNumber);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0006499C File Offset: 0x0006499C
		public static X509Name GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509Name.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000649AC File Offset: 0x000649AC
		public static X509Name GetInstance(object obj)
		{
			if (obj is X509Name)
			{
				return (X509Name)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new X509Name(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000649D4 File Offset: 0x000649D4
		protected X509Name()
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00064A00 File Offset: 0x00064A00
		protected X509Name(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				Asn1Set instance = Asn1Set.GetInstance(asn1Encodable.ToAsn1Object());
				for (int i = 0; i < instance.Count; i++)
				{
					Asn1Sequence instance2 = Asn1Sequence.GetInstance(instance[i].ToAsn1Object());
					if (instance2.Count != 2)
					{
						throw new ArgumentException("badly sized pair");
					}
					this.ordering.Add(DerObjectIdentifier.GetInstance(instance2[0].ToAsn1Object()));
					Asn1Object asn1Object = instance2[1].ToAsn1Object();
					if (asn1Object is IAsn1String && !(asn1Object is DerUniversalString))
					{
						string text = ((IAsn1String)asn1Object).GetString();
						if (Platform.StartsWith(text, "#"))
						{
							text = "\\" + text;
						}
						this.values.Add(text);
					}
					else
					{
						this.values.Add("#" + Hex.ToHexString(asn1Object.GetEncoded()));
					}
					this.added.Add(i != 0);
				}
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00064BA4 File Offset: 0x00064BA4
		public X509Name(IList ordering, IDictionary attributes) : this(ordering, attributes, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00064BB4 File Offset: 0x00064BB4
		public X509Name(IList ordering, IDictionary attributes, X509NameEntryConverter converter)
		{
			this.converter = converter;
			foreach (object obj in ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				object obj2 = attributes[derObjectIdentifier];
				if (obj2 == null)
				{
					throw new ArgumentException("No attribute for object id - " + derObjectIdentifier + " - passed to distinguished name");
				}
				this.ordering.Add(derObjectIdentifier);
				this.added.Add(false);
				this.values.Add(obj2);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00064C88 File Offset: 0x00064C88
		public X509Name(IList oids, IList values) : this(oids, values, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00064C98 File Offset: 0x00064C98
		public X509Name(IList oids, IList values, X509NameEntryConverter converter)
		{
			this.converter = converter;
			if (oids.Count != values.Count)
			{
				throw new ArgumentException("'oids' must be same length as 'values'.");
			}
			for (int i = 0; i < oids.Count; i++)
			{
				this.ordering.Add(oids[i]);
				this.values.Add(values[i]);
				this.added.Add(false);
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00064D40 File Offset: 0x00064D40
		public X509Name(string dirName) : this(X509Name.DefaultReverse, X509Name.DefaultLookup, dirName)
		{
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00064D54 File Offset: 0x00064D54
		public X509Name(string dirName, X509NameEntryConverter converter) : this(X509Name.DefaultReverse, X509Name.DefaultLookup, dirName, converter)
		{
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00064D68 File Offset: 0x00064D68
		public X509Name(bool reverse, string dirName) : this(reverse, X509Name.DefaultLookup, dirName)
		{
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00064D78 File Offset: 0x00064D78
		public X509Name(bool reverse, string dirName, X509NameEntryConverter converter) : this(reverse, X509Name.DefaultLookup, dirName, converter)
		{
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00064D88 File Offset: 0x00064D88
		public X509Name(bool reverse, IDictionary lookUp, string dirName) : this(reverse, lookUp, dirName, new X509DefaultEntryConverter())
		{
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00064D98 File Offset: 0x00064D98
		private DerObjectIdentifier DecodeOid(string name, IDictionary lookUp)
		{
			if (Platform.StartsWith(Platform.ToUpperInvariant(name), "OID."))
			{
				return new DerObjectIdentifier(name.Substring(4));
			}
			if (name[0] >= '0' && name[0] <= '9')
			{
				return new DerObjectIdentifier(name);
			}
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)lookUp[Platform.ToLowerInvariant(name)];
			if (derObjectIdentifier == null)
			{
				throw new ArgumentException("Unknown object id - " + name + " - passed to distinguished name");
			}
			return derObjectIdentifier;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00064E20 File Offset: 0x00064E20
		public X509Name(bool reverse, IDictionary lookUp, string dirName, X509NameEntryConverter converter)
		{
			this.converter = converter;
			X509NameTokenizer x509NameTokenizer = new X509NameTokenizer(dirName);
			while (x509NameTokenizer.HasMoreTokens())
			{
				string text = x509NameTokenizer.NextToken();
				int num = text.IndexOf('=');
				if (num == -1)
				{
					throw new ArgumentException("badly formated directory string");
				}
				string name = text.Substring(0, num);
				string text2 = text.Substring(num + 1);
				DerObjectIdentifier value = this.DecodeOid(name, lookUp);
				if (text2.IndexOf('+') > 0)
				{
					X509NameTokenizer x509NameTokenizer2 = new X509NameTokenizer(text2, '+');
					string value2 = x509NameTokenizer2.NextToken();
					this.ordering.Add(value);
					this.values.Add(value2);
					this.added.Add(false);
					while (x509NameTokenizer2.HasMoreTokens())
					{
						string text3 = x509NameTokenizer2.NextToken();
						int num2 = text3.IndexOf('=');
						string name2 = text3.Substring(0, num2);
						string value3 = text3.Substring(num2 + 1);
						this.ordering.Add(this.DecodeOid(name2, lookUp));
						this.values.Add(value3);
						this.added.Add(true);
					}
				}
				else
				{
					this.ordering.Add(value);
					this.values.Add(text2);
					this.added.Add(false);
				}
			}
			if (reverse)
			{
				IList list = Platform.CreateArrayList();
				IList list2 = Platform.CreateArrayList();
				IList list3 = Platform.CreateArrayList();
				int num3 = 1;
				for (int i = 0; i < this.ordering.Count; i++)
				{
					if (!(bool)this.added[i])
					{
						num3 = 0;
					}
					int index = num3++;
					list.Insert(index, this.ordering[i]);
					list2.Insert(index, this.values[i]);
					list3.Insert(index, this.added[i]);
				}
				this.ordering = list;
				this.values = list2;
				this.added = list3;
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00065064 File Offset: 0x00065064
		public IList GetOidList()
		{
			return Platform.CreateArrayList(this.ordering);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00065074 File Offset: 0x00065074
		public IList GetValueList()
		{
			return this.GetValueList(null);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00065080 File Offset: 0x00065080
		public IList GetValueList(DerObjectIdentifier oid)
		{
			IList list = Platform.CreateArrayList();
			for (int num = 0; num != this.values.Count; num++)
			{
				if (oid == null || oid.Equals(this.ordering[num]))
				{
					string text = (string)this.values[num];
					if (Platform.StartsWith(text, "\\#"))
					{
						text = text.Substring(1);
					}
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00065100 File Offset: 0x00065100
		public override Asn1Object ToAsn1Object()
		{
			if (this.seq == null)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector();
				DerObjectIdentifier derObjectIdentifier = null;
				for (int num = 0; num != this.ordering.Count; num++)
				{
					DerObjectIdentifier derObjectIdentifier2 = (DerObjectIdentifier)this.ordering[num];
					string value = (string)this.values[num];
					if (derObjectIdentifier != null && !(bool)this.added[num])
					{
						asn1EncodableVector.Add(new DerSet(asn1EncodableVector2));
						asn1EncodableVector2 = new Asn1EncodableVector();
					}
					asn1EncodableVector2.Add(new DerSequence(new Asn1Encodable[]
					{
						derObjectIdentifier2,
						this.converter.GetConvertedValue(derObjectIdentifier2, value)
					}));
					derObjectIdentifier = derObjectIdentifier2;
				}
				asn1EncodableVector.Add(new DerSet(asn1EncodableVector2));
				this.seq = new DerSequence(asn1EncodableVector);
			}
			return this.seq;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000651F4 File Offset: 0x000651F4
		public bool Equivalent(X509Name other, bool inOrder)
		{
			if (!inOrder)
			{
				return this.Equivalent(other);
			}
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			int count = this.ordering.Count;
			if (count != other.ordering.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)this.ordering[i];
				DerObjectIdentifier obj = (DerObjectIdentifier)other.ordering[i];
				if (!derObjectIdentifier.Equals(obj))
				{
					return false;
				}
				string s = (string)this.values[i];
				string s2 = (string)other.values[i];
				if (!X509Name.equivalentStrings(s, s2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000652BC File Offset: 0x000652BC
		public bool Equivalent(X509Name other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			int count = this.ordering.Count;
			if (count != other.ordering.Count)
			{
				return false;
			}
			bool[] array = new bool[count];
			int num;
			int num2;
			int num3;
			if (this.ordering[0].Equals(other.ordering[0]))
			{
				num = 0;
				num2 = count;
				num3 = 1;
			}
			else
			{
				num = count - 1;
				num2 = -1;
				num3 = -1;
			}
			for (int num4 = num; num4 != num2; num4 += num3)
			{
				bool flag = false;
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)this.ordering[num4];
				string s = (string)this.values[num4];
				for (int i = 0; i < count; i++)
				{
					if (!array[i])
					{
						DerObjectIdentifier obj = (DerObjectIdentifier)other.ordering[i];
						if (derObjectIdentifier.Equals(obj))
						{
							string s2 = (string)other.values[i];
							if (X509Name.equivalentStrings(s, s2))
							{
								array[i] = true;
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000653F4 File Offset: 0x000653F4
		private static bool equivalentStrings(string s1, string s2)
		{
			string text = X509Name.canonicalize(s1);
			string text2 = X509Name.canonicalize(s2);
			if (!text.Equals(text2))
			{
				text = X509Name.stripInternalSpaces(text);
				text2 = X509Name.stripInternalSpaces(text2);
				if (!text.Equals(text2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0006543C File Offset: 0x0006543C
		private static string canonicalize(string s)
		{
			string text = Platform.ToLowerInvariant(s).Trim();
			if (Platform.StartsWith(text, "#"))
			{
				Asn1Object asn1Object = X509Name.decodeObject(text);
				if (asn1Object is IAsn1String)
				{
					text = Platform.ToLowerInvariant(((IAsn1String)asn1Object).GetString()).Trim();
				}
			}
			return text;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00065494 File Offset: 0x00065494
		private static Asn1Object decodeObject(string v)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(Hex.DecodeStrict(v, 1, v.Length - 1));
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("unknown encoding in name: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000654E4 File Offset: 0x000654E4
		private static string stripInternalSpaces(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (str.Length != 0)
			{
				char c = str[0];
				stringBuilder.Append(c);
				for (int i = 1; i < str.Length; i++)
				{
					char c2 = str[i];
					if (c != ' ' || c2 != ' ')
					{
						stringBuilder.Append(c2);
					}
					c = c2;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00065554 File Offset: 0x00065554
		private void AppendValue(StringBuilder buf, IDictionary oidSymbols, DerObjectIdentifier oid, string val)
		{
			string text = (string)oidSymbols[oid];
			if (text != null)
			{
				buf.Append(text);
			}
			else
			{
				buf.Append(oid.Id);
			}
			buf.Append('=');
			int num = buf.Length;
			buf.Append(val);
			int num2 = buf.Length;
			if (Platform.StartsWith(val, "\\#"))
			{
				num += 2;
			}
			while (num != num2)
			{
				if (buf[num] == ',' || buf[num] == '"' || buf[num] == '\\' || buf[num] == '+' || buf[num] == '=' || buf[num] == '<' || buf[num] == '>' || buf[num] == ';')
				{
					buf.Insert(num++, "\\");
					num2++;
				}
				num++;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0006565C File Offset: 0x0006565C
		public string ToString(bool reverse, IDictionary oidSymbols)
		{
			ArrayList arrayList = new ArrayList();
			StringBuilder stringBuilder = null;
			for (int i = 0; i < this.ordering.Count; i++)
			{
				if ((bool)this.added[i])
				{
					stringBuilder.Append('+');
					this.AppendValue(stringBuilder, oidSymbols, (DerObjectIdentifier)this.ordering[i], (string)this.values[i]);
				}
				else
				{
					stringBuilder = new StringBuilder();
					this.AppendValue(stringBuilder, oidSymbols, (DerObjectIdentifier)this.ordering[i], (string)this.values[i]);
					arrayList.Add(stringBuilder);
				}
			}
			if (reverse)
			{
				arrayList.Reverse();
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (arrayList.Count > 0)
			{
				stringBuilder2.Append(arrayList[0].ToString());
				for (int j = 1; j < arrayList.Count; j++)
				{
					stringBuilder2.Append(',');
					stringBuilder2.Append(arrayList[j].ToString());
				}
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00065784 File Offset: 0x00065784
		public override string ToString()
		{
			return this.ToString(X509Name.DefaultReverse, X509Name.DefaultSymbols);
		}

		// Token: 0x04000CAE RID: 3246
		public static readonly DerObjectIdentifier C = new DerObjectIdentifier("2.5.4.6");

		// Token: 0x04000CAF RID: 3247
		public static readonly DerObjectIdentifier O = new DerObjectIdentifier("2.5.4.10");

		// Token: 0x04000CB0 RID: 3248
		public static readonly DerObjectIdentifier OU = new DerObjectIdentifier("2.5.4.11");

		// Token: 0x04000CB1 RID: 3249
		public static readonly DerObjectIdentifier T = new DerObjectIdentifier("2.5.4.12");

		// Token: 0x04000CB2 RID: 3250
		public static readonly DerObjectIdentifier CN = new DerObjectIdentifier("2.5.4.3");

		// Token: 0x04000CB3 RID: 3251
		public static readonly DerObjectIdentifier Street = new DerObjectIdentifier("2.5.4.9");

		// Token: 0x04000CB4 RID: 3252
		public static readonly DerObjectIdentifier SerialNumber = new DerObjectIdentifier("2.5.4.5");

		// Token: 0x04000CB5 RID: 3253
		public static readonly DerObjectIdentifier L = new DerObjectIdentifier("2.5.4.7");

		// Token: 0x04000CB6 RID: 3254
		public static readonly DerObjectIdentifier ST = new DerObjectIdentifier("2.5.4.8");

		// Token: 0x04000CB7 RID: 3255
		public static readonly DerObjectIdentifier Surname = new DerObjectIdentifier("2.5.4.4");

		// Token: 0x04000CB8 RID: 3256
		public static readonly DerObjectIdentifier GivenName = new DerObjectIdentifier("2.5.4.42");

		// Token: 0x04000CB9 RID: 3257
		public static readonly DerObjectIdentifier Initials = new DerObjectIdentifier("2.5.4.43");

		// Token: 0x04000CBA RID: 3258
		public static readonly DerObjectIdentifier Generation = new DerObjectIdentifier("2.5.4.44");

		// Token: 0x04000CBB RID: 3259
		public static readonly DerObjectIdentifier UniqueIdentifier = new DerObjectIdentifier("2.5.4.45");

		// Token: 0x04000CBC RID: 3260
		public static readonly DerObjectIdentifier BusinessCategory = new DerObjectIdentifier("2.5.4.15");

		// Token: 0x04000CBD RID: 3261
		public static readonly DerObjectIdentifier PostalCode = new DerObjectIdentifier("2.5.4.17");

		// Token: 0x04000CBE RID: 3262
		public static readonly DerObjectIdentifier DnQualifier = new DerObjectIdentifier("2.5.4.46");

		// Token: 0x04000CBF RID: 3263
		public static readonly DerObjectIdentifier Pseudonym = new DerObjectIdentifier("2.5.4.65");

		// Token: 0x04000CC0 RID: 3264
		public static readonly DerObjectIdentifier DateOfBirth = new DerObjectIdentifier("1.3.6.1.5.5.7.9.1");

		// Token: 0x04000CC1 RID: 3265
		public static readonly DerObjectIdentifier PlaceOfBirth = new DerObjectIdentifier("1.3.6.1.5.5.7.9.2");

		// Token: 0x04000CC2 RID: 3266
		public static readonly DerObjectIdentifier Gender = new DerObjectIdentifier("1.3.6.1.5.5.7.9.3");

		// Token: 0x04000CC3 RID: 3267
		public static readonly DerObjectIdentifier CountryOfCitizenship = new DerObjectIdentifier("1.3.6.1.5.5.7.9.4");

		// Token: 0x04000CC4 RID: 3268
		public static readonly DerObjectIdentifier CountryOfResidence = new DerObjectIdentifier("1.3.6.1.5.5.7.9.5");

		// Token: 0x04000CC5 RID: 3269
		public static readonly DerObjectIdentifier NameAtBirth = new DerObjectIdentifier("1.3.36.8.3.14");

		// Token: 0x04000CC6 RID: 3270
		public static readonly DerObjectIdentifier PostalAddress = new DerObjectIdentifier("2.5.4.16");

		// Token: 0x04000CC7 RID: 3271
		public static readonly DerObjectIdentifier DmdName = new DerObjectIdentifier("2.5.4.54");

		// Token: 0x04000CC8 RID: 3272
		public static readonly DerObjectIdentifier TelephoneNumber = X509ObjectIdentifiers.id_at_telephoneNumber;

		// Token: 0x04000CC9 RID: 3273
		public static readonly DerObjectIdentifier OrganizationIdentifier = X509ObjectIdentifiers.id_at_organizationIdentifier;

		// Token: 0x04000CCA RID: 3274
		public static readonly DerObjectIdentifier Name = X509ObjectIdentifiers.id_at_name;

		// Token: 0x04000CCB RID: 3275
		public static readonly DerObjectIdentifier EmailAddress = PkcsObjectIdentifiers.Pkcs9AtEmailAddress;

		// Token: 0x04000CCC RID: 3276
		public static readonly DerObjectIdentifier UnstructuredName = PkcsObjectIdentifiers.Pkcs9AtUnstructuredName;

		// Token: 0x04000CCD RID: 3277
		public static readonly DerObjectIdentifier UnstructuredAddress = PkcsObjectIdentifiers.Pkcs9AtUnstructuredAddress;

		// Token: 0x04000CCE RID: 3278
		public static readonly DerObjectIdentifier E = X509Name.EmailAddress;

		// Token: 0x04000CCF RID: 3279
		public static readonly DerObjectIdentifier DC = new DerObjectIdentifier("0.9.2342.19200300.100.1.25");

		// Token: 0x04000CD0 RID: 3280
		public static readonly DerObjectIdentifier UID = new DerObjectIdentifier("0.9.2342.19200300.100.1.1");

		// Token: 0x04000CD1 RID: 3281
		private static readonly bool[] defaultReverse;

		// Token: 0x04000CD2 RID: 3282
		public static readonly Hashtable DefaultSymbols;

		// Token: 0x04000CD3 RID: 3283
		public static readonly Hashtable RFC2253Symbols;

		// Token: 0x04000CD4 RID: 3284
		public static readonly Hashtable RFC1779Symbols;

		// Token: 0x04000CD5 RID: 3285
		public static readonly Hashtable DefaultLookup;

		// Token: 0x04000CD6 RID: 3286
		private readonly IList ordering = Platform.CreateArrayList();

		// Token: 0x04000CD7 RID: 3287
		private readonly X509NameEntryConverter converter;

		// Token: 0x04000CD8 RID: 3288
		private IList values = Platform.CreateArrayList();

		// Token: 0x04000CD9 RID: 3289
		private IList added = Platform.CreateArrayList();

		// Token: 0x04000CDA RID: 3290
		private Asn1Sequence seq;
	}
}
