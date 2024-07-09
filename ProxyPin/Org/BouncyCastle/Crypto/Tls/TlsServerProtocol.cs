using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200054A RID: 1354
	public class TlsServerProtocol : TlsProtocol
	{
		// Token: 0x06002984 RID: 10628 RVA: 0x000DE8E8 File Offset: 0x000DE8E8
		public TlsServerProtocol(Stream stream, SecureRandom secureRandom) : base(stream, secureRandom)
		{
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000DE924 File Offset: 0x000DE924
		public TlsServerProtocol(Stream input, Stream output, SecureRandom secureRandom) : base(input, output, secureRandom)
		{
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000DE960 File Offset: 0x000DE960
		public TlsServerProtocol(SecureRandom secureRandom) : base(secureRandom)
		{
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000DE99C File Offset: 0x000DE99C
		public virtual void Accept(TlsServer tlsServer)
		{
			if (tlsServer == null)
			{
				throw new ArgumentNullException("tlsServer");
			}
			if (this.mTlsServer != null)
			{
				throw new InvalidOperationException("'Accept' can only be called once");
			}
			this.mTlsServer = tlsServer;
			this.mSecurityParameters = new SecurityParameters();
			this.mSecurityParameters.entity = 0;
			this.mTlsServerContext = new TlsServerContextImpl(this.mSecureRandom, this.mSecurityParameters);
			this.mSecurityParameters.serverRandom = TlsProtocol.CreateRandomBlock(tlsServer.ShouldUseGmtUnixTime(), this.mTlsServerContext.NonceRandomGenerator);
			this.mTlsServer.Init(this.mTlsServerContext);
			this.mRecordStream.Init(this.mTlsServerContext);
			tlsServer.NotifyCloseHandle(this);
			this.mRecordStream.SetRestrictReadVersion(false);
			this.BlockForHandshake();
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000DEA68 File Offset: 0x000DEA68
		protected override void CleanupHandshake()
		{
			base.CleanupHandshake();
			this.mKeyExchange = null;
			this.mServerCredentials = null;
			this.mCertificateRequest = null;
			this.mPrepareFinishHash = null;
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000DEA8C File Offset: 0x000DEA8C
		protected override TlsContext Context
		{
			get
			{
				return this.mTlsServerContext;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000DEA94 File Offset: 0x000DEA94
		internal override AbstractTlsContext ContextAdmin
		{
			get
			{
				return this.mTlsServerContext;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x000DEA9C File Offset: 0x000DEA9C
		protected override TlsPeer Peer
		{
			get
			{
				return this.mTlsServer;
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000DEAA4 File Offset: 0x000DEAA4
		protected override void HandleHandshakeMessage(byte type, MemoryStream buf)
		{
			switch (type)
			{
			case 1:
			{
				short mConnectionState = this.mConnectionState;
				if (mConnectionState == 0)
				{
					this.ReceiveClientHelloMessage(buf);
					this.mConnectionState = 1;
					this.SendServerHelloMessage();
					this.mConnectionState = 2;
					this.mRecordStream.NotifyHelloComplete();
					IList serverSupplementalData = this.mTlsServer.GetServerSupplementalData();
					if (serverSupplementalData != null)
					{
						this.SendSupplementalDataMessage(serverSupplementalData);
					}
					this.mConnectionState = 3;
					this.mKeyExchange = this.mTlsServer.GetKeyExchange();
					this.mKeyExchange.Init(this.Context);
					this.mServerCredentials = this.mTlsServer.GetCredentials();
					Certificate certificate = null;
					if (this.mServerCredentials == null)
					{
						this.mKeyExchange.SkipServerCredentials();
					}
					else
					{
						this.mKeyExchange.ProcessServerCredentials(this.mServerCredentials);
						certificate = this.mServerCredentials.Certificate;
						this.SendCertificateMessage(certificate);
					}
					this.mConnectionState = 4;
					if (certificate == null || certificate.IsEmpty)
					{
						this.mAllowCertificateStatus = false;
					}
					if (this.mAllowCertificateStatus)
					{
						CertificateStatus certificateStatus = this.mTlsServer.GetCertificateStatus();
						if (certificateStatus != null)
						{
							this.SendCertificateStatusMessage(certificateStatus);
						}
					}
					this.mConnectionState = 5;
					byte[] array = this.mKeyExchange.GenerateServerKeyExchange();
					if (array != null)
					{
						this.SendServerKeyExchangeMessage(array);
					}
					this.mConnectionState = 6;
					if (this.mServerCredentials != null)
					{
						this.mCertificateRequest = this.mTlsServer.GetCertificateRequest();
						if (this.mCertificateRequest != null)
						{
							if (TlsUtilities.IsTlsV12(this.Context) != (this.mCertificateRequest.SupportedSignatureAlgorithms != null))
							{
								throw new TlsFatalAlert(80);
							}
							this.mKeyExchange.ValidateCertificateRequest(this.mCertificateRequest);
							this.SendCertificateRequestMessage(this.mCertificateRequest);
							TlsUtilities.TrackHashAlgorithms(this.mRecordStream.HandshakeHash, this.mCertificateRequest.SupportedSignatureAlgorithms);
						}
					}
					this.mConnectionState = 7;
					this.SendServerHelloDoneMessage();
					this.mConnectionState = 8;
					this.mRecordStream.HandshakeHash.SealHashAlgorithms();
					return;
				}
				if (mConnectionState != 16)
				{
					throw new TlsFatalAlert(10);
				}
				this.RefuseRenegotiation();
				return;
			}
			case 11:
				switch (this.mConnectionState)
				{
				case 8:
				case 9:
					if (this.mConnectionState < 9)
					{
						this.mTlsServer.ProcessClientSupplementalData(null);
					}
					if (this.mCertificateRequest == null)
					{
						throw new TlsFatalAlert(10);
					}
					this.ReceiveCertificateMessage(buf);
					this.mConnectionState = 10;
					return;
				default:
					throw new TlsFatalAlert(10);
				}
				break;
			case 15:
			{
				short mConnectionState = this.mConnectionState;
				if (mConnectionState != 11)
				{
					throw new TlsFatalAlert(10);
				}
				if (!this.ExpectCertificateVerifyMessage())
				{
					throw new TlsFatalAlert(10);
				}
				this.ReceiveCertificateVerifyMessage(buf);
				this.mConnectionState = 12;
				return;
			}
			case 16:
				switch (this.mConnectionState)
				{
				case 8:
				case 9:
				case 10:
					if (this.mConnectionState < 9)
					{
						this.mTlsServer.ProcessClientSupplementalData(null);
					}
					if (this.mConnectionState < 10)
					{
						if (this.mCertificateRequest == null)
						{
							this.mKeyExchange.SkipClientCredentials();
						}
						else
						{
							if (TlsUtilities.IsTlsV12(this.Context))
							{
								throw new TlsFatalAlert(10);
							}
							if (TlsUtilities.IsSsl(this.Context))
							{
								if (this.mPeerCertificate == null)
								{
									throw new TlsFatalAlert(10);
								}
							}
							else
							{
								this.NotifyClientCertificate(Certificate.EmptyChain);
							}
						}
					}
					this.ReceiveClientKeyExchangeMessage(buf);
					this.mConnectionState = 11;
					return;
				default:
					throw new TlsFatalAlert(10);
				}
				break;
			case 20:
				switch (this.mConnectionState)
				{
				case 11:
				case 12:
					if (this.mConnectionState < 12 && this.ExpectCertificateVerifyMessage())
					{
						throw new TlsFatalAlert(10);
					}
					this.ProcessFinishedMessage(buf);
					this.mConnectionState = 13;
					if (this.mExpectSessionTicket)
					{
						this.SendNewSessionTicketMessage(this.mTlsServer.GetNewSessionTicket());
					}
					this.mConnectionState = 14;
					this.SendChangeCipherSpecMessage();
					this.SendFinishedMessage();
					this.mConnectionState = 15;
					this.CompleteHandshake();
					return;
				default:
					throw new TlsFatalAlert(10);
				}
				break;
			case 23:
			{
				short mConnectionState = this.mConnectionState;
				if (mConnectionState == 8)
				{
					this.mTlsServer.ProcessClientSupplementalData(TlsProtocol.ReadSupplementalDataMessage(buf));
					this.mConnectionState = 9;
					return;
				}
				throw new TlsFatalAlert(10);
			}
			}
			throw new TlsFatalAlert(10);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000DEF48 File Offset: 0x000DEF48
		protected override void HandleAlertWarningMessage(byte alertDescription)
		{
			if (41 == alertDescription && this.mCertificateRequest != null && TlsUtilities.IsSsl(this.mTlsServerContext))
			{
				switch (this.mConnectionState)
				{
				case 8:
				case 9:
					if (this.mConnectionState < 9)
					{
						this.mTlsServer.ProcessClientSupplementalData(null);
					}
					this.NotifyClientCertificate(Certificate.EmptyChain);
					this.mConnectionState = 10;
					return;
				}
			}
			base.HandleAlertWarningMessage(alertDescription);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000DEFCC File Offset: 0x000DEFCC
		protected virtual void NotifyClientCertificate(Certificate clientCertificate)
		{
			if (this.mCertificateRequest == null)
			{
				throw new InvalidOperationException();
			}
			if (this.mPeerCertificate != null)
			{
				throw new TlsFatalAlert(10);
			}
			this.mPeerCertificate = clientCertificate;
			if (clientCertificate.IsEmpty)
			{
				this.mKeyExchange.SkipClientCredentials();
			}
			else
			{
				this.mClientCertificateType = TlsUtilities.GetClientCertificateType(clientCertificate, this.mServerCredentials.Certificate);
				this.mKeyExchange.ProcessClientCertificate(clientCertificate);
			}
			this.mTlsServer.NotifyClientCertificate(clientCertificate);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000DF054 File Offset: 0x000DF054
		protected virtual void ReceiveCertificateMessage(MemoryStream buf)
		{
			Certificate clientCertificate = Certificate.Parse(buf);
			TlsProtocol.AssertEmpty(buf);
			this.NotifyClientCertificate(clientCertificate);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000DF07C File Offset: 0x000DF07C
		protected virtual void ReceiveCertificateVerifyMessage(MemoryStream buf)
		{
			if (this.mCertificateRequest == null)
			{
				throw new InvalidOperationException();
			}
			DigitallySigned digitallySigned = DigitallySigned.Parse(this.Context, buf);
			TlsProtocol.AssertEmpty(buf);
			try
			{
				SignatureAndHashAlgorithm algorithm = digitallySigned.Algorithm;
				byte[] hash;
				if (TlsUtilities.IsTlsV12(this.Context))
				{
					TlsUtilities.VerifySupportedSignatureAlgorithm(this.mCertificateRequest.SupportedSignatureAlgorithms, algorithm);
					hash = this.mPrepareFinishHash.GetFinalHash(algorithm.Hash);
				}
				else
				{
					hash = this.mSecurityParameters.SessionHash;
				}
				X509CertificateStructure certificateAt = this.mPeerCertificate.GetCertificateAt(0);
				SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
				AsymmetricKeyParameter publicKey = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
				TlsSigner tlsSigner = TlsUtilities.CreateTlsSigner((byte)this.mClientCertificateType);
				tlsSigner.Init(this.Context);
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

		// Token: 0x06002991 RID: 10641 RVA: 0x000DF184 File Offset: 0x000DF184
		protected virtual void ReceiveClientHelloMessage(MemoryStream buf)
		{
			ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(buf);
			this.mRecordStream.SetWriteVersion(protocolVersion);
			if (protocolVersion.IsDtls)
			{
				throw new TlsFatalAlert(47);
			}
			byte[] clientRandom = TlsUtilities.ReadFully(32, buf);
			byte[] array = TlsUtilities.ReadOpaque8(buf);
			if (array.Length > 32)
			{
				throw new TlsFatalAlert(47);
			}
			int num = TlsUtilities.ReadUint16(buf);
			if (num < 2 || (num & 1) != 0)
			{
				throw new TlsFatalAlert(50);
			}
			this.mOfferedCipherSuites = TlsUtilities.ReadUint16Array(num / 2, buf);
			int num2 = (int)TlsUtilities.ReadUint8(buf);
			if (num2 < 1)
			{
				throw new TlsFatalAlert(47);
			}
			this.mOfferedCompressionMethods = TlsUtilities.ReadUint8Array(num2, buf);
			this.mClientExtensions = TlsProtocol.ReadExtensions(buf);
			this.mSecurityParameters.extendedMasterSecret = TlsExtensionsUtilities.HasExtendedMasterSecretExtension(this.mClientExtensions);
			if (!this.mSecurityParameters.IsExtendedMasterSecret && this.mTlsServer.RequiresExtendedMasterSecret())
			{
				throw new TlsFatalAlert(40);
			}
			this.ContextAdmin.SetClientVersion(protocolVersion);
			this.mTlsServer.NotifyClientVersion(protocolVersion);
			this.mTlsServer.NotifyFallback(Arrays.Contains(this.mOfferedCipherSuites, 22016));
			this.mSecurityParameters.clientRandom = clientRandom;
			this.mTlsServer.NotifyOfferedCipherSuites(this.mOfferedCipherSuites);
			this.mTlsServer.NotifyOfferedCompressionMethods(this.mOfferedCompressionMethods);
			if (Arrays.Contains(this.mOfferedCipherSuites, 255))
			{
				this.mSecureRenegotiation = true;
			}
			byte[] extensionData = TlsUtilities.GetExtensionData(this.mClientExtensions, 65281);
			if (extensionData != null)
			{
				this.mSecureRenegotiation = true;
				if (!Arrays.ConstantTimeAreEqual(extensionData, TlsProtocol.CreateRenegotiationInfo(TlsUtilities.EmptyBytes)))
				{
					throw new TlsFatalAlert(40);
				}
			}
			this.mTlsServer.NotifySecureRenegotiation(this.mSecureRenegotiation);
			if (this.mClientExtensions != null)
			{
				TlsExtensionsUtilities.GetPaddingExtension(this.mClientExtensions);
				this.mTlsServer.ProcessClientExtensions(this.mClientExtensions);
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000DF370 File Offset: 0x000DF370
		protected virtual void ReceiveClientKeyExchangeMessage(MemoryStream buf)
		{
			this.mKeyExchange.ProcessClientKeyExchange(buf);
			TlsProtocol.AssertEmpty(buf);
			if (TlsUtilities.IsSsl(this.Context))
			{
				TlsProtocol.EstablishMasterSecret(this.Context, this.mKeyExchange);
			}
			this.mPrepareFinishHash = this.mRecordStream.PrepareToFinish();
			this.mSecurityParameters.sessionHash = TlsProtocol.GetCurrentPrfHash(this.Context, this.mPrepareFinishHash, null);
			if (!TlsUtilities.IsSsl(this.Context))
			{
				TlsProtocol.EstablishMasterSecret(this.Context, this.mKeyExchange);
			}
			this.mRecordStream.SetPendingConnectionState(this.Peer.GetCompression(), this.Peer.GetCipher());
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000DF424 File Offset: 0x000DF424
		protected virtual void SendCertificateRequestMessage(CertificateRequest certificateRequest)
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(13);
			certificateRequest.Encode(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000DF44C File Offset: 0x000DF44C
		protected virtual void SendCertificateStatusMessage(CertificateStatus certificateStatus)
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(22);
			certificateStatus.Encode(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000DF474 File Offset: 0x000DF474
		protected virtual void SendNewSessionTicketMessage(NewSessionTicket newSessionTicket)
		{
			if (newSessionTicket == null)
			{
				throw new TlsFatalAlert(80);
			}
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(4);
			newSessionTicket.Encode(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000DF4A8 File Offset: 0x000DF4A8
		protected virtual void SendServerHelloMessage()
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(2);
			ProtocolVersion serverVersion = this.mTlsServer.GetServerVersion();
			if (!serverVersion.IsEqualOrEarlierVersionOf(this.Context.ClientVersion))
			{
				throw new TlsFatalAlert(80);
			}
			this.mRecordStream.ReadVersion = serverVersion;
			this.mRecordStream.SetWriteVersion(serverVersion);
			this.mRecordStream.SetRestrictReadVersion(true);
			this.ContextAdmin.SetServerVersion(serverVersion);
			TlsUtilities.WriteVersion(serverVersion, handshakeMessage);
			handshakeMessage.Write(this.mSecurityParameters.serverRandom);
			TlsUtilities.WriteOpaque8(TlsUtilities.EmptyBytes, handshakeMessage);
			int selectedCipherSuite = this.mTlsServer.GetSelectedCipherSuite();
			if (!Arrays.Contains(this.mOfferedCipherSuites, selectedCipherSuite) || selectedCipherSuite == 0 || CipherSuite.IsScsv(selectedCipherSuite) || !TlsUtilities.IsValidCipherSuiteForVersion(selectedCipherSuite, this.Context.ServerVersion))
			{
				throw new TlsFatalAlert(80);
			}
			this.mSecurityParameters.cipherSuite = selectedCipherSuite;
			byte selectedCompressionMethod = this.mTlsServer.GetSelectedCompressionMethod();
			if (!Arrays.Contains(this.mOfferedCompressionMethods, selectedCompressionMethod))
			{
				throw new TlsFatalAlert(80);
			}
			this.mSecurityParameters.compressionAlgorithm = selectedCompressionMethod;
			TlsUtilities.WriteUint16(selectedCipherSuite, handshakeMessage);
			TlsUtilities.WriteUint8(selectedCompressionMethod, handshakeMessage);
			this.mServerExtensions = TlsExtensionsUtilities.EnsureExtensionsInitialised(this.mTlsServer.GetServerExtensions());
			if (this.mSecureRenegotiation)
			{
				byte[] extensionData = TlsUtilities.GetExtensionData(this.mServerExtensions, 65281);
				bool flag = null == extensionData;
				if (flag)
				{
					this.mServerExtensions[65281] = TlsProtocol.CreateRenegotiationInfo(TlsUtilities.EmptyBytes);
				}
			}
			if (TlsUtilities.IsSsl(this.mTlsServerContext))
			{
				this.mSecurityParameters.extendedMasterSecret = false;
			}
			else if (this.mSecurityParameters.IsExtendedMasterSecret)
			{
				TlsExtensionsUtilities.AddExtendedMasterSecretExtension(this.mServerExtensions);
			}
			if (this.mServerExtensions.Count > 0)
			{
				this.mSecurityParameters.encryptThenMac = TlsExtensionsUtilities.HasEncryptThenMacExtension(this.mServerExtensions);
				this.mSecurityParameters.maxFragmentLength = this.ProcessMaxFragmentLengthExtension(this.mClientExtensions, this.mServerExtensions, 80);
				this.mSecurityParameters.truncatedHMac = TlsExtensionsUtilities.HasTruncatedHMacExtension(this.mServerExtensions);
				this.mAllowCertificateStatus = (!this.mResumedSession && TlsUtilities.HasExpectedEmptyExtensionData(this.mServerExtensions, 5, 80));
				this.mExpectSessionTicket = (!this.mResumedSession && TlsUtilities.HasExpectedEmptyExtensionData(this.mServerExtensions, 35, 80));
				TlsProtocol.WriteExtensions(handshakeMessage, this.mServerExtensions);
			}
			this.mSecurityParameters.prfAlgorithm = TlsProtocol.GetPrfAlgorithm(this.Context, this.mSecurityParameters.CipherSuite);
			this.mSecurityParameters.verifyDataLength = 12;
			this.ApplyMaxFragmentLengthExtension();
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000DF760 File Offset: 0x000DF760
		protected virtual void SendServerHelloDoneMessage()
		{
			byte[] array = new byte[4];
			TlsUtilities.WriteUint8(14, array, 0);
			TlsUtilities.WriteUint24(0, array, 1);
			this.WriteHandshakeMessage(array, 0, array.Length);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000DF794 File Offset: 0x000DF794
		protected virtual void SendServerKeyExchangeMessage(byte[] serverKeyExchange)
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(12, serverKeyExchange.Length);
			handshakeMessage.Write(serverKeyExchange);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000DF7C0 File Offset: 0x000DF7C0
		protected virtual bool ExpectCertificateVerifyMessage()
		{
			return this.mClientCertificateType >= 0 && TlsUtilities.HasSigningCapability((byte)this.mClientCertificateType);
		}

		// Token: 0x04001B02 RID: 6914
		protected TlsServer mTlsServer = null;

		// Token: 0x04001B03 RID: 6915
		internal TlsServerContextImpl mTlsServerContext = null;

		// Token: 0x04001B04 RID: 6916
		protected TlsKeyExchange mKeyExchange = null;

		// Token: 0x04001B05 RID: 6917
		protected TlsCredentials mServerCredentials = null;

		// Token: 0x04001B06 RID: 6918
		protected CertificateRequest mCertificateRequest = null;

		// Token: 0x04001B07 RID: 6919
		protected short mClientCertificateType = -1;

		// Token: 0x04001B08 RID: 6920
		protected TlsHandshakeHash mPrepareFinishHash = null;
	}
}
