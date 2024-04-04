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
      Your request is pending
    </div>
  </div>

</template>

<script>
import PianoKeyboard from '../components/PianoKeyboard.vue';
import EnrollForm from '../components/EnrollForm.vue';

export default {
  name: "HomePage",
  inject: ["eventBus"],
  data() {
    return {
      user: null,
      student_status: "None",
      enroll_status : "None",
    }
  },
  components: { PianoKeyboard , EnrollForm},
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
    setEnrollingStatus(status){
      this.enroll_status = status
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

<style></style>