<template>
    <div id="piano">
        <div id="piano-a0" class="white-key"></div>
        <div id="piano-a0#" class="black-key"></div>
        <div id="piano-b0" class="white-key"></div>
        <div id="piano-c1" class="white-key"></div>
        <div id="piano-c1#" class="black-key"></div>
        <div id="piano-d1" class="white-key"></div>
        <div id="piano-d1#" class="black-key"></div>
        <div id="piano-e1" class="white-key"></div>
        <div id="piano-f1" class="white-key"></div>
        <div id="piano-f1#" class="black-key"></div>
        <div id="piano-g1" class="white-key"></div>
        <div id="piano-g1#" class="black-key"></div>
        <div id="piano-a1" class="white-key"></div>
        <div id="piano-a1#" class="black-key"></div>
        <div id="piano-b1" class="white-key"></div>
        <div id="piano-c2" class="white-key"></div>
        <div id="piano-c2#" class="black-key"></div>
        <div id="piano-d2" class="white-key"></div>
        <div id="piano-d2#" class="black-key"></div>
        <div id="piano-e2" class="white-key"></div>
    <!-- Add more keys as needed -->
  </div>
</template>

<script>
    var keyAudioMap = {
    'piano-a0': 'assets/Piano Audio/A0.mp3',
    'piano-a0#': 'assets/Piano Audio/As0.mp3',
    'piano-b0': 'assets/Piano Audio/B0.mp3',
    'piano-c1': 'assets/Piano Audio/C1.mp3',
    'piano-c1#': 'assets/Piano Audio/Cs1.mp3',
    'piano-d1': 'assets/Piano Audio/D1.mp3',
    'piano-d1#': 'assets/Piano Audio/Ds1.mp3',
    'piano-e1': 'assets/Piano Audio/E1.mp3',
    'piano-f1': 'assets/Piano Audio/F1.mp3',
    'piano-f1#': 'assets/Piano Audio/Fs1.mp3',
    'piano-g1': 'assets/Piano Audio/G1.mp3',
    'piano-g1#': 'assets/Piano Audio/Gs1.mp3',
    'piano-a1': 'assets/Piano Audio/A1.mp3',
    'piano-a1#': 'assets/Piano Audio/As1.mp3',
    'piano-b1': 'assets/Piano Audio/B1.mp3',
    'piano-c2': 'assets/Piano Audio/C2.mp3',
    'piano-c2#': 'assets/Piano Audio/Cs2.mp3',
    'piano-d2': 'assets/Piano Audio/D2.mp3',
    'piano-d2#': 'assets/Piano Audio/Ds2.mp3',
    'piano-e2': 'assets/Piano Audio/E2.mp3'
    };

    document.getElementById('piano').addEventListener('click', function(event) {
    var clickedKey = event.target;
    if (clickedKey.classList.contains('white-key') || clickedKey.classList.contains('black-key')) {
        var keyId = clickedKey.id;
        if (keyAudioMap.hasOwnProperty(keyId)) {
        var audioFile = keyAudioMap[keyId];
        var audio = new Audio(audioFile);
        audio.play();
        }

        clickedKey.classList.add('clicked');
        setTimeout(function() {
        clickedKey.classList.remove('clicked');
        }, 300);
    }
    });
    export default {
        name : "PianoKeyboard",
        mounted(){
            var keyAudioMap = {
                'piano-a0': 'assets/Piano Audio/A0.mp3',
                'piano-a0#': 'assets/Piano Audio/As0.mp3',
                'piano-b0': 'assets/Piano Audio/B0.mp3',
                'piano-c1': 'assets/Piano Audio/C1.mp3',
                'piano-c1#': 'assets/Piano Audio/Cs1.mp3',
                'piano-d1': 'assets/Piano Audio/D1.mp3',
                'piano-d1#': 'assets/Piano Audio/Ds1.mp3',
                'piano-e1': 'assets/Piano Audio/E1.mp3',
                'piano-f1': 'assets/Piano Audio/F1.mp3',
                'piano-f1#': 'assets/Piano Audio/Fs1.mp3',
                'piano-g1': 'assets/Piano Audio/G1.mp3',
                'piano-g1#': 'assets/Piano Audio/Gs1.mp3',
                'piano-a1': 'assets/Piano Audio/A1.mp3',
                'piano-a1#': 'assets/Piano Audio/As1.mp3',
                'piano-b1': 'assets/Piano Audio/B1.mp3',
                'piano-c2': 'assets/Piano Audio/C2.mp3',
                'piano-c2#': 'assets/Piano Audio/Cs2.mp3',
                'piano-d2': 'assets/Piano Audio/D2.mp3',
                'piano-d2#': 'assets/Piano Audio/Ds2.mp3',
                'piano-e2': 'assets/Piano Audio/E2.mp3'
            };
            
            document.querySelectorAll('.white-key, .black-key').forEach(function(key) {
                key.addEventListener('mousedown', function() {
                    var keyId = this.id;
                    if (keyAudioMap.hasOwnProperty(keyId)) {
                        var audioFile = keyAudioMap[keyId];
                        var audio = new Audio(audioFile);
                        audio.play();
                    }
                });

                
            });
        }
    }
</script>
<style>
#piano {
  display: flex;
  justify-content: center; /* Align keys horizontally */
  margin-top: 50px; /* Adjust the margin as needed */
}

.white-key {
  width: 40px;
  height: 200px;
  background-color: white;
  border: 1px solid black;
}

.black-key {
  width: 25px;
  height: 150px;
  background-color: black;
  margin-left: -12.5px;
  margin-right: -12.5px;
  z-index: 1;
  position: relative; /* Add position relative to overlap with white keys */
}
.white-key:hover {
  background-color: pink; /* Change the background color when clicked */
}
.black-key:hover {
  background-color: purple; /* Change the background color when clicked */
}
</style>