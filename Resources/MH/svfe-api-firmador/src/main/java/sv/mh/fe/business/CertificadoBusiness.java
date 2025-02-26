package sv.mh.fe.business;

import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.security.NoSuchAlgorithmException;

import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.fasterxml.jackson.dataformat.xml.XmlMapper;
import sv.mh.fe.constantes.Constantes;
import sv.mh.fe.filter.FirmarDocumentoFilter;
import sv.mh.fe.models.CertificadoMH;
import sv.mh.fe.security.Cryptographic;
import sv.mh.fe.utils.FileUtils;

@Service
public class CertificadoBusiness {
	
	@Autowired
	private Cryptographic cryptographic;
	
	@Autowired
	private FileUtils fileUtilis;
	
	private static Logger logger = LoggerFactory.getLogger(CertificadoBusiness.class);		
	
	public CertificadoMH recuperarCertifiado(FirmarDocumentoFilter filter) throws IOException, NoSuchAlgorithmException {		
		XmlMapper xmlMapper = new XmlMapper();
		JavaTimeModule module = new JavaTimeModule();
		xmlMapper.registerModule(module);

		CertificadoMH certificado = null;
		String crypto = cryptographic.encrypt(filter.getPasswordPri(), Cryptographic.SHA512);
		
		Path path = Paths.get(Constantes.DIRECTORY_UPLOADS,filter.getNit()+".crt");
		String contenido = fileUtilis.LeerArchivo(path);
		certificado = xmlMapper.readValue(contenido, CertificadoMH.class);
		
		if(certificado.getPrivateKey().getClave().equals(crypto)){
			return certificado;			
		}
		logger.info("Password no valido: "+certificado.getNit());
		return null;
	}
}
