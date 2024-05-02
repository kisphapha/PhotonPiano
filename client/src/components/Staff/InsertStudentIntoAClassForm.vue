<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="font-bold text-2xl">Add new students to : {{ this.className }}</div>
        <div class="text-lg italic">Additions Left : {{ this.maxStudent - this.currentStudents }}</div>
        <div class="flex gap-2 justify-end">
            <input class="border p-1 rounded-md max-w-64" type="text" v-model="keyword_name"
                placeholder="Search by name">
            <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
                <span class="material-icons p-1">
                    search
                </span>
            </button>
        </div>
        <div class="overflow-x-auto overflow-y-auto mt-2 ">
            <table id="staff-table">
                <thead>
                    <tr>
                        <th><input type="checkbox" /></th>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in students" :key="student">
                        <td><input type="checkbox" /></td>
                        <td>{{ student.id }}</td>
                        <td><div class="overflow-x-auto w-32">{{ student.user.name }}</div></td>
                        <td><div class="overflow-x-auto w-32">{{ student.user.email }}</div></td>
                        <td>{{ student.user.phone }}</td>
                    </tr>
                </tbody>
            </table>
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
            ]
        }
    }

}
</script>

<style></style>