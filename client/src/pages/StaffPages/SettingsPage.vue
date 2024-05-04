<template>
    <div class="p-8">
        <div class="text-4xl font-bold">Settings</div>
        <div class="mt-4 flex gap-1">
            <button :class='getButtonStyle("Features")' @click='setSettingType("Features")'>Features</button>
            <button :class='getButtonStyle("Assessments")' @click='setSettingType("Assessments")'>Assessments</button>
            <button :class='getButtonStyle("Locations")' @click='setSettingType("Locations")'>Locations</button>
            <button :class='getButtonStyle("Others")' @click='setSettingType("Others")'>Others</button>
        </div>
        <div class="shadow-md rounded-lg w-full p-4">
            <FeatureSettings v-if='settingType == "Features"'/>
            <AssessmentSettings v-if='settingType == "Assessments"'/>
            <LocationSettings v-if='settingType == "Locations"'/>
            <OtherSettings v-if='settingType == "Others"'/>
        </div>
    </div>
</template>

<script>
import FeatureSettings from '../../components/Staff/Settings/FeatureSettings.vue';
import AssessmentSettings from '../../components/Staff/Settings/AssessmentSettings.vue';
import LocationSettings from '../../components/Staff/Settings/LocationSettings.vue';
import OtherSettings from '../../components/Staff/Settings/OtherSettings.vue';

export default {
    name: "SettingsPage",
    components : {FeatureSettings,AssessmentSettings,LocationSettings,OtherSettings},
    inject: ["eventBus"],
    data(){
        return {
            settingType : "Features"
        }
    },
    methods : {
        setSettingType(type){
            this.settingType = type
        },
        getButtonStyle(type){
            if (this.settingType == type){
                return "px-6 py-1 rounded-t-lg bg-gray-50 hover:bg-gray-100"
            } else {
                return "px-6 py-1 rounded-t-lg bg-gray-300 hover:bg-gray-200"
            }
        }
    }
}
  
</script>

<style>

</style>