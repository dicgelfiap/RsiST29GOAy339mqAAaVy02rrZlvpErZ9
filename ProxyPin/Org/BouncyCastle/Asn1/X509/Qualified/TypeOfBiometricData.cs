using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020001DE RID: 478
	public class TypeOfBiometricData : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06000F5C RID: 3932 RVA: 0x0005C1CC File Offset: 0x0005C1CC
		public static TypeOfBiometricData GetInstance(object obj)
		{
			if (obj == null || obj is TypeOfBiometricData)
			{
				return (TypeOfBiometricData)obj;
			}
			if (obj is DerInteger)
			{
				DerInteger instance = DerInteger.GetInstance(obj);
				int intValueExact = instance.IntValueExact;
				return new TypeOfBiometricData(intValueExact);
			}
			if (obj is DerObjectIdentifier)
			{
				DerObjectIdentifier instance2 = DerObjectIdentifier.GetInstance(obj);
				return new TypeOfBiometricData(instance2);
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0005C248 File Offset: 0x0005C248
		public TypeOfBiometricData(int predefinedBiometricType)
		{
			if (predefinedBiometricType == 0 || predefinedBiometricType == 1)
			{
				this.obj = new DerInteger(predefinedBiometricType);
				return;
			}
			throw new ArgumentException("unknow PredefinedBiometricType : " + predefinedBiometricType);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0005C280 File Offset: 0x0005C280
		public TypeOfBiometricData(DerObjectIdentifier biometricDataOid)
		{
			this.obj = biometricDataOid;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0005C290 File Offset: 0x0005C290
		public bool IsPredefined
		{
			get
			{
				return this.obj is DerInteger;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0005C2A0 File Offset: 0x0005C2A0
		public int PredefinedBiometricType
		{
			get
			{
				return ((DerInteger)this.obj).IntValueExact;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0005C2B4 File Offset: 0x0005C2B4
		public DerObjectIdentifier BiometricDataOid
		{
			get
			{
				return (DerObjectIdentifier)this.obj;
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0005C2C4 File Offset: 0x0005C2C4
		public override Asn1Object ToAsn1Object()
		{
			return this.obj.ToAsn1Object();
		}

		// Token: 0x04000B85 RID: 2949
		public const int Picture = 0;

		// Token: 0x04000B86 RID: 2950
		public const int HandwrittenSignature = 1;

		// Token: 0x04000B87 RID: 2951
		internal Asn1Encodable obj;
	}
}
