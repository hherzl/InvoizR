package sv.mh.fe.controller;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import javax.validation.Valid;

import org.json.JSONObject;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.ObjectWriter;
import sv.mh.fe.business.CertificadoBusiness;
import sv.mh.fe.business.FirmarDocumentoBusiness;
import sv.mh.fe.constantes.Errores;
import sv.mh.fe.constantes.Errores.errores;
import sv.mh.fe.filter.FirmarDocumentoFilter;
import sv.mh.fe.models.CertificadoMH;
import sv.mh.fe.validations.FirmarDocumentoValidations;

@RestController
@RequestMapping("/firmardocumento")
@CrossOrigin(origins = "*", maxAge = 3600)
public class FirmarDocumentoController extends Controller {

	final static Logger logger = LoggerFactory.getLogger(FirmarDocumentoController.class);
	
	@Autowired
	private CertificadoBusiness certificadoBusiness;
	
	@Autowired
	private FirmarDocumentoBusiness business;
	
	@Autowired
	private FirmarDocumentoValidations validation;	 
	
	/**
	 * 
	 * @param filter
	 * @return
	 * @throws Exception
	 */
	@RequestMapping(value = "/", method = RequestMethod.POST)
	public ResponseEntity<?> firmar(@Valid @RequestBody FirmarDocumentoFilter filter) {
		CertificadoMH certificado = null;		
		try {			
			validation.v5validar(filter);
			if(validation.isValido()) {
				certificado = certificadoBusiness.recuperarCertifiado(filter);
				if(certificado != null) {		        
					ObjectWriter ow = new ObjectMapper().writer().withDefaultPrettyPrinter();
			        String dteString;
					try {
						dteString = ow.writeValueAsString(filter.getDteJson());
						JSONObject dteObject = new JSONObject(dteString);					
						if(dteObject != null) {
							logger.info("dteObject != null");
							String firma;
							firma = business.firmarJSON(certificado, dteString);
							return ResponseEntity.ok(mensaje.ok(firma));
						}										
					} catch (JsonProcessingException e) {
						logger.info(errores.COD_810_CONVERTIR_JSON_A_STRING, e.getMessage());
						return ResponseEntity.ok(mensaje.error(Errores.COD_810_CONVERTIR_JSON_A_STRING));
					} catch (Exception e) {
						logger.info(errores.COD_811_CONVERTIR_STRING_A_JSON, e.getMessage());
						return ResponseEntity.ok(mensaje.error(Errores.COD_811_CONVERTIR_STRING_A_JSON));
					}	
				}else {
					return ResponseEntity.ok(mensaje.error(Errores.COD_803_ERROR_LLAVE_PRUBLICA));
				}
			}else {
				return ResponseEntity.ok(mensaje.error(errores.COD_809_DATOS_REQUERIDOS,validation.getRequeridos()));
			}			
		} catch (IOException e1) {
			logger.error(e1.getMessage());
			return ResponseEntity.ok(mensaje.error(errores.COD_812_NO_FILE, e1.getMessage()));
		} catch (NoSuchAlgorithmException e1) {
			logger.error(e1.getMessage());
			return ResponseEntity.ok(mensaje.error(errores.COD_804_ERROR_NO_CATALOGADO, e1.getMessage()));			
		}
		return ResponseEntity.ok(mensaje.error(Errores.COD_804_ERROR_NO_CATALOGADO));
	}

	@GetMapping("/status")
	public String getStatus(){
		return "Application is running...!!";
	}
}
