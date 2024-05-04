<template>
    <div class="text-3xl font-bold">Features</div>
    <div class="mt-8 flex flex-col gap-5">
        <div class="flex gap-4">
            <input type="checkbox">
            Registration
        </div>
        <div class="flex gap-4">
            <input type="checkbox">
            Entrance Test Features
        </div>
        <div class="flex gap-4">
            <input type="checkbox">
            Create / Edit / Delete Classes
        </div>
        <div class="flex gap-4">
            <input type="checkbox">
            Changing Classes
        </div>
        <div class="flex gap-4">
            <input type="checkbox">
            Changing Schedule
        </div>
        <div class="flex gap-4">
            <input type="checkbox">
            Outside Access to Forum
        </div>
    </div>
    <div class="flex gap-4 mt-4 justify-center">
        <button @click="handleSave"
            class="bg-blue-400 hover:bg-blue-200 px-6 py-2 rounded-lg text-white font-bold">OK</button>
        <button class="p-2 text-red-400 underline font-bold" @click="handleReset">Reset</button>
    </div>
</template>

<script>
export default {
    name: "FeatureSettings"
}
</script>

<style></style>