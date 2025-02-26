package sv.mh.fe.models.minec;

import java.util.LinkedList;
import java.util.List;

public class CertificatePolicies {
	
	private List<PolicyInformation> policyInformations;
	
	public CertificatePolicies() {
		super();
		this.policyInformations = new LinkedList<>();
	}
	
	public CertificatePolicies(List<PolicyInformation> policyInformations) {
		super();
		this.policyInformations = policyInformations;
	}
	
	public void add(PolicyInformation policyInformation) {
		this.policyInformations.add(policyInformation);
	}

	public List<PolicyInformation> getPolicyInformations() {
		return policyInformations;
	}

	public void setPolicyInformations(List<PolicyInformation> policyInformations) {
		this.policyInformations = policyInformations;
	}
		
}
