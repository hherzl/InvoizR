package sv.mh.fe.models.minec;

public class KeyUsage {

	private short digitalSignature;
	
	private short contentCommintment;
	
	private short dataEncipherment;
	
	private short keyAgreement;
	
	private short keyCertificateSignature;
	
	private short crlSignature;
	
	private short encipherOnly;
	
	private short decipherOnly;

	public KeyUsage() {
		super();
		this.digitalSignature = 1;
		this.contentCommintment = 1;
		this.dataEncipherment = 0;
		this.keyAgreement = 0;
		this.keyCertificateSignature = 0;
		this.crlSignature = 0;
		this.encipherOnly = 0;
		this.decipherOnly = 0;
	}
	
	public KeyUsage(short digitalSignature, short contentCommintment, short dataEncipherment, short keyAgreement,
			short keyCertificateSignature, short crlSignature, short encipherOnly, short decipherOnly) {
		super();
		this.digitalSignature = digitalSignature;
		this.contentCommintment = contentCommintment;
		this.dataEncipherment = dataEncipherment;
		this.keyAgreement = keyAgreement;
		this.keyCertificateSignature = keyCertificateSignature;
		this.crlSignature = crlSignature;
		this.encipherOnly = encipherOnly;
		this.decipherOnly = decipherOnly;
	}

	public short getDigitalSignature() {
		return digitalSignature;
	}

	public void setDigitalSignature(short digitalSignature) {
		this.digitalSignature = digitalSignature;
	}

	public short getContentCommintment() {
		return contentCommintment;
	}

	public void setContentCommintment(short contentCommintment) {
		this.contentCommintment = contentCommintment;
	}

	public short getDataEncipherment() {
		return dataEncipherment;
	}

	public void setDataEncipherment(short dataEncipherment) {
		this.dataEncipherment = dataEncipherment;
	}

	public short getKeyAgreement() {
		return keyAgreement;
	}

	public void setKeyAgreement(short keyAgreement) {
		this.keyAgreement = keyAgreement;
	}

	public short getKeyCertificateSignature() {
		return keyCertificateSignature;
	}

	public void setKeyCertificateSignature(short keyCertificateSignature) {
		this.keyCertificateSignature = keyCertificateSignature;
	}

	public short getCrlSignature() {
		return crlSignature;
	}

	public void setCrlSignature(short crlSignature) {
		this.crlSignature = crlSignature;
	}

	public short getEncipherOnly() {
		return encipherOnly;
	}

	public void setEncipherOnly(short encipherOnly) {
		this.encipherOnly = encipherOnly;
	}

	public short getDecipherOnly() {
		return decipherOnly;
	}

	public void setDecipherOnly(short decipherOnly) {
		this.decipherOnly = decipherOnly;
	}
		
}
