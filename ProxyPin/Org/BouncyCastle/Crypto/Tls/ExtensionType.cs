using System;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000502 RID: 1282
	public abstract class ExtensionType
	{
		// Token: 0x0400197E RID: 6526
		public const int server_name = 0;

		// Token: 0x0400197F RID: 6527
		public const int max_fragment_length = 1;

		// Token: 0x04001980 RID: 6528
		public const int client_certificate_url = 2;

		// Token: 0x04001981 RID: 6529
		public const int trusted_ca_keys = 3;

		// Token: 0x04001982 RID: 6530
		public const int truncated_hmac = 4;

		// Token: 0x04001983 RID: 6531
		public const int status_request = 5;

		// Token: 0x04001984 RID: 6532
		public const int user_mapping = 6;

		// Token: 0x04001985 RID: 6533
		public const int client_authz = 7;

		// Token: 0x04001986 RID: 6534
		public const int server_authz = 8;

		// Token: 0x04001987 RID: 6535
		public const int cert_type = 9;

		// Token: 0x04001988 RID: 6536
		public const int supported_groups = 10;

		// Token: 0x04001989 RID: 6537
		[Obsolete("Use 'supported_groups' instead")]
		public const int elliptic_curves = 10;

		// Token: 0x0400198A RID: 6538
		public const int ec_point_formats = 11;

		// Token: 0x0400198B RID: 6539
		public const int srp = 12;

		// Token: 0x0400198C RID: 6540
		public const int signature_algorithms = 13;

		// Token: 0x0400198D RID: 6541
		public const int use_srtp = 14;

		// Token: 0x0400198E RID: 6542
		public const int heartbeat = 15;

		// Token: 0x0400198F RID: 6543
		public const int application_layer_protocol_negotiation = 16;

		// Token: 0x04001990 RID: 6544
		public const int status_request_v2 = 17;

		// Token: 0x04001991 RID: 6545
		public const int signed_certificate_timestamp = 18;

		// Token: 0x04001992 RID: 6546
		public const int client_certificate_type = 19;

		// Token: 0x04001993 RID: 6547
		public const int server_certificate_type = 20;

		// Token: 0x04001994 RID: 6548
		public const int padding = 21;

		// Token: 0x04001995 RID: 6549
		public const int encrypt_then_mac = 22;

		// Token: 0x04001996 RID: 6550
		public const int extended_master_secret = 23;

		// Token: 0x04001997 RID: 6551
		public const int cached_info = 25;

		// Token: 0x04001998 RID: 6552
		public const int session_ticket = 35;

		// Token: 0x04001999 RID: 6553
		public const int renegotiation_info = 65281;

		// Token: 0x0400199A RID: 6554
		public static readonly int DRAFT_token_binding = 24;

		// Token: 0x0400199B RID: 6555
		public static readonly int negotiated_ff_dhe_groups = 101;
	}
}
