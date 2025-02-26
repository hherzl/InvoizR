package sv.mh.fe.models.minec;

import sv.mh.fe.models.Llave;

public class BasicEstructure {
	
	private String version;
	
	private String serial;
	
	private SignatureAlgorithm signatureAlgorithm;
	
	private Issuer issuer;
	
	private Validity validity;
	
	private Subject subject;
	
	private SubjectPublicKeyInfo subjectPublicKeyInfo;
	
	public BasicEstructure() {
		super();
	}

	public BasicEstructure(Subject subject, Llave llave) {
		super();
		this.version = "2";
		this.serial = "1.2.840.113549.1.1.11";	
		this.signatureAlgorithm = new SignatureAlgorithm();
		this.issuer = new Issuer();
		this.validity = new Validity();
		this.subject = subject;
		
		this.subjectPublicKeyInfo = new SubjectPublicKeyInfo(
				new AlgorithmIdenitifier(llave.getAlgorithm(), null),
				llave.getEncodied()) ;		
	}	

	public BasicEstructure(String version, String serial, SignatureAlgorithm signatureAlgorithm, Issuer issuer,
			Validity validity, Subject subject, SubjectPublicKeyInfo subjectPublicKeyInfo) {
		super();
		this.version = version;
		this.serial = serial;
		this.signatureAlgorithm = signatureAlgorithm;
		this.issuer = issuer;
		this.validity = validity;
		this.subject = subject;
		this.subjectPublicKeyInfo = subjectPublicKeyInfo;
	}

	public String getVersion() {
		return version;
	}

	public void setVersion(String version) {
		this.version = version;
	}

	public String getSerial() {
		return serial;
	}

	public void setSerial(String serial) {
		this.serial = serial;
	}

	public SignatureAlgorithm getSignatureAlgorithm() {
		return signatureAlgorithm;
	}

	public void setSignatureAlgorithm(SignatureAlgorithm signatureAlgorithm) {
		this.signatureAlgorithm = signatureAlgorithm;
	}

	public Issuer getIssuer() {
		return issuer;
	}

	public void setIssuer(Issuer issuer) {
		this.issuer = issuer;
	}

	public Validity getValidity() {
		return validity;
	}

	public void setValidity(Validity validity) {
		this.validity = validity;
	}

	public Subject getSubject() {
		return subject;
	}

	public void setSubject(Subject subject) {
		this.subject = subject;
	}

	public SubjectPublicKeyInfo getSubjectPublicKeyInfo() {
		return subjectPublicKeyInfo;
	}

	public void setSubjectPublicKeyInfo(SubjectPublicKeyInfo subjectPublicKeyInfo) {
		this.subjectPublicKeyInfo = subjectPublicKeyInfo;
	}
	
}
