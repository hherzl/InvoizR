package sv.mh.fe.models.minec;

public class SubjectPublicKeyInfo {

	private AlgorithmIdenitifier algorithmIdenitifier;
	
	private byte[] subjectPublicKey;

	public SubjectPublicKeyInfo() {
		super();
		this.algorithmIdenitifier = new AlgorithmIdenitifier();
		this.subjectPublicKey = null;
	}

	public SubjectPublicKeyInfo(byte[] subjectPublicKey) {
		super();
		this.algorithmIdenitifier = new AlgorithmIdenitifier();
		this.subjectPublicKey = subjectPublicKey;
	}	
	
	public SubjectPublicKeyInfo(AlgorithmIdenitifier algorithmIdenitifier, byte[] subjectPublicKey) {
		super();
		this.algorithmIdenitifier = algorithmIdenitifier;
		this.subjectPublicKey = subjectPublicKey;
	}

	public AlgorithmIdenitifier getAlgorithmIdenitifier() {
		return algorithmIdenitifier;
	}

	public void setAlgorithmIdenitifier(AlgorithmIdenitifier algorithmIdenitifier) {
		this.algorithmIdenitifier = algorithmIdenitifier;
	}

	public byte[] getSubjectPublicKey() {
		return subjectPublicKey;
	}

	public void setSubjectPublicKey(byte[] subjectPublicKey) {
		this.subjectPublicKey = subjectPublicKey;
	}
		
}
