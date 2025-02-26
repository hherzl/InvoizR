package sv.mh.fe.models.minec;

public class Issuer {

	private String countryName;
	
	private String localilyName;
	
	private String organizationalUnit;
	
	private String organizationalName;
	
	private String commonName;
	
	private String organizationIdentifier;
	
	public Issuer() {
		super();
		this.countryName = "SV";
		this.localilyName = "SAN SALVADOR";
		this.organizationalUnit = "MINISTERIO DE HACIENDA";
		this.organizationalName = "DIRECCIÓN GENERAL DE IMPUESTOS INTERNOS";
		this.commonName = "UNIDAD COORDINADORA DEL PROGAMA FORTALECIMIENTO A LA ADMINISTRACIÓN TRIBUTARÍA";
		this.organizationIdentifier = "VATSV-0614-010111-003-2";
	}

	public Issuer(String countryName, String localilyName, String organizationalUnit, String organizationalName,
			String commonName, String organizationIdentifier) {
		super();
		this.countryName = countryName;
		this.localilyName = localilyName;
		this.organizationalUnit = organizationalUnit;
		this.organizationalName = organizationalName;
		this.commonName = commonName;
		this.organizationIdentifier = organizationIdentifier;
	}

	public String getCountryName() {
		return countryName;
	}

	public void setCountryName(String countryName) {
		this.countryName = countryName;
	}

	public String getLocalilyName() {
		return localilyName;
	}

	public void setLocalilyName(String localilyName) {
		this.localilyName = localilyName;
	}

	public String getOrganizationalUnit() {
		return organizationalUnit;
	}

	public void setOrganizationalUnit(String organizationalUnit) {
		this.organizationalUnit = organizationalUnit;
	}

	public String getOrganizationalName() {
		return organizationalName;
	}

	public void setOrganizationalName(String organizationalName) {
		this.organizationalName = organizationalName;
	}

	public String getCommonName() {
		return commonName;
	}

	public void setCommonName(String commonName) {
		this.commonName = commonName;
	}

	public String getOrganizationIdentifier() {
		return organizationIdentifier;
	}

	public void setOrganizationIdentifier(String organizationIdentifier) {
		this.organizationIdentifier = organizationIdentifier;
	}	
	
}
