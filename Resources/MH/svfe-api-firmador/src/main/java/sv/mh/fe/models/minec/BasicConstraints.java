package sv.mh.fe.models.minec;

public class BasicConstraints {

	private Boolean ca;
	
	public BasicConstraints() {
		super();
		this.ca = Boolean.FALSE;
	}

	public BasicConstraints(Boolean ca) {
		super();
		this.ca = ca;
	}

	public Boolean getCa() {
		return ca;
	}

	public void setCa(Boolean ca) {
		this.ca = ca;
	}
		
}
