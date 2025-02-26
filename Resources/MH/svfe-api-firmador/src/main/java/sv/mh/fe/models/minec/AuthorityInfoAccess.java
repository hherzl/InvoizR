package sv.mh.fe.models.minec;

import java.util.ArrayList;
import java.util.List;

public class AuthorityInfoAccess {

	private List<AccessDescription> accessDescription;

	public AuthorityInfoAccess() {
		super();
		this.accessDescription = new ArrayList<AccessDescription>();		
	}
	
	public AuthorityInfoAccess(List<AccessDescription> accessDescription) {
		super();
		this.accessDescription = accessDescription;
	}
	
	public void addAccessDescription(AccessDescription accessDescription) {
		this.accessDescription.add(accessDescription);
	}

	public List<AccessDescription> getAccessDescription() {
		return accessDescription;
	}

	public void setAccessDescription(List<AccessDescription> accessDescription) {
		this.accessDescription = accessDescription;
	}
	
}
