<template>
  <div>
    <div v-if='this.enroll_status == "None"'>
      <div class="w-full flex justify-center items-center flex-col">
        <div class="text-4xl  m-8 font-bold">WELCOME TO THE PARADISE OF PIANO ART</div>
      </div>
      <PianoKeyboard />
      <div class="text-2xl m-8 font-bold text-center drop-shadow-sm	">JOIN US! BECOME A PART OF PHOTONPIANO</div>
      <div class="text-xl m-4 text-center drop-shadow-sm italic">MAKE YOURSELF BECOME A GRANDMASTER OF PIANO</div>
      <div class="text-xl m-4 text-center drop-shadow-sm italic">AND HAVE A CHANCE TO INTERACT WITH THE DIAMOND GOD!
      </div>
      <div class="flex flex-col justify-center items-center">
        <button v-if='student_status == "None"' @click="triggerOpenPopup"
          class="m-4 p-8 rounded-lg border-2 border-solid border-lime-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
          Register Now
        </button>
        <button v-if='student_status == "None"' @click="triggerOpenPopup"
          class="m-4 p-8 rounded-lg border-2 border-solid border-blue-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
          Keep Track of Your Studying
        </button>
        <button v-if='student_status == "Unregistered"' @click='setEnrollingStatus("Enrolling")'
          class="m-4 p-8 rounded-lg border-2 border-solid border-orange-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
          Enroll Now
        </button>
        <RouterLink to="/about"
          class="m-4 p-8 rounded-lg border-2 border-solid border-gray-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
          About Us
        </RouterLink>
      </div>
    </div>
    <div v-else-if='this.enroll_status == "Enrolling"'>
      <EnrollForm />
    </div>
    <div v-else-if='this.enroll_status == "Applied"'>
      <div class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
        <div class="bg-gray-300 rounded-lg p-8">
          <div class="text-5xl font-bold text-green-600">SUCCESSFULLY!</div>
          <div class="text-3xl font-bold">Your request is pending</div>
          <div class="text-2xl">2024-06-04 12:00:00</div>
          <div class="mt-4">
            Name : <span class="ml-4 font-bold"></span><br>
            Email : <span class="ml-4 font-bold"></span><br>
            Phone : <span class="ml-4 font-bold"></span><br>
            Gender : <span class="ml-4 font-bold"></span><br>
            Date Of Birth : <span class="ml-4 font-bold"></span><br>
          </div>
          <div>
            Short Description : <span class="ml-4 italic">ABXdxyz</span><br>

            <div class="flex justify-center mt-4">
              <button @click="handleCancelRegistration(true)"
                class="bg-red-500 pl-12 pr-12 pt-4 pb-4 rounded-lg font-bold hover:bg-red-700 text-white">Withdraw
                your registration</button>
            </div>
          </div>
        </div>
      </div>
      <div class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
        <div class="text-xl text-center">Let's play some music while you're waiting</div>
      </div>
      <PianoKeyboard />
    </div>
    <div v-else-if='this.enroll_status == "Accepted"'>
      <div class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
        <div class="text-5xl font-bold text-green-600">CONGRATULATIONS!!!!!</div>
        <div class="text-3xl font-bold mt-2">Your registration has been accepted!</div>
        <div class="text-2xl mt-2">2024-08-04 12:00:00</div>
        <div v-if="this.entrance_test_detail">
          <div class="text-xl italic mt-2">
            We kindly inform you about the location and time for you entrance test!
          </div>
          <div class="mt-8 mb-8 w-full bg-orange-100 p-8 rounded-xl font-bold">
            <ul>
              <li>Location : Mozart 1</li>
              <li>Time : 7:00 AM - 8:30 AM (Shift 1)</li>
            </ul>
          </div>
        </div>
        <div v-else>
          <div class="text-xl italic mt-2">Make sure to frequently visit this website or check your mail to get
            the ealiest update about the place and time for you entrance test! Thanks 😊😊
          </div>
        </div>
        <div class="mt-4">
          Name : <span class="ml-4 font-bold"></span><br>
          Email : <span class="ml-4 font-bold"></span><br>
          Phone : <span class="ml-4 font-bold"></span><br>
          Gender : <span class="ml-4 font-bold"></span><br>
          Date Of Birth : <span class="ml-4 font-bold"></span><br>
        </div>
        <div>
          Short Description : <span class="ml-4 italic">ABXdxyz</span><br>
        </div>
      </div>
      <div class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
        <div class="text-xl text-center">Let's celebrating!!</div>
      </div>
      <PianoKeyboard />
    </div>
    <div v-else-if='this.enroll_status == "Examined"'>
      <div class="flex p-8 flex-col items-center">
        <img src="../assets/smile.png" class="w-32" />
        <div class="text-3xl font-bold text-green-600">
          Congratulations on finishing your entrance test!
        </div>
        <div v-if="this.entrance_test_result">
          <div class="text-xl mt-4">
            We kindly inform you about your entrance test result as follows :
            <div class="mt-8">
              <table id="entrance-inform-table">
                <tr>
                  <th>Criteria 1</th>
                  <th>Criteria 2</th>
                  <th>Criteria 3</th>
                  <th>Criteria 4</th>
                </tr>
                <tr>
                  <td>8.0</td>
                  <td>7.5</td>
                  <td>6.0</td>
                  <td>5.0</td>
                </tr>
              </table>        
            </div>
            <div class="mt-2 font-bold w-1/2 mr-auto ml-auto">
              <div>
                <span class="font-bold">Final score :</span>
                <span class="font-bold text-3xl ml-4 text-red-400">6.4</span>
              </div>
              <div>
                <span class="font-bold">Ranked :</span>
                <span class="font-bold ml-4 text-blue-400">Level 4 (Advanced)</span>
              </div>           
            </div>
          </div>        
        </div>
        <div v-else>
          <div class="text-xl mt-4">
            Your entrance test result should be annouce within a week!
          </div>
        </div>
        <div v-if="this.entrance_test_result">
          <div class="text-xl mt-4">
            Based on your result, you will be placed in a suitable class to slowly level your skills. 
            The class placement result will be informed as soon as possible.
          </div>
          <div class="italic">
            Don't be sad if your scores are low, the entrance test is not meant to be get the highest score
            as possible. It's just a tool for us to evaluate your current skills and give you the best services possible
          </div>
          <div class="text-xl font-bold mt-4">
            What should I do now?
          </div>
          <div class="text-xl mt-4">
            Regularly visit this website, or your email. Your class information and schedule will be updated
            and you can start your journey at PhotonPiano!
          </div>
        </div>
        <div class="text-4xl font-bold mt-4">
          STAY TUNED!
        </div>
        <div class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
          <div class="text-xl text-center">You tried hard! It's time for relaxing</div>
        </div>
        <PianoKeyboard /> 
      </div>

    </div>
    <div v-else-if='this.enroll_status == "InClass"'>
      <ClassDetailOfStudent />
    </div>
  </div>

