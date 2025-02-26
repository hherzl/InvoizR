package sv.mh.fe.models;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import sv.mh.fe.models.minec.Certificado;

import java.time.LocalDateTime;
import java.util.Date;

@JsonIgnoreProperties(ignoreUnknown = true)
public class CertificadoMH extends AbsDocumentos{

	private String nit;
	private Llave publicKey;
	private Llave privateKey;
	private Boolean activo;
	private Certificado certificado;
	
	private String clavePub;
	
	private String clavePri;

	private Boolean verificado;
	@JsonFormat(pattern = "yyyy-MM-dd'Y'HH:mm:ss.SSS'Z'")
	private Date fechaVerificacion;
		
	public String getNit() {
		return nit;
	}
	public void setNit(String nit) {
		this.nit = nit;
	}

	public String getClavePub() {
		return clavePub;
	}
	public void setClavePub(String clavePub) {
		this.clavePub = clavePub;
	}
	public String getClavePri() {
		return clavePri;
	}
	public void setClavePri(String clavePri) {
		this.clavePri = clavePri;
	}
	public Boolean getActivo() {
		return activo;
	}
	public void setActivo(Boolean activo) {
		this.activo = activo;
	}

	public Llave getPublicKey() {
		return publicKey;
	}
	
	public Certificado getCertificado() {
		return certificado;
	}
	public void setCertificado(Certificado certificado) {
		this.certificado = certificado;
	}

	public Boolean getVerificado() {
		return verificado;
	}

	public void setVerificado(Boolean verificado) {
		this.verificado = verificado;
	}

	public Date getFechaVerificacion() {
		return fechaVerificacion;
	}

	public void setFechaVerificacion(Date fechaVerificacion) {
		this.fechaVerificacion = fechaVerificacion;
	}

	@Override
	public String toString() {
		return "Certificado [nit=" + nit + ", publicKey=" + publicKey + ", privateKey=" + privateKey + ", activo="
				+ activo + ", clavePub=" + clavePub + ", clavePri=" + clavePri + "]";
	}
	
	public void setPublicKey(Llave publicKey) {
		this.publicKey = publicKey;
	}
	public Llave getPrivateKey() {
		return privateKey;
	}
	public void setPrivateKey(Llave privateKey) {
		this.privateKey = privateKey;
	}
		
}
