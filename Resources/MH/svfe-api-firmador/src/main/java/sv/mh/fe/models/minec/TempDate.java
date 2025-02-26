package sv.mh.fe.models.minec;

public class TempDate {
	
	private String nano;
	private String epochSecond;
	
	public TempDate(String nano, String epochSecond) {
		super();
		this.nano = nano;
		this.epochSecond = epochSecond;
	}

	public TempDate() {
		super();
	}

	public String getNano() {
		return nano;
	}

	public void setNano(String nano) {
		this.nano = nano;
	}

	public String getEpochSecond() {
		return epochSecond;
	}

	public void setEpochSecond(String epochSecond) {
		this.epochSecond = epochSecond;
	}
	
	
}
