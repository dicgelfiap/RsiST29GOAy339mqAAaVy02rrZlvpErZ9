using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020004FB RID: 1275
	public class DtlsServerProtocol : DtlsProtocol
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x000D4880 File Offset: 0x000D4880
		public DtlsServerProtocol(SecureRandom secureRandom) : base(secureRandom)
		{
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000D4890 File Offset: 0x000D4890
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000D4898 File Offset: 0x000D4898
		public virtual bool VerifyRequests
		{
			get
			{
				return this.mVerifyRequests;
			}
			set
			{
				this.mVerifyRequests = value;
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000D48A4 File Offset: 0x000D48A4
		public virtual DtlsTransport Accept(TlsServer server, DatagramTransport transport)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			if (transport == null)
			{
				throw new ArgumentNullException("transport");
			}
			SecurityParameters securityParameters = new SecurityParameters();
			securityParameters.entity = 0;
			DtlsServerProtocol.ServerHandshakeState serverHandshakeState = new DtlsServerProtocol.ServerHandshakeState();
			serverHandshakeState.server = server;
			serverHandshakeState.serverContext = new TlsServerContextImpl(this.mSecureRandom, securityParameters);
			securityParameters.serverRandom = TlsProtocol.CreateRandomBlock(server.ShouldUseGmtUnixTime(), serverHandshakeState.serverContext.NonceRandomGenerator);
			server.Init(serverHandshakeState.serverContext);
			DtlsRecordLayer dtlsRecordLayer = new DtlsRecordLayer(transport, serverHandshakeState.serverContext, server, 22);
			server.NotifyCloseHandle(dtlsRecordLayer);
			DtlsTransport result;
			try
			{
				result = this.ServerHandshake(serverHandshakeState, dtlsRecordLayer);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.AbortServerHandshake(serverHandshakeState, dtlsRecordLayer, tlsFatalAlert.AlertDescription);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.AbortServerHandshake(serverHandshakeState, dtlsRecordLayer, 80);
				throw ex;
			}
			catch (Exception alertCause)
			{
				this.AbortServerHandshake(serverHandshakeState, dtlsRecordLayer, 80);
				throw new TlsFatalAlert(80, alertCause);
			}
			finally
			{
				securityParameters.Clear();
			}
			return result;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000D49C4 File Offset: 0x000D49C4
		internal virtual void AbortServerHandshake(DtlsServerProtocol.ServerHandshakeState state, DtlsRecordLayer recordLayer, byte alertDescription)
		{
			recordLayer.Fail(alertDescription);
			this.InvalidateSession(state);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000D49D4 File Offset: 0x000D49D4
		internal virtual DtlsTransport ServerHandshake(DtlsServerProtocol.ServerHandshakeState state, DtlsRecordLayer recordLayer)
		{
			SecurityParameters securityParameters = state.serverContext.SecurityParameters;
			DtlsReliableHandshake dtlsReliableHandshake = new DtlsReliableHandshake(state.serverContext, recordLayer);
			DtlsReliableHandshake.Message message = dtlsReliableHandshake.ReceiveMessage();
			if (message.Type != 1)
			{
				throw new TlsFatalAlert(10);
			}
			this.ProcessClientHello(state, message.Body);
			byte[] body = this.GenerateServerHello(state);
			DtlsProtocol.ApplyMaxFragmentLengthExtension(recordLayer, securityParameters.maxFragmentLength);
			ProtocolVersion serverVersion = state.serverContext.ServerVersion;
			recordLayer.ReadVersion = serverVersion;
			recordLayer.SetWriteVersion(serverVersion);
			dtlsReliableHandshake.SendMessage(2, body);
			dtlsReliableHandshake.NotifyHelloComplete();
			IList serverSupplementalData = state.server.GetServerSupplementalData();
			if (serverSupplementalData != null)
			{
				byte[] body2 = DtlsProtocol.GenerateSupplementalData(serverSupplementalData);
				dtlsReliableHandshake.SendMessage(23, body2);
			}
			state.keyExchange = state.server.GetKeyExchange();
			state.keyExchange.Init(state.serverContext);
			state.serverCredentials = state.server.GetCredentials();
			Certificate certificate = null;
			if (state.serverCredentials == null)
			{
				state.keyExchange.SkipServerCredentials();
			}
			else
			{
				state.keyExchange.ProcessServerCredentials(state.serverCredentials);
				certificate = state.serverCredentials.Certificate;
				byte[] body3 = DtlsProtocol.GenerateCertificate(certificate);
				dtlsReliableHandshake.SendMessage(11, body3);
			}
			if (certificate == null || certificate.IsEmpty)
			{
				state.allowCertificateStatus = false;
			}
			if (state.allowCertificateStatus)
			{
				CertificateStatus certificateStatus = state.server.GetCertificateStatus();
				if (certificateStatus != null)
				{
					byte[] body4 = this.GenerateCertificateStatus(state, certificateStatus);
					dtlsReliableHandshake.SendMessage(22, body4);
				}
			}
			byte[] array = state.keyExchange.GenerateServerKeyExchange();
			if (array != null)
			{
				dtlsReliableHandshake.SendMessage(12, array);
			}
			if (state.serverCredentials != null)
			{
				state.certificateRequest = state.server.GetCertificateRequest();
				if (state.certificateRequest != null)
				{
					if (TlsUtilities.IsTlsV12(state.serverContext) != (state.certificateRequest.SupportedSignatureAlgorithms != null))
					{
						throw new TlsFatalAlert(80);
					}
					state.keyExchange.ValidateCertificateRequest(state.certificateRequest);
					byte[] body5 = this.GenerateCertificateRequest(state, state.certificateRequest);
					dtlsReliableHandshake.SendMessage(13, body5);
					TlsUtilities.TrackHashAlgorithms(dtlsReliableHandshake.HandshakeHash, state.certificateRequest.SupportedSignatureAlgorithms);
				}
			}
			dtlsReliableHandshake.SendMessage(14, TlsUtilities.EmptyBytes);
			dtlsReliableHandshake.HandshakeHash.SealHashAlgorithms();
			message = dtlsReliableHandshake.ReceiveMessage();
			if (message.Type == 23)
			{
				this.ProcessClientSupplementalData(state, message.Body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			else
			{
				state.server.ProcessClientSupplementalData(null);
			}
			if (state.certificateRequest == null)
			{
				state.keyExchange.SkipClientCredentials();
			}
			else if (message.Type == 11)
			{
				this.ProcessClientCertificate(state, message.Body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			else
			{
				if (TlsUtilities.IsTlsV12(state.serverContext))
				{
					throw new TlsFatalAlert(10);
				}
				this.NotifyClientCertificate(state, Certificate.EmptyChain);
			}
			if (message.Type == 16)
			{
				this.ProcessClientKeyExchange(state, message.Body);
				TlsHandshakeHash tlsHandshakeHash = dtlsReliableHandshake.PrepareToFinish();
				securityParameters.sessionHash = TlsProtocol.GetCurrentPrfHash(state.serverContext, tlsHandshakeHash, null);
				TlsProtocol.EstablishMasterSecret(state.serverContext, state.keyExchange);
				recordLayer.InitPendingEpoch(state.server.GetCipher());
				if (this.ExpectCertificateVerifyMessage(state))
				{
					byte[] body6 = dtlsReliableHandshake.ReceiveMessageBody(15);
					this.ProcessCertificateVerify(state, body6, tlsHandshakeHash);
				}
				byte[] expected_verify_data = TlsUtilities.CalculateVerifyData(state.serverContext, "client finished", TlsProtocol.GetCurrentPrfHash(state.serverContext, dtlsReliableHandshake.HandshakeHash, null));
				this.ProcessFinished(dtlsReliableHandshake.ReceiveMessageBody(20), expected_verify_data);
				if (state.expectSessionTicket)
				{
					NewSessionTicket newSessionTicket = state.server.GetNewSessionTicket();
					byte[] body7 = this.GenerateNewSessionTicket(state, newSessionTicket);
					dtlsReliableHandshake.SendMessage(4, body7);
				}
				byte[] body8 = TlsUtilities.CalculateVerifyData(state.serverContext, "server finished", TlsProtocol.GetCurrentPrfHash(state.serverContext, dtlsReliableHandshake.HandshakeHash, null));
				dtlsReliableHandshake.SendMessage(20, body8);
				dtlsReliableHandshake.Finish();
				state.server.NotifyHandshakeComplete();
				return new DtlsTransport(recordLayer);
			}
			throw new TlsFatalAlert(10);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000D4DF0 File Offset: 0x000D4DF0
		protected virtual void InvalidateSession(DtlsServerProtocol.ServerHandshakeState state)
		{
			if (state.sessionParameters != null)
			{
				state.sessionParameters.Clear();
				state.sessionParameters = null;
			}
			if (state.tlsSession != null)
			{
				state.tlsSession.Invalidate();
				state.tlsSession = null;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000D4E2C File Offset: 0x000D4E2C
		protected virtual byte[] GenerateCertificateRequest(DtlsServerProtocol.ServerHandshakeState state, CertificateRequest certificateRequest)
		{
			MemoryStream memoryStream = new MemoryStream();
			certificateRequest.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000D4E50 File Offset: 0x000D4E50
		protected virtual byte[] GenerateCertificateStatus(DtlsServerProtocol.ServerHandshakeState state, CertificateStatus certificateStatus)
		{
			MemoryStream memoryStream = new MemoryStream();
			certificateStatus.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000D4E74 File Offset: 0x000D4E74
		protected virtual byte[] GenerateNewSessionTicket(DtlsServerProtocol.ServerHandshakeState state, NewSessionTicket newSessionTicket)
		{
			MemoryStream memoryStream = new MemoryStream();
			newSessionTicket.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000D4E98 File Offset: 0x000D4E98
		protected virtual byte[] GenerateServerHello(DtlsServerProtocol.ServerHandshakeState state)
		{
			SecurityParameters securityParameters = state.serverContext.SecurityParameters;
			MemoryStream memoryStream = new MemoryStream();
			ProtocolVersion serverVersion = state.server.GetServerVersion();
			if (!serverVersion.IsEqualOrEarlierVersionOf(state.serverContext.ClientVersion))
			{
				throw new TlsFatalAlert(80);
			}
			state.serverContext.SetServerVersion(serverVersion);
			TlsUtilities.WriteVersion(state.serverContext.ServerVersion, memoryStream);
			memoryStream.Write(securityParameters.ServerRandom, 0, securityParameters.ServerRandom.Length);
			TlsUtilities.WriteOpaque8(TlsUtilities.EmptyBytes, memoryStream);
			int selectedCipherSuite = state.server.GetSelectedCipherSuite();
			if (!Arrays.Contains(state.offeredCipherSuites, selectedCipherSuite) || selectedCipherSuite == 0 || CipherSuite.IsScsv(selectedCipherSuite) || !TlsUtilities.IsValidCipherSuiteForVersion(selectedCipherSuite, state.serverContext.ServerVersion))
			{
				throw new TlsFatalAlert(80);
			}
			DtlsProtocol.ValidateSelectedCipherSuite(selectedCipherSuite, 80);
			securityParameters.cipherSuite = selectedCipherSuite;
			byte selectedCompressionMethod = state.server.GetSelectedCompressionMethod();
			if (!Arrays.Contains(state.offeredCompressionMethods, selectedCompressionMethod))
			{
				throw new TlsFatalAlert(80);
			}
			securityParameters.compressionAlgorithm = selectedCompressionMethod;
			TlsUtilities.WriteUint16(selectedCipherSuite, memoryStream);
			TlsUtilities.WriteUint8(selectedCompressionMethod, memoryStream);
			state.serverExtensions = TlsExtensionsUtilities.EnsureExtensionsInitialised(state.server.GetServerExtensions());
			if (state.secure_renegotiation)
			{
				byte[] extensionData = TlsUtilities.GetExtensionData(state.serverExtensions, 65281);
				bool flag = null == extensionData;
				if (flag)
				{
					state.serverExtensions[65281] = TlsProtocol.CreateRenegotiationInfo(TlsUtilities.EmptyBytes);
				}
			}
			if (securityParameters.IsExtendedMasterSecret)
			{
				TlsExtensionsUtilities.AddExtendedMasterSecretExtension(state.serverExtensions);
			}
			if (state.serverExtensions.Count > 0)
			{
				securityParameters.encryptThenMac = TlsExtensionsUtilities.HasEncryptThenMacExtension(state.serverExtensions);
				securityParameters.maxFragmentLength = DtlsProtocol.EvaluateMaxFragmentLengthExtension(state.resumedSession, state.clientExtensions, state.serverExtensions, 80);
				securityParameters.truncatedHMac = TlsExtensionsUtilities.HasTruncatedHMacExtension(state.serverExtensions);
				state.allowCertificateStatus = (!state.resumedSession && TlsUtilities.HasExpectedEmptyExtensionData(state.serverExtensions, 5, 80));
				state.expectSessionTicket = (!state.resumedSession && TlsUtilities.HasExpectedEmptyExtensionData(state.serverExtensions, 35, 80));
				TlsProtocol.WriteExtensions(memoryStream, state.serverExtensions);
			}
			securityParameters.prfAlgorithm = TlsProtocol.GetPrfAlgorithm(state.serverContext, securityParameters.CipherSuite);
			securityParameters.verifyDataLength = 12;
			return memoryStream.ToArray();
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000D5100 File Offset: 0x000D5100
		protected virtual void NotifyClientCertificate(DtlsServerProtocol.ServerHandshakeState state, Certificate clientCertificate)
		{
			if (state.certificateRequest == null)
			{
				throw new InvalidOperationException();
			}
			if (state.clientCertificate != null)
			{
				throw new TlsFatalAlert(10);
			}
			state.clientCertificate = clientCertificate;
			if (clientCertificate.IsEmpty)
			{
				state.keyExchange.SkipClientCredentials();
			}
			else
			{
				state.clientCertificateType = TlsUtilities.GetClientCertificateType(clientCertificate, state.serverCredentials.Certificate);
				state.keyExchange.ProcessClientCertificate(clientCertificate);
			}
			state.server.NotifyClientCertificate(clientCertificate);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000D5188 File Offset: 0x000D5188
		protected virtual void ProcessClientCertificate(DtlsServerProtocol.ServerHandshakeState state, byte[] body)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			Certificate clientCertificate = Certificate.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			this.NotifyClientCertificate(state, clientCertificate);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000D51B8 File Offset: 0x000D51B8
		protected virtual void ProcessCertificateVerify(DtlsServerProtocol.ServerHandshakeState state, byte[] body, TlsHandshakeHash prepareFinishHash)
		{
			if (state.certificateRequest == null)
			{
				throw new InvalidOperationException();
			}
			MemoryStream memoryStream = new MemoryStream(body, false);
			TlsServerContextImpl serverContext = state.serverContext;
			DigitallySigned digitallySigned = DigitallySigned.Parse(serverContext, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			try
			{
				SignatureAndHashAlgorithm algorithm = digitallySigned.Algorithm;
				byte[] hash;
				if (TlsUtilities.IsTlsV12(serverContext))
				{
					TlsUtilities.VerifySupportedSignatureAlgorithm(state.certificateRequest.SupportedSignatureAlgorithms, algorithm);
					hash = prepareFinishHash.GetFinalHash(algorithm.Hash);
				}
				else
				{
					hash = serverContext.SecurityParameters.SessionHash;
				}
				X509CertificateStructure certificateAt = state.clientCertificate.GetCertificateAt(0);
				SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
				AsymmetricKeyParameter publicKey = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
				TlsSigner tlsSigner = TlsUtilities.CreateTlsSigner((byte)state.clientCertificateType);
				tlsSigner.Init(serverContext);
				if (!tlsSigner.VerifyRawSignature(algorithm, digitallySigned.Signature, publicKey, hash))
				{
					throw new TlsFatalAlert(51);
				}
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				throw tlsFatalAlert;
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(51, alertCause);
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000D52C0 File Offset: 0x000D52C0
		protected virtual void ProcessClientHello(DtlsServerProtocol.ServerHandshakeState state, byte[] body)
		{
			MemoryStream input = new MemoryStream(body, false);
			ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(input);
			if (!protocolVersion.IsDtls)
			{
				throw new TlsFatalAlert(47);
			}
			byte[] clientRandom = TlsUtilities.ReadFully(32, input);
			byte[] array = TlsUtilities.ReadOpaque8(input);
			if (array.Length > 32)
			{
				throw new TlsFatalAlert(47);
			}
			TlsUtilities.ReadOpaque8(input);
			int num = TlsUtilities.ReadUint16(input);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			state.offeredCipherSuites = TlsUtilities.ReadUint16Array(num / 2, input);
			int num2 = (int)TlsUtilities.ReadUint8(input);
			if (num2 < 1)
			{
				throw new TlsFatalAlert(47);
			}
			state.offeredCompressionMethods = TlsUtilities.ReadUint8Array(num2, input);
			state.clientExtensions = TlsProtocol.ReadExtensions(input);
			TlsServerContextImpl serverContext = state.serverContext;
			SecurityParameters securityParameters = serverContext.SecurityParameters;
			securityParameters.extendedMasterSecret = TlsExtensionsUtilities.HasExtendedMasterSecretExtension(state.clientExtensions);
			if (!securityParameters.IsExtendedMasterSecret && state.server.RequiresExtendedMasterSecret())
			{
				throw new TlsFatalAlert(40);
			}
			serverContext.SetClientVersion(protocolVersion);
			state.server.NotifyClientVersion(protocolVersion);
			state.server.NotifyFallback(Arrays.Contains(state.offeredCipherSuites, 22016));
			securityParameters.clientRandom = clientRandom;
			state.server.NotifyOfferedCipherSuites(state.offeredCipherSuites);
			state.server.NotifyOfferedCompressionMethods(state.offeredCompressionMethods);
			if (Arrays.Contains(state.offeredCipherSuites, 255))
			{
				state.secure_renegotiation = true;
			}
			byte[] extensionData = TlsUtilities.GetExtensionData(state.clientExtensions, 65281);
			if (extensionData != null)
			{
				state.secure_renegotiation = true;
				if (!Arrays.ConstantTimeAreEqual(extensionData, TlsProtocol.CreateRenegotiationInfo(TlsUtilities.EmptyBytes)))
				{
					throw new TlsFatalAlert(40);
				}
			}
			state.server.NotifySecureRenegotiation(state.secure_renegotiation);
			if (state.clientExtensions != null)
			{
				TlsExtensionsUtilities.GetPaddingExtension(state.clientExtensions);
				state.server.ProcessClientExtensions(state.clientExtensions);
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000D54B4 File Offset: 0x000D54B4
		protected virtual void ProcessClientKeyExchange(DtlsServerProtocol.ServerHandshakeState state, byte[] body)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			state.keyExchange.ProcessClientKeyExchange(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000D54E0 File Offset: 0x000D54E0
		protected virtual void ProcessClientSupplementalData(DtlsServerProtocol.ServerHandshakeState state, byte[] body)
		{
			MemoryStream input = new MemoryStream(body, false);
			IList clientSupplementalData = TlsProtocol.ReadSupplementalDataMessage(input);
			state.server.ProcessClientSupplementalData(clientSupplementalData);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000D550C File Offset: 0x000D550C
		protected virtual bool ExpectCertificateVerifyMessage(DtlsServerProtocol.ServerHandshakeState state)
		{
			return state.clientCertificateType >= 0 && TlsUtilities.HasSigningCapability((byte)state.clientCertificateType);
		}

		// Token: 0x04001953 RID: 6483
		protected bool mVerifyRequests = true;

		// Token: 0x02000E1F RID: 3615
		protected internal class ServerHandshakeState
		{
			// Token: 0x04004174 RID: 16756
			internal TlsServer server = null;

			// Token: 0x04004175 RID: 16757
			internal TlsServerContextImpl serverContext = null;

			// Token: 0x04004176 RID: 16758
			internal TlsSession tlsSession = null;

			// Token: 0x04004177 RID: 16759
			internal SessionParameters sessionParameters = null;

			// Token: 0x04004178 RID: 16760
			internal SessionParameters.Builder sessionParametersBuilder = null;

			// Token: 0x04004179 RID: 16761
			internal int[] offeredCipherSuites = null;

			// Token: 0x0400417A RID: 16762
			internal byte[] offeredCompressionMethods = null;

			// Token: 0x0400417B RID: 16763
			internal IDictionary clientExtensions = null;

			// Token: 0x0400417C RID: 16764
			internal IDictionary serverExtensions = null;

			// Token: 0x0400417D RID: 16765
			internal bool resumedSession = false;

			// Token: 0x0400417E RID: 16766
			internal bool secure_renegotiation = false;

			// Token: 0x0400417F RID: 16767
			internal bool allowCertificateStatus = false;

			// Token: 0x04004180 RID: 16768
			internal bool expectSessionTicket = false;

			// Token: 0x04004181 RID: 16769
			internal TlsKeyExchange keyExchange = null;

			// Token: 0x04004182 RID: 16770
			internal TlsCredentials serverCredentials = null;

			// Token: 0x04004183 RID: 16771
			internal CertificateRequest certificateRequest = null;

			// Token: 0x04004184 RID: 16772
			internal short clientCertificateType = -1;

			// Token: 0x04004185 RID: 16773
			internal Certificate clientCertificate = null;
		}
	}
}