</template>

<script>
import PianoKeyboard from '../components/PianoKeyboard.vue';
import EnrollForm from '../components/EnrollForm.vue';
import ClassDetailOfStudent from '../components/ClassDetailOfStudent.vue';

export default {
  name: "HomePage",
  inject: ["eventBus"],
  data() {
    return {
      user: null,
      student_status: "None",
      enroll_status: "InClass",
      entrance_test_detail: null,
      entrance_test_result: "yes",
    }
  },
  components: { PianoKeyboard, EnrollForm, ClassDetailOfStudent },
  methods: {
    triggerOpenPopup() {
      this.eventBus.emit("open-login-popup")
    },
    async refresh() {
      const userPromise = new Promise((resolve) => {
        this.eventBus.emit("get-user", resolve);

      });
      const user = await userPromise;
      this.user = user;
      this.student_status = this.user?.students[0]?.status ?? "None"
    },
    setEnrollingStatus(status) {
      this.enroll_status = status
    },
    handleCancelRegistration(confirmation) {
      if (confirmation) {
        this.eventBus.emit("open-confirmation-popup", {
          message: "Are you sure about this?",
          callback: "handle-withdraw-registration-home-page"
        })
      } else {
        //Call delete endpoint here
      }
    }
  },
  mounted() {
    this.eventBus.on("update-home-page", async () => {
      this.refresh()
    })
    this.eventBus.on("update-home-page-enrolling-status", async (status) => {
      this.setEnrollingStatus(status)
    })
    if (localStorage.token) {
      this.refresh()
    }
    this.eventBus.on("handle-withdraw-registration-home-page", async () => {
      this.handleCancelRegistration(false)
    })
  }
}
</script>

<style>
#entrance-inform-table tr td,
#entrance-inform-table th {
    padding: 0.5rem 2rem 0.5rem 2rem;
    border: solid 1px #a9a9bd;
    text-align : center
}
#entrance-inform-table th {
    background-color: #eeeeff;
}
</style>