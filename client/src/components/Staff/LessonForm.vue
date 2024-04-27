<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">{{ this.title }}</div>
        <div class="flex gap-4 justify-center mt-4">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="null">Ok</button>
            <button class="p-2 text-red-400 underline font-bold"
                @click="close">Cancel</button>
        </div>
    </div>
</template>

<script>
export default {
    name: "LessonForm",
    inject: ['eventBus'],
    props: ['close', 'lessonId','title'],
    methods : {

    }
}
</script>

<style></style>