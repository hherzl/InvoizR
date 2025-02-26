package sv.mh.fe.models.minec;

import java.util.ArrayList;
import java.util.List;

public class CrlDistributionPoint {

	private List<String> distributionPoint;
	
	public CrlDistributionPoint() {
		super();
		this.distributionPoint = new ArrayList<>();
	}	
	
	public CrlDistributionPoint(List<String> distributionPoint) {
		super();
		this.distributionPoint = distributionPoint;
	}

	public void add(String distributionPoint) {
		this.distributionPoint.add(distributionPoint);
	}
	
	public List<String> getDistributionPoint() {
		return distributionPoint;
	}

	public void setDistributionPoint(List<String> distributionPoint) {
		this.distributionPoint = distributionPoint;
	}
		
}
