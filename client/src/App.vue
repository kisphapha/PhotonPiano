<template>
  <Authourization>
    <div v-if='!user || user.role == "Student"'>
      <StudentLayout />
    </div>
    <div v-else-if='user.role == "Staff"' class="h-screen">
      <StaffLayout />
    </div>
    <div v-else-if='user.role == "Instructor"'>
      <InstructorLayout />
    </div>
    <div v-else>
      <UnexpectedLayout />
    </div>
  </Authourization>

</template>

<script>
import Authourization from "./components/Common/Authourization.vue";
import StudentLayout from "./layouts/StudentLayout.vue";
import StaffLayout from "./layouts/StaffLayout.vue";
import InstructorLayout from "./layouts/InstructorLayout.vue";
import UnexpectedLayout from "./layouts/UnexpectedLayout.vue";
export default {
  name: "App",
  inject: ['eventBus'],
  components: { Authourization, StudentLayout, StaffLayout, InstructorLayout, UnexpectedLayout },
  data() {
    return {
      user: {
        role : "Staff"
      }
    }
  },
  methods: {
    async getUser() {
      const userPromise = new Promise((resolve) => {
        this.eventBus.emit("get-user", resolve);
      });
      const user = await userPromise;
      this.user = user;
    }
  },
  mounted() {
    if (localStorage.token) {
      this.getUser()

    }
    this.eventBus.on("update-app-user", async () => {
      await this.getUser()
    })
  }
}
</script>
<style>
::-webkit-scrollbar {
  width: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}

#staff-table tr td,
#staff-table th {
  padding: 0.5rem 1rem 0.5rem 1rem;
  text-align: center
}

#staff-table thead tr {
  background-color: #161618;
  color: white;
  font-weight: bold;
}

#staff-table tr {
  background-color: #eeeeee;
}

#staff-table tbody tr:nth-child(even) {
  background-color: #e0e0e0;
}

#staff-table thead tr th:first-child {
  border-radius: 3rem 0 0 0;
}

#staff-table thead tr th:last-child {
  border-radius: 0 3rem 0 0;
}

#staff-table tbody tr:last-child td:first-child {
  border-radius: 0 0 0 3rem;
}

#staff-table tbody tr:last-child td:last-child {
  border-radius: 0 0 3rem 0;
}
</style>