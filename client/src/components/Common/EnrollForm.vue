<template>
  <div v-if="this.user" class="mt-8 mb-8  mr-auto ml-auto w-1/2 ">
    <a href="#" @click='setEnrollingStatus("None")' class="underline text-blue-400">&lt; Back</a>
    <div class="bg-gray-300 rounded-lg p-8">
      <div class="text-3xl font-bold">Thank you for deciding to join us!</div>
      <div class="text-2xl">Let's check your information before enrolling</div>
      <div class="mt-4">
          Name : <span class="ml-4 font-bold">{{ this.user.name }}</span><br>
          Email : <span class="ml-4 font-bold">{{ this.user.email }}</span><br>
          Phone : <span class="ml-4 font-bold">{{ this.user.phone }}</span><br>
          Gender : <span class="ml-4 font-bold">{{ this.user.gender }}</span><br>
          Date Of Birth : <span class="ml-4 font-bold">{{ this.user.doB }}</span><br>
      </div>
      <div>
        Short Description : 
        <textarea v-model="short_desc" rows="4" class="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300" placeholder="Write a short description about your piano skills"></textarea>

      </div>
      <div class="flex justify-center mt-4">
        <button @click="enroll(false)" class="bg-blue-300 pl-12 pr-12 pt-4 pb-4 rounded-lg font-bold hover:bg-blue-500">Enroll</button>
      </div>
    </div>
  </div>

</template>

<script>
import axios from 'axios';

export default {
  name: "EnrollForm",
  inject: ["eventBus"],
  data() {
    return {
      user: null,
      student_status: "None",
      short_desc : ""
    }
  },
  methods: {
    async refresh() {
      const userPromise = new Promise((resolve) => {
        this.eventBus.emit("get-user", resolve);

      });
      const user = await userPromise;
      this.user = user;
      this.student_status = this.user?.students[0]?.status ?? "None"
    },
    setEnrollingStatus(status){
      this.eventBus.emit("update-home-page-enrolling-status",status)
    },
    async enroll(notAsking){
      if (notAsking){
        await axios.patch(import.meta.env.VITE_API_URL + '/api/Student/' + this.user.students[0].id + '/change-status?status=PendingRegistration')
        await axios.patch(import.meta.env.VITE_API_URL + '/api/Student/' + this.user.students[0].id + '/change-short-desc',{
          content : this.short_desc
        })

        this.eventBus.emit("update-home-page")
      } else {
        this.eventBus.emit("open-confirmation-popup",{
          message : "Are you sure about this?",
          callback : "confirm-enroll"
        })
      }
    }
  },
  mounted() {
    this.eventBus.on("confirm-enroll", async () => {
      this.enroll(true)
    })
    if (localStorage.token) {
      this.refresh()
    }
  }
}
</script>

<style></style>