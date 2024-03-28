<template>
  <div class="w-full" id="loader">
    <div class="text-4xl p-8 font-bold flex justify-center">WELCOME TO THE PARADISE OF PIANO ART</div>
    <div class="flex gap-8 bg-slate-50">
      <img src="../../assets/piano_example.png" class="w-96 h-96"/>
      <div>
        <div class="text-2xl p-8 font-bold">WHAT IS THE PIANO?</div>
        <p class="p-4">
          The piano is not just an instrument of mechanics; it is an instrument of emotions. 
          With its wide range and dynamic capabilities, it can evoke an extensive palette of moods and feelings.
          From delicate and introspective melodies that stir the soul to thunderous chords that command 
          attention, the piano has an unparalleled ability to convey the depths of human expression.
        </p>
        <p class="p-4">
          As a musician's fingers dance across the keys, the piano yields a rich and nuanced sound. Each note, 
          carefully crafted and uniquely resonant, blends together to form harmonies that can transport listeners
          to another world. Whether played solo or accompanied by other instruments, the piano holds its ground
          as a versatile companion in any musical ensemble.
        </p>
      </div>
      
    </div>
    <div class="flex gap-8 bg-gray-100">
      <div>
        <div class="text-2xl p-8 font-bold flex justify-end">ABOUT THE FOUNDER</div>
        <p class="p-4">
          Kim Cương, also known as Diamond God, is one of Primary Members of Kisphophu King. She have a lot of
          feat in protecting this Universe. However, this site don't discuss that problem here. What you should
          concern is Kim Cương is a master, or a god of Piano. She has won a bunch of honor rewards only in the
          aspect of Piano. Here are some outstanding awards :
          <ul class="ml-16">
            <li >🥇1608 Diamond Medals Universal Music Battle</li>
            <li class="mt-2">🏆829 Champion Cup of Annual Intergalactic Piano Competition</li>
            <li class="mt-2">🎖88 awards for The Fastest Pianist</li>
            <li class="mt-2">🎖12 awards for The Most Emotional Musician</li>
            <li class="mt-2">🎖26 awards for The Most Inspiring Teacher</li>
            <li class="mt-2">✨3 Nominations for The God of Piano</li>
          </ul>
        </p>
        
        <p class="p-4 italic">
          "I'm not scared of the one who can play 10000 pieces of music, but the who one play a piece 10000 times" - Kim Cương
        </p>
      </div>
      <img src="../../assets/kclo.png" class="w-96 h-96"/>    
    </div>
    <div class="flex gap-8 bg-slate-50">
      <div class="w-1/2">
        <img src="../../assets/logo.png" class="w-96 h-96 p-4"/> 
        <img src="../../assets/center1.png" class="w-96 h-96 p-4" /> 
      </div>
      <div>     
        <div class="text-2xl p-8 font-bold">ABOUT PHOTON PIANO CENTER</div>
        <!-- <img src="../../assets/diamonds_strip5.png" class="w-96 p-4"/>  -->
        <p class="p-4">
          Due to the high demand of being instructed by the G.O.A.T of Piano, Kim Cương has open this center to statisfy
          everyone'wishes. Allow us to introduce your learning process if you ever want to study at this center          
        </p>
        <p class="pt-2">
          <div class="pl-4 italic">Registration Process :</div>
          <ul class="list-decimal italic pl-12">
            <li>Register for the entrance test</li>
            <li>Your registration has been approved and information about location and time will be given</li>
            <li>Do your entrance test. It would be a free-style performance on the piano</li>
            <li>Your entrance result along with class placement and your schedule will be annonced shortly after</li>
            <li>Let's start your journey at PhotonPiano</li>
          </ul>    
          <div class="pl-4 pt-2 italic font-bold">* Due to large amount of registration each year, we only take earliest registrations to meet the target. Don't worry if your registration don't get accepted this time. Don't withdraw and wait for us next year, you'll bee given priority for class placement.</div> 
        </p>
        <p class="pt-8">
          <div class="pl-4 italic">Studying Process :</div>
          <ul class="list-decimal italic pl-12">
            <li>Attending lessons following your schedule</li>
            <li>Complete all on going assessments, this will be include some small tests and assignments to test your skill midway</li>
            <li>Last lessons of the class will be your final exam which technically similar to the entrance test</li>
            <li>Your final score of all assessments and result will be annouced shortly after your class ended</li>
            <li>Based on the result, you can level up and decide to continue in order to be place in a higher level class next school year or just stop. And if the result is failed, you are unable to level up and have to retake another class of same level if you want to continue next year</li>
          </ul>     
          <div class="pl-4 pt-2 italic font-bold">* As you learning and become a part of PhotonPiano, you will need to pay tuition monthly</div>
        </p>
      </div>        
    </div>
  </div>
  
</template>

<script>
export default {
    name : "About"
}
</script>

<style>
#loader {
    animation: fadeInAnimation 1s ease-in forwards;
    opacity: 0;
    transform: translateY(-20px);
  }

  @keyframes fadeInAnimation {
    0% {
      opacity: 0;
      transform: translateY(-20px);
    }
    100% {
      opacity: 1;
      transform: translateY(0);
    }
  }
</style>