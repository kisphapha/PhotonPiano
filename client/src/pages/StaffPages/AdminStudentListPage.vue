<template>
    <div class="p-8">
        <div class="text-4xl font-bold">Student List</div>
        <div class="flex place-content-between mt-4">
            <div class="flex gap-2">
                <button @click="null"
                    class="flex gap-2 py-2 px-6 bg-gray-600 rounded-lg text-white font-bold shadow-md hover:bg-gray-300">
                    <span>Edit</span>
                    <span class="material-icons">
                        edit
                    </span>
                </button>
            </div>
            <div class="flex gap-2">
                <button @click="toggleFilterPopup"
                    class="flex gap-2 py-2 px-6 bg-slate-900 rounded-lg text-white font-bold shadow-md hover:bg-slate-500">
                    <span>Filter</span>
                    <span class="material-icons">
                        filter_list
                    </span>
                </button>
            </div>
        </div>
        <div class="mt-4">
            <table class="w-full" id="staff-table">
                <thead>
                    <tr>
                        <th><input type="checkbox" @change="toggleSelectAll" :checked="isSelectAll"></th>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Current Class</th>
                        <th>Join Date</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in students" :key="student.id">
                        <td><input type="checkbox" @change="toggleSelection(student.id)" :checked="isSelected(student.id)"></td>
                        <td>{{ student.id }}</td>
                        <td>
                            <button class="text-blue-400 font-bold underline">
                                <div class="w-32 break-words">
                                    {{ student.user.name }}
                                </div>
                            </button>
                        </td>
                        <td>
                            <div class="w-32 break-words">{{ student.user.email }}</div>
                        </td>
                        <td>{{ student.user.phone }}</td>
                        <td :class="getClassStyle(student.level)">{{ student.currentClass.name }}</td>
                        <td>{{ student.joinedDate }}</td>
                        <td>
                            <div :class="getStatusStyle(student.status)" class="w-32">
                                {{ displayStatus(student.status) }}
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="flex gap-4 justify-center mt-4" v-if="this.students.length > 0">
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
                    <StudentFilterForm :close="toggleFilterPopup" :id="filterDto.id" :name="filterDto.name"
                        :level="filterDto.level" :joinFrom="filterDto.joinFrom" :joinTo="filterDto.joinTo"
                        :email="filterDto.email" :phone="filterDto.phone" :className="filterDto.className"
                        :status="filterDto.status" :action="handleFilter" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import StudentFilterForm from '../../components/Staff/StudentFilterForm.vue'

export default {
    name: "AdminStudentListPage",
    components: { StudentFilterForm },
    inject: ["eventBus"],
    data() {
        return {
            students: [
                {
                    id: 1,
                    user: {
                        name: "HelloWorldWorldWorldWorld",
                        phone: "0987654321",
                        email: "example@gmail.com",
                        picture: "/src/assets/noavatar.jpg"
                    },
                    currentClass: {
                        name: "BLUE_69_2024"
                    },
                    level: 3,
                    joinedDate: "2024-01-01",
                    status: "InClass"
                },
                {
                    id: 2,
                    user: {
                        name: "Hello World",
                        phone: "0987654321",
                        email: "example@gmail.com",
                        picture: "/src/assets/noavatar.jpg"
                    },
                    currentClass: {
                        name: "BLUE_69_2024"
                    },
                    level: 2,
                    status: "Dropped",
                    joinedDate: "2024-01-01",
                },
            ],
            filterDto: {
                id: null,
                name: null,
                level: 0,
                joinFrom: null,
                joinTo: null,
                email: "",
                phone: "",
                className: "",
                status: "all"
            },
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            isOpenFilterPopup: false,
            isSelectAll : false,
            selectedStudentIds : []
        }
    },
    methods: {
        getStatusStyle(status) {
            let css = "font-bold text-white px-3 py-1 rounded-lg "
            switch (status) {
                case "InClass":
                    return css + "bg-green-400"
                case "Unregistered":
                    return css + "bg-blue-400"
                case "PendingRegistration":
                    return css + "bg-purple-400"
                case "Accepted":
                    return css + "bg-pink-400"
                case "EntranceTesting":
                    return css + "bg-yellow-500"
                case "WaitingForClassPlacement":
                    return css + "bg-orange-400"
                case "Dropped":
                    return css + "bg-red-400"
            }
        },
        displayStatus(status) {
            switch (status) {
                case "PendingRegistration":
                    return "Pending Registration"
                case "EntranceTesting":
                    return "Entrance Testing"
                case "WaitingForClassPlacement":
                    return "Waiting Class"
            }
            return status
        },
        getClassStyle(level) {
            switch (level) {
                case 1:
                    return "text-blue-400"
                case 2:
                    return "text-pink-400"
                case 3:
                    return "text-red-400"
                case 4:
                    return "text-black"
                case 5:
                    return "text-gray-400"
            }
            return ""
        },
        handleFilter(filterDto) {
            this.filterDto.id = filterDto.id
            this.filterDto.name = filterDto.name
            this.filterDto.level = filterDto.level
            this.filterDto.joinFrom = filterDto.joinFrom
            this.filterDto.joinTo = filterDto.joinTo
            this.filterDto.email = filterDto.email
            this.filterDto.phone = filterDto.phone
            this.filterDto.className = filterDto.className
            this.filterDto.status = filterDto.status
            this.toggleFilterPopup()
        },
        toggleFilterPopup() {
            this.isOpenFilterPopup = !this.isOpenFilterPopup
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
        async handlePageChange() {
            this.isSelectAll = false;
            if (this.currentPage > this.totalPage) {
                this.currentPage = this.totalPage
            }
            if (this.currentPage < 1) {
                this.currentPage = 1
            }
            //await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        isSelected(id) {
            return this.selectedStudentIds.includes(id);
        },
        toggleSelection(id) {
            if (this.isSelected(id)) {
                const index = this.selectedStudentIds.indexOf(id);
                this.selectedStudentIds.splice(index, 1);
            } else {
                this.selectedStudentIds.push(id);
            }
        },
        clearAllSelection() {
            this.selectedStudentIds = []
        },
        toggleSelectAll() {
            if (this.isSelectAll) {
                this.isSelectAll = false
                for (var student of this.students) {
                    if (this.isSelected(student.id)) {
                        const index = this.selectedStudentIds.indexOf(student.id);
                        this.selectedStudentIds.splice(index,1)
                    }
                }
            } else {
                this.isSelectAll = true
                for (var student of this.students) {
                    if (!this.isSelected(student.id)) {
                        this.selectedStudentIds.push(student.id)
                    }
                }
            }
        }
    }
}

</script>

<style>

</style>