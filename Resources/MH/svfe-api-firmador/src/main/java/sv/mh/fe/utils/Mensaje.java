package sv.mh.fe.utils;

import java.io.IOException;

import org.json.JSONObject;
import org.springframework.stereotype.Service;

import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import sv.mh.fe.constantes.Errores;

@Service
public class Mensaje {

	public ResponseBody ok(Object body) {
		ResponseBody responseBody = new ResponseBody();
		responseBody.setStatus(ResponseBody.status_ok);
		responseBody.setBody(body);
		return responseBody;
	}
	
	public ResponseBody ok(JSONObject body) throws IOException {		
		ResponseBody responseBody = new ResponseBody();
		responseBody.setStatus(ResponseBody.status_ok);
		String str = body.toString();
		ObjectMapper mapper = new ObjectMapper();
		JsonFactory factory = mapper.getFactory();
		JsonParser parser = factory.createParser(str);
		JsonNode objNode = mapper.readTree(parser);
		responseBody.setBody(objNode);
		return responseBody;
	}	
	
	public ResponseBody error(String codigo, String mensaje) {
		ResponseBody responseBody = new ResponseBody();
		BodyMensaje body = new BodyMensaje(codigo, mensaje);
		responseBody.setStatus(ResponseBody.status_error);
		responseBody.setBody(body);
		return responseBody;
	}
	
	public ResponseBody error(String codigo, Object mensaje) {
		ResponseBody responseBody = new ResponseBody();
		BodyMensaje body = new BodyMensaje(codigo, mensaje);
		responseBody.setStatus(ResponseBody.status_error);
		responseBody.setBody(body);
		return responseBody;
	}	
	
	public ResponseBody error(Object body) {
		ResponseBody responseBody = new ResponseBody();
		responseBody.setStatus(ResponseBody.status_error);
		responseBody.setBody(body);
		return responseBody;
	}
	
	public ResponseBody error(JSONObject body) {
		ResponseBody responseBody = new ResponseBody();
		responseBody.setStatus(ResponseBody.status_error);
		responseBody.setBody(body);
		return responseBody;
	}
	
	public ResponseBody error(Errores error) {
		ResponseBody responseBody = new ResponseBody();
		BodyMensaje body = new BodyMensaje(error.getCode(), error.getText());
		responseBody.setStatus(ResponseBody.status_error);
		responseBody.setBody(body);
		return responseBody;
	}	
	
}
