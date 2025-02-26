package sv.mh.fe.utils;

public class ResponseBody {

	public static String status_ok = "OK";
	public static String status_error = "ERROR";
	
	private String status;	
	
	private Object body;
	
	public ResponseBody() {
		super();
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public Object getBody() {
		return body;
	}

	public void setBody(Object body) {
		this.body = body;
	}
		
}
