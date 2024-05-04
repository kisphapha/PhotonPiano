<template>
    <div class="p-8">
        <div class="text-4xl font-bold">Staff Management</div>
        <div class="mt-8 flex justify-between flex-col lg:flex-row gap-4">
            <div class="w-full">
                <div class="flex place-content-between ">
                    <button @click="toggleAddPopup" class="px-4 py-2 bg-green-400 hover:bg-green-200 font-bold text-white rounded-lg">
                        Add New Staff
                    </button>
                    <div class="flex gap-2">
                        <input class="border p-1 rounded-md" type="text" v-model="keyword_name"
                            placeholder="Search by name">
                        <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
                            <span class="material-icons p-1">
                                search
                            </span>
                        </button>
                    </div>
                </div>

                <table id="staff-table" class="w-full mt-2">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Email</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="staff in staffs" :key="staff.id">
                            <td>{{ staff.id }}</td>
                            <td><button @click="setStaff(staff.id)"
                                    class="w-32 break-words text-blue-400 font-bold underline">{{ staff.name }}</button>
                            </td>
                            <td>{{ staff.email }}</td>
                        </tr>
                    </tbody>
                </table>
                <div class="flex gap-4 justify-center mt-4" v-if="this.staffs.length > 0">
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
            <div class="w-full">
                <div v-if="selectedStaff">
                    <div class="flex gap-12">
                        <img :src="selectedStaff.picture" class="w-48 h-48 rounded-full">
                        <div class="mt-4">
                            <ul class="list-disc">
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">Name :</span>
                                    <span class="break-words w-32" v-if="!editMode">
                                        {{ selectedStaff.name }}
                                    </span>
                                    <input v-else class="p-1 rounded-lg border w-[10rem]" type="text" v-model="editDto.name">
                                </li>
                                <hr>
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">Email :</span>
                                    <span class="break-words w-[10rem]" v-if="!editMode">{{ selectedStaff.email }}</span>
                                    <input v-else class="p-1 rounded-lg border w-[10rem]" type="text" v-model="editDto.email">
                                </li>
                                <hr>
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">Phone :</span>
                                    <span v-if="!editMode">{{ selectedStaff.phone }}</span>
                                    <input v-else class="p-1 rounded-lg border w-[10rem]" type="text" v-model="editDto.phone">
                                </li>
                                <hr>
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">DoB :</span>
                                    <span v-if="!editMode">{{ selectedStaff.doB }}</span>
                                    <input v-else class="p-1 rounded-lg border w-[10rem]" type="date" v-model="editDto.doB">
                                </li>
                                <hr>
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">Gender :</span>
                                    <span v-if="!editMode">{{ selectedStaff.gender }}</span>
                                    <select v-else class="p-1 rounded-lg border w-[10rem]" type="text" v-model="editDto.gender">
                                        <option value="Male">Male</option>
                                        <option value="Female">Female</option>
                                        <option value="Other">Other</option>
                                    </select>
                                </li>
                                <hr>
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">Address :</span>
                                    <span class="break-words w-[10rem]" v-if="!editMode">{{ selectedStaff.address }}</span>
                                    <input v-else class="p-1 rounded-lg border w-[10rem]" type="text" v-model="editDto.address">
                                </li>
                                <hr>
                                <li class="text-lg flex">
                                    <span class="w-[10rem] font-bold">Bank Account :</span>
                                    <span class="break-words w-[10rem]" v-if="!editMode">{{ selectedStaff.bankAccount }}</span>
                                    <input v-else class="p-1 rounded-lg border w-[10rem]" type="text" v-model="editDto.bankAccount">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="flex justify-end gap-4 mt-4">
                        <button class="px-4 py-2 bg-blue-400 hover:bg-blue-200 font-bold text-white rounded-lg" @click="setupEdit()" v-if="!editMode">
                            Edit Staff
                        </button>
                        <button class="px-4 py-2 bg-green-400 hover:bg-blue-200 font-bold text-white rounded-lg" @click="setupEdit()" v-if="editMode">
                            Save Changes
                        </button>
                        <button class="px-4 py-2 bg-red-400 hover:bg-blue-200 font-bold text-white rounded-lg" @click="setupEdit()" v-if="editMode">
                            Cancel
                        </button>
                        <button class="px-4 py-2 bg-red-600 hover:bg-reds-400 font-bold text-white rounded-lg">
                            Delete Staff
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="isOpenAddPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAddPopup">X</button>
                    <AddNewStaffForm :close="toggleAddPopup" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import AddNewStaffForm from '../../components/Staff/AddNewStaffForm.vue'

export default {
    name: "AdminStaffPage",
    components : {AddNewStaffForm},
    inject: ["eventBus"],
    data() {
        return {
            staffs: [
                {
                    id: 1,
                    name: "OmegaOmegaOmegaOmegaOm",
                    phone: "1234567890",
                    gender: "Male",
                    doB: "2003-08-16",
                    picture: "https://wallpapers.com/images/hd/cool-profile-picture-ld8f4n1qemczkrig.jpg",
                    email: "omega@gmail.com",
                    address: "Aearth 617",
                    bankAccount: "Hello world 797979797979",
                },
                {
                    id: 2,
                    name: "Alpha",
                    phone: "1234567890",
                    gender: "Male",
                    doB: "2003-08-16",
                    picture: "https://wallpapers.com/images/hd/cool-profile-picture-ld8f4n1qemczkrig.jpg",
                    email: "alpha@gmail.com",
                    address: "Aearth 617",
                    bankAccount: "Hello world 797979797979",
                },
                {
                    id: 3,
                    name: "Beta",
                    phone: "1234567890",
                    gender: "Male",
                    doB: "2003-08-16",
                    picture: "https://wallpapers.com/images/hd/cool-profile-picture-ld8f4n1qemczkrig.jpg",
                    email: "beta@gmail.com",
                    address: "Aearth 617",
                    bankAccount: "Hello world 797979797979",
                }
            ],
            selectedStaff: null,
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            keyword_name: "",
            editDto: {
                name: "",
                phone: "",
                gender: "",
                doB: "",
                email: "",
                address: "",
                bankAccount: "",
            },
            editMode : false,
            isOpenAddPopup : false
        }
    },
    methods: {
        async handlePageChange() {
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
        setStaff(id) {
            this.selectedStaff = this.staffs.find(s => s.id == id)
        },
        setupEdit(){
            if (this.editMode){
                this.editMode = false
            } else {
                this.editDto.name = this.selectedStaff.name
                this.editDto.address = this.selectedStaff.address
                this.editDto.bankAccount = this.selectedStaff.bankAccount
                this.editDto.doB = this.selectedStaff.doB
                this.editDto.email = this.selectedStaff.email
                this.editDto.gender = this.selectedStaff.gender
                this.editDto.phone = this.selectedStaff.phone
                this.editMode = true
            }
        },
        toggleAddPopup(){
            this.isOpenAddPopup = !this.isOpenAddPopup
        }
    }
}

</script>

<style></style>