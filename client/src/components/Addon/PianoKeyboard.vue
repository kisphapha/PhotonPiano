<template>
  <div>
    <!-- <button @click="playSomething">Hello world</button> -->
    <div id="piano">
      <div v-for="key in this.piano_key" :key="key.id" :class="key.css_string" @click="playAudio(key.id)">
        {{ isShowKey && key.isWhite ? key.note : "" }}
      </div>
    </div>
    <div class="flex justify-center gap-5">
      <input type="checkbox" v-model="isShowKey" value="Bike" />Show notes
    </div>
  </div>
</template>

<script>

export default {
  name: "PianoKeyboard",
  mounted() {
    document.addEventListener('keydown', this.handleKeyDown)
    document.addEventListener('keyup', this.handleKeyUp)
    this.generateKeys("La",21)
  },
  beforeUnmount() {
    // Remove event listener
    document.removeEventListener('keydown', this.handleKeyDown);
    document.removeEventListener('keyup', this.handleKeyUp);
  },
  methods: {
    generateKeys(startNote, numberOfKeys) {
      const startIndex = this.note.findIndex(n => n.name == startNote);
      this.piano_key = [];
      for (let i = 0; i < numberOfKeys; i++) {
        const currentIndex = (startIndex + i) % this.note.length;
        const currentNote = this.note[currentIndex].name;
        const isWhite = this.note[currentIndex].isWhite;

        // Create a new key object and push it into the piano_key array
        this.piano_key.push({
          isWhite: isWhite,
          id: i,
          note: currentNote,
          css_string: "keyid-" + i + " border border-black border-b-8 border-b-slate-300 active:border-b rounded-b " + (isWhite ? "white-key" : "black-key") + " key-" + (i < this.pressableKey.length ? this.pressableKey[i].key : '')
        });
      }
    },
    playAudio(keyId) {
      const audioIndex = keyId; // Assuming the audioList and piano_key have the same index for corresponding keys
      const audio = new Audio(this.audioList[audioIndex]);
      audio.play();
    },
    handleKeyDown(event) {
      // Get the key code of the pressed key
      var keyCode = event.keyCode;
      console.log(keyCode)
      var codeIndex = this.pressableKey.find(k => k.key.charCodeAt(0) == keyCode)
      if (codeIndex) {
        var keys = document.getElementsByClassName("key-" + codeIndex.key)
        keys[0].classList.add("border-b")
        var audioId = keys[0].className.substring(6, 8);
        const audio = new Audio(this.audioList[parseInt(audioId)]);
        audio.play();
      }
    },
    handleKeyUp(event) {
      // Get the key code of the pressed key
      var keyCode = event.keyCode;
      var codeIndex = this.pressableKey.find(k => k.key.charCodeAt(0) == keyCode)
      if (codeIndex) {
        var keys = document.getElementsByClassName("key-" + codeIndex.key)
        keys[0].classList.remove("border-b")
      }
    }

  },
  data() {
    return {
      isShowKey: false,
      audioList: [
        "src/assets/Piano Audio/A0.mp3",
        "src/assets/Piano Audio/As0.mp3",
        "src/assets/Piano Audio/B0.mp3",
        "src/assets/Piano Audio/C1.mp3",
        "src/assets/Piano Audio/Cs1.mp3",
        "src/assets/Piano Audio/D1.mp3",
        "src/assets/Piano Audio/Ds1.mp3",
        "src/assets/Piano Audio/E1.mp3",
        "src/assets/Piano Audio/F1.mp3",
        "src/assets/Piano Audio/Fs1.mp3",
        "src/assets/Piano Audio/G1.mp3",
        "src/assets/Piano Audio/Gs1.mp3",
        "src/assets/Piano Audio/A1.mp3",
        "src/assets/Piano Audio/As1.mp3",
        "src/assets/Piano Audio/B1.mp3",
        "src/assets/Piano Audio/C2.mp3",
        "src/assets/Piano Audio/Cs2.mp3",
        "src/assets/Piano Audio/D2.mp3",
        "src/assets/Piano Audio/Ds2.mp3",
        "src/assets/Piano Audio/E2.mp3",
        "src/assets/Piano Audio/F2.mp3",
      ],
      note: [
        {
          name: "Do",
          isWhite: true
        },
        {
          name: "Do#",
          isWhite: false
        },
        {
          name: "Re",
          isWhite: true
        },
        {
          name: "Re#",
          isWhite: false
        },
        {
          name: "Mi",
          isWhite: true
        },
        {
          name: "Fa",
          isWhite: true
        },
        {
          name: "Fa#",
          isWhite: false
        },
        {
          name: "Sol",
          isWhite: true
        },
        {
          name: "Sol#",
          isWhite: false
        },
        {
          name: "La",
          isWhite: true
        },
        {
          name: "La#",
          isWhite: false
        },
        {
          name: "Si",
          isWhite: true
        },
      ],
      pressableKey: [
        { key: 'A'},
        { key: 'W'},
        { key: 'S'},
        { key: 'D'},
        { key: 'E'},
        { key: 'F'},
        { key: 'R'},
        { key: 'G'},
        { key: 'H'},
        { key: 'Y'},
        { key: 'J'},
        { key: 'U'},
        { key: 'K'},
        { key: 'I'},
        { key: 'L'},
        { key: 'º' },
        { key: 'P'},
        { key: 'Þ' },
        { key: 'Û' },
        { key: 'N' },
        { key: 'M' },
      ],
      piano_key: [
        {
          isWhite: true,
          id: 0,
          note: "Do",
          css_string: "",
        }
      ]
    };
  }
};
</script>
<style>
#piano {
  display: flex;
  justify-content: center;
  /* Align keys horizontally */
  margin-top: 50px;
  /* Adjust the margin as needed */
  margin-bottom: 50px;
}

.white-key {
  width: 40px;
  height: 200px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: end;
}

.black-key {
  width: 25px;
  height: 150px;
  background-color: black;
  margin-left: -12.5px;
  margin-right: -12.5px;
  z-index: 1;
  position: relative;
}

/* .white-key:hover {
  background-color: pink; 
} */
.black-key:hover {
  background-color: purple;
  /* Change the background color when clicked */
}
</style>
