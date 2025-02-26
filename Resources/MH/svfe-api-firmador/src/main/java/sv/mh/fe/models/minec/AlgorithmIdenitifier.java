package sv.mh.fe.models.minec;

public class AlgorithmIdenitifier {
	
	private String algorithm;
	
	private String parameters;
	
	public AlgorithmIdenitifier() {
		super();
		this.algorithm = "RSA";
		this.parameters = null;
	}
	
	public AlgorithmIdenitifier(String algorithm, String parameters) {
		super();
		this.algorithm = algorithm;
		this.parameters = parameters;
	}

	public String getAlgorithm() {
		return algorithm;
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
