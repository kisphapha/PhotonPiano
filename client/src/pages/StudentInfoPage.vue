<template>
    <div v-if="student">
        <div class="flex gap-4 p-8 ">
            <img class="w-32 h-32 rounded-full" :src="student.user.picture">
            <div class="text-4xl font-bold mt-auto mb-auto">{{ student.user.name }}</div>
        </div>
        <div class="flex">
            <div class="w-1/2">
                <div class="italic text-xl ml-4">General</div>
                <hr />
                <table id="student-info-table" class="mt-4 ml-4 bg-slate-50">
                    <tbody>
                        <tr>
                            <td>Full Name</td>
                            <td>{{ student.user.name }}</td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>{{ student.user.email }}</td>
                        </tr>
                        <tr>
                            <td>Phone</td>
                            <td>{{ student.user.phone }}</td>
                        </tr>
                        <tr>
                            <td>Date of Birth</td>
                            <td>{{ student.user.dob }}</td>
                        </tr>
                        <tr>
                            <td>Address</td>
                            <td>{{ student.user.address }}</td>
                        </tr>
                        <tr>
                            <td>Gender</td>
                            <td>{{ student.user.gender }}</td>
                        </tr>
                        <tr>
                            <td>Bank Account</td>
                            <td>{{ student.user.bankAccount }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="w-1/2">
                <div class="italic text-xl ml-4">Academic</div>
                <hr />
                <table id="student-info-table" class="mt-4 ml-4 bg-slate-50">
                    <tbody>
                        <tr>
                            <td>Current Class</td>
                            <td>{{ student.currentClass?.name ?? "" }}</td>
                        </tr>
                        <tr>
                            <td>Current Instructor</td>
                            <td>{{ student.currentClass?.instructor?.user.name ?? "" }}</td>
                        </tr>
                        <tr>
                            <td>Current Level</td>
                            <td>{{ this.class_level[student.level - 1] }}</td>
                        </tr>
                        <tr>
                            <td>Status</td>
                            <td>{{ student.status }}</td>
                        </tr>
                        <tr>
                            <td>Joined date</td>
                            <td>{{ student.joinedDate }}</td>
                        </tr>
                        <tr>
                            <td>Registration date</td>
                            <td>{{ student.registrationDate.substring(0, 10) +
        " " + student.registrationDate.substring(11, 20) }}</td>
                        </tr>
                        <tr>
                            <td>Entrace Test Band Score</td>
                            <td>{{ student.entranceTests[student.entranceTests.length - 1]?.bandScore ?? "" }}</td>
                        </tr>
                        <tr>
                            <td>Entrance Test Ranked</td>
                            <td>{{ student.entranceTests[student.entranceTests.length - 1]?.rank ?? "" }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="mt-4 flex">
            <div class="w-1/2">
                <div class="italic text-xl ml-4">Tuition Debt</div>
                <hr />
                <div v-if="this.debts.length > 0" class="mt-4 ml-4">
                    <table id="student-info-table" class="bg-slate-50">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Period</th>
                                <th>Annouced Date</th>
                                <th>Deadline</th>
                                <th>Remind times</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="debt in this.debts" :key="debt.id">
                                <td>{{ debt.id }}</td>
                                <td>{{ debt.month + "/" + debt.dueDate.substring(0, 4) }}</td>
                                <td>{{ debt.createDate.substring(0, 10) }}</td>
                                <td>{{ debt.dueDate.substring(0, 10) }}</td>
                                <td>{{ calculateRemindTimes(new Date(), new Date(debt.dueDate)) }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div v-else class="italic mt-4 ml-4">
                    Congratulations! You currently have no tuition debts
                </div>
            </div>
            <div class="w-1/2">
                <div class="italic text-xl ml-4">Activeness</div>
                <hr />
                <table id="student-info-table" class="mt-4 ml-4 bg-slate-50">
                    <tbody>
                        <tr>
                            <td>Number of Posts</td>
                            <td>1</td>
                        </tr>
                        <tr>
                            <td>Number of Comments/Answers</td>
                            <td>10</td>
                        </tr>
                        <tr>
                            <td>Number of Upvotes</td>
                            <td>69</td>
                        </tr>
                        <tr>
                            <td>Number of Downvotes</td>
                            <td>40</td>
                        </tr>
                        <tr>
                            <td>Upvotes given</td>
                            <td>24</td>
                        </tr>
                        <tr>
                            <td>Downvotes given</td>
                            <td>12</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="mt-4 flex">
            <div class="w-full">
                <div class="italic text-xl ml-4">Educational Path</div>
                <hr />
                <div class="flex flex-col gap-8">
                    <div class="flex justify-center gap-8 mt-4 ml-4">
                        <div class="p-4 rounded-lg bg-gray-200 w-48 h-24 flex items-center justify-center">
                            2023
                        </div>
                        <table id="student-info-table" class="bg-slate-50 w-1/3">
                            <tbody>
                                <tr>
                                    <td>Class Name</td>
                                    <td>Pink_2_2023</td>
                                </tr>
                                <tr>
                                    <td>Instructor</td>
                                    <td>White Wine</td>
                                </tr>
                                <tr>
                                    <td>Result</td>
                                    <td>Passed</td>
                                </tr>
                                <tr>
                                    <td>GPA</td>
                                    <td>7.5</td>
                                </tr>
                                <tr>
                                    <td>Instructor Comments</td>
                                    <td>Very good! Need more finger practicing!</td>
                                </tr>
                                <tr>
                                    <td>Lesson Attended</td>
                                    <td>95/100</td>
                                </tr>
                                <tr>
                                    <td>Ranking</td>
                                    <td>Good</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="flex justify-center gap-8 mt-4 ml-4">
                        <div class="p-4 rounded-lg bg-gray-200 w-48 h-24 flex items-center justify-center">
                            2024
                        </div>
                        <table id="student-info-table" class="bg-slate-50 w-1/3">
                            <tbody>
                                <tr>
                                    <td>Class Name</td>
                                    <td>RED_1_2024</td>
                                </tr>
                                <tr>
                                    <td>Instructor</td>
                                    <td>Baby Shark</td>
                                </tr>
                                <tr>
                                    <td>Result</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>GPA</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Instructor Comments</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Lesson Attended</td>
                                    <td>10/100</td>
                                </tr>
                                <tr>
                                    <td>Ranking</td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
//import { RouterLink } from 'vue-router';

export default {
    name: "StudentInfoPage",
    data() {
        return {
            student: null,
            debts: [],
            class_level: [
                "Blue Diamond (Beginner)",
                "Pink Diamond (Novice)",
                "Red Diamond (Intermediate)",
                "Black Diamond (Advanced)",
                "White Diamond (Virtuoso)",
            ]
        }
    },
    mounted() {
        if (localStorage.token) {
            this.fetchData(localStorage.token);
        }
    },
    methods: {
        async fetchData(token) {
            const response = await axios.get(import.meta.env.VITE_API_URL + '/api/auth/who-am-i', {
                headers: {
                    'Authorization': 'Bearer ' + token,
                }
            })
            if (response.data) {
                const studentDetail = await axios.get(import.meta.env.VITE_API_URL + '/api/Student/' + response.data.students[0].id)
                if (studentDetail.data) {
                    this.student = studentDetail.data
                    // console.log(this.student)
                    this.debts = []
                    const studentClasses = studentDetail.data.studentClasses
                    studentClasses.forEach(element => {
                        element.studentClassTuitions.forEach(td => {
                            if (td.status == 0) {
                                this.debts.push(td)
                            }
                        })
                    });
                    console.log(this.debts)
                }
            }
        },
        calculateRemindTimes(date1, date2) {
            const timeDifference = Math.abs(date2.getTime() - date1.getTime());
            const weeksDifference = Math.floor(timeDifference / (1000 * 60 * 60 * 24 * 7));
            return Math.floor(weeksDifference / 2);

        }
    }
}
</script>
<style>
#student-info-table tr td,
#student-info-table th {
    padding: 0.5rem 2rem 0.5rem 2rem;
    border: solid 1px #a9a9bd
}
</style>