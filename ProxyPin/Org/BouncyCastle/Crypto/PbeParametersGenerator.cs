using System;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020003CF RID: 975
	public abstract class PbeParametersGenerator
	{
		// Token: 0x06001EC8 RID: 7880 RVA: 0x000B5144 File Offset: 0x000B5144
		public virtual void Init(byte[] password, byte[] salt, int iterationCount)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			this.mPassword = Arrays.Clone(password);
			this.mSalt = Arrays.Clone(salt);
			this.mIterationCount = iterationCount;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000B5198 File Offset: 0x000B5198
		public virtual byte[] Password
		{
			get
			{
				return Arrays.Clone(this.mPassword);
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000B51A8 File Offset: 0x000B51A8
		[Obsolete("Use 'Password' property")]
		public byte[] GetPassword()
		{
			return this.Password;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000B51B0 File Offset: 0x000B51B0
		public virtual byte[] Salt
		{
			get
			{
				return Arrays.Clone(this.mSalt);
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000B51C0 File Offset: 0x000B51C0
		[Obsolete("Use 'Salt' property")]
		public byte[] GetSalt()
		{
			return this.Salt;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000B51C8 File Offset: 0x000B51C8
		public virtual int IterationCount
		{
			get
			{
				return this.mIterationCount;
			}
		}

		// Token: 0x06001ECE RID: 7886
		[Obsolete("Use version with 'algorithm' parameter")]
		public abstract ICipherParameters GenerateDerivedParameters(int keySize);

		// Token: 0x06001ECF RID: 7887
		public abstract ICipherParameters GenerateDerivedParameters(string algorithm, int keySize);

		// Token: 0x06001ED0 RID: 7888
		[Obsolete("Use version with 'algorithm' parameter")]
		public abstract ICipherParameters GenerateDerivedParameters(int keySize, int ivSize);

		// Token: 0x06001ED1 RID: 7889
		public abstract ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize);

		// Token: 0x06001ED2 RID: 7890
		public abstract ICipherParameters GenerateDerivedMacParameters(int keySize);

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000B51D0 File Offset: 0x000B51D0
		public static byte[] Pkcs5PasswordToBytes(char[] password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Strings.ToByteArray(password);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000B51E8 File Offset: 0x000B51E8
		[Obsolete("Use version taking 'char[]' instead")]
		public static byte[] Pkcs5PasswordToBytes(string password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Strings.ToByteArray(password);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000B5200 File Offset: 0x000B5200
		public static byte[] Pkcs5PasswordToUtf8Bytes(char[] password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Encoding.UTF8.GetBytes(password);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000B521C File Offset: 0x000B521C
		[Obsolete("Use version taking 'char[]' instead")]
		public static byte[] Pkcs5PasswordToUtf8Bytes(string password)
		{
			if (password == null)
			{
				return new byte[0];
			}
			return Encoding.UTF8.GetBytes(password);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x000B5238 File Offset: 0x000B5238
		public static byte[] Pkcs12PasswordToBytes(char[] password)
		{
			return PbeParametersGenerator.Pkcs12PasswordToBytes(password, false);
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000B5244 File Offset: 0x000B5244
		public static byte[] Pkcs12PasswordToBytes(char[] password, bool wrongPkcs12Zero)
		{
			if (password == null || password.Length < 1)
			{
				return new byte[wrongPkcs12Zero ? 2 : 0];
			}
			byte[] array = new byte[(password.Length + 1) * 2];
			Encoding.BigEndianUnicode.GetBytes(password, 0, password.Length, array, 0);
			return array;
		}

		// Token: 0x04001467 RID: 5223
		protected byte[] mPassword;

		// Token: 0x04001468 RID: 5224
		protected byte[] mSalt;

		// Token: 0x04001469 RID: 5225
		protected int mIterationCount;
	}
}
