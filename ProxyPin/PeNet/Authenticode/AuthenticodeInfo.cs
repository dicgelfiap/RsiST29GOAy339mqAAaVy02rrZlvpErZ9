using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using PeNet.Asn1;
using PeNet.Structures;
using PeNet.Utilities;

namespace PeNet.Authenticode
{
	// Token: 0x02000C0A RID: 3082
	[ComVisible(true)]
	public class AuthenticodeInfo
	{
		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06007AD6 RID: 31446 RVA: 0x00244108 File Offset: 0x00244108
		public string SignerSerialNumber { get; }

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06007AD7 RID: 31447 RVA: 0x00244110 File Offset: 0x00244110
		public byte[] SignedHash { get; }

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06007AD8 RID: 31448 RVA: 0x00244118 File Offset: 0x00244118
		public bool IsAuthenticodeValid { get; }

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x06007AD9 RID: 31449 RVA: 0x00244120 File Offset: 0x00244120
		public X509Certificate2 SigningCertificate { get; }

		// Token: 0x06007ADA RID: 31450 RVA: 0x00244128 File Offset: 0x00244128
		public AuthenticodeInfo(PeFile peFile)
		{
			this._peFile = peFile;
			this._contentInfo = new ContentInfo(this._peFile.WinCertificate.bCertificate);
			this.SignerSerialNumber = this.GetSigningSerialNumber();
			this.SignedHash = this.GetSignedHash();
			this.IsAuthenticodeValid = (this.VerifyHash() && this.VerifySignature());
			this.SigningCertificate = this.GetSigningCertificate();
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x002441A4 File Offset: 0x002441A4
		private X509Certificate2 GetSigningCertificate()
		{
			WIN_CERTIFICATE winCertificate = this._peFile.WinCertificate;
			ushort? num = (winCertificate != null) ? new ushort?(winCertificate.wCertificateType) : null;
			int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
			int num3 = 2;
			if (!(num2.GetValueOrDefault() == num3 & num2 != null))
			{
				return null;
			}
			return new X509Certificate2(this._peFile.WinCertificate.bCertificate);
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x0024423C File Offset: 0x0024423C
		private X509Certificate2 GetSigningCertificateNonWindows(PeFile peFile)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			x509Certificate2Collection.Import(peFile.WinCertificate.bCertificate);
			return x509Certificate2Collection.Cast<X509Certificate2>().FirstOrDefault((X509Certificate2 cert) => string.Equals(cert.SerialNumber, this.SignerSerialNumber, StringComparison.CurrentCultureIgnoreCase));
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x0024427C File Offset: 0x0024427C
		private bool VerifySignature()
		{
			SignedCms signedCms = new SignedCms();
			signedCms.Decode(this._peFile.WinCertificate.bCertificate);
			try
			{
				signedCms.CheckSignature(true);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06007ADE RID: 31454 RVA: 0x002442CC File Offset: 0x002442CC
		private bool VerifyHash()
		{
			if (this.SignedHash == null)
			{
				return false;
			}
			int num = this.SignedHash.Length;
			HashAlgorithm hash;
			if (num <= 20)
			{
				if (num == 16)
				{
					hash = MD5.Create();
					goto IL_89;
				}
				if (num == 20)
				{
					hash = SHA1.Create();
					goto IL_89;
				}
			}
			else
			{
				if (num == 32)
				{
					hash = SHA256.Create();
					goto IL_89;
				}
				if (num == 48)
				{
					hash = SHA384.Create();
					goto IL_89;
				}
				if (num == 64)
				{
					hash = SHA512.Create();
					goto IL_89;
				}
			}
			return false;
			IL_89:
			IEnumerable<byte> second = this.ComputeAuthenticodeHashFromPeFile(hash);
			return this.SignedHash.SequenceEqual(second);
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x0024437C File Offset: 0x0024437C
		private byte[] GetSignedHash()
		{
			if (this._contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				return null;
			}
			SignedData signedData = new SignedData(this._contentInfo.Content);
			if (signedData.ContentInfo.ContentType != "1.3.6.1.4.1.311.2.1.4")
			{
				return null;
			}
			return ((Asn1OctetString)signedData.ContentInfo.Content.Nodes[0].Nodes[1].Nodes[1]).Data;
		}

		// Token: 0x06007AE0 RID: 31456 RVA: 0x0024440C File Offset: 0x0024440C
		private string GetSigningSerialNumber()
		{
			return ((Asn1Integer)this._contentInfo.Content.Nodes[0].Nodes[4].Nodes[0].Nodes[1].Nodes[1]).Value.ToHexString().Substring(2).ToUpper();
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x0024447C File Offset: 0x0024447C
		private IEnumerable<byte> ComputeAuthenticodeHashFromPeFile(HashAlgorithm hash)
		{
			int num = Convert.ToInt32(this._peFile.ImageNtHeaders.OptionalHeader.Offset) + 64;
			hash.TransformBlock(this._peFile.Buff, 0, num, new byte[num], 0);
			num += 4;
			IMAGE_DATA_DIRECTORY image_DATA_DIRECTORY = this._peFile.ImageNtHeaders.OptionalHeader.DataDirectory[4];
			int num2 = Convert.ToInt32(image_DATA_DIRECTORY.Offset) - num;
			hash.TransformBlock(this._peFile.Buff, num, num2, new byte[num2], 0);
			num += num2 + 8;
			num2 = Convert.ToInt32(this._peFile.ImageNtHeaders.OptionalHeader.SizeOfHeaders) - num;
			hash.TransformBlock(this._peFile.Buff, num, num2, new byte[num2], 0);
			int num3 = Convert.ToInt32(this._peFile.ImageNtHeaders.OptionalHeader.SizeOfHeaders);
			num2 = Convert.ToInt32(this._peFile.WinCertificate.Offset) - num3;
			hash.TransformBlock(this._peFile.Buff, num3, num2, new byte[num2], 0);
			int num4 = this._peFile.Buff.Length;
			num = Convert.ToInt32(image_DATA_DIRECTORY.Size) + Convert.ToInt32(this._peFile.WinCertificate.Offset);
			if (num4 > num)
			{
				num2 = num4 - num;
				if (num2 != 0)
				{
					hash.TransformBlock(this._peFile.Buff, num, num2, new byte[num2], 0);
				}
			}
			hash.TransformFinalBlock(this._peFile.Buff, 0, 0);
			return hash.Hash;
		}

		// Token: 0x04003B29 RID: 15145
		private readonly PeFile _peFile;

		// Token: 0x04003B2A RID: 15146
		private readonly ContentInfo _contentInfo;
	}
}
