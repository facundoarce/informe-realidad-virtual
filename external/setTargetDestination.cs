private void SetTargetDestination (int index) {
  targetDestination = destinations[ index ];

  arrowCollider.SetActive( true );
  arrowCollider.transform.position = player.position;
  pinCollider.SetActive( true );
  pinCollider.transform.position = targetDestination.position;
}
