using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000473 RID: 1139
	public class SkeinParameters : ICipherParameters
	{
		// Token: 0x0600235A RID: 9050 RVA: 0x000C6AC4 File Offset: 0x000C6AC4
		public SkeinParameters() : this(Platform.CreateHashtable())
		{
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000C6AD4 File Offset: 0x000C6AD4
		private SkeinParameters(IDictionary parameters)
		{
			this.parameters = parameters;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000C6AE4 File Offset: 0x000C6AE4
		public IDictionary GetParameters()
		{
			return this.parameters;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000C6AEC File Offset: 0x000C6AEC
		public byte[] GetKey()
		{
			return (byte[])this.parameters[0];
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000C6B04 File Offset: 0x000C6B04
		public byte[] GetPersonalisation()
		{
			return (byte[])this.parameters[8];
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000C6B1C File Offset: 0x000C6B1C
		public byte[] GetPublicKey()
		{
			return (byte[])this.parameters[12];
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000C6B38 File Offset: 0x000C6B38
		public byte[] GetKeyIdentifier()
		{
			return (byte[])this.parameters[16];
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x000C6B54 File Offset: 0x000C6B54
		public byte[] GetNonce()
		{
			return (byte[])this.parameters[20];
		}

		// Token: 0x04001673 RID: 5747
		public const int PARAM_TYPE_KEY = 0;

		// Token: 0x04001674 RID: 5748
		public const int PARAM_TYPE_CONFIG = 4;

		// Token: 0x04001675 RID: 5749
		public const int PARAM_TYPE_PERSONALISATION = 8;

		// Token: 0x04001676 RID: 5750
		public const int PARAM_TYPE_PUBLIC_KEY = 12;

		// Token: 0x04001677 RID: 5751
		public const int PARAM_TYPE_KEY_IDENTIFIER = 16;

		// Token: 0x04001678 RID: 5752
		public const int PARAM_TYPE_NONCE = 20;

		// Token: 0x04001679 RID: 5753
		public const int PARAM_TYPE_MESSAGE = 48;

		// Token: 0x0400167A RID: 5754
		public const int PARAM_TYPE_OUTPUT = 63;

		// Token: 0x0400167B RID: 5755
		private IDictionary parameters;

		// Token: 0x02000E0E RID: 3598
		public class Builder
		{
			// Token: 0x06008C15 RID: 35861 RVA: 0x002A18E0 File Offset: 0x002A18E0
			public Builder()
			{
			}

			// Token: 0x06008C16 RID: 35862 RVA: 0x002A18F4 File Offset: 0x002A18F4
			public Builder(IDictionary paramsMap)
			{
				foreach (object obj in paramsMap.Keys)
				{
					int num = (int)obj;
					this.parameters.Add(num, paramsMap[num]);
				}
			}

			// Token: 0x06008C17 RID: 35863 RVA: 0x002A195C File Offset: 0x002A195C
			public Builder(SkeinParameters parameters)
			{
				foreach (object obj in parameters.parameters.Keys)
				{
					int num = (int)obj;
					this.parameters.Add(num, parameters.parameters[num]);
				}
			}

			// Token: 0x06008C18 RID: 35864 RVA: 0x002A19CC File Offset: 0x002A19CC
			public SkeinParameters.Builder Set(int type, byte[] value)
			{
				if (value == null)
				{
					throw new ArgumentException("Parameter value must not be null.");
				}
				if (type != 0 && (type <= 4 || type >= 63 || type == 48))
				{
					throw new ArgumentException("Parameter types must be in the range 0,5..47,49..62.");
				}
				if (type == 4)
				{
					throw new ArgumentException("Parameter type " + 4 + " is reserved for internal use.");
				}
				this.parameters.Add(type, value);
				return this;
			}

			// Token: 0x06008C19 RID: 35865 RVA: 0x002A1A4C File Offset: 0x002A1A4C
			public SkeinParameters.Builder SetKey(byte[] key)
			{
				return this.Set(0, key);
			}

			// Token: 0x06008C1A RID: 35866 RVA: 0x002A1A58 File Offset: 0x002A1A58
			public SkeinParameters.Builder SetPersonalisation(byte[] personalisation)
			{
				return this.Set(8, personalisation);
			}

			// Token: 0x06008C1B RID: 35867 RVA: 0x002A1A64 File Offset: 0x002A1A64
			public SkeinParameters.Builder SetPersonalisation(DateTime date, string emailAddress, string distinguisher)
			{
				SkeinParameters.Builder result;
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
					streamWriter.Write(date.ToString("YYYYMMDD", CultureInfo.InvariantCulture));
					streamWriter.Write(" ");
					streamWriter.Write(emailAddress);
					streamWriter.Write(" ");
					streamWriter.Write(distinguisher);
					Platform.Dispose(streamWriter);
					result = this.Set(8, memoryStream.ToArray());
				}
				catch (IOException innerException)
				{
					throw new InvalidOperationException("Byte I/O failed.", innerException);
				}
				return result;
			}

			// Token: 0x06008C1C RID: 35868 RVA: 0x002A1AF8 File Offset: 0x002A1AF8
			public SkeinParameters.Builder SetPublicKey(byte[] publicKey)
			{
				return this.Set(12, publicKey);
			}

			// Token: 0x06008C1D RID: 35869 RVA: 0x002A1B04 File Offset: 0x002A1B04
			public SkeinParameters.Builder SetKeyIdentifier(byte[] keyIdentifier)
			{
				return this.Set(16, keyIdentifier);
			}

			// Token: 0x06008C1E RID: 35870 RVA: 0x002A1B10 File Offset: 0x002A1B10
			public SkeinParameters.Builder SetNonce(byte[] nonce)
			{
				return this.Set(20, nonce);
			}

			// Token: 0x06008C1F RID: 35871 RVA: 0x002A1B1C File Offset: 0x002A1B1C
			public SkeinParameters Build()
			{
				return new SkeinParameters(this.parameters);
			}

			// Token: 0x04004145 RID: 16709
			private IDictionary parameters = Platform.CreateHashtable();
		}
	}
}
