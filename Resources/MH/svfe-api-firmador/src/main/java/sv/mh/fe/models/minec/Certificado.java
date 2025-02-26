package sv.mh.fe.models.minec;

public class Certificado {	
	
	private BasicEstructure basicEstructure;
	
	private Extensions extensions;

	public Certificado() {
		super();
	}

	public BasicEstructure getBasicEstructure() {
		return basicEstructure;
	}

	public void setBasicEstructure(BasicEstructure basicEstructure) {
		this.basicEstructure = basicEstructure;
	}

	public Extensions getExtensions() {
		return extensions;
	}

	public void setExtensions(Extensions extensions) {
		this.extensions = extensions;
	}	
	
}
