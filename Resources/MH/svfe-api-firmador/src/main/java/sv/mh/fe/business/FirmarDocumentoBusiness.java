package sv.mh.fe.business;

import java.nio.file.Path;
import java.security.PrivateKey;
import org.jose4j.jws.AlgorithmIdentifiers;
import org.jose4j.jws.JsonWebSignature;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import sv.mh.fe.models.CertificadoMH;
import sv.mh.fe.security.KeyGenerator;
import sv.mh.fe.utils.FileUtils;

@Service
public class FirmarDocumentoBusiness {
	
	final static Logger logger = LoggerFactory.getLogger(FirmarDocumentoBusiness.class);
	
	@Autowired
	private FileUtils fileUtils;
	
	@Autowired
	private KeyGenerator keyGenerator;
		
	/**
	 * Método para crear un JSON Web Signing (JWS).
	 * @param certificado
	 * @param ruta
	 * @throws Exception
	 */
	public void firmarJSON(CertificadoMH certificado, Path ruta) throws Exception {		
		String contenido = fileUtils.LeerArchivo(ruta);		
		JsonWebSignature jws = new JsonWebSignature();		
		jws.setPayload(contenido);	
		jws.setAlgorithmHeaderValue(AlgorithmIdentifiers.RSA_USING_SHA512);				
		PrivateKey key =  keyGenerator.ByteToPrivateKey(certificado.getPrivateKey().getEncodied());		
		jws.setKey(key);
		fileUtils.crearArchivo(ruta.toString(), jws.getCompactSerialization());
	}	
	
	/**
	 * Método para crear un JSON Web Signing (JWS).
	 * @param certificado
	 * @param contenido, DTE que se quiere firmar
	 * @throws Exception
	 */
	public String firmarJSON(CertificadoMH certificado, String contenido) throws Exception {
		JsonWebSignature jws = new JsonWebSignature();		
		jws.setPayload(contenido);	
		jws.setAlgorithmHeaderValue(AlgorithmIdentifiers.RSA_USING_SHA512);	
		PrivateKey key =  keyGenerator.ByteToPrivateKey(certificado.getPrivateKey().getEncodied());		
		jws.setKey(key);
		return jws.getCompactSerialization();
	}			
}
