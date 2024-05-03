<template>
    <div class="p-8">
        <div class="text-4xl font-bold">Instructors </div>
        <div class="flex gap-2 mt-4 justify-end">
            <button @click="toggleFilterPopup"
                class="flex gap-2 py-2 px-6 bg-slate-900 rounded-lg text-white font-bold shadow-md hover:bg-slate-500">
                <span>Filter</span>
                <span class="material-icons">
                    filter_list
                </span>
            </button>
        </div>
        <div class="mt-2">
            <table id="staff-table" class="w-full">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Level</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                        <th>Classes Teaching</th>
                        <th>Contribute Score</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="instructor in instructors" :key="instructor.id">
                        <td>{{ instructor.id }}</td>
                        <td>
                            <div class="w-32 break-words">{{ instructor.user.name }}</div>
                        </td>
                        <td>{{ instructor.level }}</td>
                        <td>
                            <div class="w-32 break-words">{{ instructor.user.email }}</div>
                        </td>
                        <td>{{ instructor.user.phone }}</td>
                        <td>{{ instructor.classesTeaching }}</td>
                        <td>{{ instructor.contributeScore }}</td>
                        <td>
                            <div class="flex gap-2">
                                <button class="material-icons text-lime-500 text-3xl"
                                    @click="handleAccept(instructor.id)">
                                    check_circle
                                </button>
                                <button class="material-icons text-red-500 text-3xl"
                                    @click="handleReject(instructor.id)">
                                    cancel
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="flex gap-4 justify-center mt-4" v-if="this.instructors.length > 0">
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
        <div v-if="isOpenFilterPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleFilterPopup">X</button>
                    <InstructorFilterForm :close="toggleFilterPopup" :id="filterDto.id" :name="filterDto.name"
                        :level="filterDto.level" :joinFrom="filterDto.joinFrom" :joinTo="filterDto.joinTo"
                        :email="filterDto.email" :phone="filterDto.phone" :teachFrom="filterDto.teachFrom"
                        :teachTo="filterDto.teachTo" :contributeFrom="filterDto.contributeFrom"
                        :contributeTo="filterDto.contributeTo" :status="filterDto.status" :action="handleFilter" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import InstructorFilterForm from '../../components/Staff/InstructorFilterForm.vue'

export default {
    name: "AdminInstructorPage",
    components: { InstructorFilterForm },
    inject: ["eventBus"],
    data() {
        return {
            instructors: [
                {
                    id: 1,
                    user: {
                        name: "Diamond",
                        phone: "0987654321",
                        email: "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabc@gmail.com"
                    },
                    joinDate: "2003-01-01",
                    level: 5,
                    classesTeaching: 2,
                    contributeScore: 99999
                },
                {
                    id: 2,
                    user: {
                        name: "White Wine",
                        phone: "0987654321",
                        email: "abc@gmail.com"
                    },
                    joinDate: "2003-01-01",
                    level: 3,
                    classesTeaching: 1,
                    contributeScore: 696
                }
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            isOpenFilterPopup: false,
            filterDto: {
                id: null,
                name: null,
                level: 0,
                joinFrom: null,
                joinTo: null,
                teachFrom: null,
                teachTo: null,
                contributeFrom: null,
                contributeTo: null,
                email: "",
                phone: "",
                status: "all"
            },
        }
    },
    methods: {
        async handlePageChange() {
            if (this.currentPage > this.totalPage) {
                this.currentPage = this.totalPage
            }
            if (this.currentPage < 1) {
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
        toggleFilterPopup() {
            this.isOpenFilterPopup = !this.isOpenFilterPopup
        },
        handleFilter(filterDto) {
            this.filterDto.id = filterDto.id
            this.filterDto.name = filterDto.name
            this.filterDto.level = filterDto.level
            this.filterDto.joinFrom = filterDto.joinFrom
            this.filterDto.joinTo = filterDto.joinTo
            this.filterDto.contributeFrom = filterDto.contributeFrom
            this.filterDto.contributeTo = filterDto.contributeTo
            this.filterDto.teachFrom = filterDto.teachFrom
            this.filterDto.teachTo = filterDto.teachTo
            this.filterDto.email = filterDto.email
            this.filterDto.phone = filterDto.phone
            this.filterDto.status = filterDto.status
            this.toggleFilterPopup()
        },
    }
}

</script>

<style></style>