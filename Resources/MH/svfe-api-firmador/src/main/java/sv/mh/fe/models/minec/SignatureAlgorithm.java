package sv.mh.fe.models.minec;

public class SignatureAlgorithm {
	
	private String algorithm;
	
	private String parameters;

	public String getAlgorithm() {
		return algorithm;
	}

	public SignatureAlgorithm() {
		super();
		this.algorithm = "Sha256WithRSAEncryption";
		this.parameters = null;
	}	
	
	public SignatureAlgorithm(String algorithm, String parameters) {
		super();
		this.algorithm = algorithm;
		this.parameters = parameters;
	}

	public void setAlgorithm(String algorithm) {
		this.algorithm = algorithm;
	}

	public String getParameters() {
		return parameters;
	}

	public void setParameters(String parameters) {
		this.parameters = parameters;
	}
		
}
