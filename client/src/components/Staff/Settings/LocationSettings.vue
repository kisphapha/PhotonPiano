<template>
    <div class="text-3xl font-bold">Locations</div>
    <div class="flex gap-2 mt-4 justify-end">
        <input class="border p-1 rounded-md" type="text" v-model="keyword_name" placeholder="Search by name">
        <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
            <span class="material-icons p-1">
                search
            </span>
        </button>
    </div>
    <table class="mt-2 w-full" id="staff-table">
        <thead>
            <tr>
                <th>Name</th>
                <th>Capacity</th>
                <th>Status</th>
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="location in locations" :key="location.id">
                <td>
                    <span class="w-96 break-words" v-if="location.id != editingId">{{ location.name }}</span>
                    <input v-else type="text" class="p-1 rounded-lg border w-32 text-center" v-model="location.name">
                </td>
                <td>
                    <div v-if="location.id != editingId">{{ location.capacity }}</div>
                    <input v-else type="number" class="p-1 rounded-lg border text-center" v-model="location.capacity">
                </td>
                <td>
                    <span v-if="location.id != editingId">{{ location.status }}</span>
                    <select v-else class="p-1 rounded-lg borde text-center" v-model="editDto.status">
                        <option value="Available">Available</option>
                        <option value="Unavailable">Unavailable</option>
                    </select>
                </td>
                <td>
                    <div class="flex justify-center">
                        <button v-if="location.id != editingId" @click="setEditingId(location.id)">
                            <span class="material-icons">
                                edit
                            </span>
                        </button>
                        <div class="flex gap-2" v-else>
                            <button class="material-icons text-lime-500 text-3xl" @click="handleEdit()">
                                check_circle
                            </button>
                            <button class="material-icons text-red-500 text-3xl" @click="setEditingId(0)">
                                cancel
                            </button>
                        </div>
                        <button @click="handleDelete({ confirmation: true, id: crit.id })">
                            <span class="material-icons">
                                delete
                            </span>
                        </button>
                    </div>
                </td>
            </tr>
            <tr>
                <td><input v-if="addMode" type="text" class="p-1 rounded-lg border text-center" v-model="addingName">
                </td>
                <td>
                    <input v-if="addMode" type="number" class="p-1 rounded-lg border text-center"
                        v-model="addingCapacity">
                    <button v-if="!addMode"
                        class="px-3 py-1 rounded-lg bg-green-400 hover:bg-green-200 font-bold text-white"
                        @click="toggleAddMode">
                        Add New
                    </button>
                </td>
                <td>
                    <select v-if="addMode" class="p-1 rounded-lg borde text-center" v-model="addingStatus">
                        <option value="Available">Available</option>
                        <option value="Unavailable">Unavailable</option>
                    </select>
                </td>
                <td class="flex justify-center">
                    <div class="flex gap-2" v-if="addMode">
                        <button class="material-icons text-lime-500 text-3xl" @click="handleAdd">
                            check_circle
                        </button>
                        <button class="material-icons text-red-500 text-3xl" @click="toggleAddMode">
                            cancel
                        </button>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script>
export default {
    name: "LocationSettings",
    data() {
        return {
            locations: [
                {
                    id: 1,
                    name: "BeethovenBeethoven",
                    capacity: 30,
                    status: "Available"
                },
                {
                    id: 2,
                    name: "Mozart",
                    capacity: 30,
                    status: "Available"
                },
                {
                    id: 3,
                    name: "Lizst",
                    capacity: 30,
                    status: "Unavailable"
                }
            ],
            editingId: 0,
            editDto: {
                name: "",
                capacity: 0,
                status: "Available"
            },
            addMode: false,
            addingName: "",
            addingCapacity: 0,
            addingStatus: "Available",
            keyword_name: ""
        }
    },
    methods: {
        setEditingId(id) {
            this.editingId = id
            const location = this.locations.find(l => l.id == id)
            if (location) {
                this.editDto.name = location.name
                this.editDto.capacity = location.capacity
                this.editDto.status = location.status
            }
        },
        toggleAddMode() {
            this.addMode = !this.addMode
        }
    }
}
</script>

<style></style>