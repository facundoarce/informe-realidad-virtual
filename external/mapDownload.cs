IEnumerator getCalibrationPoints() {
  // Start communication with server and wait until response
  yield return new WaitForSeconds( 3.0f );
  do {
    send( "Hello" );
    yield return new WaitForSeconds( 2.0f );
  } while ( !_processText.text.Contains( "Done" ) );
  // Clean existing calibration points
  List<GameObject> children = new List<GameObject>();
  foreach ( Transform child in parent ) children.Add( child.gameObject );
  children.ForEach( child => Destroy( child ) );
  // Message parsing
  string pattern1 = @"\[([^\[\]]+)\]";  // Separate calibration points from rest of message
  string pattern2 = @"[^,;\[\]\n\r]+";  // Split each calibration point into name and coordinates
  foreach ( Match m1 in Regex.Matches( _processText.text, pattern1 ) ) {
    Match mName = Regex.Match( m1.Groups[ 0 ].Value, pattern2 );
    Match mX = mName.NextMatch();
    Match mZ = mX.NextMatch();
    Match mRY = mZ.NextMatch();
    // Check if point is repeated
    bool isNameRepeated = false;
    foreach ( Transform child in parent ) {
      isNameRepeated |= child.gameObject.name == mName.Groups[ 0 ].Value;
    }
    if ( !isNameRepeated ) {
      // Add calibration points
      GameObject calibPoint = new GameObject( mName.Groups[ 0 ].Value );
      calibPoint.transform.parent = parent;
      calibPoint.transform.name = mName.Groups[ 0 ].Value;
      calibPoint.transform.localPosition = new Vector3( float.Parse( mX.Groups[ 0 ].Value ), 0.0f, float.Parse( mZ.Groups[ 0 ].Value ) );
      calibPoint.transform.localEulerAngles = new Vector3( 0.0f, float.Parse( mRY.Groups[ 0 ].Value ), 0.0f );
      // Add labels in minimap
      GameObject label = GameObject.Instantiate( labelPrefab, labelParent );
      label.transform.localPosition += calibPoint.transform.localPosition;
      label.GetComponent<TextMeshPro>().SetText( calibPoint.transform.name );
  }
  pathFinder.Clear(); // populate dropdown with calibration points
  message.text = "Please scan QR code to start";
}
