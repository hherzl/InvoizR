package sv.mh.fe.models.minec;

import java.util.ArrayList;
import java.util.List;

public class AccessDescription {

	private String accessMethod;
	
	private List<String> accessLocation;

	public AccessDescription() {
		super();
		this.accessMethod = "";
		this.accessLocation = new ArrayList<String>();
	}
	
	public AccessDescription(List<String> accessLocation) {
		super();
		this.accessLocation = accessLocation;
	}

	public AccessDescription(String accessMethod, List<String> accessLocation) {
		super();
		this.accessMethod = accessMethod;
		this.accessLocation = accessLocation;
	}
	
	public void addAccessLocation(String accessLocation) {
		this.accessLocation.add(accessLocation);
	}

	public String getAccessMethod() {
		return accessMethod;
	}

	public void setAccessMethod(String accessMethod) {
		this.accessMethod = accessMethod;
	}

	public List<String> getAccessLocation() {
		return accessLocation;
	}

	public void setAccessLocation(List<String> accessLocation) {
		this.accessLocation = accessLocation;
	}
		
}
