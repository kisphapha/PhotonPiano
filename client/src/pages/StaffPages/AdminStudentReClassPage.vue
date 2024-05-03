<template>
    <div class="p-8">
        <div class="text-4xl font-bold">Switch class requests</div>
        <div>
            <table id="staff-table" class="w-full mt-4">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Old Class</th>
                        <th>New Class</th>
                        <th>Request Time</th>
                        <th>Expired At</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="request in requests" :key="request">
                        <td>{{ request.id }}</td>
                        <td>{{ request.student.user.name }}</td>
                        <td>{{ request.oldClass.name }}</td>
                        <td>{{ request.newClass.name }}</td>
                        <td>{{ request.requestDate }}</td>
                        <td>{{ request.expitedAt }}</td>
                        <td>
                            <div class="flex gap-2">
                                <button class="material-icons text-lime-500 text-3xl"
                                    @click="handleAccept(request.id)">
                                    check_circle
                                </button>
                                <button class="material-icons text-red-500 text-3xl"
                                    @click="handleReject(request.id)">
                                    cancel
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="flex gap-4 justify-center mt-4" v-if="this.requests.length > 0">
            <button @click="movePage(false)">
                <span class="material-icons p-1">arrow_back_ios</span>
            </button>
            <div class="flex gap-2 ">
                <input class="border p-1 rounded-md w-16" type="number" v-model="currentPage" min="1"
                    @change="handlePageChange">
                <div class="p-1"> / {{ this.totalPage }}</div>
            </div>
            <button @click="movePage(true)">
                <span class="material-icons p-1">arrow_forward_ios</span>
            </button>
        </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "AdminStudentReClassPage",
    inject: ["eventBus"],
    data() {
        return {
            requests: [
                {
                    id: 1,
                    student: {
                        user: {
                            name: "Oggy"
                        }
                    },
                    oldClass: {
                        name: "BLUE_69_2024"
                    },
                    newClass: {
                        name: "BLUE_96_2024"
                    },
                    requestDate: "2024-01-01",
                    expitedAt: "2024-03-01"
                },
                {
                    id: 2,
                    student: {
                        user: {
                            name: "Jack"
                        }
                    },
                    oldClass: {
                        name: "BLUE_69_2024"
                    },
                    newClass: {
                        name: "BLUE_96_2024"
                    },
                    requestDate: "2024-01-01",
                    expitedAt: "2024-03-01"
                }
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
        }
    },
    methods : {
        async handlePageChange() {
            if (this.currentPage > this.totalPage){
                this.currentPage = this.totalPage
            }
            if (this.currentPage < 1){
                this.currentPage = 1
            }
            //await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        async movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                await this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                await this.handlePageChange()
            }
        },
    }
}

</script>

<style></style>