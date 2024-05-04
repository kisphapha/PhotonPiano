<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="font-bold text-2xl">Add new students to : {{ this.className }}</div>
        <div class="text-lg italic">Additions Left : {{ this.maxStudent - this.currentStudents }}</div>
        <div :class='"text-lg italic " + (this.selectedStudentIds.length > (this.maxStudent - this.currentStudents) ? "text-red-500 font-bold" : "")'>
            Selected : {{ this.selectedStudentIds.length }}
        </div>
        <div class="flex gap-2 justify-end">
            <input class="border p-1 rounded-md max-w-64" type="text" v-model="keyword_name"
                placeholder="Search by name">
            <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
                <span class="material-icons p-1">
                    search
                </span>
            </button>
        </div>
        <div class="overflow-x-auto overflow-y-auto mt-2 max-h-96">
            <table id="staff-table">
                <thead>
                    <tr>
                        <th><input type="checkbox" @change="toggleSelectAll" :checked="isSelectAll" /></th>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in students" :key="student">
                        <td><input type="checkbox" @change="toggleSelection(student.id)" :checked="isSelected(student.id)"/></td>
                        <td>{{ student.id }}</td>
                        <td>
                            <div class="break-words w-32">{{ student.user.name }}</div>
                        </td>
                        <td>
                            <div class="break-words w-32">{{ student.user.email }}</div>
                        </td>
                        <td>{{ student.user.phone }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
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
        <div class="flex gap-4 mt-4 justify-center">
            <button @click="handleOk"
                class="bg-blue-400 hover:bg-blue-200 px-6 py-2 rounded-lg text-white font-bold">OK</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
    </div>
</template>

<script>
export default {
    name: "InsertStudentIntoAClassForm",
    props: ['close', 'classId', 'className', 'currentStudents', 'maxStudent'],
    data() {
        return {
            students: [
                {
                    id: 1,
                    user: {
                        name: "AWFHJAWF",
                        phone: "09876654331",
                        email: "awfawawfaw@abc.com",
                    }
                },
                {
                    id: 2,
                    user: {
                        name: "AWFHJAWF",
                        phone: "09876654331",
                        email: "awfaw@abc.com",
                    }
                },
                {
                    id: 3,
                    user: {
                        name: "AWFHJAWF",
                        phone: "09876654331",
                        email: "awfaw@abc.com",
                    }
                }
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            keyword_name: "",
            selectedStudentIds: [],
            isSelectAll: false,
            
        }
    },
    methods: {
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
        async movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                await this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                await this.handlePageChange()
            }
        },
        async handleSearch() {
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

<style></style>