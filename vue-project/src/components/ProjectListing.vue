<script setup>
import { RouterLink } from 'vue-router'
import { defineProps, ref, computed } from 'vue';
import { onMounted } from 'vue';
import placeholderImage from '@/assets/img/placeholder.png'
import axios from 'axios'

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
        description = description.substring(0, 30) + '...';
    }
    return description;
});

const imageUrl = ref(placeholderImage);

const fetchImage = async () => {
    if (!props.project.thumbnail){
        console.warn('No thumbnail specified for project: ', props.title);
        return; // Use placeholder image
    }
    try {
        console.log(props.project.thumbnail)
        const encodedThumbnail = encodeURIComponent(props.project.thumbnail);
        // example request for image url: http://localhost:5283/api/caitlinthaeler/portfolio_content/image/sk8-run/thumbnail.png
        const response = await axios.get(`http://localhost:5283/api/caitlinthaeler/portfolio_content/image/${encodedThumbnail}`,);
        console.log(response)
        // Create a URL for the blob data
        imageUrl.value = response.data.imageUrl;
        console.log(imageUrl.value);
    } catch (error) {
        console.error("Error fetching image", error);
    }
}

onMounted(fetchImage);


</script>

<template>
    <div>
        <div class="bg-lemon sharp-xl shadow-md relative">
        <div class="p-4">
            <div class="mb-6">
                <h3 class="text-3xl text-center font-bold">{{ project.title }}</h3>
                <div class="text-gray-600 my-2 text-sm">{{ project.formattedStartDate }} - {{ project.formattedEndDate }}</div>
            </div>

            <div class="mb-7">
                <img :src="imageUrl" alt="Embedded Image">
            </div>

            <div class="mb-5">
                <!-- <div>
                    {{ truncatedDescription }}
                </div>
                <button @click="toggleFullDescription" class="text-green-500 hover:text-green-600 mb-5">
                    {{ showFullDescription ? 'Less' : 'More' }}
                </button> -->
            </div>
            <div class="border border-gray-100 mb-5"></div>

            <div class="flex flex-row lg:flex-row mb-4">
                <RouterLink 
                    :to="'/projects/' + project.id"
                    class="h-[36px] bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-lg text-center text-sm"
                >
                    Github
            </RouterLink>
            <RouterLink 
                    :to="'/projects/' + project.id"
                    class="h-[36px] bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-lg text-center text-sm"
                >
                    Live Demo
            </RouterLink>
                <RouterLink 
                    :to="'/projects/' + project.id"
                    class="h-[36px] bg-plum hover:bg-green-600 text-white px-4 py-2 rounded-lg text-center text-sm"
                >
                    Read More
            </RouterLink>
            </div>
        </div>
    </div>
    </div>
    
</template>