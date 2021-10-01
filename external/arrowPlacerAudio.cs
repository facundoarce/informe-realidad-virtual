private float prevAngle = 0.0f;
private bool first = true;

private void OnTriggerEnter( Collider collider ) {
  if ( collider.tag.Equals( "Arrow" ) && !hasEnteredPinCollider ) {
    // ...
    // Play sound indication
    if ( angle < -45 ) {
      if ( prevAngle > -45 || first ) FindObjectOfType<AudioManager>().Play( "izquierda" );
    }
    else if ( angle > 45 ) {
      if ( prevAngle < 45 || first ) FindObjectOfType<AudioManager>().Play( "derecha" );
    }
    else {
      if ( prevAngle < -45 || prevAngle > 45 || first ) FindObjectOfType<AudioManager>().Play( "continue" );
    }
    first = false;
    // Update previous angle
    prevAngle = angle;
  }
  else if ( collider.tag.Equals( "Pin" ) ) {
    // ...
    first = true;
    // Play sound indication
    FindObjectOfType<AudioManager>().Play( "destino" );
  }
}
