using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004C9 RID: 1225
	public abstract class AlertDescription
	{
		// Token: 0x060025EB RID: 9707 RVA: 0x000CF5C0 File Offset: 0x000CF5C0
		public static string GetName(byte alertDescription)
		{
			if (alertDescription <= 60)
			{
				if (alertDescription <= 10)
				{
					if (alertDescription == 0)
					{
						return "close_notify";
					}
					if (alertDescription == 10)
					{
						return "unexpected_message";
					}
				}
				else
				{
					switch (alertDescription)
					{
					case 20:
						return "bad_record_mac";
					case 21:
						return "decryption_failed";
					case 22:
						return "record_overflow";
					default:
						switch (alertDescription)
						{
						case 30:
							return "decompression_failure";
						case 31:
						case 32:
						case 33:
						case 34:
						case 35:
						case 36:
						case 37:
						case 38:
						case 39:
							break;
						case 40:
							return "handshake_failure";
						case 41:
							return "no_certificate";
						case 42:
							return "bad_certificate";
						case 43:
							return "unsupported_certificate";
						case 44:
							return "certificate_revoked";
						case 45:
							return "certificate_expired";
						case 46:
							return "certificate_unknown";
						case 47:
							return "illegal_parameter";
						case 48:
							return "unknown_ca";
						case 49:
							return "access_denied";
						case 50:
							return "decode_error";
						case 51:
							return "decrypt_error";
						default:
							if (alertDescription == 60)
							{
								return "export_restriction";
							}
							break;
						}
						break;
					}
				}
			}
			else if (alertDescription <= 86)
			{
				switch (alertDescription)
				{
				case 70:
					return "protocol_version";
				case 71:
					return "insufficient_security";
				default:
					if (alertDescription == 80)
					{
						return "internal_error";
					}
					if (alertDescription == 86)
					{
						return "inappropriate_fallback";
					}
					break;
				}
			}
			else
			{
				if (alertDescription == 90)
				{
					return "user_canceled";
				}
				if (alertDescription == 100)
				{
					return "no_renegotiation";
				}
				switch (alertDescription)
				{
				case 110:
					return "unsupported_extension";
				case 111:
					return "certificate_unobtainable";
				case 112:
					return "unrecognized_name";
				case 113:
					return "bad_certificate_status_response";
				case 114:
					return "bad_certificate_hash_value";
				case 115:
					return "unknown_psk_identity";
				}
			}
			return "UNKNOWN";
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000CF7A0 File Offset: 0x000CF7A0
		public static string GetText(byte alertDescription)
		{
			return string.Concat(new object[]
			{
				AlertDescription.GetName(alertDescription),
				"(",
				alertDescription,
				")"
			});
		}

		// Token: 0x040017A7 RID: 6055
		public const byte close_notify = 0;

		// Token: 0x040017A8 RID: 6056
		public const byte unexpected_message = 10;

		// Token: 0x040017A9 RID: 6057
		public const byte bad_record_mac = 20;

		// Token: 0x040017AA RID: 6058
		public const byte decryption_failed = 21;

		// Token: 0x040017AB RID: 6059
		public const byte record_overflow = 22;

		// Token: 0x040017AC RID: 6060
		public const byte decompression_failure = 30;

		// Token: 0x040017AD RID: 6061
		public const byte handshake_failure = 40;

		// Token: 0x040017AE RID: 6062
		public const byte no_certificate = 41;

		// Token: 0x040017AF RID: 6063
		public const byte bad_certificate = 42;

		// Token: 0x040017B0 RID: 6064
		public const byte unsupported_certificate = 43;

		// Token: 0x040017B1 RID: 6065
		public const byte certificate_revoked = 44;

		// Token: 0x040017B2 RID: 6066
		public const byte certificate_expired = 45;

		// Token: 0x040017B3 RID: 6067
		public const byte certificate_unknown = 46;

		// Token: 0x040017B4 RID: 6068
		public const byte illegal_parameter = 47;

		// Token: 0x040017B5 RID: 6069
		public const byte unknown_ca = 48;

		// Token: 0x040017B6 RID: 6070
		public const byte access_denied = 49;

		// Token: 0x040017B7 RID: 6071
		public const byte decode_error = 50;

		// Token: 0x040017B8 RID: 6072
		public const byte decrypt_error = 51;

		// Token: 0x040017B9 RID: 6073
		public const byte export_restriction = 60;

		// Token: 0x040017BA RID: 6074
		public const byte protocol_version = 70;

		// Token: 0x040017BB RID: 6075
		public const byte insufficient_security = 71;

		// Token: 0x040017BC RID: 6076
		public const byte internal_error = 80;

		// Token: 0x040017BD RID: 6077
		public const byte user_canceled = 90;

		// Token: 0x040017BE RID: 6078
		public const byte no_renegotiation = 100;

		// Token: 0x040017BF RID: 6079
		public const byte unsupported_extension = 110;

		// Token: 0x040017C0 RID: 6080
		public const byte certificate_unobtainable = 111;

		// Token: 0x040017C1 RID: 6081
		public const byte unrecognized_name = 112;

		// Token: 0x040017C2 RID: 6082
		public const byte bad_certificate_status_response = 113;

		// Token: 0x040017C3 RID: 6083
		public const byte bad_certificate_hash_value = 114;

		// Token: 0x040017C4 RID: 6084
		public const byte unknown_psk_identity = 115;

		// Token: 0x040017C5 RID: 6085
		public const byte inappropriate_fallback = 86;
	}
}
