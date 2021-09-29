// Current position in real-world
Vector3 currPos = Frame.Pose.position;
float currHeading = qrScanner.deltaOrientation + Frame.Pose.rotation.eulerAngles.y;

// Difference between positions
Vector3 deltaPos = -(currPos - prevPos);
float deltaHeading = - Mathf.DeltaAngle( prevHeading, currHeading );

// Update previous position
prevPos = currPos;
prevHeading = currHeading;

// Apply transform to Player (ignoring differences in height)
float x = deltaPos.x * Mathf.Cos( qrScanner.deltaOrientation * Mathf.Deg2Rad ) + deltaPos.z * Mathf.Sin( qrScanner.deltaOrientation * Mathf.Deg2Rad );
float z = deltaPos.z * Mathf.Cos( qrScanner.deltaOrientation * Mathf.Deg2Rad ) - deltaPos.x * Mathf.Sin( qrScanner.deltaOrientation * Mathf.Deg2Rad );
player.transform.Translate( x, 0.0f, z );

// Aply rotation to Arrow indicator and auxiliar Cube
arrow.transform.localRotation = Quaternion.Euler( new Vector3( 0, currHeading, 0 ) );
auxiliar.transform.RotateAround( player.transform.position, new Vector3( 0, 0, 1 ), deltaHeading );
