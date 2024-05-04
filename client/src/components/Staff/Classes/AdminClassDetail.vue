<template>
    <button class="font-bold text-xl text-blue-500 flex mt-4" @click="back(0)">
        <span class="material-icons">arrow_back_ios</span> Back
    </button>
    <div v-if="this.class">
        <div>
            <div class="flex place-content-between ">
                <div class="flex text-4xl ">
                    <span>CLASS NAME :</span>
                    <div class="font-bold flex ml-4" v-if="!editMode">
                        {{ this.class.name }}
                        <img :src="this.colors[this.class.level - 1]" class="w-16 ml-4">
                    </div>
                    <input v-else class="ml-4 rounded-lg border" maxlength="20" v-model="editDto.className
        ">
                </div>
                <div class="font-bold flex gap-2">
                    <button v-if="!editMode"
                        class="bg-slate-800 hover:bg-slate-600 font-bold text-white px-4 py-2 rounded-lg"
                        @click="toggleEditMode">
                        Edit
                    </button>
                    <button v-if="editMode"
                        class="bg-green-400 hover:bg-green-200 font-bold text-white px-4 py-2 rounded-lg"
                        @click="toggleEditMode">
                        Save Change
                    </button>
                    <button v-if="editMode"
                        class="bg-red-400 hover:bg-red-200 font-bold text-white px-4 py-2 rounded-lg"
                        @click="toggleEditMode">
                        Cancel
                    </button>
                </div>

            </div>
            <div class="mt-4">
                <div class="text-2xl">
                    <div v-if="!editMode">
                        LEVEL : <span class="font-bold">{{ this.class.level }}</span> {{
        this.class_level[this.class.level - 1] }}
                    </div>
                    <div v-else>
                        LEVEL :
                        <select class="rounded-lg border" v-model="editDto.level">
                            <option value="1">1 {{ this.class_level[0] }}</option>
                            <option value="2">2 {{ this.class_level[1] }}</option>
                            <option value="3">3 {{ this.class_level[2] }}</option>
                            <option value="4">4 {{ this.class_level[3] }}</option>
                            <option value="5">5 {{ this.class_level[4] }}</option>
                        </select>
                    </div>
                </div>
                <div class="text-2xl">
                    <div v-if="!editMode">
                        INSTRUCTOR : <span class="font-bold">{{ this.class.instructor.user.name }}</span>
                    </div>
                    <div v-else>
                        INSTRUCTOR :
                        <select class="rounded-lg border" v-model="editDto.instructorId">
                            <option v-for="instructor in instructors" :key="instructor.id" :value="instructor.id">
                                {{ instructor.user.name }}
                            </option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="italic text-lg" v-if="!editMode">
                {{ "(" + this.class.startDate + " - " + this.class.endDate + ")" }}
            </div>
            <div v-else class="flex gap-2 text-lg">
                <span>Start Date : </span>
                <input class="ml-4 rounded-lg border" type="date" v-model="editDto.startDate">
            </div>
            <div class="text-lg">
                <div v-if="!editMode">
                    Size : <span class="font-bold">{{ this.class.size }}</span>
                </div>
                <div v-else>
                    Size :
                    <input class="ml-4 rounded-lg border" type="number" v-model="editDto.size">
                </div>
                <div>Total Student : <span class="font-bold">{{ this.class.students.length }}</span></div>
            </div>
        </div>
        <div class="mt-4 flex gap-4">
            <button @click="toggleInsertStudentPopup" v-if="this.class.size > this.class.students.length"
                class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                Add new students to this class
            </button>
            <button @click="null" v-if="!this.class.isAnnounced"
                class="p-2 bg-green-400 rounded-lg text-white font-bold shadow-md hover:bg-green-200">
                Annouce This Class
            </button>
        </div>
        <div class="flex place-content-between my-4">
            <div class=" flex gap-4">
                <span class=" p-1 text-2xl font-bold">View :</span>
                <select class="border p-1 rounded-md max-w-64 text-xl bg-slate-600 text-white" v-model="viewMode">
                    <option value="0">Student List</option>
                    <option value="1">Score management</option>
                </select>
            </div>
            <div class="flex gap-2">
                <input class="border p-1 rounded-md max-w-64" type="text" v-model="keyword_name"
                    placeholder="Search by name">
                <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
                    <span class="material-icons p-1">
                        search
                    </span>
                </button>
            </div>
        </div>
        <div v-if="viewMode == 0">
            <table id="staff-table" class="w-full">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Picture</th>
                        <th>
                            <div class="w-48">Name</div>
                        </th>
                        <th>
                            <div class="w-48">Email</div>
                        </th>
                        <th>Phone</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in this.class.students" :key="student.id">
                        <td>{{ student.id }}</td>
                        <td>
                            <div class="flex justify-center">
                                <img class="w-12 h-12 rounded-full" :src="student.user.picture">
                            </div>
                        </td>
                        <td>
                            <div class="break-words w-48">{{ student.user.name }}</div>
                        </td>
                        <td>
                            <div class="break-words w-48">{{ student.user.email }}</div>
                        </td>
                        <td>{{ student.user.phone }}</td>
                        <td>
                            <div
                                :class='"rounded-xl px-2 py-1 font-bold text-white" + (student.status == "InClass" ? " bg-green-400" : " bg-red-400")'>
                                {{ student.status }}
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-if="viewMode == 1">
            <table id="staff-table" class="w-full">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Status</th>
                        <th v-for="criterion in criteria" :key="criterion.id">
                            {{ criterion.name }}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in this.class.students" :key="student.id">
                        <td>{{ student.id }}</td>
                        <td>
                            <div class="overflow-x-auto w-32">{{ student.user.name }}</div>
                        </td>
                        <td>
                            <div
                                :class='"rounded-xl px-2 py-1 font-bold text-white" + (student.status == "InClass" ? " bg-green-400" : " bg-red-400")'>
                                {{ student.status }}
                            </div>
                        </td>
                        <td v-for="criterion in criteria" :key="criterion.id">
                            {{ getScore(student.id, criterion.id) }}
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>
        <button class="bg-red-400 hover:bg-red-200 font-bold text-white px-4 py-2 rounded-lg mt-4"
            @click="toggleEditMode">
            Delete this class
        </button>
        <div v-if="isOpenInserStudentPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center overflow-x-auto">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleInsertStudentPopup">X</button>
                    <InsertStudentIntoAClassForm :close="toggleInsertStudentPopup" :classId="this.classId"
                        :className="this.class.name" :maxStudent="this.class.size"
                        :currentStudents="this.class.students.length" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import InsertStudentIntoAClassForm from './InsertStudentIntoAClassForm.vue'

