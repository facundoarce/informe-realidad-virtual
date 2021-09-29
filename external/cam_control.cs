void Update() {
  // mouse of fingertouch moves camera
  if ( Input.GetMouseButtonDown( 0 ) ) touchStart = Input.mousePosition;

  // Detect double tap
  for ( var i = 0; i < Input.touchCount; ++i ) {
    if ( Input.GetTouch( i ).phase == TouchPhase.Began ) {
      if ( Input.GetTouch( i ).tapCount == 2 ) StartCoroutine( Pause() );
    }
  }

  // If there are two touches on the device...
  if ( Input.touchCount == 2 ) {
    // Store both touches.
    Touch touchZero = Input.GetTouch( 0 );
    Touch touchOne = Input.GetTouch( 1 );
    // Find the position in the previous frame of each touch.
    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
    // Find the magnitude of the vector (the distance) between the touches in each frame.
    float prevTouchDeltaMag = ( touchZeroPrevPos - touchOnePrevPos ).magnitude;
    float touchDeltaMag = ( touchZero.position - touchOne.position ).magnitude;
    // Find the difference in the distances between each frame.
    float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

    Zoom( deltaMagnitudeDiff * ZoomSpeed );
  }
  // Single press pan with camera, only if not scrolling in dropdown
  else if ( Input.GetMouseButton( 0 ) ) {
    bool selectingDest = false;
    foreach ( Transform child in dropdown.transform ) {
      if ( child.name.Equals( "Dropdown List" ) ) { // true when dropdown is open
        selectingDest = true;
        break;
      }
    }
    if ( !selectingDest ) {
      Vector3 direction = ( touchStart - Input.mousePosition ) / ( 0.1f * Screen.width );
      direction = new Vector3( direction.x, 0, direction.y );
      cam.transform.localPosition -= direction;
      touchStart = Input.mousePosition;
    }
  }
}

// Handle double tap
IEnumerator Pause() {
  yield return new WaitForSeconds( 0.1f );
  cam.transform.localPosition = new Vector3( 0, 20, 0 );
  cam.fieldOfView = 15.0f;
}

// Zoom with camera
private void Zoom( float incr )
{
  cam.fieldOfView = Mathf.Clamp( cam.fieldOfView - incr, zoomOutMin, zoomOutMax );
}
