<template>
    <button class="font-bold text-xl text-blue-500 flex mt-4" @click="back(0)">
        <span class="material-icons">arrow_back_ios</span> Back
    </button>
    <div v-if="this.class">
        <div>
            <div class="flex place-content-between">
                <div class="text-4xl flex">
                    CLASS NAME :
                    <div class="font-bold flex ml-4">{{ this.class.name }}
                        <img :src="this.colors[this.class.level - 1]" class="w-16 ml-4">
                    </div>
                </div>
                <div class="font-bold flex gap-2">
                    <button v-if="!editMode" class="bg-slate-800 hover:bg-slate-600 font-bold text-white px-4 py-2 rounded-lg" @click="toggleEditMode">
                        Edit
                    </button> 
                    <button v-if="editMode" class="bg-green-400 hover:bg-green-200 font-bold text-white px-4 py-2 rounded-lg" @click="toggleEditMode">
                        Save Change
                    </button> 
                    <button v-if="editMode" class="bg-red-400 hover:bg-red-200 font-bold text-white px-4 py-2 rounded-lg" @click="toggleEditMode">
                        Cancel
                    </button>               
                </div>
                
            </div>
            <div class="mt-4">
                <div class="text-2xl">
                    LEVEL : <span class="font-bold">{{ this.class.level }}</span> {{ this.class_level[this.class.level -
        1] }}
                </div>
                <div class="text-2xl">
                    INSTRUCTOR : <span class="font-bold">{{ this.class.instructor.user.name }}</span>
                </div>
            </div>
            <div class="italic text-lg">
                {{ "(" + this.class.startDate + " - " + this.class.endDate + ")" }}
            </div>
            <div class="text-lg">
                <div>Size : <span class="font-bold">{{ this.class.size }}</span></div>
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
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in this.class.students" :key="student.id">
                        <td>{{ student.id }}</td>
                        <td class="flex justify-center">
                            <img class="w-12 h-12 rounded-full" :src="student.user.picture">
                        </td>
                        <td>
                            <div class="overflow-x-auto w-48">{{ student.user.name }}</div>
                        </td>
                        <td>
                            <div class="overflow-x-auto w-48">{{ student.user.email }}</div>
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
                    user: {
                        name: "Diamond"
                    }
                },
                students: [
                    {
                        id: 1,
                        user: {
                            name: "Trần Gia",
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
                            email: "abc@a.com",
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
            isOpenInserStudentPopup: false
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
        toggleEditMode(){
            this.editMode = !this.editMode
        }
    }
}
</script>

<style></style>