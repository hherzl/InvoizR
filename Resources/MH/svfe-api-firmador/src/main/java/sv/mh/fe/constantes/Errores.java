package sv.mh.fe.constantes;

public enum Errores {
	
	COD_801_CERT_ERROR_NO_ENCOTRADO("801","No existe certificado activo"),
	COD_802_NO_VALIDO("802","No valido"),
	COD_803_ERROR_LLAVE_PRUBLICA("803","No existe llave publica para este nit"),
	COD_804_ERROR_NO_CATALOGADO("804","Error no catalogado"),
	COD_805_ERROR_CERTIFCADO_DUPLICADO("805","Ya existe una certificado activo"),
	COD_806_ERROR_GENERACION_CERTIFICADO("806","Generación de certificados satisfactoria"),
	COD_807_ERROR_DESCARGAR_ARCHIVO("807","Error en la descarga de archivo"),
	COD_808_ERROR_SUBUR_ARCHIVO("808","Error en al subir el archivo"),
	COD_809_DATOS_REQUERIDOS("809","Son datos requeridos"),
	COD_810_CONVERTIR_JSON_A_STRING("810","Problemas al convertir Json a String"),
	COD_811_CONVERTIR_STRING_A_JSON("811","Problemas al convertir String a Json"),	
	COD_812_NO_FILE("812","No se encontro el archivo");	
		
	private final String text;
	private final String code;

	public String getText() {
		return text;
	}

	public String getCode() {
		return code;
	}

	private Errores(String codigo,String texto) {
		this.code = codigo;
		this.text = texto;
	}
	
	public static class errores{
		public static final String COD_801_CERT_ERROR_NO_ENCOTRADO = "801"; 
		public static final String COD_802_NO_VALIDO = "802";
		public static final String COD_803_ERROR_LLAVE_PRUBLICA = "803";
		public static final String COD_804_ERROR_NO_CATALOGADO = "804";
		public static final String COD_805_ERROR_CERTIFCADO_DUPLICADO = "805";
		public static final String COD_806_ERROR_GENERACION_CERTIFICADO = "806";
		public static final String COD_807_ERROR_DESCARGAR_ARCHIVO = "807";
		public static final String COD_808_ERROR_SUBUR_ARCHIVO = "808";	
		public static final String COD_809_DATOS_REQUERIDOS = "809";
		public static final String COD_810_CONVERTIR_JSON_A_STRING = "810";
		public static final String COD_811_CONVERTIR_STRING_A_JSON = "811";
		public static final String COD_812_NO_FILE = "812";
	}
}