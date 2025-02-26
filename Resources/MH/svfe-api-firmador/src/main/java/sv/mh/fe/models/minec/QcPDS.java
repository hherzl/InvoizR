package sv.mh.fe.models.minec;

public class QcPDS {

	private String pdsLocation;
	
	private String url;
	
	private String language;

	public QcPDS() {
		super();
		this.pdsLocation = null;
		this.url = "https://www2.mh.gob.sv/pds";
		this.language = "ES";		
	}

	public QcPDS(String pdsLocation, String url, String language) {
		super();
		this.pdsLocation = pdsLocation;
		this.url = url;
		this.language = language;
	}

	public String getPdsLocation() {
		return pdsLocation;
	}

	public void setPdsLocation(String pdsLocation) {
		this.pdsLocation = pdsLocation;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public String getLanguage() {
		return language;
	}

	public void setLanguage(String language) {
		this.language = language;
	}
		
}
