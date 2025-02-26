package sv.mh.fe.models;

import java.io.Serializable;

public abstract class AbsDocumentos implements Serializable {

	public String _id;

	public String get_id() {
		return _id;
	}

	public void set_id(String _id) {
		this._id = _id;
	}
}
