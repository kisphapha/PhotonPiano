<template>
    <div class="mt-4 ml-4 mr-4">
        <div class="text-4xl font-bold">Scheduling</div>
        <div class="flex gap-2 place-content-between mt-6">
            <div class="flex gap-4">
                <button @click=""
                    class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                    Schedule All
                </button>
                <button @click=""
                    class="p-2 bg-green-400 rounded-lg text-white font-bold shadow-md hover:bg-green-200">
                    Announce All
                </button>
                <button @click=""
                    class="p-2 bg-red-400 rounded-lg text-white font-bold shadow-md hover:bg-red-200">
                    Clear All
                </button>
            </div>

            <div class="flex gap-2">
                <button @click="toggleAutomaticPopup"
                    class="flex gap-2 py-2 px-6 bg-slate-900 rounded-lg text-white font-bold shadow-md hover:bg-slate-500">
                    <span>Filter</span>
                    <span class="material-icons">
                        filter_list
                    </span>
                </button>
            </div>

        </div>
        <table id="staff-table" class="mt-2 w-full">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Level</th>
                    <th>Period</th>
                    <th>Lessons</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="class_ in this.classes" :key="class_.id">
                    <td>{{ class_.id }}</td>
                    <td><button class="text-blue-400 underline font-bold">{{ class_.name }}</button></td>
                    <td>{{ class_.level }}</td>
                    <td>{{ class_.period }}</td>
                    <td :class='class_.lessons == 0 ? "text-red-500 italic" : ""'>
                        {{ class_.lessons == 0 ? "(Not Schedule Yet)" : class_.lessons }}
                    </td>
                    <td>
                        <div class="flex gap-2 justify-center">
                            <button class="material-icons text-3xl">
                                notifications_none
                            </button>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="flex gap-4 justify-center mt-4">
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
</template>

<script>
export default {
    name: "SchedulePage",
    inject: ["eventBus"],
    data() {
        return {
            classes: [
                {
                    id: 1,
                    name: "BLUE_1_2024",
                    level: 1,
                    period: 2024,
                    lessons: 0,
                    isNotified : false,
                }, {
                    id: 2,
                    name: "BLUE_2_2024",
                    level: 1,
                    period: 2024,
                    lessons: 30,
                    isNotified : true,
                },
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
        }
    },
    methods: {
        handlePageChange() {

        },
        movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                this.handlePageChange()
            }
        },
    }
}

</script>

<style></style>