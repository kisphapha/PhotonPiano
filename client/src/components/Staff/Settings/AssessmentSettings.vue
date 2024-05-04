<template>
    <div class="text-3xl font-bold">Assessments</div>
    <div class="mt-4">
        <div class="text-2xl font-bold">Class Criteria</div>
        <div class="font-bold text-red-500 text-xl" v-if="checkClassCriteria()">
            WARNING : The total weight of class criteria is not equal 100, please edit to calculate the score correctly
        </div>
        <table class="mt-4" id="staff-table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Weight (%)</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="crit in classCriteria" :key="crit.id">
                    <td>
                        <span v-if="crit.id != editingId">{{ crit.name }}</span>
                        <input v-else type="text" class="p-1 rounded-lg border w-32 text-center" v-model="editDto.name">
                    </td>
                    <td>
                        <div v-if="crit.id != editingId" class="w-96 break-words">{{ crit.description }}</div>
                        <input v-else type="text" class="p-1 rounded-lg border text-center"
                            v-model="editDto.description">
                    </td>
                    <td>
                        <span v-if="crit.id != editingId">{{ crit.weight * 100 }}</span>
                        <input v-else type="number" class="p-1 rounded-lg borde text-center w-16"
                            v-model="editDto.weight">
                    </td>
                    <td>
                        <div class="flex justify-center">
                            <button v-if="crit.id != editingId" @click="setEditingId(crit.id)">
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
                    <td><input v-if="addMode == 1" type="text" class="p-1 rounded-lg border w-32"></td>
                    <td>
                        <input v-if="addMode == 1" type="text" class="p-1 rounded-lg border">
                        <button v-if="addMode != 1"
                            class="px-3 py-1 rounded-lg bg-green-400 hover:bg-green-200 font-bold text-white"
                            @click="setAddMode(1)">
                            Add New
                        </button>
                    </td>
                    <td><input v-if="addMode == 1" type="number" class="p-1 rounded-lg border w-16" min="0" max="100">
                    </td>
                    <td>
                        <div class="flex gap-2" v-if="addMode == 1">
                            <button class="material-icons text-lime-500 text-3xl" @click="handleEdit()">
                                check_circle
                            </button>
                            <button class="material-icons text-red-500 text-3xl" @click="setAddMode(0)">
                                cancel
                            </button>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="mt-12">
        <div class="text-2xl font-bold">Entrance Slot Criteria</div>
        <div class="font-bold text-red-500 text-xl" v-if="checkEntranceCriteria()">
            WARNING : The total weight of entrance criteria is not equal 100, please edit to calculate the score correctly
        </div>
        <table class="mt-4" id="staff-table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Weight (%)</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="crit in entranceCriteria" :key="crit.id">
                    <td>
                        <span v-if="crit.id != editingId">{{ crit.name }}</span>
                        <input v-else type="text" class="p-1 rounded-lg border w-32 text-center" v-model="editDto.name">
                    </td>
                    <td>
                        <div v-if="crit.id != editingId" class="w-96 break-words">{{ crit.description }}</div>
                        <input v-else type="text" class="p-1 rounded-lg border text-center"
                            v-model="editDto.description">
                    </td>
                    <td>
                        <span v-if="crit.id != editingId">{{ crit.weight * 100 }}</span>
                        <input v-else type="number" class="p-1 rounded-lg borde text-center w-16"
                            v-model="editDto.weight">
                    </td>
                    <td>
                        <div class="flex justify-center">
                            <button v-if="crit.id != editingId" @click="setEditingId(crit.id)">
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
                    <td><input v-if="addMode == 2" type="text" class="p-1 rounded-lg border w-32"></td>
                    <td>
                        <input v-if="addMode == 2" type="text" class="p-1 rounded-lg border">
                        <button v-if="addMode != 2"
                            class="px-3 py-1 rounded-lg bg-green-400 hover:bg-green-200 font-bold text-white"
                            @click="setAddMode(2)">
                            Add New
                        </button>
                    </td>
                    <td><input v-if="addMode == 2" type="number" class="p-1 rounded-lg border w-16" min="0" max="100">
                    </td>
                    <td>
                        <div class="flex gap-2" v-if="addMode == 2">
                            <button class="material-icons text-lime-500 text-3xl" @click="handleEdit()">
                                check_circle
                            </button>
                            <button class="material-icons text-red-500 text-3xl" @click="setAddMode(0)">
                                cancel
                            </button>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
export default {
    name: "AssessmentSettings",
    data() {
        return {
            classCriteria: [
                {
                    id: 1,
                    name: "Small Test",
                    weight: 0.2,
                    description: "HelloWorldHelloWorldHe"
                },
                {
                    id: 2,
                    name: "Big Test",
                    weight: 0.3,
                    description: "Hello World"
                },
                {
                    id: 3,
                    name: "Final",
                    weight: 0.5,
                    description: "Hello World"
                },
            ],
            entranceCriteria: [
                {
                    id: 4,
                    name: "Listen",
                    weight: 0.25,
                    description: "Hello World"
                },
                {
                    id: 5,
                    name: "Speak",
                    weight: 0.25,
                    description: "Hello World"
                },
                {
                    id: 6,
                    name: "Read",
                    weight: 0.25,
                    description: "Hello World"
                },
                {
                    id: 7,
                    name: "Write",
                    weight: 0.25,
                    description: "Hello World"
                },
            ],
            addMode: 0,
            addingBane: "",
            addingDescription: "",
            addingWeight: 0,
            editingId: 0,
            editDto: {
                name: "",
                description: "",
                weight: 0.0
            }
        }
    },
    methods: {
        setAddMode(mode) {
            this.addMode = mode
        },
        checkClassCriteria() {
            var weight = 0;
            for (var crit of this.classCriteria) {
                weight += crit.weight
            }
            return weight != 1.0
        },
        checkEntranceCriteria() {
            var weight = 0;
            for (var crit of this.entranceCriteria) {
                weight += crit.weight
            }
            return weight != 1.0
        },
        setEditingId(id) {
            this.editingId = id
            const classCrit = this.classCriteria.find(c => c.id == id)
            const entranceCrit = this.entranceCriteria.find(c => c.id == id)
            if (classCrit) {
                this.editDto.name = classCrit.name
                this.editDto.description = classCrit.description
                this.editDto.weight = classCrit.weight * 100
            } else if (entranceCrit) {
                this.editDto.name = entranceCrit.name
                this.editDto.description = entranceCrit.description
                this.editDto.weight = entranceCrit.weight * 100
            }
        }
    }
}
</script>

<style></style>