export default {
    name: "AdminClassDetail",
    components: { InsertStudentIntoAClassForm },
    inject: ['eventBus'],
    props: ['classId', 'back'],
    data() {
        return {
            class: {
                id: 1,
                name: "BLUE_69_2024",
                startDate: "2024-01-01",
                endDate: "2024-12-30",
                isAnnounced: false,
                size: 25,
                level: 5,
                instructor: {
                    id: 1,
                    user: {
                        name: "Diamond"
                    }
                },
                students: [
                    {
                        id: 1,
                        user: {
                            name: "TrầnGiaTrầnGiaTrầnGiaTrầnGiaTrầnGiaTrầnGiaTrầnGiaTrầnGiaTrầnGiaTrầnGia",
                            email: "xyz@a.com",
                            phone: "0987654321",
                            picture: "/src/assets/noavatar.jpg"
                        },
                        status: "InClass"
                    },
                    {
                        id: 2,
                        user: {
                            name: "xyz",
                            email: "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcvvv@a.com",
                            phone: "0123456789",
                            picture: "/src/assets/noavatar.jpg"
                        },
                        status: "DroppedOut"
                    }
                ]
            },
            criteria: [
                {
                    id: 1,
                    name: "Small Test"
                },
                {
                    id: 2,
                    name: "Assignment"
                },
                {
                    id: 3,
                    name: "Final"
                }
            ],
            classScore: [
                {
                    id: 1,
                    user: {
                        name: "abc",
                        email: "xyz@a.com",
                        phone: "0987654321"
                    },
                    studentClassScores: [
                        {
                            criteriaId: 1,
                            score: 10
                        },
                        {
                            criteriaId: 2,
                            score: 9.5
                        },
                        {
                            criteriaId: 3,
                            score: 10
                        },
                    ]
                },
                {
                    id: 2,
                    user: {
                        name: "xyz",
                        email: "abc@a.com",
                        phone: "0123456789"
                    },
                    studentClassScores: [
                        {
                            criteriaId: 1,
                            score: 9
                        },
                        {
                            criteriaId: 2,
                            score: 9
                        },
                        {
                            criteriaId: 3,
                            score: 9
                        },
                    ]
                }
            ],
            colors: [
                "/src/assets/diamond_blue.png",
                "/src/assets/diamond_pink.png",
                "/src/assets/diamond_red.png",
                "/src/assets/diamond_black.png",
                "/src/assets/diamond_white.png"
            ],
            class_level: [
                "(Beginner)",
                "(Novice)",
                "(Intermediate)",
                "(Advanced)",
                "(Virtuoso)",
            ],
            viewMode: 0,
            editMode: false,
            isOpenInserStudentPopup: false,
            keyword_name: "",
            editDto: {
                className: "",
                startDate: null,
                level: 0,
                size: 0,
                instructorId: 0
            },
            instructors: [
                {
                    id: 1,
                    user: {
                        name: "Diamond"
                    }
                },
                {
                    id: 2,
                    user: {
                        name: "Water Melon"
                    }
                },
            ]
        }
    },
    methods: {
        getScore(studentId, criteriaId) {
            const student = this.classScore.find(sc => sc.id == studentId)
            const score = student?.studentClassScores.find(scs => scs.criteriaId == criteriaId)
            return score?.score ?? 0
        },
        toggleInsertStudentPopup() {
            this.isOpenInserStudentPopup = !this.isOpenInserStudentPopup
        },
        toggleEditMode() {
            this.editMode = !this.editMode
        },
        async handleSearch() {
            //await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        async refresh() {
            this.setEditDto()
        },
        setEditDto() {
            this.editDto.className = this.class.name,
                this.editDto.startDate = this.class.startDate,
                this.editDto.level = this.class.level,
                this.editDto.instructorId = this.class.instructor.id,
                this.editDto.size = this.class.size
        }
    },
    mounted() {
        this.refresh()
    }
}
</script>

<style></style>