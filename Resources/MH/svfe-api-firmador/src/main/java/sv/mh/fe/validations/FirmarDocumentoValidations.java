package sv.mh.fe.validations;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import sv.mh.fe.filter.FirmarDocumentoFilter;


@Service
public class FirmarDocumentoValidations extends AbstractValidations {
	
	public static Logger logger = LoggerFactory.getLogger(FirmarDocumentoValidations.class);
	
	public List<String> v2validar(FirmarDocumentoFilter filter) {		
		this.requeridos = ValidarNIT(filter.getNit());
		if(filter.getCompactSerialization() == null || filter.getCompactSerialization().length()<=0) {
			this.requeridos.add(REQ_JWS);
		}		
		return this.requeridos;
	}
	
	public List<String> v1validar(FirmarDocumentoFilter filter) {		
		this.requeridos = ValidarNIT(filter.getNit());
		if(filter.getNombreDocumento() == null || filter.getNombreDocumento().length()<=0) {
			this.requeridos.add(REQ_NOMBRE_DOCUMENTO);
		}
		if(filter.getNombreFirma() == null || filter.getNombreFirma().length()<=0) {
			this.requeridos.add(REQ_NOMBRE_FIRMA);
		}
		
		logger.info("requeridos: "+requeridos);
		return requeridos;
	}
	
	public List<String> v3validar(FirmarDocumentoFilter filter) {		
		this.requeridos = ValidarNIT(filter.getNit());
		if(this.validarNIT!=null) {
			this.requeridos.add(this.validarNIT);
		}
//		if(filter.getDteJson() == null || filter.getDteJson().length()<=0) {
//			this.requeridos.add(REQ_JSON_DTE);
//		}
		if(filter.getPasswordPri() == null || filter.getPasswordPri().length()<=0) {
			this.requeridos.add(REQ_CLAVE_PRIVADA);
		}
		return requeridos;
	}
	
	public List<String> v5validar(FirmarDocumentoFilter filter) {		
		this.requeridos = ValidarNIT(filter.getNit());
		if(filter.getDteJson() == null) {
			this.requeridos.add(REQ_JSON_DTE);
		}
		if(filter.getPasswordPri() == null || filter.getPasswordPri().length()<=0) {
			this.requeridos.add(REQ_CLAVE_PRIVADA);
		}
		return requeridos;
	}		
	
	public String getValidarNIT() {
		return this.validarNIT;
	}
	
	
}
