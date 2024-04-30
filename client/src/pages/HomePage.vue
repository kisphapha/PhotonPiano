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
          <div class="text-2xl">{{ student_detail.registrationDate }}</div>
          <div class="mt-4">
            Name : <span class="ml-4 font-bold">{{ user.name }}</span><br>
            Email : <span class="ml-4 font-bold">{{ user.email }}</span><br>
            Phone : <span class="ml-4 font-bold">{{ user.phone }}</span><br>
            Gender : <span class="ml-4 font-bold">{{ user.gender }}</span><br>
            Date Of Birth : <span class="ml-4 font-bold">{{ user.doB }}</span><br>
          </div>
          <div>
            Short Description : <span class="ml-4 italic">{{ user.students[0]?.shortDesc }}</span><br>

            <div class="flex justify-center mt-4">
              <button @click="handleCancelRegistration(true)"
                class="bg-red-500 pl-12 pr-12 pt-4 pb-4 rounded-lg font-bold hover:bg-red-700 text-white">Withdraw your
                registration</button>
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
        <div v-if="entrance_test_result?.entranceTestSlot?.isAnnoucedTime">
          <div class="text-xl italic mt-2">
            We kindly inform you about the location and time for you entrance test!
          </div>
          <div class="mt-8 mb-8 w-full bg-orange-100 p-8 rounded-xl font-bold">
            <ul class="list-disc">
              <li>Location : {{ this.entrance_test_detail.entranceTestSlot.location.name }}</li>
              <li>Shift : {{ this.shifts[this.entrance_test_detail.entranceTestSlot.shift - 1] }}</li>
              <li>Date : {{ this.entrance_test_detail.entranceTestSlot.date }}</li>
            </ul>
          </div>
        </div>
        <div v-else>
          <div class="text-xl italic mt-2">Make sure to frequently visit this website or check your mail to get
            the ealiest update about the place and time for you entrance test! Thanks 😊😊
          </div>
        </div>
        <div class="mt-4">
          Name : <span class="ml-4 font-bold">{{ user.name }}</span><br>
          Email : <span class="ml-4 font-bold">{{ user.email }}</span><br>
          Phone : <span class="ml-4 font-bold">{{ user.phone }}</span><br>
          Gender : <span class="ml-4 font-bold">{{ user.gender }}</span><br>
          Date Of Birth : <span class="ml-4 font-bold">{{ user.doB }}</span><br>
          Registration time : <span class="ml-4 font-bold">{{ student_detail.registrationDate.substring(0, 10) + " " +
      student_detail.registrationDate.substring(11, 19) }}</span><br>
        </div>
        <div>
          Short Description : <span class="ml-4 italic">{{ user.students[0].shortDesc }}</span><br>
        </div>
      </div>
      <div class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
        <div class="text-xl text-center">Let's celebrate!!</div>
      </div>
      <PianoKeyboard />
    </div>
    <div v-else-if='this.enroll_status == "Examined"'>
      <div class="flex p-8 flex-col items-center">
        <img src="../assets/smile.png" class="w-32" />
        <div class="text-3xl font-bold text-green-600">
          Congratulations on finishing your entrance test!
        </div>
        <div v-if="entrance_test_result?.entranceTestSlot?.isAnnoucedScore">
          <div class="text-xl mt-4">
            We kindly inform you about your entrance test result as follows :
            <div class="mt-8">
              <table id="entrance-inform-table">
                <tr>
                  <th v-for="result in this.entrance_test_result.entranceTestResults" :key="result.id">
                    {{ result.criteria.name }} </th>
                </tr>
                <tr>
                  <td v-for="result in this.entrance_test_result.entranceTestResults" :key="result.id">{{ result.score
                    }}
                  </td>
                </tr>
              </table>
            </div>
            <div class="mt-2 font-bold w-1/2 mr-auto ml-auto">
              <div>
                <span class="font-bold">Final score :</span>
                <span class="font-bold text-3xl ml-4 text-red-400">{{ this.entrance_test_result.bandScore }}</span>
              </div>
              <div>
                <span class="font-bold">Ranked :</span>
                <span class="font-bold ml-4 text-blue-400">Level {{ (this.rank.indexOf(this.entrance_test_result.rank) +
      1)
      + " (" + this.entrance_test_result.rank + ")" }}</span>
              </div>
            </div>
          </div>
        </div>
        <div v-else>
          <div class="text-xl mt-4">
            Your entrance test result should be announced within a week!
          </div>
        </div>
        <div v-if="entrance_test_result?.entranceTestSlot?.isAnnoucedScore">
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
      <div class="p-8 flex flex-col items-center">
        <div class="text-3xl font-bold text-blue-600">
          WELCOME TO PHOTONPIANO!
        </div>
        <img src="../assets/diamonds_strip5.png" class="w-96 mt-4 mb-4">
        <div class="text-xl mt-4 text-center">
          We're pleased to inform you that you have been successfully placed in a class.
        </div>
      </div>
      <div class="mt-4 flex flex-col items-center">
        <ul class="list-disc">
          <li>Class name :
            <button class="font-bold text-blue-400" @click="goToClass">
              {{ this.student_detail.currentClass.name }}
            </button>
          </li>
          <li>Level : <span class="font-bold">{{ this.student_detail.currentClass.level }}</span></li>
          <li>Instructor : <span class="font-bold">{{ this.student_detail.currentClass.instructor.user.name }}</span>
          </li>
          <li>Duration : <span class="font-bold">{{ "From " + this.student_detail.currentClass.startDate + " to " +
      this.student_detail.currentClass.endDate }}</span></li>
        </ul>
        <button class="font-bold text-blue-400" @click="goToClass">More information here</button>
      </div>
      <div class="mt-4 italic text-center">
        Our journey is open ahead!
      </div>
      <PianoKeyboard />
    </div>
  </div>

