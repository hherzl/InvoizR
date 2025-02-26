package sv.mh.fe.models.minec;

import java.time.Duration;
import java.time.Instant;

public class Validity {

	private Instant notBefore;

	private Instant notAfter;

	public Validity(Instant notBefore, Instant notAfter) {
		super();
		this.notBefore = notBefore;
		this.notAfter = notAfter;
	}

	public Validity() {
		super();
		this.notBefore = Instant.now();
		this.notAfter = notBefore.plus(Duration.ofDays(1825));
	}

	public Instant getNotBefore() {
		return notBefore;
	}

	public void setNotBefore(Instant notBefore) {
		this.notBefore = notBefore;
	}

	public Instant getNotAfter() {
		return notAfter;
	}

	public void setNotAfter(Instant notAfter) {
		this.notAfter = notAfter;
	}

}
