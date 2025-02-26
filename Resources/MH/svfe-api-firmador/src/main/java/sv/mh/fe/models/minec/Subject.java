package sv.mh.fe.models.minec;

public class Subject {

	private String countryName;
	
	private String organizationName;
	
	private String organizationUnitName;
	
	private String organizationIdentifier;
	
	private String surname;
	
	private String givenName;
	
	private String commonName;
	
	private String description;

	public Subject(String countryName, String organizationName, String organizationUnitName,
			String organizationIdentifier, String surname, String givenName, String commonName, String description) {
		super();
		this.countryName = countryName;
		this.organizationName = organizationName;
		this.organizationUnitName = organizationUnitName;
		this.organizationIdentifier = organizationIdentifier;
		this.surname = surname;
		this.givenName = givenName;
		this.commonName = commonName;
		this.description = description;
	}
	
	public Subject() {
		super();
		this.countryName = "EL SALVADOR";
	}	

	public String getCountryName() {
		return countryName;
	}

	public void setCountryName(String countryName) {
		this.countryName = countryName;
	}

	public String getOrganizationName() {
		return organizationName;
	}

	public void setOrganizationName(String organizationName) {
		this.organizationName = organizationName;
	}

	public String getOrganizationUnitName() {
		return organizationUnitName;
	}

	public void setOrganizationUnitName(String organizationUnitName) {
		this.organizationUnitName = organizationUnitName;
	}

	public String getOrganizationIdentifier() {
		return organizationIdentifier;
	}

	public void setOrganizationIdentifier(String organizationIdentifier) {
		this.organizationIdentifier = organizationIdentifier;
	}

	public String getSurname() {
		return surname;
	}

	public void setSurname(String surname) {
		this.surname = surname;
	}

	public String getGivenName() {
		return givenName;
	}

	public void setGivenName(String givenName) {
		this.givenName = givenName;
	}

	public String getCommonName() {
		return commonName;
	}

	public void setCommonName(String commonName) {
		this.commonName = commonName;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}
		
}