</template>

<script>
import axios from 'axios';
import PianoKeyboard from '../components/Addon/PianoKeyboard.vue';
import EnrollForm from '../components/Common/EnrollForm.vue';


export default {
  name: "HomePage",
  inject: ["eventBus"],
  data() {
    return {
      user: null,
      student_detail: null,
      student_status: "None",
      enroll_status: "None",
      entrance_test_detail: null,
      entrance_test_result: "yes",
      shifts: [
        "7:00 - 8:30",
        "8:45 - 10:15",
        "10:30 - 12:00",
        "12:30 - 14:00",
        "14:15 - 15:45",
        "16:00 - 17:30",
        "18:00 - 19:30",
        "19:45 - 21:15",
      ],
      rank: [
        "Beginner",
        "Novice",
        "Intermediate",
        "Advanced",
        "Virtuoso",
      ]
    }
  },
  components: { PianoKeyboard, EnrollForm },
  methods: {
    triggerOpenPopup() {
      this.eventBus.emit("open-login-popup")
    },
    async refresh() {
      this.setEnrollingStatus("None")
      this.eventBus.emit("update-header");
      const userPromise = new Promise((resolve) => {
        this.eventBus.emit("get-user", resolve);
      });
      const user = await userPromise;
      this.user = user;
      this.student_status = this.user?.students[0]?.status ?? "None"
      if (this.user.students[0]) {
        //Calling student profile endpoint
        const studentDetail = await axios.get(import.meta.env.VITE_API_URL + '/api/Student/' + user.students[0].id)

        if (studentDetail.data) {
          this.student_detail = studentDetail.data
        }
        if (this.student_status == "PendingRegistration") {
          this.setEnrollingStatus("Applied")
        }
        if (this.student_status == "Accepted" || this.student_status == "EntranceTesting") {
          this.setEnrollingStatus("Accepted")
        }
        if (this.student_detail.entranceTests && this.student_detail.entranceTests.length > 0) {
          this.entrance_test_detail = this.student_detail.entranceTests.find(et => et.year == new Date().getFullYear())
          const examDate = this.entrance_test_detail.entranceTestSlot?.date ?? "2999-01-01"
          if (new Date(examDate) < new Date()) {
            this.setEnrollingStatus("Examined")
          }
        }

        //Calling entrance test score endpoint
        const entranceTestScore = await axios.get(import.meta.env.VITE_API_URL + '/api/EntranceTest/' + user.students[0].id + '/entrance-test-score')

        if (entranceTestScore.data) {
          this.entrance_test_result = entranceTestScore.data
        }

        if (this.student_detail.status == "InClass") {
          this.setEnrollingStatus("InClass")
        }
        console.log(this.student_detail)
      }

    },
    setEnrollingStatus(status) {
      this.enroll_status = status
    },
    async handleCancelRegistration(confirmation) {
      if (confirmation) {
        this.eventBus.emit("open-confirmation-popup", {
          message: "Are you sure about this?",
          method : this.handleCancelRegistration,
          params : false
        })
      } else {
        await axios.patch(import.meta.env.VITE_API_URL + '/api/Student/' + this.user.students[0].id + '/change-status?status=Unregistered')
        await this.refresh()
      }
    },
    goToClass() {
      window.scrollTo(0, 0)
      this.$router.push("/class")
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

  }
}
</script>

<style>
#entrance-inform-table tr td,
#entrance-inform-table th {
  padding: 0.5rem 2rem 0.5rem 2rem;
  border: solid 1px #a9a9bd;
  text-align: center
}

#entrance-inform-table th {
  background-color: #eeeeff;
}
</style>