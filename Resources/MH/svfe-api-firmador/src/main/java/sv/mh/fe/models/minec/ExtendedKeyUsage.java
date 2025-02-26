package sv.mh.fe.models.minec;

public class ExtendedKeyUsage {

	private String clientAuth;
	
	private String emailProtection;

	public ExtendedKeyUsage() {
		super();
		this.clientAuth = "";
		this.emailProtection = "";
	}	
	
	public ExtendedKeyUsage(String clientAuth, String emailProtection) {
		super();
		this.clientAuth = clientAuth;
		this.emailProtection = emailProtection;
	}

	public String getClientAuth() {
		return clientAuth;
	}

	public void setClientAuth(String clientAuth) {
		this.clientAuth = clientAuth;
	}

	public String getEmailProtection() {
		return emailProtection;
	}

	public void setEmailProtection(String emailProtection) {
		this.emailProtection = emailProtection;
	}
	
}
