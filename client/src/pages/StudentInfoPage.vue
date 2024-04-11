<template>
    <div v-if="student">
        <div class="flex gap-4 p-8 ">
            <img class="w-32 h-32 rounded-full" :src="student.user.picture">
            <div class="text-4xl font-bold mt-auto mb-auto">{{ student.user.name }}</div>
        </div>
        <div class="flex">
            <div class="w-1/2">
                <div class="flex gap-8">
                    <div class="italic text-xl ml-4">General</div>
                    <button class="material-icons" @click="toggleEditMode">
                        edit
                    </button>
                </div>
                <hr />
                <table id="student-info-table" class="mt-4 ml-4 bg-slate-50">
                    <tbody v-if="!this.editMode">
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
                            <td>{{ student.user.doB }}</td>
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
                    <tbody v-else>
                        <tr>
                            <td>Full Name</td>
                            <td><input class="w-full border border-gray-400 p-1" type="text" v-model="editDto.name">
                            </td>
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
                            <td><input class="w-full border border-gray-400 p-1" type="date" v-model="editDto.dob"></td>
                        </tr>
                        <tr>
                            <td>Address</td>
                            <td><input class="w-full border border-gray-400 p-1" type="text" v-model="editDto.address">
                            </td>
                        </tr>
                        <tr>
                            <td>Gender</td>
                            <td>
                                <select class="w-full border border-gray-400 p-1" v-model="editDto.gender">
                                    <option value="Male">Male</option>
                                    <option value="Female">Female</option>
                                    <option value="Other">Other</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>Bank Account</td>
                            <td><input class="w-full border border-gray-400 p-1" type="text"
                                    v-model="editDto.bankAccount"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="flex justify-center">
                                <button @click="updateInformation(true)" class="px-4 py-2 rounded-md bg-blue-400 text-white">Save</button>
                            </td>
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
                            <td>{{ this.class_level[student.level] }}</td>
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
                            <td>{{ this.student_post?.user.posts.length ?? 0 }}</td>
                        </tr>
                        <tr>
                            <td>Number of Comments/Answers</td>
                            <td>{{ this.student_post?.user.comments.length ?? 0 }}</td>
                        </tr>
                        <tr>
                            <td>Number of Upvotes</td>
                            <td>{{ this.student_post_statistic.up_taken }}</td>
                        </tr>
                        <tr>
                            <td>Number of Downvotes</td>
                            <td>{{ this.student_post_statistic.down_taken }}</td>
                        </tr>
                        <tr>
                            <td>Upvotes given</td>
                            <td>{{ this.student_post_statistic.up_given }}</td>
                        </tr>
                        <tr>
                            <td>Downvotes given</td>
                            <td>{{ this.student_post_statistic.up_given }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="mt-4 flex">
            <div class="w-full">
                <div class="italic text-xl ml-4">Educational Path</div>
                <hr />
                <div v-if="this.student.studentClasses.length > 0" class="flex flex-col gap-8">
                    <div class="flex justify-center gap-8 mt-4 ml-4" v-for="studentClass in this.student.studentClasses"
                        :key="studentClass.id">
                        <div class="p-4 rounded-lg bg-gray-200 w-48 h-24 flex items-center justify-center">
                            {{ studentClass.class.startDate.substring(0, 4) }}
                        </div>
                        <table id="student-info-table" class="bg-slate-50 w-1/3">
                            <tbody>
                                <tr>
                                    <td>Class Name</td>
                                    <td>{{ studentClass.class.name }}</td>
                                </tr>
                                <tr>
                                    <td>Instructor</td>
                                    <td>{{ studentClass.class.instructor?.user.name ?? "" }}</td>
                                </tr>
                                <tr>
                                    <td>Result</td>
                                    <td>{{ studentClass.result ?? "" }}</td>
                                </tr>
                                <tr>
                                    <td>GPA</td>
                                    <td>{{ studentClass.gpa ?? "" }}</td>
                                </tr>
                                <tr>
                                    <td>Instructor Comments</td>
                                    <td>{{ studentClass.instructorComment ?? "" }}</td>
                                </tr>
                                <tr>
                                    <td>Lesson Attended</td>
                                    <td>{{ calculateAttendance(studentClass.studentLessons) + " / " +
        studentClass.studentLessons.length }}</td>
                                </tr>
                                <tr>
                                    <td>Ranking</td>
                                    <td>{{ studentClass.rank ?? "" }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div v-else class="italic text-center">
                    Look like you haven't enrolled any classes yet.
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
    inject: ["eventBus"],
    data() {
        return {
            student: null,
            student_post: null,
            editMode: false,
            student_post_statistic: {
                up_taken: 0,
                down_taken: 0,
                up_given: 0,
                down_give: 0
            },
            debts: [],
            class_level: [
                "",
                "Blue Diamond (Beginner)",
                "Pink Diamond (Novice)",
                "Red Diamond (Intermediate)",
                "Black Diamond (Advanced)",
                "White Diamond (Virtuoso)",
            ],
            editDto: {
                id: 0,
                name: '',
                email: '',
                dob: null,
                phone: '',
                address: '',
                gender: '',
                bankAccount: ''
            }
        }
    },
    mounted() {
        this.eventBus.on("update-profile", () => {
            if (localStorage.token) {
                this.fetchData(localStorage.token)
            } else {
                this.$router.push('/')
            }
        })
        if (localStorage.token) {
            this.fetchData(localStorage.token);
        } else {
            this.$router.push("/")
        }
        this.eventBus.on("handle-update-information-student-profile-page", async () => {
            this.editMode = false;
            await this.updateInformation(false)
            await this.fetchData(localStorage.token)
        })
    },
    methods: {
        statisticizeActiveness() {
            let totalUpTaken = 0, totalDowntaken = 0, totalUpGiven = 0, totalDownGiven = 0
            this.student_post.user.posts.forEach(p => {
                totalUpTaken += p.upvote
                totalUpTaken += p.downvote
            })
            this.student_post.user.comments.forEach(c => {
                totalUpTaken += c.upvote
                //totalUpTaken += p.downvote
            })
            this.student_post.user.postVotes.forEach(pv => {
                if (pv.upOrDown) {
                    totalUpGiven += 1
                } else {
                    totalDownGiven += 1
                }
            })
            this.student_post.user.commentVotes.forEach(cv => {
                if (cv.upOrDown) {
                    totalUpGiven += 1
                } else {
                    totalDownGiven += 1
                }
            })
            this.student_post_statistic.up_taken = totalUpTaken
            this.student_post_statistic.down_taken = totalDowntaken
            this.student_post_statistic.up_given = totalUpGiven
            this.student_post_statistic.down_given = totalDownGiven
        },
        async fetchData(token) {
            const response = await axios.get(import.meta.env.VITE_API_URL + '/api/auth/who-am-i', {
                headers: {
                    'Authorization': 'Bearer ' + token,
                }
            })

            if (response.data) {
                if (response.data.role != "Student") {
                    this.$router.push("/")
                } else {
                    const studentDetail = await axios.get(import.meta.env.VITE_API_URL + '/api/Student/' + response.data.students[0].id)
                    if (studentDetail.data) {
                        this.student = studentDetail.data
                        // console.log(this.student)
                        this.editDto.name = this.student.user.name
                        this.editDto.bankAccount = this.student.user.bankAccount
                        this.editDto.dob = this.student.user.doB
                        this.editDto.address = this.student.user.address
                        this.editDto.gender = this.student.user.gender
                        this.debts = []
                        const studentClasses = studentDetail.data.studentClasses
                        studentClasses.forEach(element => {
                            element.studentClassTuitions.forEach(td => {
                                if (td.status == 0) {
                                    this.debts.push(td)
                                }
                            })
                        });
                    }
                    const studentPostDetail = await axios.get(import.meta.env.VITE_API_URL + '/api/Student/' + response.data.students[0].id + '/posts', {
                        headers: {
                            'Authorization': 'Bearer ' + token,
                        }
                    })
                    if (studentPostDetail.data) {
                        this.student_post = studentPostDetail.data
                        this.statisticizeActiveness()
                    }
                }
            }
        },
        calculateRemindTimes(date1, date2) {
            const timeDifference = Math.abs(date2.getTime() - date1.getTime());
            const weeksDifference = Math.floor(timeDifference / (1000 * 60 * 60 * 24 * 7));
            return Math.floor(weeksDifference / 2);

        },
        calculateAttendance(studentLessons) {
            let count = 0;
            studentLessons.forEach((sl) => {
                if (sl.attendence == "Attended") {
                    count++
                }
            })
            return count;
        },
        toggleEditMode() {
            this.editMode = !this.editMode
        },
        async updateInformation(confirmation) {
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "handle-update-information-student-profile-page"
                })
            } else {
                await axios.patch(import.meta.env.VITE_API_URL + '/api/User/' + this.student.user.id, {
                    name: this.editDto.name,
                    dob: this.editDto.dob,
                    address: this.editDto.address,
                    bankAccount: this.editDto.bankAccount,
                    gender: this.editDto.gender
                })
            }
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