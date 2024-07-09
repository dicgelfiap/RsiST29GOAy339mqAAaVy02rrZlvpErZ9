using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200052D RID: 1325
	public class TlsClientProtocol : TlsProtocol
	{
		// Token: 0x0600286A RID: 10346 RVA: 0x000D9B38 File Offset: 0x000D9B38
		public TlsClientProtocol(Stream stream, SecureRandom secureRandom) : base(stream, secureRandom)
		{
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000D9B74 File Offset: 0x000D9B74
		public TlsClientProtocol(Stream input, Stream output, SecureRandom secureRandom) : base(input, output, secureRandom)
		{
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x000D9BB0 File Offset: 0x000D9BB0
		public TlsClientProtocol(SecureRandom secureRandom) : base(secureRandom)
		{
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000D9BEC File Offset: 0x000D9BEC
		public virtual void Connect(TlsClient tlsClient)
		{
			if (tlsClient == null)
			{
				throw new ArgumentNullException("tlsClient");
			}
			if (this.mTlsClient != null)
			{
				throw new InvalidOperationException("'Connect' can only be called once");
			}
			this.mTlsClient = tlsClient;
			this.mSecurityParameters = new SecurityParameters();
			this.mSecurityParameters.entity = 1;
			this.mTlsClientContext = new TlsClientContextImpl(this.mSecureRandom, this.mSecurityParameters);
			this.mSecurityParameters.clientRandom = TlsProtocol.CreateRandomBlock(tlsClient.ShouldUseGmtUnixTime(), this.mTlsClientContext.NonceRandomGenerator);
			this.mTlsClient.Init(this.mTlsClientContext);
			this.mRecordStream.Init(this.mTlsClientContext);
			tlsClient.NotifyCloseHandle(this);
			TlsSession sessionToResume = tlsClient.GetSessionToResume();
			if (sessionToResume != null && sessionToResume.IsResumable)
			{
				SessionParameters sessionParameters = sessionToResume.ExportSessionParameters();
				if (sessionParameters != null && sessionParameters.IsExtendedMasterSecret)
				{
					this.mTlsSession = sessionToResume;
					this.mSessionParameters = sessionParameters;
				}
			}
			this.SendClientHelloMessage();
			this.mConnectionState = 1;
			this.BlockForHandshake();
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000D9CF4 File Offset: 0x000D9CF4
		protected override void CleanupHandshake()
		{
			base.CleanupHandshake();
			this.mSelectedSessionID = null;
			this.mKeyExchange = null;
			this.mAuthentication = null;
			this.mCertificateStatus = null;
			this.mCertificateRequest = null;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000D9D20 File Offset: 0x000D9D20
		protected override TlsContext Context
		{
			get
			{
				return this.mTlsClientContext;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x000D9D28 File Offset: 0x000D9D28
		internal override AbstractTlsContext ContextAdmin
		{
			get
			{
				return this.mTlsClientContext;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000D9D30 File Offset: 0x000D9D30
		protected override TlsPeer Peer
		{
			get
			{
				return this.mTlsClient;
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000D9D38 File Offset: 0x000D9D38
		protected override void HandleHandshakeMessage(byte type, MemoryStream buf)
		{
			if (!this.mResumedSession)
			{
				switch (type)
				{
				case 0:
					TlsProtocol.AssertEmpty(buf);
					if (this.mConnectionState == 16)
					{
						this.RefuseRenegotiation();
						return;
					}
					return;
				case 2:
				{
					short mConnectionState = this.mConnectionState;
					if (mConnectionState != 1)
					{
						throw new TlsFatalAlert(10);
					}
					this.ReceiveServerHelloMessage(buf);
					this.mConnectionState = 2;
					this.mRecordStream.NotifyHelloComplete();
					this.ApplyMaxFragmentLengthExtension();
					if (this.mResumedSession)
					{
						this.mSecurityParameters.masterSecret = Arrays.Clone(this.mSessionParameters.MasterSecret);
						this.mRecordStream.SetPendingConnectionState(this.Peer.GetCompression(), this.Peer.GetCipher());
						return;
					}
					this.InvalidateSession();
					if (this.mSelectedSessionID.Length > 0)
					{
						this.mTlsSession = new TlsSessionImpl(this.mSelectedSessionID, null);
						return;
					}
					return;
				}
				case 4:
				{
					short mConnectionState = this.mConnectionState;
					if (mConnectionState != 13)
					{
						throw new TlsFatalAlert(10);
					}
					if (!this.mExpectSessionTicket)
					{
						throw new TlsFatalAlert(10);
					}
					this.InvalidateSession();
					this.ReceiveNewSessionTicketMessage(buf);
					this.mConnectionState = 14;
					return;
				}
				case 11:
					switch (this.mConnectionState)
					{
					case 2:
					case 3:
						if (this.mConnectionState == 2)
						{
							this.HandleSupplementalData(null);
						}
						this.mPeerCertificate = Certificate.Parse(buf);
						TlsProtocol.AssertEmpty(buf);
						if (this.mPeerCertificate == null || this.mPeerCertificate.IsEmpty)
						{
							this.mAllowCertificateStatus = false;
						}
						this.mKeyExchange.ProcessServerCertificate(this.mPeerCertificate);
						this.mAuthentication = this.mTlsClient.GetAuthentication();
						this.mAuthentication.NotifyServerCertificate(this.mPeerCertificate);
						this.mConnectionState = 4;
						return;
					default:
						throw new TlsFatalAlert(10);
					}
					break;
				case 12:
					switch (this.mConnectionState)
					{
					case 2:
					case 3:
					case 4:
					case 5:
						if (this.mConnectionState < 3)
						{
							this.HandleSupplementalData(null);
						}
						if (this.mConnectionState < 4)
						{
							this.mKeyExchange.SkipServerCredentials();
							this.mAuthentication = null;
						}
						this.mKeyExchange.ProcessServerKeyExchange(buf);
						TlsProtocol.AssertEmpty(buf);
						this.mConnectionState = 6;
						return;
					default:
						throw new TlsFatalAlert(10);
					}
					break;
				case 13:
					switch (this.mConnectionState)
					{
					case 4:
					case 5:
					case 6:
						if (this.mConnectionState != 6)
						{
							this.mKeyExchange.SkipServerKeyExchange();
						}
						if (this.mAuthentication == null)
						{
							throw new TlsFatalAlert(40);
						}
						this.mCertificateRequest = CertificateRequest.Parse(this.Context, buf);
						TlsProtocol.AssertEmpty(buf);
						this.mKeyExchange.ValidateCertificateRequest(this.mCertificateRequest);
						TlsUtilities.TrackHashAlgorithms(this.mRecordStream.HandshakeHash, this.mCertificateRequest.SupportedSignatureAlgorithms);
						this.mConnectionState = 7;
						return;
					default:
						throw new TlsFatalAlert(10);
					}
					break;
				case 14:
					switch (this.mConnectionState)
					{
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					{
						if (this.mConnectionState < 3)
						{
							this.HandleSupplementalData(null);
						}
						if (this.mConnectionState < 4)
						{
							this.mKeyExchange.SkipServerCredentials();
							this.mAuthentication = null;
						}
						if (this.mConnectionState < 6)
						{
							this.mKeyExchange.SkipServerKeyExchange();
						}
						TlsProtocol.AssertEmpty(buf);
						this.mConnectionState = 8;
						this.mRecordStream.HandshakeHash.SealHashAlgorithms();
						IList clientSupplementalData = this.mTlsClient.GetClientSupplementalData();
						if (clientSupplementalData != null)
						{
							this.SendSupplementalDataMessage(clientSupplementalData);
						}
						this.mConnectionState = 9;
						TlsCredentials tlsCredentials = null;
						if (this.mCertificateRequest == null)
						{
							this.mKeyExchange.SkipClientCredentials();
						}
						else
						{
							tlsCredentials = this.mAuthentication.GetClientCredentials(this.mCertificateRequest);
							if (tlsCredentials == null)
							{
								this.mKeyExchange.SkipClientCredentials();
								this.SendCertificateMessage(Certificate.EmptyChain);
							}
							else
							{
								this.mKeyExchange.ProcessClientCredentials(tlsCredentials);
								this.SendCertificateMessage(tlsCredentials.Certificate);
							}
						}
						this.mConnectionState = 10;
						this.SendClientKeyExchangeMessage();
						this.mConnectionState = 11;
						if (TlsUtilities.IsSsl(this.Context))
						{
							TlsProtocol.EstablishMasterSecret(this.Context, this.mKeyExchange);
						}
						TlsHandshakeHash tlsHandshakeHash = this.mRecordStream.PrepareToFinish();
						this.mSecurityParameters.sessionHash = TlsProtocol.GetCurrentPrfHash(this.Context, tlsHandshakeHash, null);
						if (!TlsUtilities.IsSsl(this.Context))
						{
							TlsProtocol.EstablishMasterSecret(this.Context, this.mKeyExchange);
						}
						this.mRecordStream.SetPendingConnectionState(this.Peer.GetCompression(), this.Peer.GetCipher());
						if (tlsCredentials != null && tlsCredentials is TlsSignerCredentials)
						{
							TlsSignerCredentials tlsSignerCredentials = (TlsSignerCredentials)tlsCredentials;
							SignatureAndHashAlgorithm signatureAndHashAlgorithm = TlsUtilities.GetSignatureAndHashAlgorithm(this.Context, tlsSignerCredentials);
							byte[] hash;
							if (signatureAndHashAlgorithm == null)
							{
								hash = this.mSecurityParameters.SessionHash;
							}
							else
							{
								hash = tlsHandshakeHash.GetFinalHash(signatureAndHashAlgorithm.Hash);
							}
							byte[] signature = tlsSignerCredentials.GenerateCertificateSignature(hash);
							DigitallySigned certificateVerify = new DigitallySigned(signatureAndHashAlgorithm, signature);
							this.SendCertificateVerifyMessage(certificateVerify);
							this.mConnectionState = 12;
						}
						this.SendChangeCipherSpecMessage();
						this.SendFinishedMessage();
						this.mConnectionState = 13;
						return;
					}
					default:
						throw new TlsFatalAlert(10);
					}
					break;
				case 20:
					switch (this.mConnectionState)
					{
					case 13:
					case 14:
						if (this.mConnectionState == 13 && this.mExpectSessionTicket)
						{
							throw new TlsFatalAlert(10);
						}
						this.ProcessFinishedMessage(buf);
						this.mConnectionState = 15;
						this.CompleteHandshake();
						return;
					default:
						throw new TlsFatalAlert(10);
					}
					break;
				case 22:
				{
					short mConnectionState = this.mConnectionState;
					if (mConnectionState != 4)
					{
						throw new TlsFatalAlert(10);
					}
					if (!this.mAllowCertificateStatus)
					{
						throw new TlsFatalAlert(10);
					}
					this.mCertificateStatus = CertificateStatus.Parse(buf);
					TlsProtocol.AssertEmpty(buf);
					this.mConnectionState = 5;
					return;
				}
				case 23:
				{
					short mConnectionState = this.mConnectionState;
					if (mConnectionState == 2)
					{
						this.HandleSupplementalData(TlsProtocol.ReadSupplementalDataMessage(buf));
						return;
					}
					throw new TlsFatalAlert(10);
				}
				}
				throw new TlsFatalAlert(10);
			}
			if (type != 20 || this.mConnectionState != 2)
			{
				throw new TlsFatalAlert(10);
			}
			this.ProcessFinishedMessage(buf);
			this.mConnectionState = 15;
			this.SendChangeCipherSpecMessage();
			this.SendFinishedMessage();
			this.mConnectionState = 13;
			this.CompleteHandshake();
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000DA3F0 File Offset: 0x000DA3F0
		protected virtual void HandleSupplementalData(IList serverSupplementalData)
		{
			this.mTlsClient.ProcessServerSupplementalData(serverSupplementalData);
			this.mConnectionState = 3;
			this.mKeyExchange = this.mTlsClient.GetKeyExchange();
			this.mKeyExchange.Init(this.Context);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000DA438 File Offset: 0x000DA438
		protected virtual void ReceiveNewSessionTicketMessage(MemoryStream buf)
		{
			NewSessionTicket newSessionTicket = NewSessionTicket.Parse(buf);
			TlsProtocol.AssertEmpty(buf);
			this.mTlsClient.NotifyNewSessionTicket(newSessionTicket);
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000DA464 File Offset: 0x000DA464
		protected virtual void ReceiveServerHelloMessage(MemoryStream buf)
		{
			ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(buf);
			if (protocolVersion.IsDtls)
			{
				throw new TlsFatalAlert(47);
			}
			if (!protocolVersion.Equals(this.mRecordStream.ReadVersion))
			{
				throw new TlsFatalAlert(47);
			}
			ProtocolVersion clientVersion = this.Context.ClientVersion;
			if (!protocolVersion.IsEqualOrEarlierVersionOf(clientVersion))
			{
				throw new TlsFatalAlert(47);
			}
			this.mRecordStream.SetWriteVersion(protocolVersion);
			this.ContextAdmin.SetServerVersion(protocolVersion);
			this.mTlsClient.NotifyServerVersion(protocolVersion);
			this.mSecurityParameters.serverRandom = TlsUtilities.ReadFully(32, buf);
			this.mSelectedSessionID = TlsUtilities.ReadOpaque8(buf);
			if (this.mSelectedSessionID.Length > 32)
			{
				throw new TlsFatalAlert(47);
			}
			this.mTlsClient.NotifySessionID(this.mSelectedSessionID);
			this.mResumedSession = (this.mSelectedSessionID.Length > 0 && this.mTlsSession != null && Arrays.AreEqual(this.mSelectedSessionID, this.mTlsSession.SessionID));
			int num = TlsUtilities.ReadUint16(buf);
			if (!Arrays.Contains(this.mOfferedCipherSuites, num) || num == 0 || CipherSuite.IsScsv(num) || !TlsUtilities.IsValidCipherSuiteForVersion(num, this.Context.ServerVersion))
			{
				throw new TlsFatalAlert(47);
			}
			this.mTlsClient.NotifySelectedCipherSuite(num);
			byte b = TlsUtilities.ReadUint8(buf);
			if (!Arrays.Contains(this.mOfferedCompressionMethods, b))
			{
				throw new TlsFatalAlert(47);
			}
			this.mTlsClient.NotifySelectedCompressionMethod(b);
			this.mServerExtensions = TlsProtocol.ReadExtensions(buf);
			this.mSecurityParameters.extendedMasterSecret = (!TlsUtilities.IsSsl(this.mTlsClientContext) && TlsExtensionsUtilities.HasExtendedMasterSecretExtension(this.mServerExtensions));
			if (!this.mSecurityParameters.IsExtendedMasterSecret && (this.mResumedSession || this.mTlsClient.RequiresExtendedMasterSecret()))
			{
				throw new TlsFatalAlert(40);
			}
			if (this.mServerExtensions != null)
			{
				foreach (object obj in this.mServerExtensions.Keys)
				{
					int num2 = (int)obj;
					if (num2 != 65281)
					{
						if (TlsUtilities.GetExtensionData(this.mClientExtensions, num2) == null)
						{
							throw new TlsFatalAlert(110);
						}
						bool mResumedSession = this.mResumedSession;
					}
				}
			}
			byte[] extensionData = TlsUtilities.GetExtensionData(this.mServerExtensions, 65281);
			if (extensionData != null)
			{
				this.mSecureRenegotiation = true;
				if (!Arrays.ConstantTimeAreEqual(extensionData, TlsProtocol.CreateRenegotiationInfo(TlsUtilities.EmptyBytes)))
				{
					throw new TlsFatalAlert(40);
				}
			}
			this.mTlsClient.NotifySecureRenegotiation(this.mSecureRenegotiation);
			IDictionary dictionary = this.mClientExtensions;
			IDictionary dictionary2 = this.mServerExtensions;
			if (this.mResumedSession)
			{
				if (num != this.mSessionParameters.CipherSuite || b != this.mSessionParameters.CompressionAlgorithm)
				{
					throw new TlsFatalAlert(47);
				}
				dictionary = null;
				dictionary2 = this.mSessionParameters.ReadServerExtensions();
			}
			this.mSecurityParameters.cipherSuite = num;
			this.mSecurityParameters.compressionAlgorithm = b;
			if (dictionary2 != null && dictionary2.Count > 0)
			{
				bool flag = TlsExtensionsUtilities.HasEncryptThenMacExtension(dictionary2);
				if (flag && !TlsUtilities.IsBlockCipherSuite(num))
				{
					throw new TlsFatalAlert(47);
				}
				this.mSecurityParameters.encryptThenMac = flag;
				this.mSecurityParameters.maxFragmentLength = this.ProcessMaxFragmentLengthExtension(dictionary, dictionary2, 47);
				this.mSecurityParameters.truncatedHMac = TlsExtensionsUtilities.HasTruncatedHMacExtension(dictionary2);
				this.mAllowCertificateStatus = (!this.mResumedSession && TlsUtilities.HasExpectedEmptyExtensionData(dictionary2, 5, 47));
				this.mExpectSessionTicket = (!this.mResumedSession && TlsUtilities.HasExpectedEmptyExtensionData(dictionary2, 35, 47));
			}
			if (dictionary != null)
			{
				this.mTlsClient.ProcessServerExtensions(dictionary2);
			}
			this.mSecurityParameters.prfAlgorithm = TlsProtocol.GetPrfAlgorithm(this.Context, this.mSecurityParameters.CipherSuite);
			this.mSecurityParameters.verifyDataLength = 12;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000DA89C File Offset: 0x000DA89C
		protected virtual void SendCertificateVerifyMessage(DigitallySigned certificateVerify)
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(15);
			certificateVerify.Encode(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000DA8C4 File Offset: 0x000DA8C4
		protected virtual void SendClientHelloMessage()
		{
			this.mRecordStream.SetWriteVersion(this.mTlsClient.ClientHelloRecordLayerVersion);
			ProtocolVersion clientVersion = this.mTlsClient.ClientVersion;
			if (clientVersion.IsDtls)
			{
				throw new TlsFatalAlert(80);
			}
			this.ContextAdmin.SetClientVersion(clientVersion);
			byte[] array = TlsUtilities.EmptyBytes;
			if (this.mTlsSession != null)
			{
				array = this.mTlsSession.SessionID;
				if (array == null || array.Length > 32)
				{
					array = TlsUtilities.EmptyBytes;
				}
			}
			bool isFallback = this.mTlsClient.IsFallback;
			this.mOfferedCipherSuites = this.mTlsClient.GetCipherSuites();
			this.mOfferedCompressionMethods = this.mTlsClient.GetCompressionMethods();
			if (array.Length > 0 && this.mSessionParameters != null && (!this.mSessionParameters.IsExtendedMasterSecret || !Arrays.Contains(this.mOfferedCipherSuites, this.mSessionParameters.CipherSuite) || !Arrays.Contains(this.mOfferedCompressionMethods, this.mSessionParameters.CompressionAlgorithm)))
			{
				array = TlsUtilities.EmptyBytes;
			}
			this.mClientExtensions = TlsExtensionsUtilities.EnsureExtensionsInitialised(this.mTlsClient.GetClientExtensions());
			if (!clientVersion.IsSsl)
			{
				TlsExtensionsUtilities.AddExtendedMasterSecretExtension(this.mClientExtensions);
			}
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(1);
			TlsUtilities.WriteVersion(clientVersion, handshakeMessage);
			handshakeMessage.Write(this.mSecurityParameters.ClientRandom);
			TlsUtilities.WriteOpaque8(array, handshakeMessage);
			byte[] extensionData = TlsUtilities.GetExtensionData(this.mClientExtensions, 65281);
			bool flag = null == extensionData;
			bool flag2 = !Arrays.Contains(this.mOfferedCipherSuites, 255);
			if (flag && flag2)
			{
				this.mOfferedCipherSuites = Arrays.Append(this.mOfferedCipherSuites, 255);
			}
			if (isFallback && !Arrays.Contains(this.mOfferedCipherSuites, 22016))
			{
				this.mOfferedCipherSuites = Arrays.Append(this.mOfferedCipherSuites, 22016);
			}
			TlsUtilities.WriteUint16ArrayWithUint16Length(this.mOfferedCipherSuites, handshakeMessage);
			TlsUtilities.WriteUint8ArrayWithUint8Length(this.mOfferedCompressionMethods, handshakeMessage);
			TlsProtocol.WriteExtensions(handshakeMessage, this.mClientExtensions);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000DAAD8 File Offset: 0x000DAAD8
		protected virtual void SendClientKeyExchangeMessage()
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(16);
			this.mKeyExchange.GenerateClientKeyExchange(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x04001AB0 RID: 6832
		protected TlsClient mTlsClient = null;

		// Token: 0x04001AB1 RID: 6833
		internal TlsClientContextImpl mTlsClientContext = null;

		// Token: 0x04001AB2 RID: 6834
		protected byte[] mSelectedSessionID = null;

		// Token: 0x04001AB3 RID: 6835
		protected TlsKeyExchange mKeyExchange = null;

		// Token: 0x04001AB4 RID: 6836
		protected TlsAuthentication mAuthentication = null;

		// Token: 0x04001AB5 RID: 6837
		protected CertificateStatus mCertificateStatus = null;

		// Token: 0x04001AB6 RID: 6838
		protected CertificateRequest mCertificateRequest = null;
	}
}
