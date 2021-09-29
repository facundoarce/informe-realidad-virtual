// Move the player indicator to the calibration point
private void Relocate(string text) {
  text = text.Trim(); //remove spaces

  //Find the correct calibration point scanned and move the player to that position
  foreach (Transform child in calibrationPoints.transform) {
    if ( child.name == text ) {
      isRelocating = true;

      if (!initialized) {
        mapUI.SetActive(true);  // show map UI
        initialized = true;
        surface.BuildNavMesh();
      }

      player.GetComponent<NavMeshAgent>().Warp( child.position );
      deltaOrientation = Mathf.DeltaAngle( Frame.Pose.rotation.eulerAngles.y, child.localRotation.eulerAngles.y );
      isRelocating = false;
    }
  }
}
