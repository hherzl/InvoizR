package sv.mh.fe.models.minec;

public class PolicyQualifiers {

	private String cpsUri;
	
	private String userNotice;

	public PolicyQualifiers(String cpsUri, String userNotice) {
		super();
		this.cpsUri = cpsUri;
		this.userNotice = userNotice;
	}
	
	public PolicyQualifiers() {
		super();
		this.cpsUri = "www2.mh.gob.sv/dpc";
		this.userNotice = "CERTIFICADO PARA FACTURACIÓN ELECTRÓNICA";
	}
	

	public String getCpsUri() {
		return cpsUri;
	}

	public void setCpsUri(String cpsUri) {
		this.cpsUri = cpsUri;
	}

	public String getUserNotice() {
		return userNotice;
	}

	public void setUserNotice(String userNotice) {
		this.userNotice = userNotice;
	}
		
}
