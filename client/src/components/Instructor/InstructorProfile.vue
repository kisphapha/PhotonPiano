<template>
    <div v-if="instructor" class="p-8">
        <div class="font-bold text-3xl ml-8">
            {{ instructor.user.name }}
        </div>
        <div class="flex gap-12">
            <img :src="instructor.user.picture" class="w-48 h-48 rounded-full">
            <div class="mt-4">
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">Name :</span>
                        <span>{{ instructor.user.name }}</span>
                    </li>
                </ul>
                <hr>
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">Email :</span>
                        <span>{{ instructor.user.email }}</span>
                    </li>
                </ul>
                <hr>
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">Phone :</span>
                        <span>{{ instructor.user.phone }}</span>
                    </li>
                </ul>
                <hr>
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">DoB :</span>
                        <span>{{ instructor.user.dob }}</span>
                    </li>
                </ul>
                <hr>
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">Gender :</span>
                        <span>{{ instructor.user.gender }}</span>
                    </li>
                </ul>
                <hr>
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">Address :</span>
                        <span>{{ instructor.user.address }}</span>
                    </li>
                </ul>
                <hr>
                <ul class="list-disc">
                    <li class="text-lg flex">
                        <span class="w-[10rem] font-bold">Bank Account :</span>
                        <span>{{ instructor.user.bankAccount }}</span>
                    </li>
                </ul>
            </div>
        </div>
        <div class="flex place-content-between mt-4 w-full">
            <div class="flex gap-4 text-xl font-bold">
                <span class="p-2">Join Date :</span>
                <span class="p-2 text-blue-500">{{ instructor.joinDate }}</span>
            </div>
            <div class="flex gap-4 text-xl font-bold">
                <span class="p-2">Contribute Score :</span>
                <span class="p-2 text-green-500">{{ instructor.contributeScore }}</span>
            </div>
            <div class="flex gap-4 text-xl font-bold">
                <span class="p-2">Status :</span>
                <span :class="getStatusStyle(instructor.status)">{{ instructor.status }}</span>
            </div>
        </div>
        <div class="mt-4">
            <div class="font-bold text-2xl">
                Classes Teaching
            </div>
            <div class="mt-2 grid grid-cols-1 gap-8 md:grid-cols-2 lg:grid-cols-4" >
                <button v-for="class_ in instructor.classes" :key="class_.id"
                    class="rounded-lg shadow-md hover:shadow-xl">
                    <div :class="getStyleClassHeader(class_.status)">{{ class_.name }}</div>
                    <div class="p-4 text-left">
                        <div :class="getLevelStyle(class_.level)"><span>Level : </span>{{ class_.level }}</div>
                        <div><span class="font-bold">Status : </span>{{ class_.status }}</div>
                        <div><span class="font-bold">Duration : </span>{{ class_.startDate }} - {{ class_.endDate }}
                        </div>
                    </div>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "InstructorProfile",
    props: ['instructorId'],
    data() {
        return {
            instructor: {
                id: 1,
                joinDate: "2024-01-01",
                contributeScore: 6969,
                status: "Active",
                user: {
                    name: "Diamond",
                    email: "doamond@gmail.com",
                    phone: "1234567890",
                    picture: "/src/assets/noavatar.jpg",
                    dob: "2000-01-01",
                    gender: "Female",
                    address: "Earth 797979",
                    bankAccount: "1020788689 VCB"
                },
                classes: [
                    {
                        id: 1,
                        name: "BLUE_68_2024",
                        level: 1,
                        startDate: "2024-01-01",
                        endDate: "2024-12-31",
                        status: "Active"
                    },
                    {
                        id: 2,
                        name: "BLUE_68_2024",
                        level: 2,
                        startDate: "2024-01-01",
                        endDate: "2024-12-31",
                        status: "Active"
                    },
                    {
                        id: 3,
                        name: "BLUE_68_2024",
                        level: 3,
                        startDate: "2024-01-01",
                        endDate: "2024-12-31",
                        status: "Active"
                    },
                    {
                        id: 4,
                        name: "BLUE_68_2024",
                        level: 4,
                        startDate: "2024-01-01",
                        endDate: "2024-12-31",
                        status: "Finished"
                    },
                    {
                        id: 5,
                        name: "BLUE_68_2024",
                        level: 5,
                        startDate: "2024-01-01",
                        endDate: "2024-12-31",
                        status: "Finished"
                    },
                ]
            }
        }
    },
    methods: {
        getStatusStyle(status) {
            let css = "p-2 rounded-lg text-white "
            switch (status) {
                case "Active":
                    return css + "bg-green-500";
                case "Inactive":
                    return css + "bg-gray-500";
                case "Fired":
                    return css + "bg-red-500";
            }
            return css;
        },
        getStyleClassHeader(status) {
            let css = "py-2 font-bold text-white rounded-t-lg "
            switch (status) {
                case "Active":
                    return css + "bg-green-600";
                case "Finished":
                    return css + "bg-gray-500";
            }
            return css;
        },
        getLevelStyle(level) {
            let css = "font-bold "
            switch (level) {
                case 1:
                    return css + "text-blue-400";
                case 2:
                    return css + "text-pink-400";
                case 3:
                    return css + "text-red-400";
                case 4:
                    return css + "text-black";
                case 5:
                    return css + "text-gray-300";
            }
            return css;
        }
    }
}
</script>

<style></style>