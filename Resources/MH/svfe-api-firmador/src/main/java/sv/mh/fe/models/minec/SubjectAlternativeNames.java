package sv.mh.fe.models.minec;

public class SubjectAlternativeNames {

	private String rfc822Name;
	
	public SubjectAlternativeNames() {
		super();
		this.rfc822Name = "";
	}

	public SubjectAlternativeNames(String rfc822Name) {
		super();
		this.rfc822Name = rfc822Name;
	}

	public String getRfc822Name() {
		return rfc822Name;
	}

	public void setRfc822Name(String rfc822Name) {
		this.rfc822Name = rfc822Name;
	}
		
}
