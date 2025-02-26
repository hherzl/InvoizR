package sv.mh.fe.security;

import java.security.KeyFactory;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

@Service
public class KeyGenerator {
	
	final static Logger logger = LoggerFactory.getLogger(KeyGenerator.class);
	
	public static String RSA = "RSA"; 
	public static String SHA1WITHRSA = "SHA1withRSA";
	public static String SHA256WITHRSA = "SHA256withRSA";
	
	public static Integer keysize = 2048;
	
	public PrivateKey ByteToPrivateKey(byte[] bytes,String algorithm) {
		PKCS8EncodedKeySpec encode = new PKCS8EncodedKeySpec(bytes);		
	    KeyFactory keyFactory;
	    PrivateKey privatekey = null;	    
		try {
			keyFactory = KeyFactory.getInstance(algorithm);
			privatekey = keyFactory.generatePrivate(encode);				
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvalidKeySpecException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return privatekey;
	}
	
	public PrivateKey ByteToPrivateKey(byte[] bytes) {
		return ByteToPrivateKey(bytes, RSA);
	}
	
	public PublicKey ByteToPublicKey(byte[] bytes, String algorithm) {
		X509EncodedKeySpec encode = new X509EncodedKeySpec(bytes);
		KeyFactory keyFactory;
		PublicKey publicKey = null;
		try {
			keyFactory = KeyFactory.getInstance(algorithm);
			publicKey = keyFactory.generatePublic(encode);
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvalidKeySpecException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return publicKey;
	}
	
	public PublicKey ByteToPublicKey(byte[] bytes) {
		return ByteToPublicKey(bytes, RSA);
	}
	
}
