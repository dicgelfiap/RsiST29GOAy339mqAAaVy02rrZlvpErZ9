using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Date;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509.Store
{
	// Token: 0x0200070A RID: 1802
	public class X509AttrCertStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06003EF2 RID: 16114 RVA: 0x0015A2BC File Offset: 0x0015A2BC
		public X509AttrCertStoreSelector()
		{
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0015A2DC File Offset: 0x0015A2DC
		private X509AttrCertStoreSelector(X509AttrCertStoreSelector o)
		{
			this.attributeCert = o.attributeCert;
			this.attributeCertificateValid = o.attributeCertificateValid;
			this.holder = o.holder;
			this.issuer = o.issuer;
			this.serialNumber = o.serialNumber;
			this.targetGroups = new HashSet(o.targetGroups);
			this.targetNames = new HashSet(o.targetNames);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0015A368 File Offset: 0x0015A368
		public bool Match(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			IX509AttributeCertificate ix509AttributeCertificate = obj as IX509AttributeCertificate;
			if (ix509AttributeCertificate == null)
			{
				return false;
			}
			if (this.attributeCert != null && !this.attributeCert.Equals(ix509AttributeCertificate))
			{
				return false;
			}
			if (this.serialNumber != null && !ix509AttributeCertificate.SerialNumber.Equals(this.serialNumber))
			{
				return false;
			}
			if (this.holder != null && !ix509AttributeCertificate.Holder.Equals(this.holder))
			{
				return false;
			}
			if (this.issuer != null && !ix509AttributeCertificate.Issuer.Equals(this.issuer))
			{
				return false;
			}
			if (this.attributeCertificateValid != null && !ix509AttributeCertificate.IsValid(this.attributeCertificateValid.Value))
			{
				return false;
			}
			if (this.targetNames.Count > 0 || this.targetGroups.Count > 0)
			{
				Asn1OctetString extensionValue = ix509AttributeCertificate.GetExtensionValue(X509Extensions.TargetInformation);
				if (extensionValue != null)
				{
					TargetInformation instance;
					try
					{
						instance = TargetInformation.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
					}
					catch (Exception)
					{
						return false;
					}
					Targets[] targetsObjects = instance.GetTargetsObjects();
					if (this.targetNames.Count > 0)
					{
						bool flag = false;
						int num = 0;
						while (num < targetsObjects.Length && !flag)
						{
							Target[] targets = targetsObjects[num].GetTargets();
							for (int i = 0; i < targets.Length; i++)
							{
								GeneralName targetName = targets[i].TargetName;
								if (targetName != null && this.targetNames.Contains(targetName))
								{
									flag = true;
									break;
								}
							}
							num++;
						}
						if (!flag)
						{
							return false;
						}
					}
					if (this.targetGroups.Count <= 0)
					{
						return true;
					}
					bool flag2 = false;
					int num2 = 0;
					while (num2 < targetsObjects.Length && !flag2)
					{
						Target[] targets2 = targetsObjects[num2].GetTargets();
						for (int j = 0; j < targets2.Length; j++)
						{
							GeneralName targetGroup = targets2[j].TargetGroup;
							if (targetGroup != null && this.targetGroups.Contains(targetGroup))
							{
								flag2 = true;
								break;
							}
						}
						num2++;
					}
					if (!flag2)
					{
						return false;
					}
					return true;
				}
			}
			return true;
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0015A5CC File Offset: 0x0015A5CC
		public object Clone()
		{
			return new X509AttrCertStoreSelector(this);
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x0015A5D4 File Offset: 0x0015A5D4
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x0015A5DC File Offset: 0x0015A5DC
		public IX509AttributeCertificate AttributeCert
		{
			get
			{
				return this.attributeCert;
			}
			set
			{
				this.attributeCert = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x0015A5E8 File Offset: 0x0015A5E8
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x0015A5F0 File Offset: 0x0015A5F0
		[Obsolete("Use AttributeCertificateValid instead")]
		public DateTimeObject AttribueCertificateValid
		{
			get
			{
				return this.attributeCertificateValid;
			}
			set
			{
				this.attributeCertificateValid = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x0015A5FC File Offset: 0x0015A5FC
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x0015A604 File Offset: 0x0015A604
		public DateTimeObject AttributeCertificateValid
		{
			get
			{
				return this.attributeCertificateValid;
			}
			set
			{
				this.attributeCertificateValid = value;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x0015A610 File Offset: 0x0015A610
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x0015A618 File Offset: 0x0015A618
		public AttributeCertificateHolder Holder
		{
			get
			{
				return this.holder;
			}
			set
			{
				this.holder = value;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x0015A624 File Offset: 0x0015A624
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x0015A62C File Offset: 0x0015A62C
		public AttributeCertificateIssuer Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x0015A638 File Offset: 0x0015A638
		// (set) Token: 0x06003F01 RID: 16129 RVA: 0x0015A640 File Offset: 0x0015A640
		public BigInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0015A64C File Offset: 0x0015A64C
		public void AddTargetName(GeneralName name)
		{
			this.targetNames.Add(name);
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0015A65C File Offset: 0x0015A65C
		public void AddTargetName(byte[] name)
		{
			this.AddTargetName(GeneralName.GetInstance(Asn1Object.FromByteArray(name)));
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0015A670 File Offset: 0x0015A670
		public void SetTargetNames(IEnumerable names)
		{
			this.targetNames = this.ExtractGeneralNames(names);
		}

		// Token: 0x06003F05 RID: 16133 RVA: 0x0015A680 File Offset: 0x0015A680
		public IEnumerable GetTargetNames()
		{
			return new EnumerableProxy(this.targetNames);
		}

		// Token: 0x06003F06 RID: 16134 RVA: 0x0015A690 File Offset: 0x0015A690
		public void AddTargetGroup(GeneralName group)
		{
			this.targetGroups.Add(group);
		}

		// Token: 0x06003F07 RID: 16135 RVA: 0x0015A6A0 File Offset: 0x0015A6A0
		public void AddTargetGroup(byte[] name)
		{
			this.AddTargetGroup(GeneralName.GetInstance(Asn1Object.FromByteArray(name)));
		}

		// Token: 0x06003F08 RID: 16136 RVA: 0x0015A6B4 File Offset: 0x0015A6B4
		public void SetTargetGroups(IEnumerable names)
		{
			this.targetGroups = this.ExtractGeneralNames(names);
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0015A6C4 File Offset: 0x0015A6C4
		public IEnumerable GetTargetGroups()
		{
			return new EnumerableProxy(this.targetGroups);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0015A6D4 File Offset: 0x0015A6D4
		private ISet ExtractGeneralNames(IEnumerable names)
		{
			ISet set = new HashSet();
			if (names != null)
			{
				foreach (object obj in names)
				{
					if (obj is GeneralName)
					{
						set.Add(obj);
					}
					else
					{
						set.Add(GeneralName.GetInstance(Asn1Object.FromByteArray((byte[])obj)));
					}
				}
			}
			return set;
		}

		// Token: 0x0400207E RID: 8318
		private IX509AttributeCertificate attributeCert;

		// Token: 0x0400207F RID: 8319
		private DateTimeObject attributeCertificateValid;

		// Token: 0x04002080 RID: 8320
		private AttributeCertificateHolder holder;

		// Token: 0x04002081 RID: 8321
		private AttributeCertificateIssuer issuer;

		// Token: 0x04002082 RID: 8322
		private BigInteger serialNumber;

		// Token: 0x04002083 RID: 8323
		private ISet targetNames = new HashSet();

		// Token: 0x04002084 RID: 8324
		private ISet targetGroups = new HashSet();
	}
}
