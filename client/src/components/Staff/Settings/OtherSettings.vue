<template>
     <div class="text-3xl font-bold">Others</div>
     <div class="mt-8 flex flex-col gap-5">
        <div class="flex gap-4">
            <span class="p-1 font-bold">Center Target</span>
            <input type="number" class="p-1 border rounded-lg">           
        </div>
        <div class="flex gap-4">
            <span class="p-1">Max Class Size</span>
            <input type="number" class="p-1 border rounded-lg">           
        </div>
        <div class="flex gap-4">
            <span class="p-1">Lessons Target</span>
            <input type="number" class="p-1 border rounded-lg">           
        </div>
        <div class="flex gap-4">
            <span class="p-1 font-bold">Tuition Price</span>
            <input type="number" class="p-1 border rounded-lg">           
        </div>
        <div class="flex gap-4">
            <span class="p-1">Tuition debt max remind times</span>
            <input type="number" class="p-1 border rounded-lg">           
        </div>
        <div class="flex gap-4">
            <span class="p-1">Remind tuition after</span>
            <input type="number" class="p-1 border rounded-lg">         
            <span class="p-1">weeks</span>
        </div>
        <div class="flex gap-4">
            <span class="p-1">Automatically annouce tuition </span>
            <input type="number" class="p-1 border rounded-lg">         
            <span class="p-1">days before new month </span>
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
    name : "OtherSettings"
}
</script>

<style>

</style>