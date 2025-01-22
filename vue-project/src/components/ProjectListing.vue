<script setup>
import { RouterLink } from 'vue-router'
import { defineProps, ref, computed } from 'vue';

const props = defineProps({
    project: {
        type: Object,
        required: true
    }
});

const showFullDescription = ref(false);

const toggleFullDescription = () => {
    showFullDescription.value = !showFullDescription.value;
}

const truncatedDescription = computed(() => {
    let description = props.project.description;
    if (!showFullDescription.value) {
        description = description.substring(0, 90) + '...';
    }
    return description;
});
</script>

<template>
    <div>
        <div class="bg-white rounded-xl shadow-md relative">
        <div class="p-4">
            <div class="mb-6">
                <div class="text-gray-600 my-2">{{ project.startDate }} - {{ project.endDate }}</div>
                <h3 class="text-xl font-bold">{{ project.title }}</h3>
            </div>

            <div class="mb-5">
                <div>
                    {{ truncatedDescription }}
                </div>
                <button @click="toggleFullDescription" class="text-green-500 hover:text-green-600 mb-5">
                    {{ showFullDescription ? 'Less' : 'More' }}
                </button>
            </div>
            <div class="border border-gray-100 mb-5"></div>

            <div class="flex flex-col lg:flex-row justify-between mb-4">
                <RouterLink 
                    :to="'/projects/' + project.id"
                    class="h-[36px] bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-lg text-center text-sm"
                >
                    Read More
            </RouterLink>
            </div>
        </div>
    </div>
    </div>
    
</template>