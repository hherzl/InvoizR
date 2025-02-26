package sv.mh.fe.models.minec;

public class QualifiedCertificateStatements {

	private String qcCompliance;
	
	private String qcEuRetentionPeriod;
	
	private QcPDS qcPDS;
	
	private String qcType;

	public QualifiedCertificateStatements(QcPDS qcPDS) {
		super();
		this.qcCompliance = "";
		this.qcEuRetentionPeriod = "10";
		this.qcPDS = qcPDS;
		this.qcType = "id-etsi-qct-esign";
	}

	public QualifiedCertificateStatements() {
		super();
	}

	public QualifiedCertificateStatements(String qcCompliance, String qcEuRetentionPeriod, QcPDS qcPDS, String qcType) {
		super();
		this.qcCompliance = qcCompliance;
		this.qcEuRetentionPeriod = qcEuRetentionPeriod;
		this.qcPDS = qcPDS;
		this.qcType = qcType;
	}

	public String getQcCompliance() {
		return qcCompliance;
	}

	public void setQcCompliance(String qcCompliance) {
		this.qcCompliance = qcCompliance;
	}

	public String getQcEuRetentionPeriod() {
		return qcEuRetentionPeriod;
	}

	public void setQcEuRetentionPeriod(String qcEuRetentionPeriod) {
		this.qcEuRetentionPeriod = qcEuRetentionPeriod;
	}

	public QcPDS getQcPDS() {
		return qcPDS;
	}

	public void setQcPDS(QcPDS qcPDS) {
		this.qcPDS = qcPDS;
	}

	public String getQcType() {
		return qcType;
	}

	public void setQcType(String qcType) {
		this.qcType = qcType;
	}
		
}
