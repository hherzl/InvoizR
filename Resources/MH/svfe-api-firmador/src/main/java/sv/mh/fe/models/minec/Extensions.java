package sv.mh.fe.models.minec;

import java.util.List;

public class Extensions {

	private AuthorityKeyIdentifier authorityKeyIdentifier;
	
	private SubjectKeyIdentifier subjectKeyIdentifier;
	
	private KeyUsage keyUsage;
	
	private CertificatePolicies certificatePolicies;
	
	private SubjectAlternativeNames subjectAlternativeNames;
	
	private ExtendedKeyUsage extendedKeyUsage;
	
	private CrlDistributionPoint crlDistributionPoint;
	
	private AuthorityInfoAccess authorityInfoAccess;
	
	private QualifiedCertificateStatements qualifiedCertificateStatements;
	
	private BasicConstraints basicConstraints;
	
	public Extensions(String rfc822Name, List<String> crlDistributionPoint, List<String> accessLocation) {
		super();
		this.authorityKeyIdentifier = new AuthorityKeyIdentifier();
		this.subjectKeyIdentifier = new SubjectKeyIdentifier();
		this.keyUsage = new KeyUsage();
		this.certificatePolicies = new CertificatePolicies();
		this.subjectAlternativeNames = new SubjectAlternativeNames(rfc822Name);
		this.extendedKeyUsage = new ExtendedKeyUsage();
		
		this.crlDistributionPoint = new CrlDistributionPoint(crlDistributionPoint);
//		this.crlDistributionPoint.add("http://www2.mh.gob.sv/crl");
//		this.crlDistributionPoint.add("http://www2.mh.gob.sv/crl2");
		
		this.authorityInfoAccess = new AuthorityInfoAccess();
		this.authorityInfoAccess.addAccessDescription(new AccessDescription(accessLocation));
//		accessDescription.addAccessLocation("https://www.minec.gob.sv/ca/public/donwload/subordinadal.crt");
		
		this.qualifiedCertificateStatements = new QualifiedCertificateStatements(new QcPDS());
		this.basicConstraints = new BasicConstraints();
	}	
	
	public Extensions() {
		super();
	}

	public Extensions(AuthorityKeyIdentifier authorityKeyIdentifier, SubjectKeyIdentifier subjectKeyIdentifier,
			KeyUsage keyUsage, CertificatePolicies certificatePolicies, SubjectAlternativeNames subjectAlternativeNames,
			ExtendedKeyUsage extendedKeyUsage, CrlDistributionPoint crlDistributionPoint,
			AuthorityInfoAccess authorityInfoAccess, QualifiedCertificateStatements qualifiedCertificateStatements,
			BasicConstraints basicConstraints) {
		super();
		this.authorityKeyIdentifier = authorityKeyIdentifier;
		this.subjectKeyIdentifier = subjectKeyIdentifier;
		this.keyUsage = keyUsage;
		this.certificatePolicies = certificatePolicies;
		this.subjectAlternativeNames = subjectAlternativeNames;
		this.extendedKeyUsage = extendedKeyUsage;
		this.crlDistributionPoint = crlDistributionPoint;
		this.authorityInfoAccess = authorityInfoAccess;
		this.qualifiedCertificateStatements = qualifiedCertificateStatements;
		this.basicConstraints = basicConstraints;
	}

	public AuthorityKeyIdentifier getAuthorityKeyIdentifier() {
		return authorityKeyIdentifier;
	}

	public void setAuthorityKeyIdentifier(AuthorityKeyIdentifier authorityKeyIdentifier) {
		this.authorityKeyIdentifier = authorityKeyIdentifier;
	}

	public SubjectKeyIdentifier getSubjectKeyIdentifier() {
		return subjectKeyIdentifier;
	}

	public void setSubjectKeyIdentifier(SubjectKeyIdentifier subjectKeyIdentifier) {
		this.subjectKeyIdentifier = subjectKeyIdentifier;
	}

	public KeyUsage getKeyUsage() {
		return keyUsage;
	}

	public void setKeyUsage(KeyUsage keyUsage) {
		this.keyUsage = keyUsage;
	}

	public CertificatePolicies getCertificatePolicies() {
		return certificatePolicies;
	}

	public void setCertificatePolicies(CertificatePolicies certificatePolicies) {
		this.certificatePolicies = certificatePolicies;
	}

	public SubjectAlternativeNames getSubjectAlternativeNames() {
		return subjectAlternativeNames;
	}

	public void setSubjectAlternativeNames(SubjectAlternativeNames subjectAlternativeNames) {
		this.subjectAlternativeNames = subjectAlternativeNames;
	}

	public ExtendedKeyUsage getExtendedKeyUsage() {
		return extendedKeyUsage;
	}

	public void setExtendedKeyUsage(ExtendedKeyUsage extendedKeyUsage) {
		this.extendedKeyUsage = extendedKeyUsage;
	}

	public CrlDistributionPoint getCrlDistributionPoint() {
		return crlDistributionPoint;
	}

	public void setCrlDistributionPoint(CrlDistributionPoint crlDistributionPoint) {
		this.crlDistributionPoint = crlDistributionPoint;
	}

	public AuthorityInfoAccess getAuthorityInfoAccess() {
		return authorityInfoAccess;
	}

	public void setAuthorityInfoAccess(AuthorityInfoAccess authorityInfoAccess) {
		this.authorityInfoAccess = authorityInfoAccess;
	}

	public QualifiedCertificateStatements getQualifiedCertificateStatements() {
		return qualifiedCertificateStatements;
	}

	public void setQualifiedCertificateStatements(QualifiedCertificateStatements qualifiedCertificateStatements) {
		this.qualifiedCertificateStatements = qualifiedCertificateStatements;
	}

	public BasicConstraints getBasicConstraints() {
		return basicConstraints;
	}

	public void setBasicConstraints(BasicConstraints basicConstraints) {
		this.basicConstraints = basicConstraints;
	}
	
}
