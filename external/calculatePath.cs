void Start() {
  path = new NavMeshPath();
  line = GetComponent<LineRenderer>(); // get LineRenderer component on current object
  line.enabled = false;
  // ....
}

void Update() {
  if (targetDestination != null) {
    NavMesh.CalculatePath(player.position, targetDestination.position, NavMesh.AllAreas, path);

    line.positionCount = path.corners.Length;
    line.SetPositions( path.corners );
    line.enabled = true;
  }
}
