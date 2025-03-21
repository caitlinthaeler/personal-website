<script setup>
import { RouterLink } from 'vue-router'
import { defineProps, ref, computed } from 'vue';
import { onMounted } from 'vue';
import placeholderImage from '@/assets/img/placeholder.png'
import { fetchImage } from '@/utils/fetchImage.js';
import axios from 'axios'

const props = defineProps({
    project: {
        type: Object,
        required: true,
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

const hasGithub = computed(() => {
    return props.project.githubLink != '';
});

const imageUrl = ref(placeholderImage);

const getImage = async () => {
    if (!props.project.thumbnail){
        console.warn('No thumbnail specified for project: ', props.title);
        return; // Use placeholder image
    }
    try {
        const response = await fetchImage(props.project.thumbnail.filePath);
        if (response){
            imageUrl.value = response;
        }
    } catch (error) {
        console.error("Error fetching image", error);
    }
}

onMounted(getImage);


</script>

<template>
    <div class="justify-self-center">
        <div class="bg-lemon text-grape sharp-xl shadow-md w-[400px] transition-transform duration-300 ease-in-out hover:scale-105">
        <div class="p-4 ">
            <div class="mb-6 ">
                <h3 class="text-3xl text-center font-bold">{{ project.title }}</h3>
                <div class="text-gray-600 my-2 text-sm">{{ project.formattedStartDate }} - {{ project.formattedEndDate }}</div>
            </div>

            <div class="mb-5">
                <img :src="imageUrl" alt="Embedded Image" >
            </div>

            <div class="mb-5">
                <!-- <div>
                    {{ truncatedDescription }}
                </div>
                <button @click="toggleFullDescription" class="text-green-500 hover:text-green-600 mb-5">
                    {{ showFullDescription ? 'Less' : 'More' }}
                </button> -->
            </div>
            <div class="mb-4 text-sm text-grape">
                <div v-for="skill in project.skills" :key="skill" class="inline-block bg-plum px-2 py-1 rounded-md m-1">
                    {{ skill }}
                </div>
            </div>
 
            <div class="border border-gray-100 mb-5"></div>

            

            <div class="flex flex-row lg:flex-row">
                <a
                    :href="hasGithub ? project.githubLink : '#'"
                    :target="hasGithub ? '_blank' : null"
                    :rel="hasGithub ? 'noopener noreferrer' : null" 
                    class="h-[36px] hover:underline hover:underline-offset-8 hover:text-lemon-dark px-4 py-2 text-center text-md transition-transform duration-300 ease-in-out hover:scale-110"                >
                    Github
                </a>
                <RouterLink 
                        :to="'/projects/' + project.id"
                        class="h-[36px] hover:underline hover:underline-offset-8 hover:text-lemon-dark px-4 py-2 text-center text-md transition-transform duration-300 ease-in-out hover:scale-110"                >
                        Live Demo
                </RouterLink>
                    <RouterLink 
                        :to="'/projects/' + project.title"
                        class="h-[36px] hover:underline hover:underline-offset-8 hover:text-lemon-dark px-4 py-2 text-center text-md transition-transform duration-300 ease-in-out hover:scale-110"
                    >
                        Read More
                </RouterLink>
            </div>
            
        </div>
    </div>
    </div>
    
</template>