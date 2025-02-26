package sv.mh.fe.models;

import java.util.Arrays;

import sv.mh.fe.constantes.TipoLlave;

public class Llave {
	
	private TipoLlave keyType;
	
	private String algorithm;
	
	private byte[]	encodied;
	
	private String format;
	
	private String clave;

	public TipoLlave getKeyType() {
		return keyType;
	}
	public void setKeyType(TipoLlave keyType) {
		this.keyType = keyType;
	}

	public String getAlgorithm() {
		return algorithm;
	}
	public void setAlgorithm(String algorithm) {
		this.algorithm = algorithm;
	}

	public byte[] getEncodied() {
		return encodied;
	}

	public void setEncodied(byte[] encodied) {
		this.encodied = encodied;
	}

	public String getFormat() {
		return format;
	}

	public void setFormat(String format) {
		this.format = format;
	}

	public String getClave() {
		return clave;
	}
	public void setClave(String clave) {
		this.clave = clave;
	}
	
	@Override
	public String toString() {
		return "Key [keyType=" + keyType + ", algorithm=" + algorithm + ", encodied=" + Arrays.toString(encodied)
				+ ", format=" + format + "]";
	}
	
}
