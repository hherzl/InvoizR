package sv.mh.fe.filter;

public class FirmarDocumentoFilter {

	private String passwordPub;
	private String passwordPri;
	private String nit;
	private String nombreDocumento;
	private String nombreFirma;
	private String compactSerialization;
	private Object dteJson;
	private String dte;
	private boolean activo;
	

	public String getPasswordPub() {
		return passwordPub;
	}
	public void setPasswordPub(String passwordPub) {
		this.passwordPub = passwordPub;
	}
	public String getPasswordPri() {
		return passwordPri;
	}
	public void setPasswordPri(String passwordPri) {
		this.passwordPri = passwordPri;
	}
	public String getNit() {
		return nit;
	}
	public void setNit(String nit) {
		this.nit = nit;
	}
	public boolean getActivo() {
		return activo;
	}
	public void setActivo(boolean activo) {
		this.activo = activo;
	}
	public String getNombreDocumento() {
		return nombreDocumento;
	}
	public void setNombreDocumento(String nombreDocumento) {
		this.nombreDocumento = nombreDocumento;
	}
	public String getNombreFirma() {
		return nombreFirma;
	}
	public void setNombreFirma(String nombreFirma) {
		this.nombreFirma = nombreFirma;
	}
	public String getCompactSerialization() {
		return compactSerialization;
	}
	public void setCompactSerialization(String compactSerialization) {
		this.compactSerialization = compactSerialization;
	}
	
	public Object getDteJson() {
		return dteJson;
	}
	public void setDteJson(Object dteJson) {
		this.dteJson = dteJson;
	}

	public String getDte() {
		return dte;
	}
	public void setDte(String dte) {
		this.dte = dte;
	}
	
}
