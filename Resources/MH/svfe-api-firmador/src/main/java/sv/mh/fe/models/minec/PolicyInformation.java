package sv.mh.fe.models.minec;

public class PolicyInformation {
	
	private String policyIdentifier;
	
	private PolicyQualifiers policyQualifiers;	

	public PolicyInformation() {
		super();
		this.policyIdentifier = "";
		this.policyQualifiers = new PolicyQualifiers();
	}
	
	public PolicyInformation(String policyIdentifier, PolicyQualifiers policyQualifiers) {
		super();
		this.policyIdentifier = policyIdentifier;
		this.policyQualifiers = policyQualifiers;
	}

	public String getPolicyIdentifier() {
		return policyIdentifier;
	}

	public void setPolicyIdentifier(String policyIdentifier) {
		this.policyIdentifier = policyIdentifier;
	}

	public PolicyQualifiers getPolicyQualifiers() {
		return policyQualifiers;
	}

	public void setPolicyQualifiers(PolicyQualifiers policyQualifiers) {
		this.policyQualifiers = policyQualifiers;
	}		

}
