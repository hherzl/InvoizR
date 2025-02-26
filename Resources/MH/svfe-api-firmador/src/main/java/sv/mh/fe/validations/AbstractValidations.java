package sv.mh.fe.validations;

import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


public abstract class AbstractValidations {

	final static Logger logger = LoggerFactory.getLogger(AbstractValidations.class); 
	
	public static String REQ_NIT = "NIT es requerido";
	public static String REQ_DATOS = "Objeto se recibió vacío";
	public static String REQ_NIT_FORMATO = "Formato de NIT no valido - (00000000000000)  ";
	public static String REQ_JWS = "JSON WEB Signing es requerido";
	public static String REQ_NOMBRE_DOCUMENTO = "El nombre del docuemnto es requerido";
	public static String REQ_NOMBRE_FIRMA = "El nombre del firma es requerido";
	public static String REQ_JSON_DTE = "JsonDTE es requerido";
	public static String REQ_CLAVE_PRIVADA = "Clave privada es requerida";
	public static String REQ_CONFIRMACION_PRI = "Clave priva y confirmación no son iguales";
	public static String REQ_CLAVE_PUBLICA = "Clave publica es requerida";
	public static String REQ_CONFIRMACION_PUB = "Clave publica y confirmación no son iguales";
	public static String REQ_COMPACT_SERIALIZATION = "La Serialización Compacta es requerida";
	
	public static String REQ_SUBJECT_CONTRY_NAME = "Nombre del país es requerido";
	public static String REQ_SUBJECT_ORGANI_NAME = "Nombre de la organización es requerido";
	public static String REQ_SUBJECT_ORGANI_UNIT = "Nombre de la unidad es requerido";
	public static String REQ_SUBJECT_ORGANI_IDEN = "Organization Identifier es requerido";
	public static String REQ_SUBJECT_SURNAME = "Apellido del firmante es un campo requerido";
	public static String REQ_SUBJECT_GIVENNAME = "Nombre del firmante es un campo requerido";
	public static String REQ_SUBJECT_COMMON_NAME = "CommonName es un campo requerido";
	public static String REQ_SUBJECT_DESCRIPCION = "NRC es un campo requerido";
	public static String REQ_SUBJECT_EMAIL_ORGANI = "Correo de organización es un campo requerido";
	
	protected List<String> requeridos = null;
	protected String validarNIT = null;
	
	protected Boolean valido = null;	
	
	public List<String> ValidarNIT(String nit){
		List<String> requeridos = new ArrayList<>();				
		if(nit==null) {
			requeridos.add(REQ_NIT);
		}else if(!nit.matches("\\d{14}")){
			requeridos.add(REQ_NIT_FORMATO);
		}
		return requeridos;
	}
		
	public boolean isValido() {
		valido = false;		
		if(requeridos.size() == 0)
			valido = true;
		else {
			logger.info("isValido(): "+requeridos.size());
		}
		return valido;
	}	
	
	public List<String> getRequeridos() {
		return requeridos;
	}

	public void setRequeridos(List<String> requeridos) {
		this.requeridos = requeridos;
	}	

}
