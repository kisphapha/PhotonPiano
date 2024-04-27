<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Accept Automatically</div>
        <div class="mb-4 italic">
            <div>Total Registation left : <span class="font-bold">{{ totalRegistrations - thisYearRegistration }}</span></div>
            <div>Targets : <span class="font-bold">{{ centerMaxValue }}</span></div>
            <div>This year accepted : <span class="font-bold">{{ thisYearRegistration }}</span></div>
        </div>
        <div v-if="(acceptValue > centerMaxValue - thisYearRegistration) || acceptValue < 0" class="bg-red-200 p-6 mb-2 rounded-lg">
            <div class="font-bold mr-2">WARNING : </div>
            <div>Number of students accepted has suppassed the center's target.
                Please reconsider before committing since undo task is very complicated</div>
        </div>
        <div class="mb-2 flex gap-2">
            <div>No. Student</div>
            <input class="p-2 rounded-lg border w-full" type="number" min="0" :max="totalRegistrations - thisYearRegistration" v-model="acceptValue">
        </div>
        
        <div class="flex gap-4 justify-center">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold" @click="handleApply">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
        
    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';
import axios from 'axios';

export default {
    name: "AutoAcceptPopup",
    inject : ['eventBus'],
    props : ['totalRegistrations','thisYearRegistration','centerMaxValue','close'],
    data(){
        return {
            acceptValue : 0,
        }
    },
    mounted(){
        this.acceptValue = this.totalRegistrations - this.thisYearRegistration;
        this.acceptValue = (this.acceptValue < 0) ? 0 : this.acceptValue 
    },
    methods : {
        async handleApply(){
            try {
                await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/auto-accepting?number=${this.acceptValue}`)

                this.eventBus.emit("open-result-dialog",{
                    message : "Success!!",
                    type : "Success"
                })
                await this.eventBus.emit("refresh-registration-page")
                this.handleCancel()
            }
            catch (e) {
                console.log(e)
                this.eventBus.emit("open-result-dialog",{
                    message : e.response?.data?.ErrorMessage ?? "Something went wrong!",
                    type : "Error"
                })
            }
        }
    }
}
</script>
<style></style>