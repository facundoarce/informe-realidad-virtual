public Sound[] sounds;
private bool isPlaying = false;

void Awake() {
  // Add AudioSource object to the Sound objects to be able to play them
  foreach (Sound s in sounds) {
    s.source = gameObject.AddComponent<AudioSource>();
    s.source.clip = s.clip; s.source.volume = s.volume;
    s.source.pitch = s.pitch; s.source.loop = s.loop;
  }
}

public void Play(string sound) {
  Sound s = Array.Find( sounds, item => item.name == sound );
  if ( s == null ) return;
  StartCoroutine( playAndWait( s ) );
}

IEnumerator playAndWait( Sound s ) {
  yield return new WaitUntil( () => !isPlaying );
  isPlaying = true;
  s.source.Play();
  yield return new WaitForSeconds( s.source.clip.length );
  isPlaying = false;
}
